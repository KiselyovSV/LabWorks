using System.Text.Json;

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

        public static bool Factorial(int n, out int answer)
        {
            bool ok = true;
            try
            {
                checked
                {
                    if (n == 1 || n == 0) answer = 1;
                    else if (n < 0) throw new Exception("\nОшибка! Число не может быть меньше 0.");
                    else
                    {
                        int f = 1;
                        for (int k = 2; k <= n; k++)
                        {
                            f *= k;
                        }
                        answer = f;
                    }
                }
            }
            catch (OverflowException)
            {
                Console.WriteLine($"\nОшибка переполнения данных!");
                answer = 0;
                ok = false;
            }

            catch (Exception e)
            {
                Console.WriteLine($"\nОшибка! {e.Message}");
                answer = 0;
                ok = false;
            }
            return ok;
        }
    }
}
