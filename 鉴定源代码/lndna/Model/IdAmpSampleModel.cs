using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("id_amp_sample")]
    [DataContract]
    public class IdAmpSampleModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public ulong id_amp_id { get; set; }
        [DataMember]
        public ulong case_sample_id { get; set; }
        [DataMember]
        public string position { get; set; }
        [DataMember]
        public int amount { get; set; }
        [DataMember]
        public int user_id { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
