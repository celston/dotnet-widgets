using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities.Data.CommandBuilders.FieldVarcharActive
{
    public class FieldVarcharActiveSelectByIdCommandBuilder : CommandBuilder
    {
        public DbCommand Build(DbConnection conn, Guid id)
        {
            DbCommand cmd = BuildTextCommand(conn);

            cmd.CommandText = "SELECT * FROM FieldVarcharActive WHERE id = @id";
            parameterHelper.AddParameter(cmd, "@id", id);

            return cmd;
        }
    }
}
