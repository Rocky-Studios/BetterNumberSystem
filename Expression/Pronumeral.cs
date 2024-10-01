namespace BetterNumberSystem.Expression;

/// <summary>
///     A value represented by a symbol. <br />
///     Do not use this class directly, use <see cref="Variable" /> or <see cref="Constant" /> instead.
/// </summary>
public abstract class Pronumeral
{
    /// <summary>
    ///     The short symbol of the pronumeral.
    /// </summary>
    public string Symbol;

    /// <summary>
    ///     The name of the pronumeral.
    /// </summary>
    public string Name;

    /// <summary>
    ///     The value of the pronumeral.
    /// </summary>
    public ExpressionTerm Value;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Pronumeral" /> class with the specified symbol, name, and value.
    /// </summary>
    /// <param name="symbol"> The short symbol of the pronumeral. </param>
    /// <param name="name"> The name of the pronumeral. </param>
    /// <param name="value"> The value of the pronumeral. </param>
    protected Pronumeral(string symbol, string name, ExpressionTerm value)
    {
        Symbol = symbol;
        Name = name;
        Value = value;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Pronumeral" /> class with the specified symbol and value.
    ///     The name of the pronumeral is set to the symbol.
    /// </summary>
    /// <param name="symbol"> The short symbol of the pronumeral. </param>
    /// <param name="value"> The value of the pronumeral. </param>
    protected Pronumeral(string symbol, ExpressionTerm value)
    {
        Symbol = symbol;
        Name = symbol;
        Value = value;
    }

    /// <summary>
    ///     Represents the pronumeral of a term that has no other pronumerals. This is to be used exclusively as a key in a
    ///     <see cref="LikeTermsCollection" />.
    /// </summary>
    public static Pronumeral NoPronumeral = new Constant("", new Number(1));

    /// <summary>
    ///     Provides methods to compare lists of <see cref="Pronumeral" /> objects for equality and to generate hash codes for
    ///     such lists.
    /// </summary>
    /// <example>
    ///     <code>
    ///         var comparer = new PronumeralListEqualityComparer();
    ///         var list1 = [ new Variable("x", new ExpressionTerm()), new Constant("π", new Number(MathF.PI)) ];
    ///         var list2 = [ new Variable("x", new ExpressionTerm()), new Constant("π", new Number(MathF.PI)) ];
    ///         bool areEqual = comparer.Equals(list1, list2); // returns true
    ///         int hashCode = comparer.GetHashCode(list1); // returns a hash code for the list
    ///     </code>
    /// </example>
    public class PronumeralListEqualityComparer : IEqualityComparer<List<Pronumeral>>
    {
        /// <summary>
        ///     Determines whether the specified lists of <see cref="Pronumeral" /> objects are equal.
        /// </summary>
        /// <param name="x"> The first list of <see cref="Pronumeral" /> objects to compare. </param>
        /// <param name="y"> The second list of <see cref="Pronumeral" /> objects to compare. </param>
        /// <returns> <c> true </c> if the specified lists are equal; otherwise, <c> false </c>. </returns>
        public bool Equals(List<Pronumeral>? x, List<Pronumeral>? y)
        {
            if (x == null || y == null)
                return x == y;

            return x.Count == y.Count && !x.Except(y).Any();
        }

        /// <summary>
        ///     Returns a hash code for the specified list of <see cref="Pronumeral" /> objects.
        /// </summary>
        /// <param name="obj"> The list of <see cref="Pronumeral" /> objects for which to get the hash code. </param>
        /// <returns> A hash code for the specified list of <see cref="Pronumeral" /> objects. </returns>
        /// <example>
        ///     <code>
        /// var comparer = new PronumeralListEqualityComparer();
        /// var list = [ new Variable("x", new ExpressionTerm()), new Constant("π", new Number(MathF.PI)) ];
        /// int hashCode = comparer.GetHashCode(list); // returns a hash code for the list
        /// </code>
        /// </example>
        public int GetHashCode(List<Pronumeral> obj)
        {
            // Use a combination of pronumeral hashes to generate a hash code for the list
            unchecked {
                return obj.Aggregate(0, (current, pronumeral) => (current * 397) ^ pronumeral.GetHashCode());
            }
        }
    }

    /// <summary>
    ///     A pronumeral whose value can change
    /// </summary>
    public class Variable : Pronumeral
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Pronumeral" /> class with the specified symbol, name, and value.
        /// </summary>
        /// <param name="symbol"> The short symbol of the pronumeral. </param>
        /// <param name="name"> The name of the pronumeral. </param>
        /// <param name="value"> The value of the pronumeral. </param>
        public Variable(string symbol, string name, ExpressionTerm value) : base(symbol, name, value) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Pronumeral" /> class with the specified symbol and value.
        ///     The name of the pronumeral is set to the symbol.
        /// </summary>
        /// <param name="symbol"> The short symbol of the pronumeral. </param>
        /// <param name="value"> The value of the pronumeral. </param>
        public Variable(string symbol, ExpressionTerm value) : base(symbol, value) { }
    }

    /// <summary>
    ///     A pronumeral whose value is constant
    /// </summary>
    public class Constant : Pronumeral
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Pronumeral" /> class with the specified symbol, name, and value.
        /// </summary>
        /// <param name="symbol"> The short symbol of the pronumeral. </param>
        /// <param name="name"> The name of the pronumeral. </param>
        /// <param name="value"> The value of the pronumeral. </param>
        public Constant(string symbol, string name, ExpressionTerm value) : base(symbol, name, value) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Pronumeral" /> class with the specified symbol and value.
        ///     The name of the pronumeral is set to the symbol.
        /// </summary>
        /// <param name="symbol"> The short symbol of the pronumeral. </param>
        /// <param name="value"> The value of the pronumeral. </param>
        public Constant(string symbol, ExpressionTerm value) : base(symbol, value) { }

        /// <summary>
        ///     A circle's circumference divided by its diameter
        /// </summary>
        public static Constant Pi = new("π", "pi", new Number(MathF.PI));

        /// <summary>
        ///     The base of the natural logarithm
        /// </summary>
        public static Constant E = new("e", new Number(MathF.E));
    }
}