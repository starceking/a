using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("hsi_plan")]
    [DataContract]
    public class HsiPlanModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public int hsi_account_id { get; set; }
        [DataMember]
        public int stock_amount { get; set; }
        [DataMember]
        public decimal money_debt { get; set; }
        [DataMember]
        public decimal money_margin { get; set; }
        [DataMember]
        public decimal money_fee { get; set; }
        [DataMember]
        public decimal profit { get; set; }
        [DataMember]
        public decimal cmp_earn { get; set; }
        [DataMember]
        public DateTime create_date { get; set; }
        [DataMember]
        public int plan_status_id { get; set; }
        [DataMember]
        public int sys_user_id { get; set; }
        [DataMember]
        public DateTime end_time { get; set; }
        [DataMember]
        public int sub_amount3 { get; set; }
        [DataMember]
        public int sub_amount4 { get; set; }
        [DataMember]
        public int sub_amount5 { get; set; }
        [DataMember]
        public int sub_amount6 { get; set; }
        [DataMember]
        public int sub_amount7 { get; set; }
        [DataMember]
        public int oper_amount { get; set; }
        //外键信息
        [DataMember]
        public string hsi_account_number { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
