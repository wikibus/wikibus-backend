using Argolis.Templates;
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
            Repository = new SourcesRepository(
                sourceContext,
                new EntityFactory(new UriTemplateExpander(new TestBaseUriProvider(), new ModelTemplateProvider()), configuration),
                new UriTemplateMatcher(new TestBaseUriProvider()));
        }

        public SourcesRepository Repository { get; }

        public Source Source { get; set; }
    }
}