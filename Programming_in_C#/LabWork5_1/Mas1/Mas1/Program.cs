using System;
using System.IO;

namespace Mas1
{

    class FileDetails
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(args.Length);
            //foreach (string arg in args)
            //{
            //    Console.WriteLine(arg);
            //}
            //Console.ReadLine();

            string fileName = args[0];
            FileStream stream = new FileStream(fileName, FileMode.Open);
            StreamReader reader = new StreamReader(stream);

            int size = (int)stream.Length;
            char[] contents = new char [size]; 
            for (int i = 0; i < size; i++)
            {
                contents[i] = (char)reader.Read();
            }
            //foreach (char c in contents)
            //{
            //    Console.Write(c);
            //}
            reader.Close();
            var tup = Summarize(contents);
            Console.WriteLine($"\nОбщее кол-во символов в файле равно: {contents.Length}.");
            Console.WriteLine($"\nОбщее кол-во гласных в файле равно: {tup.v}.");
            Console.WriteLine($"\nОбщее кол-во согласных в файле равно: {tup.c}.");
            Console.WriteLine($"\nОбщее кол-во строк в файле равно: {tup.l}.");
            Console.ReadLine();
        }
        static (int v,int c,int l) Summarize(char[] mas)
        {
            var tuple = (vowels: 0, cons: 0, lines: 0);
            foreach (char c in mas)
            {
                if (Char.IsLetter(c))
                {
                    if ("AEIOUYaeiouy".IndexOf(c) != -1) tuple.vowels++;
                    else tuple.cons++;
                }
                else if (c == '\n') tuple.lines++;
            }
            return tuple;
        }
    }

}
