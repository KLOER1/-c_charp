using System;
using System.Collections.Generic;

namespace FourthLaboratoryWork_Task12345
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string message;

            // Задание 1. Составить программу, которая переносит в конец непустого списка L его первый элемент.

            //List<object> list = new List<object>();
            //list.Add("1");
            //list.Add("2");
            //list.Add("3");
            //list.Add(new A());
            //list.Add("5");


            //foreach (object item in list)
            //    Console.WriteLine(item.ToString());

            //object Temp = list[0];

            //for (int i = 0; i<list.Count - 1; i++)
            //    list[i] = list[i + 1];
            //list[list.Count - 1] = Temp;
            //Console.Clear();
            //Console.WriteLine("───┬─────────────────────────────────");
            //for (int i = 0; i<list.Count; i++)
            //{
            //    Console.WriteLine($" { i } │ { list[i].ToString() }");
            //    if (i != list.Count - 1)
            //        Console.WriteLine("───┼─────────────────────────────────");
            //}
            //Console.WriteLine("───┴─────────────────────────────────");


            //Задание 2.Из списка L, содержащего не менее двух элементов, удалить все элементы, у которых одинаковые
            // «соседи» (первый и последний элементы считать соседями).
            LinkedList<object> L = new LinkedList<object>();
            L.AddLast("1");
            L.AddLast("2");
            L.AddLast("1");
            L.AddLast("1");
            L.AddLast("1");

            LinkedListNode<object> node = L.First;

            while (node != null && L.Count > 1)
            {
                if (node.Previous == null && (L.Last.Value == node.Next.Value))
                {
                    node = node.Next;
                    L.Remove(node.Previous);
                }

                else if (node.Next == null && (node.Previous.Value == L.First.Value))
                {
                    node = node.Previous;
                    L.Remove(node.Next);
                }

                else if (node.Previous != null && node.Next != null && node.Previous.Value == node.Next.Value)
                {
                    node = node.Previous;
                    L.Remove(node.Next);
                }

                else
                    node = node.Next;
                foreach (object item in L)
                    Console.WriteLine(item.ToString());
                Console.WriteLine();
            }

            // Задание 3. Есть перечень мебельных фабрик, продукция которых представлена в мебельном магазине.
            // Известно, мебель каких фабрик приобреталась n покупателями. Определить для каждой фабрики,
            // мебель каких из них приобреталась всеми покупателями, каких — некоторыми из покупателей, и
            // каких — никем из покупателей.
            //HashSet<string> Factory = new HashSet<string>() { "Фабрика1", "Фабрика2", "Фабрика3", "Фабрика4" };
            //HashSet<HashSet<string>> ConsumerBasket = new HashSet<HashSet<string>>()
            //{
            //    new HashSet<string> { "Фабрика1", "Фабрика2", "Фабрика3", "Фабрика4" },
            //    new HashSet<string> { "Фабрика1", "Фабрика2", "Фабрика3", "Фабрика4" },
            //    new HashSet<string> { "Фабрика2", "Фабрика3", "Фабрика4" },
            //    new HashSet<string> { "Фабрика1", "Фабрика2", "Фабрика3", "Фабрика4" },
            //    new HashSet<string> { "Фабрика1", "Фабрика2", "Фабрика3", "Фабрика4" },
            //    new HashSet<string> { "Фабрика1", "Фабрика2", "Фабрика3", "Фабрика4" },
            //    new HashSet<string> { "Фабрика1", "Фабрика2", "Фабрика3", "Фабрика4" },
            //    new HashSet<string> { "Фабрика1", "Фабрика2", "Фабрика4" },
            //    new HashSet<string> { "Фабрика1", "Фабрика2", "Фабрика3", "Фабрика4" },
            //    new HashSet<string> { "Фабрика1", "Фабрика2", "Фабрика3", "Фабрика4" },
            //    new HashSet<string> { "Фабрика1", "Фабрика2", "Фабрика3", "Фабрика4" },
            //};

            //(HashSet<string>, HashSet<string>, HashSet<string>) result = CollectionsWork<object>.Task3(Factory, ConsumerBasket);
            //Console.WriteLine("Фабрики, чью мебель купили ВСЕ: ");
            //foreach (string all in result.Item1)
            //    Console.WriteLine(all);

            //Console.WriteLine("\nФабрики, чью мебель купили ПОЧТИ ВСЕ: ");
            //foreach (string partly in result.Item2)
            //    Console.WriteLine(partly);

            //Console.WriteLine("\nФабрики, чью мебель НИКТО не купил: ");
            //foreach (string nothing in result.Item3)
            //    Console.WriteLine(nothing);

            //Задание 4.Файл содержит текст на русском языке.Напечатать в алфавитном порядке все глухие согласные
            //буквы, которые входят в каждое нечетное слово и не входят хотя бы одно четное слово.

            string path = "Task4.txt";
            List<string> text = new List<string>();
            text.Add("ППППППКККККТТ");
            text.Add("ПППППККККК");

            CollectionsWork<string>.FileWriterStringInLine(path, text);

            foreach (char symbol in CollectionsWork<string>.Task4(path))
                Console.WriteLine(symbol);


            // Задание 5


        }
    }
}
