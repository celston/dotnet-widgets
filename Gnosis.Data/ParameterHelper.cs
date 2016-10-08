using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Data
{
    public class ParameterHelper
    {
        private ParameterBuilder builder = new ParameterBuilder();

        public void AddParameter(DbCommand cmd, string name, object value)
        {
            cmd.Parameters.Add(builder.Build(cmd, name, value));
        }

        public void AddParameters(DbCommand cmd, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            foreach (KeyValuePair<string, object> parameter in parameters)
            {
                AddParameter(cmd, parameter.Key, parameter.Value);
            }
        }
    }
}
