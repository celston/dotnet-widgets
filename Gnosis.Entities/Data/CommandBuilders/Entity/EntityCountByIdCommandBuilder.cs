using System;
using System.Data;
using System.Data.Common;

using Gnosis.Data;

namespace Gnosis.Entities.Data.CommandBuilders.Entity
{
    public class EntityCountByIdCommandBuilder
    {
        private ParameterHelper parameterHelper = new ParameterHelper();

        public DbCommand Build(DbConnection conn, Guid id)
        {
            DbCommand cmd = conn.CreateCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT COUNT(*) FROM Entity WHERE id = @id";

            parameterHelper.AddParameter(cmd, "@id", id);

            return cmd;
        }
    }
}
