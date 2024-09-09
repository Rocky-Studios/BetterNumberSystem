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
        public List<Pronumeral> Pronumerals;

        public override string ToString()
        {
            string pronumerals = "";
            foreach (Pronumeral pronumeral in Pronumerals)
            {
                pronumerals += pronumeral.Symbol;
            }
            return Value.ToString() + pronumerals;
        }
    }

    /// <summary>
    /// The solver will prioritize things inside this group (basically acts like brackets)
    /// </summary>
    public class ExpressionGroup : IExpressionPart
    {
        public List<IExpressionPart> Parts = new();

        public override string ToString()
        {
            string output = "";
            foreach (IExpressionPart part in Parts)
            {
                output += part.ToString() + " ";
            }
            return output;
        }
        /// <summary>
        /// Converts multiple (or one) expression group to a collection of like terms
        /// </summary>
        /// <param name="expressionGroups"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Nested function simplification not implemented yet</exception>
        public static LikeTermsCollection ToLikeTermsCollection(ExpressionGroup[] expressionGroups)
        {
            List<ExpressionTerm> terms = [];
            LikeTermsCollection likeTerms = [];
            foreach (ExpressionGroup input in expressionGroups)
            {
                foreach (IExpressionPart part in input.Parts)
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
                List<Pronumeral> pronumerals = term.Pronumerals;

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
