using System;
using Nancy.Bootstrapper;
using Nancy.RDF;
using VDS.RDF;
using wikibus.common.Vocabularies;

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
            mapper.AddNamespace(Dc.Prefix, new Uri(Dc.BaseUri));
            mapper.AddNamespace(DCTerms.Prefix, new Uri(DCTerms.BaseUri));
            mapper.AddNamespace(Schema.Prefix, new Uri(Schema.BaseUri));
            mapper.AddNamespace(common.Vocabularies.Hydra.Prefix, new Uri(common.Vocabularies.Hydra.BaseUri));
            mapper.AddNamespace(Rdf.Prefix, new Uri(Rdf.BaseUri));
            mapper.AddNamespace(Rdfs.Prefix, new Uri(Rdfs.BaseUri));
            mapper.AddNamespace("wbo", new Uri("http://wikibus.org/ontology#"));
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
