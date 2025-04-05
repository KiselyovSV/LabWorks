using System.ComponentModel.DataAnnotations;

namespace MvcCreditApp.Models
{
    public class Bid
    {
        // ID заявки
        public virtual int BidId { get; set; }
        // Имя заявителя
        public virtual string Name { get; set; }
        // Название кредита
        public virtual string CreditHead { get; set; }
        // Дата подачи заявки

        private DateTime bid_date;

        [DataType(DataType.Date)]
        public virtual DateTime bidDate 
        { 
          get { return bid_date; } 
          set { bid_date = value.ToUniversalTime(); }
        }
    }
}
