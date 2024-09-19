using BetterNumberSystem.Expression;
using NUnit.Framework;

namespace BetterNumberSystem.Tests;

/// <summary>
/// Tests for <see cref="BetterNumberSystem.Expression.Pronumeral"/>s and related classes
/// </summary>
public class PronumeralTests
{
    /// <summary>
    /// Testing if constants are correctly defined and are correctly converted to strings
    /// </summary>
    [Test]
    public void ValueTests()
    {
        Assert.AreEqual(Constant.HYPERFINE_TRANSITION_FREQUENCY_OF_CAESIUM.ToString(), "9192631770hz");
        Assert.AreEqual(Constant.SPEED_OF_LIGHT.ToString(), "299792458ms^-1");
        Assert.AreEqual(Constant.PLANCK_CONSTANT.ToString(), "6.62607015E-34Jhz^-1");
        Assert.AreEqual(Constant.ELEMENTARY_CHARGE.ToString(), "1.602176634E-19C");
        Assert.AreEqual(Constant.BOLTZMANN_CONSTANT.ToString(), "1.380649E-23JK^-1");
        Assert.AreEqual(Constant.AVOGADRO_CONSTANT.ToString(), "6.02214076E+23mol^-1");
        Assert.AreEqual(Constant.LUMINOUS_EFFICACY.ToString(), "683lmW^-1");
    }
}