using VDS.RDF;
using VDS.RDF.Query;

namespace wikibus.web
{
    public class Bootstrapper : Nancy.DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(Nancy.TinyIoc.TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<ISparqlQueryProcessor>(new LeviathanQueryProcessor(new TripleStore()));
        }
    }
}