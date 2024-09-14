using NUnit.Framework;

namespace BetterNumberSystem.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    
    [Test]
    public void NumericConvertTest()
    {
        Number number = new Number(1.05, UnitManager.Units["Metre"]);
        Assert.AreEqual(number.Convert("Millimetre").NumericValue, 1050);
    }
    
    [Test]
    public void TextConvertTest()
    {
        Number number = new Number(1.05, UnitManager.Units["Metre"]);
        Assert.AreEqual(number.Convert("Millimetre").ToString(), "1050mm");
    }
    
    [Test]
    public void ParseTest()
    {
        Number number = Number.Parse("1.05m");
        Assert.AreEqual(number.ToString(), "1.05m");
    }
}