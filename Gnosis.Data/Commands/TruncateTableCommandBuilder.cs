using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Data.Commands
{
    public abstract class TruncateTableCommandBuilder : CommandBuilder
    {
        protected abstract string TableName { get; }

        public DbCommand Build(DbConnection conn)
        {
            DbCommand cmd = BuildTextCommand(conn);

            cmd.CommandText = "TRUNCATE TABLE " + TableName;

            return cmd;
        }
    }
}
