namespace FifthLaboratoryWork
{
    internal class TypesServices
    {
        private ushort _id;
        private string _title;

        public TypesServices(ushort typeCode, string title)
        {
            _id = typeCode;
            _title = title;
        }

        public ushort Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = (value.Length <= 15) ? value : value.Substring(0, 15); }
        }
    }
}
