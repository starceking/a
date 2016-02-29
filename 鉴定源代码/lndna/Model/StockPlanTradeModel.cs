using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("stock_plan_trade")]
    [DataContract]
    public class StockPlanTradeModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public ulong plan_id { get; set; }
        [DataMember]
        public int buy_or_sell { get; set; }
        [DataMember]
        public DateTime trade_time { get; set; }
        [DataMember]
        public decimal price { get; set; }
        [DataMember]
        public int amount { get; set; }
        [DataMember]
        public string trade_no { get; set; }
        [DataMember]
        public int sys_user_id { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
