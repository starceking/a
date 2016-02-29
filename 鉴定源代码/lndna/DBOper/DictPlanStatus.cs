using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBOper
{
    public static class DictPlanStatus
    {
        static IDictionary<int, string> list = null;
        public static IDictionary<int, string> LIST
        {
            get
            {
                if (list == null)
                {
                    list = new Dictionary<int, string>();
                    list.Add(1, "匹配中");
                    list.Add(2, "交易中");
                    list.Add(3, "持仓中");
                    list.Add(4, "待平仓");
                    list.Add(5, "平仓中");
                    list.Add(6, "待结算");
                    list.Add(7, "已结算");
                    list.Add(-1, "已流单");
                }
                return list;
            }
        }
        public static string GetName(int id)
        {
            if (LIST.ContainsKey(id)) return LIST[id];
            return string.Empty;
        }
    }
}
