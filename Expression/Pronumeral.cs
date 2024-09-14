using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterNumberSystem.Expression
{
    public class Pronumeral
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public IExpressionValue Value { get; set; }

        public static Pronumeral NO_PRONUMERAL = new Pronumeral()
        {
            Symbol = "",
            Name = "",
            Value = null
        };
    }

    public class PronumeralListEqualityComparer : IEqualityComparer<List<Pronumeral>>
    {
        public bool Equals(List<Pronumeral> x, List<Pronumeral> y)
        {
            if (x == null || y == null)
                return x == y;

            return x.Count == y.Count && !x.Except(y).Any();
        }

        public int GetHashCode(List<Pronumeral> obj)
        {
            // Use a combination of pronumeral hashes to generate a hash code for the list
            unchecked
            {
                return obj.Aggregate(0, (current, pronumeral) => current * 397 ^ (pronumeral?.GetHashCode() ?? 0));
            }
        }
    }

    public class Variable : Pronumeral
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public IExpressionValue Value { get; set; }
    }

    public class Constant : Pronumeral
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public IExpressionValue Value { get; set; }
        /// <summary>
        /// A circle's circumference divided by its diameter
        /// </summary>
        public static Constant PI = new Constant()
        {
            Name = "pi",
            Symbol = "π",
            Value = new Number(MathF.PI)
        };
        /// <summary>
        /// The base of the natural logarithm
        /// </summary>
        public static Constant E = new Constant()
        {
            Name = "e",
            Symbol = "e",
            Value = new Number(MathF.E)
        };
    }
}
