using Wikibus.Common;

namespace wikibus.tests
{
    public class TestConfiguration : IWikibusConfiguration
    {
        public string BaseResourceNamespace
        {
            get { return "http://wikibus.org/"; }
        }

        public string BaseApiNamespace
        {
            get { return "http://wikibus.org/data/"; }
        }

        public string BaseWebNamespace
        {
            get { return "http://www.wikibus.org/"; }
        }
    }
}
