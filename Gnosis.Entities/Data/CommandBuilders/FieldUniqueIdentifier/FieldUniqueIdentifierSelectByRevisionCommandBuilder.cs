using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities.Data.CommandBuilders.FieldUniqueIdentifier
{
    public class FieldUniqueIdentifierSelectByRevisionCommandBuilder : CommandBuilder
    {
        public DbCommand Build(DbConnection conn, Guid revision)
        {
            DbCommand cmd = BuildTextCommand(conn);

            cmd.CommandText = "SELECT * FROM FieldUniqueIdentifier WHERE revision = @revision";
            parameterHelper.AddParameter(cmd, "@revision", revision);

            return cmd;
        }
    }
}
