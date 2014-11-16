using System;

namespace wikibus.sources.dotNetRDF
{
    public class SourcesRepository : ISourcesRepository
    {
        public T Get<T>(Uri uri) where T : Source
        {
            return new Brochure
                {
                    Title = "Jelcz M11 - nowość"
                } as T;
        }
    }
}