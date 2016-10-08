using Gnosis.Data;
using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Gnosis.Entities.Data.CommandBuilders.Entity
{
    public class EntitySelectByIdListCommandBuilder : CommandBuilder
    {
        public DbCommand Build(DbConnection conn, IEnumerable<Guid> ids)
        {
            DbCommand cmd = BuildTextCommand(conn);

            List<string> placeholders = new List<string>();
            int i = 0;
            foreach (Guid id in ids)
            {
                string placeholder = string.Format("@id{0}", i++);
                parameterHelper.AddParameter(cmd, placeholder, id);
            }

            cmd.CommandText = "SELECT * FROM Entity WHERE id IN (" + string.Join(", ", placeholders) + ")";

            return cmd;
        }
    }
}
