using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Dynamic;
using System.Configuration;
using System.Reflection;

namespace Util
{
    public static class DBHelper
    {
        static readonly string strConnection = ConfigurationManager.AppSettings["MySql"];

        #region async
        public static async Task<int> Insert(string table, IDictionary<string, string> dict)
        {
            if (dict.Count > 0)
            {
                SqlPara sp = GetInsert(table, dict);
                return await ExecuteSql(sp.sql, sp.para);
            }
            return 0;
        }
        public static async Task<int> Insert(string table, IDictionary<string, string> dict, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (dict.Count > 0)
            {
                SqlPara sp = GetInsert(table, dict);
                return await ExecuteSql(sp.sql, sp.para, dbConnection, trans);
            }
            return 0;
        }
        public static async Task<int> Update(string table, IDictionary<string, string> dict, IDictionary<string, string> fdict, string andor)
        {
            if (dict.Count > 0 && fdict.Count > 0)
            {
                SqlPara sp = GetUpdate(table, dict, fdict, andor);
                return await ExecuteSql(sp.sql, sp.para);
            }
            return 0;
        }
        public static async Task<int> Update(string table, IDictionary<string, string> dict, IDictionary<string, string> fdict, string andor, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (dict.Count > 0 && fdict.Count > 0)
            {
                SqlPara sp = GetUpdate(table, dict, fdict, andor);
                return await ExecuteSql(sp.sql, sp.para, dbConnection, trans);
            }
            return 0;
        }
        public static async Task<int> Delete(string table, IDictionary<string, string> fdict, string andor)
        {
            if (fdict.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("delete from ");
                builder.Append(table);
                SqlPara sp = GetWhere(table, fdict, andor);
                builder.Append(sp.sql);
                return await ExecuteSql(builder.ToString(), sp.para);
            }
            return 0;
        }
        public static async Task<int> Delete(string table, IDictionary<string, string> fdict, string andor, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (fdict.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("delete from ");
                builder.Append(table);
                SqlPara sp = GetWhere(table, fdict, andor);
                builder.Append(sp.sql);
                return await ExecuteSql(builder.ToString(), sp.para, dbConnection, trans);
            }
            return 0;
        }
        public static async Task<ulong> GetNewId(MySqlConnection dbConnection, MySqlTransaction trans)
        {
            return (await dbConnection.QueryAsync<ulong>("select LAST_INSERT_ID()", null, trans)).Single();
        }
        public static async Task<T> GetOne<T>(string id) where T : class
        {
            T obj = await Redis.Get<T>(id);
            if (obj == null)
            {
                using (MySqlConnection dbConnection = await GetOpenCon())
                {
                    obj = await dbConnection.GetAsync<T>(id);
                }
                if (obj != null) await Redis.Insert<T>(id, obj);
            }
            return obj;
        }
        public static async Task<T> GetOne<T, A>(string table, IDictionary<string, string> fdict, string andor) where T : class
        {
            string key = MakeMultiIdKey(table, fdict, andor);
            string id = await Redis.GetString(key);
            if (string.IsNullOrWhiteSpace(id))
            {
                IEnumerable<T> list = await GetList<T, A>(table, "*", string.Empty, fdict, andor);
                if (list.Count() == 0) return null;
                T obj = list.First<T>();
                await Redis.InsertString(key, typeof(T).GetProperty("id").GetValue(obj).ToString());
                return obj;
            }
            else
            {
                T obj = await GetOne<T>(id);
                if (obj == null)
                {
                    await Redis.DeleteKey(key);
                }
                return obj;
            }
        }
        public static async Task<long> GetCount(string table, IDictionary<string, string> fdict, string andor)
        {
            SqlPara sp = GetWhere(table, fdict, andor);
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(*) from ");
            builder.Append(table);
            MySqlParameter[] parameters = null;
            builder.Append(sp.sql);
            parameters = sp.para;
            var template = MakeSBTemp(builder.ToString(), parameters);
            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                return (await dbConnection.QueryAsync<long>(template.RawSql, template.Parameters)).SingleOrDefault();
            }
        }
        public static async Task<long> GetCountFree(string sql)
        {
            var template = MakeSBTemp(sql, new MySqlParameter[] { });
            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                return (await dbConnection.QueryAsync<long>(template.RawSql, template.Parameters)).SingleOrDefault();
            }
        }
        public static async Task<T> GetSumFree<T>(string sql)
        {
            var template = MakeSBTemp(sql, new MySqlParameter[] { });
            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                return (await dbConnection.QueryAsync<T>(template.RawSql, template.Parameters)).SingleOrDefault();
            }
        }
        public static async Task<long> GetCount(string table, IDictionary<string, string> fdict, string andor, MySqlConnection dbConnection)
        {
            SqlPara sp = GetWhere(table, fdict, andor);
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(*) from ");
            builder.Append(table);
            builder.Append(sp.sql);
            MySqlParameter[] parameters = sp.para;
            var template = MakeSBTemp(builder.ToString(), parameters);
            return (await dbConnection.QueryAsync<long>(template.RawSql, template.Parameters)).SingleOrDefault();
        }
        public static async Task<IEnumerable<T>> GetList<T, A>(string table, string getter, string order, IDictionary<string, string> fdict, string andor, bool redis = true) where T : class
        {
            SqlPara sp = GetWhere(table, fdict, andor);
            StringBuilder builder = new StringBuilder();
            builder.Append("select id from ");
            builder.Append(table);
            builder.Append(sp.sql);
            if (order.Length > 0)
            {
                builder.Append(" order by ");
                builder.Append(order);
            }
            MySqlParameter[] parameters = sp.para;
            var template = MakeSBTemp(builder.ToString(), parameters);
            IDictionary<ulong, T> dictT = new Dictionary<ulong, T>();
            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                IEnumerable<A> ids = await dbConnection.QueryAsync<A>(template.RawSql, template.Parameters);
                if (ids.Count() > 0)
                {
                    IList<ulong> nList = new List<ulong>();
                    foreach (A id in ids)
                    {
                        ulong uid = 0;
                        if (ulong.TryParse(id.ToString(), out uid))
                        {
                            T obj = (redis ? await Redis.Get<T>(id.ToString()) : null);
                            dictT.Add(uid, obj);
                            if (obj == null) nList.Add(uid);
                        }
                    }

                    if (nList.Count > 0)
                    {
                        StringBuilder filter = new StringBuilder();
                        foreach (ulong id in nList)
                        {
                            filter.Append(",");
                            filter.Append(id);
                        }
                        builder = new StringBuilder();
                        builder.Append("select ");
                        builder.Append(getter);
                        builder.Append(" from ");
                        builder.Append(table);
                        builder.Append(" where id in (");
                        builder.Append(filter.ToString().Substring(1));
                        builder.Append(")");
                        if (order.Length > 0)
                        {
                            builder.Append(" order by ");
                            builder.Append(order);
                        }
                        IEnumerable<T> list = await dbConnection.QueryAsync<T>(builder.ToString());

                        var type = typeof(T);
                        foreach (T obj in list)
                        {
                            PropertyInfo pi = type.GetProperty("id");
                            if (pi != null)
                            {
                                object val = pi.GetValue(obj);
                                if (val != null)
                                {
                                    ulong id = 0;
                                    if (ulong.TryParse(val.ToString(), out id))
                                    {
                                        dictT[id] = obj;
                                        if (redis)
                                            await Redis.Insert<T>(id.ToString(), obj);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (dictT.Count > 0)
            {
                IList<T> list = new List<T>();
                foreach (ulong id in dictT.Keys)
                {
                    list.Add(dictT[id]);
                }
                return list;
            }
            return new List<T>();
        }
        public static async Task<IEnumerable<T>> GetList<T, A>(string table, string getter, string order, IDictionary<string, string> fdict, string andor, int pageSize, int pageIndex, bool redis = true) where T : class
        {
            if (pageSize == 0) return await GetList<T, A>(table, getter, order, fdict, andor);
            pageIndex--;
            SqlPara sp = GetWhere(table, fdict, andor);
            StringBuilder builder = new StringBuilder();
            builder.Append("select id from ");
            builder.Append(table);
            builder.Append(sp.sql);
            if (order.Length > 0)
            {
                builder.Append(" order by ");
                builder.Append(order);
            }
            builder.Append(" limit ");
            builder.Append((pageIndex * pageSize).ToString());
            builder.Append(",");
            builder.Append(pageSize.ToString());
            MySqlParameter[] parameters = sp.para;
            var template = MakeSBTemp(builder.ToString(), parameters);
            IDictionary<ulong, T> dictT = new Dictionary<ulong, T>();
            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                IEnumerable<A> ids = (await dbConnection.QueryAsync<A>(template.RawSql, template.Parameters));
                if (ids.Count() > 0)
                {
                    IList<ulong> nList = new List<ulong>();
                    foreach (A id in ids)
                    {
                        ulong uid = 0;
                        if (ulong.TryParse(id.ToString(), out uid))
                        {
                            T obj = (redis ? await Redis.Get<T>(id.ToString()) : null);
                            dictT.Add(uid, obj);
                            if (obj == null) nList.Add(uid);
                        }
                    }

                    if (nList.Count > 0)
                    {
                        StringBuilder filter = new StringBuilder();
                        foreach (ulong id in nList)
                        {
                            filter.Append(",");
                            filter.Append(id);
                        }
                        builder = new StringBuilder();
                        builder.Append("select ");
                        builder.Append(getter);
                        builder.Append(" from ");
                        builder.Append(table);
                        builder.Append(" where id in (");
                        builder.Append(filter.ToString().Substring(1));
                        builder.Append(")");
                        if (order.Length > 0)
                        {
                            builder.Append(" order by ");
                            builder.Append(order);
                        }
                        IEnumerable<T> list = await dbConnection.QueryAsync<T>(builder.ToString());

                        var type = typeof(T);
                        foreach (T obj in list)
                        {
                            PropertyInfo pi = type.GetProperty("id");
                            if (pi != null)
                            {
                                object val = pi.GetValue(obj);
                                if (val != null)
                                {
                                    ulong id = 0;
                                    if (ulong.TryParse(val.ToString(), out id))
                                    {
                                        dictT[id] = obj;
                                        if (redis)
                                            await Redis.Insert<T>(id.ToString(), obj);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (dictT.Count > 0)
            {
                IList<T> list = new List<T>();
                foreach (ulong id in dictT.Keys)
                {
                    list.Add(dictT[id]);
                }
                return list;
            }
            return new List<T>();
        }
        public static async Task<IEnumerable<T>> GetFree<T>(string sql)
        {
            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                return await dbConnection.QueryAsync<T>(sql, null);
            }
        }
        public static async Task<IEnumerable<T>> GetFreeList<T, A>(string sql, string table, string getter, string order, int pageSize, int pageIndex, bool redis = true) where T : class
        {
            if (pageSize == 0) return await GetFree<T>(sql.Replace("select id from", "select * from"));
            pageIndex--;
            StringBuilder builder = new StringBuilder();
            builder.Append(sql);
            builder.Append(" limit ");
            builder.Append((pageIndex * pageSize).ToString());
            builder.Append(",");
            builder.Append(pageSize.ToString());
            IDictionary<ulong, T> dictT = new Dictionary<ulong, T>();
            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                IEnumerable<A> ids = await dbConnection.QueryAsync<A>(builder.ToString());
                if (ids.Count() > 0)
                {
                    IList<ulong> nList = new List<ulong>();
                    foreach (A id in ids)
                    {
                        ulong uid = 0;
                        if (ulong.TryParse(id.ToString(), out uid))
                        {
                            T obj = (redis ? await Redis.Get<T>(id.ToString()) : null);
                            dictT.Add(uid, obj);
                            if (obj == null) nList.Add(uid);
                        }
                    }

                    if (nList.Count > 0)
                    {
                        StringBuilder filter = new StringBuilder();
                        foreach (ulong id in nList)
                        {
                            filter.Append(",");
                            filter.Append(id);
                        }
                        builder = new StringBuilder();
                        builder.Append("select ");
                        builder.Append(getter);
                        builder.Append(" from ");
                        builder.Append(table);
                        builder.Append(" where id in (");
                        builder.Append(filter.ToString().Substring(1));
                        builder.Append(")");
                        if (order.Length > 0)
                        {
                            builder.Append(" order by ");
                            builder.Append(order);
                        }
                        IEnumerable<T> list = await dbConnection.QueryAsync<T>(builder.ToString());

                        var type = typeof(T);
                        foreach (T obj in list)
                        {
                            PropertyInfo pi = type.GetProperty("id");
                            if (pi != null)
                            {
                                object val = pi.GetValue(obj);
                                if (val != null)
                                {
                                    ulong id = 0;
                                    if (ulong.TryParse(val.ToString(), out id))
                                    {
                                        dictT[id] = obj;
                                        if (redis)
                                            await Redis.Insert<T>(id.ToString(), obj);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (dictT.Count > 0)
            {
                IList<T> list = new List<T>();
                foreach (ulong id in dictT.Keys)
                {
                    list.Add(dictT[id]);
                }
                return list;
            }
            return new List<T>();
        }
        #endregion
        #region sync
        public static int InsertSync(string table, IDictionary<string, string> dict)
        {
            if (dict.Count > 0)
            {
                SqlPara sp = GetInsert(table, dict);
                return ExecuteSqlSync(sp.sql, sp.para);
            }
            return 0;
        }
        public static int InsertSync(string table, IDictionary<string, string> dict, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (dict.Count > 0)
            {
                SqlPara sp = GetInsert(table, dict);
                return ExecuteSqlSync(sp.sql, sp.para, dbConnection, trans);
            }
            return 0;
        }
        public static int UpdateSync(string table, IDictionary<string, string> dict, IDictionary<string, string> fdict, string andor)
        {
            if (dict.Count > 0 && fdict.Count > 0)
            {
                SqlPara sp = GetUpdate(table, dict, fdict, andor);
                return ExecuteSqlSync(sp.sql, sp.para);
            }
            return 0;
        }
        public static int UpdateSync(string table, IDictionary<string, string> dict, IDictionary<string, string> fdict, string andor, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (dict.Count > 0 && fdict.Count > 0)
            {
                SqlPara sp = GetUpdate(table, dict, fdict, andor);
                return ExecuteSqlSync(sp.sql, sp.para, dbConnection, trans);
            }
            return 0;
        }
        public static int DeleteSync(string table, IDictionary<string, string> fdict, string andor)
        {
            if (fdict.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("delete from ");
                builder.Append(table);
                SqlPara sp = GetWhere(table, fdict, andor);
                builder.Append(sp.sql);
                return ExecuteSqlSync(builder.ToString(), sp.para);
            }
            return 0;
        }
        public static int DeleteSync(string table, IDictionary<string, string> fdict, string andor, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (fdict.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("delete from ");
                builder.Append(table);
                SqlPara sp = GetWhere(table, fdict, andor);
                builder.Append(sp.sql);
                return ExecuteSqlSync(builder.ToString(), sp.para, dbConnection, trans);
            }
            return 0;
        }
        public static ulong GetNewIdSync(MySqlConnection dbConnection, MySqlTransaction trans)
        {
            return (dbConnection.Query<ulong>("select LAST_INSERT_ID()", null, trans)).Single();
        }
        public static T GetOneSync<T>(string id) where T : class
        {
            T obj = Redis.GetSync<T>(id);
            if (obj == null)
            {
                using (MySqlConnection dbConnection = GetOpenConSync())
                {
                    obj = dbConnection.Get<T>(id);
                }
                if (obj != null) Redis.InsertSync<T>(id, obj);
            }
            return obj;
        }
        public static T GetOneSync<T, A>(string table, IDictionary<string, string> fdict, string andor) where T : class
        {
            string key = MakeMultiIdKey(table, fdict, andor);
            string id = Redis.GetStringSync(key);
            if (string.IsNullOrWhiteSpace(id))
            {
                IEnumerable<T> list = GetListSync<T, A>(table, "*", string.Empty, fdict, andor);
                if (list.Count() == 0) return null;
                T obj = list.First<T>();
                Redis.InsertStringSync(key, typeof(T).GetProperty("id").GetValue(obj).ToString());
                return obj;
            }

            else
            {
                T obj = GetOneSync<T>(id);
                if (obj == null)
                {
                    Redis.DeleteKeySync(key);
                }
                return obj;
            }
        }
        public static long GetCountSync(string table, IDictionary<string, string> fdict, string andor)
        {
            SqlPara sp = GetWhere(table, fdict, andor);
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(*) from ");
            builder.Append(table);
            MySqlParameter[] parameters = null;
            builder.Append(sp.sql);
            parameters = sp.para;
            var template = MakeSBTemp(builder.ToString(), parameters);
            using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
            {
                return (dbConnection.Query<long>(template.RawSql, template.Parameters)).SingleOrDefault();
            }
        }
        public static long GetCountFreeSync(string sql)
        {
            var template = MakeSBTemp(sql, new MySqlParameter[] { });
            using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
            {
                return (dbConnection.Query<long>(template.RawSql, template.Parameters)).SingleOrDefault();
            }
        }
        public static T GetSumFreeSync<T>(string sql)
        {
            var template = MakeSBTemp(sql, new MySqlParameter[] { });
            using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
            {
                return (dbConnection.Query<T>(template.RawSql, template.Parameters)).SingleOrDefault();
            }
        }
        public static long GetCountSync(string table, IDictionary<string, string> fdict, string andor, MySqlConnection dbConnection)
        {
            SqlPara sp = GetWhere(table, fdict, andor);
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(*) from ");
            builder.Append(table);
            builder.Append(sp.sql);
            MySqlParameter[] parameters = sp.para;
            var template = MakeSBTemp(builder.ToString(), parameters);
            return (dbConnection.Query<long>(template.RawSql, template.Parameters)).SingleOrDefault();
        }
        public static IEnumerable<T> GetListSync<T, A>(string table, string getter, string order, IDictionary<string, string> fdict, string andor, bool redis = true) where T : class
        {
            SqlPara sp = GetWhere(table, fdict, andor);
            StringBuilder builder = new StringBuilder();
            builder.Append("select id from ");
            builder.Append(table);
            builder.Append(sp.sql);
            if (order.Length > 0)
            {
                builder.Append(" order by ");
                builder.Append(order);
            }
            MySqlParameter[] parameters = sp.para;
            var template = MakeSBTemp(builder.ToString(), parameters);
            IDictionary<ulong, T> dictT = new Dictionary<ulong, T>();
            using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
            {
                IEnumerable<A> ids = dbConnection.Query<A>(template.RawSql, template.Parameters);
                if (ids.Count() > 0)
                {
                    IList<ulong> nList = new List<ulong>();
                    foreach (A id in ids)
                    {
                        ulong uid = 0;
                        if (ulong.TryParse(id.ToString(), out uid))
                        {
                            T obj = (redis ? Redis.GetSync<T>(id.ToString()) : null);
                            dictT.Add(uid, obj);
                            if (obj == null) nList.Add(uid);
                        }
                    }

                    if (nList.Count > 0)
                    {
                        StringBuilder filter = new StringBuilder();
                        foreach (ulong id in nList)
                        {
                            filter.Append(",");
                            filter.Append(id);
                        }
                        builder = new StringBuilder();
                        builder.Append("select ");
                        builder.Append(getter);
                        builder.Append(" from ");
                        builder.Append(table);
                        builder.Append(" where id in (");
                        builder.Append(filter.ToString().Substring(1));
                        builder.Append(")");
                        if (order.Length > 0)
                        {
                            builder.Append(" order by ");
                            builder.Append(order);
                        }
                        IEnumerable<T> list = dbConnection.Query<T>(builder.ToString());

                        var type = typeof(T);
                        foreach (T obj in list)
                        {
                            PropertyInfo pi = type.GetProperty("id");
                            if (pi != null)
                            {
                                object val = pi.GetValue(obj);
                                if (val != null)
                                {
                                    ulong id = 0;
                                    if (ulong.TryParse(val.ToString(), out id))
                                    {
                                        dictT[id] = obj;
                                        if (redis)
                                            Redis.InsertSync<T>(id.ToString(), obj);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (dictT.Count > 0)
            {
                IList<T> list = new List<T>();
                foreach (ulong id in dictT.Keys)
                {
                    list.Add(dictT[id]);
                }
                return list;
            }
            return new List<T>();
        }
        public static IEnumerable<T> GetListSync<T, A>(string table, string getter, string order, IDictionary<string, string> fdict, string andor, int pageSize, int pageIndex, bool redis = true) where T : class
        {
            if (pageSize == 0) return GetListSync<T, A>(table, getter, order, fdict, andor);
            pageIndex--;
            SqlPara sp = GetWhere(table, fdict, andor);
            StringBuilder builder = new StringBuilder();
            builder.Append("select id from ");
            builder.Append(table);
            builder.Append(sp.sql);
            if (order.Length > 0)
            {
                builder.Append(" order by ");
                builder.Append(order);
            }
            builder.Append(" limit ");
            builder.Append((pageIndex * pageSize).ToString());
            builder.Append(",");
            builder.Append(pageSize.ToString());
            MySqlParameter[] parameters = sp.para;
            var template = MakeSBTemp(builder.ToString(), parameters);
            IDictionary<ulong, T> dictT = new Dictionary<ulong, T>();
            using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
            {
                IEnumerable<A> ids = (dbConnection.Query<A>(template.RawSql, template.Parameters));
                if (ids.Count() > 0)
                {
                    IList<ulong> nList = new List<ulong>();
                    foreach (A id in ids)
                    {
                        ulong uid = 0;
                        if (ulong.TryParse(id.ToString(), out uid))
                        {
                            T obj = (redis ? Redis.GetSync<T>(id.ToString()) : null);
                            dictT.Add(uid, obj);
                            if (obj == null) nList.Add(uid);
                        }
                    }

                    if (nList.Count > 0)
                    {
                        StringBuilder filter = new StringBuilder();
                        foreach (ulong id in nList)
                        {
                            filter.Append(",");
                            filter.Append(id);
                        }
                        builder = new StringBuilder();
                        builder.Append("select ");
                        builder.Append(getter);
                        builder.Append(" from ");
                        builder.Append(table);
                        builder.Append(" where id in (");
                        builder.Append(filter.ToString().Substring(1));
                        builder.Append(")");
                        if (order.Length > 0)
                        {
                            builder.Append(" order by ");
                            builder.Append(order);
                        }
                        IEnumerable<T> list = dbConnection.Query<T>(builder.ToString());

                        var type = typeof(T);
                        foreach (T obj in list)
                        {
                            PropertyInfo pi = type.GetProperty("id");
                            if (pi != null)
                            {
                                object val = pi.GetValue(obj);
                                if (val != null)
                                {
                                    ulong id = 0;
                                    if (ulong.TryParse(val.ToString(), out id))
                                    {
                                        dictT[id] = obj;
                                        if (redis)
                                            Redis.InsertSync<T>(id.ToString(), obj);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (dictT.Count > 0)
            {
                IList<T> list = new List<T>();
                foreach (ulong id in dictT.Keys)
                {
                    list.Add(dictT[id]);
                }
                return list;
            }
            return new List<T>();
        }
        public static IEnumerable<T> GetFreeSync<T>(string sql)
        {
            using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
            {
                return dbConnection.Query<T>(sql, null);
            }
        }
        public static IEnumerable<T> GetFreeListSync<T, A>(string sql, string table, string getter, string order, int pageSize, int pageIndex, bool redis = true) where T : class
        {
            if (pageSize == 0) return GetFreeSync<T>(sql.Replace("select id from", "select * from"));
            pageIndex--;
            StringBuilder builder = new StringBuilder();
            builder.Append(sql);
            builder.Append(" limit ");
            builder.Append((pageIndex * pageSize).ToString());
            builder.Append(",");
            builder.Append(pageSize.ToString());
            IDictionary<ulong, T> dictT = new Dictionary<ulong, T>();
            using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
            {
                IEnumerable<A> ids = dbConnection.Query<A>(builder.ToString());
                if (ids.Count() > 0)
                {
                    IList<ulong> nList = new List<ulong>();
                    foreach (A id in ids)
                    {
                        ulong uid = 0;
                        if (ulong.TryParse(id.ToString(), out uid))
                        {
                            T obj = (redis ? Redis.GetSync<T>(id.ToString()) : null);
                            dictT.Add(uid, obj);
                            if (obj == null) nList.Add(uid);
                        }
                    }

                    if (nList.Count > 0)
                    {
                        StringBuilder filter = new StringBuilder();
                        foreach (ulong id in nList)
                        {
                            filter.Append(",");
                            filter.Append(id);
                        }
                        builder = new StringBuilder();
                        builder.Append("select ");
                        builder.Append(getter);
                        builder.Append(" from ");
                        builder.Append(table);
                        builder.Append(" where id in (");
                        builder.Append(filter.ToString().Substring(1));
                        builder.Append(")");
                        if (order.Length > 0)
                        {
                            builder.Append(" order by ");
                            builder.Append(order);
                        }
                        IEnumerable<T> list = dbConnection.Query<T>(builder.ToString());

                        var type = typeof(T);
                        foreach (T obj in list)
                        {
                            PropertyInfo pi = type.GetProperty("id");
                            if (pi != null)
                            {
                                object val = pi.GetValue(obj);
                                if (val != null)
                                {
                                    ulong id = 0;
                                    if (ulong.TryParse(val.ToString(), out id))
                                    {
                                        dictT[id] = obj;
                                        if (redis)
                                            Redis.InsertSync<T>(id.ToString(), obj);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (dictT.Count > 0)
            {
                IList<T> list = new List<T>();
                foreach (ulong id in dictT.Keys)
                {
                    list.Add(dictT[id]);
                }
                return list;
            }
            return new List<T>();
        }
        #endregion
        #region basic
        static object GetDBValue(object obj)
        {
            if (obj == null) return DBNull.Value;
            string val = obj.ToString();
            if (val.Length == 0) return DBNull.Value;
            return val;
        }
        static string RemoveDupCol(string col)
        {
            string[] cols = col.Split(new string[] { "重复字段" }, StringSplitOptions.RemoveEmptyEntries);
            if (cols.Length == 2) return cols[1];
            return col;
        }
        static SqlPara GetInsert(string table, IDictionary<string, string> fdict)
        {
            int counter = 1;
            IList<MySqlParameter> paraList = new List<MySqlParameter>();
            StringBuilder cols = new StringBuilder();
            StringBuilder vals = new StringBuilder();
            foreach (string key in fdict.Keys)
            {
                cols.Append(key);
                vals.Append("@"); vals.Append(key);
                if (counter == fdict.Count)
                {
                    cols.Append(")");
                    vals.Append(")");
                }
                else
                {
                    cols.Append(",");
                    vals.Append(",");
                }
                paraList.Add(new MySqlParameter(key, GetDBValue(fdict[key])));
                counter++;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ");
            builder.Append(table);
            builder.Append("(");
            builder.Append(cols);
            builder.Append(" values (");
            builder.Append(vals);
            MySqlParameter[] parameters = new MySqlParameter[paraList.Count];
            for (int i = 0; i < paraList.Count; i++)
            {
                parameters[i] = paraList[i];
            }
            return new SqlPara { sql = builder.ToString(), para = parameters };
        }
        static SqlPara GetUpdate(string table, IDictionary<string, string> dict, IDictionary<string, string> fdict, string andor)
        {
            int counter = 1;
            IList<MySqlParameter> paraList = new List<MySqlParameter>();
            StringBuilder builder = new StringBuilder();
            builder.Append("update ");
            builder.Append(table);
            builder.Append(" set ");
            foreach (string key in dict.Keys)
            {
                if (dict[key].StartsWith("数字相加+") || dict[key].StartsWith("数字相减-"))
                {
                    builder.Append(key);
                    builder.Append("=");
                    builder.Append(key);
                    builder.Append(dict[key].Substring(4, 1));
                    builder.Append("@");
                    builder.Append(key);
                    paraList.Add(new MySqlParameter(key, GetDBValue(dict[key].Substring(5))));
                }
                else
                {
                    builder.Append(key);
                    builder.Append("=@");
                    builder.Append(key);
                    paraList.Add(new MySqlParameter(key, GetDBValue(dict[key])));
                }
                builder.Append(counter == dict.Count ? string.Empty : ",");

                counter++;
            }
            MySqlParameter[] parameters = new MySqlParameter[paraList.Count];
            for (int i = 0; i < paraList.Count; i++)
            {
                parameters[i] = paraList[i];
            }

            SqlPara wherePara = GetWhere(table, fdict, andor);
            if (wherePara.sql.Length > 0)
            {
                builder.Append(wherePara.sql);
                parameters = parameters.Concat(wherePara.para).ToArray();
            }

            return new SqlPara { sql = builder.ToString(), para = parameters };
        }
        static SqlPara GetWhere(string table, IDictionary<string, string> fdict, string andor)
        {
            IList<MySqlParameter> paraList = new List<MySqlParameter>();
            if (fdict.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(" where ");
                bool first = true;
                foreach (string key in fdict.Keys)
                {
                    //if (string.IsNullOrWhiteSpace(fdict[key])) continue;
                    if (!first)
                    {
                        builder.Append(" "); builder.Append(andor); builder.Append(" ");
                    }
                    first = false;
                    if (fdict[key].ToUpper().Equals("IS NULL") || fdict[key].ToUpper().Equals("IS NOT NULL"))
                    {
                        builder.Append(RemoveDupCol(key)); builder.Append(" "); builder.Append(fdict[key]);
                    }
                    else if (fdict[key].Equals("<>''"))
                    {
                        builder.Append(RemoveDupCol(key)); builder.Append(fdict[key]);
                    }
                    else if (fdict[key].StartsWith("<>") || fdict[key].StartsWith(">=") || fdict[key].StartsWith("<="))
                    {
                        builder.Append(RemoveDupCol(key)); builder.Append(" "); builder.Append(fdict[key].Substring(0, 2)); builder.Append(" @"); builder.Append(key);
                        paraList.Add(new MySqlParameter(key, GetDBValue(fdict[key].Substring(2))));
                    }
                    else if (fdict[key].StartsWith(">") || fdict[key].StartsWith("<"))
                    {
                        builder.Append(RemoveDupCol(key)); builder.Append(" "); builder.Append(fdict[key].Substring(0, 1)); builder.Append(" @"); builder.Append(key);
                        paraList.Add(new MySqlParameter(key, GetDBValue(fdict[key].Substring(1))));
                    }
                    else if (fdict[key].StartsWith("%") || fdict[key].EndsWith("%"))
                    {
                        builder.Append(RemoveDupCol(key)); builder.Append(" like @"); builder.Append(key);
                        paraList.Add(new MySqlParameter("@" + key, GetDBValue(fdict[key])));
                    }
                    else if (fdict[key].StartsWith("in "))
                    {
                        builder.Append(RemoveDupCol(key));
                        builder.Append(" " + fdict[key]);
                    }
                    else if (string.IsNullOrWhiteSpace(fdict[key]))
                    {
                        builder.Append(RemoveDupCol(key)); builder.Append("=''");
                    }
                    else
                    {
                        builder.Append(RemoveDupCol(key)); builder.Append("=@"); builder.Append(key);
                        paraList.Add(new MySqlParameter(key, GetDBValue(fdict[key])));
                    }
                }
                MySqlParameter[] parameters = new MySqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    parameters[i] = paraList[i];
                }

                return new SqlPara { sql = builder.ToString(), para = parameters };
            }
            return new SqlPara { sql = string.Empty, para = new MySqlParameter[0] };
        }
        static SqlBuilder.Template MakeSBTemp(string sql, MySqlParameter[] parameters)
        {
            var sqlBd = new SqlBuilder();
            var template = sqlBd.AddTemplate(sql);
            if (parameters != null)
            {
                foreach (MySqlParameter para in parameters)
                {
                    dynamic dyn = new ExpandoObject();
                    ((IDictionary<string, object>)dyn).Add(para.ParameterName, para.Value);
                    sqlBd.AddParameters(dyn);
                }
            }
            return template;
        }
        public static string MakeMultiIdKey(string table, IDictionary<string, string> fdict, string andor)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(table);
            builder.Append("_");
            builder.Append(andor);
            builder.Append("_");
            foreach (string key in fdict.Keys)
            {
                builder.Append(key);
                builder.Append("_");
                builder.Append(fdict[key]);
                builder.Append("_");
            }
            return builder.ToString();
        }
        public static async Task<int> ExecuteSql(string strSql, MySqlParameter[] dd)
        {
            int result = 0;
            if (strSql.Length > 0)
            {
                using (MySqlConnection dbConnection = new MySqlConnection(strConnection))
                {
                    dbConnection.Open();
                    using (MySqlCommand objCommand = new MySqlCommand(strSql, dbConnection))
                    {
                        using (MySqlTransaction trans = dbConnection.BeginTransaction())
                        {
                            //objCommand.CommandTimeout = 0;
                            objCommand.Transaction = trans;
                            objCommand.Parameters.AddRange(dd);
                            result = await objCommand.ExecuteNonQueryAsync();
                            trans.Commit();
                        }
                    }
                }
            }
            return result;
        }
        public static async Task<int> ExecuteSql(string strSql, MySqlParameter[] dd, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            int result = 0;
            if (strSql.Length > 0)
            {
                using (MySqlCommand objCommand = new MySqlCommand(strSql, dbConnection))
                {
                    objCommand.Transaction = trans;
                    objCommand.Parameters.AddRange(dd);
                    result = await objCommand.ExecuteNonQueryAsync();
                }
            }
            return result;
        }
        public static async Task<MySqlConnection> GetOpenCon()
        {
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            await dbConnection.OpenAsync();
            return dbConnection;
        }
        static int ExecuteSqlSync(string strSql, MySqlParameter[] dd)
        {
            int result = 0;
            if (strSql.Length > 0)
            {
                using (MySqlConnection dbConnection = new MySqlConnection(strConnection))
                {
                    dbConnection.Open();
                    using (MySqlCommand objCommand = new MySqlCommand(strSql, dbConnection))
                    {
                        using (MySqlTransaction trans = dbConnection.BeginTransaction())
                        {
                            //objCommand.CommandTimeout = 0;
                            objCommand.Transaction = trans;
                            objCommand.Parameters.AddRange(dd);
                            result = objCommand.ExecuteNonQuery();
                            trans.Commit();
                        }
                    }
                }
            }
            return result;
        }
        static int ExecuteSqlSync(string strSql, MySqlParameter[] dd, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            int result = 0;
            if (strSql.Length > 0)
            {
                using (MySqlCommand objCommand = new MySqlCommand(strSql, dbConnection))
                {
                    objCommand.Transaction = trans;
                    objCommand.Parameters.AddRange(dd);
                    result = objCommand.ExecuteNonQuery();
                }
            }
            return result;
        }
        public static MySqlConnection GetOpenConSync()
        {
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            dbConnection.Open();
            return dbConnection;
        }
        #endregion
    }
    class SqlPara
    {
        public string sql { get; set; }
        public MySqlParameter[] para { get; set; }
    }
}
