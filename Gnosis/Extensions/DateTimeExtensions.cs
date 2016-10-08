using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gnosis.Helpers;

namespace Gnosis.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool VirtuallyEquals(this DateTime a, DateTime? b)
        {
            return DateTimeHelper.Instance.VirtuallyEqual(a, b);
        }
    }
}
