using wikibus.tests.Mappings;
using Wikibus.Sources;
using Wikibus.Sources.EF;

namespace wikibus.tests.sources.EF
{
    public class EntitiFrameworkSourceTestContext
    {
        public EntitiFrameworkSourceTestContext()
        {
            var sourceContext = new SourceContext(Database.TestConnectionString);
            var identifierTemplates = new IdentifierTemplates(new TestConfiguration());
            Repository = new Wikibus.Sources.EF.SourcesRepository(sourceContext, new IdRetriever(identifierTemplates), new EntityFactory(identifierTemplates));
        }

        public Wikibus.Sources.EF.SourcesRepository Repository { get; }

        public Source Source { get; set; }
    }
}