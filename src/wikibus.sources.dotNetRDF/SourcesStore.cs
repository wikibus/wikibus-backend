﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TCode.r2rml4net;
using VDS.RDF;
using VDS.RDF.Configuration;
using wikibus.sources.dotNetRDF;

namespace wikibus.sources.dotNetRDF
{
    /// <summary>
    /// Store, which loads mapped triples from Sources SQL database
    /// </summary>
    public class SourcesStore : TripleStore
    {
        private readonly IR2RML _mappings = new WikibusR2RML();

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesStore"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public SourcesStore(IDbConnection connection)
        {
            var processor = new W3CR2RMLProcessor(connection);

            processor.GenerateTriples(_mappings, this);
        }
    }
}