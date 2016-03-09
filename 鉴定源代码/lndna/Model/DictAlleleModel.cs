using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //基因座设置
    public class DictAlleleModel
    {
        public string name { get; set; }//名称
        public int imp_flag { get; set; }//是否核心基因座：0、1
        public string vals { get; set; }//取值范围：10/11/12/14/14.5
    }
}
