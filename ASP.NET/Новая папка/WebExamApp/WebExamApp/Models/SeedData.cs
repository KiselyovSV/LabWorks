using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using WebExamApp.Data;

namespace WebExamApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StatementDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<StatementDbContext>>()))
            {
                if (context == null || context.Evaluations == null || context.Lessons == null || context.Students == null || context.Statement == null)
                {
                    throw new ArgumentNullException("Null StatementDbContext");
                }
                // Если в базе данных уже есть кредиты,
                // то возвращается инициализатор заполнения и кредиты не добавляются
                if (context.Statement.Any() || context.Statement.Any() || context.Students.Any() || context.Lessons.Any() || context.Evaluations.Any())
                {
                    return;
                }
                Evaluation one = new() { Name = "1" };
                Evaluation two = new() { Name = "2" };
                Evaluation three = new() { Name = "3" };
                Evaluation four = new() { Name = "4" };
                Evaluation five = new() { Name = "5" };
                Evaluation setOff = new() { Name = "Зачёт" };
                Evaluation notSetOff = new() { Name = "Незачёт" };
                context.Evaluations.AddRange(one, two, three, four, five, setOff, notSetOff);

                Lesson mvc_1 = new() { Name = "Введение в разработку ASP.NET MVC приложений" };
                Lesson mvc_2 = new() { Name = "ASP.NET MVC Представления" };
                Lesson mvc_3 = new() { Name = "MVC паттерн, контроллеры, модель" };
                Lesson mvc_4 = new() { Name = "ASP.NET MVC Валидация на стороне клиента" };
                Lesson mvc_5 = new() { Name = "Создание  стилизованных страниц в ASP.NET MVC" };
                Lesson mvc_6 = new() { Name = "Авторизация и аутентификация в ASP.NET MVC 5" };
                context.Lessons.AddRange(mvc_1, mvc_2, mvc_3, mvc_4, mvc_5, mvc_6);

                Student st1 = new() { FirstName = "Эльвира", Patronymic ="Сахипзадовна", LastName ="Набиулина", Address="ул.Ленина,д.8, кв.15", Email ="nab@gmail.com", Phone ="89823456718"};
                Student st2 = new() { FirstName = "Владимир", Patronymic = "Владимирович", LastName = "Путин", Address = "ул.Марата,д.34, кв.89", Email = "BOSS@ya.ru", Phone = "89257777777" };
                Student st3 = new() { FirstName = "Вадим", Patronymic = "Вениаминович", LastName = "Бурундуков", Address = "пр.Лиговский,д.48, кв.9", Email = "Bvv@mail.ru", Phone = "89693408112" };
                Student st4 = new() { FirstName = "Ольга", Patronymic = "Олеговна", LastName = "Ивашова", Address = "ул.Блохина,д.22, кв.54", Email = "", Phone = "3440115" };
                context.Students.AddRange(st1, st2, st3, st4);
                context.SaveChanges();
            }
        }
    }
}
    

