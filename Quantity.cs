namespace BetterNumberSystem;

/// <summary>
///     A property that can be quantified by measurement.
/// </summary>
public enum Quantity
{
    /// <summary>
    ///     A quantity that is either the amount of something or the number of items in a set or a ratio between two other
    ///     quantities.
    /// </summary>
    Plain,

    #region Base quantities

    /// <summary>
    ///     The length of the shortest straight path between two points.
    /// </summary>
    Length,

    /// <summary>
    ///     The amount of matter in an object.
    /// </summary>
    Mass,

    /// <summary>
    ///     The duration of an event or the duration of the gap between two events.
    /// </summary>
    Time,

    /// <summary>
    ///     The quantity of electron flow through a substance.
    /// </summary>
    ElectricCurrent,

    /// <summary>
    ///     The kinetic vibrations of particles in a substance.
    /// </summary>
    Temperature,

    /// <summary>
    ///     How much of something there is.
    /// </summary>
    AmountOfSubstance,

    /// <summary>
    ///     How intense light is.
    /// </summary>
    LuminousIntensity,

    #endregion

    #region Derived quantities

    /// <summary>
    ///     The rate at which something happens.
    /// </summary>
    Frequency,

    /// <summary>
    ///     A geometric concept formed by two rays that share a common endpoint.
    /// </summary>
    Angle,

    /// <summary>
    ///     A three-dimensional measure of the extent of an object as viewed from a point, quantified in steradians and
    ///     representing the area on the surface of a sphere subtended by that angle.
    /// </summary>
    SolidAngle,

    /// <summary>
    ///     An influence to an object's velocity
    /// </summary>
    Force,

    /// <summary>
    ///     The extent of gravitational force acted upon an object
    /// </summary>
    Weight,

    /// <summary>
    ///     A force applied perpendicular to a surface
    /// </summary>
    Pressure,

    /// <summary>
    ///     The forces present during deformation
    /// </summary>
    Stress,

    /// <summary>
    ///     Energy is the capacity to do work or produce change.
    /// </summary>
    Energy,

    /// <summary>
    ///     Thermal energy that is transferred between substances due to a temperature difference.
    /// </summary>
    Heat,

    /// <summary>
    ///     Power is the rate at which energy is transferred, converted, or used.
    /// </summary>
    Power,

    /// <summary>
    ///     The total amount of radiant energy emitted, transmitted, or received per unit time, and represents the power of
    ///     electromagnetic radiation, including visible light.
    /// </summary>
    RadiantFlux,

    /// <summary>
    ///     The property of matter that causes it to experience a force when placed in an electromagnetic field.
    /// </summary>
    ElectricCharge,

    /// <summary>
    ///     The potential electric difference between two points in a circuit.
    /// </summary>
    Voltage,

    /// <summary>
    ///     The capacity of a device to store electric charge.
    /// </summary>
    ElectricCapacitance,

    /// <summary>
    ///     The opposition to the flow of electric current.
    /// </summary>
    ElectricResistance,

    /// <summary>
    ///     The ease with which an electric current can flow through a substance.
    /// </summary>
    ElectricConductance,

    /// <summary>
    ///     The surface integral of the normal component of the magnetic field over a surface.
    /// </summary>
    MagneticFlux,

    /// <summary>
    ///     The strength and direction of a magnetic field at a specific point.
    /// </summary>
    MagneticFluxDensity,

    /// <summary>
    ///     A conductor's ability to store magnetic energy in response to an electric current.
    /// </summary>
    MagneticInductance,

    /// <summary>
    ///     The tendency of an electrical conductor to oppose a change in the electric current flowing through it.
    /// </summary>
    ElectricalInductance,

    /// <summary>
    ///     The luminous energy emitted over time.
    /// </summary>
    LuminousFlux,

    /// <summary>
    ///     The total luminous flux over a surface.
    /// </summary>
    Illuminance,

    /// <summary>
    ///     The average rate at which a radioactive substance undergoes decay
    /// </summary>
    NuclearRadioactivity,

    /// <summary>
    ///     The amount of energy absorbed by a substance per unit mass.
    /// </summary>
    AbsorbedDose,

    /// <summary>
    ///     The probability of ionizing radiation-induced cancer and/or genetic damage.
    /// </summary>
    EquivalentDose,

    /// <summary>
    ///     The increase to the rate of a chemical reaction when a catalyst is present.
    /// </summary>
    CatalyticActivity,

    // Kinematics
    /// <summary>
    ///     The rate of change of position of an object.
    /// </summary>
    Velocity,

    /// <summary>
    ///     The rate of change of velocity of an object.
    /// </summary>
    Acceleration,

    /// <summary>
    ///     The rate of change of acceleration of an object.
    /// </summary>
    Jerk,

    /// <summary>
    ///     The rate of change of jerk of an object.
    /// </summary>
    Jounce,

    /// <summary>
    ///     The angular rate of change of position of an object.
    /// </summary>
    AngularVelocity,

    /// <summary>
    ///     The angular rate of change of velocity of an object.
    /// </summary>
    AngularAcceleration,

    /// <summary>
    ///     The offset of an oscillator from its nominal frequency.
    /// </summary>
    FrequencyDrift,

    /// <summary>
    ///     The volume of fluid passing through a point in a system over time.
    /// </summary>
    VolumetricFlowRate,

    // Mechanics
    /// <summary>
    ///     The size of a surface.
    /// </summary>
    Area,

    /// <summary>
    ///     The amount of space an object occupies.
    /// </summary>
    Volume,

    /// <summary>
    ///     The product of the mass and velocity of an object.
    /// </summary>
    Momentum,

    /// <summary>
    ///     The rotational analog of linear momentum.
    /// </summary>
    AngularMomentum,

    /// <summary>
    ///     The rotational analog of linear force.
    /// </summary>
    Torque,

    /// <summary>
    ///     The rate of change of force.
    /// </summary>
    Yank,

    /// <summary>
    ///     The spatial frequency of a wave.
    /// </summary>
    Wavenumber,

    /// <summary>
    ///     A substance's mass per unit volume.
    /// </summary>
    Density,

    /// <summary>
    ///     A substance's mass per unit area.
    /// </summary>
    AreaDensity,

    /// <summary>
    ///     A substance's energy per unit volume.
    /// </summary>
    EnergyDensity,

    /// <summary>
    ///     The measure of a quantity of any characteristic value per unit of length.
    /// </summary>
    LinearMassDensity,

    /// <summary>
    ///     The rate of the transfer of energy through a surface.
    /// </summary>
    EnergyFluxDensity,

    /// <summary>
    ///     The volume of a substance per unit mass.
    /// </summary>
    SpecificVolume,

    /// <summary>
    ///     The energy of a substance per unit mass.
    /// </summary>
    SpecificEnergy,

    /// <summary>
    ///     The tendency of a fluid to shrink to its minimum surface area.
    /// </summary>
    SurfaceTension,

    /// <summary>
    ///     How the balance of kinetic versus potential energy of a physical system changes with trajectory.
    /// </summary>
    Action,

    /// <summary>
    ///     The radiant flux emitted, reflected, transmitted, or received by a surface or volume.
    /// </summary>
    Radiance,

    /// <summary>
    ///     The radiant flux received by a surface or volume.
    /// </summary>
    Irradiance,

    /// <summary>
    ///     A fluid's resistance to deformation, in velocity.
    /// </summary>
    KinematicViscosity,

    /// <summary>
    ///     A fluid's resistance to deformation, in force.
    /// </summary>
    DynamicViscosity,

    /// <summary>
    ///     The rate at which a substance's mass changes over time.
    /// </summary>
    MassFlowRate,

    /// <summary>
    ///     The radiant flux per frequency.
    /// </summary>
    SpectralPower,

    /// <summary>
    ///     The rate at which absorbed dose is delivered.
    /// </summary>
    AbsorbedDoseRate,

    /// <summary>
    ///     The efficiency of a fuel in producing energy.
    /// </summary>
    FuelEfficiency,

    /// <summary>
    ///     The amount of power per unit area.
    /// </summary>
    PowerDensity,

    /// <summary>
    ///     The measure of the relative volume change of a fluid or solid as a response to a pressure change.
    /// </summary>
    Compressibility,

    /// <summary>
    ///     The amount of radiant energy received by a surface.
    /// </summary>
    RadiantExposure,

    /// <summary>
    ///     The resistance of an object to changes in its rotational motion.
    /// </summary>
    MomentOfInertia,

    /// <summary>
    ///     The angular momentum per unit mass.
    /// </summary>
    SpecificAngularMomentum,

    /// <summary>
    ///     The power emitted by a source in the form of radiation.
    /// </summary>
    RadiantIntensity,

    /// <summary>
    ///     The power per unit area emitted by a source in the form of radiation.
    /// </summary>
    SpectralIntensity,

    // Chemistry

    /// <summary>
    ///     The concentration of a solution expressed as the number of moles of solute per liter of solution.
    /// </summary>
    Molarity,

    /// <summary>
    ///     The volume occupied by one mole of a substance.
    /// </summary>
    MolarVolume,

    /// <summary>
    ///     The amount of heat required to raise the temperature of one mole of a substance by one degree Celsius.
    /// </summary>
    MolarHeatCapacity,

    /// <summary>
    ///     The energy content of one mole of a substance.
    /// </summary>
    MolarEnergy,

    /// <summary>
    ///     The ability of a substance to conduct electricity.
    /// </summary>
    MolarConductivity,

    /// <summary>
    ///     The concentration of a solution expressed as the number of moles of solute per kilogram of solvent.
    /// </summary>
    Molality,

    /// <summary>
    ///     The mass of one mole of a substance.
    /// </summary>
    MolarMass,

    /// <summary>
    ///     The efficiency of a catalyst in increasing the rate of a chemical reaction.
    /// </summary>
    CatalyticEfficiency,

    // Electromagnetics

    /// <summary>
    ///     The electric displacement field in a material.
    /// </summary>
    ElectricDisplacementField,

    /// <summary>
    ///     The amount of electric charge per unit volume.
    /// </summary>
    ElectricChargeDensity,

    /// <summary>
    ///     The amount of electric current per unit area.
    /// </summary>
    ElectricCurrentDensity,

    /// <summary>
    ///     The ability of a material to conduct electric current.
    /// </summary>
    Conductivity,

    /// <summary>
    ///     The ability of a material to permit the passage of an electric field.
    /// </summary>
    Permittivity,

    /// <summary>
    ///     The ability of a material to support the formation of a magnetic field.
    /// </summary>
    Permeability,

    /// <summary>
    ///     The strength of an electric field at a point.
    /// </summary>
    ElectricFieldStrength,

    /// <summary>
    ///     The strength of a magnetic field at a point.
    /// </summary>
    MagneticFieldStrength,

    /// <summary>
    ///     The amount of exposure to an electric field.
    /// </summary>
    Exposure,

    /// <summary>
    ///     The resistance of a material to the flow of electric current.
    /// </summary>
    Resistivity,

    /// <summary>
    ///     The amount of electric charge per unit length.
    /// </summary>
    LinearChargeDensity,

    /// <summary>
    ///     The magnetic moment of a dipole.
    /// </summary>
    MagneticDipoleMoment,

    /// <summary>
    ///     The ability of an electron to move through a material.
    /// </summary>
    ElectronMobility,

    /// <summary>
    ///     The resistance of a material to the formation of a magnetic field.
    /// </summary>
    MagneticReluctance,

    /// <summary>
    ///     The vector potential of a magnetic field.
    /// </summary>
    MagneticVectorPotential,

    /// <summary>
    ///     The magnetic moment of a material.
    /// </summary>
    MagneticMoment,

    /// <summary>
    ///     The rigidity of a magnetic field.
    /// </summary>
    MagneticRigidity,

    /// <summary>
    ///     The force that drives a magnetic field.
    /// </summary>
    MagnetomotiveForce,

    /// <summary>
    ///     The susceptibility of a material to become magnetized.
    /// </summary>
    MagneticSusceptibility,

    // Photometry

    /// <summary>
    ///     The total luminous energy emitted by a source.
    /// </summary>
    LuminousEnergy,

    /// <summary>
    ///     The amount of luminous energy received by a surface.
    /// </summary>
    LuminousExposure,

    /// <summary>
    ///     The amount of luminous flux per unit area.
    /// </summary>
    Luminance,

    /// <summary>
    ///     The efficiency of a light source in converting energy to visible light.
    /// </summary>
    LuminousEfficacy,

    // Thermodynamics

    /// <summary>
    ///     The amount of heat required to change the temperature of an object.
    /// </summary>
    HeatCapacity,

    /// <summary>
    ///     The amount of heat required to change the temperature of a unit mass of a substance.
    /// </summary>
    SpecificHeatCapacity,

    /// <summary>
    ///     The ability of a material to conduct heat.
    /// </summary>
    ThermalConductivity,

    /// <summary>
    ///     The resistance of a material to the flow of heat.
    /// </summary>
    ThermalResistance,

    /// <summary>
    ///     The rate at which a material expands with temperature.
    /// </summary>
    ThermalExpansionCoefficient,

    /// <summary>
    ///     The rate of change of temperature with distance.
    /// </summary>
    TemperatureGradient,

    #endregion
}