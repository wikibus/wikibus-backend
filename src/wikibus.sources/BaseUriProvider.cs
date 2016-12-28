using Argolis.Models;
using Wikibus.Common;

namespace Wikibus.Sources
{
    public class BaseUriProvider : IBaseUriProvider
    {
        public BaseUriProvider(IWikibusConfiguration configuration)
        {
            this.BaseResourceUri = configuration.BaseResourceNamespace;
        }

        public string BaseResourceUri { get; }
    }
}