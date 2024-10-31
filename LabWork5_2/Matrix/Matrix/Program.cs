namespace Matrix
{
    class MatrixMultiply
    {
        static void Main()
        {
            //int[,] a = { { 1, 2 }, { 3, 4 } };
            int[,] a = InputA();
            int[,] b = InputB();
            int[,] result = Multiply(a, b);
            //result = NewMethodMultyply(a, b);
            PrintResult(result);

        }

        private static int[,] InputB()
        {
            int[,] b = new int[2, 2];
            Console.Write("\nВведите значение элемента двухмерного массива B с индексом 0,0:\t");
            b[0, 0] = int.Parse(Console.ReadLine());
            Console.Write("\nВведите значение элемента двухмерного массива B с индексом 0,1:\t");
            b[0, 1] = int.Parse(Console.ReadLine());
            Console.Write("\nВведите значение элемента двухмерного массива B с индексом 1,0:\t");
            b[1, 0] = int.Parse(Console.ReadLine());
            Console.Write("\nВведите значение элемента двухмерного массива B с индексом 1,1:\t");
            b[1, 1] = int.Parse(Console.ReadLine());
            return b;
        }

        private static int[,] InputA()
        {
            int[,] a = new int[2, 2];
            Console.Write("\nВведите значение элемента двухмерного массива A с индексом 0,0:\t");
            a[0, 0] = int.Parse(Console.ReadLine());
            Console.Write("\nВведите значение элемента двухмерного массива A с индексом 0,1:\t");
            a[0, 1] = int.Parse(Console.ReadLine());
            Console.Write("\nВведите значение элемента двухмерного массива A с индексом 1,0:\t");
            a[1, 0] = int.Parse(Console.ReadLine());
            Console.Write("\nВведите значение элемента двухмерного массива A с индексом 1,1:\t");
            a[1, 1] = int.Parse(Console.ReadLine());
            return a;
        }

        //private static int[,] NewMethodMultyply(int[,] a, int[,] b)
        //{
        //    int x = a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0];
        //    int y = a[0, 0] * b[0, 1] + a[0, 1] * b[1, 1];
        //    int z = a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0];
        //    int f = a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1];
        //    int[,] result = { { x, y }, { z, f } };
        //    for (int i = 0; i < result.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < result.GetLength(0); j++)
        //        {
        //            result[i, j] = a[i, 0] * b[0, j] + a[i, 1] * b[1, j];
        //        }
        //    }

        //    return result;
        //}

        private static int[,] Multiply(int[,] a, int[,] b)
        {
            int x = a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0];
            int y = a[0, 0] * b[0, 1] + a[0, 1] * b[1, 1];
            int z = a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0];
            int f = a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1];
            int[,] result = { { x, y }, { z, f } };
            return result;
        }

        private static void PrintResult(int[,] result)
        {
            int rows = result.GetUpperBound(0) + 1;
            int columns = result.GetUpperBound(1) + 1;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{result[i, j]}\t");
                }
                Console.WriteLine();
            }
        }
    }

}
