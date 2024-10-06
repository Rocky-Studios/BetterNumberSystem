using NUnit.Framework;

namespace BetterNumberSystem.Tests;

/// <summary>
///     Tests for the <see cref="BetterNumberSystem.Function" /> class
/// </summary>
public class FunctionTests
{
    #region Sum Function

    /// <summary>
    ///     Test if the conversion method updates the units correctly
    /// </summary>
    [Test]
    public void SumTest()
    {
        Term t1 = new(new Number(10));
        Term t2 = new(new Number(2));
        Assert.AreEqual(12, ((t1 + t2)[0].Coefficient as Number)!.Value);
    }

    /// <summary>
    ///     Test if the conversion method updates the units correctly
    /// </summary>
    [Test]
    public void SumTestWithUnits()
    {
        Term t1 = new(new Number(10), ["Centi", "metre"]);
        Term t2 = new(new Number(2), ["Centi", "metre"]);
        Assert.AreEqual("12 cm", (t1 + t2)[0].ToString());
    }

    /// <summary>
    ///     Test if the conversion method updates the units correctly
    /// </summary>
    [Test]
    public void SumTestWithDifferentUnits()
    {
        Term t1 = new(new Number(10));
        Term t2 = new(new Number(2), ["Centi", "metre"]);
        Assert.AreEqual("10, 2 cm", Term.ToString(t1 + t2));
    }

    #endregion

    #region Difference Function

    /// <summary>
    ///     Test if the conversion method updates the units correctly
    /// </summary>
    [Test]
    public void DifferenceTest()
    {
        Term t1 = new(new Number(10));
        Term t2 = new(new Number(2));
        Assert.AreEqual(8, ((t1 - t2)[0].Coefficient as Number)!.Value);
    }

    /// <summary>
    ///     Test if the conversion method updates the units correctly
    /// </summary>
    [Test]
    public void DifferenceTestWithUnits()
    {
        Term t1 = new(new Number(10), ["Centi", "metre"]);
        Term t2 = new(new Number(2), ["Centi", "metre"]);
        Assert.AreEqual("8 cm", (t1 - t2)[0].ToString());
    }

    #endregion

    /// <summary>
    ///     Test if plain numbers are multiplied correctly
    /// </summary>
    [Test]
    public void ProductTest()
    {
        Term t1 = new(new Number(10));
        Term t2 = new(new Number(2));
        Assert.AreEqual("20", (t1 * t2)[0].ToString());
    }

    /// <summary>
    ///     Test if numbers with units are multiplied correctly
    /// </summary>
    [Test]
    public void AreaProductTest()
    {
        Term t1 = new(new Number(10), "metre");
        Term t2 = new(new Number(2), "metre");
        Assert.AreEqual("20 m^2", (t1 * t2)[0].ToString());
    }

    #region Trig Functions

    /// <summary>
    ///     Test if the conversion method updates the units correctly
    /// </summary>
    [Test]
    public void SineTest()
    {
        Term t1 = new(new Number(1), ["radian"]);
        Assert.AreEqual(Math.Sin(1), (Function.Sine.Process([t1])[0].Coefficient as Number)!.Value);
    }

    /// <summary>
    ///     Test if the conversion method updates the units correctly
    /// </summary>
    [Test]
    public void CosineTest()
    {
        Term t1 = new(new Number(1), ["radian"]);
        Assert.AreEqual(Math.Cos(1), (Function.Cosine.Process([t1])[0].Coefficient as Number)!.Value);
    }

    #endregion
}