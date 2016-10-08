using System.Data;
using System.Data.Common;

namespace Gnosis.Data.Commands
{
    public class StoredProcdureCommandBuilder
    {
        private string commandText;

        public StoredProcdureCommandBuilder(string commandText)
        {
            this.commandText = commandText;
        }

        public DbCommand Build(DbConnection conn, DbTransaction trans)
        {
            DbCommand cmd = Build(conn);
            cmd.Transaction = trans;

            return cmd;
        }

        public DbCommand Build(DbConnection conn)
        {
            DbCommand cmd = conn.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = commandText;

            return cmd;
        }
    }
}
