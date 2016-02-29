using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBOper
{
    public static class DictHsiPlanStatus
    {
        static IDictionary<int, string> list = null;
        public static IDictionary<int, string> LIST
        {
            get
            {
                if (list == null)
                {
                    list = new Dictionary<int, string>();
                    list.Add(1, "准备中");
                    list.Add(2, "入金中");
                    list.Add(3, "操盘中");
                    list.Add(4, "待平仓");
                    list.Add(5, "已平仓");
                    list.Add(6, "待清算");
                    list.Add(7, "已清算");
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
