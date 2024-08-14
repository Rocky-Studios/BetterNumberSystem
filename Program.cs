namespace BetterNumberSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Number myNum = new Number(
                15,
                UnitManager.Unit["Millimetre"]
                );
            Console.WriteLine(myNum);
            Console.WriteLine(myNum.Convert(UnitManager.Unit["Metre"]));
        }
    }
}