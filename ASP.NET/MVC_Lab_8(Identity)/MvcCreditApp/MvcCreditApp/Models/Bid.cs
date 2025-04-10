using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations;

namespace MvcCreditApp1.Models
{
    public class Bid
    {
        // ID заявки
        public virtual int BidId { get; set; }
        // Имя заявителя
        [Required(ErrorMessage = "Не указано имя"),Display(Name = "Имя заявителя")]
        public virtual string? Name { get; set; }
        // Название кредита
        [Required(ErrorMessage = "Не указано название кредита"),Display(Name = "Выбор названия кредита:")]
        public virtual string? CreditHead { get; set; }
        // Дата подачи заявки

        private DateTime bid_date;

        [DataType(DataType.Date),Display(Name = "Дата подачи заявки")]
        [Required(ErrorMessage = "Не указана дата")]
        public virtual DateTime bidDate 
        { 
          get { return bid_date; } 
          set { bid_date = value.ToUniversalTime(); }
        }

        public override string? ToString()
        {
            if (string.IsNullOrEmpty(BidId.ToString())) return base.ToString();
            return $"\nID заявки: {BidId}\n Имя заявителя: {Name}\n Название кредита: {CreditHead}\n Дата подачи заявки: {bidDate}";
        }
    }
}
