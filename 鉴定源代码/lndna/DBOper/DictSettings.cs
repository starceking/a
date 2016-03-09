using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBOper
{
    /// <summary>
    /// 一般设置
    /// </summary>
    public static class DictSettings
    {
        public static bool TEST_MODE = true;//是否测试模式

        public static int rcCeil = 2;//容差上限
        public static int ppFloor = 8;//匹配下限

        public static string DEPT_NO = ConfigurationManager.AppSettings["DeptNo"];//当前网站的所属单位（鉴定单位的编码）
        /// <summary>
        /// 判断是否上级或同级单位；例外：如果是省厅DNA，那么对应sub必须也是省厅DNA才行
        /// 如果top==true，那么省厅对所有单位返回true
        /// </summary>
        public static bool DeptIsSub(string fatr, string sub, bool top = false)
        {
            if (TEST_MODE) return true;
            if (string.IsNullOrWhiteSpace(fatr)) return false;
            if (string.IsNullOrWhiteSpace(sub)) return true;

            return true;
        }
        /// <summary>
        /// 判断是否委托单位
        /// </summary>
        public static bool DeptIsCg(string number)
        {
            return true;
        }
        public static string CgUserAuths = "";
    }
}
