using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widgets.Validators
{
    public class SpecificationsValidator
    {
        public void Validate(ISpecifications data)
        {
            if (data.Volume <= 0)
            {
                throw new Exception(string.Format("Invalid volume ({0})", data.Volume));
            }
            if (data.Weight <= 0)
            {
                throw new Exception(string.Format("Invalid length ({0})", data.Weight));
            }
        }
    }
}
