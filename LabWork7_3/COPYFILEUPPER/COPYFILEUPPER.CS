using System;
using System.IO;

/// <summary>
///    Class to create an upper case copy of a file
/// </summary>
public class CopyFileUpper
{
		public static void Main()
		{
        Console.Write("\nВведите имя входного файла:\t");
        string sFrom = Console.ReadLine();
        Console.Write("\nВведите имя выходного файла:\t");
        string sTo = Console.ReadLine();
        Console.WriteLine("\nКопируем из {0} в {1}", sFrom, sTo);
        try
        {
            StreamReader srFrom = new StreamReader(sFrom);
            StreamWriter swTo = new StreamWriter(sTo);
            while (srFrom.Peek()!=-1)
            {
                string sBuffer = srFrom.ReadLine();
                sBuffer = sBuffer.ToUpper();
                swTo.WriteLine(sBuffer);
            }
            swTo.Close();
            srFrom.Close();
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("Файл не найден.{0}",ex.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Возникло исключение:{e.Message}.");
        }
        

        }       
    }
