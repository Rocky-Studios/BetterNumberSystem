using NUnit.Framework;
using BetterNumberSystem;
using BetterNumberSystem.Expression;

namespace BetterNumberSystem.Tests;
/// <summary>
/// Tests regarding expressions and math
/// </summary>
public class ExpressionTests
{
    /// <summary>
    /// Tests whether adding numbers works correctly
    /// </summary>
    [Test]
    public void NumberSumTest()
    {
        Number numA = new(1.05);
        Number numB = new(2.0);
        Expression.Expression expression = numA + numB;
        Assert.AreEqual(expression.Evaluate().ToString(), "3.05");
    }
    
    /// <summary>
    /// Tests whether adding numbers with units works correctly
    /// </summary>
    [Test]
    public void NumberSumTestWithUnits()
    {
        Number numA = new(1.05, UnitManager.Units["Metre"]);
        Number numB = new(2.0, UnitManager.Units["Centimetre"]);
        Expression.Expression expression = numA + numB;
        Assert.AreEqual(expression.Evaluate().ToString(), "1.07m");
    }
    
    /// <summary>
    /// Tests whether subtracting numbers works correctly
    /// </summary>
    [Test]
    public void NumberDiffTest()
    {
        Number numA = new(1.05);
        Number numB = new(0.2);
        Expression.Expression expression = numA - numB;
        Assert.AreEqual(expression.Evaluate().ToString(), "0.85");
    }
    
    /// <summary>
    /// Tests whether subtracting numbers with units works correctly
    /// </summary>
    [Test]
    public void NumberDiffTestWithUnits()
    {
        Number numA = new(1.05, UnitManager.Units["Metre"]);
        Number numB = new(2, UnitManager.Units["Centimetre"]);
        Expression.Expression expression = numA - numB;
        Assert.AreEqual(expression.Evaluate().ToString(), "1.03m");
    }
    

    /// <summary>
    /// Tests the cosine function
    /// </summary>
    [Test]
    public void CosineTest()
    {
        Number angle = new(1, UnitManager.Units["Radian"]);
        Expression.Expression expression = new(FunctionManager.Get("Cosine", [new ExpressionGroup(angle)]));
        Assert.AreEqual((expression.Evaluate()[[Pronumeral.NO_PRONUMERAL]][0].Value as Number).NumericValue, Math.Cos(1));
    }
}