using BetterNumberSystem.Expression;

namespace BetterNumberSystem
{
    /// <summary>
    /// A singular unit of measurement
    /// </summary>
    public class Unit : Constant
    {
        #region Fields
        /// <summary>
        /// Whether this unit can be negative
        /// </summary>
        public bool CanBeNegative { get; set; }
        public Quantity Quantity;
        /// <summary>
        /// 
        /// </summary>
        public delegate double ConversionFunction(double value);
        /// <summary>
        /// Function to convert from this unit to the base unit
        /// </summary>
        public ConversionFunction ToBaseUnit { get; set; }

        /// <summary>
        /// Function to convert from the base unit to this unit
        /// </summary>
        public ConversionFunction FromBaseUnit { get; set; }
        /// <summary>
        /// This unit expressed in terms of the base units
        /// NOTE: if this is a base unit, this will be an empty array
        /// </summary>
        public PronumeralCollection? UnitAsBaseUnits = []; 
        #endregion

        #region Constructors
        /// <summary>
        /// Initialises a number unit
        /// </summary>
        /// <param name="name">The full name</param>
        /// <param name="symbol">The suffix after a number</param>
        /// <param name="quantity">Which category of measurement it goes into</param>
        /// <param name="toBaseUnit">The conversion function from this to the base unit</param>
        /// <param name="fromBaseUnit">The conversion function from the base unit to this unit</param>
        /// <param name="canBeNegative">Whether this value can be negative</param>
        public Unit(string name, string symbol, Quantity quantity, bool generatePrefixes, ConversionFunction toBaseUnit = null, ConversionFunction fromBaseUnit = null, bool canBeNegative = false, PronumeralCollection unitAsBaseUnits = null): base()
        {
            Name = name;
            Symbol = symbol;
            ToBaseUnit = toBaseUnit;
            FromBaseUnit = fromBaseUnit;
            Quantity = quantity;
            CanBeNegative = canBeNegative;
            if (toBaseUnit is null)
            {
                ToBaseUnit = value => value;
            }
            if (fromBaseUnit is null)
            {
                FromBaseUnit = value => value;
            }
            if(generatePrefixes)
            {
                foreach (KeyValuePair<int, (string,string)> prefix in UnitManager.Prefixes)
                {
                    _ = new Unit(prefix.Value.Item1 + name.ToLower(), prefix.Value.Item2 + symbol, quantity, false, canBeNegative: canBeNegative)
                    {
                        ToBaseUnit = value => value * Math.Pow(10, prefix.Key),
                        FromBaseUnit = value => value / Math.Pow(10, prefix.Key),
                        Quantity = quantity
                    };
                }
            }
            UnitAsBaseUnits = unitAsBaseUnits;
            UnitManager.Units.Add(name, this);
        }
        #endregion
        /// <summary>
        /// Gets all the currently available number units
        /// </summary>
        /// <returns></returns>
        public static List<Unit> GetNumberUnits()
        {
            List<Unit> units = new();
            foreach (KeyValuePair<string, Unit> unit in UnitManager.Units)
            {
                units.Add(unit.Value);
            }
            return units;
        }
        /// <summary>
        /// Query for a numberUnit by its full name
        /// <br/> Use UnitManager.Unit["XXXXXX"] if possible
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static Unit? GetNumberUnitByFullName(string fullName)
        {
            foreach (Unit numberUnit in GetNumberUnits())
            {
                if (fullName.Equals(numberUnit.Name))
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
        public static Unit? GetNumberUnitBySuffix(string suffix)
        {
            foreach (Unit numberUnit in GetNumberUnits())
            {
                if (suffix.Equals(numberUnit.Symbol))
                {
                    return numberUnit;
                }
            }
            return null;
        }
    }

    public sealed class UnitManager
    {
        private static readonly Lazy<UnitManager> instance = new Lazy<UnitManager>(() => new UnitManager());
        public static UnitManager Unit => instance.Value;

        public static Dictionary<string, Unit> Units = new Dictionary<string, Unit>();
        
        
        /// <summary>
        /// Prefixes added to units to produce multiples and submultiples of the original unit
        /// The key is the exponent eg. 3 for kilo or -3 for milli, and the value is the name and symbol
        /// </summary>
        public static Dictionary<int, (string, string)> Prefixes = [];

        public Unit this[string name]
        {
            get
            {
                Units.TryGetValue(name, out Unit returnedUnit);
                return returnedUnit;
            }
        }

        static UnitManager()
        {
            #region Prefixes
            Dictionary<int, (string,string)> prefixes = UnitManager.Prefixes;
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

            _ = new Unit("Plain", "", Quantity.Plain, false, value => value, value => value, canBeNegative: true);

            #region Base Units
            _ = new Unit("Second", "s", Quantity.Time, generatePrefixes: true);
            _ = new Unit("Metre", "m", Quantity.Length, generatePrefixes: true);
            _ = new Unit("Gram", "g", Quantity.Mass, generatePrefixes: true);
            _ = new Unit("Ampere", "A", Quantity.ElectricCurrent, generatePrefixes: true);
            _ = new Unit("Kelvin", "K", Quantity.Temperature, generatePrefixes: false, canBeNegative: false);
            _ = new Unit("Mole", "mol", Quantity.AmountOfSubstance, generatePrefixes: true);
            _ = new Unit("Candela", "cd", Quantity.LuminousIntensity, generatePrefixes: true);
            #endregion
            #region Derived Units
            //https://en.wikipedia.org/wiki/International_System_of_Units#Derived_units
            _ = new Unit("Radian", "rad", Quantity.Angle, false, Radians => Radians, Radians => Radians, canBeNegative: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m")!, 1),
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m")!, -1),
            ]);
            _ = new Unit("Steradian", "sr", Quantity.SolidAngle, false, Steradians => Steradians, Steradians => Steradians, canBeNegative: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m")!, 2),
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m")!, -2),
            ]);
            
            _ = new Unit("Hertz", "hz", Quantity.Frequency, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), -1)!
            ]);
            _ = new Unit("Newton", "N", Quantity.Force, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "kg"), 1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), 1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), -2)!
            ]);
            _ = new Unit("Pascal", "Pa", Quantity.Pressure, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "N"), 1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), -2)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), -2)!
            ]);
            _ = new Unit("Joule", "J", Quantity.Energy, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "N"), 1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), 2)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), -2)!
            ]);
            _ = new Unit("Watt", "W", Quantity.Power, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "Kg"), 1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), 2)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), -3)!,
            ]);
            _ = new Unit("Coulomb", "C", Quantity.ElectricCharge, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), 1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "A"), 1)!,
            ]);
            _ = new Unit("Volt", "V", Quantity.Voltage, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "Kg"), 1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), 2)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), 3)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "A"), -1)!,
            ]);
            _ = new Unit("Farad", "F", Quantity.Capacitance, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "Kg"), -1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), -2)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), 4)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "A"), 2)!,
            ]);
            _ = new Unit("Ohm", "Ω", Quantity.Resistance, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "Kg"), 1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), 2)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), -3)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "A"), 2)!,
            ]);
            _ = new Unit("Siemens", "S", Quantity.Resistance, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "Kg"), -1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), -2)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), 3)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "A"), 2)!,
            ]);
            _ = new Unit("Weber", "Wb", Quantity.MagneticFlux, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "Kg"), 1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), 2)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), -2)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "A"), -1)!,
            ]);
            _ = new Unit("Tesla", "T", Quantity.MagneticFieldStrength, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "Kg"), 1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), -2)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "A"), -1)!,
            ]);
            _ = new Unit("Henry", "H", Quantity.Inductance, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "Kg"), 1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), 2)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), -2)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "A"), -2)!,
            ]);
            _ = new Unit("Celsius", "°C", Quantity.Temperature, false, Kelvin => Kelvin + 273.15, Kelvin => Kelvin - 273.15, canBeNegative: false, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "K"), 1)!
            ]);
            _ = new Unit("Lumen", "lm", Quantity.Illuminance, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "cd"), 1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), 2)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), -2)!,
            ]);
            _ = new Unit("Lux", "lx", Quantity.Illuminance, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "cd"), 1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), 2)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), -4)!,
            ]);
            _ = new Unit("Becquerel", "Bq", Quantity.Radioactivity, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), -1)!
            ]);
            _ = new Unit("Gray", "Gy", Quantity.AbsorbedDose, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), 2)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), -2)!,
            ]);
            _ = new Unit("Katal", "kat", Quantity.CatalyticActivity, generatePrefixes: true, unitAsBaseUnits: [
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "mol"), 1)!,
                (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), -1)!,
            ]);
            #endregion
            #region Non-SI Units
            _ = new Unit("Litre", "L", Quantity.Volume, generatePrefixes: true);
            _ = new Unit("Degree", "°", Quantity.Angle, false, Radians => Radians * (Math.PI / 180), Radians => Radians * (180 / Math.PI), canBeNegative: true);
            #endregion
        }
    }
}
