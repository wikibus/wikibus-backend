using System;
using Hydra.Resources;
using JsonLD.Entities;
using JsonLD.Entities.Context;
using Newtonsoft.Json.Linq;
using VDS.RDF;
using VDS.RDF.Query;
using wikibus.sources;

namespace wikibus.tests.SourcesRepository
{
    public class RepositoryScenarioContext
    {
        public RepositoryScenarioContext()
        {
            Store = new TripleStore();
            var contextProvider = new StaticContextProvider();
            var jObject = new JObject(
                "hydra".IsPrefixOf(Hydra.Hydra.BaseUri),
                "member".IsProperty(Hydra.Hydra.member).Container().Set(),
                "totalItems".IsProperty(Hydra.Hydra.totalItems));
            contextProvider.SetContext(typeof(Collection<Book>), jObject);
            var frameProvider = new WikibusModelFrames();
            var serializer = new EntitySerializer(contextProvider, frameProvider);

            Repository = new wikibus.sources.dotNetRDF.SourcesRepository(new Lazy<ISparqlQueryProcessor>(() => new LeviathanQueryProcessor(Store)), serializer);
        }

        public wikibus.sources.dotNetRDF.SourcesRepository Repository { get; private set; }

        public TripleStore Store { get; private set; }
    }
}
