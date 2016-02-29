using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class ModelHelper
    {
        static readonly ConcurrentDictionary<RuntimeTypeHandle, string> TypeTableName =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();
        //外部销售团队
        static readonly string CMP_ID = ConfigurationManager.AppSettings["CmpId"];

        public static async Task<T> FillFkInfo<T, F>(string id, T obj, string[] cols, string[] fkcols)
            where T : class
            where F : class
        {
            if (id == null || id.Length == 0) return obj;
            if (obj == null) return null;
            long idl = 0;
            if (!long.TryParse(id, out idl)) return obj;
            if (idl <= 0) return obj;

            F model = await DBHelper.GetOne<F>(id);
            return FillFkInfo(obj, model, cols, fkcols);
        }
        public static T FillFkInfoSync<T, F>(string id, T obj, string[] cols, string[] fkcols)
            where T : class
            where F : class
        {
            if (id == null || id.Length == 0) return obj;
            if (obj == null) return null;
            long idl = 0;
            if (!long.TryParse(id, out idl)) return obj;
            if (idl <= 0) return obj;

            F model = DBHelper.GetOneSync<F>(id);
            return FillFkInfo(obj, model, cols, fkcols);
        }
        public static async Task<IEnumerable<T>> FillFkInfo<T, F, A>(string table, string fkcol, IList<string> ids,
            IEnumerable<T> list, string[] cols, string[] fkcols, bool cmp = true) where F : class
        {
            if (ids.Count == 0) return list;

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            foreach (string id in ids)
            {
                long idl = 0;
                if (!long.TryParse(id, out idl)) continue;
                if (idl <= 0) continue;

                StringBuilder builder = new StringBuilder();
                builder.Append("重复字段");
                builder.Append(id);
                builder.Append("重复字段id");
                fdict.Add(builder.ToString(), id);
            }
            IEnumerable<F> users = await DBHelper.GetList<F, A>(table, "*", string.Empty, fdict, "or");

            IList<T> retList = list.ToList<T>();
            foreach (F model in users)
            {
                //CMP权限设定
                if (cmp && (!string.IsNullOrWhiteSpace(CMP_ID)))
                {
                    PropertyInfo cmpPi = typeof(F).GetProperty("cmp_id");
                    if (cmpPi != null)
                    {
                        var piVal = cmpPi.GetValue(model);
                        if (piVal != null)
                        {
                            if (!piVal.ToString().Equals(CMP_ID))
                            {
                                continue;
                            }
                        }
                    }
                }

                for (int i = 0; i < retList.Count(); i++)
                {
                    PropertyInfo pi = typeof(F).GetProperty("id"); if (pi == null) continue;
                    var key = pi.GetValue(model);
                    pi = typeof(T).GetProperty(fkcol); if (pi == null) continue;
                    var fkey = pi.GetValue(retList[i]);
                    if (key != null && fkey != null && Convert.ToInt64(key).Equals(Convert.ToInt64(fkey)))
                        retList[i] = FillFkInfo<T, F>(retList[i], model, cols, fkcols);
                }
            }
            return retList;
        }
        public static IEnumerable<T> FillFkInfoSync<T, F, A>(string table, string fkcol, IList<string> ids,
            IEnumerable<T> list, string[] cols, string[] fkcols) where F : class
        {
            if (ids.Count == 0) return list;

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            foreach (string id in ids)
            {
                long idl = 0;
                if (!long.TryParse(id, out idl)) continue;
                if (idl <= 0) continue;

                StringBuilder builder = new StringBuilder();
                builder.Append("重复字段");
                builder.Append(id);
                builder.Append("重复字段id");
                fdict.Add(builder.ToString(), id);
            }
            IEnumerable<F> users = DBHelper.GetListSync<F, A>(table, "*", string.Empty, fdict, "or");

            IList<T> retList = list.ToList<T>();
            foreach (F model in users)
            {
                for (int i = 0; i < retList.Count(); i++)
                {
                    PropertyInfo pi = typeof(F).GetProperty("id"); if (pi == null) continue;
                    var key = pi.GetValue(model);
                    pi = typeof(T).GetProperty(fkcol); if (pi == null) continue;
                    var fkey = pi.GetValue(retList[i]);
                    if (key != null && fkey != null && Convert.ToInt64(key).Equals(Convert.ToInt64(fkey)))
                        retList[i] = FillFkInfo<T, F>(retList[i], model, cols, fkcols);
                }
            }
            return retList;
        }
        public static string GetTableName(Type type)
        {
            string name;
            if (!TypeTableName.TryGetValue(type.TypeHandle, out name))
            {
                name = type.Name + "s";
                if (type.IsInterface && name.StartsWith("I"))
                    name = name.Substring(1);

                var tableattr = type.GetCustomAttributes(false).Where(attr => attr.GetType().Name == "TableAttribute").SingleOrDefault() as
                     dynamic;
                if (tableattr != null)
                    name = tableattr.Name;
                TypeTableName[type.TypeHandle] = name;
            }
            return name;
        }
        static T FillFkInfo<T, F>(T obj, F model, string[] cols, string[] fkcols)
        {
            if (model != null)
            {
                for (int i = 0; i < cols.Length; i++)
                {
                    PropertyInfo pi = typeof(F).GetProperty(cols[i]); if (pi == null) continue;
                    var val = pi.GetValue(model);
                    if (val != null && val.ToString().Length > 0)
                    {
                        pi = typeof(T).GetProperty(fkcols[i]); if (pi == null) continue;
                        pi.SetValue(obj, val);
                    }
                }
            }
            return obj;
        }
    }
}
