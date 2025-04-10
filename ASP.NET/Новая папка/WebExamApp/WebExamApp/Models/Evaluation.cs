using System.ComponentModel.DataAnnotations;

namespace WebExamApp.Models
{
    public class Evaluation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указана оценка"), Display(Name = "Оценка")]
        public string? Name { get; set; } 
    }
}
