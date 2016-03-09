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
        public ulong case_info_id { get; set; }
        [DataMember]
        public string number { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string sample_type { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string remark { get; set; }
        [DataMember]
        public string accept_remark { get; set; }
        [DataMember]
        public int id_status_id { get; set; }
        [DataMember]
        public string ref_table { get; set; }
        [DataMember]
        public ulong ref_id { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
