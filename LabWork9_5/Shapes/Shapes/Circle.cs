using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    internal class Circle: Shape
    {
        public double Radius { get; private set; }

        const double pi = 3.1428; 

        public override double Area { get; protected set; }

        public override double Perim { get; protected set; }

        public Circle(double rad)
        {
            Radius = rad;
        }

        public Circle(Point a, Point b)
        {
        Radius = a.CalculatingLength(b);
        }

        public override void СalculationArea()
        {
           Area = pi*Math.Pow(Radius,2); 
        }

        public override void СalculationPerim()
        {
            Perim = 2*pi*Radius;
        }

        public override void Show()
        {
                Console.WriteLine($"\nРадиус круга равен: {Radius}\nДлина окружности равна: {Perim:F2}\nПлощадь круга равна: {Area:F2}");
        }



    }
}
