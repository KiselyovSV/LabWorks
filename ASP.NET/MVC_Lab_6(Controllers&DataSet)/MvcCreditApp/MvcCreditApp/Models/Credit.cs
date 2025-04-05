using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MvcCreditApp1.Models
{
    public class Credit
    {
        // ID кредита
        public virtual int CreditId { get; set; }
        // Название
        [Required(ErrorMessage = "Не указано название"), Display(Name = "Название")]
        public virtual string? Head { get; set; }
        // Период, на который выдается кредит
        [Required(ErrorMessage = "Не указан период"), Display(Name = "Период")]
        public virtual int? Period { get; set; }
        // Максимальная сумма кредита
        [Required(ErrorMessage = "Не указана сумма кредита"),Display(Name = "Максимальная сумма кредита")]
        [Range(1, int.MaxValue, ErrorMessage = "Значение должно быть больше 0")]
        public virtual int? Sum { get; set; }
        // Процентная ставка
        [Required(ErrorMessage = "Не указана процентная ставка"), Display(Name = "Процентная ставка")]
        [Range(3, 25, ErrorMessage = "Значение должно быть от 3 до 25")]
        public virtual int? Procent { get; set; }
    }
}
