using System;
using Nancy.Bootstrapper;
using Nancy.RDF;
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
            mapper.AddNamespace(Vocabularies.Dc.Prefix, new Uri(Vocabularies.Dc.BaseUri));
            mapper.AddNamespace(Vocabularies.DCTerms.Prefix, new Uri(Vocabularies.DCTerms.BaseUri));
            mapper.AddNamespace(Vocabularies.Sch.Prefix, new Uri(Vocabularies.Sch.BaseUri));
            mapper.AddNamespace(Vocabularies.Hydra.Prefix, new Uri(Vocabularies.Hydra.BaseUri));
            mapper.AddNamespace(Vocabularies.Rdf.Prefix, new Uri(Vocabularies.Rdf.BaseUri));
            mapper.AddNamespace(Vocabularies.Rdfs.Prefix, new Uri(Vocabularies.Rdfs.BaseUri));
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
