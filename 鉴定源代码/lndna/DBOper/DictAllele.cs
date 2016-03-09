using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBOper
{
    /// <summary>
    /// 基因座设置
    /// </summary>
    public static class DictAllele
    {
        public static IList<DictAlleleModel> LIST;

        public static void Init()
        {
            LIST = new List<DictAlleleModel>();
            if (DictSettings.TEST_MODE)
            {
                LIST.Add(new DictAlleleModel { name = "AMEL", imp_flag = 1, vals = "X/Y" });
            }
            else
            {
                //dna库中读取初始化
            }
        }
    }
}
