namespace BetterNumberSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Number myNum = new Number(
                5,
                NumberUnit.METRE
                );
            Number myNum2 = new Number(
                50,
                NumberUnit.METRE
                );
            Console.WriteLine(myNum);
            Console.WriteLine(myNum2);
            Console.WriteLine(myNum2 * myNum);
            Console.WriteLine((myNum2 * myNum) * myNum);
        }
    }
}