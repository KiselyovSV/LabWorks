using System;

namespace Op
{
    public class Operation
    {
        public static double AreaOfTheTriangle(double a, out bool ex)
        {
            double p = a*3 / 2;
            double s = Math.Sqrt(p * (p - a) * (p - a) * (p - a));
            ex = ExistenceOfATriangle(s);
            return s;
        }
        public static double AreaOfTheTriangle(double a, double b, double c, out bool ex)
        {
            double p = (a + b + c) / 2;
            double s = Math.Sqrt(p*(p-a)*(p-b)*(p-c));
            ex = ExistenceOfATriangle(s);
            return s;
            
        }

        static bool ExistenceOfATriangle(double s)
        {
            if (s > 0) return true;
            return false;
        }


    }
}
