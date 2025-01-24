namespace SecondLaboratoryWork_Task1
{
    public class PointOn3D : Point
    {
        private double lenght;                   // Модуль вектора от начала координат до точки
        private (double, double, double) angles; // Углы между векторами-нормалью плоскостей и заданным вектором

        public double Lenght { get => lenght; }

        public (double, double, double) Angles { get => angles; }

        public PointOn3D(string x, string y, string z) : base(x, y, z)
        {
            lenght = LenghtVector();
            angles = (AngleBetweenTwoVectors(base.coordinateY), AngleBetweenTwoVectors(base.coordinateX), AngleBetweenTwoVectors(base.coordinateZ));
        }

        public PointOn3D(PointOn3D OldPointOn3D) : base(OldPointOn3D)
        {
            this.lenght = OldPointOn3D.lenght;
            this.angles = OldPointOn3D.angles;
        }

        public override string ToString()
        {
            return $"The point has coodinates ({base.CoordinateX}, {base.CoordinateY}, {base.CoordinateZ})\n" +
                $"The modulus of the vector is equal to {lenght}\n" +
                $"The angle between the vector and the Ox plane is {angles.Item1}°\n" +
                $"The angle between the vector and the Oy plane is {angles.Item2}°\n" +
                $"The angle between the vector and the Oz plane is {angles.Item3}°";
        }

        private double AngleBetweenTwoVectors(double coordinate)
        {
            if (lenght != 0)
                return Math.Round(Math.Asin(Math.Abs(coordinate) / lenght) * 180 / Math.PI, 2); // Очень оптимизированная формула
            return 0;
        }

        private double LenghtVector() =>
            Math.Sqrt(Math.Pow(base.coordinateX, 2) + Math.Pow(base.coordinateY, 2) + Math.Pow(base.coordinateZ, 2));
    }
}
