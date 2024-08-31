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
            Console.WriteLine(myNum);
            ExpressionFunction add = new ExpressionFunction()
            {
                Name = "Sum",
                Symbol = "+",
                Function = inputs =>
                {
                    for (int i = 0; i < inputs.Length - 1; i++)
                    {
                        IExpressionValue current = inputs[i];
                        IExpressionValue next = inputs[i++];

                        if (current.GetType() != next.GetType()) throw new Exception("Different types!");
                    }
                    if (inputs[0] is not Number) throw new NotImplementedException();

                    Number result = new Number(0, (inputs[0] as Number).Unit);

                    foreach (IExpressionValue value in inputs)
                    {
                        result.NumericValue += (value as Number).Convert(result.Unit).NumericValue;
                    }

                    return result;
                }
            };

            Expression.Expression exp = new Expression.Expression();
            exp.Parts = [
                add,
                new ExpressionFunctionInputs()
                {
                    Inputs = [
                        new ExpressionFunctionInput("Value", new ExpressionGroup()
                        {
                            Parts = [new ExpressionTerm()
                            {
                                Value = myNum
                            }]
                        }),
                        new ExpressionFunctionInput("Value", new ExpressionGroup()
                        {
                            Parts = [new ExpressionTerm()
                            {
                                Value = myNum
                            }]
                        })
                    ],
                    InputType = new ExpressionFunctionInputAmount()
                    {
                        Infinite = true
                    }
                }
            ];
            exp.Evaluate();
        }
    }
}