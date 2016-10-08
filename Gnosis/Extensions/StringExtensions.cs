using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gnosis.Helpers;

namespace Gnosis.Extensions
{
    public static class StringExtensions
    {
        public static string LowercaseFirst(this string s)
        {
            return StringHelper.Instance.LowercaseFirst(s);
        }
    }
}
