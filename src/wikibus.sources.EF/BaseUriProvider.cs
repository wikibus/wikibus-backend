using Argolis.Templates;
using Wikibus.Common;

namespace Wikibus.Sources.EF
{
    public class BaseUriProvider : IBaseUriProvider
    {
        public BaseUriProvider(IWikibusConfiguration configuration)
        {
            this.BaseUri = configuration.BaseResourceNamespace;
        }

        public string BaseUri { get; private set; }
    }
}