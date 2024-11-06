
internal class Triangle
{
    private double sideA;
    private double sideB;
    private double sideC;
    public double SideA
    {
        get => sideA;

        set
        {
            if (value > 0) sideA = value;
            else Console.WriteLine("\nЗначение должно быть больше 0.");
        }
    }
    public double SideB
    {
        get => sideB;

        set
        {
            if (value > 0) sideB = value;
            else Console.WriteLine("\nЗначение должно быть больше 0.");
        }
    }
    public double SideC
    {
        get => sideC;

        set
        {
            if (value > 0) sideC = value;
            else Console.WriteLine("\nЗначение должно быть больше 0.");
        }
    }
    public string OllSides
    {
        get => $"Сторона А равна:{sideA}, сторона В равна:{sideB}, сторона С равна:{sideC}"; 
    }

    public Triangle(double sideA, double sideB, double sideC)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
    }

    internal double Perimeter()
    {
        if (Area() > 0)
        {
            double sum = sideA + sideB + sideC;
            return sum;
        }
        else return 0;
    }

    internal double Area()
    {
        double half = (sideA+sideB+sideC)/2;
        double area = Math.Sqrt(half*(half-sideA)*(half-sideB)*(half-sideC));
        if (area is double.NaN) return 0;
        else return area;
    }
}