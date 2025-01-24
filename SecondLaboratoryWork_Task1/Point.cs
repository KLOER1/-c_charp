using System;
using System.Collections.Generic;
using System.Linq;
namespace SecondLaboratoryWork_Task1
{
    public class Point
    {
        private protected int coordinateX = 0; // Координата на оси Ox
        private protected int coordinateY = 0; // Координата на оси Oy
        private protected int coordinateZ = 0; // Координата на оси Oz

        public int CoordinateX { get => coordinateX; }

        public int CoordinateY { get => coordinateY; }

        public int CoordinateZ { get => coordinateZ; }

        public Point(string x, string y, string z)
        {
            int.TryParse(x, out coordinateX);
            int.TryParse(y, out coordinateY);
            int.TryParse(z, out coordinateZ);
        }

        public Point(Point OldPoint)
        {
            this.coordinateX = OldPoint.coordinateX;
            this.coordinateY = OldPoint.coordinateY;
            this.coordinateZ = OldPoint.coordinateZ;
        }

        public override string ToString() => $"The point has coodinates ({CoordinateX}, {CoordinateY}, {CoordinateZ})";

        public int Multiplication() => coordinateX * coordinateY * coordinateZ;
    }
}