using System;
using System.Data.Common;

using Gnosis.Data;
using Gnosis.Data.Commands;

namespace Gnosis.Entities.Data.CommandBuilders.FieldVarchar
{
    public class FieldVarcharSelectByRevisionCommandBuilder : CommandBuilder
    {
        public DbCommand Build(DbConnection conn, Guid revision)
        {
            DbCommand cmd = BuildTextCommand(conn);

            cmd.CommandText = "SELECT * FROM FieldVarchar WHERE revision = @revision";
            parameterHelper.AddParameter(cmd, "@revision", revision);

            return cmd;
        }
    }
}
