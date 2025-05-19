using System.ComponentModel.DataAnnotations;

namespace Grad_WebApp.Models
{
    public class Coach
    {
        [Display(Name = "Id тренера")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя"), Display(Name = "Имя")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Не указано отчество"), Display(Name = "Отчество")]
        public string? Patronymic { get; set; }

        [Required(ErrorMessage = "Не указана фамилия"), Display(Name = "Фамилия")]
        public string? LastName { get; set; }
            
        [Display(Name = "Адрес электронной почты")]
        [Required(ErrorMessage = "Адрес E-mail обязателен")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string? Email { get; set; }

        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Номер телефона обязателен")]
        [RegularExpression(@"^\+?[1-9]+[0-9]{6,10}$", ErrorMessage = "Некорректный номер")]
        public string? Phone { get; set; }

        [Display(Name = "Образование и сертификаты"), StringLength(500)]
        public string? Information { get; set; }

        [Display(Name = "Комментарий"), StringLength(250)]
        public string? Сomment { get; set; }
    }
}
