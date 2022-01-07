using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StoreDB
{
    public static class DBHelper
    {
        public static IList<IDataRecord> GetList(this Db db, string query)
        {
            List<IDataRecord> records = new();
            db.IterativeReadQuery(query, records.Add);
            return records;
        }

        public static IList<TRecord> GetList<TRecord>(this Db db, string query, Func<IDataRecord, TRecord> generator)
        {
            List<TRecord> records = new();
            db.IterativeReadQuery(query, record => records.Add(generator(record)));
            return records;
        }

        public static IEnumerable<UserModel> GetUsers(this Db db)
        {
            return db.GetList("Select * from User", record => BuildModel<UserModel>(record));
        }

        public static UserModel GetUser(this Db db, string username)
        {
            IDataRecord user = db.GetList("Select * from User").Single(record => username.Equals(record["Username"]));
            return BuildModel<UserModel>(user);
        }

        static TModel BuildModel<TModel>(IDataRecord record) where TModel : IModel, new()
        {
            TModel model = new() { Id = Convert.ToInt32(record["Id"]) };

            foreach (var prop in typeof(TModel).GetProperties())
            {
                prop.SetValue(model, Convert.ChangeType(record[prop.Name], prop.PropertyType));
            }

            return model;
        }
    }
}
