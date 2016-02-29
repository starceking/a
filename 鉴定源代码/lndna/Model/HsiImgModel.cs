using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class HsiImgModel
    {
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public string disk { get; set; }
        [DataMember]
        public string url { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
