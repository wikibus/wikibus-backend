using Argolis.Models;

namespace Wikibus.Tests
{
    public class TestBaseUriProvider : IBaseUriProvider
    {
        private static readonly TestConfiguration TestConfiguration = new TestConfiguration();

        public string BaseResourceUri => TestConfiguration.BaseResourceNamespace;
    }
}