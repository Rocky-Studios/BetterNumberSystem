namespace BetterNumberSystem.Expression;

/// <summary>
/// A collection of pronumerals and their exponents
/// </summary>
public class PronumeralCollection : List<(Pronumeral, int)>
{
    public override string ToString()
    {
        return string.Join("", this.Select(pronumeral => pronumeral.Item1 + (pronumeral.Item2 > 1 ? pronumeral.Item2.ToString() : "")));
    }
}