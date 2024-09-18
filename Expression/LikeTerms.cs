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
    public class LikeTermsCollection : Dictionary<List<(Pronumeral, int)>, List<ExpressionTerm>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LikeTermCollection"/> class 
        /// with a custom equality comparer for pronumeral lists.
        /// </summary>
        public LikeTermsCollection() : base(new PronumeralListEqualityComparer())
        {

        }
        /// <summary>
        /// Converts this collection to an array of terms grouped by their pronumerals
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string output = "";
            foreach (var likeTerms in this)
                foreach (ExpressionTerm term in likeTerms.Value)
                    output += (term.ToString() + ", ");
            return output[..^2];
        }   
    }
}
