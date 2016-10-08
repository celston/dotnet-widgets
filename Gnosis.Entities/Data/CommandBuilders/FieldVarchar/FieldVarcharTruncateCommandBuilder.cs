using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities.Data.CommandBuilders.FieldVarchar
{
    public class FieldVarcharTruncateCommandBuilder : TruncateTableCommandBuilder
    {
        protected override string TableName
        {
            get
            {
                return "FieldVarchar";
            }
        }
    }
}
