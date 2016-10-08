using Gnosis.Data;
using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities.Data.CommandBuilders.Entity
{
    public class EntitySelectByTypeCommandBuilder : CommandBuilder
    {
        public DbCommand Build(DbConnection conn, string type)
        {
            DbCommand cmd = BuildTextCommand(conn);

            cmd.CommandText = "SELECT * FROM Entity WHERE type = @type";
            parameterHelper.AddParameter(cmd, "@type", type);

            return cmd;
        }
    }
}
