using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Grad_WebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Не указан E-mail")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Не указан год рождения"), Range(1900,2100)]
        [Display(Name = "Год рождения")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [StringLength(100, ErrorMessage = "{0} не может быть менее {2} и более {1} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        public string? PasswordConfirm { get; set; }
    }
}
