using Gnosis.Helpers;

namespace Gnosis.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        #region Protected Properties

        protected IConfigurationProvider Provider { get; set; }
        protected string KeyPrefix { get; set; }

        #endregion

        #region Protected Virtual Methods

        public virtual string TranslateKey(string key)
        {
            return KeyPrefix + key;
        }

        #endregion

        #region Constructors

        public ConfigurationProvider() : this("", ConfigurationHelper.Instance)
        {
        }

        public ConfigurationProvider(string keyPrefix) : this(keyPrefix, ConfigurationHelper.Instance)
        {
        }

        public ConfigurationProvider(string keyPrefix, IConfigurationProvider provider)
        {
            KeyPrefix = keyPrefix;
            Provider = provider;
        }

        #endregion

        #region IConfigurationProvider Implementation

        public decimal? GetDecimal(string key)
        {
            return Provider.GetDecimal(TranslateKey(key));
        }

        public int? GetInt(string key)
        {
            return Provider.GetInt(TranslateKey(key));
        }

        public string GetString(string key)
        {
            return Provider.GetString(TranslateKey(key));
        }

        #endregion
    }
}
