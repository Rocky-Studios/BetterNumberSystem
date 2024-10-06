namespace BetterNumberSystem;

/// <summary>
///     A collection of pronumerals and their exponents
/// </summary>
public class PronumeralCollection : List<(Pronumeral, int)>
{
    /// <summary>
    ///     Converts the collection of pronumerals and their exponents to a string representation.
    /// </summary>
    /// <returns> </returns>
    public override string ToString()
    {
        return string.Join("",
            this.Select(pronumeral => pronumeral.Item1 + (pronumeral.Item2 > 1 ? pronumeral.Item2.ToString() : "")));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PronumeralCollection" /> class.
    /// </summary>
    public PronumeralCollection() { }
}