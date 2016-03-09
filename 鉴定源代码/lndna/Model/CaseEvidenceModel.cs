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
        public ulong case_info_id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string evi_type { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string remark { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
