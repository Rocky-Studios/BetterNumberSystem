namespace BetterNumberSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Number myNum = new Number(
                60,
                MeasurementType.Volume,
                NumberUnit.GetNumberUnitByFullName("CuMetre")
                );
            Number myNum2 = new Number(
                50,
                MeasurementType.Volume,
                NumberUnit.GetNumberUnitByFullName("Litre")
                );
            Console.WriteLine(myNum);
            Console.WriteLine(myNum2);
            Console.WriteLine(myNum + myNum2);
        }
    }
}