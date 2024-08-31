using BetterNumberSystem.Expression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterNumberSystem
{
    public interface IVector : IExpressionValue
    {
        public Number X { get; set; }
        public Number Y { get; set; }

        public virtual Number GetMagnitude()
        {
            Number YConverted = Y.Convert(X.Unit);
            return new Number(Math.Sqrt(X.NumericValue * X.NumericValue + YConverted.NumericValue * YConverted.NumericValue), X.Unit);
        }
    }

    public class Vector2 : IVector
    {
        public Number X { get; set; }
        public Number Y { get; set; }

        public Vector2(Number x, Number y)
        {
            X = x;
            Y = y;
        }

        public Number GetMagnitude()
        {
            Number YConverted = Y.Convert(X.Unit);
            return new Number(Math.Sqrt(X.NumericValue * X.NumericValue + YConverted.NumericValue * YConverted.NumericValue), X.Unit);
        }
    }

    public class Vector3 : IVector
    {
        public Number X { get; set; }
        public Number Y { get; set; }
        public Number Z;

        public Vector3(Number x, Number y, Number z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector2 XY() { return new Vector2(X, Y); }

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
