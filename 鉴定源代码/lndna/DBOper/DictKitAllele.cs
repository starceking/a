using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBOper
{
    /// <summary>
    /// 试剂盒对应基因座
    /// </summary>
    public static class DictKitAllele
    {
        public static IDictionary<string, IList<DictAlleleModel>> LIST;

        public static void Init()
        {
            LIST = new Dictionary<string, IList<DictAlleleModel>>();
            if (DictSettings.TEST_MODE)
            {
                IList<DictAlleleModel> aList1 = new List<DictAlleleModel>();
                aList1.Add(new DictAlleleModel { name = "AMEL", imp_flag = 1, vals = "X/Y" });
                LIST.Add("sjh1", aList1);
            }
            else
            {
                //dna库中读取初始化
            }
        }
    }
}
