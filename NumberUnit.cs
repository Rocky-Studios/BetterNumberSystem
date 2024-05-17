namespace RockyStudios.BetterNumberSystem
{
    /// <summary>
    /// A unit of measurement
    /// </summary>
    public class NumberUnit
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
        public string Suffix = "";
        /// <summary>
        /// Whether this unit is a base of its category eg. metres is the base for length
        /// </summary>
        public bool BaseUnit = false;
        /// <summary>
        /// The ratio of this unit to the base unit eg. for millimetres the base unit is metres so the proportion is 1/1000 or 0.001
        /// </summary>
        public double ProportionToBaseUnit = 1;
        public NumberUnit(string fullName, string suffix, bool baseUnit = false, double proportionalToBaseUnit = 1)
        {
            FullName = fullName;
            FullNamePlural = fullName + "s";
            Suffix = suffix;
            BaseUnit = baseUnit;
            ProportionToBaseUnit = proportionalToBaseUnit;
            if (_numberUnits == null) _numberUnits = new List<NumberUnit>();
            _numberUnits.Add(this);
        }

        /// <summary>
        /// The list of all number units avaiable for use
        /// </summary>
        private static List<NumberUnit> _numberUnits = new()
        {
            new NumberUnit("Plain", "", baseUnit: true),
            new NumberUnit("Millimetre", "mm", proportionalToBaseUnit: 0.001),
            new NumberUnit("Centimetre", "cm", proportionalToBaseUnit: 0.01),
            new NumberUnit("Metre", "m", baseUnit: true),
            new NumberUnit("Kilometre", "km", proportionalToBaseUnit: 1000),
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
}
