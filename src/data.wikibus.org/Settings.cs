using System;
using System.Configuration;
using Wikibus.Sources.DotNetRDF;

namespace Data.Wikibus.Org
{
    public class Settings : ISourcesDatabaseSettings
    {
        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["wikibus.sources#sql"].ConnectionString; }
        }

        public Uri SourcesSparqlEndpoint
        {
            get
            {
                var setting = ConfigurationManager.AppSettings["wikibus.sources.dotNetRDF#sparqlEndpoint"];
                return new Uri(setting);
            }
        }
    }
}