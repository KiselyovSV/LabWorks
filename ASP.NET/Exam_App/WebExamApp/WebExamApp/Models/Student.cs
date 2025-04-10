using System.ComponentModel.DataAnnotations;

namespace WebExamApp.Models
{
    public class Student
    {
        [Display(Name = "Ф.И.О. студента")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя"), Display(Name = "Имя студента")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Не указано отчество"), Display(Name = "Отчество студента")]
        public string? Patronymic { get; set; }

        [Required(ErrorMessage = "Не указана фамилия"), Display(Name = "Фамилия студента")]
        public string? LastName { get; set; }

        [Display(Name = "Адрес электронной почты")]
        [Required(ErrorMessage = "Адрес E-mail обязателен")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string? Email { get; set; }

        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Номер телефона обязателен")]
        [RegularExpression(@"^\+?[1-9]+[0-9]{6,10}$", ErrorMessage = "Некорректный номер")]
        public string? Phone { get; set; }

        [Display(Name = "Адрес")]
        public string? Address { get; set; }

    }
}
