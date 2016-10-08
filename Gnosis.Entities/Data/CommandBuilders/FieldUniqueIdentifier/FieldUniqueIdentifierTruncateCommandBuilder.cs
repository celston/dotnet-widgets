using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities.Data.CommandBuilders.FieldUniqueIdentifier
{
    public class FieldUniqueIdentifierTruncateCommandBuilder : TruncateTableCommandBuilder
    {
        protected override string TableName
        {
            get
            {
                return "FieldUniqueIdentifier";
            }
        }
    }
}
