using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("stock_plan")]
    [DataContract]
    public class StockPlanModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public ulong user_id { get; set; }
        [DataMember]
        public string stock_no { get; set; }
        [DataMember]
        public string stock_name { get; set; }
        [DataMember]
        public decimal money_debt { get; set; }
        [DataMember]
        public int stock_amount { get; set; }
        [DataMember]
        public int stock_amount_already { get; set; }
        [DataMember]
        public DateTime start_date { get; set; }
        [DataMember]
        public DateTime end_date { get; set; }
        [DataMember]
        public int defer_times { get; set; }
        [DataMember]
        public decimal defer_fee { get; set; }
        [DataMember]
        public decimal money_npay { get; set; }
        [DataMember]
        public decimal defer_ref_fee { get; set; }
        [DataMember]
        public decimal money_fee { get; set; }
        [DataMember]
        public decimal money_margin { get; set; }
        [DataMember]
        public decimal stop_loss_money { get; set; }
        [DataMember]
        public decimal stop_earn_point { get; set; }
        [DataMember]
        public decimal stop_loss_point { get; set; }
        [DataMember]
        public int stop_earn_percent { get; set; }
        [DataMember]
        public int plan_status_id { get; set; }
        [DataMember]
        public string remark { get; set; }
        [DataMember]
        public int sys_user_id { get; set; }
        [DataMember]
        public decimal buy_price { get; set; }
        [DataMember]
        public DateTime buy_time { get; set; }
        [DataMember]
        public decimal sell_price { get; set; }
        [DataMember]
        public DateTime sell_time { get; set; }
        [DataMember]
        public decimal profit { get; set; }
        [DataMember]
        public decimal user_profit { get; set; }
        [DataMember]
        public decimal user_money { get; set; }
        [DataMember]
        public decimal sys_user_money { get; set; }
        [DataMember]
        public string po_user { get; set; }
        //导出数据时使用
        [DataMember]
        public string user_nick_name { get; set; }
        [DataMember]
        public string user_mobile { get; set; }
        [DataMember]
        public string user_name { get; set; }
        [DataMember]
        public string sys_user_name { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
        //辅助记录
        [DataMember]
        public int start_amount { get; set; }
        [DataMember]
        public decimal start_price { get; set; }
        [DataMember]
        public DateTime start_time { get; set; }
        [DataMember]
        public decimal end_price { get; set; }
        //外部销售团队
        [DataMember]
        public int cmp_id { get; set; }
        //除权除息
        [DataMember]
        public int xrd_id { get; set; }
        [DataMember]
        public int xrd_amount { get; set; }
        [DataMember]
        public decimal xrd_price { get; set; }
    }
}
