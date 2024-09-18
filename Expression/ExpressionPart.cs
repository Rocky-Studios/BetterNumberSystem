using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterNumberSystem.Expression
{
    /// <summary>
    /// The base class for things that make up an expression.
    /// </summary>
    public abstract class ExpressionPart
    {
        /// <summary>
        /// Implicitly converts a number to a term.
        /// </summary>
        /// <param name="num">The number to be converted</param>
        public static implicit operator ExpressionPart(Number num) => new ExpressionTerm()
        {
            Value = num,
            Pronumerals = [(Pronumeral.NO_PRONUMERAL, 1)]
        };
    }
    /// <summary>
    /// The values on which mathematical operations occur in an algebraic expression.
    /// </summary>
    public class ExpressionTerm : ExpressionPart
    {
        /// <summary>
        /// The coefficient of the term.
        /// </summary>
        public IExpressionValue Value;
        /// <summary>
        /// The variables or constants of the term. <br/>
        /// Item1: The pronumeral. <br/>
        /// Item2: The power of the pronumeral.
        /// </summary>
        public List<(Pronumeral, int)> Pronumerals;

        /// <summary>
        /// Shows the coefficient and pronumerals of the term as text.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string pronumerals = "";
            foreach (var pronumeral in Pronumerals)
            {
                bool powerIsOne = pronumeral.Item2 == 1;
                pronumerals += pronumeral.Item1.Symbol;
                if (!powerIsOne) pronumerals += "^" + pronumeral.Item2;
                
            }
            return Value.ToString() + pronumerals;
        }
    }

    /// <summary>
    /// A group of functions and terms.
    /// </summary>
    public class ExpressionGroup : ExpressionPart
    {
        /// <summary>
        /// The collection of functions and terms.
        /// </summary>
        public List<ExpressionPart> Parts = [];
        /// <summary>
        /// Creates an empty <c>ExpressionGroup</c>.
        /// </summary>
        public ExpressionGroup()
        {
            Parts = [];
        }
        /// <summary>
        /// Creates an <c>ExpressionGroup</c> with specified parts.
        /// </summary>
        /// <param name="parts">The list of parts.</param>
        public ExpressionGroup(params ExpressionPart[] parts)
        {
            Parts = [.. parts];
        }
        /// <summary>
        /// Converts the <c>ExpressionGroup</c> to text by converting each part to text.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string output = "";
            foreach (ExpressionPart part in Parts)
            {
                output += part.ToString() + " ";
            }
            return output;
        }
        /// <summary>
        /// Converts one or more expression groups to a collection of like terms.
        /// </summary>
        /// <param name="expressionGroups"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Nested function simplification not implemented yet.</exception>
        public static LikeTermsCollection ToLikeTermsCollection(ExpressionGroup[] expressionGroups)
        {
            List<ExpressionTerm> terms = [];
            LikeTermsCollection likeTerms = [];
            foreach (ExpressionGroup input in expressionGroups)
            {
                foreach (ExpressionPart part in input.Parts)
                {
                    if (part is ExpressionFunction function)
                    {
                        throw new NotImplementedException();
                    }
                    else if (part is ExpressionTerm term)
                    {
                        terms.Add(term);
                    }
                }
            }
            foreach (var term in terms)
            {
                List< (Pronumeral, int)> pronumerals = term.Pronumerals;

                if (!likeTerms.TryGetValue(pronumerals, out List<ExpressionTerm>? value))
                {
                    value = [];
                    likeTerms[pronumerals] = value;
                }

                value.Add(term);
            }
            return likeTerms;
        }
    }
}
