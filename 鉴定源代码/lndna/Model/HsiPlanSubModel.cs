using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("hsi_plan_sub")]
    [DataContract]
    public class HsiPlanSubModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public ulong user_id { get; set; }
        [DataMember]
        public int hsi_account_id { get; set; }
        [DataMember]
        public int hsi_account_sub_id { get; set; }
        [DataMember]
        public int stock_amount { get; set; }
        [DataMember]
        public decimal money_debt { get; set; }
        [DataMember]
        public decimal money_margin { get; set; }
        [DataMember]
        public decimal money_fee { get; set; }
        [DataMember]
        public decimal profit_d { get; set; }
        [DataMember]
        public decimal profit { get; set; }
        [DataMember]
        public DateTime create_date { get; set; }
        [DataMember]
        public int plan_status_id { get; set; }
        [DataMember]
        public int sys_user_id { get; set; }
        [DataMember]
        public DateTime start_time { get; set; }
        [DataMember]
        public DateTime end_time { get; set; }
        [DataMember]
        public DateTime sell_time { get; set; }
        //外键信息
        [DataMember]
        public string user_nick_name { get; set; }
        [DataMember]
        public string user_mobile { get; set; }
        [DataMember]
        public string user_name { get; set; }
        [DataMember]
        public string hsi_account_number { get; set; }
        [DataMember]
        public string hsi_account_sub_number { get; set; }
        [DataMember]
        public string hsi_account_sub_pwd { get; set; }
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
