using System;

namespace BetterNumberSystem.Expression
{
    /// <summary>
    /// Anything that changes values, +, -, sin, summation, exponents, integrals etc...
    /// </summary>
    public class ExpressionFunction : ExpressionPart
    {
        /// <summary>
        /// The full name of the function eg. Sum, Integral.
        /// </summary>
        public string Name = "";
        /// <summary>
        /// A short and recognisable symbol used to identify this function eg. + for sum, ∫ for integral.
        /// </summary>
        public string? Symbol = "";
        /// <summary>
        /// The computations that happen when the function is evaluated.
        /// </summary>
        public MathFunction Function;
        /// <summary>
        /// The inputs to the computation.
        /// </summary>
        public ExpressionFunctionInputs Inputs;

        /// <summary>
        /// Creates an <c>ExpressionFunction</c> with the specified parameters.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="symbol"></param>
        /// <param name="function"></param>
        /// <param name="inputs"></param>
        /// <param name="cloned">Whether this instance of this function has been cloned from another</param>
        public ExpressionFunction(string name, string? symbol, MathFunction function, ExpressionFunctionInputs inputs, bool cloned = false)
        {
            Name = name;
            Symbol = symbol;
            Function = function;
            Inputs = inputs;

            if(!cloned) FunctionManager.Functions.Add(name, this);
        }
        /// <summary>
        /// Generates a new duplicate instance of this function.
        /// </summary>
        /// <returns></returns>
        public ExpressionFunction Clone()
        {
            ExpressionFunction newFunction = new(Name, Symbol, Function, Inputs, true);
            return newFunction;
        }
    }

    /// <summary>
    /// The computations that happen when a function is evaluated.
    /// </summary>
    public delegate LikeTermsCollection MathFunction(ExpressionFunctionInputs inputs);
    /// <summary>
    /// Defines the structure of expression inputs.
    /// </summary>
    public class ExpressionFunctionInputs
    {
        /// <summary>
        /// How many inputs this function can take. <br/>
        /// NOTE: If this function can take any amount of inputs, ALWAYS use only the infinite property.
        /// </summary>
        public ExpressionFunctionInputAmount InputType;
        /// <summary>
        /// The inputs for this function. <br/>
        /// NOTE: the last input will be used for functions that take an infinite amount of inputs.
        /// </summary>
        public List<ExpressionFunctionInput> Inputs = [];
        /// <summary>
        /// Creates a new function input decleration by specifying the input amounts
        /// </summary>
        public ExpressionFunctionInputs(bool one = false, bool two = false, bool three = false, bool infinite = false)
        {
            InputType = new ExpressionFunctionInputAmount(one, two, three, infinite);
            Inputs = [];
        }
    }
    /// <summary>
    /// How many inputs a function can take.<br/>
    /// NOTE: If this function can take any amount of inputs, ALWAYS use only the infinite property.
    /// </summary>
    public struct ExpressionFunctionInputAmount(bool one, bool two, bool three, bool infinite)
    {
        /// <summary>
        /// Where this function works when it has only one input.
        /// </summary>
        public bool One = one;
        /// <summary>
        /// Where this function works when it has 2 inputs.
        /// </summary>
        public bool Two = two;
        /// <summary>
        /// Where this function works when it has 2 inputs.
        /// </summary>
        public bool Three = three;
        /// <summary>
        /// Where this function works when it has any amonut of inputs.
        /// </summary>
        public bool Infinite = infinite;
    }

    /// <summary>
    /// Describes an input for a function.
    /// </summary>
    /// <param name="name">The name used to identify this input.</param>
    public struct ExpressionFunctionInput(string? name)
    {
        /// <summary>
        /// The name used to identify this input.
        /// </summary>
        public string? Name = name;
        /// <summary>
        /// The value of this input.
        /// </summary>
        public ExpressionGroup? Value = null;
        /// <summary>
        /// Converts the input to a collection of like terms.
        /// </summary>
        /// <returns></returns>
        public LikeTermsCollection ToLikeTermsCollection()
        {
            List<ExpressionGroup> inputsAsGroups = [];
            if (Value is not null) inputsAsGroups.Add(Value);
            return ExpressionGroup.ToLikeTermsCollection([.. inputsAsGroups]);
        }
    }
    /// <summary>
    /// A class for cloning functions for use in expressions.
    /// </summary>
    public static class FunctionManager
    {
        /// <summary>
        /// The list of all available functions.
        /// </summary>
        public static readonly Dictionary<string, ExpressionFunction> Functions = [];
        /// <summary>
        /// Query for a function by its full name.
        /// </summary>
        /// <param name="name">The full name of the function eg. Sum for +.</param>
        /// <returns></returns>
        public static ExpressionFunction Get(string name)
        {
            try
            {
                Functions.TryGetValue(name, out ExpressionFunction returnedFunction);
                returnedFunction = returnedFunction.Clone();
                return returnedFunction;
            }
            catch (Exception)
            {
                throw new ArgumentException("Cannot find function with name " + name);
            }
        }
        /// <summary>
        /// Query for a function by its full name and assign its inputs.
        /// </summary>
        /// <param name="name">The full name of the function eg. Sum for +</param>
        /// <param name="inputs">The inputs to assign</param>
        /// <returns></returns>
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
            else throw new NotImplementedException();
            return returnedFunction;
        }
        
        static FunctionManager()
        {
            // Initialize all default functions
            _ = new ExpressionFunction("Sum", "+",
                (ExpressionFunctionInputs inputs) =>
                {
                    LikeTermsCollection likeTerms = [];
                    foreach (ExpressionFunctionInput input in inputs.Inputs)
                    {
                        foreach(var likeTerm in input.ToLikeTermsCollection())
                        {
                            if (likeTerms.ContainsKey(likeTerm.Key)) {
                                List<ExpressionTerm> terms = likeTerms[likeTerm.Key];
                                terms.AddRange(likeTerm.Value);
                                likeTerms[likeTerm.Key] = terms;
                            } else
                                likeTerms.Add(likeTerm.Key, likeTerm.Value);
                        }
                    }
                    // Assuming we're only dealing with numbers (no vectors or matrices yet)
                    foreach (KeyValuePair<List<Pronumeral>, List<ExpressionTerm>> likeTermCollection in likeTerms)
                        foreach (ExpressionTerm expressionTerm in likeTermCollection.Value)
                            if (expressionTerm.Value is not Number) throw new NotImplementedException();

                    LikeTermsCollection output = [];

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

                        output.Add(pronumerals, [(ExpressionTerm)total]);
                    }
                    return output;
                },
                new ExpressionFunctionInputs(infinite: true)
            );
        }
    }
}
