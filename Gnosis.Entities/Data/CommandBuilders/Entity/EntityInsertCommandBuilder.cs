using System;
using System.Collections.Generic;
using System.Data.Common;

using Gnosis.Data;
using Gnosis.Data.Commands;

namespace Gnosis.Entities.Data.CommandBuilders.Entity
{
    public class EntityInsertCommandBuilder : InsertCommandBuilder
    {
        protected override IEnumerable<string> Columns
        {
            get
            {
                return new string[] { "id", "revision", "type", "created", "updated" };
            }
        }

        protected override string TableName
        {
            get
            {
                return "Entity";
            }
        }

        public DbCommand Build(DbConnection conn, DbTransaction trans, Guid id, Guid revision, string type, DateTime created)
        {
            DbCommand cmd = DoBuild(conn, trans);

            parameterHelper.AddParameters(cmd, new Dictionary<string, object>()
            {
                { "@id", id },
                { "@revision", revision },
                { "@type", type },
                { "@created", created },
                { "@updated", created }
            });

            return cmd;
        }
    }
}
