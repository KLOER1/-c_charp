using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthLaboratoryWork_Task12345
{
    [Serializable]
    internal class SourCream
    {
        private string firm;
        private string street;
        private uint fatContent;
        private uint price;

        public string Firm
        {
            get => firm;
        }

        public string Street
        {
            get => street;
        }

        public uint FatContent
        {
            get => fatContent;
        }

        public uint Price
        {
            get => price;
        }

        public SourCream(string firm, string street, uint fatContent, uint price)
        {
            this.firm = firm;
            this.street = street;
            this.fatContent = fatContent;
            this.price = price;
        }
    }
}
