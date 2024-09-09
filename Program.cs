using BetterNumberSystem.Expression;

namespace BetterNumberSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Number myNum = new Number(
                15,
                UnitManager.Unit["Millimetre"]
                );

            Expression.Expression exp = new(FunctionManager.Get("Sum", [new ExpressionGroup() { Parts = [
                new ExpressionTerm()
                {
                    Value = myNum,
                    Pronumerals = [Pronumeral.NO_PRONUMERAL]
                },
                new ExpressionTerm()
                {
                    Value = myNum,
                    Pronumerals = [Pronumeral.NO_PRONUMERAL]
                }
                ]}]));
            Console.WriteLine(exp.Evaluate());
        }
    }
}