using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities.Data.CommandBuilders
{
    public abstract class DenormalizedFieldInsertCommandBuilder : InsertCommandBuilder
    {
        protected override IEnumerable<string> Columns
        {
            get
            {
                return new string[] { "id", "revision", "name", "delta", "value" };
            }
        }
    }
}
