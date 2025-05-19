using System.ComponentModel.DataAnnotations;

namespace Grad_WebApp.Models
{
    public class Timetable_Client
    {
        public int Id { get; set; }

        [Display(Name = "Дата, время, Id тренировки и название")]
        public int TimetableId { get; set; }

        [Display(Name = "Дата, время, Id тренировки и название")]
        public Timetable? Timetable { get; set; }

        [Display(Name = "Ф.И.О. клиента")]
        public int СlientId { get; set; }

        [Display(Name = "Ф.И.О. клиента")]
        public Client? Сlient { get; set; }
    }
}
