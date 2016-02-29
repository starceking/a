using Dapper.Contrib.Extensions;
using System;
using System.Runtime.Serialization;

namespace Model
{
    [Table("notice")]
    [DataContract]
    public class NoticeModel
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string head { get; set; }
        [DataMember]
        public string content { get; set; }
        [DataMember]
        public DateTime create_time { get; set; }
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
