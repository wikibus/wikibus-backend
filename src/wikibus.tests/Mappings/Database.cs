using System.Configuration;
using System.Data.SqlClient;
using NDbUnit.Core;
using NDbUnit.Core.SqlClient;
using Resourcer;

namespace wikibus.tests.Mappings
{
    /// <summary>
    /// Cleans and initializes test database
    /// </summary>
    public static class Database
    {
        public static readonly string TestConnectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        private static readonly string MasterConnString = ConfigurationManager.ConnectionStrings["master"].ConnectionString;

        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <param name="connection"></param>
        public static INDbUnitTest Initialize(SqlConnection connection)
        {
            SqlDbUnitTest database;
            using (var sqlConnection = new SqlConnection(MasterConnString))
            {
                database = new SqlDbUnitTest(sqlConnection);
                database.Scripts.AddSingle("Scripts\\InitDatabase.sql");
                database.ExecuteScripts();
                database.Scripts.ClearAll();
            }

            database = new SqlDbUnitTest(connection);
            database.Scripts.AddSingle("Scripts\\InitSchema.sql");
            database.Scripts.AddWithWildcard("Scripts", "InitTable_*.sql");
            database.ExecuteScripts();
            database.ReadXmlSchema(Resource.AsStream("Wikibus.xsd"));
            database.PerformDbOperation(DbOperationFlag.DeleteAll);

            return database;
        }
    }
}
