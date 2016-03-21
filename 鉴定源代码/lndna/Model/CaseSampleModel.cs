using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("case_sample")]
    [DataContract]
    public class CaseSampleModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public ulong case_info_id { get; set; }//案件id
        [DataMember]
        public string number { get; set; }//样本编码
        [DataMember]
        public string name { get; set; }//名称
        [DataMember]
        public string sample_type { get; set; }//样本类型
        [DataMember]
        public string description { get; set; }//描述
        [DataMember]
        public string remark { get; set; }//备注
        [DataMember]
        public string accept_remark { get; set; }//受理意见
        [DataMember]
        public int id_status_id { get; set; }//-1拒绝受理；1、待送检；2、待受理；3、已受理;4、检验中
        [DataMember]
        public int accept_user_id { get; set; }//一检人
        [DataMember]
        public string ref_table { get; set; }//对应表：case_evidence、case_person
        [DataMember]
        public ulong ref_id { get; set; }//对应id
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
