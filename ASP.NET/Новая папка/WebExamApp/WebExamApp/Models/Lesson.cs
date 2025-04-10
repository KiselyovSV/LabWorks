using System.ComponentModel.DataAnnotations;

namespace WebExamApp.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано название темы"), Display(Name = "Тема урока")]
        public string? Name { get; set; }
    }
}
