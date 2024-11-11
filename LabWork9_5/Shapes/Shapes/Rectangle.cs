using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    internal class Rectangle: Shape, IRotatable
    {
        public double SideAB { get; private set; }
        public double SideBC { get; private set; }
        public double SideCD { get; private set; }
        public double SideDA { get; private set; }

        public override double Area { get;  protected set; }
       
        public override double Perim { get; protected set; }

        public Rectangle (double sideA, double sideB)
        {
            SideAB = sideA;
            SideBC = sideB;
            SideCD = sideA;
            SideDA = sideB;
        }

        public Rectangle(Point a, Point b, Point c, Point d)
        {
                SideAB = a.CalculatingLength(b);
                SideBC = b.CalculatingLength(c);
                SideCD = c.CalculatingLength(d);
                SideDA = d.CalculatingLength(a);
        }

        public override void СalculationArea()
        {
            double kvAC = Math.Pow(SideAB, 2) + Math.Pow(SideBC, 2);
            double kvBD = Math.Pow(SideBC, 2) + Math.Pow(SideCD, 2);
            if (kvAC == kvBD) Area = SideAB * SideBC;
            else
            {
                Area = 0;
            }
        
        }

        public override void СalculationPerim()
        {
            Perim = (SideAB+SideBC)*2;
        }

        public override void Show()
        {
            if (Area == 0)
            {
                Console.WriteLine("\nПрямоугольник НЕ СУЩЕСТВУЕТ! Это другая фигура.");
            }
            else
            {
                Console.WriteLine($"\nСторона AB прямоугольника равна: {SideAB}\n" +
                    $"Сторона BC равна: {SideBC}\nСторона CD равна: {SideAB}\n" +
                    $"Сторона DA равна: {SideBC}\nПериметр прямоугольника равен: {Perim:F2}\nПлощадь прямоугольника равна: {Area:F2}");
            }
        }

        public void Rotation()
        {
            Console.WriteLine("Прямоугольник вращается.");
        }

    }
}
