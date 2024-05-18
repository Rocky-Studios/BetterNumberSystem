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
        public NumberUnit(string fullName, string suffix, MeasurementType measurementType, bool baseUnit = false, double proportionalToBaseUnit = 1, bool canBeNegative = false)
        {
            FullName = fullName;
            FullNamePlural = fullName + "s";
            Suffix = suffix;
            BaseUnit = baseUnit;
            ProportionToBaseUnit = proportionalToBaseUnit;
            MeasurementType = measurementType;
            CanBeNegative = canBeNegative;
            if (_numberUnits == null) _numberUnits = new List<NumberUnit>();
            _numberUnits.Add(this);
        }

        /// <summary>
        /// The list of all number units avaiable for use
        /// </summary>
        private static List<NumberUnit> _numberUnits = new()
        {
            new NumberUnit("Plain", "", MeasurementType.Plain, baseUnit: true, canBeNegative: true),
            // Length
            new NumberUnit("Millimetre", "mm", MeasurementType.Length, proportionalToBaseUnit: 1000),
            new NumberUnit("Centimetre", "cm", MeasurementType.Length, proportionalToBaseUnit: 100),
            new NumberUnit("Metre", "m", MeasurementType.Length, baseUnit: true),
            new NumberUnit("Kilometre", "km", MeasurementType.Length, proportionalToBaseUnit: 0.001f),
            // Area
            new NumberUnit("SqMillimetre", "mm²", MeasurementType.Area, proportionalToBaseUnit: 1000000),
            new NumberUnit("SqCentimetre", "cm²", MeasurementType.Area, proportionalToBaseUnit: 10000),
            new NumberUnit("SqMetre", "m²", MeasurementType.Area, baseUnit: true),
            new NumberUnit("SqKilometre", "km²", MeasurementType.Area, proportionalToBaseUnit: 0.000001f),
            new NumberUnit("Hectare", "ha", MeasurementType.Area, proportionalToBaseUnit: 0.0001f),
            // Volume
            new NumberUnit("CuMillimetre", "mm³", MeasurementType.Volume, proportionalToBaseUnit: 1000000000),
            new NumberUnit("CuCentimetre", "cm³", MeasurementType.Volume, proportionalToBaseUnit: 1000000),
            new NumberUnit("CuMetre", "m³", MeasurementType.Volume, baseUnit: true),
            new NumberUnit("CuKilometre", "km³", MeasurementType.Volume, proportionalToBaseUnit: 0.000000001f),
            new NumberUnit("Millilitre", "ml", MeasurementType.Volume, proportionalToBaseUnit: 1000000f),
            new NumberUnit("Litre", "l", MeasurementType.Volume, proportionalToBaseUnit: 1000f),
            new NumberUnit("Kilolitre", "kl", MeasurementType.Volume, proportionalToBaseUnit: 1f),
            // Time
            new NumberUnit("Millisecond", "ms", MeasurementType.Time, proportionalToBaseUnit: 3600000f),
            new NumberUnit("Second", "s", MeasurementType.Time, proportionalToBaseUnit: 3600f),
            new NumberUnit("Minute", "m", MeasurementType.Time, proportionalToBaseUnit: 60f),
            new NumberUnit("Hour", "h", MeasurementType.Time, baseUnit: true),
            new NumberUnit("Day", "d", MeasurementType.Time, proportionalToBaseUnit: 1f/24f),
            new NumberUnit("Week", "w", MeasurementType.Time, proportionalToBaseUnit: 1f/(24f*7f)),
            new NumberUnit("Year", "y", MeasurementType.Time, proportionalToBaseUnit: 1f/(24f*365f)),
            new NumberUnit("Decade", "d", MeasurementType.Time, proportionalToBaseUnit: 1f/(24f*365f*10)),
            new NumberUnit("Century", "c", MeasurementType.Time, proportionalToBaseUnit: 1f /(24f * 365f * 100)),
            new NumberUnit("Millenium", "m", MeasurementType.Time, proportionalToBaseUnit: 1f /(24f * 365f * 1000)),
            // Mass
            new NumberUnit("Milligram", "mg", MeasurementType.Mass, proportionalToBaseUnit: 1000f),
            new NumberUnit("Gram", "g", MeasurementType.Mass, baseUnit: true),
            new NumberUnit("Kilogram", "kg", MeasurementType.Mass, proportionalToBaseUnit: 0.001f),
            new NumberUnit("Tonne", "t", MeasurementType.Mass, proportionalToBaseUnit: 0.000001f),
            // Angles
            new NumberUnit("Radian", "rad", MeasurementType.Angle, proportionalToBaseUnit: MathF.PI/180f, canBeNegative: true),
            new NumberUnit("Degree", "°", MeasurementType.Angle, baseUnit: true, canBeNegative: true)
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
        bool CanBeNegative { get; set; }
        string FullName { get; set; }
    }
}
