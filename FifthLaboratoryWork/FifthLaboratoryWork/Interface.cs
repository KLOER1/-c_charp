using Npgsql;

namespace FifthLaboratoryWork
{
    internal class Interface
    {
        private List<Client> clients = new List<Client>();
        private List<Orders> orders = new List<Orders>();
        private List<Services> services = new List<Services>();
        private List<TypesServices> typeServices = new List<TypesServices>();
        private NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Port=5432; Database=LaboratoryWork5; User ID = postgres; Password=GreenMile10");
        private string? message = "";
        private StreamWriter? log;

        public void Menu()
        {
            while (log == null)
            {
                Console.Write("Выберите как Вы хотели бы вести протоколирование. Чтобы выбрать, напишите в консоль подчёркнутое слово \n\x1b[4mnew\x1b[0m - в новом файле\n\x1b[4mold\x1b[0m - в старом файле \n >> ");
                switch (Console.ReadLine())
                {
                    case "new":
                        log = new StreamWriter(new FileStream("log.txt", FileMode.Create));
                        break;
                    case "old":
                        log = new StreamWriter(new FileStream("log.txt", FileMode.OpenOrCreate));
                        break;
                    default:
                        break;
                }
            }

            if (connection.State != System.Data.ConnectionState.Open)
                DataBaseConnection();

            Logging($"Выбран FileMode файла, ввёл {message}");
            while (message != "end")
            {
                Console.Clear();
                Console.Write("           Меню          │         Вариант №7\n" +
                              "──────────┬──────────────┴──────────────────────────────\n" +
                             $" database │     {((connection.State != System.Data.ConnectionState.Open) ? "Не прочитана" : "Прочитана")}\n" +
                              "──────┬───┴─────────────────────────────────────────────\n" +
                              "  1   │ Просмотр базы данных\n" +
                              "──────┼─────────────────────────────────────────────────\n" +
                              "  2   │ Удаление элемента ( по ключу )\n" +
                              "──────┼─────────────────────────────────────────────────\n" +
                              "  3   │ Модификация элементов ( по ключу )\n" +
                              "──────┼─────────────────────────────────────────────────\n" +
                              "  4   │ Добавление элементов\n" +
                              "──────┼─────────────────────────────────────────────────\n" +
                              "  5   │ Запросы в базу данных\n" +
                              "──────┴───────────┬─────────────────────────────────────\n" +
                              " Протоколирование │   Новый файл / Существующий файл\n" +
                              "──────────────────┴─────────────────────────────────────\n" +
                              " >> ");
                message = Console.ReadLine();
                switch (message)
                {
                    case "1":
                        Logging("Переход к выбору таблицы для просмотра");
                        ChoiceTable("Show");
                        break;
                    case "2":
                        ChoiceTable("Delete");
                        break;
                    case "3":
                        ChoiceTable("Update");
                        break;
                    case "4":
                        ChoiceTable("Insert");
                        break;
                    case "5":
                        ChoiceRequest();
                        break;
                    case "end":
                        if (connection.State == System.Data.ConnectionState.Open) connection.Close();
                        if (log != null) log.Close();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Logging(string message)
        {
            log.WriteLine($"{DateTime.Now} {message}");
        }

        private void SendRequestToBD(NpgsqlCommand request)
        {
            request.ExecuteNonQuery();
        }

        private void DataBaseConnection()
        {
            connection.Open();
            Logging("База данных открыта");
            using (NpgsqlDataReader reader = new NpgsqlCommand("SELECT * FROM clients", connection).ExecuteReader())
                while (reader.Read())
                    clients.Add(new Client((uint)reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));

            using (NpgsqlDataReader reader = new NpgsqlCommand("SELECT * FROM orders", connection).ExecuteReader())
                while (reader.Read())
                    orders.Add(new Orders((uint)reader.GetInt32(0), (uint)reader.GetInt32(1), DateOnly.FromDateTime(reader.GetDateTime(2)), (ushort)reader.GetInt16(3), (ushort)reader.GetInt16(4)));

            using (NpgsqlDataReader reader = new NpgsqlCommand("SELECT * FROM services", connection).ExecuteReader())
                while (reader.Read())
                    services.Add(new Services((ushort)reader.GetInt16(0), (ushort)reader.GetInt16(1), reader.GetString(2), (uint)reader.GetInt32(3)));

            using (NpgsqlDataReader reader = new NpgsqlCommand("SELECT * FROM typeservices", connection).ExecuteReader())
                while (reader.Read())
                    typeServices.Add(new TypesServices((ushort)reader.GetInt16(0), reader.GetString(1)));
        }

        private void ChoiceTable(string? act)
        {
            while (message != "menu")
            {
                Console.Clear();
                Console.Write("          Выбор таблицы\n" +
                              "──────┬─────────────────────────\n" +
                              "  1   │ Клиенты\n" +
                              "──────┼─────────────────────────\n" +
                              "  2   │ Заказы\n" +
                              "──────┼─────────────────────────\n" +
                              "  3   │ Услуги\n" +
                              "──────┼─────────────────────────\n" +
                              "  4   │ Типы услуг\n" +
                              "──────┼─────────────────────────\n" +
                              " menu │ Вернуться в меню\n" +
                              "──────┴─────────────────────────\n >> ");
                message = Console.ReadLine();
                Console.Clear();
                switch (message)
                {
                    case "1":
                        switch (act)
                        {
                            case "Insert":
                                Client insertClient = new Client((uint)InsertOrModification((uint)0, "ИДЕНТИФИКАТОР КЛИЕНТА", 'c'),
                                                                 (string)InsertOrModification("", "ФАМИЛИЯ КЛИЕНТА"),
                                                                 (string)InsertOrModification("", "ИМЯ КЛИЕНТА"),
                                                                 (string)InsertOrModification("", "ОТЧЕСТВО КЛИЕНТА"),
                                                                 (string)InsertOrModification("", "МЕСТО ПРОЖИВАНИЯ"));
                                clients.Add(insertClient);
                                clients = clients.OrderBy(c => c.Id).ToList();
                                SendRequestToBD(new NpgsqlCommand("INSERT INTO clients VALUES(@p1, @p2, @p3, @p4, @p5)", connection)
                                {
                                    Parameters =
                                    {
                                        new("p1", (int)insertClient.Id),
                                        new("p2", insertClient.LastName),
                                        new("p3", insertClient.FirstName),
                                        new("p4", insertClient.Patronymic),
                                        new("p5", insertClient.PlaceOfResidence)
                                    }
                                });

                                break;

                            case "Delete":
                                uint deleteKey = (uint)InsertOrModification((uint)0, "ИДЕНТИФИКАТОР КЛИЕНТА");
                                clients.Remove((from c in clients where c.Id == deleteKey select c).First());
                                SendRequestToBD(new NpgsqlCommand("DELETE FROM clients WHERE clientid=@p1", connection)
                                {
                                    Parameters =
                                    {
                                        new("p1", (int)deleteKey)
                                    }
                                });

                                break;

                            case "Update":
                                uint key = (uint)InsertOrModification((uint)0, "ИДЕНТИФИКАТОР КЛИЕНТА");
                                Client? client = (from c in clients where c.Id == key select c).First();
                                if (client != null)
                                {
                                    Console.Clear();
                                    Console.Write("─────────────┬─────────────────┬─────────────────┬────────────────────┬─────────────────────────────\n" +
                                                  " Код клиента │ Фамилия         │ Имя             │ Отчество           │ Место жительства\n" +
                                                  "─────────────┼─────────────────┼─────────────────┼────────────────────┼─────────────────────────────\n" +
                                                  $" {client.Id,-10}  │ {client.LastName,-15} │ {client.FirstName,-15} │ {client.Patronymic,-18} │ {client.PlaceOfResidence,-27}\n" +
                                                  "─────────────┴─────────────────┴─────────────────┴────────────────────┴─────────────────────────────\n" +
                                                  "Чтобы изменить значение столбца, напишите порядковый номер столбца (начиная с 1). Иное значение выходит из этой команды!\n >> ");
                                    switch (Console.ReadLine())
                                    {
                                        case "1":
                                            client.Id = (uint)InsertOrModification((uint)0, "ИДЕНТИФИКАТОР КЛИЕНТА", 'c');
                                            break;
                                        case "2":
                                            client.LastName = (string)InsertOrModification("", "ФАМИЛИЯ КЛИЕНТА");
                                            break;
                                        case "3":
                                            client.FirstName = (string)InsertOrModification("", "ИМЯ КЛИЕНТА");
                                            break;
                                        case "4":
                                            client.Patronymic = (string)InsertOrModification("", "ОТЧЕСТВО КЛИЕНТА");
                                            break;
                                        case "5":
                                            client.PlaceOfResidence = (string)InsertOrModification("", "МЕСТО ПРОЖИВАНИЯ");
                                            break;
                                        default:
                                            break;
                                    }
                                    SendRequestToBD(new NpgsqlCommand("UPDATE clients SET lastname=@p2, firstname=@p3, patronymic=@p4, placeofresidence=@p5 WHERE clientid=@p1", connection)
                                    {
                                        Parameters =
                                        {
                                            new("p1", (int)client.Id),
                                            new("p2", client.LastName),
                                            new("p3", client.FirstName),
                                            new("p4", client.Patronymic),
                                            new("p5", client.PlaceOfResidence)
                                        }
                                    });
                                }
                                clients = clients.OrderBy(c => c.Id).ToList();
                                break;
                        }
                        TableClients(clients);
                        break;

                    case "2":
                        switch (act)
                        {
                            case "Insert":
                                Orders insertOrder = new Orders((uint)InsertOrModification((uint)0, "ИДЕНТИФИКАТОР ЗАКАЗА", 'o'),
                                                       (uint)InsertOrModification((uint)0, "ИДЕНТИФИКАТОР КЛИЕНТА"),
                                                       (DateOnly)InsertOrModification(new DateOnly(1, 1, 1), "ДАТА ЗАКАЗА"),
                                                       (ushort)InsertOrModification((ushort)0, "ИДЕНТИФИКАТОР УСЛУГИ"),
                                                       (ushort)InsertOrModification((ushort)0, "КОЛИЧЕСТВО"));
                                orders.Add(insertOrder);
                                orders = orders.OrderBy(o => o.Id).ToList();
                                SendRequestToBD(new NpgsqlCommand("INSERT INTO orders VALUES(@p1, @p2, @p3, @p4, @p5)", connection)
                                {
                                    Parameters =
                                    {
                                        new("p1", (int)insertOrder.Id),
                                        new("p2", (int)insertOrder.ClientCode),
                                        new("p3", insertOrder.DateOrder.ToDateTime(TimeOnly.MinValue)),
                                        new("p4", (Int16)insertOrder.ServiceCode),
                                        new("p5", (Int16)insertOrder.Quantity)
                                    }
                                });
                                break;

                            case "Delete":
                                uint deleteKey = (uint)InsertOrModification((uint)0, "ИДЕНТИФИКАТОР ЗАКАЗА");
                                orders.Remove((from o in orders where o.Id == deleteKey select o).First());
                                SendRequestToBD(new NpgsqlCommand("DELETE FROM orders WHERE id=@p1", connection)
                                {
                                    Parameters =
                                    {
                                        new("p1", (int)deleteKey)
                                    }
                                });
                                break;

                            case "Update":
                                uint key = (uint)InsertOrModification((uint)0, "ИДЕНТИФИКАТОР ЗАКАЗА");
                                Orders? order = (from o in orders where o.Id == key select o).First();
                                if (order != null)
                                {
                                    Console.Clear();
                                    Console.Write("────────────┬─────────────┬─────────────┬────────────┬────────────\n" +
                                                  " Код заказа │ Код клиента │ Дата заказа │ Код услуги │ Количество\n" +
                                                  "────────────┼─────────────┼─────────────┼────────────┼────────────\n" +
                                                 $" {order.Id,-10} │ {order.ClientCode,-10}  │ {order.DateOrder.ToString()}  │ {order.ServiceCode,-5}      │ {order.Quantity,-5}\n" +
                                                  "────────────┴─────────────┴─────────────┴────────────┴────────────\n" +
                                                  "Чтобы изменить значение столбца, напишите порядковый номер столбца (начиная с 1). Иное значение выходит из этой команды!\n >> ");
                                    switch (Console.ReadLine())
                                    {
                                        case "1":
                                            order.Id = (uint)InsertOrModification((uint)0, "ИДЕНТИФИКАТОР КЛИЕНТА", 'o');
                                            break;
                                        case "2":
                                            order.ClientCode = (uint)InsertOrModification((uint)0, "ИДЕНТИФИКАТОР КЛИЕНТА");
                                            break;
                                        case "3":
                                            order.DateOrder = (DateOnly)InsertOrModification(new DateOnly(1, 1, 1), "ДАТА ЗАКАЗА");
                                            break;
                                        case "4":
                                            order.ServiceCode = (ushort)InsertOrModification((ushort)0, "ИДЕНТИФИКАТОР УСЛУГИ");
                                            break;
                                        case "5":
                                            order.Quantity = (ushort)InsertOrModification((ushort)0, "КОЛИЧЕСТВО");
                                            break;
                                        default:
                                            break;
                                    }
                                    SendRequestToBD(new NpgsqlCommand("UPDATE clients SET clientid=@p2, dateorder=@p3, serviceid=@p4, quantity=@p5 WHERE id=@p1", connection)
                                    {
                                        Parameters =
                                        {
                                            new("p1", (int)order.Id),
                                            new("p2", (int)order.ClientCode),
                                            new("p3", order.DateOrder.ToDateTime(TimeOnly.MinValue)),
                                            new("p4", (Int16)order.ServiceCode),
                                            new("p5", (Int16)order.Quantity)
                                        }
                                    });
                                }
                                orders = orders.OrderBy(o => o.Id).ToList();
                                break;

                            default: 
                                break;
                        }
                        TableOrder(orders);
                        break;

                    case "3":
                        switch (act)
                        {
                            case "Insert":
                                Services insertService = new Services((ushort)InsertOrModification((ushort)0, "ИДЕНТИФИКАТОР УСЛУГИ", 's'),
                                                          (ushort)InsertOrModification((ushort)0, "ИДЕНТИФИКАТОР ТИПА УСЛУГ"),
                                                          (string)InsertOrModification("", "НАЗВАНИЕ УСЛУГИ"),
                                                          (uint)InsertOrModification((uint)0, "ЦЕНА УСЛУГИ"));
                                services.Add(insertService);
                                services = services.OrderBy(s => s.Id).ToList();
                                SendRequestToBD(new NpgsqlCommand("INSERT INTO services VALUES(@p1, @p2, @p3, @p4)", connection)
                                {
                                    Parameters =
                                    {
                                        new("p1", (Int16)insertService.Id),
                                        new("p2", (Int16)insertService.TypeCode),
                                        new("p3", insertService.Title),
                                        new("p4", (int)insertService.Cost),
                                    }
                                });
                                break;

                            case "Delete":
                                uint deleteKey = (uint)InsertOrModification((uint)0, "ИДЕНТИФИКАТОР УСЛУГИ");
                                services.Remove((from s in services where s.Id == deleteKey select s).First());
                                SendRequestToBD(new NpgsqlCommand("DELETE FROM services WHERE serviceid=@p1", connection)
                                {
                                    Parameters =
                                    {
                                        new("p1", (int)deleteKey)
                                    }
                                });
                                break;

                            case "Update":
                                uint key = (uint)InsertOrModification((uint)0, "ИДЕНТИФИКАТОР КЛИЕНТА");
                                Services? service = (from s in services where s.Id == key select s).First();
                                if (service != null)
                                {
                                    Console.Clear();
                                    Console.Write("────────────┬──────────┬─────────────────────────────────────┬───────────────\n" +
                                                  " Код услуги │ Код типа │ Название                            │ Стоимость\n" +
                                                  "────────────┼──────────┼─────────────────────────────────────┼───────────────\n" +
                                                 $" {service.Id,-5}      │ {service.TypeCode,-5}    │ {service.Title,-35} │ {service.Cost,-10} р.\n" +
                                                  "────────────┴──────────┴─────────────────────────────────────┴───────────────\n" +
                                                  "Чтобы изменить значение столбца, напишите порядковый номер столбца (начиная с 1). Иное значение выходит из этой команды!\n >> ");
                                    switch (Console.ReadLine())
                                    {
                                        case "1":
                                            service.Id = (ushort)InsertOrModification((ushort)0, "ИДЕНТИФИКАТОР УСЛУГИ", 's');
                                            break;
                                        case "2":
                                            service.TypeCode = (ushort)InsertOrModification((ushort)0, "ИДЕНТИФИКАТОР ТИПА УСЛУГ");
                                            break;
                                        case "3":
                                            service.Title = (string)InsertOrModification("", "НАЗВАНИЕ УСЛУГИ");
                                            break;
                                        case "4":
                                            service.Cost = (uint)InsertOrModification((uint)0, "ЦЕНА УСЛУГИ");
                                            break;
                                        default:
                                            break;
                                    }
                                    SendRequestToBD(new NpgsqlCommand("UPDATE services SET typeid=@p2, title=@p3, cost=@p4 WHERE serviceid=@p1", connection)
                                    {
                                        Parameters =
                                        {
                                            new("p1", (Int16)service.Id),
                                            new("p2", (Int16)service.TypeCode),
                                            new("p3", service.Title),
                                            new("p4", (int)service.Cost),
                                        }
                                    });
                                }
                                services = services.OrderBy(s => s.Id).ToList();
                                break;

                            default:
                                break;
                        }
                        TableServices(services);
                        break;

                    case "4":
                        switch (act)
                        {
                            case "Insert":
                                TypesServices insertTypeServices = new TypesServices((ushort)InsertOrModification((ushort)0, "ИДЕНТИФИКАТОР ТИПА УСЛУГ", 's'),
                                                                    (string)InsertOrModification("", "НАЗВАНИЕ ТИПА УСЛУГ"));
                                typeServices.Add(insertTypeServices);
                                typeServices = typeServices.OrderBy(s => s.Id).ToList();
                                SendRequestToBD(new NpgsqlCommand("INSERT INTO typeservices VALUES(@p1, @p2)", connection)
                                {
                                    Parameters =
                                    {
                                        new("p1", (Int16)insertTypeServices.Id),
                                        new("p2", insertTypeServices.Title),
                                    }
                                });
                                break;

                            case "Delete":
                                uint deleteKey = (uint)InsertOrModification((uint)0, "ИДЕНТИФИКАТОР ТИПА УСЛУГ");
                                typeServices.Remove((from t in typeServices where t.Id == deleteKey select t).First());
                                SendRequestToBD(new NpgsqlCommand("DELETE FROM typeservices WHERE serviceid=@p1", connection)
                                {
                                    Parameters =
                                    {
                                        new("p1", (int)deleteKey)
                                    }
                                });
                                break;

                            case "Update":
                                uint key = (uint)InsertOrModification((uint)0, "ИДЕНТИФИКАТОР КЛИЕНТА");
                                TypesServices? typeService = (from t in typeServices where t.Id == key select t).First();
                                if (typeService != null)
                                {
                                    Console.Clear();
                                    Console.Write("──────────┬───────────────────────────────────\n" +
                                                  " Код типа │ Название\n" +
                                                  "──────────┼───────────────────────────────────\n" +
                                                 $" {typeService.Id,-5}    │ {typeService.Title,-35}\n" +
                                                  "──────────┴───────────────────────────────────\n" +
                                                  "Чтобы изменить значение столбца, напишите порядковый номер столбца (начиная с 1). Иное значение выходит из этой команды!\n >> ");
                                    switch (Console.ReadLine())
                                    {
                                        case "1":
                                            typeService.Id = (ushort)InsertOrModification((ushort)0, "ИДЕНТИФИКАТОР ТИПА УСЛУГ", 's');
                                            break;
                                        case "2":
                                            typeService.Title = (string)InsertOrModification("", "НАЗВАНИЕ ТИПА УСЛУГ");
                                            break;
                                        default:
                                            break;
                                    }
                                    SendRequestToBD(new NpgsqlCommand("UPDATE services SET title=@p2 WHERE serviceid=@p1", connection)
                                    {
                                        Parameters =
                                        {
                                            new("p1", (Int16)typeService.Id),
                                            new("p2", typeService.Title),
                                        }
                                    });
                                }
                                typeServices = typeServices.OrderBy(t => t.Id).ToList();
                                break;

                            default:
                                break;
                        }
                        TableTypeServices(typeServices);
                        break;
                    default:
                        break;
                }
            }
        }

        private object InsertOrModification(object domain, string atribute, char className = ' ')
        {
            switch (domain)
            {
                case uint:
                    uint uint_value = 0;
                    while (uint_value == 0 || CheckKey(uint_value, className))
                    {
                        Console.Write($"Введите значение типа uint для поля {atribute}: ");
                        uint.TryParse(Console.ReadLine(), out uint_value);
                    }
                    return uint_value;

                case ushort:
                    ushort ushort_value = 0;
                    while (ushort_value == 0)
                    {
                        Console.Write($"Введите значение типа ushort для поля {atribute}: ");
                        ushort.TryParse(Console.ReadLine(), out ushort_value);
                    }
                    return ushort_value;

                case string:
                    string string_value = "";
                    bool a = true;
                    while (a)
                    {
                        a = false;
                        Console.Write($"Введите значение типа string для поля {atribute}: ");
                        string_value = Console.ReadLine();
                        foreach (char letter in string_value)
                            if (!('A' <= letter && letter <= 'я'))
                                if (!(atribute == "МЕСТО ПРОЖИВАНИЯ" && (letter == ' ' || letter == '.')))
                                    a = true;
                    }
                    return string_value;

                case DateOnly:
                    DateOnly date_value = new DateOnly();
                    while (date_value.Year < 2018)
                    {
                        Console.Write($"Введите значение типа DateOnly в формате ГГГГ-ММ-ДД для поля {atribute}: ");
                        DateOnly.TryParse(Console.ReadLine(), out date_value);
                    }
                    return date_value;

                default:
                    return false;
            }
            // Тут нужно добавить запись в логи и изменение в бд
        }

        private bool CheckKey(uint id, char className)
        {
            switch (className)
            {
                case 'c':
                    return (from c in clients select c.Id).Contains(id);
                case 'o':
                    return (from o in orders select o.Id).Contains(id);
                case 's':
                    return (from s in services select s.Id).Contains((ushort)id);
                case 't':
                    return (from t in typeServices select t.Id).Contains((ushort)id);
                default:
                    return false;
            }
        }

        private void ChoiceRequest()
        {
            while (message != "menu")
            {
                Console.Clear();
                Console.Write("          Выбор запроса\n" +
                              "──────┬─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────\n" +
                              "  1   │ Вывести список клиентов, чьё имя \"Виктория\" (Запрос к одной таблице -> перечень)\n" +
                              "──────┼─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────\n" +
                              "  2   │ Вывести список услуг типа \"Дизайн\" (Запрос к двум таблицам -> перечень)\n" +
                              "──────┼─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────\n" +
                              "  3   │ Вывести количество выполненных услуг \"Дизайн сайта\" за всё лето из г. Владивостока (Запрос к трём таблицам -> значение)\n" +
                              "──────┼─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────\n" +
                              "  4   │ Определить общую стоимость выполненных услуг типа \"Полиграфия\" за июнь 2018 года (Запрос к трём таблица -> значение)\n" +
                              "──────┼─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────\n" +
                              " menu │ Вернуться в меню\n" +
                              "──────┴─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────\n >> ");
                message = Console.ReadLine();
                switch (message)
                {
                    case "1":
                        Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────────────────────\n" +
                                          "                                           Результат");
                        TableClients(request1());
                        break;
                    case "2":
                        Console.WriteLine("───────────────────────────────────\n" +
                                          "            Результат");
                        foreach (string service in request2())
                            Console.WriteLine("───────────────────────────────────\n" +
                                             $" {service,-30}");
                        Console.WriteLine("───────────────────────────────────");
                        Console.WriteLine("Чтобы продолжить нажмите любую кнопку");
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.WriteLine("───────────────────────────────────\n" +
                                          "            Результат\n" +
                                          "───────────────────────────────────\n" +
                                         $" {request3()}\n" +
                                          "───────────────────────────────────");
                        Console.WriteLine("Чтобы продолжить нажмите любую кнопку");
                        Console.ReadLine();
                        break;
                    case "4":
                        Console.WriteLine("───────────────────────────────────\n" +
                                          "            Результат\n" +
                                          "───────────────────────────────────\n" +
                                         $" {request4()}\n" +
                                          "───────────────────────────────────");
                        Console.WriteLine("Чтобы продолжить нажмите любую кнопку");
                        Console.ReadLine();
                        break;
                    default:
                        break;
                }
            }
        }

        private List<Client> request1()
        {
            return (from c in clients
                    where c.FirstName == "Виктория"
                    select c).ToList();
        }

        private List<string> request2()
        {
            return (from s in services
                    join t in typeServices on s.TypeCode equals t.Id
                    where t.Title == "Дизайн"
                    select s.Title).ToList();
        }

        private uint request3()
        {
            return (from o in orders
                    join s in services on o.ServiceCode equals s.Id
                    join c in clients on o.ClientCode equals c.Id
                    where new DateOnly(2018, 6, 1) <= o.DateOrder && o.DateOrder < new DateOnly(2018, 9, 1) && s.Title == "Дизайн сайта" && c.PlaceOfResidence == "г. Владивосток"
                    select (uint)o.Quantity).ToList().Aggregate((x, y) => x + y);
        }

        private uint request4()
        {
            return (from o in orders
                    join s in services on o.ServiceCode equals s.Id
                    join t in typeServices on s.TypeCode equals t.Id
                    where new DateOnly(2018, 6, 1) <= o.DateOrder && o.DateOrder < new DateOnly(2018, 7, 1) && t.Title == "Полиграфия"
                    select (o.Quantity * s.Cost)).ToList().Aggregate((x, y) => x + y);
        }

        private void TableClients(List<Client> currentList)
        {
            Console.Clear();
            Console.WriteLine("─────────────┬─────────────────┬─────────────────┬────────────────────┬─────────────────────────────\n" +
                              " Код клиента │ Фамилия         │ Имя             │ Отчество           │ Место жительства");
            foreach (Client client in currentList)
                Console.WriteLine("─────────────┼─────────────────┼─────────────────┼────────────────────┼─────────────────────────────\n" +
                                 $" {client.Id,-10}  │ {client.LastName,-15} │ {client.FirstName,-15} │ {client.Patronymic,-18} │ {client.PlaceOfResidence,-27}");
            Console.WriteLine("─────────────┴─────────────────┴─────────────────┴────────────────────┴─────────────────────────────");
            Console.WriteLine("Чтобы продолжить нажмите любую кнопку");
            Console.ReadLine();
        }

        private void TableOrder(List<Orders> currentList)
        {
            Console.Clear();
            Console.WriteLine("────────────┬─────────────┬─────────────┬────────────┬────────────\n" +
                              " Код заказа │ Код клиента │ Дата заказа │ Код услуги │ Количество");
            foreach (Orders order in currentList)
                Console.WriteLine("────────────┼─────────────┼─────────────┼────────────┼────────────\n" +
                                 $" {order.Id,-10} │ {order.ClientCode,-10}  │ {order.DateOrder.ToString()}  │ {order.ServiceCode,-5}      │ {order.Quantity,-5}");
            Console.WriteLine("────────────┴─────────────┴─────────────┴────────────┴────────────");
            Console.WriteLine("Чтобы продолжить нажмите любую кнопку");
            Console.ReadLine();
        }

        private void TableServices(List<Services> currentList)
        {
            Console.Clear();
            Console.WriteLine("────────────┬──────────┬─────────────────────────────────────┬───────────────\n" +
                              " Код услуги │ Код типа │ Название                            │ Стоимость");
            foreach (Services service in currentList)
                Console.WriteLine("────────────┼──────────┼─────────────────────────────────────┼───────────────\n" +
                                 $" {service.Id,-5}      │ {service.TypeCode,-5}    │ {service.Title,-35} │ {service.Cost,-10} р.");
            Console.WriteLine("────────────┴──────────┴─────────────────────────────────────┴───────────────");
            Console.WriteLine("Чтобы продолжить нажмите любую кнопку");
            Console.ReadLine();
        }

        private void TableTypeServices(List<TypesServices> currentList)
        {
            Console.Clear();
            Console.WriteLine("──────────┬───────────────────────────────────\n" +
                              " Код типа │ Название");
            foreach (TypesServices typeService in currentList)
                Console.WriteLine("──────────┼───────────────────────────────────\n" +
                                 $" {typeService.Id,-5}    │ {typeService.Title,-35}");
            Console.WriteLine("──────────┴───────────────────────────────────");
            Console.WriteLine("Чтобы продолжить нажмите любую кнопку");
            Console.ReadLine();
        }
    }
}

// │─┼┴┬ \x1B[4m - левая граница подчеркивания, \x1B[0m - правая граница подчеркивания

//                         меню

//            Меню          │         Вариант №7
// ──────────┬──────────────┴──────────────────────────────
//  database │ Чтение БД:    Не прочитано / Прочитано
// ──────┬───┴─────────────────────────────────────────────
//   1   │ Просмотр базы данных
// ──────┼─────────────────────────────────────────────────
//   2   │ Удаление элемента ( по ключу )
// ──────┼─────────────────────────────────────────────────
//   3   │ Модификация элементов ( по ключу )
// ──────┼─────────────────────────────────────────────────
//   4   │ Добавление элементов
// ──────┼─────────────────────────────────────────────────
//   5   │ Запросы в базу данных
// ──────┴───────────┬─────────────────────────────────────
//  Протоколирование │   Новый файл / Существующий файл 
// ──────────────────┴─────────────────────────────────────


//         Выбор таблицы
// ──────┬─────────────────────────
//   1   │ Клиенты
// ──────┼─────────────────────────
//   2   │ Заказы
// ──────┼─────────────────────────
//   3   │ Услуги
// ──────┼─────────────────────────
//   4   │ Типы услуг
// ──────┼─────────────────────────
//  menu │ Вернуться в меню
// ──────┴─────────────────────────




//                                   Клиенты
// ─────────────┬─────────────────┬─────────────────┬────────────────────┬─────────────────────────────     --------> "─────────────┬─────────────────┬─────────────────┬────────────────────┬─────────────────────────────"
//  Код клиента │ Фамилия         │ Имя             │ Отчество           │ Место жительства                 --------> " Код клиента │ Фамилия         │ Имя             │ Отчество           │ Место жительства"
// ─────────────┼─────────────────┼─────────────────┼────────────────────┼─────────────────────────────     --------> "─────────────┼─────────────────┼─────────────────┼────────────────────┼─────────────────────────────"
//  4294967295  │ Константинова   │ Максимильян     │ Константиновна     │ г. Кременчуг-Константиновск      --------> $" {client.Id,-10}  │ {client.LastName,-15} │ {client.FirstName,-15} │ {client.Patronymic,-18} │ {client.PlaceOfResidence,-27}"
// ─────────────┴─────────────────┴─────────────────┴────────────────────┴─────────────────────────────     --------> "─────────────┴─────────────────┴─────────────────┴────────────────────┴─────────────────────────────"




//                               заказы
// ────────────┬─────────────┬─────────────┬────────────┬────────────                                       --------> "────────────┬─────────────┬─────────────┬────────────┬────────────"
//  Код заказа │ Код клиента │ Дата заказа │ Код услуги │ Количество                                        --------> " Код заказа │ Код клиента │ Дата заказа │ Код услуги │ Количество"
// ────────────┼─────────────┼─────────────┼────────────┼────────────                                       --------> "────────────┼─────────────┼─────────────┼────────────┼────────────"
//  4294967295 │ 4294967295  │ DD.MM.YYYY  │ 65535      │ 65535                                             --------> $" {order.OrderCode,-10} │ {order.ClientCode,-10}  │ {order.DateOrder,-10}  │ {order.ServiceCode,-5}      │ {order.Quantity,-5}"
// ────────────┴─────────────┴─────────────┴────────────┴────────────                                       --------> "────────────┴─────────────┴─────────────┴────────────┴────────────"




//                           Услуги
// ────────────┬──────────┬─────────────────────────────────────┬───────────────                            --------> "────────────┬──────────┬─────────────────────────────────────┬───────────────"
//  Код услуги │ Код типа │ Название                            │ Стоимость                                 --------> " Код услуги │ Код типа │ Название                          │ Стоимость"
// ────────────┼──────────┼─────────────────────────────────────┼───────────────                            --------> "────────────┼──────────┼───────────────────────────────────┼───────────────"
//  65535      │ 65535    │ Индивидуальный графический дизайн   │ 4294967295 р.                             --------> $" {service.ServiceCode,-5}      │ {service.TypeCode,-5}    │ {service.Title,-35} │ {service.Cost,-10} р."
// ────────────┴──────────┴─────────────────────────────────────┴───────────────                            --------> "────────────┴──────────┴───────────────────────────────────┴───────────────"




//                    Типы услуг
// ──────────┬─────────────────────────────────────                                                         --------> "──────────┬───────────────────────────────────"
//  Код типа │ Название                                                                                     --------> " Код типа │ Название"
// ──────────┼─────────────────────────────────────                                                         --------> "──────────┼───────────────────────────────────"
//  65535    │ Индивидуальный графический дизайн                                                            --------> $" {typeService.TypeCode,-5}    │ {typeService.Title,-35}"
// ──────────┴─────────────────────────────────────                                                         --------> "──────────┴───────────────────────────────────"