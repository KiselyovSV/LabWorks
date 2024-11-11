using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    internal class Triangle : Shape, IRotatable
    {
        public double SideAB { get; set; }
        public double SideBC { get; set; }
        public double SideCA { get; set; }

        private double area;
        public override double Area
        {
            get => area;
            protected set
            {
                if (double.IsNaN(value)) Console.WriteLine("Треугольник НЕ СУЩЕСТВУЕТ!");
                else area = value;
            }
        }
        public override double Perim { get; protected set; }

        public Triangle(double sideAB, double sideBC, double sideCA)
        {
            SideAB = sideAB;
            SideBC = sideBC;
            SideCA = sideCA;
        }

        public Triangle(Point a, Point b, Point c)
        {
            SideAB = a.CalculatingLength(b);
            SideBC = b.CalculatingLength(c);
            SideCA = c.CalculatingLength(a);

        }

        public override void СalculationArea()
        {
            double half = (SideAB + SideBC + SideCA) / 2;
            Area = Math.Sqrt(half * (half - SideAB) * (half - SideBC) * (half - SideCA));
        }

        public override void СalculationPerim()
        {
            Perim = SideAB + SideBC + SideCA;
        }

        public override void Show()
        {
            Console.WriteLine($"Сторона AB треугольника равна: {SideAB}\n" +
                $"Сторона BC равна: {SideBC}\nСторона CA равна: {SideCA}\n" +
                $"Периметр равен: {Perim:F2}\nПлощадь треугольника равна: {Area:F2}");
        }

        public void Rotation()
        {
            Console.WriteLine("Треугольник вращается.");
        }
    }



}

