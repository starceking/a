using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class HsiDebt
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        const string TABLE = "hsi_debt";
        /// <summary>
        /// 每手保证金
        /// </summary>
        public const decimal MARGIN = 10000;
        #endregion
        #region 一般操作
        public static async Task<string> Insert(int amount, decimal money_debt, decimal fee)
        {
            if (amount <= 0 || money_debt <= 0 || fee < 0) return "参数不全";
            if (DICT.ContainsKey(amount)) return "该配置已存在";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("amount", amount.ToString());
            dict.Add("money_debt", money_debt.ToString());
            dict.Add("fee", fee.ToString());
            await DBHelper.Insert(TABLE, dict);

            HsiDebtModel model = new HsiDebtModel() { amount = amount, money_debt = money_debt, fee = fee };
            lock (lockDict)
            {
                DICT.Add(amount, model);
            }

            return string.Empty;
        }
        public static async Task<string> Update(int id, int amount, decimal money_debt, decimal fee)
        {
            if (id <= 0 || amount <= 0 || money_debt <= 0 || fee < 0) return "参数不全";
            HsiDebtModel model = await GetOne(id);
            if (model == null) return "读取不到MODEL";
            if (model.amount != amount && DICT.ContainsKey(amount)) return "该配置已存在";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("amount", amount.ToString());
            dict.Add("money_debt", money_debt.ToString());
            dict.Add("fee", fee.ToString());
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            await DBHelper.Update(TABLE, dict, fdict, "and");

            await DeleteRedis(id);

            if (model.amount != amount)
            {
                if (DICT.ContainsKey(model.amount))
                {
                    lock (lockDict)
                    {
                        DICT.Remove(model.amount);
                    }
                }
                model.amount = amount;
                model.money_debt = money_debt;
                model.fee = fee;
                lock (lockDict)
                {
                    DICT.Add(amount, model);
                }
            }
            else
            {
                lock (lockDict)
                {
                    DICT[amount].money_debt = money_debt;
                    DICT[amount].fee = fee;
                }
            }

            return string.Empty;
        }
        public static async Task<string> Delete(int id)
        {
            if (id <= 0) return "参数不全";
            HsiDebtModel model = await GetOne(id);
            if (model == null) return "读取不到MODEL";

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            await DBHelper.Delete(TABLE, fdict, "and");

            await DeleteRedis(id);

            if (DICT.ContainsKey(model.amount))
            {
                lock (lockDict)
                {
                    DICT.Remove(model.amount);
                }
            }

            return string.Empty;
        }
        public static async Task<IEnumerable<HsiDebtModel>> GetList()
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            return await DBHelper.GetList<HsiDebtModel, int>(TABLE, "*", "id", fdict, "and");
        }
        public static async Task<HsiDebtModel> GetOne(int id)
        {
            if (id <= 0) return null;
            return await DBHelper.GetOne<HsiDebtModel>(id.ToString());
        }
        public static async Task DeleteRedis(int id)
        {
            if (id <= 0) return;
            await Redis.Delete<HsiDebtModel>(id.ToString());
        }
        #endregion
        #region 手数映射
        static IDictionary<int, HsiDebtModel> DICT;
        static object lockDict = new object();
        public static void Init()
        {
            if (DICT == null)
            {
                DICT = new Dictionary<int, HsiDebtModel>();
                IEnumerable<HsiDebtModel> list = DBHelper.GetListSync<HsiDebtModel, long>(TABLE, "*", "id", new Dictionary<string, string>(), "and");
                foreach (HsiDebtModel model in list)
                {
                    if (!DICT.ContainsKey(model.amount))
                    {
                        DICT.Add(model.amount, model);
                    }
                }
            }
        }
        public static HsiDebtModel GetDebt(int amount)
        {
            if (DICT.ContainsKey(amount)) return DICT[amount];
            return null;
        }
        #endregion
    }
}
