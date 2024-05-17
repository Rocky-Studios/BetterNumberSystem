﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RockyStudios.BetterNumberSystem
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
        public MeasurementUnit MeasurementUnit = MeasurementUnit.Plain;

        /// <summary>
        /// Generates a plain number (0)
        /// </summary>
        public Number()
        {
            NumericValue = 0;
            MeasurementType = MeasurementType.Plain;
            MeasurementUnit = MeasurementUnit.Plain;
        }
        /// <summary>
        /// Generates a number with custom parameters
        /// </summary>
        /// <param name="numericValue">The numerical value of the number</param>
        /// <param name="measurementType">The category of measurement</param>
        /// <param name="measurementUnit">The unit of measurement</param>
        public Number(double numericValue, MeasurementType measurementType, MeasurementUnit measurementUnit)
        {
            NumericValue =(decimal)numericValue;
            MeasurementType = measurementType;
            MeasurementUnit = measurementUnit;
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

            // Measurement unit
            MeasurementUnit parsedMeasurementUnit;
            bool measurementUnitParseResult = Enum.TryParse<MeasurementUnit>(match.Groups[3].Value, out parsedMeasurementUnit);
            if (!measurementUnitParseResult) throw new ArgumentException("Number data string had invalid measurement unit.");
            output.MeasurementUnit = parsedMeasurementUnit;

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
                    outputString += MeasurementUnit.ToString();
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
                    outputString += MeasurementUnit.ToString();
                }
                if (type)
                {
                    outputString += " " + MeasurementType.ToString();
                }
            }
            return outputString;
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
        Force
    }

    /// <summary>
    /// The different units a measurement can have
    /// </summary>
    public enum MeasurementUnit
    {
        /// <summary>
        /// No unit
        /// </summary>
        Plain,
        /// <summary>
        /// Millimetres (mm)
        /// </summary>
        Millimetre,
        /// <summary>
        /// Millimetres (mm)
        /// </summary>
        Centimetre,
        /// <summary>
        /// Metre (m)
        /// </summary>
        Metre,
        /// <summary>
        /// Kilometres (km)
        /// </summary>
        Kilometre,
        /// <summary>
        /// Square Millimetres (mm²)
        /// </summary>
        SqMillimetre,
        /// <summary>
        /// Square Centimetres (cm²)
        /// </summary>
        SqCentimetre,
        /// <summary>
        /// Square Metres (m²)
        /// </summary>
        SqMetre,
        /// <summary>
        /// Square Kilometres (km²)
        /// </summary>
        SqKilometre,
        /// <summary>
        /// Hectares (ha)
        /// </summary>
        Hectare
    }
}
