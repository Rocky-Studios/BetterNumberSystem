namespace BetterNumberSystem;

/// <summary>
///     A singular unit of measurement
/// </summary>
public class Unit
{
    /// <summary>
    ///     Prefixes added to units to produce multiples and submultiples of the original unit
    ///     The key is the exponent eg. 3 for kilo or -3 for milli, and the value is the name and symbol
    /// </summary>
    public static Dictionary<int, (string, string)> Prefixes = [];

    #region Fields

    /// <summary>
    ///     The full name of the unit eg. Millimetre
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    ///     The plural full name of the unit eg. Millimetres
    /// </summary>
    public string FullNamePlural;

    /// <summary>
    ///     The short suffix of the unit eg. mm
    /// </summary>
    public string Suffix { get; set; }

    /// <summary>
    ///     Whether this unit can be negative
    /// </summary>
    public bool CanBeNegative { get; set; }

    /// <summary>
    ///     The type of measurement this unit represents
    /// </summary>
    public MeasurementType MeasurementType;

    /// <summary>
    ///     A function to convert a value from this unit to the base unit or vice versa
    /// </summary>
    public delegate double ConversionFunction(double value);

    /// <summary>
    ///     Function to convert from this unit to the base unit
    /// </summary>
    public ConversionFunction ToBaseUnit { get; set; }

    /// <summary>
    ///     Function to convert from the base unit to this unit
    /// </summary>
    public ConversionFunction FromBaseUnit { get; set; }

    #endregion

    /// <summary>
    ///     Initialises a number unit
    /// </summary>
    /// <param name="fullName"> The full name </param>
    /// <param name="suffix"> The suffix after a number </param>
    /// <param name="measurementType"> Which category of measurement it goes into </param>
    /// <param name="toBaseUnit"> Function to convert to the base unit </param>
    /// <param name="fromBaseUnit"> Function to convert from the base unit </param>
    /// <param name="canBeNegative"> Whether this value can be negative </param>
    public Unit(string fullName, string suffix, MeasurementType measurementType, ConversionFunction toBaseUnit,
        ConversionFunction fromBaseUnit, bool canBeNegative = false)
    {
        FullName = fullName;
        FullNamePlural = fullName + "s";
        Suffix = suffix;
        ToBaseUnit = toBaseUnit;
        FromBaseUnit = fromBaseUnit;
        MeasurementType = measurementType;
        CanBeNegative = canBeNegative;
        UnitManager.Units.Add(fullName, this);
    }

    /// <summary>
    ///     Initialises a number unit
    /// </summary>
    /// <param name="fullName"> The full name </param>
    /// <param name="suffix"> The suffix after a number </param>
    /// <param name="measurementType"> Which category of measurement it goes into </param>
    /// <param name="generatePrefixes"> Whether to auto-generate additionally units for milli, kilo etc. </param>
    /// <param name="canBeNegative"> Whether this value can be negative </param>
    public Unit(string fullName, string suffix, MeasurementType measurementType, bool generatePrefixes,
        bool canBeNegative = false)
    {
        FullName = fullName;
        FullNamePlural = fullName + "s";
        Suffix = suffix;
        ToBaseUnit = value => value;
        FromBaseUnit = value => value;
        MeasurementType = measurementType;
        CanBeNegative = canBeNegative;
        UnitManager.Units.Add(fullName, this);
        if (generatePrefixes)
            foreach (KeyValuePair<int, (string, string)> prefix in Prefixes)
                _ = new Unit(prefix.Value.Item1 + fullName.ToLower(), prefix.Value.Item2 + suffix, measurementType,
                    false, canBeNegative)
                {
                    ToBaseUnit = value => value * Math.Pow(10, prefix.Key),
                    FromBaseUnit = value => value / Math.Pow(10, prefix.Key),
                    MeasurementType = measurementType
                };
    }

    /// <summary>
    ///     Gets all the currently available number units
    /// </summary>
    /// <returns> </returns>
    public static List<Unit> GetNumberUnits()
    {
        List<Unit> units = new();
        foreach (KeyValuePair<string, Unit> unit in UnitManager.Units) units.Add(unit.Value);
        return units;
    }

    /// <summary>
    ///     Query for a numberUnit by its full name
    ///     <br /> Use UnitManager.Unit["XXXXXX"] if possible
    /// </summary>
    /// <param name="fullName"> </param>
    /// <returns> </returns>
    public static Unit? GetNumberUnitByFullName(string fullName)
    {
        foreach (Unit numberUnit in GetNumberUnits())
            if (fullName.Equals(numberUnit.FullName))
                return numberUnit;
        return null;
    }

    /// <summary>
    ///     Query for a numberUnit by its suffix
    /// </summary>
    /// <param name="suffix"> </param>
    /// <returns> </returns>
    public static Unit? GetNumberUnitBySuffix(string suffix)
    {
        foreach (Unit numberUnit in GetNumberUnits())
            if (suffix.Equals(numberUnit.Suffix))
                return numberUnit;
        return null;
    }
}

/// <summary>
///     The manager for all units
/// </summary>
public sealed class UnitManager
{
    private static readonly Lazy<UnitManager> Instance = new(() => new UnitManager());

    /// <summary>
    ///     The instance of the unit manager
    /// </summary>
    public static UnitManager Unit => Instance.Value;

    /// <summary>
    ///     All the units currently available
    /// </summary>
    public static Dictionary<string, Unit> Units = new();

    /// <summary>
    ///     Query for a numberUnit by its full name
    /// </summary>
    /// <param name="name"> </param>
    public Unit this[string name]
    {
        get
        {
            Units.TryGetValue(name, out Unit returnedUnit);
            return returnedUnit ?? throw new KeyNotFoundException($"Unit {name} not found");
        }
    }

    static UnitManager()
    {
        #region Prefixes

        Dictionary<int, (string, string)> prefixes = BetterNumberSystem.Unit.Prefixes;
        // https://en.wikipedia.org/wiki/International_System_of_Units#Prefixes
        prefixes.Add(30, ("Quetta", "Q"));
        prefixes.Add(27, ("Ronna", "R"));
        prefixes.Add(24, ("Yotta", "Y"));
        prefixes.Add(21, ("Zetta", "Z"));
        prefixes.Add(18, ("Exa", "E"));
        prefixes.Add(15, ("Peta", "P"));
        prefixes.Add(12, ("Tera", "T"));
        prefixes.Add(9, ("Giga", "G"));
        prefixes.Add(6, ("Mega", "M"));
        prefixes.Add(3, ("Kilo", "k"));
        prefixes.Add(2, ("Hecto", "h"));
        prefixes.Add(1, ("Deca", "da"));

        prefixes.Add(-30, ("Quecto", "q"));
        prefixes.Add(-27, ("Ronto", "r"));
        prefixes.Add(-24, ("Yocto", "y"));
        prefixes.Add(-21, ("Zepto", "z"));
        prefixes.Add(-18, ("Atto", "a"));
        prefixes.Add(-15, ("Femto", "f"));
        prefixes.Add(-12, ("Pico", "p"));
        prefixes.Add(-9, ("Nano", "n"));
        prefixes.Add(-6, ("Micro", "μ"));
        prefixes.Add(-3, ("Milli", "m"));
        prefixes.Add(-2, ("Centi", "c"));
        prefixes.Add(-1, ("Deci", "prefixes"));

        #endregion

        _ = new Unit("Plain", "", MeasurementType.Plain, value => value, value => value, true);

        _ = new Unit("Metre", "m", MeasurementType.Length, true);
        _ = new Unit("Litre", "L", MeasurementType.Volume, true);
        _ = new Unit("Second", "s", MeasurementType.Time, true);
        _ = new Unit("Gram", "g", MeasurementType.Mass, true);

        //https://en.wikipedia.org/wiki/International_System_of_Units#Derived_units
        // DERVIED UNITS
        _ = new Unit("Hertz", "Hz", MeasurementType.Frequency, true) { FullNamePlural = "Hertz" };
        _ = new Unit("Newton", "N", MeasurementType.Force, true);
        _ = new Unit("Pascal", "Pa", MeasurementType.Pressure, true);
        _ = new Unit("Joule", "J", MeasurementType.Energy, true);
        _ = new Unit("Watt", "W", MeasurementType.Power, true);
        _ = new Unit("Coulomb", "C", MeasurementType.ElectricCharge, true);
        _ = new Unit("Volt", "V", MeasurementType.Voltage, true);
        _ = new Unit("Farad", "F", MeasurementType.Capacitance, true);
        _ = new Unit("Ohm", "Ω", MeasurementType.Resistance, true);
        _ = new Unit("Siemens", "Ω", MeasurementType.Resistance, true) { FullNamePlural = "Siemens" };
        _ = new Unit("Weber", "Wb", MeasurementType.MagneticFlux, true);
        _ = new Unit("Tesla", "T", MeasurementType.MagneticFieldStrength, true);
        _ = new Unit("Henry", "H", MeasurementType.Inductance, true) { FullNamePlural = "Henries" };
        _ = new Unit("Lux", "lx", MeasurementType.Illuminance, true) { FullNamePlural = "Luxes" };

        // Units with weird conversions / units that do not use the metric prefixes
        _ = new Unit("Radian", "rad", MeasurementType.Angle, radians => radians, radians => radians, true);
        _ = new Unit("Degree", "°", MeasurementType.Angle, radians => radians * (Math.PI / 180),
            radians => radians * (180 / Math.PI), true);

        _ = new Unit("Kelvin", "K", MeasurementType.Temperature, kelvin => kelvin, kelvin => kelvin);
        _ = new Unit("Celsius", "°C", MeasurementType.Temperature, kelvin => kelvin + 273.15,
            kelvin => kelvin - 273.15);
    }
}