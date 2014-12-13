using System.Configuration;
using JsonLD.Entities;
using Nancy.Bootstrapper;
using Slp.r2rml4net.Storage;
using Slp.r2rml4net.Storage.Sql;
using Slp.r2rml4net.Storage.Sql.Vendor;
using VDS.RDF.Query;
using VDS.RDF.Storage;
using wikibus.sources;
using wikibus.sources.dotNetRDF;

namespace wikibus.nancy
{
    /// <summary>
    /// Install all required components
    /// </summary>
    public class ComponentsInstaller : Registrations
    {
        public ComponentsInstaller()
        {
            Register<ISparqlQueryProcessor>(typeof(GenericQueryProcessor));
            Register<IQueryableStorage>(typeof(R2RMLStorage));
            Register<ISqlDb>(new MSSQLDb(ConfigurationManager.ConnectionStrings["sql"].ConnectionString));
            Register(new InstanceRegistration(typeof(IContextProvider), CreateContextProvider()));
            Register<IR2RML>(typeof(WikibusR2RML));
        }

        private static IContextProvider CreateContextProvider()
        {
            var contextProvider = new StaticContextProvider();
            contextProvider.SetupSourcesContexts();
            return contextProvider;
        }
    }
}
