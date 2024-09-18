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
        public IExpressionValue Value { get; set; }
    }

    public class Constant : Pronumeral
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public IExpressionValue Value { get; set; }

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
            Value = new Number(9192631770)
        };
        /// <summary>
        ///     The speed of light in a vacuum
        /// </summary>
        public static Constant SPEED_OF_LIGHT = new()
        {
            Name = "Speed of Light",
            Symbol = "c",
            Value = new Number(299792458)
        };
        /// <summary>
        ///     The ground state hyperfine splitting frequency of caesium-133
        /// </summary>
        public static Constant PLANCK_CONSTANT = new()
        {
            Name = "Planck Constant",
            Symbol = "h",
            Value = new Number(6.62607015e-34)
        };
        /// <summary>
        ///     The charge of a single electron
        /// </summary>
        public static Constant ELEMENTARY_CHARGE = new()
        {
            Name = "Elementary Charge",
            Symbol = "e",
            Value = new Number(1.602176634e-19)
        };
        /// <summary>
        ///     The mass of a single electron
        /// </summary>
        public static Constant BOLTZMANN_CONSTANT = new()
        {
            Name = "Boltzmann Constant",
            Symbol = "k",
            Value = new Number(1.380649e-23)
        };
        /// <summary>
        ///     The number of constituent particles per mole
        /// </summary>
        public static Constant AVOGADRO_CONSTANT = new()
        {
            Name = "Avogadro Constant",
            Symbol = "NA",
            Value = new Number(6.02214076e23)
        };
        /// <summary>
        ///     The luminous efficacy of monochromatic radiation of frequency 540 THz
        /// </summary>
        public static Constant LUMINOUS_EFFICACY = new()
        {
            Name = "Luminous Efficacy",
            Symbol = "Kcd",
            Value = new Number(683)
        };

        #endregion

    }
}
