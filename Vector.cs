using BetterNumberSystem.Expression;

namespace BetterNumberSystem;

/// <summary>
///     A quantity with both direction and magnitude
/// </summary>
public interface IVector : IExpressionValue
{
    /// <summary>
    ///     The horizontal component
    /// </summary>
    public Number X { get; set; }

    /// <summary>
    ///     The vertical component
    /// </summary>
    public Number Y { get; set; }

    /// <summary>
    ///     The distance from the origin to this point
    /// </summary>
    public Number GetMagnitude()
    {
        Number yConverted = Y.Convert(X.Unit);
        return new Number(
            Math.Sqrt(X.NumericValue * X.NumericValue + yConverted.NumericValue * yConverted.NumericValue), X.Unit);
    }
}

/// <summary>
///     A 2D quantity with both direction and magnitude
/// </summary>
public class Vector2 : IVector
{
    /// <summary>
    ///     The horizontal component
    /// </summary>
    public Number X { get; set; }

    /// <summary>
    ///     The vertical component
    /// </summary>
    public Number Y { get; set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Vector2" /> class with the specified components.
    /// </summary>
    /// <param name="x"> The horizontal component. </param>
    /// <param name="y"> The vertical component. </param>
    public Vector2(Number x, Number y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    ///     The distance from the origin to this point
    /// </summary>
    public Number GetMagnitude()
    {
        Number yConverted = Y.Convert(X.Unit);
        return new Number(
            Math.Sqrt(X.NumericValue * X.NumericValue + yConverted.NumericValue * yConverted.NumericValue), X.Unit);
    }
}

/// <summary>
///     A 3D quantity with both direction and magnitude
/// </summary>
public class Vector3 : IVector
{
    /// <summary>
    ///     The horizontal component
    /// </summary>
    public Number X { get; set; }

    /// <summary>
    ///     The vertical component
    /// </summary>
    public Number Y { get; set; }

    /// <summary>
    ///     The depth component
    /// </summary>
    public Number Z;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Vector3" /> class with the specified components.
    /// </summary>
    /// <param name="x"> The horizontal component. </param>
    /// <param name="y"> The vertical component. </param>
    /// <param name="z"> The depth component. </param>
    public Vector3(Number x, Number y, Number z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    ///     Converts this Vector3 to a Vector2
    /// </summary>
    public Vector2 Xy()
    {
        return new Vector2(X, Y);
    }

    /// <summary>
    ///     The distance from the origin to this point
    /// </summary>
    public Number GetMagnitude()
    {
        Number yConverted = Y.Convert(X.Unit);
        Number zConverted = Z.Convert(X.Unit);
        return new Number(Math.Sqrt(
            X.NumericValue * X.NumericValue +
            (yConverted.NumericValue * yConverted.NumericValue +
             zConverted.NumericValue * zConverted.NumericValue
            )), X.Unit);
    }
}