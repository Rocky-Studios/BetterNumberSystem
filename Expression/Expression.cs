using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterNumberSystem.Expression
{
    /// <summary>
    /// A collection of functions and terms.
    /// </summary>
    /// <param name="rootFunction">The last function to be evaluated, the root of the expression tree.</param>
    /// <example>
    /// In the expression 5 × 4 + 3, the root function is the +, because it is evaluated last.
    /// </example>
    public class Expression(ExpressionFunction rootFunction)
    {
        /// <summary>
        /// The last function to be evaluated, the root of the expression tree.
        /// </summary>
        /// <example>
        /// In the expression 5 × 4 + 3, the root function is the +, because it is evaluated last.
        /// </example>
        public ExpressionFunction RootFunction = rootFunction;

        /// <summary>
        /// The result of the expression when evaluated.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ExpressionGroup Evaluate()
        {
            if (RootFunction is null) throw new Exception("Empty/invalid expresssion");

            return RootFunction.Function(RootFunction.Inputs);
        }

        public LikeTermsCollection Simplify()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Evalutes the expression and displays the result as text
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Evaluate().ToString();
        }
    }
}
