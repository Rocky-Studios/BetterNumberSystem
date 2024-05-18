namespace BetterNumberSystem
{
    /// <summary>
    /// A unit of measurement with two smaller units eg. km/h
    /// </summary>
    public class PerNumberUnit : INumberUnit
    {
        /// <summary>
        /// The first unit of the 'per' unit
        /// </summary>
        public NumberUnit UnitA;
        /// <summary>
        /// The second unit of the 'per' unit
        /// </summary>
        public NumberUnit UnitB;
        /// <summary>
        /// The full name of the unit eg. Millimetre
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// The plural full name of the unit eg. Millimetres
        /// </summary>
        public string FullNamePlural = "";
        /// <summary>
        /// The short suffix of the unit eg. mm
        /// </summary>
        public string Suffix { get; set; }
        /// <summary>
        /// Whether this unit is a base of its category eg. metres is the base for length
        /// </summary>
        public bool BaseUnit = false;
        /// <summary>
        /// The ratio of this unit to the base unit eg. for millimetres the base unit is metres so the proportion is 1/1000 or 0.001
        /// </summary>
        public double ProportionToBaseUnit = 1;
        /// <summary>
        /// Whether this unit can be negative
        /// </summary>
        public bool CanBeNegative { get; set; }
        public MeasurementType MeasurementType;

        public PerNumberUnit(NumberUnit unitA, NumberUnit unitB, MeasurementType measurementType, bool baseUnit = false, double proportionalToBaseUnit = 1, bool canBeNegative = false)
        {
            UnitA = unitA; UnitB = unitB;
            FullName = unitA.FullName + " Per " + unitB.FullName;
            Suffix = unitA.Suffix + "/" + unitB.Suffix;
            MeasurementType = measurementType;
            if (_perNumberUnits == null) _perNumberUnits = new List<PerNumberUnit>();
            _perNumberUnits.Add(this);
            CanBeNegative = canBeNegative;
        }

        private static List<PerNumberUnit> _perNumberUnits = new()
        {
            new PerNumberUnit(NumberUnit.GetNumberUnitByFullName("Metre"), NumberUnit.GetNumberUnitByFullName("Second"), MeasurementType.Speed, baseUnit: true)
        };

        public static PerNumberUnit? GetPerNumberUnitByFullName(string fullName)
        {
            foreach (PerNumberUnit numberUnit in _perNumberUnits)
            {
                if (fullName.Equals(numberUnit.FullName))
                {
                    return numberUnit;
                }
            }
            return null;
        }
    }
}
