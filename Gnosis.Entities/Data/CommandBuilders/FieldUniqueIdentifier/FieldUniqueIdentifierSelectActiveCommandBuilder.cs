using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities.Data.CommandBuilders.FieldUniqueIdentifier
{
    public class FieldUniqueIdentifierSelectActiveCommandBuilder : CommandBuilder
    {
        public DbCommand Build(DbConnection conn)
        {
            DbCommand cmd = BuildTextCommand(conn);

            cmd.CommandText = "SELECT FieldUniqueIdentifier.* FROM Entity JOIN FieldUniqueIdentifier ON Entity.revision = FieldUniqueIdentifier.revision";

            return cmd;
        }
    }
}
