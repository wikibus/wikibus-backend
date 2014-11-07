using System;

namespace wikibus.sources
{
    public interface ISourcesRepository
    {
        T Get<T>(Uri uri) where T : Source;
    }
}
