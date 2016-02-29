using Dapper.Contrib.Extensions;
using System;
using System.Runtime.Serialization;

namespace Model
{
    [Table("user")]
    [DataContract]
    public class UserModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public string login_pwd { get; set; }
        [DataMember]
        public string mobile { get; set; }
        [DataMember]
        public string nick_name { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string id_card_no { get; set; }
        [DataMember]
        public int gender_id { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string qq { get; set; }
        [DataMember]
        public decimal money { get; set; }
        [DataMember]
        public decimal money_npay { get; set; }
        [DataMember]
        public DateTime create_time { get; set; }
        [DataMember]
        public int delete_flag { get; set; }
        [DataMember]
        public decimal profit_from_ref { get; set; }
        [DataMember]
        public ulong ref_id { get; set; }
        //投资人信息
        [DataMember]
        public int sys_user_id { get; set; }
        [DataMember]
        public string sys_user_name { get; set; }
        //stock_plan
        [DataMember]
        public int stock_plan_amount { get; set; }
        [DataMember]
        public decimal stock_plan_earn { get; set; }
        [DataMember]
        public decimal stock_plan_loss { get; set; }
        [DataMember]
        public decimal stock_plan_debt { get; set; }
        [DataMember]
        public decimal stock_plan_debt_delay { get; set; }
        //hsi
        [DataMember]
        public int hsi_account_sub_id { get; set; }
        [DataMember]
        public int hsi_plan_amount { get; set; }
        [DataMember]
        public decimal hsi_plan_profit { get; set; }
        //ref_info
        [DataMember]
        public string ref_info { get; set; }
        //外部销售团队
        [DataMember]
        public int cmp_id { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
        [DataMember]
        public string refresh_token { get; set; }
    }
}
