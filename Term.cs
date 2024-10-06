namespace BetterNumberSystem;

/// <summary>
///     Value with a coefficient and pronumerals.
/// </summary>
public class Term
{
    /// <summary>
    ///     The coefficient of the term.
    /// </summary>
    public IValue Coefficient;

    /// <summary>
    ///     The pronumerals of the term.
    /// </summary>
    public PronumeralCollection Pronumerals;

    /// <summary>
    ///     The unit of the term.
    /// </summary>
    public Unit? Unit
    {
        get
        {
            try {
                return Pronumerals.First(p => p.Item1 is Unit).Item1 as Unit;
            }
            catch (Exception _) {return null;}
        }
    }

    /// <summary>
    ///     The prefix of the unit of the term.
    /// </summary>
    public Prefix? Prefix
    {
        get
        {
            Prefix? p;
            try {
                p = Pronumerals.First(pr => pr.Item1 is Prefix).Item1 as Prefix;
            }
            catch {
                p = null;
            }

            return p;
        }
    }
    
    /// <summary>
    ///     The prefix of the unit of the term.
    /// </summary>
    public Quantity Quantity    
    {
        get
        {
            if (Unit is null) return Quantity.Plain;
            return Unit.Quantity;
        }
    }

    /// <summary>
    ///     Initialises a term.
    /// </summary>
    /// <param name="coefficient"> </param>
    /// <param name="pronumerals"> </param>
    public Term(IValue coefficient, PronumeralCollection pronumerals)
    {
        Coefficient = coefficient;
        Pronumerals = pronumerals;
    }

    /// <summary>
    ///     Initialises a term.
    /// </summary>
    /// <param name="coefficient"> </param>
    /// <param name="pronumeral"> </param>
    public Term(IValue coefficient, Pronumeral pronumeral)
    {
        Coefficient = coefficient;
        Pronumerals = pronumeral;
    }

    /// <summary>
    ///     Initialises a term.
    /// </summary>
    /// <param name="coefficient"> </param>
    /// <param name="pronumerals"> </param>
    public Term(IValue coefficient, string[] pronumerals)
    {
        Coefficient = coefficient;
        Pronumerals = new PronumeralCollection();
        foreach (string pronumeral in pronumerals)
            Pronumerals.Add((PronumeralManager.FindPronumeralByName<Pronumeral>(pronumeral), 1));
    }

    /// <summary>
    ///     Initialises a term.
    /// </summary>
    /// <param name="coefficient"> </param>
    public Term(IValue coefficient)
    {
        Coefficient = coefficient;
        Pronumerals = [];
    }

    /// <summary>
    ///     Converts the term to a new prefix.
    /// </summary>
    /// <param name="newPrefix"> </param>
    /// <returns> </returns>
    /// <exception cref="Exception"> </exception>
    public Term Convert(Prefix newPrefix)
    {
        if (Coefficient is not Number) throw new Exception("Cannot convert a term that is not a number");
        if (Unit is null) throw new Exception("Cannot convert a term that doesn't have a unit in it");
        double convertedValue;
        if (Prefix is null) convertedValue = (Coefficient as Number)!.Value / newPrefix.Multiplier;
        else convertedValue = (Coefficient as Number)!.Value / newPrefix.Multiplier * Prefix.Multiplier;
        return new Term(new Number(convertedValue), [(Unit, 1), (newPrefix, 1)]);
    }

    /// <summary>
    ///     Converts the term to a new prefix.
    /// </summary>
    /// <param name="newPrefix"> </param>
    /// <returns> </returns>
    public Term Convert(string newPrefix)
    {
        return Convert((PronumeralManager.FindPronumeralByName<Prefix>(newPrefix) as Prefix)!);
    }

    /// <summary>
    ///     Converts the term to a new unit.
    /// </summary>
    /// <param name="newUnit"> </param>
    /// <returns> </returns>
    /// <exception cref="Exception"> </exception>
    public Term ConvertUnit(Unit newUnit)
    {
        if (Coefficient is not Number) throw new Exception("Cannot convert a term that is not a number.");
        if (Unit is null) throw new Exception("Cannot convert a term that doesn't have a unit in it.");
        if (Unit.Quantity != newUnit.Quantity)
            throw new Exception("Cannot convert between " + Unit.Quantity + " and " + newUnit.Quantity +
                                ". Did you intend to use a math operation?");
        double value = (Coefficient as Number)!.Value;
        if (Unit == newUnit) return this;
        switch (Unit.Quantity) {
            case Quantity.Temperature:
                if (Unit.Symbol == "K" && newUnit.Symbol == "C")
                    value -= 273.15;
                else if (Unit.Symbol == "°C" && newUnit.Symbol == "K")
                    value += 273.15;
                else
                    throw new Exception("Unsupported temperature conversion");

                break;
            case Quantity.Angle:
                if (Unit.Symbol == "rad" && newUnit.Symbol == "°")
                    value *= 180 / Math.PI;
                else if (Unit.Symbol == "°" && newUnit.Symbol == "rad")
                    value *= Math.PI / 180;
                else
                    throw new Exception("Unsupported angle conversion");
                break;
        }

        return new Term(new Number(value), [(newUnit, 1)]);
    }

    /// <summary>
    ///     Converts the term to text.
    /// </summary>
    /// <returns> </returns>
    public override string ToString()
    {
        string output = "";
        if (Coefficient is Number n)
            if (Math.Abs(n.Value - 1) > 0.0000000001) {
                output += Coefficient.ToString();
                if(Pronumerals.Count > 0) 
                    output += " ";
            }

        PronumeralCollection negativePronumerals = [];
        PronumeralCollection positivePronumerals = [];
        
        foreach ((Pronumeral, int) pronumeral in Pronumerals)
            if (pronumeral.Item2 < 0) negativePronumerals.Add(pronumeral);
            else if (pronumeral.Item2 > 0) positivePronumerals.Add(pronumeral);
    
        foreach ((Pronumeral, int) p in positivePronumerals) {
            output += p.Item1.Symbol;
            if (p.Item2 != 1) output += "^" + p.Item2;
        }

        if (negativePronumerals.Count > 0) output += "/";
        foreach ((Pronumeral, int) p in negativePronumerals) {
            output += p.Item1.Symbol;
            if (p.Item2 != -1) output += "^" + p.Item2 * -1;
        }

        return output;
    }

    public static bool MatchingPrefix(Term a, Term b)
    {
        return a.Prefix.Name == b.Prefix.Name;
    }

    public static string ToString(Term[] terms) {
        string output = terms.Aggregate("", (current, term) => current + (term + ", "));
        return output[..^2];
    }
    
    public static Term[] operator +(Term a, Term b) => Function.Sum.Process([a, b]);
    public static Term[] operator -(Term a, Term b) => Function.Difference.Process([a, b]);
    public static Term[] operator *(Term a, Term b) => Function.Product.Process([a, b]);
}

/// <summary>
///     A value that can be used in a term.
/// </summary>
public interface IValue { }