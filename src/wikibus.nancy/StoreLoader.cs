using System;
using System.Configuration;
using System.Data.SqlClient;
using VDS.RDF;
using VDS.RDF.Configuration;
using wikibus.sources.dotNetRDF;

namespace wikibus.nancy
{
    /// <summary>
    /// Loades sources store
    /// </summary>
    public class StoreLoader : IObjectFactory
    {
        private static readonly ConnectionStringSettings Sql = ConfigurationManager.ConnectionStrings["sql"];

        /// <summary>
        /// Attempts to load an Object of the given type identified by the given Node and returned as the Type that this loader generates
        /// </summary>
        public bool TryLoadObject(IGraph g, INode objNode, Type targetType, out object obj)
        {
            obj = new SourcesStore(new SqlConnection(Sql.ConnectionString));
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
