using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterNumberSystem.Expression
{
    public class Expression(ExpressionFunction rootFunction)
    {
        public ExpressionFunction RootFunction = rootFunction;

        public ExpressionGroup Evaluate()
        {
            if (RootFunction is null) throw new Exception("Empty/invalid expresssion");

            return RootFunction.Function(RootFunction.Inputs);
        }

        public (IExpressionValue, Expression) Simplify()
        {
            throw new NotImplementedException();
        }
    }
}
