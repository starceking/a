using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("sys_user_stock_money_change")]
    [DataContract]
    public class SysUserStockMoneyChangeModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public int user_id { get; set; }
        [DataMember]
        public int money_flow_id { get; set; }
        [DataMember]
        public decimal money { get; set; }
        [DataMember]
        public decimal final_money { get; set; }
        [DataMember]
        public string info { get; set; }
        [DataMember]
        public string ref_table { get; set; }
        [DataMember]
        public ulong ref_id { get; set; }
        [DataMember]
        public DateTime create_time { get; set; }
        [DataMember]
        public int sys_user_id { get; set; }
        [DataMember]
        public string stock_no { get; set; }
        [DataMember]
        public string stock_name { get; set; }
        //导出数据时使用
        [DataMember]
        public string user_name { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
