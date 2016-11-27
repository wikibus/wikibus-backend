using Wikibus.Sources;
using Wikibus.Sources.EF;
using Wikibus.Tests.Mappings;

namespace Wikibus.Tests.sources.EF
{
    public class EntityFrameworkSourceTestContext
    {
        public EntityFrameworkSourceTestContext()
        {
            var sourceContext = new SourceContext(Database.TestConnectionString);
            var configuration = new TestConfiguration();
            var identifierTemplates = new IdentifierTemplates(configuration);
            Repository = new SourcesRepository(sourceContext, new IdRetriever(identifierTemplates), new EntityFactory(identifierTemplates, configuration));
        }

        public SourcesRepository Repository { get; }

        public Source Source { get; set; }
    }
}