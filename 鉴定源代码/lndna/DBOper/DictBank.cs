using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBOper
{
    public static class DictBank
    {
        static IDictionary<int, string> list = null;
        public static IDictionary<int, string> LIST
        {
            get
            {
                if (list == null)
                {
                    list = new Dictionary<int, string>();
                    list.Add(1, "工商银行");
                    list.Add(2, "农业银行");
                    list.Add(3, "中国银行");
                    list.Add(4, "建设银行");
                    list.Add(5, "交通银行");
                    list.Add(6, "中信银行");
                    list.Add(7, "光大银行");
                    list.Add(8, "华夏银行");
                    list.Add(9, "民生银行");
                    list.Add(10, "广发银行");
                    list.Add(11, "平安银行");
                    list.Add(12, "招商银行");
                    list.Add(13, "兴业银行");
                    list.Add(14, "上海浦发银行");
                    list.Add(15, "恒丰银行");
                    list.Add(16, "浙商银行");
                    list.Add(17, "渤海银行");
                    list.Add(18, "徽商银行");
                    list.Add(19, "邮政储蓄银行");
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
