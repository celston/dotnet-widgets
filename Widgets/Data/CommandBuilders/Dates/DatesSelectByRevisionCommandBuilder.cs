using Gnosis.Entities.Data.CommandBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widgets.Data.CommandBuilders.Dates
{
    public class DatesSelectByRevisionCommandBuilder : NormalizedDataSelectByRevisionCommandBuilder
    {
        protected override string TableName
        {
            get
            {
                return "Dates";
            }
        }
    }
}
