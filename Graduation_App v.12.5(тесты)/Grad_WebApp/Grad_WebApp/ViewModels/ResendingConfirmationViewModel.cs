using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Grad_WebApp.ViewModels
{
    public class ResendingConfirmationViewModel
    {
        [Required(ErrorMessage = "Не указан E-mail")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string? Email { get; set; }
    }
}
