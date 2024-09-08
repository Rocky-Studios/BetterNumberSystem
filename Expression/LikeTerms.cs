using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterNumberSystem.Expression
{
    /// <summary>
    /// Represents a collection of terms grouped by their pronumerals.
    /// </summary>
    public class LikeTermsCollection : Dictionary<List<Pronumeral>, List<ExpressionTerm>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LikeTermCollection"/> class 
        /// with a custom equality comparer for pronumeral lists.
        /// </summary>
        public LikeTermsCollection() : base(new PronumeralListEqualityComparer())
        {

        }
    }
}
