namespace BetterNumberSystem;

/// <summary>
///     A prefix for a unit of measurement in the SI system
/// </summary>
public class Prefix : Constant
{
    /// <summary>
    ///     Initialises a prefix
    /// </summary>
    /// <param name="name"> </param>
    /// <param name="symbol"> </param>
    /// <param name="value"> </param>
    public Prefix(string name, string symbol, Term value) : base(name, symbol, value) { }
    
    /// <summary>
    ///     The multiplier for this prefix
    /// </summary>
    public double Multiplier
    {
        get { return (double) (Value.Value as Number)!.Value; }
    }

}