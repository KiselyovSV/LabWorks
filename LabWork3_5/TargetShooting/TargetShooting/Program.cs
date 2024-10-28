using System;

namespace TargetShooting
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int result = 0;
                byte counter = 0;
                Random rnd = new();
                int x2 = rnd.Next(0, 100);
                int y2 = rnd.Next(0, 100);
                
                for (int i = 1; i < 4; i++)
                {
                    Console.Write($"\nВведите значение X для {i}-ого выстрела от 0 до 100:\t");
                    int x = int.Parse(Console.ReadLine());
                    Console.Write($"\nВведите значение Y для {i}-ого выстрела от 0 до 100:\t");
                    int y = int.Parse(Console.ReadLine());
                    int misfire = rnd.Next();
                    // Проверка попадания в "яблочко" (радиус центральной окружности мишени равен 2)
                    if (misfire % 2 == 0)
                    {
                        Console.WriteLine($"\nОсечка!");
                        counter++;
                        continue; ;
                    }
                    else
                    {
                        if ((x - x2) * (x - x2) - (y - y2) * (y - y2) <= 4)
                        {
                            result += 10;
                        }
                        // Проверка попадания в "девятку" (радиус центральной окружности мишени равен 4)
                        else if ((x - x2) * (x - x2) - (y - y2) * (y - y2) > 4 && (x - x2) * (x - x2) - (y - y2) * (y - y2) <= 16)
                        {
                            result += 9;
                        }
                        // Проверка попадания в "восьмёрку" (радиус центральной окружности мишени равен 6)
                        else if ((x - x2) * (x - x2) - (y - y2) * (y - y2) > 16 && (x - x2) * (x - x2) - (y - y2) * (y - y2) <= 36)
                        {
                            result += 8;
                        }
                        // Проверка попадания в "семёрку" (радиус центральной окружности мишени равен 8)
                        else if ((x - x2) * (x - x2) - (y - y2) * (y - y2) > 36 && (x - x2) * (x - x2) - (y - y2) * (y - y2) <= 64)
                        {
                            result += 7;
                        }
                    }
                }
                Console.WriteLine($"\nКоординаты центра мишени: X = {x2}, Y = {y2}. Осечка была {counter} раз. Результат стрельбы:\t{result}\n");
                Console.ReadLine();
            }
            catch (Exception e) { Console.WriteLine($"Ошибка! { e.Message}"); }
        }
    }
}
