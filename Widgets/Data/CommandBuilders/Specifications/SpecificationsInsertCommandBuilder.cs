using Gnosis.Data.Commands;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Widgets.Data.CommandBuilders.Specifications
{
    public class SpecificationsInsertCommandBuilder : InsertCommandBuilder, ISpecificationsInsertCommandBuilder
    {
        protected override IEnumerable<string> Columns
        {
            get
            {
                return new string[] { "id", "revision", "volume", "weight" };
            }
        }

        protected override string TableName
        {
            get
            {
                return "Specifications";
            }
        }

        public DbCommand Build(DbConnection conn, DbTransaction trans, Guid id, Guid revision, ISpecifications data)
        {
            DbCommand cmd = DoBuild(conn, trans);

            parameterHelper.AddParameters(cmd, new Dictionary<string, object>()
            {
                { "@id", id },
                { "@revision", revision }
            });
            parameterHelper.AddParameters(cmd, new Dictionary<string, object>()
            {
                { "@volume", data.Volume },
                { "@weight", data.Weight }
            });

            return cmd;
        }
    }
}
