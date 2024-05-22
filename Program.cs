namespace BetterNumberSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Number myNum = new Number(
                5,
                MeasurementType.Length,
                NumberUnit.GetNumberUnitByFullName("Metre")
                );
            Number myNum2 = new Number(
                50,
                MeasurementType.Length,
                NumberUnit.GetNumberUnitByFullName("Metre")
                );
            Console.WriteLine(myNum);
            Console.WriteLine(myNum2);
            Console.WriteLine(myNum2 * myNum);
            Console.WriteLine((myNum2 * myNum) * myNum);
        }
    }
}