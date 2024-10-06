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
        Term t = new(new Number(10.5), ["Centi", "metre"]);
        t = t.Convert("milli");
        Assert.AreEqual(105, (t.Coefficient as Number)!.Value);
    }

    /// <summary>
    ///     Test if the number is converted to a string correctly
    /// </summary>
    [Test]
    public void TextConvertTest()
    {
        Term t = new(new Number(10.5), ["Centi", "metre"]);
        t = t.Convert("milli");
        Assert.AreEqual("105 mm", t.ToString());
    }

    /// <summary>
    ///     Test if the conversion from Celsius to Kelvin is correct
    /// </summary>
    [Test]
    public void CelsiusToKelvinTest()
    {
        Term t = new(new Number(25), ["Celsius"]);
        t = t.ConvertUnit((PronumeralManager.FindPronumeralBySymbol<Unit>("K") as Unit)!);
        Assert.AreEqual("298.15 K", t.ToString());
    }
    
    /// <summary>
    ///     Test if the conversion from Celsius to Kelvin is correct
    /// </summary>
    [Test]
    public void RadToDegreeTest()
    {
        Term t = new(new Number(Math.PI), ["radian"]);
        t = t.ConvertUnit((PronumeralManager.Pronumerals["°"] as Unit)!);
        Assert.AreEqual("180 °", t.ToString());
    }
}