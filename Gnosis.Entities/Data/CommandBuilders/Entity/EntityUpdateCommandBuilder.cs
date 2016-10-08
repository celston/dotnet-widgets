using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using Gnosis.Data;

namespace Gnosis.Entities.Data.CommandBuilders.Entity
{
    public class EntityUpdateCommandBuilder
    {
        private ParameterHelper parameterHelper = new ParameterHelper();

        public DbCommand Build(DbConnection conn, DbTransaction trans, Guid id, Guid revision, DateTime updated)
        {
            DbCommand cmd = conn.CreateCommand();
            cmd.Transaction = trans;

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Entity SET revision = @revision, updated = @updated WHERE id = @id";

            parameterHelper.AddParameters(cmd, new Dictionary<string, object>()
            {
                { "@id", id },
                { "@revision", revision },
                { "@updated", updated }
            });

            return cmd;
        }
    }
}
