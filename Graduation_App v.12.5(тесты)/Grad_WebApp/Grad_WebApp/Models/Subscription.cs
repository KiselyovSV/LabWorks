using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Grad_WebApp.Models
{
    public class Subscription
    {
        [Display(Name = "Id абонемента")]
        public int Id { get; set; }

        [Display(Name = "Название"), Required(ErrorMessage = "Не указано название"), StringLength(50)]
        public string? Name { get; set; }

        [Display(Name = "Количество тренировок"), Required(ErrorMessage = "Не указано количество"), Range(0, 365)]  
        public int? AmountOfTrainings { get; set; } 

        [Display(Name = "Цена"), Required(ErrorMessage = "Не указана цена"), Range(0.0, Double.MaxValue)]
        public double? Cost { get; set; } 

        [Display(Name = "Период действия (дней)"), Required(ErrorMessage = "Не указан период"), Range(0, 365)]
        public int? Period { get; set; } 

        [Display(Name = "Описание"), Required(ErrorMessage = "Нет описания"), StringLength(250)]
        public string? Description { get; set; }

        [Display(Name = "Актуальность (да/нет)")]
        public bool IsActive { get; set; } = true;

        
    }
}
