using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fruits
{
    public class FruitValidator
    {
        public void Validate(IFruit data)
        {
            if (data.Weight <= 0)
            {
                throw new Exception(string.Format("Invalid weight ({0})", data.Weight));
            }
        }
    }
}
