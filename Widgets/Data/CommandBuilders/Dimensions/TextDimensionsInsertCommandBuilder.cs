using System.Data.Common;

using Gnosis.Data;
using Gnosis.Data.Commands;

namespace Widgets.Data.CommandBuilders.Dimensions
{
    public class TextDimensionsInsertCommandBuilder : IDimensionsInsertCommandBuilder
    {
        public DbCommand Build(DbConnection conn, DbTransaction trans)
        {
            TextCommandBuilder inner = new TextCommandBuilder();
            DbCommand cmd = inner.Build(conn, trans);

            cmd.CommandText = "INSERT INTO Dimensions (id, revision, length, width, height) VALUES (@id, @revision, @length, @width, @height)";

            return cmd;
        }
    }
}
