using System;
using System.IO;

namespace Mas1
{

    class FileDetails
    {
        static void Main(string[] args)
        {
            //    Console.WriteLine(args.Length);
            //    foreach (string arg in args)
            //    {
            //        Console.WriteLine(arg);
            //    }
            //    Console.ReadLine();

            string fileName = args[0];
            FileStream stream = new FileStream(fileName, FileMode.Open);
            StreamReader reader = new StreamReader(stream);

            int size = (int)stream.Length;
            char[] contents = new char [size]; 
            for (int i = 0; i < size; i++)
            {
                contents[i] = (char)reader.Read();
            }
            foreach (char c in contents)
            {
                Console.Write(c);
            }
            Console.ReadLine();
        }

    }

}
