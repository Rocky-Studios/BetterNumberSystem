namespace BetterNumberSystem;

/// <summary>
///     A value represented by a symbol
/// </summary>
public abstract class Pronumeral
{
    /// <summary>
    ///     Gets or sets the name of the pronumeral.
    /// </summary>
    public string Name;

    /// <summary>
    ///     Gets or sets the symbol of the pronumeral.
    /// </summary>
    public string Symbol;

    /// <summary>
    ///     Gets or sets the value of the pronumeral.
    /// </summary>
    public Term Value;

    protected Pronumeral(string name, string symbol, Term value)
    {
        Name = name;
        Symbol = symbol;
        Value = value;
    }

    public override string ToString()
    {
        return $"{Value}";
    }
    /// <summary>
    ///   Implicitly converts a pronumeral to a pronumeral collection
    /// </summary>
    /// <param name="pronumeral"></param>
    /// <returns></returns>
    public static implicit operator PronumeralCollection (Pronumeral pronumeral)
    {
        return [(pronumeral, 1)];
    }
    /// <summary>
    ///  Adds an exponent of 1 to the pronumeral
    /// </summary>
    /// <param name="pronumeral"></param>
    /// <returns></returns>
    public static implicit operator (Pronumeral, int) (Pronumeral pronumeral)
    {
        return (pronumeral, 1);
    }
    /// <summary>
    ///  Finds a pronumeral by its name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static implicit operator Pronumeral (string name)
    {
        return PronumeralManager.FindPronumeralByName<Pronumeral>(name);
    }
}

/// <summary>
///   Manages all the units and prefixes
/// </summary>
public sealed class PronumeralManager
{
    private static readonly Lazy<PronumeralManager> Instance = new(() => new PronumeralManager());
    /// <summary>
    ///   The singleton instance of the UnitManager
    /// </summary>
    public static PronumeralManager PronumeralM => Instance.Value;
    /// <summary>
    ///   A dictionary of all the units
    /// </summary>
    public static Dictionary<string, Pronumeral> Pronumerals = new();



    /// <summary>
    ///   Gets a pronumeral by its symbol
    /// </summary>
    /// <param name="symbol"></param>
    public static Pronumeral FindPronumeralBySymbol<T>(string symbol) where T : Pronumeral
    {
        return Pronumerals[symbol] as T ?? throw new Exception("Pronumeral not found");
    }
    
    /// <summary>
    ///   Gets a pronumeral by its name
    /// </summary>
    /// <param name="name"></param>
    public static Pronumeral FindPronumeralByName<T>(string name) where T : Pronumeral
    {
        return Pronumerals.First(p => p.Value.Name.ToLower() == name.ToLower()).Value as T ?? throw new Exception("Pronumeral not found");
    }

    static PronumeralManager()
    {
        _ = new Unit("Plain", "", Quantity.Plain,[]);

        #region Prefixes
        Pronumerals["yotta"] = new Prefix("Yotta", "Y", new Number(1e24));
        Pronumerals["zetta"] = new Prefix("Zetta", "Z", new Number(1e21));
        Pronumerals["exa"] = new Prefix("Exa", "E", new Number(1e18));
        Pronumerals["peta"] = new Prefix("Peta", "P", new Number(1e15));
        Pronumerals["tera"] = new Prefix("Tera", "T", new Number(1e12));
        Pronumerals["giga"] = new Prefix("Giga", "G", new Number(1e9));
        Pronumerals["mega"] = new Prefix("Mega", "M", new Number(1e6));
        Pronumerals["kilo"] = new Prefix("Kilo", "k", new Number(1e3));
        Pronumerals["hecto"] = new Prefix("Hecto", "h", new Number(1e2));
        Pronumerals["deca"] = new Prefix("Deca", "da", new Number(1e1));
        Pronumerals["deci"] = new Prefix("Deci", "d", new Number(1e-1));
        Pronumerals["centi"] = new Prefix("Centi", "c", new Number(1e-2));
        Pronumerals["milli"] = new Prefix("Milli", "m", new Number(1e-3));
        Pronumerals["micro"] = new Prefix("Micro", "μ", new Number(1e-6));
        Pronumerals["nano"] = new Prefix("Nano", "n", new Number(1e-9));
        Pronumerals["pico"] = new Prefix("Pico", "p", new Number(1e-12));
        Pronumerals["femto"] = new Prefix("Femto", "f", new Number(1e-15));
        Pronumerals["atto"] = new Prefix("Atto", "a", new Number(1e-18));
        Pronumerals["zepto"] = new Prefix("Zepto", "z", new Number(1e-21));
        Pronumerals["yocto"] = new Prefix("Yocto", "y", new Number(1e-24));
        #endregion
        
        #region Base Units

        _ = new Unit("Second", "s", Quantity.Time);
        _ = new Unit("Metre", "m", Quantity.Length);
        _ = new Unit("Gram", "g", Quantity.Mass);
        _ = new Unit("Ampere", "A", Quantity.ElectricCurrent);
        _ = new Unit("Kelvin", "K", Quantity.Temperature);
        _ = new Unit("Mole", "mol", Quantity.AmountOfSubstance);
        _ = new Unit("Candela", "cd", Quantity.LuminousIntensity);

        #endregion

        _ = new Unit("Celsius", "°C", Quantity.Temperature);
        
        //#region Derived Units

        //https://en.wikipedia.org/wiki/International_System_of_Units#Derived_units
        _ = new Unit("Radian", "rad", Quantity.Angle, 
            unitAsBaseUnits:
            [
                (Pronumerals["s"], 1),
                (Pronumerals["m"], -1),
            ]);
        _ = new Unit("Steradian", "sr", Quantity.SolidAngle, 
            unitAsBaseUnits:
            [
                (Pronumerals["m"], 2),
                (Pronumerals["m"], -2),
            ]);
        _ = new Unit("Hertz", "Hz", Quantity.Frequency, 
            unitAsBaseUnits:
            [
                (Pronumerals["s"], -1),
            ]);
        _ = new Unit("Newton", "N", Quantity.Force, 
            unitAsBaseUnits:
            [
                Pronumerals["kilo"],
                (Pronumerals["g"], 1),
                (Pronumerals["m"], 1),
                (Pronumerals["s"], -2),
            ]);
        _ = new Unit("Pascal", "Pa", Quantity.Pressure, 
            unitAsBaseUnits:
            [
                (Pronumerals["N"], 1),
                (Pronumerals["m"], -2),
                (Pronumerals["s"], -2),
            ]);
        _ = new Unit("Joule", "J", Quantity.Energy, 
            unitAsBaseUnits:
            [
                Pronumerals["kilo"],
                (Pronumerals["g"], 1),
                (Pronumerals["m"], 2),
                (Pronumerals["s"], -2),
            ]);
        _ = new Unit("Watt", "W", Quantity.Power, 
            unitAsBaseUnits:
            [
                Pronumerals["kilo"],
                (Pronumerals["g"], 1),
                (Pronumerals["m"], 2),
                (Pronumerals["s"], -3),
            ]);
        _ = new Unit("Coulomb", "C", Quantity.ElectricCharge, 
            unitAsBaseUnits:
            [
                (Pronumerals["s"], 1),
                (Pronumerals["A"], 1),
            ]);
        _ = new Unit("Volt", "V", Quantity.Voltage, 
            unitAsBaseUnits:
            [
                Pronumerals["kilo"],
                (Pronumerals["g"], 1),
                (Pronumerals["m"], 2),
                (Pronumerals["s"], -3),
                (Pronumerals["A"], -1),
            ]);
        _ = new Unit("Farad", "F", Quantity.ElectricCapacitance, 
            unitAsBaseUnits:
            [
                (Pronumerals["kilo"], -1),
                (Pronumerals["g"], -1),
                (Pronumerals["m"], -2),
                (Pronumerals["s"], 4),
                (Pronumerals["A"], 2),
            ]);
        _ = new Unit("Ohm", "Ω", Quantity.ElectricResistance,  
            unitAsBaseUnits:
            [
                Pronumerals["kilo"],
                (Pronumerals["g"], 1),
                (Pronumerals["m"], 2),
                (Pronumerals["s"], -3),
                (Pronumerals["A"], 2),
            ]);
        _ = new Unit("Siemens", "S", Quantity.ElectricConductance, 
            unitAsBaseUnits:
            [
                Pronumerals["kilo"],
                (Pronumerals["g"], -1),
                (Pronumerals["m"], -2),
                (Pronumerals["s"], 3),
                (Pronumerals["A"], 2),
            ]);
        _ = new Unit("Weber", "Wb", Quantity.MagneticFlux, 
            unitAsBaseUnits:
            [
                Pronumerals["kilo"],
                (Pronumerals["g"], 1),
                (Pronumerals["m"], 2),
                (Pronumerals["s"], -2),
                (Pronumerals["A"], -1),
            ]);
        _ = new Unit("Tesla", "T", Quantity.MagneticFluxDensity, 
            unitAsBaseUnits:
            [
                Pronumerals["kilo"],
                (Pronumerals["s"], -2),
                (Pronumerals["A"], -1),
            ]);
        _ = new Unit("Henry", "H", Quantity.MagneticInductance, 
            unitAsBaseUnits:
            [
                Pronumerals["kilo"],
                (Pronumerals["g"], 1),
                (Pronumerals["m"], 2),
                (Pronumerals["s"], -2),
                (Pronumerals["A"], -2),
            ]);
        _ = new Unit("Lumen", "lm", Quantity.LuminousFlux, 
            unitAsBaseUnits:
            [
                (Pronumerals["cd"], 1),
                (Pronumerals["sr"], 1),
            ]);
        /*
                _ = new Unit("Lux", "lx", Quantity.Illuminance, autoGeneratePrefixes: true, unitAsBaseUnits:
                [
                    (Pronumeral.Pronumerals.Find(p => p.Symbol == "cd"), 1)!,
                    (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), 2)!,
                    (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), -4)!
                ]);
                _ = new Unit("Becquerel", "Bq", Quantity.NuclearRadioactivity, autoGeneratePrefixes: true, unitAsBaseUnits:
                [
                    (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), -1)!
                ]);
                _ = new Unit("Gray", "Gy", Quantity.AbsorbedDose, autoGeneratePrefixes: true, unitAsBaseUnits:
                [
                    (Pronumeral.Pronumerals.Find(p => p.Symbol == "m"), 2)!,
                    (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), -2)!
                ]);
                _ = new Unit("Katal", "kat", Quantity.CatalyticActivity, autoGeneratePrefixes: true, unitAsBaseUnits:
                [
                    (Pronumeral.Pronumerals.Find(p => p.Symbol == "mol"), 1)!,
                    (Pronumeral.Pronumerals.Find(p => p.Symbol == "s"), -1)!
                ]);

                #endregion

                #region Non-SI Units

                _ = new Unit("Litre", "L", Quantity.Volume, true);
                _ = new Unit("Degree", "°", Quantity.Angle, false, Radians => Radians * (Math.PI / 180),
                    Radians => Radians * (180 / Math.PI), true);

                #endregion
                */
    }
}

/// <summary>
///     Provides methods to compare two PronumeralCollection objects for equality.
/// </summary>
public class PronumeralListEqualityComparer : IEqualityComparer<PronumeralCollection>
{
    public bool Equals(PronumeralCollection x, PronumeralCollection y)
    {
        if (x == null || y == null)
            return x == y;

        return x.Count == y.Count && !x.Except(y).Any();
    }

    public int GetHashCode(PronumeralCollection obj)
    {
        // Use a combination of pronumeral hashes to generate a hash code for the list
        unchecked {
            return obj.Aggregate(0, (current, pronumeral) => (current * 397) ^ (pronumeral.Item1?.GetHashCode() ?? 0));
        }
    }
}

/// <summary>
///     A pronumeral that has its value explicitly marked as variable.
/// </summary>
public class Variable : Pronumeral
{
    public Variable(string name, string symbol, Term value) : base(name, symbol, value) { }

    public override string ToString()
    {
        return $"{Value}";
    }
}

/// <summary>
///     A pronumeral that has its value explicitly marked as constant.
/// </summary>
public class Constant : Pronumeral
{
    public Constant(string name, string symbol, Term value) : base(name, symbol, value) { }

    public override string ToString()
    {
        return $"{Value}";
    }

    #region Mathematical Constants

    /// <summary>
    ///     A circle's circumference divided by its diameter
    /// </summary>
    public static Constant Pi = new("pi", "π", new Number(MathF.PI));

    /// <summary>
    ///     The base of the natural logarithm
    /// </summary>
    public static Constant E = new("e", "e", new Number(Math.E));

    #endregion

    #region SI Defining Constants
    // https://en.wikipedia.org/wiki/International_System_of_Units#SI_defining_constants
    public static readonly Constant HyperfineTransitionFrequencyCs133 = new("Hyperfine transition frequency of caesium", "ΔνCs", new Term(new Number(9192631770), "Hertz"));
    public static readonly Constant SpeedOfLight = new("Speed of light", "c", new Term(new Number(299792458), [("Metre", 1), ("Second", -1)]));
    public static readonly Constant PlanckConstant = new("Planck constant", "h", new Term(new Number(6.62607015e-34), [("Joule", 1), ("Second", 1)]));
    public static readonly Constant ElementaryCharge = new("Elementary charge", "e", new Term(new Number(1.602176634e-19), "Coulomb"));
    public static readonly Constant BoltzmannConstant = new("Boltzmann constant", "k", new Term(new Number(1.380649e-23), [("Joule", 1), ("Kelvin", -1)]));
    public static readonly Constant AvogadroConstant = new("Avogadro constant", "N_A", new Term(new Number(6.02214076e23), [("Mole", -1)]));
    public static readonly Constant LuminousEfficacy = new("Luminous efficacy of 540 THz radiation", "Kcd", new Term(new Number(683), [("Lumen", 1), ("Watt", -1)]));
    #endregion
}