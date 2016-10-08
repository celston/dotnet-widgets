using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Data.Hydrators
{
    public class DecimalPropertyHydrator : Hydrator
    {
        public void Hydrate<T>(string propertyName, string input, T output)
        {
            typeof(T).GetProperty(propertyName).SetValue(output, decimal.Parse(input));
        }
    }
}
