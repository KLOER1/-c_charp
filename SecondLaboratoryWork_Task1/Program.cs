namespace SecondLaboratoryWork_Task1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Program program = new Program();
            string FirstNumber, SecondNumber, ThirdNumber;

            Console.WriteLine("Enter the value");
            Console.Write("FirstNumber = ");
            FirstNumber = Console.ReadLine();

            Console.Write("SecondNumber = ");
            SecondNumber = Console.ReadLine();

            Console.Write("ThirdNumber = ");
            ThirdNumber = Console.ReadLine();

            PointOn3D pointOn3D = new PointOn3D(FirstNumber, SecondNumber, ThirdNumber);
            PointOn3D newP = pointOn3D;
            Console.WriteLine(newP.ToString());
        }
    }
}