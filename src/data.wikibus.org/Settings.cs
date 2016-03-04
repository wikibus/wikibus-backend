using System;
using System.Configuration;
using wikibus.sources.dotNetRDF;

namespace data.wikibus.org
{
    public class Settings : ISourcesDatabaseSettings
    {
        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["sql"].ConnectionString; }
        }

        public Uri SourcesSparqlEndpoint
        {
            get
            {
                var setting = ConfigurationManager.AppSettings["wikibus.source.dotNetRDF#sparqlEndpoint"];
                return new Uri(setting);
            }
        }
    }
}