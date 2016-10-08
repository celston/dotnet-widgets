using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities.Data.CommandBuilders.FieldUniqueIdentifier
{
    public class FieldUniqueIdentifierSelectActiveByNameValueCommandBuilder : CommandBuilder
    {
        public DbCommand Build(DbConnection conn, string name, Guid value)
        {
            DbCommand cmd = BuildTextCommand(conn);

            cmd.CommandText = @"
                SELECT
	                Entity.*
                FROM
	                Entity
	                JOIN FieldUniqueIdentifier ON Entity.revision = FieldUniqueIdentifier.revision AND FieldUniqueIdentifier.name = @name
                WHERE
	                FieldUniqueIdentifier.value = @value
            ";

            parameterHelper.AddParameter(cmd, "@name", name);
            parameterHelper.AddParameter(cmd, "@value", value);

            return cmd;
        }
    }
}
