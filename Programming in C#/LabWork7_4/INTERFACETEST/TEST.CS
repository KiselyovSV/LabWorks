namespace Utils
{
    using System;

    /// <summary>
    ///   This the test harness
    /// </summary>

    public class Test
    {
        public static void Main() 
        {
            int a = 12;
            ulong d = 224;
            string name = "Masha";
            bool b = Utils.IsItFormattable(a);
            Console.WriteLine(b);
            bool f = Utils.IsItFormattable(d);
            Console.WriteLine(f);
            bool m = Utils.IsItFormattable(name);
            Console.WriteLine(m);
        }
    }
}
