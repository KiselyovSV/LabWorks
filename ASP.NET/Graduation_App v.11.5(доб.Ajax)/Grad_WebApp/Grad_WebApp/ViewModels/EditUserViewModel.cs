using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Grad_WebApp.ViewModels
{
    public class EditUserViewModel
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public int? Year { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
