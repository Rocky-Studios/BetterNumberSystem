namespace BetterNumberSystem;

/// <summary>
///     The different categories that a number can be grouped into
/// </summary>
public enum Quantity
{
    /// <summary>
    ///     A number with no unit
    /// </summary>
    Plain,

    /// <summary>
    ///     A number representing length in space
    /// </summary>
    Length,

    /// <summary>
    ///     A number representing area on a surface
    /// </summary>
    Area,

    /// <summary>
    ///     A number representing a quantity of 3D space
    /// </summary>
    Volume,

    /// <summary>
    ///     A number representing distance travelled over time
    /// </summary>
    Speed,

    /// <summary>
    ///     A number representing the temperature of particles
    /// </summary>
    Temperature,

    /// <summary>
    ///     A number representing the angle between two lines or surfaces
    /// </summary>
    Angle,

    /// <summary>
    ///     A number representing an object's total mass
    /// </summary>
    Mass,

    /// <summary>
    ///     An object representing the average mass per a unit of space
    /// </summary>
    Density,

    /// <summary>
    ///     A number representing a force acting on an object
    /// </summary>
    Force,

    /// <summary>
    ///     A number representing an amount of time
    /// </summary>
    Time,

    /// <summary>
    ///     A number representing an amount of energy
    /// </summary>
    Energy,

    /// <summary>
    ///     The amount of times something happens, usually over a fixed amount of time
    /// </summary>
    Frequency,

    /// <summary>
    ///     How much force something is pushing something else by
    /// </summary>
    Pressure,

    /// <summary>
    /// </summary>
    Current,

    /// <summary>
    ///     How fast work is done, or how fast energy is transferred from object to another
    /// </summary>
    Power,

    /// <summary>
    /// </summary>
    ElectricCharge,

    /// <summary>
    ///     The force making electrical charge move
    /// </summary>
    Voltage,

    /// <summary>
    ///     The capability of a deice to store electric charge
    /// </summary>
    Capacitance,

    /// <summary>
    ///     The difficulty of passing an electric current through a substance (opposite of conductance)
    /// </summary>
    Resistance,

    /// <summary>
    ///     The ease of an electric current passing through a substance (opposite of resistance)
    /// </summary>
    Conductance,

    /// <summary>
    /// </summary>
    MagneticFlux,

    /// <summary>
    ///     The strength of a field created by a magnet
    /// </summary>
    MagneticFieldStrength,

    /// <summary>
    /// </summary>
    Inductance,

    /// <summary>
    ///     How bright a light is
    /// </summary>
    Illuminance,

    /// <summary>
    /// </summary>
    Radioactivity,

    /// <summary>
    ///     How much there is of something
    /// </summary>
    AmountOfSubstance,

    /// <summary>
    ///     How much light is emitted by a source
    /// </summary>
    LuminousIntensity,

    /// <summary>
    ///     The flow of charged particles
    /// </summary>
    ElectricCurrent,

    /// <summary>
    ///     The speed of an object in a certain direction
    /// </summary>
    Velocity,

    /// <summary>
    ///     The rate of change of velocity
    /// </summary>
    Acceleration,

    /// <summary>
    /// </summary>
    WaveNumber,

    /// <summary>
    ///     The amount of a substance in a certain area
    /// </summary>
    SurfaceDensity,

    /// <summary>
    ///     The amount of a current in a certain area
    /// </summary>
    CurrentDensity,

    /// <summary>
    ///     The concentration of particles in a volume
    /// </summary>
    Concentration,

    /// <summary>
    ///     The concentration of mass in a volume
    /// </summary>
    MassConcentration,

    /// <summary>
    ///     The concentration of light in an area
    /// </summary>
    Luminance,

    /// <summary>
    ///     A fluid's resistance to deformation
    /// </summary>
    DynamicViscosity,

    /// <summary>
    ///     Measure of force about an axis
    /// </summary>
    Torque,

    /// <summary>
    ///     The tendency of a fluid to shrink to its minimum surface area
    /// </summary>
    SurfaceTension,

    /// <summary>
    ///     How angular position changes over time
    /// </summary>
    AngularVelocity,

    /// <summary>
    ///     The rate of change of angular velocity
    /// </summary>
    AngularAcceleration,

    /// <summary>
    ///     Energy input to an area
    /// </summary>
    HeatFluxDensity,

    /// <summary>
    ///     The amount of heat energy needed to raise the temperature of a substance
    /// </summary>
    HeatCapacity,

    /// <summary>
    ///     The amount of heat energy needed to raise the temperature of a substance per unit mass
    /// </summary>
    SpecificHeatCapacity,

    /// <summary>
    ///     Energy per unit mass
    /// </summary>
    SpecificEnergy,

    /// <summary>
    ///     A material's ability to conduct heat
    /// </summary>
    ThermalConductivity,

    /// <summary>
    ///     The amount of energy per volume
    /// </summary>
    EnergyDensity,

    /// <summary>
    ///     The strength of the field around charged particles
    /// </summary>
    ElectricFieldStrength,

    /// <summary>
    ///     The amount of electric charge per volume
    /// </summary>
    ElectricChargeDensity,

    /// <summary>
    ///     The electromagnetic effects of polarisation
    /// </summary>
    ElectricFluxDensity,

    /// <summary>
    ///     The electric polarizability of a material
    /// </summary>
    Permittivity,

    /// <summary>
    ///     The magnetization produced by a magnetic field
    /// </summary>
    Permeability,

    /// <summary>
    ///     The amount of energy per amount of substance
    /// </summary>
    MolarEnergy,

    /// <summary>
    ///     The amount of heat energy needed to raise the temperature of a substance per mole
    /// </summary>
    MolarHeatCapacity,

    /// <summary>
    ///     Measure of exposure to ionizing radiation
    /// </summary>
    Exposure,

    /// <summary>
    ///     The amount of ionizing radiation absorbed by a material
    /// </summary>
    AbsorbedDose,

    /// <summary>
    ///     The radiant flux emitted, reflected, transmitted or received by a surface per unit solid angle
    /// </summary>
    RadiantIntensity,

    /// <summary>
    ///     The radiant flux emitted, reflected, transmitted or received per surface
    /// </summary>
    Radiance,

    /// <summary>
    ///     Increase of rate of a chemical reaction due to a catalyst
    /// </summary>
    CatalyticActivity,
    /// <summary>
    ///     
    /// </summary>
    SolidAngle,
}