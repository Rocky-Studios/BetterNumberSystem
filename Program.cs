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
            ExpressionFunction add = new ExpressionFunction()
            {
                Name = "Sum",
                Symbol = "+",
                Function = inputs =>
                {
                    LikeTermsCollection likeTerms = ExpressionGroup.ToLikeTermsCollection(inputs);

                    // Assuming we're only dealing with numbers (no vectors or matrices yet)
                    foreach (KeyValuePair<List<Pronumeral>, List<ExpressionTerm>> likeTermCollection in likeTerms)
                        foreach (ExpressionTerm expressionTerm in likeTermCollection.Value)
                            if (expressionTerm.Value is not Number) throw new NotImplementedException();

                    ExpressionGroup output = new();

                    foreach (KeyValuePair<List<Pronumeral>, List<ExpressionTerm>> likeTermCollection in likeTerms)
                    {
                        List<Pronumeral> pronumerals = likeTermCollection.Key;
                        Number total = new(0, (likeTermCollection.Value[0].Value as Number).Unit);

                        foreach (ExpressionTerm t in likeTermCollection.Value)
                        {
                            Number n = t.Value as Number;
                            Number nConverted = n.Convert(total.Unit);
                            total.NumericValue += nConverted.NumericValue;
                        }

                        output.Parts.Add(new ExpressionTerm() { Value = total, Pronumerals = pronumerals});
                    }
                    return output;
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
                                Value = myNum,
                                Pronumerals = [Pronumeral.NO_PRONUMERAL]
                            }]
                        }),
                        new ExpressionFunctionInput("Value", new ExpressionGroup()
                        {
                            Parts = [new ExpressionTerm()
                            {
                                Value = myNum,
                                Pronumerals = [Pronumeral.NO_PRONUMERAL]
                            }]
                        })
                    ],
                    InputType = new ExpressionFunctionInputAmount()
                    {
                        Infinite = true
                    }
                }
            ];
            Console.WriteLine(exp.Evaluate());
        }
    }
}