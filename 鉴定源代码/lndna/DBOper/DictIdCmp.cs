using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBOper
{
    /// <summary>
    /// 对照样本设置
    /// </summary>
    public static class DictIdCmp
    {
        public static IDictionary<string, IList<DictIdCmpModel>> LIST;

        public static void Init()
        {
            LIST = new Dictionary<string, IList<DictIdCmpModel>>();
            if (DictSettings.TEST_MODE)
            {
                IList<DictIdCmpModel> aList1 = new List<DictIdCmpModel>();
                aList1.Add(new DictIdCmpModel { number = "01", name = "阳性对照" });
                LIST.Add("pre_exam", aList1);
            }
            else
            {
                //dna库中读取初始化
            }
        }
    }
}
