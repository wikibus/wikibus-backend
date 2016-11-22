using System.Configuration;

namespace Wikibus.Common
{
    /// <summary>
    /// Retrieves settings from setting configuration section
    /// </summary>
    public class AppSettingsConfiguration : IWikibusConfiguration
    {
        private const string Prefix = "wikibus#";

        private const string BaseUrl = Prefix + "baseUrl";
        private const string ApiUrl = Prefix + "apiUrl";
        private const string WebUrl = Prefix + "websiteUrl";

        /// <summary>
        /// Gets the base namespace for data resources.
        /// </summary>
        public string BaseResourceNamespace
        {
            get { return ConfigurationManager.AppSettings[BaseUrl]; }
        }

        /// <summary>
        /// Gets the base namespace for API resources.
        /// </summary>
        public string BaseApiNamespace
        {
            get { return ConfigurationManager.AppSettings[ApiUrl]; }
        }

        /// <summary>
        /// Gets the base address for the wikibus.org website
        /// </summary>
        public string BaseWebNamespace
        {
            get { return ConfigurationManager.AppSettings[WebUrl]; }
        }
    }
}
