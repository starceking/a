using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("id_comparison")]
    [DataContract]
    public class IdComparisonModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public string number { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int amount { get; set; }
        [DataMember]
        public string ref_table { get; set; }//id_pre、id_extract
        [DataMember]
        public ulong ref_id { get; set; }
        [DataMember]
        public int user_id { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
