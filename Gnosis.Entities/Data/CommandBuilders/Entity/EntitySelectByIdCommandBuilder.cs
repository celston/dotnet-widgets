using System;
using System.Data.Common;

using Gnosis.Data;
using Gnosis.Data.Commands;

namespace Gnosis.Entities.Data.CommandBuilders.Entity
{
    public class EntitySelectByIdCommandBuilder : CommandBuilder
    {
        public DbCommand Build(DbConnection conn, Guid id)
        {
            DbCommand cmd = BuildTextCommand(conn);

            cmd.CommandText = "SELECT * FROM Entity WHERE id = @id";
            parameterHelper.AddParameter(cmd, "@id", id);

            return cmd;
        }
    }
}
