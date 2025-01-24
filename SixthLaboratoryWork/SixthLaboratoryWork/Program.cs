namespace SixthLaboratoryWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cat barsik = new Cat("Барсик");
            Cat Tisha = new Cat("Тиша");

            MeowCount countingForBarsik = new MeowCount(barsik);
            MeowCount countingForTisha = new MeowCount(Tisha);

            countingForBarsik.meow();
            countingForBarsik.meow(3);

            
            countingForTisha.meow();

            List<IMeowable> meowables = new List<IMeowable>() { countingForBarsik, countingForTisha };
            Meowing.meowing(meowables);

            Console.WriteLine($"Количество мяуканий Барсика: {countingForBarsik.count}");
            Console.WriteLine($"Количество мяуканий Тиши: {countingForTisha.count}");
        }
    }
}