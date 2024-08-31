using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterNumberSystem.Expression
{
    /// <summary>
    /// The base class for things that make up an expression
    /// </summary>
    public interface IExpressionPart
    {

    }
    /// <summary>
    /// The values on which mathematical operations occur in an algebraic expression
    /// </summary>
    public class ExpressionTerm : IExpressionPart
    {
        public IExpressionValue Value;
        public List<IPronumeral> Pronumerals;
    }
    /// <summary>
    /// Anything that changes values, +, -, sin, summation, exponents, integrals etc...
    /// </summary>
    public class ExpressionFunction : IExpressionPart
    {
        /// <summary>
        /// The full name of the function eg. Sum, Integral
        /// </summary>
        public string Name = "";
        /// <summary>
        /// A symbol used to identify this function eg. + for sum, ∫ for integral
        /// </summary>
        public string? Symbol = "";
        /// <summary>
        /// The computations that happen when the function is evaluated
        /// </summary>
        public MathFunction Function;
    }
    /// <summary>
    /// The computations that happen when a function is evaluated
    /// </summary>
    public delegate IExpressionValue MathFunction(IExpressionValue[] inputs);
    /// <summary>
    /// The solver will prioritize things inside this group (basically acts like brackets)
    /// </summary>
    public class ExpressionGroup : IExpressionPart
    {
        public List<IExpressionPart> Parts = new();
    }
}
