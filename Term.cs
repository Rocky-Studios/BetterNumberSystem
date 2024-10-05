namespace BetterNumberSystem;

public class Term
{
    public IValue Value;
    public PronumeralCollection Pronumerals;

    public Unit? Unit
    {
        get { return Pronumerals.First(p => p.Item1 is Unit).Item1 as Unit; }
    }
    
    public Prefix? Prefix
    {
        get
        {
            Prefix? p;
            try {
                p = Pronumerals.First(p => p.Item1 is Prefix).Item1 as Prefix;
            } catch {
                p = null;
            }
            return p;
        }
    }

    public Term(IValue value, PronumeralCollection pronumerals)
    {
        Value = value;
        Pronumerals = pronumerals;
    }
    
    public Term(IValue value, Pronumeral pronumeral)
    {
        Value = value;
        Pronumerals = pronumeral;
    }
    
    public Term(IValue value, string[] pronumerals)
    {
        Value = value;
        Pronumerals = new PronumeralCollection();
        foreach (var pronumeral in pronumerals)
        {
            Pronumerals.Add((PronumeralManager.FindPronumeralByName<Pronumeral>(pronumeral), 1));
        }
    }
    
    public Term(IValue value)
    {
        Value = value;
        Pronumerals = [];
    }
    
    public Term Convert(Prefix newPrefix)
    {
        if(Value is not Number) throw new Exception("Cannot convert a term that is not a number");
        if (Unit is null) throw new Exception("Cannot convert a term that doesn't have a unit in it");
        double convertedValue;
        if(Prefix is null) convertedValue = (Value as Number).Value / newPrefix.Multiplier;
        else convertedValue = (Value as Number).Value / newPrefix.Multiplier * Prefix.Multiplier;
        return new Term(new Number(convertedValue), [(Unit, 1), (newPrefix, 1)]);
    }
    
    public Term Convert(string newPrefix)
    {
        return Convert(PronumeralManager.FindPronumeralByName<Prefix>(newPrefix) as Prefix);
    }
    
    
    public Term ConvertUnit(Unit newUnit)
    {
        if (Value is not Number) throw new Exception("Cannot convert a term that is not a number.");
        if (Unit is null) throw new Exception("Cannot convert a term that doesn't have a unit in it.");
        if (Unit.Quantity != newUnit.Quantity) throw new Exception("Cannot convert between " + Unit.Quantity +" and " + newUnit.Quantity +". Did you intend to use a math operation?");
        double value = (Value as Number)!.Value;
        if(Unit == newUnit) return this;
        switch (Unit.Quantity) {
            case Quantity.Temperature:
                if (Unit.Symbol == "K" && newUnit.Symbol == "C") {
                    value -= 273.15;
                }
                else if (Unit.Symbol == "°C" && newUnit.Symbol == "K") {
                    value += 273.15;
                }
                else {
                    throw new Exception("Unsupported temperature conversion");
                }

                break;
        }
        return new Term(new Number(value), [(newUnit, 1)]);
    }
    
    public override string ToString()
    {
        string output = "";
        if(Value is Number n)
        if (n.Value != 1) {
            output += Value.ToString();
            output += " ";
        }

        PronumeralCollection negativePronumerals = [];
        PronumeralCollection positivePronumerals = [];
        
        foreach (var pronumeral in Pronumerals)
        {
            if (pronumeral.Item2 < 0) negativePronumerals.Add(pronumeral);
            else if(pronumeral.Item2 == 0) continue;
            else positivePronumerals.Add(pronumeral);
        }

        foreach (var p in positivePronumerals) {
            output += p.Item1.Symbol;
            if (p.Item2 != 1) output += "^" + p.Item2;
        }
        if(negativePronumerals.Count > 0)output += "/";
        foreach (var p in negativePronumerals) {
            output += p.Item1.Symbol;
            if (p.Item2 != -1) output += "^" + (p.Item2 * -1);
        }

        return output;
    }
}

public interface IValue
{
    
}