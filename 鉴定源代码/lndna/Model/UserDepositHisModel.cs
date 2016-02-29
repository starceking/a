using Dapper.Contrib.Extensions;
using System;
using System.Runtime.Serialization;

namespace Model
{
    [Table("user_deposit_his")]
    [DataContract]
    public class UserDepositHisModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public string number { get; set; }
        [DataMember]
        public int deposit_src_id { get; set; }
        [DataMember]
        public ulong user_id { get; set; }
        [DataMember]
        public decimal money { get; set; }
        [DataMember]
        public string info { get; set; }
        [DataMember]
        public DateTime create_time { get; set; }
        [DataMember]
        public int sys_user_id { get; set; }
        //导出数据时使用
        [DataMember]
        public string user_nick_name { get; set; }
        [DataMember]
        public string user_mobile { get; set; }
        [DataMember]
        public string user_name { get; set; }
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
