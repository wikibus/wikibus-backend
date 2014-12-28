using Nancy.Bootstrapper;
using Nancy.RDF;

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
            ////Nancy.RDF.RdfResponses.SetDefaultSerialization(pipelines, RdfSerialization.Turtle);
        }
    }
}
