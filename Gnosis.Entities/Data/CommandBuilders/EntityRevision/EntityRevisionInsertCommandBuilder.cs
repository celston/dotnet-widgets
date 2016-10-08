using System;
using System.Collections.Generic;
using System.Data.Common;
using Gnosis.Data.Commands;

namespace Gnosis.Entities.Data.CommandBuilders.EntityRevision
{
    public class EntityRevisionInsertCommandBuilder : InsertCommandBuilder
    {
        protected override IEnumerable<string> Columns
        {
            get
            {
                return new string[] { "id", "revision", "timestamp" };
            }
        }

        protected override string TableName
        {
            get
            {
                return "EntityRevision";
            }
        }

        public DbCommand Build(DbConnection conn, DbTransaction trans, Guid id, Guid revision, DateTime timestamp)
        {
            DbCommand cmd = DoBuild(conn, trans);

            parameterHelper.AddParameters(cmd, new Dictionary<string, object>()
            {
                { "@id", id },
                { "@revision", revision },
                { "@timestamp", timestamp }
            });

            return cmd;
        }
    }
}
