using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widgets.Validators
{
    public class WidgetValidator
    {
        private DimensionsValidator dimensionsValidator = new DimensionsValidator();
        private SpecificationsValidator specificationsValidator = new SpecificationsValidator();

        public void Validate(IWidget data)
        {
            dimensionsValidator.Validate(data);
            specificationsValidator.Validate(data);
        }
    }
}
