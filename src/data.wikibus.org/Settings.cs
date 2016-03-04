using System.Configuration;
using wikibus.sources.dotNetRDF;

namespace data.wikibus.org
{
    public class Settings : ISourcesDatabaseConnectionStringProvider
    {
        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["sql"].ConnectionString; }
        }
    }
}