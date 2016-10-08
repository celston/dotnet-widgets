using System.Data.Common;

using Gnosis.Data;
using Gnosis.Data.Commands;

namespace Gnosis.Entities.Data.CommandBuilders.FieldDecimal
{
    public class TextFieldDecimalInsertCommandBuilder : IFieldDecimalInsertCommandBuilder
    {
        public DbCommand Build(DbConnection conn)
        {
            TextCommandBuilder inner = new TextCommandBuilder();
            DbCommand cmd = inner.Build(conn);

            cmd.CommandText = "INSERT INTO FieldDecimal (id, revision, name, delta, value) VALUES (@id, @revision, @name, @delta, @value)";

            return cmd;
        }
    }
}
