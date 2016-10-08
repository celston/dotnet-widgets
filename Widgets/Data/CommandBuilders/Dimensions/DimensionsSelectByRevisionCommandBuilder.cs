using Gnosis.Data;
using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widgets.Data.CommandBuilders.Dimensions
{
    public class DimensionsSelectByRevisionCommandBuilder : CommandBuilder
    {
        public DbCommand Build(DbConnection conn, Guid revision)
        {
            DbCommand cmd = BuildTextCommand(conn);

            cmd.CommandText = "SELECT * FROM Dimensions WHERE revision = @revision";
            parameterHelper.AddParameter(cmd, "@revision", revision);

            return cmd;
        }
    }
}
