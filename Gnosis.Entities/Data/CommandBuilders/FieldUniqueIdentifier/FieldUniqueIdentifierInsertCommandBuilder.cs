using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gnosis.Data;
using System.Data.Common;
using Gnosis.Data.Commands;

namespace Gnosis.Entities.Data.CommandBuilders.FieldUniqueIdentifier
{
    public class FieldUniqueIdentifierInsertCommandBuilder : DenormalizedFieldInsertCommandBuilder
    {
        protected override string TableName
        {
            get
            {
                return "FieldUniqueIdentifier";
            }
        }

        public DbCommand Build(DbConnection conn, DbTransaction trans, Guid id, Guid revision, string name, int delta, Guid value)
        {
            DbCommand cmd = DoBuild(conn, trans);

            parameterHelper.AddParameters(cmd, new Dictionary<string, object>()
            {
                { "@id", id },
                { "@revision", revision },
                { "@name", name },
                { "@delta", delta },
                { "@value", value }
            });

            return cmd;
        }
    }
}
