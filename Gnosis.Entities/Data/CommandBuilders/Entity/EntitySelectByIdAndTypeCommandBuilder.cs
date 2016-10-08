using System;
using System.Data.Common;

using Gnosis.Data;
using Gnosis.Data.Commands;

namespace Gnosis.Entities.Data.CommandBuilders.Entity
{
    public class EntitySelectByIdAndTypeCommandBuilder : CommandBuilder
    {
        public DbCommand Build(DbConnection conn, Guid id, string type)
        {
            DbCommand cmd = BuildTextCommand(conn);

            cmd.CommandText = "SELECT * FROM Entity WHERE id = @id AND type = @type";
            parameterHelper.AddParameter(cmd, "@id", id);
            parameterHelper.AddParameter(cmd, "@type", type);

            return cmd;
        }
    }
}
