using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Helpers
{
    public class DecimalHelper : Helper<DecimalHelper>
    {
        public bool AreVirtuallyEqual(decimal? a, decimal? b)
        {
            if (!a.HasValue)
            {
                return !b.HasValue;
            }

            if (!b.HasValue)
            {
                return false;
            }

            decimal diff = Math.Abs(a.Value - b.Value);

            return diff < 0.0000001m;
        }
    }
}
