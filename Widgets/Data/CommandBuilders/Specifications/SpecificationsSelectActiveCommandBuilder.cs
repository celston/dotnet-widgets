using Gnosis.Data.Commands;
using System.Data.Common;

namespace Widgets.Data.CommandBuilders.Specifications
{
    public class SpecificationsSelectActiveCommandBuilder : CommandBuilder
    {
        public DbCommand Build(DbConnection conn)
        {
            DbCommand cmd = BuildTextCommand(conn);

            cmd.CommandText = "SELECT Specifications.* FROM Entity JOIN Specifications ON Entity.revision = Specifications.revision";

            return cmd;
        }
    }
}
