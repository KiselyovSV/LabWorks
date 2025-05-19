using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Grad_WebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не указан E-mail")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [StringLength(100, ErrorMessage = "{0} не может быть менее {2} и более {1} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
