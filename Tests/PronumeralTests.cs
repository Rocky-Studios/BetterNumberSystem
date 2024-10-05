using NUnit.Framework;

namespace BetterNumberSystem.Tests;

/// <summary>
/// Tests for <see cref="BetterNumberSystem.Pronumeral"/>s and related classes
/// </summary>
public class PronumeralTests
{
    /// <summary>
    /// Testing if pronumerals are correctly defined and are correctly converted to strings
    /// </summary>
    [Test]
    public void ValueTests()
    {
        Assert.AreEqual("9192631770 Hz", Constant.HyperfineTransitionFrequencyCs133.ToString());
        Assert.AreEqual("299792458 m/s", Constant.SpeedOfLight.ToString());
        Assert.AreEqual("6.62607015E-34 Js", Constant.PlanckConstant.ToString());
        Assert.AreEqual("1.602176634E-19 C", Constant.ElementaryCharge.ToString());
        Assert.AreEqual("1.380649E-23 J/K", Constant.BoltzmannConstant.ToString());
        Assert.AreEqual("6.02214076E+23 /mol", Constant.AvogadroConstant.ToString());
        Assert.AreEqual("683 lm/W", Constant.LuminousEfficacy.ToString());
        Assert.AreEqual("kgm^2/s^2", PronumeralManager.FindPronumeralBySymbol<Unit>("J").ToString());
    }
}