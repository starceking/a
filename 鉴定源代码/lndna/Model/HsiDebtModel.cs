using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("hsi_debt")]
    [DataContract]
    public class HsiDebtModel
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int amount { get; set; }
        [DataMember]
        public decimal money_debt { get; set; }
        [DataMember]
        public decimal fee { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
