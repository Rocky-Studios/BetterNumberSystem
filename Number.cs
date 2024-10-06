using System.Globalization;

namespace BetterNumberSystem;

/// <summary>
///     Represents a number.
/// </summary>
public class Number : IValue
{
    /// <summary>
    ///     The value of the number.
    /// </summary>
    public double Value;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Number" /> class.
    /// </summary>
    public Number()
    {
        Value = 0;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Number" /> class.
    /// </summary>
    /// <param name="value"> </param>
    public Number(double value)
    {
        Value = value;
    }

    /// <summary>
    ///     Converts a number to a term.
    /// </summary>
    /// <param name="num"> </param>
    /// <returns> </returns>
    public static implicit operator Term(Number num)
    {
        return new Term(num, new PronumeralCollection());
    }

    /// <summary>
    ///     Converts a number to a string.
    /// </summary>
    /// <returns> </returns>
    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///     Converts a number to a double.
    /// </summary>
    /// <param name="num"> </param>
    /// <returns> </returns>
    public static implicit operator Number(double num)
    {
        return new Number(num);
    }
}