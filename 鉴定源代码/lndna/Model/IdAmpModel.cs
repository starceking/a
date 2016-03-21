using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("id_amp")]
    [DataContract]
    public class IdAmpModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public string number { get; set; }//扩增编号
        [DataMember]
        public string name { get; set; }//名称
        [DataMember]
        public int user_id { get; set; }//实验人
        [DataMember]
        public string shelf_type { get; set; }//试剂架
        [DataMember]
        public DateTime id_day { get; set; }//实验日期
        [DataMember]
        public string method { get; set; }//扩增方法
        [DataMember]
        public string machine { get; set; }//扩增仪器
        [DataMember]
        public string kit { get; set; }//试剂盒
        [DataMember]
        public int amount { get; set; }//扩增体系
        [DataMember]
        public string batch { get; set; }//批号
        [DataMember]
        public int circle { get; set; }//循环数
        [DataMember]
        public string board { get; set; }//板号
        [DataMember]
        public string sample_ids { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
