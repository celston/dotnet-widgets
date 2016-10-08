using Gnosis.Data.Commands;
using System.Data.Common;

namespace Widgets.Data.CommandBuilders.Dimensions
{
    public class DimensionsSelectActiveCommandBuilder : CommandBuilder
    {
        public DbCommand Build(DbConnection conn)
        {
            DbCommand cmd = BuildTextCommand(conn);

            cmd.CommandText = "SELECT Dimensions.* FROM Entity JOIN Dimensions ON Entity.revision = Dimensions.revision";

            return cmd;
        }
    }
}
