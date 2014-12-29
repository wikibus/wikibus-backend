using System.Data;
using TCode.r2rml4net;
using VDS.RDF;
using wikibus.common;

namespace wikibus.sources.dotNetRDF
{
    /// <summary>
    /// Store, which loads mapped triples from Sources SQL database
    /// </summary>
    public class SourcesStore : TripleStore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesStore"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="config">The configuration</param>
        public SourcesStore(IDbConnection connection, IWikibusConfiguration config)
        {
            var processor = new W3CR2RMLProcessor(connection);

            processor.GenerateTriples(new WikibusR2RML(config), this);
        }
    }
}
