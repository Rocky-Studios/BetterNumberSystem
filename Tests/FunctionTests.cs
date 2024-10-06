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
        Assert.AreEqual(12, (Function.Sum.Process([t1, t2])[0].Coefficient as Number)!.Value);
    }

    /// <summary>
    ///     Test if the conversion method updates the units correctly
    /// </summary>
    [Test]
    public void SumTestWithUnits()
    {
        Term t1 = new(new Number(10), ["Centi", "metre"]);
        Term t2 = new(new Number(2), ["Centi", "metre"]);
        Assert.AreEqual("12 cm", Function.Sum.Process([t1, t2])[0].ToString());
    }

    /// <summary>
    ///     Test if the conversion method updates the units correctly
    /// </summary>
    [Test]
    public void SumTestWithDifferentUnits()
    {
        Term t1 = new(new Number(10));
        Term t2 = new(new Number(2), ["Centi", "metre"]);
        Assert.AreEqual("10, 2 cm", Term.ToString(Function.Sum.Process([t1, t2])));
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
        Assert.AreEqual(8, (Function.Difference.Process([t1, t2])[0].Coefficient as Number)!.Value);
    }

    /// <summary>
    ///     Test if the conversion method updates the units correctly
    /// </summary>
    [Test]
    public void DifferenceTestWithUnits()
    {
        Term t1 = new(new Number(10), ["Centi", "metre"]);
        Term t2 = new(new Number(2), ["Centi", "metre"]);
        Assert.AreEqual("8 cm", Function.Difference.Process([t1, t2])[0].ToString());
    }
    #endregion
}