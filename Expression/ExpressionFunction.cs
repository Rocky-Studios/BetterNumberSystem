using System;

namespace BetterNumberSystem.Expression
{
    /// <summary>
    /// Anything that changes values, +, -, sin, summation, exponents, integrals etc...
    /// </summary>
    public class ExpressionFunction : ExpressionPart
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
        public ExpressionFunction Clone()
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
    /// <param name="value"></param>
    public struct ExpressionFunctionInput(string? name, ExpressionGroup? value)
    {
        /// <summary>
        /// The name used to identify this input
        /// </summary>
        public string? Name = name;
        /// <summary>
        /// The identity value, such that when it is the first input in a two input function, the function will return the second input eg. the multiplicative identity is 1 because 1 × x = x
        /// </summary>
        public ExpressionGroup? Value = value;
    }

    public static class FunctionManager
    {
        public static Dictionary<string, ExpressionFunction> Functions = [];

        public static ExpressionFunction Get(string name)
        {
            Functions.TryGetValue(name, out ExpressionFunction returnedFunction);
            returnedFunction = returnedFunction.Clone();
            return returnedFunction;
        }

        public static ExpressionFunction Get(string name, ExpressionGroup[] inputs)
        {
            Functions.TryGetValue(name, out ExpressionFunction returnedFunction);
            returnedFunction = returnedFunction.Clone();
            if (returnedFunction.Inputs.InputType.Infinite == true)
            {
                foreach (ExpressionGroup input in inputs)
                {
                    returnedFunction.Inputs.Inputs.Add(new ExpressionFunctionInput()
                    {
                        Name = "",
                        Value = input
                    });
                }
            }
            return returnedFunction;
        }

        static FunctionManager()
        {
            Functions.Add("Sum",
                new("Sum", "+",
                (ExpressionFunctionInputs inputs) =>
                {
                    LikeTermsCollection likeTerms = inputs.ToLikeTermsCollection();

                    // Assuming we're only dealing with numbers (no vectors or matrices yet)
                    foreach (KeyValuePair<List<Pronumeral>, List<ExpressionTerm>> likeTermCollection in likeTerms)
                        foreach (ExpressionTerm expressionTerm in likeTermCollection.Value)
                            if (expressionTerm.Value is not Number) throw new NotImplementedException();

                    ExpressionGroup output = new();

                    foreach (KeyValuePair<List<Pronumeral>, List<ExpressionTerm>> likeTermCollection in likeTerms)
                    {
                        List<Pronumeral> pronumerals = likeTermCollection.Key;
                        Number total = new(0, (likeTermCollection.Value[0].Value as Number).Unit);

                        foreach (ExpressionTerm t in likeTermCollection.Value)
                        {
                            Number n = t.Value as Number;
                            Number nConverted = n.Convert(total.Unit);
                            total.NumericValue += nConverted.NumericValue;
                        }

                        output.Parts.Add(new ExpressionTerm() { Value = total, Pronumerals = pronumerals });
                    }
                    return output;
                },
                new ExpressionFunctionInputs()
                {
                    InputType = new ExpressionFunctionInputAmount()
                    {
                        Infinite = true
                    }
                }
            ));
        }
    }
}
