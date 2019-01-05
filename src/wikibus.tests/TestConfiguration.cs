using Argolis.Models;
using Wikibus.Common;

namespace Wikibus.Tests
{
    public class TestConfiguration : IWikibusConfiguration, IBaseUriProvider
    {
        public string BaseResourceNamespace
        {
            get { return "https://wikibus.org/"; }
        }

        public string BaseApiNamespace
        {
            get { return "https://wikibus.org/data/"; }
        }

        public string BaseWebNamespace
        {
            get { return "https://www.wikibus.org/"; }
        }

        public string BaseResourceUri => this.BaseResourceNamespace;
    }
}
