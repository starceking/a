using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("stock_plan_xrd")]
    [DataContract]
    public class StockPlanXrdModel
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string stock_no { get; set; }
        [DataMember]
        public string stock_name { get; set; }
        [DataMember]
        public int amount { get; set; }
        [DataMember]
        public DateTime create_time { get; set; }
        [DataMember]
        public DateTime finish_time { get; set; }
        [DataMember]
        public DateTime cancel_time { get; set; }
        [DataMember]
        public int process_status_id { get; set; }
        [DataMember]
        public int sys_user_id { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
