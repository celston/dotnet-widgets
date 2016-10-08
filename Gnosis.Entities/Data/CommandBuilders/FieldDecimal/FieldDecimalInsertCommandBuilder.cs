using System;
using System.Collections.Generic;
using System.Data.Common;

using Gnosis.Data;

namespace Gnosis.Entities.Data.CommandBuilders.FieldDecimal
{
    public class FieldDecimalInsertCommandBuilder
    {
        public DbCommand Build(DbConnection conn, IFieldDecimalInsertCommandBuilder inner, Guid id, Guid revision, string name, int delta, decimal value)
        {
            DbCommand cmd = inner.Build(conn);

            ParameterHelper helper = new ParameterHelper();
            helper.AddParameters(cmd, new Dictionary<string, object>()
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
