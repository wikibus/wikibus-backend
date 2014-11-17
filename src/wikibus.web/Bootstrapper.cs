using VDS.RDF;
using VDS.RDF.Query;

namespace wikibus.web
{
    /// <summary>
    /// Bootstrapper for wikibus.org API app
    /// </summary>
    public class Bootstrapper : Nancy.DefaultNancyBootstrapper
    {
        /// <summary>
        /// Configures the container using AutoRegister followed by registration
        /// of default INancyModuleCatalog and IRouteResolver.
        /// </summary>
        protected override void ConfigureApplicationContainer(Nancy.TinyIoc.TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<ISparqlQueryProcessor>(new LeviathanQueryProcessor(new TripleStore()));
        }
    }
}
