using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterNumberSystem.Expression
{
    public class Expression
    {
        public List<IExpressionPart> Parts = new();

        public ExpressionGroup Evaluate()
        {
            if (Parts.Count == 0) throw new Exception("Empty expresssion");
            if (Parts.Count == 1)
            {
                if (Parts[0] is ExpressionTerm) return new ExpressionGroup() { Parts = [Parts[0]] };
                else throw new Exception("Empty expresssion");
            }

            Stack<ExpressionFunction> functions = new Stack<ExpressionFunction>();
            ExpressionGroup output = new();

            foreach (IExpressionPart part in Parts)
                if (part is ExpressionFunction function) functions.Push(function);
            
            while (functions.Count > 0)
            {
                ExpressionFunction function = functions.Pop();
                var inputs = Parts[Parts.IndexOf(function)+1];
                Parts.RemoveAt(Parts.IndexOf(function) + 1);
                if (inputs is not ExpressionFunctionInputs) throw new Exception("Invalid expression structure");
                inputs = inputs as ExpressionFunctionInputs;

                List<ExpressionGroup> functionInputs = [];
                foreach (ExpressionFunctionInput input in (inputs as ExpressionFunctionInputs).Inputs)
                {
                    functionInputs.Add(input.Value);
                }

                ExpressionGroup result = function.Function.Invoke(functionInputs.ToArray());
                
                output.Parts.Add(result);
            }

            return output;
        }

        public (IExpressionValue, Expression) Simplify()
        {
            throw new NotImplementedException();
        }
    }
}
