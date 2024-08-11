using System;
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
        public double NumericValue = 0;

        /// <summary>
        /// The measurement category
        /// </summary>
        public MeasurementType MeasurementType = MeasurementType.Plain;

        /// <summary>
        /// The measurement unit
        /// </summary>
        public NumberUnit Unit = NumberUnit.PLAIN;

        /// <summary>
        /// Generates a plain number (0)
        /// </summary>
        public Number()
        {
            NumericValue = 0;
            MeasurementType = MeasurementType.Plain;
            Unit = NumberUnit.PLAIN;
        }
        /// <summary>
        /// Generates a number with custom parameters
        /// </summary>
        /// <param name="numericValue">The numerical value of the number</param>
        /// <param name="measurementType">The category of measurement</param>
        /// <param name="unit">The unit of measurement</param>
        public Number(double numericValue = 0, NumberUnit? unit = null, MeasurementType? measurementType = MeasurementType.Plain)
        {
            if (unit == null) Unit = NumberUnit.PLAIN;
            else Unit = unit;

            if (numericValue < 0 && !Unit.CanBeNegative) throw new ArgumentException(Unit.FullName + " cannot be negative");
            else NumericValue = numericValue;
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
            double numericValue = System.Convert.ToDouble(match.Groups[1].Value);
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
        public Number Convert(NumberUnit unit)
        {
            if(MeasurementType != (unit as NumberUnit).MeasurementType)
            {
                throw new ArgumentException("Cannot implicitly convert " + MeasurementType + " to " + Unit.MeasurementType + ". Did you intend to use a math function?");
            }
            double scale = unit.ProportionToBaseUnit / (Unit as NumberUnit).ProportionToBaseUnit;
            return new Number((NumericValue) * scale, unit, MeasurementType);
        }

        private static string ToScientificNotation(double number)
        {
            int exponent = (int)Math.Floor(Math.Log10(Math.Abs((double)number)));
            double mantissa = (double)number / Math.Pow(10, exponent);
            string mantissaStr = mantissa.ToString("R");
            int doubleIndex = mantissaStr.IndexOf('.');
            if (doubleIndex != -1 && mantissaStr.Length - doubleIndex - 1 > 10)
            {
                mantissaStr = mantissaStr.Substring(0, doubleIndex + 11);
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
        /// Whether two objects (have to be numbers) have equal value
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Number)
                return (obj as Number) == this;
            else return false;
        }

        /// <summary>
        /// Returns the hash code for the value of this instance
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (MeasurementType.GetHashCode() + NumericValue.GetHashCode() - Unit.GetHashCode()) * (MeasurementType.GetHashCode() - NumericValue.GetHashCode() + Unit.GetHashCode());
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
        /// <summary>
        /// Whether number a is greater than number b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(Number a, Number b)
        {
            if (a.MeasurementType != b.MeasurementType)
            {
                return false;
            }
            Number bConverted = b.Convert(a.Unit);
            return a.NumericValue > bConverted.NumericValue;
        }
        /// <summary>
        /// Whether number a is less than number b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <(Number a, Number b)
        {
            if (a.MeasurementType != b.MeasurementType)
            {
                return false;
            }
            Number bConverted = b.Convert(a.Unit);
            return a.NumericValue < bConverted.NumericValue;
        }
        /// <summary>
        /// Whether number a is greater than or equal to number b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >=(Number a, Number b)
        {
            if (a.MeasurementType != b.MeasurementType)
            {
                return false;
            }
            Number bConverted = b.Convert(a.Unit);
            return a.NumericValue >= bConverted.NumericValue;
        }
        /// <summary>
        /// Whether number a is less than or equal to number b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <=(Number a, Number b)
        {
            if (a.MeasurementType != b.MeasurementType)
            {
                return false;
            }
            Number bConverted = b.Convert(a.Unit);
            return a.NumericValue <= bConverted.NumericValue;
        }
        /// <summary>
        /// Adds two numbers, giving the output in terms of the first number
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Number operator + (Number a, Number b)
        {
            if (a.MeasurementType != b.MeasurementType)
            {
                throw new ArgumentException("Cannot add numbers with different measurement types");
            }
            Number bConverted = b.Convert(a.Unit);
            if ((double)(a.NumericValue + bConverted.NumericValue) < 0 && !a.Unit.CanBeNegative) throw new Exception(a.Unit.FullName + " cannot be negative");
            return new Number(
                (double)(a.NumericValue + bConverted.NumericValue),
                a.Unit,
                a.MeasurementType
                );
        }
        /// <summary>
        /// Subtracts two numbers, giving the output in terms of the first number
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Number operator -(Number a, Number b)
        {
            if (a.MeasurementType != b.MeasurementType)
            {
                throw new ArgumentException("Cannot subtract numbers with different measurement types");
            }
            Number bConverted = b.Convert(a.Unit);
            if ((double)(a.NumericValue - bConverted.NumericValue) < 0 && !a.Unit.CanBeNegative) throw new Exception(a.Unit.FullName + " cannot be negative");
            return new Number(
                (double)(a.NumericValue - bConverted.NumericValue),
                a.Unit,
                a.MeasurementType
                );
        }

        public static Number operator *(Number a, Number b)
        {
            switch (a.MeasurementType)
            {
                case MeasurementType.Length:
                    if(b.MeasurementType == MeasurementType.Length)
                    {
                        return new Number(
                            (float)a.NumericValue * (float)b.Convert(a.Unit).NumericValue,
                            NumberUnit.GetNumberUnitByFullName("Sq" + a.Unit.FullName),
                            MeasurementType.Area
                            );
                    }
                    else if(b.MeasurementType == MeasurementType.Area)
                    {
                        return new Number(
                            (float)a.NumericValue * (float)b.Convert(a.Unit).NumericValue,
                            NumberUnit.GetNumberUnitByFullName("Cu" + a.Unit.FullName),
                            MeasurementType.Volume
                            );
                    }
                    break;
                case MeasurementType.Area:
                    if (b.MeasurementType == MeasurementType.Area)
                    {
                        throw new Exception();
                    }
                    else if (b.MeasurementType == MeasurementType.Length)
                    {
                        return new Number(
                            (float)(a.NumericValue * a.NumericValue) * ((float)b.Convert(NumberUnit.GetNumberUnitByFullName(a.Unit.FullName.Replace("Sq", ""))).NumericValue),
                            NumberUnit.GetNumberUnitByFullName("Cu" + a.Unit.FullName.Replace("Sq", "")),
                            MeasurementType.Volume
                            );
                    }
                    break;
            }
            throw new NotImplementedException();
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
        /// <summary>
        /// A number representing an amount of energy
        /// </summary>
        Energy,
        /// <summary>
        /// The amount of times something happens, usually over a fixed amount of time
        /// </summary>
        Frequency,
        /// <summary>
        /// How much force something is pushing something else by
        /// </summary>
        Pressure,
        /// <summary>
        /// 
        /// </summary>
        Current,
        /// <summary>
        /// How fast work is done, or how fast energy is transferred from object to another
        /// </summary>
        Power,
        /// <summary>
        /// The force making electrical charge move
        /// </summary>
        Voltage,
        /// <summary>
        /// The capability of a deice to store electric charge
        /// </summary>
        Capacitance,
        /// <summary>
        /// The difficulty of passing an electric current through a substance (opposite of conductance)
        /// </summary>
        Resistance,
        /// <summary>
        /// The ease of an electric current passing through a substance (opposite of resistance)
        /// </summary>
        Conductance,
        /// <summary>
        /// The strength of a field created by a magnet
        /// </summary>
        MagneticFieldStrength,
        /// <summary>
        /// 
        /// </summary>
        Inductance,
        /// <summary>
        /// How bright a light is
        /// </summary>
        Illuminance,
        /// <summary>
        /// 
        /// </summary>
        Radioactivity,

    }
}