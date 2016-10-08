using System.Data;
using System.Data.Common;

namespace Gnosis.Data.Commands
{
    public class TextCommandBuilder
    {
        public DbCommand Build(DbConnection conn, DbTransaction trans)
        {
            DbCommand cmd = Build(conn);
            cmd.Transaction = trans;

            return cmd;
        }

        public DbCommand Build(DbConnection conn)
        {
            DbCommand cmd = conn.CreateCommand();

            cmd.CommandType = CommandType.Text;

            return cmd;
        }
    }
}
