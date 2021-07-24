using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ValidationApp.Models
{
	public class User
	{
		public int UserId { get; set; }

		[Display(Name = "Имя учетной записи")]
		public string Login { get; set; }

		[Display(Name = "Адрес электронной почты")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required(ErrorMessage = "Укажите номер телефона")]
		[RegularExpression(@"\+\d \(\d{3}\) \d{3}\-\d{4}", ErrorMessage = "Номер введен неверно")]
		[Display(Name = "Номер телефона")]
		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Введите пароль")]
		[StringLength(100, MinimumLength = 10, ErrorMessage = "Пароль не должен быть короче 10 символов")]
		[Display(Name = "Пароль учетной записи")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Подтвердите пароль")]
		[Compare("Password", ErrorMessage = "Пароль не совпадает с подтверждением")]
		[Display(Name = "Подтверждение пароля")]
		[DataType(DataType.Password)]
		public string PasswordConfirmation { get; set; }

		[Required(ErrorMessage = "Введите свое имя")]
		[Display(Name = "Имя")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Введите свою фамилию")]
		[Display(Name = "Фамилия")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Выберите дату своего рождения")]
		[Display(Name = "Дата рождения")]
		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }
	}
}
