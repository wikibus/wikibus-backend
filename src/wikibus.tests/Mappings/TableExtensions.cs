using System.Data;
using TechTalk.SpecFlow;

namespace Wikibus.Tests.Mappings
{
    /// <summary>
    /// Table extension to populate test database
    /// </summary>
    public static class TableExtensions
    {
        /// <summary>
        /// Converts <paramref name="table"/> to <see cref="DataSet"/>
        /// </summary>
        public static DataSet ToDataSet(this Table table, string name)
        {
            DataSet dataSet = new DataSet("Wikibus")
            {
                Namespace = "http://tempuri.org/Wikibus.xsd"
            };
            var dataTable = new DataTable(name);
            dataSet.Tables.Add(dataTable);

            foreach (var colname in table.Header)
            {
                dataTable.Columns.Add(colname);
            }

            foreach (var row in table.Rows)
            {
                var sourcesSourceRow = dataTable.NewRow();
                foreach (var cell in row)
                {
                    if (cell.Value != "NULL")
                    {
                        sourcesSourceRow[cell.Key] = cell.Value;
                    }
                }

                dataTable.Rows.Add(sourcesSourceRow);
            }

            dataSet.AcceptChanges();
            return dataSet;
        }
    }
}
