namespace BetterNumberSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Number myNum2 = new Number(
                60,
                MeasurementType.Length,
                NumberUnit.GetNumberUnitByFullName("Metre")
                );
            Console.WriteLine(myNum2);
            Number myNum2Cm = myNum2.Convert(NumberUnit.GetNumberUnitByFullName("Centimetre"));
            Console.WriteLine(myNum2Cm);
        }
    }
}