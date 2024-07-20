namespace BetterNumberSystem
{
    /// <summary>
    /// A singular unit of measurement
    /// </summary>
    public class NumberUnit : INumberUnit
    {
        /// <summary>
        /// The full name of the unit eg. Millimetre
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// The plural full name of the unit eg. Millimetres
        /// </summary>
        public string FullNamePlural = "";
        /// <summary>
        /// The short suffix of the unit eg. mm
        /// </summary>
        public string Suffix { get; set; }
        /// <summary>
        /// Whether this unit is a base of its category eg. metres is the base for length
        /// </summary>
        public bool BaseUnit = false;
        /// <summary>
        /// The ratio of this unit to the base unit eg. for millimetres the base unit is metres so the proportion is 1/1000 or 0.001
        /// </summary>
        public double ProportionToBaseUnit = 1;
        /// <summary>
        /// Whether this unit can be negative
        /// </summary>
        public bool CanBeNegative { get; set; }
        public MeasurementType MeasurementType;
        /// <summary>
        /// Initialises a number unit
        /// </summary>
        /// <param name="fullName">The full name</param>
        /// <param name="suffix">The suffix after a number</param>
        /// <param name="measurementType">Which category of measurement it goes into</param>
        /// <param name="baseUnit">Whether this is the standard base unit used in conversions</param>
        /// <param name="proportionalToBaseUnit">How many base units equal one of these</param>
        /// <param name="canBeNegative">Whether this value can be negative</param>
        public NumberUnit(string fullName, string suffix, MeasurementType measurementType, bool baseUnit = false, double proportionalToBaseUnit = 1, bool canBeNegative = false)
        {
            FullName = fullName;
            FullNamePlural = fullName + "s";
            Suffix = suffix;
            BaseUnit = baseUnit;
            ProportionToBaseUnit = proportionalToBaseUnit;
            MeasurementType = measurementType;
            CanBeNegative = canBeNegative;
            if (_numberUnits == null) _numberUnits = new List<NumberUnit>();
            _numberUnits.Add(this);
        }
        /// <summary>
        /// A number with no unit
        /// </summary>
        public static readonly NumberUnit PLAIN = new NumberUnit("Plain", "", MeasurementType.Plain, baseUnit: true, canBeNegative: true);

        #region Length
        /// <summary>
        /// Millimetre (mm) <br/>
        /// 1000th of a metre
        /// </summary>
        public static readonly NumberUnit MILLIMETRE = new NumberUnit("Millimetre", "mm", MeasurementType.Length, proportionalToBaseUnit: 1f/1000f);
        /// <summary>
        /// Centimetre (cm) <br/>
        /// 100th of a metre
        /// </summary>
        public static readonly NumberUnit CENTIMETRE = new NumberUnit("Centimetre", "cm", MeasurementType.Length, proportionalToBaseUnit: 1f/100f);
        /// <summary>
        /// <strong> SI Base Unit </strong> Metre (m) <br/>
        /// The length of the path travelled by light in vacuum in ⁠1/299792458⁠ of a second
        /// </summary>
        public static readonly NumberUnit METRE = new NumberUnit("Metre", "m", MeasurementType.Length, baseUnit: true);
        /// <summary>
        /// Kilometre (Km) <br/>
        /// 1000 metres
        /// </summary>
        public static readonly NumberUnit KILOMETRE = new NumberUnit("Kilometre", "Km", MeasurementType.Length, proportionalToBaseUnit: 1000f);
        #endregion

        #region Area
        /// <summary>
        /// Square millimetre (mm²) <br/>
        /// The area of a square with edges one millimetre in length.
        /// </summary>
        public static readonly NumberUnit SQUARE_MILLIMETRE = new NumberUnit("SqMillimetre", "mm²", MeasurementType.Area, proportionalToBaseUnit: 1f/1000000f);
        /// <summary>
        /// Square centimetre (cm²) <br/>
        /// The area of a square with edges one centimetre in length.
        /// </summary>
        public static readonly NumberUnit SQUARE_CENTIMETRE = new NumberUnit("SqCentimetre", "cm²", MeasurementType.Area, proportionalToBaseUnit: 1f/10000f);
        /// <summary>
        /// <strong>SI Base Unit</strong> Square metre (m²) <br/>
        /// The area of a square with edges one metre in length.
        /// </summary>
        public static readonly NumberUnit SQUARE_METRE = new NumberUnit("SqMetre", "m²", MeasurementType.Area, baseUnit: true);
        /// <summary>
        /// Square kilometre (Km²) <br/>
        /// The area of a square with edges one kilometre in length.
        /// </summary>
        public static readonly NumberUnit SQUARE_KILOMETRE = new NumberUnit("SqKilometre", "Km²", MeasurementType.Area, proportionalToBaseUnit: 1000000f);
        /// <summary>
        /// Hectare (ha) <br/>
        /// The area of a square with edges 100 metres in length.
        /// </summary>
        public static readonly NumberUnit HECTARE = new NumberUnit("Hectare", "ha", MeasurementType.Area, proportionalToBaseUnit: 10000f);
        #endregion

        #region Volume
        /// <summary>
        /// Cubic millimetre (mm³) <br/>
        /// The volume of a cube with edges one millimetre in length.
        /// </summary>
        public static readonly NumberUnit CUBIC_MILLIMETRE = new NumberUnit("CuMillimetre", "mm³", MeasurementType.Volume, proportionalToBaseUnit: 1f/1000000000f);
        /// <summary>
        /// Cubic centimetre (cm³) <br/>
        /// The volume of a cube with edges one centimetre in length.
        /// </summary>
        public static readonly NumberUnit CUBIC_CENTIMETRE = new NumberUnit("CuCentimetre", "cm³", MeasurementType.Volume, proportionalToBaseUnit: 1f/1000000f);
        /// <summary>
        /// <strong>SI Base Unit</strong> Cubic metre (m³)
        /// The volume of a cube with edges one metre in length.
        /// </summary>
        public static readonly NumberUnit CUBIC_METRE = new NumberUnit("CuMetre", "m³", MeasurementType.Volume, baseUnit: true);
        /// <summary>
        /// Cubic kilometre (Km³) <br/>
        /// The volume of a cube with edges one kilometre in length.
        /// </summary>
        public static readonly NumberUnit CUBIC_KILOMETRE = new NumberUnit("CuKilometre", "Km³", MeasurementType.Volume, proportionalToBaseUnit: 1000000000f);
        /// <summary>
        /// Millilitre (mL)<br/>
        /// 1000 mL = 1 L
        /// </summary>
        public static readonly NumberUnit MILLILITRE = new NumberUnit("Millilitre", "mL", MeasurementType.Volume, proportionalToBaseUnit: 1f/1000000f);
        /// <summary>
        /// Litre (L)<br/>
        /// 1000 L = 1 m³
        /// </summary>
        public static readonly NumberUnit LITRE = new NumberUnit("Litre", "L", MeasurementType.Volume, proportionalToBaseUnit: 1f/1000f);
        /// <summary>
        /// Kilolitre (KL)<br/>
        /// KL = m³, 1 KL = 1000 L
        /// </summary>
        public static readonly NumberUnit KILOLITRE = new NumberUnit("Kilolitre", "KL", MeasurementType.Volume, proportionalToBaseUnit: 1f);
        #endregion

        #region Time
        /// <summary>
        /// Millisecond (ms) <br/>
        /// 1000th of a second
        /// </summary>
        public static readonly NumberUnit MILLISECOND = new NumberUnit("Millisecond", "ms", MeasurementType.Time, proportionalToBaseUnit: 1f/1000f);
        /// <summary>
        /// <strong>SI Base Unit</strong> Second (s) <br/>
        /// The second is defined by taking the fixed numerical value of the caesium frequency, ΔνCs, the unperturbed ground-state hyperfine transition frequency of the caesium 133 atom, to be 9192631770 when expressed in the unit Hz, which is equal to s−1.
        /// </summary>
        public static readonly NumberUnit SECOND = new NumberUnit("Second", "s", MeasurementType.Time, baseUnit: true);
        /// <summary>
        /// Minute (m) <br/>
        /// 60 seconds
        /// </summary>
        public static readonly NumberUnit MINUTE = new NumberUnit("Minute", "m", MeasurementType.Time, proportionalToBaseUnit: 60f);
        /// <summary>
        /// Hour (h) <br/>
        /// 60 minutes / 3600 seconds
        /// </summary>
        public static readonly NumberUnit HOUR = new NumberUnit("Hour", "h", MeasurementType.Time, proportionalToBaseUnit: 3600f);
        /// <summary>
        /// Day (d) <br/>
        /// 24 hours
        /// </summary>
        public static readonly NumberUnit DAY = new NumberUnit("Day", "d", MeasurementType.Time, proportionalToBaseUnit: 60f * 60f * 24f);
        /// <summary>
        /// Week (w) <br/>
        /// 7 days
        /// </summary>
        public static readonly NumberUnit WEEK = new NumberUnit("Week", "w", MeasurementType.Time, proportionalToBaseUnit: 60f * 60f * 24f * 7f);
        /// <summary>
        /// Year (y) <br/>
        /// <strong>NOTE: </strong> A year here is defined as 365 days
        /// </summary>
        public static readonly NumberUnit YEAR = new NumberUnit("Year", "y", MeasurementType.Time, proportionalToBaseUnit: 60f * 60f * 24f * 365f);
        /// <summary>
        /// Decade (dy) <br/>
        /// 10 years
        /// </summary>
        public static readonly NumberUnit DECADE = new NumberUnit("Decade", "dy", MeasurementType.Time, proportionalToBaseUnit: 60f * 60f * 24f * 365f * 10f);
        /// <summary>
        /// Century (cy) <br/>
        /// 100 years
        /// </summary>
        public static readonly NumberUnit CENTURY = new NumberUnit("Century", "dy", MeasurementType.Time, proportionalToBaseUnit: 60f * 60f * 24f * 365f * 100f);
        /// <summary>
        /// Millenium (my) <br/>
        /// 1000 years
        /// </summary>
        public static readonly NumberUnit MILLENIUM = new NumberUnit("Millenium", "my", MeasurementType.Time, proportionalToBaseUnit: 60f * 60f * 24f * 365f * 1000f);
        #endregion

        #region Mass
        /// <summary>
        /// Milligram (mg) <br/>
        /// 1000th of a gram
        /// </summary>
        public static readonly NumberUnit MILLIGRAM = new NumberUnit("Milligram", "mg", MeasurementType.Mass, proportionalToBaseUnit: 1f/1000f);
        /// <summary>
        /// Gram (g) <br/>
        /// 1000th of a kilogram
        /// </summary>
        public static readonly NumberUnit GRAM = new NumberUnit("Gram", "g", MeasurementType.Mass, baseUnit: true);
        /// <summary>
        /// <strong>SI Base Unit</strong> Kilogram (kg) <br/>
        /// The mass of 1 litre of pure water.
        /// </summary>
        public static readonly NumberUnit KILOGRAM = new NumberUnit("Kilogram", "Kg", MeasurementType.Mass, proportionalToBaseUnit: 1000f);
        /// <summary>
        /// Tonne (t) <br/>
        /// 1000 kilograms
        /// </summary>
        public static readonly NumberUnit TONNE = new NumberUnit("Tonne", "t", MeasurementType.Mass, proportionalToBaseUnit: 1000000f);
        #endregion

        #region Angle
        /// <summary>
        /// <strong>SI Base Unit</strong> Radian (rad) <br/>
        /// An arc of a circle with the same length as the radius of that circle subtends an angle of 1 radian. The circumference subtends an angle of 2π radians.
        /// </summary>
        public static readonly NumberUnit RADIAN = new NumberUnit("Radian", "rad", MeasurementType.Angle, baseUnit: true, canBeNegative: true);
        /// <summary>
        /// Degree of an arc (°) <br/>
        /// 1/360th of a full revolution around a circle
        /// </summary>
        public static readonly NumberUnit DEGREE = new NumberUnit("Degree", "°", MeasurementType.Angle, proportionalToBaseUnit: MathF.PI / 180, canBeNegative: true);
        #endregion

        #region Energy
        /// <summary>
        /// <strong>SI Base Unit</strong> Joule (J)<br/>
        /// The work required to produce one watt of power for one second, or one watt-second (W⋅s) (compare kilowatt-hour, which is 3.6 megajoules).
        /// </summary>
        public static readonly NumberUnit JOULE = new NumberUnit("Joule", "j", MeasurementType.Energy, baseUnit: true);
        /// <summary>
        /// Kilojoule (KJ)<br/>
        /// 1000 joules
        /// </summary>
        public static readonly NumberUnit KILOJOULE = new NumberUnit("Kilojoule", "Kj", MeasurementType.Energy, proportionalToBaseUnit: 1000f);
        /// <summary>
        /// Megajoule (MJ)<br/>
        /// 1000000 joules
        /// </summary>
        public static readonly NumberUnit MEGAJOULE = new NumberUnit("Megajoule", "Mj", MeasurementType.Energy, proportionalToBaseUnit: 1000000f);
        /// <summary>
        /// Calorie (cal)<br/>
        /// 1000th of a kilocalorie
        /// </summary>
        public static readonly NumberUnit CALORIE = new NumberUnit("Calorie", "cal", MeasurementType.Energy, proportionalToBaseUnit: 4.184f);
        /// <summary>
        /// Kilocalorie (kcal)<br/>
        /// The amount of (heat) energy needed to raise the temperature of one liter of water by one degree Celsius (or kelvin).
        /// </summary>
        public static readonly NumberUnit KILOCALORIE = new NumberUnit("Kilocalorie", "kcal", MeasurementType.Energy, proportionalToBaseUnit: 4184f);
        #endregion


        /// <summary>
        /// The list of all number units avaiable for use
        /// </summary>
        private static List<NumberUnit> _numberUnits = new();
        /// <summary>
        /// Gets all the currently available number units
        /// </summary>
        /// <returns></returns>
        public static List<NumberUnit> GetNumberUnits()
        {
            return _numberUnits;
        }
        /// <summary>
        /// Query for a numberUnit by its full name
        /// <br/> Use NumberUnit.XXXXXXXX if possible
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static NumberUnit? GetNumberUnitByFullName(string fullName)
        {
            foreach (NumberUnit numberUnit in _numberUnits)
            {
                if (fullName.Equals(numberUnit.FullName))
                {
                    return numberUnit;
                }
            }
            return null;
        }
        /// <summary>
        /// Query for a numberUnit by its suffix
        /// </summary>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static NumberUnit? GetNumberUnitBySuffix(string suffix)
        {
            foreach (NumberUnit numberUnit in _numberUnits)
            {
                if (suffix.Equals(numberUnit.Suffix))
                {
                    return numberUnit;
                }
            }
            return null;
        }
    }

    public interface INumberUnit
    {
        string Suffix { get; set; }
        bool CanBeNegative { get; set; }
        string FullName { get; set; }
    }
}
