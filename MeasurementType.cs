using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterNumberSystem
{
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
        /// 
        /// </summary>
        ElectricCharge,
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
        /// 
        /// </summary>
        MagneticFlux,
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
        /// <summary>
        /// How much there is of something
        /// </summary>
        AmountOfSubstance,
        /// <summary>
        /// How much light is emitted by a source
        /// </summary>
        LuminousIntensity,
    }
}
