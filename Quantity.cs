namespace BetterNumberSystem;


/// <summary>
///    A property that can be quantified by measurement.
/// </summary>
public enum Quantity
{
    /// <summary>
    ///     A quantity that is either the amount of something or the number of items in a set or a ratio between two other quantities.
    /// </summary>
    Plain,

    #region Base quantities
    /// <summary>
    ///   The length of the shortest straight path between two points.
    /// </summary>
    Length,

    /// <summary>
    ///     The amount of matter in an object.
    /// </summary>
    Mass,
    
    /// <summary>
    ///  The duration of an event or the duration of the gap between two events.
    /// </summary>
    Time,

    /// <summary>
    ///  The quantity of electron flow through a substance.
    /// </summary>
    ElectricCurrent,

    /// <summary>
    ///  The kinetic vibrations of particles in a substance.
    /// </summary>
    Temperature,
    
    /// <summary>
    ///  How much of something there is.
    /// </summary>
    AmountOfSubstance,

    /// <summary>
    /// How intense light is.
    /// </summary>
    LuminousIntensity,
    #endregion
    
    #region Derived quantities
    /// <summary>
    /// The rate at which something happens.
    /// </summary>
    Frequency,
    /// <summary>
    /// A geometric concept formed by two rays that share a common endpoint.
    /// </summary>
    Angle,
    /// <summary>
    /// A three-dimensional measure of the extent of an object as viewed from a point, quantified in steradians and representing the area on the surface of a sphere subtended by that angle.
    /// </summary>
    SolidAngle,
    /// <summary>
    /// An influence to an object's velocity
    /// </summary>
    Force,
    /// <summary>
    /// The extent of gravitational force acted upon an object
    /// </summary>
    Weight,
    /// <summary>
    /// A force applied perpendicular to a surface
    /// </summary>
    Pressure,
    /// <summary>
    /// The forces present during deformation
    /// </summary>
    Stress,
    /// <summary>
    /// Energy is the capacity to do work or produce change.
    /// </summary>
    Energy,
    /// <summary>
    /// Thermal energy that is transferred between substances due to a temperature difference.
    /// </summary>
    Heat,
    /// <summary>
    /// Power is the rate at which energy is transferred, converted, or used.
    /// </summary>
    Power,
    /// <summary>
    /// The total amount of radiant energy emitted, transmitted, or received per unit time, and represents the power of electromagnetic radiation, including visible light.
    /// </summary>
    RadiantFlux,
    /// <summary>
    /// The property of matter that causes it to experience a force when placed in an electromagnetic field.
    /// </summary>
    ElectricCharge,
    /// <summary>
    /// The potential electric difference between two points in a circuit.
    /// </summary>
    Voltage,
    /// <summary>
    /// The capacity of a device to store electric charge.
    /// </summary>
    ElectricCapacitance,
    /// <summary>
    /// The opposition to the flow of electric current.
    /// </summary>
    ElectricResistance,
    /// <summary>
    /// The ease with which an electric current can flow through a substance.
    /// </summary>
    ElectricConductance,
    /// <summary>
    /// The surface integral of the normal component of the magnetic field over a surface.
    /// </summary>
    MagneticFlux,
    /// <summary>
    /// The strength and direction of a magnetic field at a specific point.
    /// </summary>
    MagneticFluxDensity,
    /// <summary>
    /// A conductor's ability to store magnetic energy in response to an electric current.
    /// </summary>
    MagneticInductance,
    /// <summary>
    /// The tendency of an electrical conductor to oppose a change in the electric current flowing through it.
    /// </summary>
    ElectricalInductance,
    /// <summary>
    /// The luminous energy emitted over time.
    /// </summary>
    LuminousFlux,
    /// <summary>
    /// The total luminous flux over a surface.
    /// </summary>
    Illuminance,
    /// <summary>
    /// The average rate at which a radioactive substance undergoes decay
    /// </summary>
    NuclearRadioactivity,
    /// <summary>
    /// The amount of energy absorbed by a substance per unit mass.
    /// </summary>
    AbsorbedDose,
    /// <summary>
    /// The probability of ionizing radiation-induced cancer and/or genetic damage.
    /// </summary>
    EquivalentDose,
    /// <summary>
    /// The increase to the rate of a chemical reaction when a catalyst is present.
    /// </summary>
    CatalyticActivity,
    #endregion
}