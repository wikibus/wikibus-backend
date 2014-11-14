using System.IO;
using VDS.RDF;

namespace wikibus.tests.FluentAssertions
{
    public static class AssertionsExtensions
    {
        public static IGraph AsGraph(this string rdf)
        {
            var tripleStore = new Graph();
            tripleStore.LoadFromString(rdf);

            return tripleStore;
        }

        public static IGraph AsGraph(this Stream rdf)
        {
            var tripleStore = new Graph();
            using (var reader = new StreamReader(rdf))
            {
                tripleStore.LoadFromString(reader.ReadToEnd());
            }

            return tripleStore;
        }

        public static IGraph AsGraph(this string rdf, IRdfReader parser)
        {
            var tripleStore = new Graph();
            tripleStore.LoadFromString(rdf, parser);

            return tripleStore;
        }

        public static IGraph AsGraph(this Stream rdf, IRdfReader parser)
        {
            var tripleStore = new Graph();
            using (var reader = new StreamReader(rdf))
            {
                tripleStore.LoadFromString(reader.ReadToEnd(), parser);
            }

            return tripleStore;
        }

        public static StoreAssertions Should(this TripleStore store)
        {
            return new StoreAssertions(store);
        }

        public static StoreAssertions Should(this IGraph store)
        {
            return new StoreAssertions(store);
        }
    }
}
