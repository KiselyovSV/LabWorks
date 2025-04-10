using System.ComponentModel.DataAnnotations;

namespace WebExamApp.Models
{
    public class Lesson
    {
        [Display(Name = "Темы занятий")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано название темы"), Display(Name = "Тема занятия")]
        public string? Name { get; set; }
    }
}
