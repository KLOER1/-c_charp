using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ThirdLaboratoryWork_Task123
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] array1 = { { 1, 1, 1, 1 }, { 2, 2, 2, 2 }, { 3, 3, 3, 3 }, { 4, 4, 4, 4 } };
            int[,] array2 = { { 1, 1, 1, 1 }, { 2, 2, 2, 2 }, { 3, 3, 3, 3 }, { 4, 4, 4, 4 } };
            Matrix in1 = new Matrix(array1);
            Matrix in2 = new Matrix(array2);
            
            Console.WriteLine(in1.Transpose().ToString());
            Console.WriteLine(in2.ToString());
            Console.WriteLine((in1.Transpose() * in2).ToString());
        }
    }
}
