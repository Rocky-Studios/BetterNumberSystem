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
    
    public delegate LikeTermsCollection OnEvaluate(LikeTermsCollection[] inputs);
    
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
            if (returnedFunction.Inputs.InputType.Infinite || 
                (returnedFunction.Inputs.InputType.One && inputs.Length == 1) || 
                (returnedFunction.Inputs.InputType.Two && inputs.Length == 2) || 
                (returnedFunction.Inputs.InputType.Three && inputs.Length == 3))
            {
                returnedFunction.Inputs.Inputs = inputs.Select(input => new ExpressionFunctionInput(null) { Value = input }).ToList();
                return returnedFunction;
            }
            else
            {
                throw new ArgumentException("Invalid number of inputs for function " + name);
            }
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
             _ = new ExpressionFunction("Difference", "-",
                (ExpressionFunctionInputs inputs) =>
                {
                    switch (inputs.Inputs.Count) {
                        case 1:
                        {
                            LikeTermsCollection likeTerms = [];
                            foreach (var likeTerm in inputs.Inputs[0].ToLikeTermsCollection()) {
                                if (likeTerms.ContainsKey(likeTerm.Key)) {
                                    List<ExpressionTerm> terms = likeTerms[likeTerm.Key];
                                    terms.AddRange(likeTerm.Value);
                                    likeTerms[likeTerm.Key] = terms;
                                }
                                else
                                    likeTerms.Add(likeTerm.Key, likeTerm.Value);
                            }

                            foreach (var likeTerm in likeTerms) {
                                foreach (var term in likeTerm.Value) {
                                    switch (term.Value) {
                                        case Number n:
                                            n.NumericValue *= -1;
                                            break;
                                        case Vector2 v2:
                                            v2.X.NumericValue *= -1;
                                            v2.Y.NumericValue *= -1;
                                            break;
                                        case Vector3 v3:
                                            v3.X.NumericValue *= -1;
                                            v3.Y.NumericValue *= -1;
                                            v3.Z.NumericValue *= -1;
                                            break;
                                        default:
                                            throw new ArgumentException("Cannot negate this type of value");
                                    }
                                }
                            }

                            return likeTerms;
                            break;
                        }
                        case 2:
                            LikeTermsCollection likeTermsA = [];
                            foreach (var likeTerm in inputs.Inputs[0].ToLikeTermsCollection()) {
                                if (likeTermsA.ContainsKey(likeTerm.Key)) {
                                    List<ExpressionTerm> terms = likeTermsA[likeTerm.Key];
                                    terms.AddRange(likeTerm.Value);
                                    likeTermsA[likeTerm.Key] = terms;
                                }
                                else
                                    likeTermsA.Add(likeTerm.Key, likeTerm.Value);
                            }
                            LikeTermsCollection likeTermsB = [];
                            foreach (var likeTerm in inputs.Inputs[1].ToLikeTermsCollection()) {
                                if (likeTermsB.ContainsKey(likeTerm.Key)) {
                                    List<ExpressionTerm> terms = likeTermsB[likeTerm.Key];
                                    terms.AddRange(likeTerm.Value);
                                    likeTermsB[likeTerm.Key] = terms;
                                }
                                else
                                    likeTermsB.Add(likeTerm.Key, likeTerm.Value);
                            }
                            
                            // Assuming we're only dealing with numbers (no vectors or matrices yet)
                            if (
                                likeTermsA.SelectMany(likeTermCollection
                                    => likeTermCollection.Value).Any(expressionTerm => expressionTerm.Value is not Number) ||
                                likeTermsB.SelectMany(likeTermCollection
                                    => likeTermCollection.Value).Any(expressionTerm => expressionTerm.Value is not Number)) {
                                throw new NotImplementedException();
                            }

                            LikeTermsCollection output = [];

                            foreach (var likeTermCollection in likeTermsA)
                            {
                                List<Pronumeral> pronumerals = likeTermCollection.Key;
                                Number total = new(0, (likeTermCollection.Value[0].Value as Number).Unit);
                            
                                foreach (ExpressionTerm t in likeTermCollection.Value)
                                {
                                    Number n = t.Value as Number;
                                    Number nConverted = n.Convert(total.Unit);
                                    total.NumericValue += nConverted.NumericValue;
                                }
                            
                                foreach (ExpressionTerm t in likeTermsB[pronumerals])
                                {
                                    Number n = t.Value as Number;
                                    Number nConverted = n.Convert(total.Unit);
                                    total.NumericValue -= nConverted.NumericValue;
                                }

                                total.NumericValue = Math.Round(total.NumericValue, 12);
                                output.Add(pronumerals, [(ExpressionTerm)total]);
                            }
                            
                            return output;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(inputs), "Difference function can only take 1 or 2 inputs");
                    }
                    

                    
                },
                new ExpressionFunctionInputs(one: true, two: true)
            );
             
             _ = new ExpressionFunction("Sine", "sin",
                (ExpressionFunctionInputs inputs) =>
                {
                    LikeTermsCollection likeTerms = [];
                    
                    foreach(var likeTerm in inputs.Inputs[0].ToLikeTermsCollection())
                    {
                        if (likeTerms.ContainsKey(likeTerm.Key)) {
                            List<ExpressionTerm> terms = likeTerms[likeTerm.Key];
                            terms.AddRange(likeTerm.Value);
                            likeTerms[likeTerm.Key] = terms;
                        } else
                            likeTerms.Add(likeTerm.Key, likeTerm.Value);
                    }
                    
                    // Assuming we're only dealing with numbers (no vectors or matrices yet)
                    foreach (KeyValuePair<List<Pronumeral>, List<ExpressionTerm>> likeTermCollection in likeTerms)
                        foreach (ExpressionTerm expressionTerm in likeTermCollection.Value)
                            if (expressionTerm.Value is not Number) throw new NotImplementedException();

                    if((likeTerms.First().Value[0].Value as Number).MeasurementType != MeasurementType.Angle) throw new ArgumentException("Sine function only works with angles");
                    
                    LikeTermsCollection output = [];

                    foreach (KeyValuePair<List<Pronumeral>, List<ExpressionTerm>> likeTermCollection in likeTerms)
                    {
                        List<Pronumeral> pronumerals = likeTermCollection.Key;
                        Number angle = likeTermCollection.Value[0].Value as Number;
                        double angleInRads;
                        if(angle.Unit == UnitManager.Units["Degree"]) angleInRads = angle.NumericValue * Math.PI / 180;
                        else if(angle.Unit == UnitManager.Units["Radian"]) angleInRads = angle.NumericValue;
                        else throw new ArgumentException("Sine function only works with angles");
                        
                        output.Add(pronumerals, [(ExpressionTerm)new Number(Math.Sin(angleInRads), UnitManager.Units["Plain"])]);
                    }
                    return output;
                },
                new ExpressionFunctionInputs(one: true)
            );
             
             _ = new ExpressionFunction("Cosine", "cos",
                (ExpressionFunctionInputs inputs) =>
                {
                    LikeTermsCollection likeTerms = [];
                    
                    foreach(var likeTerm in inputs.Inputs[0].ToLikeTermsCollection())
                    {
                        if (likeTerms.ContainsKey(likeTerm.Key)) {
                            List<ExpressionTerm> terms = likeTerms[likeTerm.Key];
                            terms.AddRange(likeTerm.Value);
                            likeTerms[likeTerm.Key] = terms;
                        } else
                            likeTerms.Add(likeTerm.Key, likeTerm.Value);
                    }
                    
                    // Assuming we're only dealing with numbers (no vectors or matrices yet)
                    foreach (KeyValuePair<List<Pronumeral>, List<ExpressionTerm>> likeTermCollection in likeTerms)
                        foreach (ExpressionTerm expressionTerm in likeTermCollection.Value)
                            if (expressionTerm.Value is not Number) throw new NotImplementedException();

                    if((likeTerms.First().Value[0].Value as Number).MeasurementType != MeasurementType.Angle) throw new ArgumentException("Sine function only works with angles");
                    
                    LikeTermsCollection output = [];

                    foreach (KeyValuePair<List<Pronumeral>, List<ExpressionTerm>> likeTermCollection in likeTerms)
                    {
                        List<Pronumeral> pronumerals = likeTermCollection.Key;
                        Number angle = likeTermCollection.Value[0].Value as Number;
                        double angleInRads;
                        if(angle.Unit == UnitManager.Units["Degree"]) angleInRads = angle.NumericValue * Math.PI / 180;
                        else if(angle.Unit == UnitManager.Units["Radian"]) angleInRads = angle.NumericValue;
                        else throw new ArgumentException("Cosine function only works with angles");
                        
                        output.Add(pronumerals, [(ExpressionTerm)new Number(Math.Cos(angleInRads), UnitManager.Units["Plain"])]);
                    }
                    return output;
                },
                new ExpressionFunctionInputs(one: true)
            );
        }
    }
}
