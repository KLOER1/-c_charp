using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace FourthLaboratoryWork_Task12345
{
    internal class CollectionsWork<T>
    {
        public static List<T> CreateList()
        {
            
            return new List<T>();
        }

        public static List<T> Task1(List<T> L) // Задание 1
        {
            L.Add(L[0]);            // Дублирование первого элемента в конец
            L.RemoveAt(0);          // Удаление первого элемента
            return L;
        }

        public static List<T> CreateLinkedList()
        {

            return new List<T>();
        }

        public static void Task2() // Задание 2
        {

        }

        public static void CreateHashSet()
        {

        }

        public static (HashSet<string>, HashSet<string>, HashSet<string>) Task3(HashSet<string> Factory, HashSet<HashSet<string>> ConsumerBasket) // Задание 3
        {
            HashSet<string> All = new HashSet<string>(Factory);
            HashSet<string> Partly = new HashSet<string>(Factory);
            HashSet<string> Nothing = new HashSet<string>(Factory);

            foreach (HashSet<string> customer in ConsumerBasket)
            {
                All.IntersectWith(customer);
                Nothing.ExceptWith(customer);
            }

            Partly.ExceptWith(All);
            Partly.ExceptWith(Nothing);

            return (All, Partly, Nothing);
        }

        public static void FileWriterStringInLine(string path, List<string> text)
        {
            using (FileStream filestream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter file = new StreamWriter(filestream))
                {
                    file.Write(text[0]);

                }
            }
        }

        public static List<string> FileReaderStringInLine(string path)
        {
            List<string> result = new List<string>();
            using (StreamReader filestream = new StreamReader(path))
            {         
                while (!filestream.EndOfStream)
                {
                    string line = filestream.ReadLine();
                    foreach (string word in line.Split('.', ',', '!', '?', ';', ':', '-', '(', ')'))
                        result.Add(word);
                }
            }
            return result; 
        }

        public static HashSet<char> Task4(string path) // Задание 4
        {
            // список глухих согласных букв: к п с т ф х ц ч ш щ
            char[] DeafConsonantLettersInAlphabeticalOrder = { 'к', 'п', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ' };
            HashSet<char> OddSet = new HashSet<char>(DeafConsonantLettersInAlphabeticalOrder);  // для нечетных слов
            HashSet<char> EvenSet = new HashSet<char>(); // для чётных слов

            foreach (string word in FileReaderStringInLine(path))
            {
                HashSet<char> CurrentWordSet = new HashSet<char>(word.ToLower());
                if (word.Length % 2 == 0) EvenSet.UnionWith(CurrentWordSet); // Операция пересечения множества
                else OddSet.IntersectWith(CurrentWordSet);                   // Операция вычитания множеств
            }
            OddSet.ExceptWith(EvenSet);

            return OddSet;
        }

        public void Serialize(string path, List<SourCream> list)
        {
            using (FileStream filestream = new FileStream(path, FileMode.Create))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SourCream>));
                xmlSerializer.Serialize(filestream, list);
            }
        }

        public static List<SourCream> Deserialize(string path)
        {
            List<SourCream> Result = new List<SourCream>();
            using (FileStream filestream = new FileStream(path, FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SourCream>));
                Result = (List<SourCream>)xmlSerializer.Deserialize(filestream);
            }

            return Result;
        }

        public static (uint, uint, uint) Task5(string path) //задание 5
        {

            Dictionary<uint, (uint, uint)> Result = new Dictionary<uint, (uint, uint)>()
            { 
                { 15, (uint.MaxValue, 0) },
                { 20, (uint.MaxValue, 0) },
                { 25, (uint.MaxValue, 0) }
            }; // ключ : (цена, количество)
            foreach (SourCream sourCream in Deserialize(path))
            {
                if (Result[sourCream.FatContent].Item1 > sourCream.Price)
                    Result[sourCream.FatContent] = (sourCream.Price, 1);
                else if (Result[sourCream.FatContent].Item1 == sourCream.Price)
                    Result[sourCream.FatContent] = (sourCream.Price, Result[sourCream.FatContent].Item2 + 1);
            }
                
            return (Result[15].Item2, Result[20].Item2, Result[25].Item2);
        }
    }
}
