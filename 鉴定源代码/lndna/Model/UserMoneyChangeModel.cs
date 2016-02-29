using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("user_money_change")]
    [DataContract]
    public class UserMoneyChangeModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public ulong user_id { get; set; }
        [DataMember]
        public int money_flow_id { get; set; }
        [DataMember]
        public decimal money { get; set; }
        [DataMember]
        public decimal final_money { get; set; }
        [DataMember]
        public string info { get; set; }
        [DataMember]
        public string ref_table { get; set; }
        [DataMember]
        public ulong ref_id { get; set; }
        [DataMember]
        public DateTime create_time { get; set; }
        [DataMember]
        public int sys_user_id { get; set; }
        //外部销售团队
        [DataMember]
        public int cmp_id { get; set; }
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
