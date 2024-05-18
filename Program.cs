﻿namespace BetterNumberSystem
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
                NumberUnit.GetNumberUnitByFullName("CuMetre")
                );
            Console.WriteLine(myNum2);
            Number myNum2Cm = myNum2.Convert(NumberUnit.GetNumberUnitByFullName("Litre"));
            Console.WriteLine(myNum2Cm);
            Console.WriteLine(myNum2 <= myNum2Cm);
        }
    }
}