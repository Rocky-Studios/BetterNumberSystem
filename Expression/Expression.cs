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

        public IExpressionValue Evaluate()
        {
            throw new NotImplementedException();
        }

        public (IExpressionValue, Expression) Simplify()
        {
            throw new NotImplementedException();
        }
    }
}
