using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("stock_plan_log")]
    [DataContract]
    public class StockPlanLogModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public ulong plan_id { get; set; }
        [DataMember]
        public string log_type { get; set; }
        [DataMember]
        public string info { get; set; }
        [DataMember]
        public int sys_user_id { get; set; }
        [DataMember]
        public DateTime create_time { get; set; }
    }
}
