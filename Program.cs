namespace BetterNumberSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Number myNum = new Number(
                -50,
                MeasurementType.Angle,
                NumberUnit.GetNumberUnitByFullName("Degree")
                );
            Number myNum2 = new Number(
                60,
                MeasurementType.Angle,
                NumberUnit.GetNumberUnitByFullName("Degree")
                );
            Console.WriteLine(myNum);
            Console.WriteLine(myNum2);
            Console.WriteLine(myNum - myNum2);
        }
    }
}