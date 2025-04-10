using Humanizer;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace WebExamApp.Models
{
    public class Statement
    {
        public int Id { get; set; }

        [Display(Name = "Ф.И.О. студента")]
        public int StudentId { get; set; }

        [Display(Name = "Ф.И.О. студента")]
        public Student? Student { get; set; }

        [Display(Name = "Тема урока")]
        public int LessonId { get; set; }

        [Display(Name = "Тема урока")]
        public Lesson? Lesson { get; set; }

        [Display(Name = "Оценка")]
        public int EvaluationId { get; set; }

        [Display(Name = "Оценка")]
        public Evaluation? Evaluation { get; set; }

        private DateTime date;

        [DataType(DataType.Date), Display(Name = "Дата")]
        [Required(ErrorMessage = "Не указана дата")]
        public DateTime Date 
        {
            get { TimeZoneInfo local = TimeZoneInfo.Local;
                  TimeSpan offset = local.GetUtcOffset(this.date);
                Debug.WriteLine($"Смещение составляет:{offset.Hours}");
                  return date.AddHours(offset.Hours); } 
            set { date = value.ToUniversalTime(); }
        }

        public override string? ToString()
        {
            if (string.IsNullOrEmpty(Id.ToString())) return base.ToString();
            return $"\nID: {Id}\n Ф.И.О. студента: {Student?.LastName} {Student?.FirstName} {Student?.Patronymic}\n " +
            $"Тема: {Lesson?.Name}\n Оценка: {Evaluation?.Name}\n Дата: {Date}";
        }

    }
}
