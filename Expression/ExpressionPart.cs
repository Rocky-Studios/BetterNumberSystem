namespace BetterNumberSystem.Expression;

/// <summary>
///     The base class for things that make up an expression.
/// </summary>
public abstract class ExpressionPart
{
    /// <summary>
    ///     Implicitly converts a number to a term.
    /// </summary>
    /// <param name="num"> The number to be converted </param>
    public static implicit operator ExpressionPart(Number num)
    {
        return new ExpressionTerm(num, [Pronumeral.NoPronumeral]);
    }
}

/// <summary>
///     The values on which mathematical operations occur in an algebraic expression.
/// </summary>
public class ExpressionTerm : ExpressionPart
{
    /// <summary>
    ///     The coefficient of the term.
    /// </summary>
    public IExpressionValue Value;

    /// <summary>
    ///     The variables or constants of the term.
    /// </summary>
    public List<Pronumeral> Pronumerals;

    /// <summary>
    ///     Shows the coefficient and pronumerals of the term as text.
    /// </summary>
    /// <returns> </returns>
    public override string ToString()
    {
        string pronumerals = "";
        foreach (Pronumeral pronumeral in Pronumerals) pronumerals += pronumeral.Symbol;
        return Value + pronumerals;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ExpressionTerm" /> class with the specified value and pronumerals.
    /// </summary>
    /// <param name="value"> The coefficient of the term. </param>
    /// <param name="pronumerals"> The variables or constants of the term. </param>
    public ExpressionTerm(IExpressionValue value, List<Pronumeral> pronumerals)
    {
        Value = value;
        Pronumerals = pronumerals;
    }
}

/// <summary>
///     A group of functions and terms.
/// </summary>
public class ExpressionGroup : ExpressionPart
{
    /// <summary>
    ///     The collection of functions and terms.
    /// </summary>
    public List<ExpressionPart> Parts;

    /// <summary>
    ///     Creates an empty <c> ExpressionGroup </c>.
    /// </summary>
    public ExpressionGroup()
    {
        Parts = [];
    }

    /// <summary>
    ///     Creates an <c> ExpressionGroup </c> with specified parts.
    /// </summary>
    /// <param name="parts"> The list of parts. </param>
    public ExpressionGroup(params ExpressionPart[] parts)
    {
        Parts = [.. parts];
    }

    /// <summary>
    ///     Converts the <c> ExpressionGroup </c> to text by converting each part to text.
    /// </summary>
    /// <returns> </returns>
    public override string ToString()
    {
        string output = "";
        foreach (ExpressionPart part in Parts) output += part + " ";
        return output;
    }

    /// <summary>
    ///     Converts one or more expression groups to a collection of like terms.
    /// </summary>
    /// <param name="expressionGroups"> </param>
    /// <returns> </returns>
    /// <exception cref="NotImplementedException"> Nested function simplification not implemented yet. </exception>
    public static LikeTermsCollection ToLikeTermsCollection(ExpressionGroup[] expressionGroups)
    {
        List<ExpressionTerm> terms = [];
        LikeTermsCollection likeTerms = [];
        foreach (ExpressionGroup input in expressionGroups)
        foreach (ExpressionPart part in input.Parts)
            if (part is ExpressionFunction function)
                throw new NotImplementedException();
            else if (part is ExpressionTerm term) terms.Add(term);
        foreach (ExpressionTerm term in terms) {
            List<Pronumeral> pronumerals = term.Pronumerals;

            if (!likeTerms.TryGetValue(pronumerals, out List<ExpressionTerm>? value)) {
                value = [];
                likeTerms[pronumerals] = value;
            }

            value.Add(term);
        }

        return likeTerms;
    }
}