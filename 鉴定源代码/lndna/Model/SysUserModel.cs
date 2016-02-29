using Dapper.Contrib.Extensions;
using System.Runtime.Serialization;

namespace Model
{
    [Table("sys_user")]
    [DataContract]
    public class SysUserModel
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string login_name { get; set; }
        [DataMember]
        public string login_pwd { get; set; }
        [DataMember]
        public string mobile { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int delete_flag { get; set; }
        //投资人信息
        [DataMember]
        public ulong user_id { get; set; }
        //stock_plan
        [DataMember]
        public int stock_plan_amount { get; set; }
        [DataMember]
        public decimal stock_plan_earn { get; set; }
        [DataMember]
        public decimal stock_plan_loss { get; set; }
        [DataMember]
        public decimal stock_money { get; set; }
        [DataMember]
        public int buy_pj { get; set; }
        [DataMember]
        public int sell_pj { get; set; }
        [DataMember]
        public int buy_onem { get; set; }
        [DataMember]
        public int sell_onem { get; set; }
        [DataMember]
        public string privilege_ids { get; set; }
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
