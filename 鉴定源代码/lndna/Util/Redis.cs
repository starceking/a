using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class Redis
    {
        static string IdsKey = ConfigurationManager.AppSettings["IdsKey"];
        #region object
        public static async Task Insert<T>(string id, T obj, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            var type = typeof(T);
            StringBuilder builder = new StringBuilder();
            builder.Append(type.ToString());
            builder.Append("-");
            builder.Append(id);
            string key = builder.ToString();
            if (pre) key = GetKeyPre(key);
            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                var val = pi.GetValue(obj);
                if (val != null && val.ToString().Length > 0)
                {
                    await db.HashSetAsync(key, pi.Name, pi.GetValue(obj).ToString(), When.Always, CommandFlags.FireAndForget);
                }
            }
        }
        public static async Task InsertOneProperty<T>(string id, string pro, string val, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            var type = typeof(T);
            StringBuilder builder = new StringBuilder();
            builder.Append(type.ToString());
            builder.Append("-");
            builder.Append(id);
            string key = builder.ToString();
            if (pre) key = GetKeyPre(key);
            await db.HashSetAsync(key, pro, val, When.Always, CommandFlags.FireAndForget);
        }
        public static async Task Delete<T>(string id, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            var type = typeof(T);
            StringBuilder builder = new StringBuilder();
            builder.Append(type.ToString());
            builder.Append("-");
            builder.Append(id);
            string key = builder.ToString();
            if (pre) key = GetKeyPre(key);
            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                await db.HashDeleteAsync(key, pi.Name, CommandFlags.FireAndForget);
            }
            await DeleteKey(key);
        }
        public static async Task<T> Get<T>(string id, bool pre = true) where T : class
        {
            IDatabase db = GetDB();
            if (db == null) return null;
            var type = typeof(T);
            StringBuilder builder = new StringBuilder();
            builder.Append(type.ToString());
            builder.Append("-");
            builder.Append(id);
            string key = builder.ToString();
            if (pre) key = GetKeyPre(key);
            T obj = (T)Assembly.Load(type.Namespace).CreateInstance(type.ToString());
            PropertyInfo[] pis = type.GetProperties();
            bool hasValue = false;
            HashEntry[] hes = await db.HashGetAllAsync(key);
            foreach (HashEntry he in hes)
            {
                PropertyInfo pi = type.GetProperty(he.Name);
                if (pi != null)
                {
                    RedisValue rv = he.Value;
                    var rvtype = pi.PropertyType;
                    if (rvtype.Equals(typeof(int)) || rvtype.Equals(typeof(int?))) pi.SetValue(obj, Convert.ToInt32(rv));
                    else if (rvtype.Equals(typeof(long)) || rvtype.Equals(typeof(long?))) pi.SetValue(obj, Convert.ToInt64(rv));
                    else if (rvtype.Equals(typeof(ulong)) || rvtype.Equals(typeof(ulong?))) pi.SetValue(obj, Convert.ToUInt64(rv));
                    else if (rvtype.Equals(typeof(Decimal)) || rvtype.Equals(typeof(Decimal?))) pi.SetValue(obj, Convert.ToDecimal(rv));
                    else if (rvtype.Equals(typeof(DateTime)) || rvtype.Equals(typeof(DateTime?))) pi.SetValue(obj, Convert.ToDateTime(rv));
                    else if (rvtype.Equals(typeof(Boolean)) || rvtype.Equals(typeof(Boolean?))) pi.SetValue(obj, Convert.ToBoolean(rv));
                    else pi.SetValue(obj, rv.ToString());

                    if (he.Name.Equals("id") && he.Value.ToString().Equals(id)) hasValue = true;
                }
            }
            if (!hasValue)
            {
                await DeleteKey(key, false);
                return null;
            }
            return obj;
        }
        public static async Task Delete<T>(string table, IDictionary<string, string> fdict, string andor, bool pre = true) where T : class
        {
            string id = DBHelper.MakeMultiIdKey(table, fdict, andor);
            await DeleteKey(id, pre);
        }
        #endregion
        #region list
        public static async Task InsertList(string key, string value, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            await db.ListRightPushAsync(key, value, When.Always, CommandFlags.FireAndForget);
        }
        public static async Task DeleteList(string key, string value, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            await db.ListRemoveAsync(key, value, 0, CommandFlags.FireAndForget);
        }
        public static async Task<RedisValue[]> GetList(string key, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return null;
            if (pre) key = GetKeyPre(key);
            return await db.ListRangeAsync(key);
        }
        #endregion
        #region set
        public static async Task InsertSet(string key, string value, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            await db.SetAddAsync(key, value, CommandFlags.FireAndForget);
        }
        public static async Task DeleteSet(string key, string value, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            await db.SetRemoveAsync(key, value, CommandFlags.FireAndForget);
        }
        public static async Task DeleteSet(string key, RedisValue[] values, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            await db.SetRemoveAsync(key, values, CommandFlags.FireAndForget);
        }
        public static async Task<RedisValue[]> GetSet(string key, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return null;
            if (pre) key = GetKeyPre(key);
            return await db.SetMembersAsync(key);
        }
        public static async Task<RedisValue> PopSet(string key, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return RedisValue.Null;
            if (pre) key = GetKeyPre(key);
            return await db.SetPopAsync(key);
        }
        #endregion
        #region tcr
        public static async Task InsertTCR(string table, string column, string rcolumn, string value, string rvalue, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            string key = GetTCRKey(table, column, rcolumn, value, pre);
            await db.StringSetAsync(key, rvalue, null, When.Always, CommandFlags.FireAndForget);
        }
        public static async Task DeleteTCR(string table, string column, string rcolumn, string value, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            string key = GetTCRKey(table, column, rcolumn, value, pre);
            await db.KeyDeleteAsync(key, CommandFlags.FireAndForget);
        }
        public static async Task<string> GetTCR(string table, string column, string rcolumn, string value, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return string.Empty;
            string key = GetTCRKey(table, column, rcolumn, value, pre);
            RedisValue rv = await db.StringGetAsync(key);
            if (rv.HasValue && rv.ToString().Length > 0) return rv.ToString();
            return string.Empty;
        }
        public static async Task InsertTCRList(string table, string column, string rcolumn, string value, string rvalue, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            string key = GetTCRKey(table, column, rcolumn, value, pre);
            await db.ListRightPushAsync(key, rvalue, When.Always, CommandFlags.FireAndForget);
        }
        public static async Task<RedisValue[]> GetTCRList(string table, string column, string rcolumn, string value, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return null;
            string key = GetTCRKey(table, column, rcolumn, value, pre);
            return await db.ListRangeAsync(key);
        }
        #endregion
        #region string
        public static async Task InsertString(string key, string val, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            await db.StringSetAsync(key, val, null, When.Always, CommandFlags.FireAndForget);
        }
        public static async Task InsertString(string key, string val, TimeSpan ts, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            await db.StringSetAsync(key, val, ts, When.Always, CommandFlags.FireAndForget);
        }
        public static async Task<string> GetString(string key, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return null;
            if (pre) key = GetKeyPre(key);
            return await db.StringGetAsync(key);
        }
        #endregion
        #region subscribe
        public static async Task<bool> Subscribe(string channel, Action<RedisChannel, RedisValue> action, bool pre = true)
        {
            ISubscriber sub = GetSubscriber();
            if (sub == null) return false;
            if (pre) channel = GetKeyPre(channel);
            await sub.SubscribeAsync(channel, action);
            return true;
        }
        public static async Task<bool> UnSubscribe(string channel, bool pre = true)
        {
            ISubscriber sub = GetSubscriber();
            if (sub == null) return false;
            if (pre) channel = GetKeyPre(channel);
            await sub.UnsubscribeAsync(channel, null);
            return true;
        }
        public static async Task<bool> Publish(string channel, string value, bool pre = true)
        {
            ISubscriber sub = GetSubscriber();
            if (sub == null) return false;
            if (pre) channel = GetKeyPre(channel);
            await sub.PublishAsync(channel, value);
            return true;
        }
        #endregion
        #region sorted set
        public static async Task InsertSortedSet(string key, string val, double score, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            await db.SortedSetAddAsync(key, val, score, CommandFlags.FireAndForget);
        }
        public static async Task<SortedSetEntry[]> GetSortedSet(string key, long start, long stop, Order order, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return null;
            if (pre) key = GetKeyPre(key);
            return await db.SortedSetRangeByRankWithScoresAsync(key, start, stop, order);
        }
        public static async Task<long?> GetSortedSetRank(string key, string val, Order order, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return null;
            if (pre) key = GetKeyPre(key);
            return await db.SortedSetRankAsync(key, val, order);
        }
        //public static async Task<SortedSetEntry[]> GetSortedSetByScore(string key, double start, double stop,
        //    Exclude enclude, Order order, bool pre = true)
        //{
        //    IDatabase db = GetDB();
        //    if (db == null) return null;
        //    if (pre) key = GetKeyPre(key);
        //    return await db.SortedSetRangeByScoreWithScoresAsync(key, start, stop, enclude, order);
        //}
        //public static SortedSetEntry[] GetSortedSetByScoreSync(string key, double start, double stop,
        //    Exclude enclude, Order order, bool pre = true)
        //{
        //    IDatabase db = GetDB();
        //    if (db == null) return null;
        //    if (pre) key = GetKeyPre(key);
        //    return db.SortedSetRangeByScoreWithScores(key, start, stop, enclude, order);
        //}
        public static async Task DeleteSortedSet(string key, string val, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            await db.SortedSetRemoveAsync(key, val, CommandFlags.FireAndForget);
        }
        #endregion
        #region number
        public static async Task<double> InsertNumber(string key, double val, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return -1;
            if (pre) key = GetKeyPre(key);
            return await db.StringIncrementAsync(key, val);
        }
        #endregion
        #region object sync
        public static void InsertSync<T>(string id, T obj, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            var type = typeof(T);
            StringBuilder builder = new StringBuilder();
            builder.Append(type.ToString());
            builder.Append("-");
            builder.Append(id);
            string key = builder.ToString();
            if (pre) key = GetKeyPre(key);
            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                var val = pi.GetValue(obj);
                if (val != null && val.ToString().Length > 0)
                {
                    db.HashSet(key, pi.Name, pi.GetValue(obj).ToString(), When.Always, CommandFlags.FireAndForget);
                }
            }
        }
        public static void InsertOnePropertySync<T>(string id, string pro, string val, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            var type = typeof(T);
            StringBuilder builder = new StringBuilder();
            builder.Append(type.ToString());
            builder.Append("-");
            builder.Append(id);
            string key = builder.ToString();
            if (pre) key = GetKeyPre(key);
            db.HashSet(key, pro, val, When.Always, CommandFlags.FireAndForget);
        }
        public static void DeleteSync<T>(string id, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            var type = typeof(T);
            StringBuilder builder = new StringBuilder();
            builder.Append(type.ToString());
            builder.Append("-");
            builder.Append(id);
            string key = builder.ToString();
            if (pre) key = GetKeyPre(key);
            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                db.HashDelete(key, pi.Name, CommandFlags.FireAndForget);
            }
            DeleteKeySync(key);
        }
        public static T GetSync<T>(string id, bool pre = true) where T : class
        {
            IDatabase db = GetDB();
            if (db == null) return null;
            var type = typeof(T);
            StringBuilder builder = new StringBuilder();
            builder.Append(type.ToString());
            builder.Append("-");
            builder.Append(id);
            string key = builder.ToString();
            if (pre) key = GetKeyPre(key);
            T obj = (T)Assembly.Load(type.Namespace).CreateInstance(type.ToString());
            PropertyInfo[] pis = type.GetProperties();
            bool hasValue = false;
            HashEntry[] hes = db.HashGetAll(key);
            foreach (HashEntry he in hes)
            {
                PropertyInfo pi = type.GetProperty(he.Name);
                if (pi != null)
                {
                    RedisValue rv = he.Value;
                    var rvtype = pi.PropertyType;
                    if (rvtype.Equals(typeof(int)) || rvtype.Equals(typeof(int?))) pi.SetValue(obj, Convert.ToInt32(rv));
                    else if (rvtype.Equals(typeof(long)) || rvtype.Equals(typeof(long?))) pi.SetValue(obj, Convert.ToInt64(rv));
                    else if (rvtype.Equals(typeof(ulong)) || rvtype.Equals(typeof(ulong?))) pi.SetValue(obj, Convert.ToUInt64(rv));
                    else if (rvtype.Equals(typeof(Decimal)) || rvtype.Equals(typeof(Decimal?))) pi.SetValue(obj, Convert.ToDecimal(rv));
                    else if (rvtype.Equals(typeof(DateTime)) || rvtype.Equals(typeof(DateTime?))) pi.SetValue(obj, Convert.ToDateTime(rv));
                    else if (rvtype.Equals(typeof(Boolean)) || rvtype.Equals(typeof(Boolean?))) pi.SetValue(obj, Convert.ToBoolean(rv));
                    else pi.SetValue(obj, rv.ToString());

                    if (he.Name.Equals("id") && he.Value.ToString().Equals(id)) hasValue = true;
                }
            }
            if (!hasValue)
            {
                DeleteKeySync(key, false);
                return null;
            }
            return obj;
        }
        public static void DeleteSync<T>(string table, IDictionary<string, string> fdict, string andor, bool pre = true) where T : class
        {
            string id = DBHelper.MakeMultiIdKey(table, fdict, andor);
            DeleteKeySync(id, pre);
        }
        #endregion
        #region list
        public static void InsertListSync(string key, string value, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            db.ListRightPush(key, value, When.Always, CommandFlags.FireAndForget);
        }
        public static void DeleteListSync(string key, string value, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            db.ListRemove(key, value, 0, CommandFlags.FireAndForget);
        }
        public static RedisValue[] GetListSync(string key, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return null;
            if (pre) key = GetKeyPre(key);
            return db.ListRange(key);
        }
        #endregion
        #region set
        public static void InsertSetSync(string key, string value, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            db.SetAdd(key, value, CommandFlags.FireAndForget);
        }
        public static void DeleteSetSync(string key, string value, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            db.SetRemove(key, value, CommandFlags.FireAndForget);
        }
        public static void DeleteSetSync(string key, RedisValue[] values, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            db.SetRemove(key, values, CommandFlags.FireAndForget);
        }
        public static RedisValue[] GetSetSync(string key, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return null;
            if (pre) key = GetKeyPre(key);
            return db.SetMembers(key);
        }
        public static RedisValue PopSetSync(string key, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return RedisValue.Null;
            if (pre) key = GetKeyPre(key);
            return db.SetPop(key);
        }
        #endregion
        #region tcr
        public static void InsertTCRSync(string table, string column, string rcolumn, string value, string rvalue, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            string key = GetTCRKey(table, column, rcolumn, value, pre);
            db.StringSet(key, rvalue, null, When.Always, CommandFlags.FireAndForget);
        }
        public static void DeleteTCRSync(string table, string column, string rcolumn, string value, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            string key = GetTCRKey(table, column, rcolumn, value, pre);
            db.KeyDelete(key, CommandFlags.FireAndForget);
        }
        public static string GetTCRSync(string table, string column, string rcolumn, string value, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return string.Empty;
            string key = GetTCRKey(table, column, rcolumn, value, pre);
            RedisValue rv = db.StringGet(key);
            if (rv.HasValue && rv.ToString().Length > 0) return rv.ToString();
            return string.Empty;
        }
        public static void InsertTCRListSync(string table, string column, string rcolumn, string value, string rvalue, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            string key = GetTCRKey(table, column, rcolumn, value, pre);
            db.ListRightPush(key, rvalue, When.Always, CommandFlags.FireAndForget);
        }
        public static RedisValue[] GetTCRListSync(string table, string column, string rcolumn, string value, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return null;
            string key = GetTCRKey(table, column, rcolumn, value, pre);
            return db.ListRange(key);
        }
        #endregion
        #region string
        public static void InsertStringSync(string key, string val, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            db.StringSet(key, val, null, When.Always, CommandFlags.FireAndForget);
        }
        public static void InsertStringSync(string key, string val, TimeSpan ts, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            db.StringSet(key, val, ts, When.Always, CommandFlags.FireAndForget);
        }
        public static string GetStringSync(string key, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return null;
            if (pre) key = GetKeyPre(key);
            return db.StringGet(key);
        }
        #endregion
        #region subscribe
        public static bool SubscribeSync(string channel, Action<RedisChannel, RedisValue> action, bool pre = true)
        {
            ISubscriber sub = GetSubscriber();
            if (sub == null) return false;
            if (pre) channel = GetKeyPre(channel);
            sub.Subscribe(channel, action);
            return true;
        }
        public static bool UnSubscribeSync(string channel, bool pre = true)
        {
            ISubscriber sub = GetSubscriber();
            if (sub == null) return false;
            if (pre) channel = GetKeyPre(channel);
            sub.Unsubscribe(channel, null);
            return true;
        }
        public static bool PublishSync(string channel, string value, bool pre = true)
        {
            ISubscriber sub = GetSubscriber();
            if (sub == null) return false;
            if (pre) channel = GetKeyPre(channel);
            sub.Publish(channel, value);
            return true;
            //用法
            //Action<RedisChannel, RedisValue> action = (RedisChannel, RedisValue) =>
            //{
            //    if (RedisChannel.ToString().Equals("fxxk"))
            //    {
            //        Console.WriteLine(RedisValue);
            //    }
            //};

            //Redis.SubscribeSync("fxxk", action, false);
            //do
            //{
            //    Redis.PublishSync("fxxk", DateTime.Now.ToString(), false);
            //    Thread.Sleep(1000);
            //}
            //while (true);
        }
        #endregion
        #region sorted set
        public static void InsertSortedSetSync(string key, string val, double score, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            db.SortedSetAdd(key, val, score, CommandFlags.FireAndForget);
        }
        public static SortedSetEntry[] GetSortedSetSync(string key, long start, long stop, Order order, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return null;
            if (pre) key = GetKeyPre(key);
            return db.SortedSetRangeByRankWithScores(key, start, stop, order);
        }
        public static long? GetSortedSetRankSync(string key, string val, Order order, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return null;
            if (pre) key = GetKeyPre(key);
            return db.SortedSetRank(key, val, order);
        }
        public static void DeleteSortedSetSync(string key, string val, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            if (pre) key = GetKeyPre(key);
            db.SortedSetRemove(key, val, CommandFlags.FireAndForget);
        }
        #endregion
        #region number
        public static double InsertNumberSync(string key, double val, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return -1;
            if (pre) key = GetKeyPre(key);
            return db.StringIncrement(key, val);
        }
        #endregion
        #region basic
        static readonly string strConfig = ConfigurationManager.AppSettings["Redis"];
        static ConnectionMultiplexer redis;
        static ConnectionMultiplexer REDIS
        {
            get
            {
                Init();
                return redis;
            }
        }
        static IDatabase GetDB()
        {
            if (REDIS == null) return null;
            IDatabase db = REDIS.GetDatabase();
            return db;
        }
        static ISubscriber GetSubscriber()
        {
            if (REDIS == null) return null;
            ISubscriber sub = REDIS.GetSubscriber();
            return sub;
        }
        static string GetTCRKey(string table, string column, string rcolumn, string value, bool pre = true)
        {
            StringBuilder builder = new StringBuilder();
            if (pre && !string.IsNullOrWhiteSpace(IdsKey))
            {
                builder.Append(IdsKey);
                builder.Append("_");
            }
            builder.Append(table);
            builder.Append("-");
            builder.Append(column);
            builder.Append("-");
            builder.Append(rcolumn);
            builder.Append("-");
            builder.Append(value);
            return builder.ToString();
        }
        public static async Task DeleteKey(string key, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            await db.KeyDeleteAsync(pre ? GetKeyPre(key) : key, CommandFlags.FireAndForget);
        }
        public static void DeleteKeySync(string key, bool pre = true)
        {
            IDatabase db = GetDB();
            if (db == null) return;
            db.KeyDelete(pre ? GetKeyPre(key) : key, CommandFlags.FireAndForget);
        }
        static string GetKeyPre(string key)
        {
            if (!string.IsNullOrWhiteSpace(IdsKey))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(IdsKey);
                builder.Append("_");
                builder.Append(key);
                key = builder.ToString();
            }
            return key;
        }
        public static void Init()
        {
            if (redis == null)
            {
                //var config = new ConfigurationOptions
                //{
                //    AbortOnConnectFail = false,
                //    EndPoints =
                //    {
                //    { "127.0.0.1:1234" }
                //    },
                //    ConnectTimeout = 200
                //};
                //var log = new stringWriter();
                //using (var conn = ConnectionMultiplexer.Connect(config, log))
                //{
                //    Console.WriteLine(log);
                //    Assert.IsFalse(conn.IsConnected);
                //}
                redis = ConnectionMultiplexer.Connect(strConfig);
            }
        }
        public static bool TryConn()
        {
            IDatabase db = GetDB();
            if (db == null) return false;
            return true;
        }
        #endregion
    }
}
