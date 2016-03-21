using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("user")]
    [DataContract]
    public class UserModel
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string dept_no { get; set; }
        [DataMember]
        public string police_no { get; set; }
        [DataMember]
        public string login_pwd { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string id_card_no { get; set; }
        [DataMember]
        public string auth_ids { get; set; }
        [DataMember]
        public int cg_flag { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
