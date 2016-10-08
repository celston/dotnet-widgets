using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using Gnosis.Data;

namespace Gnosis.Entities.Data.CommandBuilders.Entity
{
    public class EntitySelectCountByIdAndRevisionCommandBuilder
    {
        private ParameterHelper parameterHelper = new ParameterHelper();

        public DbCommand Build(DbConnection conn, Guid id, Guid revision)
        {
            DbCommand cmd = conn.CreateCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT COUNT(*) FROM Entity WHERE id = @id AND revision = @revision";

            parameterHelper.AddParameters(cmd, new Dictionary<string, object>()
            {
                { "@id", id },
                { "@revision", revision }
            });

            return cmd;
        }
    }
}
