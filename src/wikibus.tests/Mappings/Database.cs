using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlLocalDb;
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
        private const string InstanceName = "WikibusTest";

        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <param name="connection"></param>
        public static INDbUnitTest Initialize(SqlConnection connection)
        {
            if (SqlLocalDbApi.GetInstanceInfo(InstanceName).Exists)
            {
                SqlLocalDbApi.StopInstance(InstanceName, TimeSpan.FromSeconds(10));
                SqlLocalDbApi.DeleteInstance(InstanceName, true);
            }

            SqlLocalDbApi.CreateInstance(InstanceName);

            var database = new SqlDbUnitTest(connection);

            database.Scripts.AddSingle("Scripts\\InitSchema.sql");
            database.Scripts.AddWithWildcard("Scripts", "InitTable_*.sql");
            database.ExecuteScripts();
            database.ReadXmlSchema(Resource.AsStream("Wikibus.xsd"));
            database.PerformDbOperation(DbOperationFlag.DeleteAll);

            return database;
        }
    }
}
