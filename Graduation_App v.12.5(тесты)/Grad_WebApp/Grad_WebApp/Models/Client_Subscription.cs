using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Grad_WebApp.Models
{
    public class Client_Subscription
    {
        public int Id { get; set; }

        [Display(Name = "Ф.И.О клиента")]
        public int ClientId { get; set; }

        [Display(Name = "Ф.И.О клиента")]
        public Client? Client { get; set; }

        [Display(Name = "Название абонемента")]
        public int SubscriptionId { get; set; }

        [Display(Name = "Название абонемента")]
        public Subscription? Subscription { get; set; }

        private DateTime startDate;

        [DataType(DataType.Date), Display(Name = "Дата покупки (начала действия)")]
        [Required(ErrorMessage = "Не указана дата")]
        public DateTime StartDate
        {
            get
            {
                TimeZoneInfo local = TimeZoneInfo.Local;
                TimeSpan offset = local.GetUtcOffset(this.startDate);
                Debug.WriteLine($"Смещение составляет:{offset.Hours}");
                return startDate.AddHours(offset.Hours);
            }
            set { startDate = value.ToUniversalTime(); }
        }

        private DateTime endDate;

        [DataType(DataType.Date), Display(Name = "Дата окончания")]
        //[Required(ErrorMessage = "Не указана дата")]
        public DateTime EndDate
        {
            get
            {
                TimeZoneInfo local = TimeZoneInfo.Local;
                TimeSpan offset = local.GetUtcOffset(this.endDate);
                Debug.WriteLine($"Смещение составляет:{offset.Hours}");
                return endDate.AddHours(offset.Hours);
            }
            set { endDate = value.ToUniversalTime(); } 
        }
        [Display(Name = "Цена с учетом скидки"), Range(0, Double.MaxValue)] /*[Required(ErrorMessage = "Не указана цена со скидкой")]*/
        public double? CostIncludingDiscount { get; set; }

        [Display(Name = "Осталось тренировок"), Range(0, 365)]
        public int? Trainings { get; set; } 
             
    }
}
