using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixthLaboratoryWork
{
    internal class MeowCount : IMeowable
    {
        private IMeowable meowable;
        public uint count;

        public MeowCount(IMeowable meowable)
        {
            this.meowable = meowable;
            count = 0;
        }

        public void meow()
        {
            count++;
            meowable.meow();
        }

        public void meow(uint N)
        {
            if (meowable is Cat cat)
            {
                count += N;
                cat.meow(N);
            }
        }
    }
}
