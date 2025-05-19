using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Grad_WebApp.Models
{
    public class Timetable
    {
        public int Id { get; set; }

        [Display(Name = "Тип тренировки")]
        public int WorkoutId { get; set; }

        [Display(Name = "Тип тренировки")]
        public Workout? Workout { get; set; }

        [Display(Name = "Ф.И.О. тренера")]
        public int CoachId { get; set; }

        [Display(Name = "Тренер")]
        public Coach? Coach { get; set; }

        private DateTime date;

        [Display(Name = "Дата и время проведения")]
        [Required(ErrorMessage = "Не указаны дата и время")]
        public DateTime Date
        {
            get
            {
                TimeZoneInfo local = TimeZoneInfo.Local;
                TimeSpan offset = local.GetUtcOffset(this.date);
                Debug.WriteLine($"Смещение составляет:{offset.Hours}");
                return date.AddHours(offset.Hours);
            }
            set { date = value.ToUniversalTime(); }
        }

        public override string? ToString()
        {
            if (string.IsNullOrEmpty(Id.ToString())) return base.ToString();
            return $"\nДата проведения: {Date}\n Тип тренировки: {Workout!.Type}\n " +
            $"Описание тренировки: {Workout!.Description}\n Тренер: {Coach!.LastName} {Coach!.FirstName}\n";
        }

    }
}
