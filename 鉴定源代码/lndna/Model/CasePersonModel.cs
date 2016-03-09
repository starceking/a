using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("case_person")]
    [DataContract]
    public class CasePersonModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public ulong case_info_id { get; set; }
        [DataMember]
        public int person_case_type { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string gender { get; set; }
        [DataMember]
        public string nation { get; set; }
        [DataMember]
        public string id_card_no { get; set; }
        [DataMember]
        public string id_type { get; set; }
        [DataMember]
        public string id_number { get; set; }
        [DataMember]
        public string person_type { get; set; }
        [DataMember]
        public string spec { get; set; }
        [DataMember]
        public DateTime? birthday { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string alias { get; set; }
        [DataMember]
        public string hjd_number { get; set; }
        [DataMember]
        public string hjd_addr { get; set; }
        [DataMember]
        public string xzz_number { get; set; }
        [DataMember]
        public string xzz_addr { get; set; }
        [DataMember]
        public string remark { get; set; }
        [DataMember]
        public int age { get; set; }
        [DataMember]
        public DateTime? missing_day { get; set; }
        [DataMember]
        public string missing_addr { get; set; }
        [DataMember]
        public ulong relative_id { get; set; }
        [DataMember]
        public string relative_type { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
