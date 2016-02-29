using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("user_bank")]
    [DataContract]
    public class UserBankModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public ulong user_id { get; set; }
        [DataMember]
        public int bank_id { get; set; }
        [DataMember]
        public int province_id { get; set; }
        [DataMember]
        public int city_id { get; set; }
        [DataMember]
        public int district_id { get; set; }
        [DataMember]
        public string branch_name { get; set; }
        [DataMember]
        public string card_no { get; set; }
        [DataMember]
        public int default_flag { get; set; }
        [DataMember]
        public int process_status_id { get; set; }
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
