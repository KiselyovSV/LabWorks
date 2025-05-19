using System.ComponentModel.DataAnnotations;

namespace Grad_WebApp.Models
{
    public class CoachPhoto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }

        [Display(Name = "Ф.И.О. тренера")]
        public int CoachId { get; set; }

        [Display(Name = "Тренер")]
        public Coach? Coach { get; set; }
    }
}
