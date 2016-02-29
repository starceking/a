using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("stock_plan_day")]
    [DataContract]
    public class StockPlanDayModel
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public DateTime sta_day { get; set; }
        [DataMember]
        public int person { get; set; }
        [DataMember]
        public int cs { get; set; }
        [DataMember]
        public decimal money_debt { get; set; }
        [DataMember]
        public decimal money_margin { get; set; }
        [DataMember]
        public decimal profit_earn { get; set; }
        [DataMember]
        public decimal profit_loss { get; set; }
        [DataMember]
        public decimal start_fee { get; set; }
        [DataMember]
        public decimal delay_fee { get; set; }
        [DataMember]
        public decimal delay_ref_fee { get; set; }
        [DataMember]
        public decimal cc_loss { get; set; }
        [DataMember]
        public decimal tp_loss { get; set; }
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
