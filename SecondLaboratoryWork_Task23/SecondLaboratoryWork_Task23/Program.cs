using System;

namespace SecondLaboratoryWork_Task23
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Time time1 = new Time("12", "34");
            Time time2 = new Time("21", "43");

            GraphicalInterface graphical_interface = new GraphicalInterface();

            graphical_interface.Menu(ref time1, ref time2);
        }
    }
}