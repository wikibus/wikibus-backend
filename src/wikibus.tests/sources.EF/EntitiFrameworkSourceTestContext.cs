using wikibus.sources;
using wikibus.sources.EF;
using wikibus.tests.Mappings;

namespace wikibus.tests.sources.EF
{
    public class EntitiFrameworkSourceTestContext
    {
        public EntitiFrameworkSourceTestContext()
        {
            var sourceContext = new SourceContext(Database.TestConnectionString);
            var identifierTemplates = new IdentifierTemplates(new TestConfiguration());
            Repository = new wikibus.sources.EF.SourcesRepository(sourceContext, new IdRetriever(identifierTemplates), new EntityFactory(identifierTemplates));
        }

        public wikibus.sources.EF.SourcesRepository Repository { get; }

        public Source Source { get; set; }
    }
}