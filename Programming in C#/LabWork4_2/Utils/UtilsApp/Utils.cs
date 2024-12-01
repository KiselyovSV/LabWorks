namespace App1
{
    public class Utils
    {
        public static int Greater (int x, int y) 
        {
        int z = x > y ? x : y;
          return z;
        }

        public static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
    }
}
