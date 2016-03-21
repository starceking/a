using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("case_person")]
    [DataContract]
    public class CasePersonModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }//详见：人员对应字段.xlsx
        [DataMember]
        public ulong case_info_id { get; set; }//案件id
        [DataMember]
        public int person_case_type { get; set; }//1、嫌疑人；2、受害人；3、其它；4、身份不明；5、失踪；6、亲属
        [DataMember]
        public string name { get; set; }//姓名
        [DataMember]
        public string gender { get; set; }//性别
        [DataMember]
        public string nation { get; set; }//民族
        [DataMember]
        public string id_card_no { get; set; }//身份证
        [DataMember]
        public string id_type { get; set; }//证件类型
        [DataMember]
        public string id_number { get; set; }//证件号
        [DataMember]
        public string person_type { get; set; }//人员类型
        [DataMember]
        public string spec { get; set; }//特性
        [DataMember]
        public DateTime? birthday { get; set; }//生日
        [DataMember]
        public string country { get; set; }//国家
        [DataMember]
        public string alias { get; set; }//昵称
        [DataMember]
        public string hjd_number { get; set; }//户籍地
        [DataMember]
        public string hjd_addr { get; set; }
        [DataMember]
        public string xzz_number { get; set; }//现住址
        [DataMember]
        public string xzz_addr { get; set; }
        [DataMember]
        public string remark { get; set; }//备注
        [DataMember]
        public int age { get; set; }//年龄
        [DataMember]
        public DateTime? missing_day { get; set; }//失踪日期
        [DataMember]
        public string missing_addr { get; set; }//失踪地点
        [DataMember]
        public ulong relative_id { get; set; }//关联亲属的id
        [DataMember]
        public string relative_type { get; set; }//亲属类型
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
