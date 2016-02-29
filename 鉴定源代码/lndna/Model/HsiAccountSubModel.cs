using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("hsi_account_sub")]
    [DataContract]
    public class HsiAccountSubModel
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int account_id { get; set; }
        [DataMember]
        public string number { get; set; }
        [DataMember]
        public string pwd { get; set; }
        [DataMember]
        public ulong user_id { get; set; }
        [DataMember]
        public DateTime last_plan_date { get; set; }
        //导出数据时使用
        [DataMember]
        public string user_nick_name { get; set; }
        [DataMember]
        public string user_mobile { get; set; }
        [DataMember]
        public string user_name { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
