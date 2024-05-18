using System.Text.RegularExpressions;

namespace BetterNumberSystem
{
    /// <summary>
    /// The universal class for all measurements in the Better Number System
    /// </summary>
    public class Number
    {
        // =========== CLASS VARIABLES ===========

        /// <summary>
        /// The numerical value of the number
        /// </summary>
        public decimal NumericValue = 0;

        /// <summary>
        /// The measurement category
        /// </summary>
        public MeasurementType MeasurementType = MeasurementType.Plain;

        /// <summary>
        /// The measurement unit
        /// </summary>
        public INumberUnit Unit = NumberUnit.GetNumberUnitByFullName("Plain");

        /// <summary>
        /// Generates a plain number (0)
        /// </summary>
        public Number()
        {
            NumericValue = 0;
            MeasurementType = MeasurementType.Plain;
            Unit = NumberUnit.GetNumberUnitByFullName("Plain");
        }
        /// <summary>
        /// Generates a number with custom parameters
        /// </summary>
        /// <param name="numericValue">The numerical value of the number</param>
        /// <param name="measurementType">The category of measurement</param>
        /// <param name="unit">The unit of measurement</param>
        public Number(double numericValue, MeasurementType measurementType, INumberUnit unit)
        {
            NumericValue = (decimal)numericValue;
            MeasurementType = measurementType;
            Unit = unit;
        }


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
            string pattern = @"^(-?\d+(\.\d+)?)(\S*)\s(\S*)$";
            Match match = Regex.Match(numberData, pattern);
            if (!match.Success)
            {
                throw new ArgumentException("Please provide a valid number data string.");
            }
            Number output = new Number();

            // Numerical value
            decimal numericValue = System.Convert.ToDecimal(match.Groups[1].Value);
            output.NumericValue = numericValue;

            // Measurement type
            MeasurementType parsedMeasurementType;
            bool measurementTypeParseResult = Enum.TryParse<MeasurementType>(match.Groups[4].Value, out parsedMeasurementType);
            if (!measurementTypeParseResult) throw new ArgumentException("Number data string had invalid measurement type.");
            output.MeasurementType = parsedMeasurementType;

            // Unit
            NumberUnit parsedNumberUnit = NumberUnit.GetNumberUnitBySuffix(match.Groups[3].Value);
            if (parsedNumberUnit == null) throw new ArgumentException("Number data string had invalid unit.");
            output.Unit = parsedNumberUnit;

            return output;
        }

        /// <summary>
        /// Returns the number in full format
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return Get(unit: true, type: true);
        }

        // ========= GET METHODS ===========
        /// <summary>
        /// Display the number class as text
        /// </summary>
        /// <param name="numericValue">Where to include the plain number</param>
        /// <param name="unit">Whether to include the units</param>
        /// <param name="type">Whether to include the measurement type</param>
        /// <param name="scientific">Whether to display in scientific notation</param>
        /// <returns>The string version of the number according to the parameters</returns>
        public dynamic Get(bool numericValue = true, bool unit = false, bool type = false, bool scientific = false)
        {
            if (numericValue && !unit && !type && !scientific)
            {
                //Plain
                return NumericValue;
            }
            string outputString = "";
            if (!scientific)
            {
                if (numericValue)
                {
                    outputString += NumericValue;
                }
                if (unit)
                {
                    outputString += Unit.Suffix;
                }
                if (type)
                {
                    outputString += " " + MeasurementType.ToString();
                }
            }
            else
            {
                // Scientific
                outputString = ToScientificNotation(NumericValue);
                if (unit)
                {
                    outputString += Unit.ToString();
                }
                if (type)
                {
                    outputString += " " + MeasurementType.ToString();
                }
            }
            return outputString;
        }

        /// <summary>
        /// Converts a number to a different unit
        /// </summary>
        /// <param name="unit">The unit to convert to</param>
        /// <returns>The converted number</returns>
        /// <exception cref="ArgumentException">Implicit conversion not possible</exception>
        public Number Convert(INumberUnit unit)
        {
            if(MeasurementType != (unit as NumberUnit).MeasurementType)
            {
                throw new ArgumentException("Cannot implicitly convert " + MeasurementType + " to " + (unit as NumberUnit).MeasurementType + ". Did you intend to use a math function?");
            }
            double scale = (unit as NumberUnit).ProportionToBaseUnit / (Unit as NumberUnit).ProportionToBaseUnit;
            return new Number(((double)NumericValue) * scale, MeasurementType, unit);
        }

        private static string ToScientificNotation(decimal number)
        {
            int exponent = (int)Math.Floor(Math.Log10(Math.Abs((double)number)));
            double mantissa = (double)number / Math.Pow(10, exponent);
            string mantissaStr = mantissa.ToString("R");
            int decimalIndex = mantissaStr.IndexOf('.');
            if (decimalIndex != -1 && mantissaStr.Length - decimalIndex - 1 > 10)
            {
                mantissaStr = mantissaStr.Substring(0, decimalIndex + 11);
            }
            return $"{mantissaStr} x 10^{exponent}";
        }

        // ========= MATH METHODS ===========
        /// <summary>
        /// Whether two numbers have the same value when their units are converted
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator == (Number a, Number b)
        {
            if (a.MeasurementType != b.MeasurementType)
            {
                return false;
            }
            Number bConverted = b.Convert(a.Unit);
            return a.NumericValue == bConverted.NumericValue;
        }
        /// <summary>
        /// Whether two numbers have different values even when their units are converted
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Number a, Number b)
        {
            if (a.MeasurementType != b.MeasurementType)
            {
                return false;
            }
            Number bConverted = b.Convert(a.Unit);
            return a.NumericValue != bConverted.NumericValue;
        }
    }
    /// <summary>
    /// The different categories that a number can be grouped into
    /// </summary>
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
        Force,

        /// <summary>
        /// A number representing an amount of time
        /// </summary>
        Time,
    }
}
