using BetterNumberSystem.Expression;

namespace BetterNumberSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Number myNum = new Number(
                15,
                UnitManager.Unit["Picofarad"]
                );
            Console.WriteLine(myNum + myNum);
        }
    }
}