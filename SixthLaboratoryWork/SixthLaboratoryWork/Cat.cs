using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixthLaboratoryWork
{
    internal class Cat : IMeowable
    {
        private string name;

        public Cat(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return $"Кот: {name}";
        }

        public void meow()
        {
            Console.WriteLine($"{name}: мяу!");
        }

        public void meow(uint N)
        {
            if (N != 0)
            {
                string result = $"{name}: ";
                if (N > 1)
                    for (int i = 0; i < N - 1; i++)
                        result += "мяу-";
                result += "мяу!";
                Console.WriteLine(result);
            }    
        }
    }
}
