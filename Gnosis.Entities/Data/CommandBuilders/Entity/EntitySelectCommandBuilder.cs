using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities.Data.CommandBuilders.Entity
{
    public class EntitySelectCommandBuilder : CommandBuilder
    {
        public DbCommand Build(DbConnection conn)
        {
            DbCommand cmd = BuildTextCommand(conn);

            cmd.CommandText = "SELECT * FROM Entity";

            return cmd;
        }
    }
}
