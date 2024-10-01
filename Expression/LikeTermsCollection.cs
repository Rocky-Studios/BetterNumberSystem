namespace BetterNumberSystem.Expression;

/// <summary>
///     Represents a collection of terms grouped by their pronumerals.
/// </summary>
public class LikeTermsCollection : Dictionary<List<Pronumeral>, List<ExpressionTerm>>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LikeTermsCollection" /> class
    ///     with a custom equality comparer for pronumeral lists.
    /// </summary>
    public LikeTermsCollection() : base(new Pronumeral.PronumeralListEqualityComparer()) { }

    /// <summary>
    ///     Converts this collection to an array of terms grouped by their pronumerals
    /// </summary>
    /// <returns> </returns>
    public override string ToString()
    {
        string output = "";
        foreach (KeyValuePair<List<Pronumeral>, List<ExpressionTerm>> likeTerms in this)
        foreach (ExpressionTerm term in likeTerms.Value) {
            output += term.Value;
            foreach (Pronumeral pronumeral in likeTerms.Key) output += pronumeral.Symbol;
            output += ", ";
        }

        return output[..^2];
    }
}