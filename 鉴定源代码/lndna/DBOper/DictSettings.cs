using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBOper
{
    /// <summary>
    /// 一般设置
    /// </summary>
    public static class DictSettings
    {
        #region 比对设置
        static int rcCeil = 0;//容差上限
        public static int RCCeil
        {
            get
            {
                if (rcCeil == 0)
                {
                    //dna库中读取初始化
                    rcCeil = 2;
                }
                return rcCeil;
            }
        }
        static int ppFloor = 0;//匹配下限
        public static int PPFloor
        {
            get
            {
                if (ppFloor == 0)
                {
                    //dna库中读取初始化
                    ppFloor = 8;
                }
                return ppFloor;
            }
        }
        #endregion
        #region 单位
        //当前网站的实验室编码
        public static string LAB_NO = ConfigurationManager.AppSettings["LabNo"];
        /// <summary>
        /// 判断是否上级或同级单位
        /// </summary>
        public static bool DeptIsSub(string fatr, string sub)
        {
            if (string.IsNullOrWhiteSpace(fatr)) return false;
            if (string.IsNullOrWhiteSpace(sub)) return true;
            if (fatr.Length == 6 && sub.Length == 6) return DeptIsSub(fatr, sub, 6);
            if (fatr.Length == 12 && sub.Length == 12) return DeptIsSub(fatr, sub, 12);
            return sub.StartsWith(fatr);
        }
        static bool DeptIsSub(string fatr, string sub, int len)
        {
            for (int i = 2; i <= len; i += 2)
            {
                string fatrSub = fatr.Substring(0, i);
                string subSub = sub.Substring(0, i);
                if (!fatrSub.Equals(subSub))
                {
                    if (!fatrSub.EndsWith("00")) return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 判断是否委托单位
        /// </summary>
        public static bool DeptIsCg(string number)
        {
            if (string.IsNullOrWhiteSpace(number)) return false;
            return number.Length != 6;
        }
        /// <summary>
        /// 获取所有下级单位的前缀
        /// </summary>
        public static string DebtGetSubs(string number)
        {
            while (number.EndsWith("00"))
                number = number.Substring(0, number.Length - 2);
            return number;
        }
        //委托人默认权限
        public static string CgUserAuths = "2,21,22,23";
        #endregion
        #region js文件
        /// <summary>
        /// 字典库初始化，生成js文件
        /// </summary>
        public static void DictInit(int type)
        {
            switch (type)
            {
                case 1: DictAmpMachine(); break;
                case 2: DictAmpMethod(); break;
                case 3: DictCaseLevel(); break;
                case 4: DictCaseProperty(); break;
                case 5: DictCaseType(); break;
                case 6: DictCg(); break;
                case 7: DictCountry(); break;
                case 8: DictCreType(); break;
                case 9: DictDuty(); break;
                case 10: DictEpMachine(); break;
                case 11: DictEviType(); break;
                case 12: DictExtractMachine(); break;
                case 13: DictExtractMethod(); break;
                case 14: DictGender(); break;
                case 15: DictIdCmp(); break;
                case 16: DictIdType(); break;
                case 17: DictKit(); break;
                case 18: DictLabNo(); break;
                case 19: DictNation(); break;
                case 20: DictPersonSpec(); break;
                case 21: DictPersonType(); break;
                case 22: DictPreMethod(); break;
                case 23: DictPreResult(); break;
                case 24: DictRelativeType(); break;
                case 25: DictSampleType(); break;
                case 26: DictShelfType(); break;
                case 27: DictRegion(); break;
                default: break;
            }
        }
        static void DictAmpMachine()
        {
            //扩增机器
            string js = "var dict_amp_machine_json=\"[{ 'id':'1','name':'DictAmpMachine1'},{ 'id':'2','name':'DictAmpMachine2'}]\";";
            SaveJs("dict_amp_machine_json", js);
        }
        static void DictAmpMethod()
        {
            //扩增方法
            string js = "var dict_amp_method_json=\"[{ 'id':'1','name':'DictAmpMethod1'},{ 'id':'2','name':'DictAmpMethod2'}]\";";
            SaveJs("dict_amp_method_json", js);
        }
        static void DictCaseLevel()
        {
            //案件级别
            string js = "var dict_case_level_json=\"[{ 'id':'1','name':'DictCaseLevel1'},{ 'id':'2','name':'DictCaseLevel2'}]\";";
            SaveJs("dict_case_level_json", js);
        }
        static void DictCaseProperty()
        {
            //案件性质
            string js = "var dict_case_property_json=\"[{ 'id':'1','name':'DictCaseProperty1'},{ 'id':'2','name':'DictCaseProperty2'}]\"";
            SaveJs("dict_case_property_json", js);
        }
        static void DictCaseType()
        {
            //案件类型
            string js = "var dict_case_type_json=\"[{ 'id':'1','name':'DictCaseType1'},{ 'id':'2','name':'DictCaseType2'}]\"";
            SaveJs("dict_case_type_json", js);
        }
        static void DictCg()
        {
            //委托单位
            string js = "var dict_cg_json=\"[{ 'id':'01','name':'浙江',parent_id:'000000000000'},{ 'id':'02','name':'辽宁',parent_id:'000000000000'},{ 'id':'0201','name':'沈阳',parent_id:'02'},{ 'id':'0202','name':'大连',parent_id:'02'}]\"";
            SaveJs("dict_cg_json", js);
        }
        static void DictCountry()
        {
            //国家
            string js = "var dict_country_json=\"[{ 'id':'1','name':'DictCountry1'},{ 'id':'2','name':'DictCountry2'}]\"";
            SaveJs("dict_country_json", js);
        }
        static void DictCreType()
        {
            //委托人员的证件类型
            string js = "var dict_cre_type_json=\"[{ 'id':'1','name':'DictCreType1'},{ 'id':'2','name':'DictCreType2'}]\"";
            SaveJs("dict_cre_type_json", js);
        }
        static void DictDuty()
        {
            //职务
            string js = "var dict_duty_json=\"[{ 'id':'1','name':'DictDuty1'},{ 'id':'2','name':'DictDuty2'}]\"";
            SaveJs("dict_duty_json", js);
        }
        static void DictEpMachine()
        {
            //电泳机器
            string js = "var dict_ep_machine_json=\"[{ 'id':'1','name':'DictEpMachine1'},{ 'id':'2','name':'DictEpMachine2'}]\"";
            SaveJs("dict_ep_machine_json", js);
        }
        static void DictEviType()
        {
            //物证类型
            string js = "var dict_evi_type_json=\"[{ 'id':'1','name':'DictEviType1'},{ 'id':'2','name':'DictEviType2'}]\"";
            SaveJs("dict_evi_type_json", js);
        }
        static void DictExtractMachine()
        {
            //提取仪器
            string js = "var dict_extract_machine_json=\"[{ 'id':'1','name':'DictExtractMachine1'},{ 'id':'2','name':'xx证'}]\"";
            SaveJs("dict_extract_machine_json", js);
        }
        static void DictExtractMethod()
        {
            //提取方法
            string js = "var dict_extract_method_json=\"[{ 'id':'1','name':'DictExtractMethod'},{ 'id':'2','name':'xx证'}]\"";
            SaveJs("dict_extract_method_json", js);
        }
        static void DictGender()
        {
            //性别
            string js = "var dict_gender_json=\"[{ 'id':'1','name':'DictGender'},{ 'id':'2','name':'xx证'}]\"";
            SaveJs("dict_gender_json", js);
        }
        static void DictIdCmp()
        {
            //对照样本
            string js = "var dict_id_cmp_json=\"[{ 'id':'01','name':'DictIdCmp',type:'pre_exam'},{ 'id':'02','name':'辽宁',parent_id:'000000000000'},{ 'id':'0201','name':'沈阳',parent_id:'02'},{ 'id':'0202','name':'大连',parent_id:'02'}]\"";
            SaveJs("dict_id_cmp_json", js);
        }
        static void DictIdType()
        {
            //案件人员的证件类型
            string js = "var dict_id_type_json=\"[{ 'id':'1','name':'DictIdType'},{ 'id':'2','name':'xx证'}]\"";
            SaveJs("dict_id_type_json", js);
        }
        static void DictKit()
        {
            //试剂盒
            string js = "var dict_kit_json=\"[{ 'id':'1','name':'DictKit'},{ 'id':'2','name':'xx证'}]\"";
            SaveJs("dict_kit_json", js);
        }
        static void DictLabNo()
        {
            //实验室编码
            string js = "var dict_lab_no_json=\"[{ 'id':'210000','name':'省厅'},{ 'id':'210100','name':'沈阳'},{ 'id':'210200','name':'大连'},{ 'id':'210203','name':'大连西岗'}]\"";
            SaveJs("dict_lab_no_json", js);
        }
        static void DictNation()
        {
            //民族
            string js = "var dict_nation_json=\"[{ 'id':'1','name':'DictNation'},{ 'id':'2','name':'xx证'}]\"";
            SaveJs("dict_nation_json", js);
        }
        static void DictPersonSpec()
        {
            //案件人员的特性，一堆checkbox
            string js = "var dict_person_spec_json=\"spec1,spec2,spec3\";";
            SaveJs("dict_person_spec_json", js);
        }
        static void DictPersonType()
        {
            //人员类型
            string js = "var dict_person_type_json=\"[{ 'id':'1','name':'DictPersonType'},{ 'id':'2','name':'xx证'}]\"";
            SaveJs("dict_person_type_json", js);
        }
        static void DictPreMethod()
        {
            //预实验方法
            string js = "var dict_pre_method_json=\"[{ 'id':'1','name':'DictPreMethod'},{ 'id':'2','name':'xx证'}]\"";
            SaveJs("dict_pre_method_json", js);
        }
        static void DictPreResult()
        {
            //预实验结果
            string js = "var dict_pre_result_json=\"[{ 'id':'1','name':'DictPreResult'},{ 'id':'2','name':'xx证'}]\"";
            SaveJs("dict_pre_result_json", js);
        }
        static void DictRelativeType()
        {
            //亲属类型
            string js = "var dict_relative_type_json=\"[{ 'id':'1','name':'DictRelativeType'},{ 'id':'2','name':'xx证'}]\"";
            SaveJs("dict_relative_type_json", js);
        }
        static void DictSampleType()
        {
            //样本类型
            string js = "var dict_sample_type_json=\"[{ 'id':'1','name':'DictSampleType'},{ 'id':'2','name':'xx证'}]\"";
            SaveJs("dict_sample_type_json", js);
        }
        static void DictShelfType()
        {
            //试剂架
            string js = "var dict_shelf_type_json=\"[{ 'id':'1','name':'DictShelfType'},{ 'id':'2','name':'xx证'}]\"";
            SaveJs("dict_shelf_type_json", js);
        }
        static void DictRegion()
        {
            //行政区划
            string js = "var dict_region_json=\"[{ 'id':'01','name':'浙江',parent_id:'000000'},{ 'id':'02','name':'辽宁',parent_id:'000000'},{ 'id':'0201','name':'沈阳',parent_id:'02'},{ 'id':'0202','name':'大连',parent_id:'02'}]\"";
            SaveJs("dict_region_json", js);
        }
        static string JsPath = ConfigurationManager.AppSettings["JsPath"];//js路径
        static void SaveJs(string name, string js)
        {
            FileStream fs = new FileStream(JsPath + name + ".js", FileMode.Open);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(js);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        #endregion
    }
}
