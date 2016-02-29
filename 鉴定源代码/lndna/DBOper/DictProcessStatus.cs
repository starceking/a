using System.Collections.Generic;

namespace DBOper
{
    public static class DictProcessStatus
    {
        static IDictionary<int, string> list = null;
        public static IDictionary<int, string> LIST
        {
            get
            {
                if (list == null)
                {
                    list = new Dictionary<int, string>();
                    list.Add(1, "未处理");
                    list.Add(2, "处理中");
                    list.Add(3, "处理完成");
                    list.Add(-1, "拒绝");
                    list.Add(-2, "处理失败");
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
