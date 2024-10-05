using System.Data.SqlTypes;

namespace BetterNumberSystem;

/// <summary>
///     Represents a number.
/// </summary>
public class Number : IValue
{
    public double Value;
    
    public Number()
    {
        Value = 0;
    }
    
    public Number(double value)
    {
        Value = value;
    }

    public static implicit operator Term(Number num)
    {
        return new Term(num, new PronumeralCollection());
    }
    
    public override string ToString()
    {
        return Value.ToString();
    } 
    
    public static implicit operator Number(double num)
    {
        return new Number(num);
    }
}