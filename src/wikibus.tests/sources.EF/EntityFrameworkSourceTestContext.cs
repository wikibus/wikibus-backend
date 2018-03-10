using Argolis.Models;
using Argolis.Models.TunnelVisionLabs;
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
            var templateProvider = new AttributeModelTemplateProvider(configuration);
            Repository = new SourcesRepository(
                sourceContext,
                new EntityFactory(new TunnelVisionLabsUriTemplateExpander(templateProvider), configuration), new TunnelVisionLabsUriTemplateMatcher(templateProvider));
            //Repository = new SourcesRepository(
            //    sourceContext,
            //    new IdRetriever(new TunnelVisionLabsUriTemplateMatcher(templateProvider)),
            //    new EntityFactory(new TunnelVisionLabsUriTemplateExpander(templateProvider), configuration));
        }

        public SourcesRepository Repository { get; }

        public Source Source { get; set; }
    }
}