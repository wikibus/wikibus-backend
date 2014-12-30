using Nancy.Bootstrapper;
using Nancy.RDF;
using Nancy.RDF.Responses;
using VDS.RDF;

namespace wikibus.nancy
{
    /// <summary>
    /// Sets-up RDF serialization options
    /// </summary>
    public class NancyRdfStartup : IApplicationStartup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NancyRdfStartup"/> class.
        /// </summary>
        public NancyRdfStartup(INamespaceMapper mapper)
        {
            mapper.Import(new NamespaceMapper());
            mapper.AddNamespace("");
        }

        /// <summary>
        /// Sets Rdf response processing options
        /// </summary>
        public void Initialize(IPipelines pipelines)
        {
            RdfResponses.SetDefaultSerialization(pipelines, RdfSerialization.Turtle);
        }
    }
}
