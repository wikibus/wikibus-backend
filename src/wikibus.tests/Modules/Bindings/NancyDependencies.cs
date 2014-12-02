﻿using FakeItEasy;
using JsonLD.Entities;
using Nancy.Testing;
using wikibus.sources;
using wikibus.sources.nancy;

namespace wikibus.tests.Modules.Bindings
{
    public class NancyDependencies
    {
        public NancyDependencies()
        {
            Sources = A.Fake<ISourcesRepository>(mock => mock.Strict());
            Browser = new Browser(with => with.Module<SourcesModule>()
                                              .Dependencies(Sources, A.Dummy<IEntitySerializer>()));
        }

        public Browser Browser { get; private set; }

        public ISourcesRepository Sources { get; private set; }
    }
}
