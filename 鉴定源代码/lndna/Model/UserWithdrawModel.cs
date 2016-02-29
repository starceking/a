using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("user_withdraw")]
    [DataContract]
    public class UserWithdrawModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public ulong user_id { get; set; }
        [DataMember]
        public ulong user_bank_id { get; set; }
        [DataMember]
        public int sys_user_id { get; set; }
        [DataMember]
        public int process_status_id { get; set; }
        [DataMember]
        public decimal money { get; set; }
        [DataMember]
        public DateTime create_time { get; set; }
        [DataMember]
        public DateTime finish_time { get; set; }
        [DataMember]
        public string remark { get; set; }
        //外键信息
        [DataMember]
        public string user_name { get; set; }
        [DataMember]
        public string user_mobile { get; set; }
        [DataMember]
        public string branch_name { get; set; }
        [DataMember]
        public string card_no { get; set; }
        [DataMember]
        public string bank_name { get; set; }
        [DataMember]
        public string province_name { get; set; }
        [DataMember]
        public string city_name { get; set; }
        //外部销售团队
        [DataMember]
        public int cmp_id { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
