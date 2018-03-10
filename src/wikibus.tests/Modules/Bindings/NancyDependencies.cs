using System.Collections.Generic;
using System.Linq;
using Argolis.Hydra;
using Argolis.Models;
using Argolis.Models.TunnelVisionLabs;
using FakeItEasy;
using JsonLD.Entities;
using Nancy.Rdf;
using Nancy.Rdf.Contexts;
using Nancy.Testing;
using Wikibus.Common;
using Wikibus.Sources;
using Wikibus.Sources.Nancy;

namespace Wikibus.Tests.Modules.Bindings
{
    public class NancyDependencies
    {
        private readonly IImageResizer resizer;

        public NancyDependencies()
        {
            Sources = A.Fake<ISourcesRepository>(mock => mock.Strict());
            SourceImages = A.Fake<ISourceImagesRepository>();
            resizer = A.Dummy<IImageResizer>();
            A.CallTo(() => resizer.Resize(A<byte[]>.Ignored, A<int>.Ignored))
             .ReturnsLazily(c => c.Arguments.Get<byte[]>(0));

            Browser = new Browser(with => with.Module<SourcesModule>()
                                              .Module<SourceImagesModule>()
                                              .RouteResolver<global::Nancy.Routing.UriTemplates.UriTemplateRouteResolver>()
                                              .Dependency<IUriTemplateExpander>(typeof(TunnelVisionLabsUriTemplateExpander))
                                              .Dependencies(GetDependencies().ToArray()));
        }

        public Browser Browser { get; private set; }

        public ISourcesRepository Sources { get; private set; }

        public ISourceImagesRepository SourceImages { get; private set; }

        private IEnumerable<object> GetDependencies()
        {
            yield return Sources;
            yield return A.Dummy<IEntitySerializer>();
            yield return SourceImages;
            yield return resizer;
            yield return A.Dummy<INamespaceManager>();
            yield return A.Dummy<IContextPathMapper>();
            yield return A.Dummy<IHydraDocumentationSettings>();

            IWikibusConfiguration testConfiguration = new TestConfiguration();
            yield return testConfiguration;
        }
    }
}
