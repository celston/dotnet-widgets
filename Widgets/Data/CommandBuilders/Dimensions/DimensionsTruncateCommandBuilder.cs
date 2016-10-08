using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widgets.Data.CommandBuilders.Dimensions
{
    public class DimensionsTruncateCommandBuilder : TruncateTableCommandBuilder
    {
        protected override string TableName
        {
            get
            {
                return "Dimensions";
            }
        }
    }
}
