namespace BetterNumberSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Number myNum2 = new Number(
                2,
                MeasurementType.Angle,
                NumberUnit.GetNumberUnitByFullName("Radian")
                );
            Console.WriteLine(myNum2);
            Number myNum2Cm = myNum2.Convert(NumberUnit.GetNumberUnitByFullName("Degree"));
            Console.WriteLine(myNum2Cm);
        }
    }
}