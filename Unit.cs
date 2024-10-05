namespace BetterNumberSystem;

/// <summary>
///     A unit of measurement
/// </summary>
public class Unit : Constant
{
    #region Fields
    /// <summary>
    /// What quantity this unit measures
    /// </summary>
    public Quantity Quantity;

    /// <summary>
    ///     This unit expressed in terms of the base SI Units
    /// </summary>
    public PronumeralCollection? UnitAsBaseUnits;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initialises a number unit
    /// </summary>
    /// <param name="name"> The full name </param>
    /// <param name="symbol"> The suffix after a number </param>
    /// <param name="quantity"> Which category of measurement it goes into </param>
    /// <param name="unitAsBaseUnits"> The unit expressed in terms of the base SI Units </param>
    public Unit(string name, string symbol, Quantity quantity, PronumeralCollection unitAsBaseUnits = null
        )
        : base(name, symbol, new Term(new Number(1d), unitAsBaseUnits))
    {
        Name = name;
        Symbol = symbol;
        Quantity = quantity;
        if (unitAsBaseUnits is null) UnitAsBaseUnits = [(this, 1)];
        base.Value.Pronumerals = unitAsBaseUnits;
        UnitAsBaseUnits = unitAsBaseUnits;
        PronumeralManager.Pronumerals.Add(symbol, this);
    }

    #endregion

    /// <summary>
    ///     Query for a numberUnit by its full name
    ///     <br /> Use UnitManager.Unit["XXXXXX"] if possible
    /// </summary>
    /// <param name="fullName"> </param>
    /// <returns> </returns>
    public static Unit? GetNumberUnitByFullName(string fullName)
    {
        return PronumeralManager.Pronumerals.First(unit => unit.Value.Name == fullName).Value as Unit;
    }

    /// <summary>
    ///     Query for a numberUnit by its symbol
    /// </summary>
    /// <param name="symbol"> </param>
    /// <returns> </returns>
    public static Unit? GetNumberUnitBySymbol(string symbol)
    {
        return PronumeralManager.Pronumerals.First(unit => unit.Value.Symbol == symbol).Value as Unit;
    }
}