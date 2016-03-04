using System;
using System.Data.SqlClient;

namespace wikibus.sources.dotNetRDF
{
    /// <summary>
    /// Implements getting images from SQL database
    /// </summary>
    public class SourceImagesRepository : ISourceImagesRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceImagesRepository"/> class.
        /// </summary>
        public SourceImagesRepository(ISourcesDatabaseConnectionStringProvider configuration)
        {
            _connectionString = configuration.ConnectionString;
        }

        /// <summary>
        /// Gets raw image from database
        /// </summary>
        public byte[] GetImageBytes(int sourceId)
        {
            return GetBytes(
                "SELECT Image FROM [Sources].[Source] WHERE Id = @id AND Image is not null",
                new Tuple<string, object>("id", sourceId));
        }

        /// <summary>
        /// Gets raw image from database
        /// </summary>
        public byte[] GetImageBytes(string magazine, string issueNumber)
        {
            return GetBytes(
                @"SELECT Image
                  FROM [Sources].[Source] i
                  JOIN [Sources].[Magazine] m ON m.Id = i.MagIssueMagazine
                  WHERE m.Name = @mag
                    AND i.MagIssueNumber = @issue
                    AND Image is not null",
                new Tuple<string, object>("mag", magazine),
                new Tuple<string, object>("issue", issueNumber));
        }

        private byte[] GetBytes(string query, params Tuple<string, object>[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = query;
                foreach (var parameter in parameters)
                {
                    sqlCommand.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                }

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    return dataReader.GetSqlBinary(0).Value;
                }

                return new byte[0];
            }
        }
    }
}
