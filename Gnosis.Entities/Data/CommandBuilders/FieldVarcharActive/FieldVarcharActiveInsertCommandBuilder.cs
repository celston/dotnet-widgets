using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities.Data.CommandBuilders.FieldVarcharActive
{
    public class FieldVarcharActiveInsertCommandBuilder : InsertCommandBuilder
    {
        protected override IEnumerable<string> Columns
        {
            get
            {
                return new string[] { "id", "name", "delta", "value" };
            }
        }

        protected override string TableName
        {
            get
            {
                return "FieldVarcharActive";
            }
        }

        public DbCommand Build(DbConnection conn, DbTransaction trans, Guid id, string name, int delta, object value)
        {
            DbCommand cmd = DoBuild(conn, trans);

            parameterHelper.AddParameters(cmd, new Dictionary<string, object>()
            {
                { "@id", id },
                { "@name", name },
                { "@delta", delta },
                { "@value", value.ToString() }
            });

            return cmd;
        }
    }
}
