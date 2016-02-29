using Dapper.Contrib.Extensions;
using System;
using System.Runtime.Serialization;

namespace Model
{
    [Table("news")]
    [DataContract]
    public class NewsModel
    {
        [Key]
        [DataMember]
        public long newsID { get; set; }
        [DataMember]
        public string newsTitle { get; set; }
        [DataMember]
        public string newsSummary { get; set; }
        [DataMember]
        public string newsBody { get; set; }
        [DataMember]
        public string newsURL { get; set; }
        [DataMember]
        public string newsOriginSource { get; set; }
        [DataMember]
        public string newsAuthor { get; set; }
        [DataMember]
        public string newsPublishSite { get; set; }
        [DataMember]
        public DateTime? newsInsertTime { get; set; }
        [DataMember]
        public DateTime? newsPublishTime { get; set; }
    }
}
