using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterNumberSystem.Expression
{
    public interface IPronumeral
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public IExpressionValue Value { get; set; }
    }

    public class Variable : IPronumeral
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public IExpressionValue Value { get; set; }
    }

    public class Constant : IPronumeral
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
