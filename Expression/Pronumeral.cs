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
        public ExpressionTerm Value { get; set; }

        public static Pronumeral NO_PRONUMERAL = new Pronumeral()
        {
            Symbol = "",
            Name = "",
            Value = null
        };

        public static List<Pronumeral> Pronumerals = [];
        
        public override string ToString()
        {
            return $"{Value}";
        }
    }

    public class PronumeralListEqualityComparer : IEqualityComparer<List<(Pronumeral, int)>>
    {
        public bool Equals(List<(Pronumeral, int)> x, List<(Pronumeral, int)> y)
        {
            if (x == null || y == null)
                return x == y;

            return x.Count == y.Count && !x.Except(y).Any();
        }

        public int GetHashCode(List<(Pronumeral, int)> obj)
        {
            // Use a combination of pronumeral hashes to generate a hash code for the list
            unchecked
            {
                return obj.Aggregate(0, (current, pronumeral) => current * 397 ^ (pronumeral.Item1?.GetHashCode() ?? 0));
            }
        }
    }

    public class Variable : Pronumeral
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public ExpressionTerm Value { get; set; }

        public override string ToString()
        {
            return $"{Value}";
        }
    }

    public class Constant : Pronumeral
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public ExpressionTerm Value { get; set; }
        
        public override string ToString()
        {
            return $"{Value}";
        }
        
        #region Mathematical Constants
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
        /// <summary>
        /// The imaginary unit, such that i^2 = -1
        /// </summary>
        public static Constant I = new Constant()
        {
            Name = "i",
            Symbol = "i",
            Value = new ExpressionTerm()
            { 
                Value = new Number(0),
                Pronumerals = [
                    (Pronumeral.NO_PRONUMERAL, 0)]
            }
        };
        
        /// <summary>
        /// The length of a regular pentagon's diagonal is its side times the golden ratio
        /// </summary>
        public static Constant GOLDEN_RATIO = new Constant()
        {
            Name = "Golden Ratio",
            Symbol = "φ",
            Value = new Number((1 + MathF.Sqrt(5)) / 2)
        };
        #endregion

        #region SI Defining Constants
        // https://en.wikipedia.org/wiki/International_System_of_Units#SI_defining_constants
        /// <summary>
        ///     The hyperfine transition frequency of caesium-133
        /// </summary>
        public static Constant HYPERFINE_TRANSITION_FREQUENCY_OF_CAESIUM = new()
        {
            Name = "Hyperfine Transition Frequency of Cs-133",
            Symbol = "ΔνCs",
            Value = new ExpressionTerm()
            {
                Value = new Number(9192631770),
                Pronumerals = [
                    (Pronumeral.Pronumerals.Find(P => P.Symbol == "hz"), 1)]
            }
        };
        /// <summary>
        ///     The speed of light in a vacuum
        /// </summary>
        public static Constant SPEED_OF_LIGHT = new()
        {
            Name = "Speed of Light",
            Symbol = "c",
            Value = new ExpressionTerm()
            {
                Value = new Number(299792458),
                Pronumerals = [
                    (Pronumeral.Pronumerals.Find(P => P.Symbol == "m"), 1),
                    (Pronumeral.Pronumerals.Find(P => P.Symbol == "s"), -1)]
            }
        };
        /// <summary>
        ///     The ground state hyperfine splitting frequency of caesium-133
        /// </summary>
        public static Constant PLANCK_CONSTANT = new()
        {
            Name = "Planck Constant",
            Symbol = "h",
            Value = new ExpressionTerm()
            {
                Value = new Number(6.62607015e-34),
                Pronumerals = [
                    (Pronumeral.Pronumerals.Find(P => P.Symbol == "J"), 1),
                    (Pronumeral.Pronumerals.Find(P => P.Symbol == "hz"), -1)]
            }
        };
        /// <summary>
        ///     The charge of a single electron
        /// </summary>
        public static Constant ELEMENTARY_CHARGE = new()
        {
            Name = "Elementary Charge",
            Symbol = "e",
            Value = new ExpressionTerm()
            {
                Value = new Number(1.602176634e-19),
                Pronumerals = [
                    (Pronumeral.Pronumerals.Find(P => P.Symbol == "C"), 1)]
            }
        };
        /// <summary>
        ///     The mass of a single electron
        /// </summary>
        public static Constant BOLTZMANN_CONSTANT = new()
        {
            Name = "Boltzmann Constant",
            Symbol = "k",
            Value = new ExpressionTerm()
            {
                Value = new Number(1.380649e-23),
                Pronumerals = [
                    (Pronumeral.Pronumerals.Find(P => P.Symbol == "J"), 1),
                    (Pronumeral.Pronumerals.Find(P => P.Symbol == "K"), -1)]
            }
        };
        /// <summary>
        ///     The number of constituent particles per mole
        /// </summary>
        public static Constant AVOGADRO_CONSTANT = new()
        {
            Name = "Avogadro Constant",
            Symbol = "NA",
            Value = new ExpressionTerm()
            {
                Value = new Number(6.02214076e+23),
                Pronumerals = [
                    (Pronumeral.Pronumerals.Find(P => P.Symbol == "mol"), -1)]
            }
        };
        /// <summary>
        ///     The luminous efficacy of monochromatic radiation of frequency 540 THz
        /// </summary>
        public static Constant LUMINOUS_EFFICACY = new()
        {
            Name = "Luminous Efficacy",
            Symbol = "Kcd",
            Value = new ExpressionTerm()
            {
                Value = new Number(683 ),
                Pronumerals = [
                    (Pronumeral.Pronumerals.Find(P => P.Symbol == "lm"), 1),
                    (Pronumeral.Pronumerals.Find(P => P.Symbol == "W"), -1)]
            }
        };

        #endregion

    }
}
