﻿using System;
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

    public class ExpressionFunctionInputs : IExpressionPart
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
    }
}
