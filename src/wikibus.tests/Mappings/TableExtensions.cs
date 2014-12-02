using System.Collections.Generic;
using Slp.r2rml4net.Storage.Sql.SqlQuery;
using TechTalk.SpecFlow;

namespace wikibus.tests.Mappings
{
    internal static class TableExtensions
    {
        public static StaticDataReader ToStaticDataReader(this Table table)
        {
            IList<StaticDataReaderRow> rows = new List<StaticDataReaderRow>();
            foreach (var tableRow in table.Rows)
            {
                var row = new List<StaticDataReaderColumn>();
                foreach (var key in tableRow.Keys)
                {
                    row.Add(new StaticDataReaderColumn(key, tableRow[key]));
                }

                rows.Add(new StaticDataReaderRow(row));
            }

            var result = new StaticDataReader(rows);
            return result;
        }
    }
}
