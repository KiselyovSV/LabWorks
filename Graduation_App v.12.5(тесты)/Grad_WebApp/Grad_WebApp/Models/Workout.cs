using System.ComponentModel.DataAnnotations;

namespace Grad_WebApp.Models
{
    public class Workout
    {
        [Display(Name = "Id тренировки")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указан тип"), Display(Name = "Тип тренировки"), StringLength(50)]
        public string? Type { get; set; }

        [Required(ErrorMessage = "Нет описания"), Display(Name = "Описание тренировки"), StringLength(500)]
        public string? Description { get; set; }

        [Display(Name = "Актуальность (да/нет)")]
        public bool IsActive { get; set; } = true;
    }
}
