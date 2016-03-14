using System;
using Nancy.Bootstrapper;
using Nancy.Rdf;
using wikibus.common;
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
        public NancyRdfStartup(INamespaceManager mapper, IWikibusConfiguration config)
        {
            mapper.SetBaseUri(new Uri(config.BaseResourceNamespace));

            mapper.AddNamespace(Dc.Prefix, new Uri(Dc.BaseUri));
            mapper.AddNamespace(DCTerms.Prefix, new Uri(DCTerms.BaseUri));
            mapper.AddNamespace(Schema.Prefix, new Uri(Schema.BaseUri));
            mapper.AddNamespace(global::Hydra.Hydra.Prefix, new Uri(global::Hydra.Hydra.BaseUri));
            mapper.AddNamespace(Rdf.Prefix, new Uri(Rdf.BaseUri));
            mapper.AddNamespace(Rdfs.Prefix, new Uri(Rdfs.BaseUri));
            mapper.AddNamespace(Bibo.Prefix, new Uri(Bibo.BaseUri));
            mapper.AddNamespace(Opus.Prefix, new Uri(Opus.BaseUri));
            mapper.AddNamespace("lexvo", new Uri(Lexvo.iso639_1));
            mapper.AddNamespace(Wbo.Prefix, new Uri(Wbo.BaseUri));
        }

        /// <summary>
        /// Sets Rdf response processing options
        /// </summary>
        public void Initialize(IPipelines pipelines)
        {
            RdfResponses.SetDefaultSerialization(pipelines, RdfSerialization.JsonLd);
        }
    }
}
