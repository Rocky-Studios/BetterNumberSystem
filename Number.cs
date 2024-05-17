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
        /// The numerical value of the number (in base 10)
        /// </summary>
        public double NumericValue = 0;

        /// <summary>
        /// The measurement category
        /// </summary>
        public MeasurementType MeasurementType = MeasurementType.Plain;

        /// <summary>
        /// The measurement unit
        /// </summary>
        public MeasurementUnit MeasurementUnit = MeasurementUnit.Plain;

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
            if(!measurementTypeParseResult) throw new ArgumentException("Number data string had invalid measurement type.");
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
            return "==Number== \n" +
            "numericValue: " + NumericValue.ToString() + "\n" +
            "measurementType: " + MeasurementType.ToString() + "\n" +
            "measurementUnit: " + MeasurementUnit.ToString() + "\n";
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