namespace Wikibus.Common
{
    /// <summary>
    /// Common wikibus settings
    /// </summary>
    public interface IWikibusConfiguration
    {
        /// <summary>
        /// Gets the base namespace for data resources.
        /// </summary>
        string BaseResourceNamespace { get; }

        /// <summary>
        /// Gets the base namespace for API resources.
        /// </summary>
        string BaseApiNamespace { get; }

        /// <summary>
        /// Gets the base address for the wikibus.org website
        /// </summary>
        string BaseWebNamespace { get; }
    }
}
