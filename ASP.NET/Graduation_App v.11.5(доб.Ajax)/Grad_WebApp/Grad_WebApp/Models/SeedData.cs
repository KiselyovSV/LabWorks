using Grad_WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace Grad_WebApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FitnessDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<FitnessDbContext>>()))
            {
                if (context == null || context.Client_Subscriptions == null || context.Subscriptions == null || context.Timetables == null || context.Timetable_Clients == null
                    || context.Workouts == null || context.Clients == null || context.Сoachs == null)
                {
                    throw new ArgumentNullException("Null StatementDbContext");
                }
                // Если в базе данных уже есть,
                // то возвращается инициализатор заполнения и не добавляются
                if (context.Client_Subscriptions.Any() || context.Subscriptions.Any() || context.Timetables.Any() || context.Timetable_Clients.Any()
                    || context.Workouts.Any() || context.Clients.Any() || context.Сoachs.Any())
                {
                    return;
                }
                Coach coach1 = new()
                {
                    FirstName = "Инна",
                    Patronymic = "Семеновна",
                    LastName = "Эстрина",
                    Email = "estr@gmail.com",
                    Phone = "89814324418",
                    Information = "- Санкт-Петербургский гуманитарный университет профсоюзов\n" +
                    "- Сертифицированный тренер Romana's Pilates\n - Сертифицированный тренер программ Less Mills: Body Balance\n - Сертифицированный тренер групповых программ"
                };
                Coach coach2 = new()
                {
                    FirstName = "Олеся",
                    Patronymic = "Михайловна",
                    LastName = "Дзюбий",
                    Email = "tex_body@ya.ru",
                    Phone = "89620897977",
                    Information = "- Челябинский государственный педагогический университет\n" +
                    "- Сертифиципованный тренер ФФАР\n - Сертифицированный тренер Less Mills"
                };
                Coach coach3 = new()
                {
                    FirstName = "Денис",
                    Patronymic = "Петрович",
                    LastName = "Шумихин",
                    Email = "Shumden@mail.ru",
                    Phone = "89623850909",
                    Information = "- Национальный государственный университет физической культуры им.П.Ф.Лестгафта\n" +
                    "- Заслуженный тренер по боксу\n - Заслуженный мастер спорта по боксу "
                };
                Coach coach4 = new()
                {
                    FirstName = "Андрей",
                    Patronymic = "Сергеевич",
                    LastName = "Андреев",
                    Email = "Box@list.ru",
                    Phone = "89343441810",
                    Information = "- Волгоградский государственный институт искусств и культуры\n" +
                    "- Сертифицированный тренер Less Mills\n - Сертифицированный тренер Body Step Less Mills\n - Antygravity yoga, Antygravity Suspention fitness, Port de Bras"
                };
                context.Сoachs.AddRange(coach1, coach2, coach3, coach4);

                Workout wkt_1 = new() { Type = "Aqua Fitness", Description = "Силовая тренировка для продвинутых любителей аквафитнеса, направленная на укрепление мышц и снижение веса" };
                Workout wkt_2 = new() { Type = "TRX Blast", Description = "Функциональная тренировка с использованием специального оборудования ремней \"TRX\". Тренировка направлена на улучшение выносливости и сжигание лишних калорий" };
                Workout wkt_3 = new() { Type = "Make Body", Description = "Универсальная тренировка на снижение веса и укрепление всех основных мышечных групп" };
                Workout wkt_4 = new() { Type = "W Balance", Description = "Программа, сочетающая в себе базовые элементы Йоги, Тай-Чи, Пилатес. Контроль над дыханием, концентрация, гибкость и комплекс из силовых упражнений образуют целостную тренировочную систему, которая приведёт тело и разум в состояние равновесия." };
                Workout wkt_5 = new() { Type = "ZUMBA", Description = "Простая для восприятия танцевальная тренировка под зажигательные латиноамериканские и мировые ритмы" };
                Workout wkt_6 = new() { Type = "Functional training", Description = "Функциональный тренинг – энергоемкое занятие, в основе которого многосуставные движения во всех плоскостях" };
                Workout wkt_7 = new() { Type = "Здоровая спина", Description = "Силовые упражнения для укрепления мышц спины и пресса, направленные на поддержание правильной осанки.Подходит для всех уровней подготовки." };
               context.Workouts.AddRange(wkt_1, wkt_2, wkt_3, wkt_4, wkt_5, wkt_6, wkt_7);

                Subscription sub1 = new()
                {
                    Name = "Абонемент \"Фитнес 16 посещений - 60 дней\"",
                    AmountOfTrainings = 16,
                    Cost = 3900.0,
                    Period = 60,
                    Description = "Посещение ограниченного количества занятий в любой день недели и времени в соответствии с расписанием занятий школы фитнеса или тренажерных залов по индивидуальному плану\".",
                };
                Subscription sub2 = new()
                {
                    Name = "Абонемент \"4 занятия - 30 дней\"",
                    AmountOfTrainings = 4,
                    Cost = 1200.0,
                    Period = 30,
                    Description = "Посещение ограниченного количества занятий в любой день недели и времени в соответствии с расписанием занятий школы фитнеса или тренажерных залов по индивидуальному плану\".",
                };
                Subscription sub3 = new()
                {
                    Name = "Абонемент \"8 занятия - 30 дней\"",
                    AmountOfTrainings = 8,
                    Cost = 2000.0,
                    Period = 30,
                    Description = "Посещение ограниченного количества занятий в любой день недели и времени в соответствии с расписанием занятий школы фитнеса или тренажерных залов по индивидуальному плану\".",
                };
                Subscription sub4 = new()
                {
                    Name = "Абонемент \"Фитнес - 30 дней\"",
                    AmountOfTrainings = 30,
                    Cost = 3000.0,
                    Period = 30,
                    Description = "Посещение групповых занятий в любой день недели и времени в соответствии с расписанием занятий школы фитнеса или тренажерных залов по индивидуальному плану\".",
                };

                Subscription sub5 = new()
                {
                    Name = "Абонемент \"Фитнес - 90 дней\"",
                    AmountOfTrainings = 90,
                    Cost = 8100.0,
                    Period = 90,
                    Description = "Посещение групповых занятий в любой день недели и времени в соответствии с расписанием занятий школы фитнеса или тренажерных залов по индивидуальному плану\".",
                };

                Subscription sub6 = new()
                {
                    Name = "Разовое посещение",
                    AmountOfTrainings = 1,
                    Cost = 700.0,
                    Period = 1,
                    Description = "Посещение групповых занятий в любой день недели и времени в соответствии с расписанием занятий школы фитнеса или тренажерных залов по индивидуальному плану.",
                };
                context.Subscriptions.AddRange(sub1, sub2, sub3, sub4, sub5, sub6);
                context.SaveChanges();
            }
        }
    }
}
