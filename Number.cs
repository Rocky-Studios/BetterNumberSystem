using System;
using System.Text.RegularExpressions;
using BetterNumberSystem.Expression;

namespace BetterNumberSystem
{
    /// <summary>
    /// The universal class for all measurements in the Better Number System
    /// </summary>
    public class Number : IExpressionValue
    {
        #region Fields

        /// <summary>
        /// The numerical value of the number
        /// </summary>
        public double NumericValue = 0;

        /// <summary>
        /// The measurement category
        /// </summary>
        public Quantity Quantity = Quantity.Plain;

        /// <summary>
        /// The measurement unit
        /// </summary>
        public Unit Unit = UnitManager.Unit["Plain"];

        #endregion

        #region Constructors

        /// <summary>
        /// Generates a plain number (0)
        /// </summary>
        public Number()
        {
            NumericValue = 0;
            Quantity = Quantity.Plain;
            Unit = UnitManager.Unit["Plain"];
        }

        /// <summary>
        /// Generates a number with custom parameters
        /// </summary>
        /// <param name="numericValue">The numerical value of the number</param>
        /// <param name="measurementType">The category of measurement</param>
        /// <param name="unit">The unit of measurement</param>
        public Number(double numericValue = 0, Unit? unit = null, Quantity? measurementType = null)
        {
            Unit = unit ?? UnitManager.Unit["Plain"];
            Quantity = measurementType ?? Unit.Quantity;
            if (numericValue < 0 && !Unit.CanBeNegative)
                throw new ArgumentException(Unit.FullName + " cannot be negative");
            else NumericValue = numericValue;
        }

        #endregion

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

            string pattern = @"^(-?\d+(\.\d+)?)(\S*)";
            Match match = Regex.Match(numberData, pattern);
            if (!match.Success)
            {
                throw new ArgumentException("Please provide a valid number data string.");
            }

            Number output = new Number();

            // Numerical value
            double numericValue = System.Convert.ToDouble(match.Groups[1].Value);
            output.NumericValue = numericValue;

            // Unit
            Unit parsedNumberUnit = Unit.GetNumberUnitBySuffix(match.Groups[3].Value);
            if (parsedNumberUnit == null) throw new ArgumentException("Number data string had invalid unit.");
            output.Unit = parsedNumberUnit;

            output.Quantity = output.Unit.Quantity;

            return output;
        }

        /// <summary>
        /// Returns the number in full format
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return Get(unit: true);
        }

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
                    outputString += " " + Quantity.ToString();
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
                    outputString += " " + Quantity.ToString();
                }
            }

            return outputString;
        }

        #region Convert

        /// <summary>
        /// Converts a number to a different unit
        /// </summary>
        /// <param name="targetUnit">The unit to convert to</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Implicit conversion not possible</exception>
        public Number Convert(Unit targetUnit)
        {
            if (Quantity != (targetUnit as Unit).Quantity)
            {
                throw new ArgumentException("Cannot implicitly convert " + Quantity + " to " +
                                            targetUnit.Quantity + ". Did you intend to use a math function?");
            }

            double valueInBaseUnit = Unit.ToBaseUnit(NumericValue);

            // Convert from base unit to the target unit
            double convertedValue = targetUnit.FromBaseUnit(valueInBaseUnit);

            return new Number(convertedValue, targetUnit, targetUnit.Quantity);
        }

        /// <summary>
        /// Converts a number to a different unit
        /// </summary>
        /// <param name="unit">The string name of the unit</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Number Convert(string unit)
        {
            Unit targetUnit = UnitManager.Units[unit];
            if (Quantity != (targetUnit as Unit).Quantity)
            {
                throw new ArgumentException("Cannot implicitly convert " + Quantity + " to " +
                                            targetUnit.Quantity + ". Did you intend to use a math function?");
            }

            double valueInBaseUnit = Unit.ToBaseUnit(NumericValue);

            // Convert from base unit to the target unit
            double convertedValue = targetUnit.FromBaseUnit(valueInBaseUnit);

            return new Number(convertedValue, targetUnit, targetUnit.Quantity);
        }

        #endregion

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

        #region Logic

        /// <summary>
        /// Whether two numbers have the same value when their units are converted
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Number a, Number b)
        {
            if (a.Quantity != b.Quantity)
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
            return (Quantity.GetHashCode() + NumericValue.GetHashCode() - Unit.GetHashCode()) *
                   (Quantity.GetHashCode() - NumericValue.GetHashCode() + Unit.GetHashCode());
        }

        /// <summary>
        /// Whether two numbers have different values even when their units are converted
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Number a, Number b)
        {
            if (a.Quantity != b.Quantity)
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
            if (a.Quantity != b.Quantity)
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
            if (a.Quantity != b.Quantity)
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
            if (a.Quantity != b.Quantity)
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
            if (a.Quantity != b.Quantity)
            {
                return false;
            }

            Number bConverted = b.Convert(a.Unit);
            return a.NumericValue <= bConverted.NumericValue;
        }

        #endregion

        #region Math

        /// <summary>
        /// Adds two numbers, giving the output in terms of the first number
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Expression.Expression operator +(Number a, Number b)
        {
            return new Expression.Expression(FunctionManager.Get("Sum",
                [new ExpressionGroup(a), new ExpressionGroup(b)]));
        }

        /// <summary>
        /// Subtracts two numbers, giving the output in terms of the first number
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Expression.Expression operator -(Number a, Number b)
        {
            return new Expression.Expression(FunctionManager.Get("Difference",
                [new ExpressionGroup(a), new ExpressionGroup(b)]));
        }

        public static Number operator *(Number a, Number b)
        {
            switch (a.Quantity)
            {
                case Quantity.Length:
                    if (b.Quantity == Quantity.Length)
                    {
                        return new Number(
                            (float)a.NumericValue * (float)b.Convert(a.Unit).NumericValue,
                            Unit.GetNumberUnitByFullName("Sq" + a.Unit.FullName),
                            Quantity.Area
                        );
                    }
                    else if (b.Quantity == Quantity.Area)
                    {
                        return new Number(
                            (float)a.NumericValue * (float)b.Convert(a.Unit).NumericValue,
                            Unit.GetNumberUnitByFullName("Cu" + a.Unit.FullName),
                            Quantity.Volume
                        );
                    }

                    break;
                case Quantity.Area:
                    if (b.Quantity == Quantity.Area)
                    {
                        throw new Exception();
                    }
                    else if (b.Quantity == Quantity.Length)
                    {
                        return new Number(
                            (float)(a.NumericValue * a.NumericValue) * ((float)b
                                .Convert(Unit.GetNumberUnitByFullName(a.Unit.FullName.Replace("Sq", ""))).NumericValue),
                            Unit.GetNumberUnitByFullName("Cu" + a.Unit.FullName.Replace("Sq", "")),
                            Quantity.Volume
                        );
                    }

                    break;
            }

            throw new NotImplementedException();
        }

        #endregion

        public static implicit operator ExpressionTerm(Number value)
        {
            return new ExpressionTerm()
            {
                Pronumerals = [(Pronumeral.NO_PRONUMERAL, 1)],
                Value = value
            };
        }
    }
}