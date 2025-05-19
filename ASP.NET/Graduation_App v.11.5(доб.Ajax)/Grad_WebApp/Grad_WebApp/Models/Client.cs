using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Grad_WebApp.Models
{
    public class Client
    {
        [Display(Name = "Id клиента")]
        public int Id { get; set; }

      [Required(ErrorMessage = "Не указано имя"), Display(Name = "Имя")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Не указано отчество"), Display(Name = "Отчество")]
        public string? Patronymic { get; set; }

        [Required(ErrorMessage = "Не указана фамилия"), Display(Name = "Фамилия")]
        public string? LastName { get; set; }

        [Display(Name = "Адрес электронной почты")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string? Email { get; set; }

        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Номер телефона обязателен")]
        [RegularExpression(@"^\+?[1-9]+[0-9]{6,10}$", ErrorMessage = "Некорректный номер")]
        public string? Phone { get; set; }

        public DateTime dateOfBirth;

        [Display(Name = "Дата рождения"), DataType(DataType.Date)]
        public DateTime DateOfBirth
         {
            get
            {
                TimeZoneInfo local = TimeZoneInfo.Local;
                TimeSpan offset = local.GetUtcOffset(this.dateOfBirth);
                Debug.WriteLine($"Смещение составляет:{offset.Hours}");
                return dateOfBirth.AddHours(offset.Hours);
            }
            set { dateOfBirth = value.ToUniversalTime(); }
        }

        [Display(Name = "Скидка"), Range(0, 25)]
        public int? Discount { get; set; } = 0;

        [Display(Name = "Комментарий"), StringLength(250)]
        public string? Сomment { get; set; }

        public string? UserId { get; set; }

        public User? User { get; set; }

    }
}
