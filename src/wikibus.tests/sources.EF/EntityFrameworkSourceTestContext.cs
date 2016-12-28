using Argolis.Models;
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
            var modelTemplateProvider = new AttributeModelTemplateProvider(new TestBaseUriProvider());
            Repository = new SourcesRepository(
                sourceContext,
                new EntityFactory(new TunnelVisionLabsUriTemplateExpander(modelTemplateProvider), configuration),
                new TunnelVisionLabsUriTemplateMatcher(modelTemplateProvider));
        }

        public SourcesRepository Repository { get; }

        public Source Source { get; set; }
    }
}