﻿namespace RockyStudios.BetterNumberSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Number myNum = Number.Parse("100000km Length");
            Number myNum2 = new Number(
                15.2354,
                MeasurementType.Length,
                NumberUnit.GetNumberUnitByFullName("Metre")
                );
            Console.WriteLine(myNum2);
        }
    }
}