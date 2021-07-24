using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValidationApp.Models;

namespace ValidationApp.Pages
{
    public class SuccessModel : PageModel
    {
        private readonly UserDbContext _context;

        public SuccessModel(UserDbContext context)
        {
            _context = context;
        }

        public User user { get; set; }
        public async Task<IActionResult> OnGet(int userId)
        {
            user = await _context.Users.FindAsync(userId);
			if (user != null)
			{
                return Page();
			}
			else
			{
                return NotFound();
			}
        }
    }
}
