using Gnosis.Data;
using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities.Data.CommandBuilders.FieldVarchar
{
    public class FieldVarcharSelectActiveCommandBuilder : CommandBuilder
    {
        public DbCommand Build(DbConnection conn)
        {
            DbCommand cmd = BuildTextCommand(conn);

            cmd.CommandText = "SELECT FieldVarchar.* FROM Entity JOIN FieldVarchar ON Entity.revision = FieldVarchar.revision";

            return cmd;
        }
    }
}
