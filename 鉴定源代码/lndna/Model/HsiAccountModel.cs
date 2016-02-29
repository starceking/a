using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("hsi_account")]
    [DataContract]
    public class HsiAccountModel
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int src_id { get; set; }
        [DataMember]
        public int calc_id { get; set; }
        [DataMember]
        public string number { get; set; }
        [DataMember]
        public int sub_amount { get; set; }
        [DataMember]
        public int sub_used { get; set; }
        [DataMember]
        public int sub_free { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
