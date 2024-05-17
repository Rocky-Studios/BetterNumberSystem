namespace RockyStudios.BetterNumberSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Number myNum = Number.Parse("100000Millimetre Length");
            Console.WriteLine(myNum);
            Console.WriteLine(myNum.Get(unit: true, scientific: true, type: true));
        }
    }
}