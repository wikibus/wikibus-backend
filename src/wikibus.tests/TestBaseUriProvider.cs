using System;
using Argolis.Templates;

namespace Wikibus.Tests
{
    public class TestBaseUriProvider : IBaseUriProvider
    {
        private static readonly TestConfiguration TestConfiguration = new TestConfiguration();

        public string BaseUri => TestConfiguration.BaseResourceNamespace;
    }
}