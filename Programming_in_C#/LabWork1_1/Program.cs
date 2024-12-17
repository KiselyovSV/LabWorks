using System;

    namespace HelloApp 
 {
      class Program 
      {
          static void Main(string[]args)
          {
             Console.WriteLine("Здравствуйте! Как Вас зовут? Введите своё имя.");
             string? name = Console.ReadLine();
             Console.WriteLine($"Приятно познакомиться,{name}! Меня зовут DotNet.");
             Console.ReadKey();
          }

      }
 }