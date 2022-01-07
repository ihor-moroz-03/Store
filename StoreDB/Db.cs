using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace StoreDB
{
    public class Db
    {
        readonly IDbConnection _dbConnection = new SQLiteConnection();
        readonly IDbCommand _dbCommand = new SQLiteCommand();

        public Db(string connection)
        {
            _dbConnection.ConnectionString = connection;
            _dbCommand.Connection = _dbConnection;
        }

        public Db()
            : this("Data Source = StoreDB.db")
        { }

        public void Query(string query) => Query(query, () => _dbCommand.ExecuteNonQuery());

        public void IterativeReadQuery(string query, Action<IDataRecord> forEach) =>
            Query(query, () =>
            {
                using IDataReader reader = _dbCommand.ExecuteReader();
                while (reader.Read())
                    forEach(reader);
            });

        private void Query(string query, Action action)
        {
            _dbCommand.CommandText = query;
            try
            {
                _dbConnection.Open();
                action();
            }
            catch (DbException e)
            {
                throw new InvalidOperationException($"Something went wrong with db query. Details: {e.Message}");
            }
            finally
            {
                _dbConnection.Close();
            }
        }
    }
}
