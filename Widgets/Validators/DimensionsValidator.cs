using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widgets.Validators
{
    public class DimensionsValidator
    {
        public void Validate(IDimensions data)
        {
            if (data.Height <= 0)
            {
                throw new Exception(string.Format("Invalid height ({0})", data.Height));
            }
            if (data.Length <= 0)
            {
                throw new Exception(string.Format("Invalid length ({0})", data.Length));
            }
            if (data.Width <= 0)
            {
                throw new Exception(string.Format("Invalid width ({0})", data.Width));
            }
        }
    }
}
