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
        public int type_id { get; set; }
        [DataMember]
        public string cg_number { get; set; }
        [DataMember]
        public string cg_name { get; set; }
        [DataMember]
        public string cg_mobile { get; set; }
        [DataMember]
        public string cg_fax { get; set; }
        [DataMember]
        public DateTime cg_day { get; set; }
        [DataMember]
        public string cg_addr { get; set; }
        [DataMember]
        public string cg_postcode { get; set; }
        [DataMember]
        public string cg_man1 { get; set; }
        [DataMember]
        public string cg_mobile1 { get; set; }
        [DataMember]
        public string cg_cre_type1 { get; set; }
        [DataMember]
        public string cg_cre_number1 { get; set; }
        [DataMember]
        public string cg_man2 { get; set; }
        [DataMember]
        public string cg_mobile2 { get; set; }
        [DataMember]
        public string cg_cre_type2 { get; set; }
        [DataMember]
        public string cg_cre_number2 { get; set; }
        [DataMember]
        public string lab_no { get; set; }
        [DataMember]
        public string id_request { get; set; }
        [DataMember]
        public string cg_summary { get; set; }
        [DataMember]
        public string id_src_info { get; set; }
        [DataMember]
        public string id_reason { get; set; }
        [DataMember]
        public string cg_remark { get; set; }
        [DataMember]
        public string case_name { get; set; }
        [DataMember]
        public string case_type { get; set; }
        [DataMember]
        public string case_property { get; set; }
        [DataMember]
        public string case_day { get; set; }
        [DataMember]
        public string case_addr_number { get; set; }
        [DataMember]
        public string case_addr { get; set; }
        [DataMember]
        public string case_level { get; set; }
        [DataMember]
        public string case_summary { get; set; }
        [DataMember]
        public int case_status_id { get; set; }
        [DataMember]
        public string consign_number { get; set; }
        [DataMember]
        public string accept_number { get; set; }
        [DataMember]
        public string accept_remark { get; set; }
        [DataMember]
        public string ref_sys_number1 { get; set; }
        [DataMember]
        public string ref_sys_number2 { get; set; }
        [DataMember]
        public string ref_sys_number3 { get; set; }
        [DataMember]
        public string create_dept_number { get; set; }
        [DataMember]
        public string create_lab_no { get; set; }
        //access_token
        [DataMember]
        public int access_id { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }
}
