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
            int a = 236;
            string str = "Bob";
            Coordinate c = new Coordinate(21.0, 68.0);

            Utils.Display(a);
            Utils.Display(str);
            Utils.Display(c);
        }
    }
}
