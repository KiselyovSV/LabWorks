namespace Shapes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Point a = new Point(3, -4);
            Point b = new Point(3, 5);
            Point c = new Point(-3, 4);
            Point d = new Point(-3, -4);  
            Triangle tr1 = new Triangle(a, b, c);
            tr1.СalculationArea();
            tr1.СalculationPerim();
            tr1.Show();
            tr1.Rotation();
            Rectangle rec1 = new Rectangle(10, 20);
            rec1.СalculationArea();
            rec1.СalculationPerim();
            rec1.Show();
            rec1.Rotation();
            Rectangle rec2 = new Rectangle(a,b,c,d);
            rec2.СalculationArea();
            rec2.СalculationPerim();
            rec2.Show();
            Circle cir1 = new Circle(a, b);
            cir1.СalculationPerim();
            cir1.СalculationArea();
            cir1.Show();
            Circle cir2 = new Circle(31.18);
            cir2.СalculationPerim();
            cir2.СalculationArea();
            cir2.Show();

        }
    }
}
