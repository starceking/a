using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("case_info")]
    [DataContract]
    public class CaseInfoModel
    {
        [Key]
        [DataMember]
        public ulong id { get; set; }
        [DataMember]
        public int type_id { get; set; }//1、案件；2、不明；3、失踪
        [DataMember]
        public string cg_number { get; set; }//委托单位编码，根据操作人自动填充
        [DataMember]
        public string cg_name { get; set; }//委托单位名称，根据操作人自动填充
        [DataMember]
        public string cg_mobile { get; set; }//委托单位电话
        [DataMember]
        public string cg_fax { get; set; }//传真
        [DataMember]
        public DateTime cg_day { get; set; }//委托日期
        [DataMember]
        public string cg_addr { get; set; }//地址
        [DataMember]
        public string cg_postcode { get; set; }//邮编
        [DataMember]
        public string cg_man1 { get; set; }//委托人1
        [DataMember]
        public string cg_mobile1 { get; set; }//电话
        [DataMember]
        public string cg_cre_type1 { get; set; }//证件类型
        [DataMember]
        public string cg_cre_number1 { get; set; }//号码
        [DataMember]
        public string cg_duty1 { get; set; }//职务，下同
        [DataMember]
        public string cg_man2 { get; set; }
        [DataMember]
        public string cg_mobile2 { get; set; }
        [DataMember]
        public string cg_cre_type2 { get; set; }
        [DataMember]
        public string cg_cre_number2 { get; set; }
        [DataMember]
        public string cg_duty2 { get; set; }
        [DataMember]
        public string lab_no { get; set; }//实验室编码
        [DataMember]
        public string id_request { get; set; }//鉴定要求
        [DataMember]
        public string cg_summary { get; set; }//委托详情
        [DataMember]
        public string id_src_info { get; set; }//原鉴定情况
        [DataMember]
        public string id_reason { get; set; }//重新鉴定理由
        [DataMember]
        public string cg_remark { get; set; }//备注
        [DataMember]
        public string case_name { get; set; }//案件名称
        [DataMember]
        public string case_type { get; set; }//类型
        [DataMember]
        public string case_property { get; set; }//性质
        [DataMember]
        public DateTime case_day { get; set; }//案发时间
        [DataMember]
        public string case_addr_number { get; set; }//地点编码
        [DataMember]
        public string case_addr { get; set; }//地点
        [DataMember]
        public string case_level { get; set; }//级别
        [DataMember]
        public string case_summary { get; set; }//简要案情
        [DataMember]
        public int case_status_id { get; set; }//-1拒绝受理；1、待送检；2、待受理；3、已受理;
        [DataMember]
        public string consign_number { get; set; }//委托编号
        [DataMember]
        public string accept_number { get; set; }//受理编号
        [DataMember]
        public string accept_remark { get; set; }//受理意见
        [DataMember]
        public int accept_user_id { get; set; }//受理人、第一鉴定人
        [DataMember]
        public string ref_sys_number1 { get; set; }//关联系统编号1
        [DataMember]
        public string ref_sys_number2 { get; set; }
        [DataMember]
        public string ref_sys_number3 { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
