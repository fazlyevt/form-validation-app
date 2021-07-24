using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ValidationApp.Models;

namespace ValidationApp.Pages
{
	public class IndexModel : PageModel
	{
		private readonly UserDbContext _context;

		public IndexModel(UserDbContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		public User user { get; set; }

		// вспомогательные копии свойств из модели User для клиентской валидации
		// т.к. PageRemote некорректно работает с nested объектами
		[BindProperty]
		[Required(ErrorMessage = "Укажите имя учетной записи")]
		[PageRemote(
			ErrorMessage = "Имя учетной записи уже используется",
			AdditionalFields = "__RequestVerificationToken",
			HttpMethod = "post",
			PageHandler = "CheckLogin"
			)]
		[Display(Name = "Имя учетной записи")]
		public string Login { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Введите адрес электронной почты")]
		[EmailAddress(ErrorMessage = "Некорректный адрес почты")]
		[PageRemote(
			ErrorMessage = "Адрес электронной почты уже используется",
			AdditionalFields = "__RequestVerificationToken",
			HttpMethod = "post",
			PageHandler = "CheckEmail"
			)]
		[Display(Name = "Адрес электронной почты")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		public async Task<JsonResult> OnPostCheckLoginAsync(string login)
		{
			var loginExists = await _context.Users.AnyAsync(o => o.Login.Equals(login));
			return new JsonResult(!loginExists);
		}

		public async Task<JsonResult> OnPostCheckEmailAsync(string email)
		{
			var emailExists = await _context.Users.AnyAsync(o => o.Email.Equals(email));
			return new JsonResult(!emailExists);
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (await _context.Users.AnyAsync(o => o.Login.Equals(Login)))
				ModelState.AddModelError("Login", "Имя учетной записи уже используется");

			if (await _context.Users.AnyAsync(o => o.Email.Equals(Email)))
				ModelState.AddModelError("Email", "Адрес электронной почты уже используется");

			if (!ModelState.IsValid)
				return Page();

			user.Login = Login;
			user.Email = Email;

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return RedirectToPage("/Success", new { userId = user.UserId });
		}
	}
}
