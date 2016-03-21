using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("case_evidence")]
    [DataContract]
    public class CaseEvidenceModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public ulong case_info_id { get; set; }//案件id
        [DataMember]
        public string name { get; set; }//名称
        [DataMember]
        public string evi_type { get; set; }//物证类型
        [DataMember]
        public string description { get; set; }//描述
        [DataMember]
        public string remark { get; set; }//备注
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
