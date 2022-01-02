using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreUI
{
    class DataGridIndexerAdapter
    {
        readonly DataTable _table = new();

        public DataGridIndexerAdapter(string primaryKey)
        {
            _table.Columns.Add(primaryKey);

            _table.PrimaryKey = new[] { _table.Columns[0] };
        }

        public DataGridIndexerAdapter(IEnumerable<string> rows, IEnumerable<string> columns)
        {
            foreach (string column in columns)
                _table.Columns.Add(new DataColumn(column));

            foreach (string row in rows)
                _table.Rows.Add(row);

            _table.PrimaryKey = new[] { _table.Columns[0] };
        }

        public DataView DataView => _table.DefaultView;

        public string this[string row, string column]
        {
            get => _table.Rows.Find(row)[column].ToString();
            set => _table.Rows.Find(row)[column] = value;
        }

        public void AddColumns(IEnumerable<string> columns) =>
            _table.Columns.AddRange(columns.Select(s => new DataColumn(s)).ToArray());

        public void AddRow(IDictionary<string, string> row)
        {
            var newColumns = row.Keys.Where(key => !_table.Columns.Contains(key));
            AddColumns(newColumns);

            _table.Rows.Add(row["Name"]);
            foreach (var pair in row)
            {
                this[row["Name"], pair.Key] = pair.Value;
            }
        }

        public void RemoveRow(string name) =>
            _table.Rows.Remove(_table.Rows.Find(name));
    }
}
