using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class Notice
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        public const string TABLE = "notice";
        #endregion
        #region 一般操作
        public static string Insert(string head, string content)
        {
            if (string.IsNullOrWhiteSpace(head) || string.IsNullOrWhiteSpace(content)) return "参数不全";
            if (head.Length > 50) head = head.Substring(0, 50);

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("head", head);
            dict.Add("content", content);
            dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) dict.Add("cmp_id", User.CMP_ID);
            DBHelper.InsertSync(TABLE, dict);

            return string.Empty;
        }
        public static string Update(int id, string head, string content)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(head) || string.IsNullOrWhiteSpace(content)) return "参数不全";
            if (head.Length > 50) head = head.Substring(0, 50);

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("head", head);
            dict.Add("content", content);
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            DBHelper.UpdateSync(TABLE, dict, fdict, "and");
            DeleteRedisSync(id);

            return string.Empty;
        }
        public static async Task<string> Delete(int id)
        {
            if (id <= 0) return "参数不全";

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            await DBHelper.Delete(TABLE, fdict, "and");

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<IEnumerable<NoticeModel>> GetList(string page_size, string page_index)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", "0");
            else fdict.Add("cmp_id", User.CMP_ID);

            return await DBHelper.GetList<NoticeModel, int>(TABLE, "*", "id desc", fdict, "and",
                        Convert.ToInt32(page_size), Convert.ToInt32(page_index));
        }
        public static async Task<long> GetCount()
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", "0");
            else fdict.Add("cmp_id", User.CMP_ID);

            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        public static async Task<NoticeModel> GetOne(int id)
        {
            if (id <= 0) return null;
            return await DBHelper.GetOne<NoticeModel>(id.ToString());
        }
        public static NoticeModel GetOneSync(int id)
        {
            if (id <= 0) return null;
            return DBHelper.GetOneSync<NoticeModel>(id.ToString());
        }
        public static async Task DeleteRedis(int id)
        {
            if (id <= 0) return;
            await Redis.Delete<NoticeModel>(id.ToString());
        }
        public static void DeleteRedisSync(int id)
        {
            if (id <= 0) return;
            Redis.DeleteSync<NoticeModel>(id.ToString());
        }
        #endregion
    }
}
