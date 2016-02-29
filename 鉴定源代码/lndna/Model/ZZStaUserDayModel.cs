using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("zzsta_user_day")]
    [DataContract]
    public class ZZStaUserDayModel
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public DateTime sta_day { get; set; }
        [DataMember]
        public int ip_amount { get; set; }
        [DataMember]
        public int cs_amount { get; set; }
        [DataMember]
        public int reg_user { get; set; }
        [DataMember]
        public decimal reg_buy { get; set; }
        [DataMember]
        public int dep_user { get; set; }
        [DataMember]
        public int dep_buy { get; set; }
        [DataMember]
        public decimal buy_user { get; set; }
        [DataMember]
        public decimal buy_money { get; set; }
        [DataMember]
        public decimal buy_my_pj { get; set; }
        [DataMember]
        public int buy_amount { get; set; }
        [DataMember]
        public decimal buy_amt_pj { get; set; }
        [DataMember]
        public int hsi_buy_user { get; set; }
        [DataMember]
        public int hsi_buy_ss { get; set; }
        [DataMember]
        public decimal hsi_buy_ss_pj { get; set; }
        [DataMember]
        public int hsi_buy_amount { get; set; }
        [DataMember]
        public decimal hsi_buy_amt_pj { get; set; }
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
