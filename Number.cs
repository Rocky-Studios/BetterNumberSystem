using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BetterNumberSystem
{
    /// <summary>
    /// The universal class for all measurements in the Better Number System
    /// </summary>
    public class Number
    {
        // =========== CLASS VARIABLES ===========

        /// <summary>
        /// The numerical value of the number (in base 10)
        /// </summary>
        public double NumericValue = 0;

        /// <summary>
        /// The measurement category
        /// </summary>
        public MeasurementType MeasurementType = MeasurementType.Plain;

        /// <summary>
        /// Converts a string of number data into the Number class
        /// </summary>
        /// <param name="numberData">The data of the number, example: 10cm Length b10 (numberUnit Type bBase)
        /// </param>
        public static Number Parse(string numberData)
        {
            if (numberData.Length == 0)
            {
                throw new ArgumentException("Please provide a valid number data string.");
            }
            string pattern = @"^(-?\d+(\.\d+)?)(\S*)\s\S*\sb([2-9]|\d{2,})$";
            Match match = Regex.Match(numberData, pattern);
            if (!match.Success)
            {
                throw new ArgumentException("Please provide a valid number data string.");
            }
            // Correct format all good!
            Number output = new Number();

            double numericValue = System.Convert.ToDouble(match.Groups[1].Value);
            output.NumericValue = numericValue;

            int @base = System.Convert.ToInt32(match.Groups[4].Value);
            Console.WriteLine("base" + @base);

            return output;
        }

        /// <summary>
        /// Returns the number in full format
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return "Number; numericValue: " + NumericValue.ToString() + ", base: ";
        }
    }
    /// <summary>
    /// The different categories that a number can be grouped into
    /// </summary>
    /// <example>MeasurementType.Length</example>
    public enum MeasurementType
    {
        /// <summary>
        /// A number with no unit
        /// </summary>
        Plain,

        /// <summary>
        /// A number representing length in space
        /// </summary>
        Length,

        /// <summary>
        /// A number representing area on a surface
        /// </summary>
        Area,

        /// <summary>
        /// A number representing a quantity of 3D space
        /// </summary>
        Volume,

        /// <summary>
        /// A number representing distance travelled over time
        /// </summary>
        Speed,

        /// <summary>
        /// A number representing the temperature of particles
        /// </summary>
        Temperature,

        /// <summary>
        /// A number representing the angle between two lines or surfaces
        /// </summary>
        Angle,

        /// <summary>
        /// A number representing an object's total mass
        /// </summary>
        Mass,

        /// <summary>
        /// An object representing the average mass per a unit of space
        /// </summary>
        Density,

        /// <summary>
        /// A number representing a force acting on an object
        /// </summary>
        Force
    }
}
