using System.Collections.Generic;

namespace DBOper
{
    public static class DictDepositSrc
    {
        static IDictionary<int, string> list = null;
        public static IDictionary<int, string> LIST
        {
            get
            {
                if (list == null)
                {
                    list = new Dictionary<int, string>();
                    list.Add(1, "网银");
                    list.Add(2, "通联");
                    list.Add(3, "支付宝");
                    list.Add(4, "微信");
                    list.Add(99, "人工");
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
