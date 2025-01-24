using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixthLaboratoryWork
{
    internal static class Meowing
    {
        public static void meowing(List<IMeowable> meowingEntities)
        {
            foreach (var entity in meowingEntities)
            {
                entity.meow();
            }
        }
    }
}
