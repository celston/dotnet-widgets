using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Helpers
{
    public class StringHelper : Helper<StringHelper>
    {
        #region Public Methods

        public string LowercaseFirst(string s)
        {
            return Char.ToLowerInvariant(s[0]) + s.Substring(1);
        }

        #endregion
    }
}
