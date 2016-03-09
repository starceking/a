using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("id_pre")]
    [DataContract]
    public class IdPreModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public string number { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int user_id { get; set; }
        [DataMember]
        public string shelf_type { get; set; }
        [DataMember]
        public DateTime id_day { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
