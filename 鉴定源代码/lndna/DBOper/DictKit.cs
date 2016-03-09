using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBOper
{
    /// <summary>
    /// 试剂盒设置
    /// </summary>
    public static class DictKit
    {
        public static IList<DictKitModel> LIST;

        public static void Init()
        {
            LIST = new List<DictKitModel>();
            if (DictSettings.TEST_MODE)
            {
                LIST.Add(new DictKitModel { number = "001", name = "sjh1" });
            }
            else
            {
                //dna库中读取初始化
            }
        }
    }
}
