using System.Data.Common;

namespace Gnosis.Data
{
    public class ParameterBuilder
    {
        public DbParameter Build(DbCommand cmd, string name, object value)
        {
            DbParameter p = cmd.CreateParameter();
            p.ParameterName = name;
            p.Value = value;

            return p;
        }
    }
}
