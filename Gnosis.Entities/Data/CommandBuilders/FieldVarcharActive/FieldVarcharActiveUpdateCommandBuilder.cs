using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities.Data.CommandBuilders.FieldVarcharActive
{
    public class FieldVarcharActiveUpdateCommandBuilder : CommandBuilder
    {
        public DbCommand Build(DbConnection conn, DbTransaction trans, Guid id, string name, int delta, object value)
        {
            DbCommand cmd = BuildTextCommand(conn, trans);
            cmd.CommandText = "UPDATE FieldVarcharActive SET value = @value WHERE id = @id AND name = @name AND delta = @delta";

            parameterHelper.AddParameters(cmd, new Dictionary<string, object>()
            {
                { "@id", id },
                { "@value", value.ToString() },
                { "@name", name },
                { "@delta", delta }
            });

            return cmd;
        }
    }
}
