using BetterNumberSystem.Expression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterNumberSystem
{
    /// <summary>
    /// A quantity with both direction and magnitude
    /// </summary>
    public interface IVector
    {
        /// <summary>
        /// The horizontal component
        /// </summary>
        public Number X { get; set; }
        /// <summary>
        /// The vertical component
        /// </summary>
        public Number Y { get; set; }

        /// <summary>
        /// The distance from the origin to this point
        /// </summary>
        public virtual Number GetMagnitude()
        {
            Number YConverted = Y.Convert(X.Unit);
            return new Number(Math.Sqrt(X.NumericValue * X.NumericValue + YConverted.NumericValue * YConverted.NumericValue), X.Unit);
        }
    }
    /// <summary>
    /// A 2D quantity with both direction and magnitude
    /// </summary>
    public class Vector2 : IVector
    {
        /// <summary>
        /// The horizontal component
        /// </summary>
        public Number X { get; set; }
        /// <summary>
        /// The vertical component
        /// </summary>
        public Number Y { get; set; }
        public Vector2(Number x, Number y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// The distance from the origin to this point
        /// </summary>
        public Number GetMagnitude()
        {
            Number YConverted = Y.Convert(X.Unit);
            return new Number(Math.Sqrt(X.NumericValue * X.NumericValue + YConverted.NumericValue * YConverted.NumericValue), X.Unit);
        }
    }
    /// <summary>
    /// A 3D quantity with both direction and magnitude
    /// </summary>
    public class Vector3 : IVector
    {
        /// <summary>
        /// The horizontal component
        /// </summary>
        public Number X { get; set; }
        /// <summary>
        /// The vertical component
        /// </summary>
        public Number Y { get; set; }
        /// <summary>
        /// The depth component
        /// </summary>
        public Number Z;

        public Vector3(Number x, Number y, Number z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Converts this Vector3 to a Vector2
        /// </summary>
        public Vector2 XY() { return new Vector2(X, Y); }

        /// <summary>
        /// The distance from the origin to this point
        /// </summary>
        public Number GetMagnitude()
        {
            Number YConverted = Y.Convert(X.Unit);
            Number ZConverted = Z.Convert(X.Unit);
            return new Number(Math.Sqrt(
                X.NumericValue * X.NumericValue +
                (YConverted.NumericValue * YConverted.NumericValue +
                ZConverted.NumericValue * ZConverted.NumericValue
                )), X.Unit);
        }
    }
}
