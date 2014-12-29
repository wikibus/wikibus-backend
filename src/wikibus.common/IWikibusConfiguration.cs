namespace wikibus.common
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
    }
}
