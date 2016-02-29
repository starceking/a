using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("zstock_settings")]
    [DataContract]
    public class ZStockSettingsModel
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string value { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
