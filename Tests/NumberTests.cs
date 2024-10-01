using NUnit.Framework;

namespace BetterNumberSystem.Tests;

/// <summary>
///     Tests for the <see cref="BetterNumberSystem.Number" /> class
/// </summary>
public class NumberTests
{
    /// <summary>
    ///     Test if the conversion method updates the units correctly
    /// </summary>
    [Test]
    public void NumericConvertTest()
    {
        Number number = new(1.05, UnitManager.Units["Metre"]);
        Assert.AreEqual(number.Convert("Millimetre").NumericValue, 1050);
    }

    /// <summary>
    ///     Test if the number is converted to a string correctly
    /// </summary>
    [Test]
    public void TextConvertTest()
    {
        Number number = new(1.05, UnitManager.Units["Metre"]);
        Assert.AreEqual(number.Convert("Millimetre").ToString(), "1050mm");
    }

    /// <summary>
    ///     Test if the conversion from Celsius to Kelvin is correct
    /// </summary>
    [Test]
    public void CelsiusToKelvinTest()
    {
        Number number = new(25, UnitManager.Units["Celsius"]);
        Assert.AreEqual(number.Convert("Kelvin").NumericValue, 298.15);
    }

    /// <summary>
    ///     Test if the number is parsed correctly
    /// </summary>
    [Test]
    public void ParseTest()
    {
        Number number = Number.Parse("1.05m");
        Assert.AreEqual(number.ToString(), "1.05m");
    }

    /// <summary>
    ///     Test whether converting the number to a string and then parsing it back results in the same number
    /// </summary>
    [Test]
    public void ReverseParseTest()
    {
        Number number = new(1.05, UnitManager.Units["Metre"]);
        Assert.AreEqual(Number.Parse(number.ToString()), number);
    }
}