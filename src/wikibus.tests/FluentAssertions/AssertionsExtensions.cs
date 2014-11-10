using System.IO;
using VDS.RDF;

namespace wikibus.tests.FluentAssertions
{
    public static class AssertionsExtensions
    {
        public static TripleStore AsRdf(this string rdf)
        {
            var tripleStore = new TripleStore();
            tripleStore.LoadFromString(rdf);

            return tripleStore;
        }

        public static TripleStore AsRdf(this Stream rdf)
        {
            var tripleStore = new TripleStore();
            using (var reader = new StreamReader(rdf))
            {
                tripleStore.LoadFromString(reader.ReadToEnd());
            }

            return tripleStore;
        }

        public static StoreAssertions Should(this TripleStore store)
        {
            return new StoreAssertions(store);
        }
    }
}
