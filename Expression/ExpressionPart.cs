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
    /// Anything that changes values, +, -, sin, summation, exponents, integrals etc...
    /// </summary>
    public class ExpressionFunction : IExpressionPart, ICloneable
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
        public ExpressionFunctionInputs Inputs;

        public ExpressionFunction(string name, string? symbol, MathFunction function, ExpressionFunctionInputs inputs)
        {
            Name = name;
            Symbol = symbol;
            Function = function;
            Inputs = inputs;
        }
        /// <summary>
        /// Generates a new instance with duplicate
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new ExpressionFunction(Name, Symbol, Function, Inputs);
        }
    }
    /// <summary>
    /// The computations that happen when a function is evaluated
    /// </summary>
    public delegate ExpressionGroup MathFunction(ExpressionFunctionInputs inputs);

    public class ExpressionFunctionInputs
    {
        /// <summary>
        /// How many inputs this function can take <br/>
        /// NOTE: If this function can take any amount of inputs, ALWAYS use only the infinite property
        /// </summary>
        public ExpressionFunctionInputAmount InputType;
        /// <summary>
        /// The inputs for this function <br/>
        /// NOTE: the last input will be used for functions that take an infinite amount of inputs
        /// </summary>
        public List<ExpressionFunctionInput> Inputs = [];

        public LikeTermsCollection ToLikeTermsCollection()
        {
            List<ExpressionGroup> inputsAsGroups = [];
            foreach (ExpressionFunctionInput input in Inputs)
            {
                inputsAsGroups.Add(input.Value);
            }
            return ExpressionGroup.ToLikeTermsCollection(inputsAsGroups.ToArray());
        }
}
    /// <summary>
    /// How many inputs a function can take <br/>
    /// NOTE: If this function can take any amount of inputs, ALWAYS use only the infinite property
    /// </summary>
    public struct ExpressionFunctionInputAmount
    {
        public bool One = false;
        public bool Two = true;
        public bool Three = false;
        public bool Infinite = false;

        public ExpressionFunctionInputAmount(bool one, bool two, bool three, bool infinite)
        {
            One = one;
            Two = two;
            Three = three;
            Infinite = infinite;
        }
    }

    /// <summary>
    /// Describes an input for a function
    /// </summary>
    /// <param name="name">The name used to identify this input</param>
    /// <param name="identityValue">The identity value, such that when it is the first input in a two input function, the function will return the second input eg. the multiplicative identity is 1 because 1 × x = x</param>
    public struct ExpressionFunctionInput(string? name, ExpressionGroup? identityValue)
    {
        /// <summary>
        /// The name used to identify this input
        /// </summary>
        public string? Name = name;
        /// <summary>
        /// The identity value, such that when it is the first input in a two input function, the function will return the second input eg. the multiplicative identity is 1 because 1 × x = x
        /// </summary>
        public ExpressionGroup? Value = identityValue;
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
