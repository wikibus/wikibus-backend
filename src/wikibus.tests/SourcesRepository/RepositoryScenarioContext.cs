using JsonLD.Entities;
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
            var frameProvider = new StaticFrameProvider();
            frameProvider.SetupSourcesFrames();
            var serializer = new EntitySerializer(contextProvider, frameProvider);
            var config = new TestConfiguration();

            Repository = new sources.dotNetRDF.SourcesRepository(new LeviathanQueryProcessor(Store), serializer, config);
        }

        public sources.dotNetRDF.SourcesRepository Repository { get; private set; }

        public TripleStore Store { get; private set; }
    }
}
