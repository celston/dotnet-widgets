using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Helpers
{
    public class DateTimeHelper : Helper<DateTimeHelper>
    {
        #region Public Methods

        public bool VirtuallyEqual(DateTime? a, DateTime? b)
        {
            if (!a.HasValue)
            {
                if (b.HasValue)
                {
                    return false;
                }
            }
            else
            {
                if (!b.HasValue)
                {
                    return false;
                }
            }

            TimeSpan ts = a.Value - b.Value;
            Debug.Print("Total Seconds: {0}", ts.TotalSeconds);

            return Math.Abs(ts.TotalSeconds) < 1;
        }

        #endregion
    }
}
