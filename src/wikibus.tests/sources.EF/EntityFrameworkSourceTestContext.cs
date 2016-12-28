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
            var modelTemplateProvider = new ModelTemplateProvider(new TestBaseUriProvider());
            Repository = new SourcesRepository(
                sourceContext,
                new EntityFactory(new UriTemplateExpander(modelTemplateProvider), configuration),
                new UriTemplateMatcher(modelTemplateProvider));
        }

        public SourcesRepository Repository { get; }

        public Source Source { get; set; }
    }
}