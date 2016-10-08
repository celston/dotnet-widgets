using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Helpers
{
    public abstract class Helper<T>
        where T : new()
    {
        protected static readonly Lazy<T> lazy = new Lazy<T>(() =>
        {
            return new T();
        });

        protected Helper()
        {
        }

        public static T Instance
        {
            get
            {
                return lazy.Value;
            }
        }
    }
}
