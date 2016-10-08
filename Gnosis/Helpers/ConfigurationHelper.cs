using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gnosis.Configuration;

namespace Gnosis.Helpers
{
    public class ConfigurationHelper : Helper<ConfigurationHelper>, IConfigurationProvider
    {
        public string GetString(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public int? GetInt(string key)
        {
            string value = GetString(key);
            int result = 0;
            if (int.TryParse(value, out result))
            {
                return result;
            }

            return null;
        }

        public decimal? GetDecimal(string key)
        {
            string value = GetString(key);
            decimal result = 0;
            if (decimal.TryParse(value, out result))
            {
                return result;
            }

            return null;
        }
    }
}
