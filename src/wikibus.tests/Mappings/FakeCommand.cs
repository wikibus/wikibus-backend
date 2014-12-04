using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using TechTalk.SpecFlow;

namespace wikibus.tests.Mappings
{
    public class FakeCommand : DbCommand
    {
        private readonly string _tableName;
        private readonly Table _table;

        public FakeCommand(string tableName, Table table)
        {
            _tableName = tableName;
            _table = table;
        }

        public override string CommandText { get; set; }

        public override int CommandTimeout { get; set; }

        public override CommandType CommandType { get; set; }

        public override UpdateRowSource UpdatedRowSource { get; set; }

        public override bool DesignTimeVisible { get; set; }

        protected override DbConnection DbConnection { get; set; }

        protected override DbParameterCollection DbParameterCollection
        {
            get { throw new NotImplementedException(); }
        }

        protected override DbTransaction DbTransaction { get; set; }

        public override void Prepare()
        {
            throw new NotImplementedException();
        }

        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        public override int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public override object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        protected override DbParameter CreateDbParameter()
        {
            throw new NotImplementedException();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            var dataTable = new DataTable(_tableName);
            foreach (var column in _table.Header)
            {
                dataTable.Columns.Add(column);
            }

            foreach (var row in _table.Rows)
            {
                var values = from value in row.Values
                             let val = value == "NULL" ? null : value
                             select (object)val;

                dataTable.LoadDataRow(values.ToArray(), true);
            }

            return new DataTableReader(dataTable);
        }
    }
}
