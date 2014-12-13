using System.Data;
using System.Linq;
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
            var dataSet = new DataSet();
            var dataTable = new DataTable(name);

            foreach (var column in table.Header)
            {
                dataTable.Columns.Add(column);
            }

            foreach (var row in table.Rows)
            {
                var values = from value in row.Values
                             let val = value == "NULL" ? null : value
                             select (object)val;

                dataTable.LoadDataRow(values.ToArray(), true);
            }

            dataSet.Tables.Add(dataTable);
            return dataSet;
        }
    }
}