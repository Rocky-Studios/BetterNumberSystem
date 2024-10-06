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

    /// <summary>
    ///     Initialises a pronumeral
    /// </summary>
    /// <param name="name"> </param>
    /// <param name="symbol"> </param>
    /// <param name="value"> </param>
    protected Pronumeral(string name, string symbol, Term value)
    {
        Name = name;
        Symbol = symbol;
        Value = value;
    }

    /// <summary>
    ///     Converts the pronumeral to a string
    /// </summary>
    /// <returns> </returns>
    public override string ToString()
    {
        return $"{Value}";
    }

    /// <summary>
    ///     Implicitly converts a pronumeral to a pronumeral collection
    /// </summary>
    /// <param name="pronumeral"> </param>
    /// <returns> </returns>
    public static implicit operator PronumeralCollection(Pronumeral pronumeral)
    {
        return [(pronumeral, 1)];
    }

    /// <summary>
    ///     Adds an exponent of 1 to the pronumeral
    /// </summary>
    /// <param name="pronumeral"> </param>
    /// <returns> </returns>
    public static implicit operator (Pronumeral, int)(Pronumeral pronumeral)
    {
        return (pronumeral, 1);
    }

    /// <summary>
    ///     Finds a pronumeral by its name
    /// </summary>
    /// <param name="name"> </param>
    /// <returns> </returns>
    public static implicit operator Pronumeral(string name)
    {
        return PronumeralManager.FindPronumeralByName<Pronumeral>(name);
    }
}

/// <summary>
///     Manages all the units and prefixes
/// </summary>
public sealed class PronumeralManager
{
    private static readonly Lazy<PronumeralManager> Instance = new(() => new PronumeralManager());

    /// <summary>
    ///     The singleton instance of the UnitManager
    /// </summary>
    public static PronumeralManager PronumeralM => Instance.Value;

    /// <summary>
    ///     A dictionary of all the units
    /// </summary>
    public static Dictionary<string, Pronumeral> Pronumerals = new();


    /// <summary>
    ///     Gets a pronumeral by its symbol
    /// </summary>
    /// <param name="symbol"> </param>
    public static Pronumeral FindPronumeralBySymbol<T>(string symbol) where T : Pronumeral
    {
        return Pronumerals[symbol] as T ?? throw new Exception("Pronumeral not found");
    }

    /// <summary>
    ///     Gets a pronumeral by its name
    /// </summary>
    /// <param name="name"> </param>
    public static Pronumeral FindPronumeralByName<T>(string name) where T : Pronumeral
    {
        return Pronumerals.First(p => p.Value.Name.ToLower() == name.ToLower()).Value as T ??
               throw new Exception("Pronumeral not found");
    }

    static PronumeralManager()
    {
        _ = new Unit("Plain", "", Quantity.Plain, []);

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

        _ = new Unit("Second", "s", Quantity.Time, null!);
        _ = new Unit("Metre", "m", Quantity.Length, null!);
        _ = new Unit("Gram", "g", Quantity.Mass, null!);
        _ = new Unit("Ampere", "A", Quantity.ElectricCurrent, null!);
        _ = new Unit("Kelvin", "K", Quantity.Temperature, null!);
        _ = new Unit("Mole", "mol", Quantity.AmountOfSubstance, null!);
        _ = new Unit("Candela", "cd", Quantity.LuminousIntensity, null!);

        #endregion

        _ = new Unit("Celsius", "°C", Quantity.Temperature, [("Kelvin", 1)]);
        _ = new Unit("Litre", "L", Quantity.Volume, [("Metre", 3)]);

        //#region Derived Units

        //https://en.wikipedia.org/wiki/International_System_of_Units#Derived_units
        _ = new Unit("Radian", "rad", Quantity.Angle,
        [
            (Pronumerals["m"], 1),
            (Pronumerals["m"], -1)
        ]);
        _ = new Unit("Degree", "°", Quantity.Angle, [("Radian", 1)]);
        _ = new Unit("Steradian", "sr", Quantity.SolidAngle,
        [
            (Pronumerals["m"], 2),
            (Pronumerals["m"], -2)
        ]);
        _ = new Unit("Hertz", "Hz", Quantity.Frequency,
        [
            (Pronumerals["s"], -1)
        ]);
        _ = new Unit("Newton", "N", Quantity.Force,
        [
            Pronumerals["kilo"],
            Pronumerals["g"],
            (Pronumerals["m"], 1),
            (Pronumerals["s"], -2)
        ]);
        _ = new Unit("Pascal", "Pa", Quantity.Pressure,
        [
            Pronumerals["kilo"],
            Pronumerals["g"],
            (Pronumerals["m"], -1),
            (Pronumerals["s"], -2)
        ]);
        _ = new Unit("Joule", "J", Quantity.Energy,
        [
            Pronumerals["kilo"],
            Pronumerals["g"],
            (Pronumerals["m"], 2),
            (Pronumerals["s"], -2)
        ]);
        _ = new Unit("Watt", "W", Quantity.Power,
        [
            Pronumerals["kilo"],
            Pronumerals["g"],
            (Pronumerals["m"], 2),
            (Pronumerals["s"], -3)
        ]);
        _ = new Unit("Coulomb", "C", Quantity.ElectricCharge,
        [
            Pronumerals["s"],
            Pronumerals["A"]
        ]);
        _ = new Unit("Volt", "V", Quantity.Voltage,
        [
            Pronumerals["kilo"],
            Pronumerals["g"],
            (Pronumerals["m"], 2),
            (Pronumerals["s"], -3),
            (Pronumerals["A"], -1)
        ]);
        _ = new Unit("Farad", "F", Quantity.ElectricCapacitance,
        [
            (Pronumerals["kilo"], -1),
            (Pronumerals["g"], -1),
            (Pronumerals["m"], -2),
            (Pronumerals["s"], 4),
            (Pronumerals["A"], 2)
        ]);
        _ = new Unit("Ohm", "Ω", Quantity.ElectricResistance,
        [
            Pronumerals["kilo"],
            Pronumerals["g"],
            (Pronumerals["m"], 2),
            (Pronumerals["s"], -3),
            (Pronumerals["A"], -2)
        ]);
        _ = new Unit("Siemens", "S", Quantity.ElectricConductance,
        [
            (Pronumerals["kilo"], -1),
            (Pronumerals["g"], -1),
            (Pronumerals["m"], -2),
            (Pronumerals["s"], 3),
            (Pronumerals["A"], 2)
        ]);
        _ = new Unit("Weber", "Wb", Quantity.MagneticFlux,
        [
            Pronumerals["kilo"],
            Pronumerals["g"],
            (Pronumerals["m"], 2),
            (Pronumerals["s"], -2),
            (Pronumerals["A"], -1)
        ]);
        _ = new Unit("Tesla", "T", Quantity.MagneticFluxDensity,
        [
            Pronumerals["kilo"],
            (Pronumerals["g"], 1),
            (Pronumerals["s"], -2),
            (Pronumerals["A"], -1)
        ]);
        _ = new Unit("Henry", "H", Quantity.MagneticInductance,
        [
            Pronumerals["kilo"],
            Pronumerals["g"],
            (Pronumerals["m"], 2),
            (Pronumerals["s"], -2),
            (Pronumerals["A"], -2)
        ]);
        _ = new Unit("Lumen", "lm", Quantity.LuminousFlux,
        [
            Pronumerals["cd"],
            (Pronumerals["m"], 2),
            (Pronumerals["m"], -2)
        ]);
        _ = new Unit("Lux", "lx", Quantity.Illuminance,
        [
            Pronumerals["cd"],
            (Pronumerals["m"], 2),
            (Pronumerals["m"], -4)
        ]);
        _ = new Unit("Becquerel", "Bq", Quantity.NuclearRadioactivity,
        [
            (Pronumerals["s"], -1)
        ]);
        _ = new Unit("Gray", "Gy", Quantity.AbsorbedDose,
        [
            (Pronumerals["m"], 2),
            (Pronumerals["s"], -2)
        ]);
        _ = new Unit("Sievert", "Sv", Quantity.EquivalentDose,
        [
            (Pronumerals["m"], 2),
            (Pronumerals["s"], -2)
        ]);
        _ = new Unit("Katal", "kat", Quantity.CatalyticActivity,
        [
            (Pronumerals["mol"], 1),
            (Pronumerals["s"], -1)
        ]);

        /*
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
    /// <summary>
    ///     Determines whether two PronumeralCollection objects are equal.
    /// </summary>
    /// <param name="x"> </param>
    /// <param name="y"> </param>
    /// <returns> </returns>
    public bool Equals(PronumeralCollection? x, PronumeralCollection? y)
    {
        if (x == null || y == null)
            return x == y;

        return x.Count == y.Count && !x.Except(y).Any();
    }

    /// <summary>
    ///     Returns a hash code for the specified PronumeralCollection.
    /// </summary>
    /// <param name="obj"> </param>
    /// <returns> </returns>
    public int GetHashCode(PronumeralCollection obj)
    {
        // Use a combination of pronumeral hashes to generate a hash code for the list
        unchecked {
            return obj.Aggregate(0, (current, pronumeral) => (current * 397) ^ pronumeral.Item1.GetHashCode());
        }
    }
}

/// <summary>
///     A pronumeral that has its value explicitly marked as variable.
/// </summary>
public class Variable : Pronumeral
{
    /// <summary>
    ///     Initialises a variable
    /// </summary>
    /// <param name="name"> </param>
    /// <param name="symbol"> </param>
    /// <param name="value"> </param>
    public Variable(string name, string symbol, Term value) : base(name, symbol, value) { }

    /// <summary>
    ///     Converts the variable to a string
    /// </summary>
    /// <returns> </returns>
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
    /// <summary>
    ///     Initialises a constant
    /// </summary>
    /// <param name="name"> </param>
    /// <param name="symbol"> </param>
    /// <param name="value"> </param>
    public Constant(string name, string symbol, Term value) : base(name, symbol, value) { }

    /// <summary>
    ///     Converts the constant to a string
    /// </summary>
    /// <returns> </returns>
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
    /// <summary>
    ///     The hyperfine transition frequency of caesium-133.
    /// </summary>
    public static readonly Constant HyperfineTransitionFrequencyCs133 = new("Hyperfine transition frequency of caesium",
        "ΔνCs", new Term(new Number(9192631770), "Hertz"));

    /// <summary>
    ///     The speed of light in vacuum.
    /// </summary>
    public static readonly Constant SpeedOfLight = new("Speed of light", "c",
        new Term(new Number(299792458), [("Metre", 1), ("Second", -1)]));

    /// <summary>
    ///     A photon's energy is equal to its frequency multiplied by the Planck constant.
    /// </summary>
    public static readonly Constant PlanckConstant = new("Planck constant", "h",
        new Term(new Number(6.62607015e-34), [("Joule", 1), ("Second", 1)]));

    /// <summary>
    ///     The charge of a single electron.
    /// </summary>
    public static readonly Constant ElementaryCharge =
        new("Elementary charge", "e", new Term(new Number(1.602176634e-19), "Coulomb"));

    /// <summary>
    ///     The average relative thermal energy of particles in a gas in relation to the thermodynamic temperature of the gas.
    /// </summary>
    public static readonly Constant BoltzmannConstant = new("Boltzmann constant", "k",
        new Term(new Number(1.380649e-23), [("Joule", 1), ("Kelvin", -1)]));

    /// <summary>
    ///     The number of constituent particles (usually molecules, atoms, ions, or ion pairs) per mole.
    /// </summary>
    public static readonly Constant AvogadroConstant =
        new("Avogadro constant", "N_A", new Term(new Number(6.02214076e23), [("Mole", -1)]));

    /// <summary>
    ///     The ratio of luminous flux to power in a light source.
    /// </summary>
    public static readonly Constant LuminousEfficacy =
        new("Luminous efficacy", "Kcd", new Term(new Number(683), [("Lumen", 1), ("Watt", -1)]));

    #endregion
}