using Gnosis.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Extensions
{
    public static class DecimalExtensions
    {
        public static bool IsVirtuallyEqual(this decimal x, decimal y)
        {
            return DecimalHelper.Instance.AreVirtuallyEqual(x, y);
        }
    }
}
