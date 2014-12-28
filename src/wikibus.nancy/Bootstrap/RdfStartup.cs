using Nancy.Bootstrapper;
using Nancy.RDF;
using Nancy.RDF.Responses;

namespace wikibus.nancy
{
    /// <summary>
    /// Sets-up RDF serialization options
    /// </summary>
    public class RdfStartup : IApplicationStartup
    {
        /// <summary>
        /// Sets Rdf response processing options
        /// </summary>
        public void Initialize(IPipelines pipelines)
        {
            RdfResponses.SetDefaultSerialization(pipelines, RdfSerialization.Turtle);
        }
    }
}
