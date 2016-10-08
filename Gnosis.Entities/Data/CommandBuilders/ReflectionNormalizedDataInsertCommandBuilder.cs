using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;

using Gnosis.Data.Commands;

namespace Gnosis.Entities.Data.CommandBuilders
{
    public abstract class ReflectionNormalizedDataInsertCommandBuilder<T> : InsertCommandBuilder
    {
        Type type = typeof(T);

        protected override IEnumerable<string> Columns
        {
            get
            {
                List<string> columns = new List<string>() { "id", "revision" };

                columns.AddRange(type.GetProperties().Select(x => x.Name));

                return columns;
            }
        }

        public DbCommand Build(DbConnection conn, DbTransaction trans, Guid id, Guid revision, T data)
        {
            DbCommand cmd = DoBuild(conn, trans);

            parameterHelper.AddParameters(cmd, new Dictionary<string, object>()
            {
                { "@id", id },
                { "@revision", revision }
            });

            foreach (PropertyInfo pi in type.GetProperties())
            {
                object value = pi.GetValue(data);
                if (value != null)
                {
                    parameterHelper.AddParameter(cmd, "@" + pi.Name, value);
                }
                else
                {
                    parameterHelper.AddParameter(cmd, "@" + pi.Name, DBNull.Value);
                }
            }

            return cmd;
        }
    }
}
