using System;

namespace Shapes
{
    abstract class Shape
    {
        abstract public double Area { get; protected set; }
        abstract public double Perim { get; protected set; }

        abstract public void СalculationArea();

        abstract public void СalculationPerim();

        abstract public void Show();


    }
}
