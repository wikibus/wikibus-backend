using Hydra;

namespace wikibus.nancy
{
    /// <summary>
    /// Serves wikibus API
    /// </summary>
    public class HydraModule : Nancy.Hydra.HydraModule
    {
        /// <summary>
        /// Creates the API documentation.
        /// </summary>
        protected override ApiDocumentation CreateApiDocumentation()
        {
            return new WikibusApi();
        }
    }
}
