using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using VDS.RDF;
using VDS.RDF.Configuration;
using VDS.RDF.Writing;
using wikibus.sources.dotNetRDF;

namespace wikibus.nancy
{
    /// <summary>
    /// Loads sources store
    /// </summary>
    public class StoreLoader : IObjectFactory
    {
        private static readonly ConnectionStringSettings Sql = ConfigurationManager.ConnectionStrings["sql"];
        private readonly string _storePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreLoader"/> class.
        /// </summary>
        /// <param name="storePath">The store path.</param>
        public StoreLoader(string storePath)
        {
            _storePath = storePath;
        }

        /// <summary>
        /// Attempts to load an Object of the given type identified by the given Node and returned as the Type that this loader generates
        /// </summary>
        public bool TryLoadObject(IGraph g, INode objNode, Type targetType, out object obj)
        {
            if (!File.Exists(_storePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_storePath));
                new SourcesStore(new SqlConnection(Sql.ConnectionString)).SaveToFile(_storePath, new TriGWriter());
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