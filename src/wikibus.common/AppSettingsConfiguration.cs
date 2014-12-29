using System.Configuration;

namespace wikibus.common
{
    /// <summary>
    /// Retrieves settings from setting configuration section
    /// </summary>
    public class AppSettingsConfiguration : IWikibusConfiguration
    {
        /// <summary>
        /// Gets the base namespace for data resources.
        /// </summary>
        public string BaseResourceNamespace
        {
            get { return ConfigurationManager.AppSettings["BaseUrl"]; }
        }

        /// <summary>
        /// Gets the base namespace for API resources.
        /// </summary>
        public string BaseApiNamespace
        {
            get { return ConfigurationManager.AppSettings["ApiUrl"]; }
        }
    }
}
