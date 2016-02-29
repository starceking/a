using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class StockForbidden
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        const string TABLE = "stock_forbidden";
        /// <summary>
        /// 存储
        /// </summary>
        static IList<StockForbiddenModel> LIST = null;
        #endregion
        #region 一般操作
        public static async Task<string> Insert(string number, string name, int reason_id)
        {
            if (string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(name) || reason_id <= 0) return "参数不全";
            if (number.Length > 50 || name.Length > 50) return "内容过长";
            if ((await GetOne(0, number)) != null) return "该股票已存在";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("number", number);
            dict.Add("name", name);
            dict.Add("reason_id", reason_id.ToString());
            if ((await DBHelper.Insert(TABLE, dict)) <= 0) return "异常，请重试";

            if (LIST != null) LIST.Clear();

            return string.Empty;
        }
        public static async Task<string> Delete(int id, string number)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(number)) return "参数不全";

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if ((await DBHelper.Delete(TABLE, fdict, "and")) <= 0) return "异常，请重试";

            fdict = new Dictionary<string, string>();
            fdict.Add("number", number);
            string key = DBHelper.MakeMultiIdKey(TABLE, fdict, "and");
            await Redis.DeleteKey(key);

            if (LIST != null) LIST.Clear();

            return string.Empty;
        }
        public static async Task<StockForbiddenModel> GetOne(int id, string number)
        {
            if (id > 0) return await DBHelper.GetOne<StockForbiddenModel>(id.ToString());
            else if (!string.IsNullOrWhiteSpace(number))
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                fdict.Add("number", number);
                return await DBHelper.GetOne<StockForbiddenModel, int>(TABLE, fdict, "and");
            }
            else return null;
        }
        public static async Task<IEnumerable<StockForbiddenModel>> GetList()
        {
            if (LIST == null || LIST.Count == 0)
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                LIST = (await DBHelper.GetList<StockForbiddenModel, int>(TABLE, "*", "id desc", fdict, "and")).ToList();
            }
            return LIST;
        }
        public static IEnumerable<StockForbiddenModel> GetListSync()
        {
            if (LIST == null || LIST.Count == 0)
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                return DBHelper.GetListSync<StockForbiddenModel, int>(TABLE, "*", "id desc", fdict, "and");
            }
            return LIST;
        }
        #endregion
        #region 辅助
        public static async Task<bool> CanBuy(string number)
        {
            IEnumerable<StockForbiddenModel> list = await GetList();
            foreach (StockForbiddenModel model in list)
            {
                if (model.number.Equals(number)) return false;
            }
            return true;
        }
        public static bool CanBuySync(string number)
        {
            IEnumerable<StockForbiddenModel> list = GetListSync();
            foreach (StockForbiddenModel model in list)
            {
                if (model.number.Equals(number)) return false;
            }
            return true;
        }
        #endregion
    }
}
