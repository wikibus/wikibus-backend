using System.Data;
using TechTalk.SpecFlow;

namespace wikibus.tests.Mappings
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
            DataSet dataSet = new Wikibus();
            var dataTable = dataSet.Tables[name];

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
