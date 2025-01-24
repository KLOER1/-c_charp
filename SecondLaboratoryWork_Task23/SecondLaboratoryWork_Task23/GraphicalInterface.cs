using System;

namespace SecondLaboratoryWork_Task23
{
    public class GraphicalInterface
    {
        string Message;

        public void Menu(ref Time time1, ref Time time2)
        {
            while (Message != "end")
            {
                Console.Clear();
                Console.Write("       Меню        │      Вариант 7\n" +
                              "───────────────────┼────────────────────\n" +
                             $" Time1:   {time1.ToString(),5}    │ Time2:   {time2.ToString(),5}\n" +
                              "───┬───────────────┴────────────────────\n" +
                              " 1 │ Вычесть из переменной Time1 Time2\n" +
                              "───┼────────────────────────────────────\n" +
                              " 2 │ Вычесть из переменной Time2 Time1\n" +
                              "───┼────────────────────────────────────\n" +
                              " 3 │ Добавить минуту к объекту Time1\n" +
                              "───┼────────────────────────────────────\n" +
                              " 4 │ Вычесть минуту из объекта Time2\n" +
                              "───┼────────────────────────────────────\n" +
                              " 5 │ Преобразовать неявно в int\n" +
                              "───┼────────────────────────────────────\n" +
                              " 6 │ Преобразовать явно в bool\n" +
                              "───┼────────────────────────────────────\n" +
                              " 7 │ Бинарная операция Time1 < Time2\n" +
                              "───┼────────────────────────────────────\n" +
                              " 8 │ Бинарная операция Time1 > Time2\n" +
                              "───┼────────────────────────────────────\n" +
                              "end│ Завершение программы\n" +
                              "───┴────────────────────────────────────\n" +
                              " >> ");
                Message = Console.ReadLine();
                switch (Message)
                {
                    case "1":
                        Subtraction(time1, time2);
                        break;
                    case "2":
                        Subtraction(time2, time1);
                        break;
                    case "3":
                        time1++;
                        break;
                    case "4":
                        time2--;
                        break;
                    case "5":
                        ImplisitOperatorInt(time1, time2);
                        break;
                    case "6":
                        ExplisitOperatorBool(time1, time2);
                        break;
                    case "7":
                        BinaryOperator(time1, time2, '<');
                        break;
                    case "8":
                        BinaryOperator(time1, time2, '>');
                        break;
                    default:
                        break;
                }
            }
        }

        public void Subtraction(Time time1, Time time2)
        {
            while (Message != "back")
            {
                Console.Clear();
                Console.Write($"           {time1.ToString(),5} - {time2.ToString(),5}\n" +
                              "──────────────────────────────────────────\n" +
                             $" Результат: {time1.Subtraction(time2).ToString()}\n" +
                              "──────┬───────────────────────────────────\n" +
                              " back │ Вернуться в меню\n" +
                              "──────┴───────────────────────────────────\n" +
                              " >> ");
                Message = Console.ReadLine();
                switch (Message)
                {
                    default:
                        break;
                }
            }
        }

        public void ImplisitOperatorInt(Time time1, Time time2)
        {
            while (Message != "back")
            {
                Console.Clear();
                Console.Write("          Преобразование в int\n" +
                              "───────────────────┬──────────────────────\n" +
                             $"(int)Time1:  {(int)time1,4}  │ (int)Time2:  {(int)time2,4}\n" +
                              "──────┬────────────┴──────────────────────\n" +
                              " back │ Вернуться в меню\n" +
                              "──────┴───────────────────────────────────\n" +
                              " >> ");
                Message = Console.ReadLine();
                switch (Message)
                {
                    default:
                        break;
                }
            }
        }

        public void ExplisitOperatorBool(Time time1, Time time2)
        {
            while (Message != "back")
            {
                bool BoolTime1 = (bool)time1, BoolTime2 = (bool)time2;
                Console.Clear();
                Console.Write("          Преобразование в int\n" +
                              "─────────────────────┬────────────────────\n" +
                             $"(bool)Time1: {BoolTime1,5}   │ (bool)Time2: {BoolTime2,5}\n" +
                              "──────┬──────────────┴────────────────────\n" +
                              " back │ Вернуться в меню\n" +
                              "──────┴───────────────────────────────────\n" +
                              " >> ");
                Message = Console.ReadLine();
                switch (Message)
                {
                    default:
                        break;
                }
            }
        }

        public void BinaryOperator(Time time1, Time time2, char letter)
        {
            while (Message != "back")
            {
                bool BoolTime1 = (bool)time1, BoolTime2 = (bool)time2;
                Console.Clear();
                Console.Write($"     Бинарная операция Time1 {letter} Time2\n" +
                               "──────────────────────────────────────────\n" +
                              $" Результат: {(letter == '>' ? time1 > time2 : time1 < time2)}\n" +
                               "──────┬───────────────────────────────────\n" +
                               " back │ Вернуться в меню\n" +
                               "──────┴───────────────────────────────────\n" +
                               " >> ");
                Message = Console.ReadLine();
                switch (Message)
                {
                    default:
                        break;
                }
            }
        }
    }
}
