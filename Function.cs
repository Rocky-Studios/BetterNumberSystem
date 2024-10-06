﻿using System.Reflection;

namespace BetterNumberSystem;

/// <summary>
///     Represents a function that can be used to process terms.
/// </summary>
public class Function
{
    /// <summary>
    ///     The name of the function.
    /// </summary>
    public string Name;

    /// <summary>
    ///     The symbol of the function.
    /// </summary>
    public string Symbol;

    /// <summary>
    ///     All the processes that can be used by the function to act upon the inputs.
    /// </summary>
    public List<Delegate> Processes = [];

    /// <summary>
    ///     Compares two arrays of types to see if they are the same.
    /// </summary>
    /// <param name="a"> </param>
    /// <param name="b"> </param>
    /// <returns> </returns>
    private static bool CompareArguments(Type[] a, Type[] b)
    {
        if (a.Length != b.Length) return false;
        if (a.Length == 0) return true;
        bool discrepancy = false;
        foreach (Type bType in b)
        foreach (Type aType in a)
            if (aType != bType)
                discrepancy = true;
        return !discrepancy;
    }

    /// <summary>
    ///     Computes the function with the given inputs.
    /// </summary>
    /// <param name="inputs"> </param>
    /// <returns> </returns>
    /// <exception cref="ArgumentException"> There is no suitable process found for this combination of inputs </exception>
    public Term[] Process(Term[] inputs)
    {
        List<Type> coefficientTypes = [];
        coefficientTypes.AddRange(inputs.Select(input => input.Coefficient.GetType()));
        foreach (Delegate process in Processes) {
            Type[] genericArguments = process.GetType().GetGenericArguments();
            if (!CompareArguments(genericArguments, coefficientTypes.ToArray())) continue;

            MethodInfo? methodInfo = process.GetType().GetMethod("Invoke");
            if (methodInfo != null) return (Term[])methodInfo.Invoke(process, new object[] { inputs })!;
        }

        throw new ArgumentException("No matching process found for the given inputs.");
    }

    /// <summary>
    ///     Initialises a function with the default processes.
    /// </summary>
    /// <param name="name"> The name of the function.
    ///     <example> Sum </example>
    /// </param>
    /// <param name="symbol"> The symbol of the function.
    ///     <example> + </example>
    /// </param>
    public Function(string name, string symbol)
    {
        Name = name;
        Symbol = symbol;
        Processes.Add(new FunctionDelegate1In<Number>(inputs => inputs));
        Processes.Add(new FunctionDelegate2In<Number, Number>(inputs => inputs));
        Processes.Add(new FunctionDelegate3In<Number, Number, Number>(inputs => inputs));
        Processes.Add(new FunctionDelegate4In<Number, Number, Number, Number>(inputs => inputs));
        Processes.Add(new FunctionDelegate5In<Number, Number, Number, Number, Number>(inputs => inputs));
    }

    /// <summary>
    ///     Initialises a function with custom processes.
    /// </summary>
    /// <param name="name"> The name of the function.
    ///     <example> Sum </example>
    /// </param>
    /// <param name="symbol"> The symbol of the function.
    ///     <example> + </example>
    /// </param>
    /// <param name="customProcesses"> The custom, user-defined processes for the function. </param>
    /// <param name="defaultProcesses"> The built-in, autogenerated processes that if there is just one input, it returns it unchanged. </param>
    public Function(string name, string symbol, Delegate[] customProcesses, bool defaultProcesses = true)
    {
        Name = name;
        Symbol = symbol;
        Processes.Add(new FunctionDelegate1In<Number>(inputs => inputs));
        Processes.Add(new FunctionDelegate2In<Number, Number>(inputs => inputs));
        Processes.Add(new FunctionDelegate3In<Number, Number, Number>(inputs => inputs));
        Processes.Add(new FunctionDelegate4In<Number, Number, Number, Number>(inputs => inputs));
        Processes.Add(new FunctionDelegate5In<Number, Number, Number, Number, Number>(inputs => inputs));

        Processes = [..customProcesses];
    }

    /// <summary>
    ///     Represents the addition function.
    /// </summary>
    public static Function Sum = new("Sum", "+",
    [
        new FunctionDelegate2In<Number, Number>(inputs =>
        {
            Term t1 = inputs[0];
            Term t2 = inputs[1];

            if (t1.Quantity != t2.Quantity)
                return [t1, t2];
            if (t1.Prefix == t2.Prefix)
                return
                [
                    new Term(new Number((t1.Coefficient as Number)!.Value + (t2.Coefficient as Number)!.Value),
                        t1.Pronumerals)
                ];

            Term t2Converted = t2.Convert(t1.Prefix!);
            return
            [
                new Term(new Number((t1.Coefficient as Number)!.Value + (t2Converted.Coefficient as Number)!.Value),
                    t1.Pronumerals)
            ];
        })
    ]);
    
    /// <summary>
    ///     Represents the subtraction function.
    /// </summary>
    public static Function Difference = new("Difference", "-",
    [
        new FunctionDelegate1In<Number>(inputs =>
        {
            Term t1 = inputs[0];
            return
            [
                new Term(new Number(-(t1.Coefficient as Number)!.Value),
                    t1.Pronumerals)
            ];
        }),
        new FunctionDelegate2In<Number, Number>(inputs =>
        {
            Term t1 = inputs[0];
            Term t2 = inputs[1];

            if (t1.Quantity != t2.Quantity)
                throw new ArithmeticException("Cannot subtract terms that represent different quantities.");
            if (t1.Prefix == t2.Prefix)
                return
                [
                    new Term(new Number((t1.Coefficient as Number)!.Value - (t2.Coefficient as Number)!.Value),
                        t1.Pronumerals)
                ];

            Term t2Converted = t2.Convert(t1.Prefix!);
            return
            [
                new Term(new Number((t1.Coefficient as Number)!.Value - (t2Converted.Coefficient as Number)!.Value),
                    t1.Pronumerals)
            ];
        })
    ], false);
}

/// <summary>
///     How the function should be processed with 1 input.
/// </summary>
/// <typeparam name="TypeA"> </typeparam>
public delegate Term[] FunctionDelegate1In<TypeA>(Term[] inputs);

/// <summary>
///     How the function should be processed with 2 inputs.
/// </summary>
/// <typeparam name="TypeA"> </typeparam>
/// <typeparam name="TypeB"> </typeparam>
public delegate Term[] FunctionDelegate2In<TypeA, TypeB>(Term[] inputs);

/// <summary>
///     How the function should be processed with 3 inputs.
/// </summary>
/// <typeparam name="TypeA"> </typeparam>
/// <typeparam name="TypeB"> </typeparam>
/// <typeparam name="TypeC"> </typeparam>
public delegate Term[] FunctionDelegate3In<TypeA, TypeB, TypeC>(Term[] inputs);

/// <summary>
///     How the function should be processed with 4 inputs.
/// </summary>
/// <typeparam name="TypeA"> </typeparam>
/// <typeparam name="TypeB"> </typeparam>
/// <typeparam name="TypeC"> </typeparam>
/// <typeparam name="TypeD"> </typeparam>
public delegate Term[] FunctionDelegate4In<TypeA, TypeB, TypeC, TypeD>(Term[] inputs);

/// <summary>
///     How the function should be processed with 5 inputs.
/// </summary>
/// <typeparam name="TypeA"> </typeparam>
/// <typeparam name="TypeB"> </typeparam>
/// <typeparam name="TypeC"> </typeparam>
/// <typeparam name="TypeD"> </typeparam>
/// <typeparam name="TypeE"> </typeparam>
public delegate Term[] FunctionDelegate5In<TypeA, TypeB, TypeC, TypeD, TypeE>(Term[] inputs);