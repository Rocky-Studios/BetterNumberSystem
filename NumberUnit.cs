namespace BetterNumberSystem
{
    /// <summary>
    /// A singular unit of measurement
    /// </summary>
    public class NumberUnit : INumberUnit
    {
        /// <summary>
        /// The full name of the unit eg. Millimetre
        /// </summary>
        public string FullName = "";
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
        public MeasurementType MeasurementType;
        public NumberUnit(string fullName, string suffix, MeasurementType measurementType, bool baseUnit = false, double proportionalToBaseUnit = 1)
        {
            FullName = fullName;
            FullNamePlural = fullName + "s";
            Suffix = suffix;
            BaseUnit = baseUnit;
            ProportionToBaseUnit = proportionalToBaseUnit;
            MeasurementType = measurementType;
            if (_numberUnits == null) _numberUnits = new List<NumberUnit>();
            _numberUnits.Add(this);
        }

        /// <summary>
        /// The list of all number units avaiable for use
        /// </summary>
        private static List<NumberUnit> _numberUnits = new()
        {
            new NumberUnit("Plain", "", MeasurementType.Plain, baseUnit: true),
            // Length
            new NumberUnit("Millimetre", "mm", MeasurementType.Length, proportionalToBaseUnit: 1000),
            new NumberUnit("Centimetre", "cm", MeasurementType.Length, proportionalToBaseUnit: 100),
            new NumberUnit("Metre", "m", MeasurementType.Length, baseUnit: true),
            new NumberUnit("Kilometre", "km", MeasurementType.Length, proportionalToBaseUnit: 0.001f),
            // Time
            new NumberUnit("Millisecond", "ms", MeasurementType.Time, proportionalToBaseUnit: 3600000f),
            new NumberUnit("Second", "s", MeasurementType.Time, proportionalToBaseUnit: 3600f),
            new NumberUnit("Minute", "m", MeasurementType.Time, proportionalToBaseUnit: 60f),
            new NumberUnit("Hour", "h", MeasurementType.Time, baseUnit: true),
            new NumberUnit("Day", "d", MeasurementType.Time, proportionalToBaseUnit: 24f),
            new NumberUnit("Week", "w", MeasurementType.Time, proportionalToBaseUnit: 24f*7f),
            new NumberUnit("Year", "y", MeasurementType.Time, proportionalToBaseUnit: 24f*365f),
            new NumberUnit("Decade", "d", MeasurementType.Time, proportionalToBaseUnit: 24f*365f*10),
            new NumberUnit("Century", "c", MeasurementType.Time, proportionalToBaseUnit: 24f*365f*100),
            new NumberUnit("Millenium", "m", MeasurementType.Time, proportionalToBaseUnit: 24f*365f*1000),
        };
        /// <summary>
        /// Gets all the currently available number units
        /// </summary>
        /// <returns></returns>
        public static List<NumberUnit> GetNumberUnits()
        {
            return _numberUnits;
        }
        /// <summary>
        /// Query for a numberUnit by its full name
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static NumberUnit? GetNumberUnitByFullName(string fullName)
        {
            foreach (NumberUnit numberUnit in _numberUnits)
            {
                if (fullName.Equals(numberUnit.FullName))
                {
                    return numberUnit;
                }
            }
            return null;
        }
        /// <summary>
        /// Query for a numberUnit by its suffix
        /// </summary>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static NumberUnit? GetNumberUnitBySuffix(string suffix)
        {
            foreach (NumberUnit numberUnit in _numberUnits)
            {
                if (suffix.Equals(numberUnit.Suffix))
                {
                    return numberUnit;
                }
            }
            return null;
        }
    }

    public interface INumberUnit
    {
        string Suffix { get; set; }
    }
}
