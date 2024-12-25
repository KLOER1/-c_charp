namespace FifthLaboratoryWork
{
    internal class Client
    {
        private uint _id; // первичный ключ
        private string _firstName;
        private string _lastName;
        private string _patronymic;
        private string _placeOfResidence;

        // Правила
        // Идентификатор должен быть положительным значением
        // Имя имеет максимальную длину 15 букв                                               => максимальное количество в таблице 11
        // Фамилия имеет максимальную длину 15 букв                                           => максимальное количество в таблице 11
        // Отчество имеет максимальную длину 18                                               => максимальное количество в таблице 14
        // Город имеет максимальную длину 24 букв, а именно "г. Кременчуг-Константиновск"     => максимальное количество в таблице 24

        public Client(uint id, string lastname, string firstname, string patronyumic, string placeofresidence)
        {
            Id = id;
            LastName = lastname;
            FirstName = firstname;
            Patronymic = patronyumic;
            PlaceOfResidence = placeofresidence;
        }

        public uint Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = (value.Length <= 15) ? value : value.Substring(0, 15); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = (value.Length <= 15) ? value : value.Substring(0, 15); }
        }

        public string Patronymic
        {
            get { return _patronymic; }
            set { _patronymic = (value.Length <= 18) ? value : value.Substring(0, 18); }
        }

        public string PlaceOfResidence
        {
            get { return _placeOfResidence; }
            set { _placeOfResidence = (value.Length <= 28) ? value : value.Substring(0, 27); }
        }
    }
}
