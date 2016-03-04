using System;
using System.Data.SqlClient;
using System.IO;
using VDS.RDF;
using VDS.RDF.Configuration;
using VDS.RDF.Writing;
using wikibus.common;
using wikibus.sources.dotNetRDF;

namespace wikibus.nancy
{
    /// <summary>
    /// Loads sources store
    /// </summary>
    public class StoreLoader : IObjectFactory
    {
        private readonly string _storePath;
        private readonly IWikibusConfiguration _config;
        private readonly ISourcesDatabaseConnectionStringProvider _sourcesDatabaseConnectionStringProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreLoader" /> class.
        /// </summary>
        /// <param name="storePath">The store path.</param>
        /// <param name="config">The configuration</param>
        /// <param name="sourcesDatabaseConnectionStringProvider">The sources database connection string provider.</param>
        public StoreLoader(string storePath, IWikibusConfiguration config, ISourcesDatabaseConnectionStringProvider sourcesDatabaseConnectionStringProvider)
        {
            _storePath = storePath;
            _config = config;
            _sourcesDatabaseConnectionStringProvider = sourcesDatabaseConnectionStringProvider;
        }

        /// <summary>
        /// Attempts to load an Object of the given type identified by the given Node and returned as the Type that this loader generates
        /// </summary>
        public bool TryLoadObject(IGraph g, INode objNode, Type targetType, out object obj)
        {
            if (!File.Exists(_storePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_storePath));
                var sourcesStore = new SourcesStore(new SqlConnection(_sourcesDatabaseConnectionStringProvider.ConnectionString), _config);
                sourcesStore.Initialize();
                sourcesStore.SaveToFile(_storePath, new TriGWriter());

                obj = sourcesStore;
                return true;
            }

            var tripleStore = new TripleStore();
            tripleStore.LoadFromFile(_storePath);
            obj = tripleStore;

            return true;
        }

        /// <summary>
        /// Determines whether this Factory is capable of creating objects of the given type
        /// </summary>
        public bool CanLoadObject(Type t)
        {
            return t == typeof(SourcesStore);
        }
    }
}
