using Gnosis.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gnosis.Tests.Configuration
{
    [TestClass]
    public class ConfigurationProviderTests
    {
        [TestMethod]
        public void TranslateKey_Normal_Success()
        {
            ConfigurationProvider configurationProvider = CreateNormalConfigurationProvider();
            string key = configurationProvider.TranslateKey("Foo");
            Assert.AreEqual("Foo", key);
        }

        [TestMethod]
        public void TranslateKey_Prefixed_Success()
        {
            ConfigurationProvider configurationProvider = new ConfigurationProvider("Prefix_");
            string key = configurationProvider.TranslateKey("Foo");
            Assert.AreEqual("Prefix_Foo", key);
        }

        [TestMethod]
        public void GetInt_Normal_Success()
        {
            ConfigurationProvider configurationProvider = CreateNormalConfigurationProvider();
            Assert.AreEqual(23, configurationProvider.GetInt("SomeInteger"));
        }

        [TestMethod]
        public void GetInt_Prefixed_Success()
        {
            ConfigurationProvider configurationProvider = CreatePrefixedConfigurationProvider();
            Assert.AreEqual(46, configurationProvider.GetInt("SomeInteger"));
        }

        [TestMethod]
        public void GetString_Normal_Success()
        {
            ConfigurationProvider configurationProvider = CreateNormalConfigurationProvider();
            Assert.AreEqual("Quick Brown Fox", configurationProvider.GetString("SomeString"));
        }

        [TestMethod]
        public void GetString_Prefixed_Success()
        {
            ConfigurationProvider configurationProvider = CreatePrefixedConfigurationProvider();
            Assert.AreEqual("Lazy Dog", configurationProvider.GetString("SomeString"));
        }

        [TestMethod]
        public void GetDecimal_Normal_Success()
        {
            ConfigurationProvider configurationProvider = CreateNormalConfigurationProvider();
            Assert.AreEqual(3.14m, configurationProvider.GetDecimal("SomeDecimal"));
        }

        [TestMethod]
        public void GetDecimal_Prefixed_Success()
        {
            ConfigurationProvider configurationProvider = CreatePrefixedConfigurationProvider();
            Assert.AreEqual(2.81m, configurationProvider.GetDecimal("SomeDecimal"));
        }

        private ConfigurationProvider CreateNormalConfigurationProvider()
        {
            return new ConfigurationProvider();
        }

        private ConfigurationProvider CreatePrefixedConfigurationProvider()
        {
            return new ConfigurationProvider("Prefix_");
        }
    }
}