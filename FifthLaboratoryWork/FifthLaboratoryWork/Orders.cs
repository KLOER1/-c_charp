namespace FifthLaboratoryWork
{
    internal class Orders
    {
        private uint _id;
        private uint _clientCode;
        private DateOnly _dateOrder;
        private ushort _serviceCode;
        private ushort _quantity;

        public Orders(uint orderCode, uint clientCode, DateOnly dateOrder, ushort serviceCode, ushort quantity)
        {
            _id = orderCode;
            _clientCode = clientCode;
            _dateOrder = dateOrder;
            _serviceCode = serviceCode;
            _quantity = quantity;
        }

        public uint Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public uint ClientCode
        {
            get { return _clientCode; }
            set { _clientCode = value; }
        }

        public DateOnly DateOrder
        {
            get { return _dateOrder; }
            set { _dateOrder = value; }
        }

        public ushort ServiceCode
        {
            get { return _serviceCode; }
            set { _serviceCode = value; }
        }

        public ushort Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
    }
}
