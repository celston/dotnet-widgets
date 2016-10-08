using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Data
{
    public class ConfigurationConnectionBuilder : IConnectionBuilder
    {
        string connectionStringSettingsName;

        public ConfigurationConnectionBuilder(string connectionStringSettingsName)
        {
            this.connectionStringSettingsName = connectionStringSettingsName;
        }

        public DbConnection Build()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionStringSettingsName];
            if (settings == null)
            {
                throw new Exception(string.Format("No connection string settings for {0}", connectionStringSettingsName));
            }

            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(settings.ProviderName);
            DbConnection conn = dbProviderFactory.CreateConnection();
            conn.ConnectionString = settings.ConnectionString;
            conn.Open();

            return conn;
        }
    }
}
