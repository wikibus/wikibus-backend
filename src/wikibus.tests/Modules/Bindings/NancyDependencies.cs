using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using JsonLD.Entities;
using Nancy.Testing;
using wikibus.common;
using wikibus.sources;
using wikibus.sources.nancy;

namespace wikibus.tests.Modules.Bindings
{
    public class NancyDependencies
    {
        private readonly IImageResizer _resizer;

        public NancyDependencies()
        {
            Sources = A.Fake<ISourcesRepository>(mock => mock.Strict());
            SourceImages = A.Fake<ISourceImagesRepository>();
            _resizer = A.Dummy<IImageResizer>();
            A.CallTo(() => _resizer.Resize(A<byte[]>.Ignored, A<int>.Ignored))
             .ReturnsLazily(c => c.Arguments.Get<byte[]>(0));

            Browser = new Browser(with => with.Module<SourcesModule>()
                                              .Module<SourceImagesModule>()
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
            yield return _resizer;
        }
    }
}
