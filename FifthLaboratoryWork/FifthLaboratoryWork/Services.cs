namespace FifthLaboratoryWork
{
    internal class Services
    {
        private ushort _id;
        private ushort _typeCode;
        private string _title;
        private uint _cost;

        public Services(ushort serviceCode, ushort typeCode, string title, uint cost)
        {
            _id = serviceCode;
            _typeCode = typeCode;
            _title = title;
            _cost = cost;
        }

        public ushort Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public ushort TypeCode
        {
            get { return _typeCode; }
            set { _typeCode = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = (value.Length <= 15) ? value : value.Substring(0, 15); }
        }

        public uint Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
    }
}
