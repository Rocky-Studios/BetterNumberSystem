namespace BetterNumberSystem.Expression;

/// <summary>
///     Represents a collection of terms grouped by their pronumerals.
/// </summary>
public class LikeTermsCollection : Dictionary<List<Pronumeral>, List<Term>>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LikeTermsCollection" /> class
    ///     with a custom equality comparer for pronumeral lists.
    /// </summary>
    public LikeTermsCollection() { }

    /// <summary>
    ///     Converts this collection to a string
    /// </summary>
    /// <returns> </returns>
    public override string ToString()
    {
        string output = "";
        foreach (KeyValuePair<List<Pronumeral>, List<Term>> likeTerms in this)
        foreach (Term term in likeTerms.Value) {
            output += term.Coefficient;
            foreach (Pronumeral pronumeral in likeTerms.Key) output += pronumeral.Symbol;
            output += ", ";
        }

        return output[..^2];
    }
}