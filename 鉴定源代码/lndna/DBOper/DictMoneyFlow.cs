using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBOper
{
    public static class DictMoneyFlow
    {
        static IDictionary<int, string> list = null;
        public static IDictionary<int, string> LIST
        {
            get
            {
                if (list == null)
                {
                    list = new Dictionary<int, string>();
                    list.Add(1, "收入");
                    list.Add(2, "支出");
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
