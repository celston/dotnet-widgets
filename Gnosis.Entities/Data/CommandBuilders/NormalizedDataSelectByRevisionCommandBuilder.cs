using System;
using System.Data.Common;

using Gnosis.Data;
using Gnosis.Data.Commands;

namespace Gnosis.Entities.Data.CommandBuilders
{
    public abstract class NormalizedDataSelectByRevisionCommandBuilder : CommandBuilder
    {
        protected abstract string TableName { get; }

        public DbCommand Build(DbConnection conn, Guid revision)
        {
            DbCommand cmd = BuildTextCommand(conn);

            cmd.CommandText = string.Format("SELECT * FROM {0} WHERE revision = @revision", TableName);
            parameterHelper.AddParameter(cmd, "@revision", revision);

            return cmd;
        }
    }
}
