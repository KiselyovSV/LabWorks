

using System.ComponentModel.DataAnnotations;

namespace MvcDataViews.Models
{
    public class Person
    {

        [Required(ErrorMessage = "Не указан Id")]
        [Range(1, int.MaxValue, ErrorMessage = "Значение должно быть больше 0")]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Ввод имени обязателен")]
        [Display(Name = "Имя")]
        public string? Name { get; set; }

        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Не указан возраст")]
        [Range(1, 110, ErrorMessage = "Недопустимый возраст")]
        public int? Age { get; set; }

        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Номер телефона обязателен")]
        [RegularExpression(@"^\+?[1-9]+[0-9]{6,10}$", ErrorMessage = "Некорректный номер")]
        public string? Phone { get; set; }

        [Display(Name = "Адрес электронной почты")]
        [Required(ErrorMessage = "Адрес E-mail обязателен")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string? Email { get; set; }
    }
}
