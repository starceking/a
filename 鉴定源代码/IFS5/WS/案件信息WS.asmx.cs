using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using DAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using DNADAL;
using System.Data.OracleClient;
using IFSOracleDAL;
using LIB;

namespace WS
{
    /// <summary>
    /// ok
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class 案件信息WS : System.Web.Services.WebService
    {
        /// <summary>
        /// 新的委托
        /// </summary>
        [WebMethod]
        public string Insert(string ID, string 委托编号, string 打防管控, string 现场勘验, string 案件名称, string 案件类型, string 案件类别, string 案件编号,
            string 发案地点, string 区划代码, string 发案时间, string 案件性质, string 简要案情, string SRCID,
            string 鉴定单位, string 委托表号, string 委托单位, string 委托单位简称, string 送检人一,
            string 一送姓名, string 一送警号, string 一送电话, string 二送姓名, string 二送警号, string 二送电话,
            string 委托年份, string 委托序号, string 委托时间, string 鉴定专业, string 鉴定类别, string 鉴定项目, string 鉴定要求, string 文书名称,
            string 原鉴定情况, string 案件序号)
        {
            委托编号 = Helper.GenerateConNo(委托编号);

            if (!ValidConNo(委托编号, 委托年份, 委托序号, 委托单位简称, 鉴定单位))
            {
                return "委托序号重复！请获取最新序号。";
            }

            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("委托编号", 委托编号);
                    dict.Add("鉴定单位", 鉴定单位);
                    if (委托编号.StartsWith("D"))
                    {
                        dict.Add("委托表号", 委托表号);
                    }
                    dict.Add("委托单位", 委托单位);
                    dict.Add("送检人一", 送检人一);
                    dict.Add("一送姓名", 一送姓名);
                    dict.Add("一送警号", 一送警号);
                    dict.Add("一送电话", 一送电话);
                    dict.Add("二送姓名", 二送姓名);
                    dict.Add("二送警号", 二送警号);
                    dict.Add("二送电话", 二送电话);
                    dict.Add("委托年份", 委托年份);
                    dict.Add("委托序号", 委托序号);
                    dict.Add("委托时间", 委托时间);
                    dict.Add("鉴定专业", 鉴定专业);
                    dict.Add("鉴定类别", 鉴定类别);
                    dict.Add("鉴定项目", 鉴定项目);
                    dict.Add("鉴定要求", 鉴定要求);
                    dict.Add("文书名称", 文书名称);
                    dict.Add("原鉴定情况", 原鉴定情况);
                    dict.Add("案件序号", 案件序号);
                    DBHelperSQL.Insert("鉴定流程", dict, dbConnection, trans);

                    dict = new Dictionary<string, string>();
                    dict.Add("ID", ID);
                    dict.Add("委托编号", 委托编号);
                    dict.Add("打防管控", 打防管控);
                    dict.Add("现场勘验", 现场勘验);
                    dict.Add("案件名称", 案件名称);
                    dict.Add("案件类型", 案件类型);
                    dict.Add("案件类别", 案件类别);
                    dict.Add("发案地点", 发案地点);
                    dict.Add("区划代码", 区划代码);
                    dict.Add("发案时间", 发案时间);
                    dict.Add("案件性质", 案件性质);
                    dict.Add("简要案情", 简要案情);
                    dict.Add("案件编号", 案件编号);
                    dict.Add("SRCID", SRCID);
                    DBHelperSQL.Insert("案件信息", dict, dbConnection, trans);

                    trans.Commit();
                }
            }
            return 委托编号;
        }
        [WebMethod]
        public string Update(string ID, string 委托编号, string 打防管控, string 现场勘验, string 案件名称, string 案件类型, string 案件类别, string 案件编号,
            string 发案地点, string 区划代码, string 发案时间, string 案件性质, string 简要案情,
            string 鉴定单位, string 委托表号, string 委托单位, string 委托单位简称, string 送检人一,
            string 一送姓名, string 一送警号, string 一送电话, string 二送姓名, string 二送警号, string 二送电话,
            string 委托年份, string 委托序号, string 委托时间, string 鉴定类别, string 鉴定项目, string 鉴定要求, string 文书名称,
            string 原鉴定情况, string ORA_FLAG)//2012.1.4
        {

            if (!ValidConNo(委托编号, 委托年份, 委托序号, 委托单位简称, 鉴定单位))
            {
                return "委托序号重复！请获取最新序号。";
            }

            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("打防管控", 打防管控);
                    dict.Add("现场勘验", 现场勘验);
                    dict.Add("案件名称", 案件名称);
                    dict.Add("案件类型", 案件类型);
                    dict.Add("案件类别", 案件类别);
                    dict.Add("案件编号", 案件编号);
                    dict.Add("发案地点", 发案地点);
                    dict.Add("区划代码", 区划代码);
                    dict.Add("发案时间", 发案时间);
                    dict.Add("案件性质", 案件性质);
                    dict.Add("简要案情", 简要案情);
                    DBHelperSQL.Update("案件信息", "ID='" + ID + "'", dict, dbConnection, trans);

                    dict = new Dictionary<string, string>();
                    dict.Add("委托单位", 委托单位);
                    dict.Add("一送姓名", 一送姓名);
                    dict.Add("一送警号", 一送警号);
                    dict.Add("一送电话", 一送电话);
                    dict.Add("二送姓名", 二送姓名);
                    dict.Add("二送警号", 二送警号);
                    dict.Add("二送电话", 二送电话);
                    dict.Add("委托年份", 委托年份);
                    dict.Add("委托序号", 委托序号);
                    dict.Add("委托时间", 委托时间);
                    dict.Add("鉴定类别", 鉴定类别);
                    dict.Add("鉴定项目", 鉴定项目);
                    dict.Add("鉴定要求", 鉴定要求);
                    dict.Add("文书名称", 文书名称);
                    dict.Add("原鉴定情况", 原鉴定情况);
                    DBHelperSQL.Update("鉴定流程", "委托编号='" + 委托编号 + "'", dict, dbConnection, trans);

                    if (ORA_FLAG.Equals("1"))
                    {
                        DNADICTWS dictWs = new DNADICTWS();
                        IDictionary<string, string> EVIDENCE_CASE_TYPE = dictWs.GetDnaDict("案件类型");
                        IDictionary<string, string> CASE_PROPERTY = dictWs.GetDnaDict("案件性质");

                        using (OracleConnection dbConnectionOra = new OracleConnection(DBHelperOracle.strConnectionString))
                        {
                            dbConnectionOra.Open();
                            using (OracleTransaction transOra = dbConnectionOra.BeginTransaction())
                            {
                                LAW_CASEOracleDAL.UpdateWithTransaction(ID,
                                案件编号, (案件编号 + "-" + 案件名称), Helper.CutRegionCode6(区划代码), 发案地点, 发案时间,
                                EVIDENCE_CASE_TYPE[案件类型], Helper.GetDNADictVal(CASE_PROPERTY, 案件性质), 鉴定要求,
                                简要案情, string.Empty, "admin", dbConnectionOra, transOra);
                                transOra.Commit();
                            }
                        }
                    }
                    trans.Commit();
                }
            }
            return "1";
        }
        [WebMethod]
        public string DeleteC(string ID, string 委托编号)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("案件人员", "委托编号，" + 委托编号);
            dict.Add("鉴定材料", "委托编号，" + 委托编号);
            dict.Add("案件信息", "ID，" + ID);
            dict.Add("鉴定流程", "委托编号，" + 委托编号);
            return DBHelperSQL.Delete(dict);
        }
        /// <summary>
        /// 如果有基于该案件的补送，则不能删除
        /// </summary>
        [WebMethod]
        public string DeleteD(string ID, string 委托编号, string CONSIGNID, string ORA_FLAG)//2012.1.4
        {
            int rc = DBHelperSQL.QueryRowCount("select count(id) from 案件信息 where SRCID='" + ID + "'");
            if (rc <= 1)
            {
                IDictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("涉案人员", "委托编号，" + 委托编号);
                dict.Add("无名尸体", "委托编号，" + 委托编号);
                dict.Add("样本信息", "委托编号，" + 委托编号);
                dict.Add("案件信息", "ID，" + ID);
                dict.Add("鉴定流程", "委托编号，" + 委托编号);

                using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
                {
                    dbConnection.Open();
                    using (SqlTransaction trans = dbConnection.BeginTransaction())
                    {
                        if (ORA_FLAG.Equals("1"))
                        {
                            DeleteDNAOra(ID, CONSIGNID, 委托编号, dbConnection, trans);
                        }

                        string sql = "delete from 亲属定义 where 委托编号='" + 委托编号 + "';";
                        DataSet ds = DBHelperSQL.Query("select id,亲属一ID,亲属二ID from 亲属定义 where 委托编号='" + 委托编号 + "';", dbConnection, trans);

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            sql += "delete from 亲属信息 where id='" + dr["亲属一ID"].ToString() + "';";
                            if (dr["亲属二ID"].ToString().Length > 0)
                            {
                                sql += "delete from 亲属信息 where id='" + dr["亲属二ID"].ToString() + "';";
                            }
                        }
                        DBHelperSQL.ExecuteSql(sql, dbConnection, trans);
                        DBHelperSQL.Delete(dict, dbConnection, trans);
                        trans.Commit();
                        return "1";
                    }
                }

            }
            else
            {
                return "存在基于该案件的补送，不能删除！";
            }
        }
        /// <summary>
        /// DNA案件，排除掉补送
        /// </summary>
        [WebMethod]
        public string GetAllD(string 案件名称, string 案件编号, string 鉴定单位, string pageSize, string pageIndex)
        {
            string filter = "鉴定单位='" + 鉴定单位 + "' and 委托编号 like 'D%' and ID=SRCID and 受理时间 is not null and ";
            if (案件名称.Length > 0) filter += "案件名称 like '%" + 案件名称 + "%' and ";
            if (案件编号.Length > 0) filter += "案件编号 like '%" + 案件编号 + "%' and ";

            return DBHelperSQL.SelectRowCount("案件视图", Helper.CutFilter(filter), "案件编号 DESC", "*",
                Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }

        #region 其他
        [WebMethod]
        public string ImportToOraCase(string SRCID, string userName, string IDS)
        {
            DNADICTWS dictWs = new DNADICTWS();
            IDictionary<string, string> EVIDENCE_TYPE = dictWs.GetDnaDict("样本类型");
            IDictionary<string, string> GENDER = dictWs.GetDnaDict("性别");
            IDictionary<string, string> PERSONNEL_TYPE = dictWs.GetDnaDict("人员类型");
            IDictionary<string, string> NATIONALITY = dictWs.GetDnaDict("民族");
            IDictionary<string, string> COUNTRY = dictWs.GetDnaDict("国籍");
            IDictionary<string, string> EDUCATION_LEVEL = dictWs.GetDnaDict("学历");
            IDictionary<string, string> IDENTITY = dictWs.GetDnaDict("身份");
            IDictionary<string, string> RELATION = dictWs.GetDnaDict("亲属关系");
            IDictionary<string, string> RELATION_WITH_TARGET = dictWs.GetDnaDict("目标关系");
            IDictionary<string, string> EVIDENCE_CASE_TYPE = dictWs.GetDnaDict("案件类型");
            IDictionary<string, string> CASE_PROPERTY = dictWs.GetDnaDict("案件性质");
            IDictionary<string, string> EVIDENCE_CARRIER_TYPE = dictWs.GetDnaDict("承载物");

            DataSet ajds = DBHelperSQL.Select("案件信息", "ID='" + SRCID + "'", string.Empty, "*");
            DataRow ajdr = ajds.Tables[0].Rows[0];
            DataSet viewds = DBHelperSQL.Select("案件视图", "ID='" + SRCID + "'", string.Empty, "*");
            DataRow viewdr = viewds.Tables[0].Rows[0];
            string oraFlag = ajdr["ORA_FLAG"].ToString();
            string userOraId = DBHelperOracle.GetUserOraId(viewdr["一检姓名"].ToString());

            if (DBHelperOracle.Query("select id from gdna.law_case where id='" + SRCID + "'").Tables[0].Rows.Count == 0)
            {
                using (OracleConnection dbConnectionOra = new OracleConnection(DBHelperOracle.strConnectionString))
                {
                    dbConnectionOra.Open();
                    using (OracleTransaction transOra = dbConnectionOra.BeginTransaction())
                    {
                        //辽宁省厅修改【委托单位电话】为【一送电话】
                        string AGENCY_PHONE = viewdr["一送电话"].ToString();//viewdr["委托单位电话"].ToString()

                        CONSIGNMENTOracleDAL.AddWithTransaction(viewdr["委托表号"].ToString(), Helper.CutRegionCode6(viewdr["委托单位编号"].ToString()), string.Empty,
                                  viewdr["委托单位名称"].ToString(), AGENCY_PHONE, viewdr["受理时间"].ToString(), viewdr["一送姓名"].ToString(),
                                  viewdr["一送警号"].ToString(), viewdr["委托单位地址"].ToString(), userName, dbConnectionOra, transOra);

                        //大连DNA修改【案件名称前加案件编号】
                        string CASE_NAME = (ajdr["案件编号"].ToString() + "-" + ajdr["案件名称"].ToString());
                        //辽宁省厅修改【dna案件编号】规则
                        string xk_NO = ajdr["现场勘验"].ToString().Trim();
                        string jz_NO = ajdr["打防管控"].ToString().Trim();
                        string CASE_NO = string.Empty;
                        if (xk_NO.Length == 23) CASE_NO = "A" + xk_NO.Substring(1, 22);
                        else if (jz_NO.Length == 23) CASE_NO = "A" + jz_NO.Substring(1, 22);
                        else CASE_NO = Helper.GetNextNoForDna(DBHelperOracle.INIT_SERVER_NO, "A");
                        //辽宁省厅修改【鉴定要求】规则
                        string IDENTIFIY_REQUEST = "警情编号：\r\n";
                        IDENTIFIY_REQUEST += "警综编号：" + jz_NO + "\r\n";
                        IDENTIFIY_REQUEST += "现勘编号：" + xk_NO + "\r\n";

                        LAW_CASEOracleDAL.AddWithTransaction(SRCID, viewdr["委托表号"].ToString(), CASE_NO,
                            ajdr["案件编号"].ToString(), CASE_NAME, Helper.CutRegionCode6(ajdr["区划代码"].ToString()), ajdr["发案地点"].ToString(), ajdr["发案时间"].ToString(),
                            EVIDENCE_CASE_TYPE[ajdr["案件类型"].ToString()], Helper.GetDNADictVal(CASE_PROPERTY, ajdr["案件性质"].ToString()), IDENTIFIY_REQUEST,
                            ajdr["简要案情"].ToString(), string.Empty, userOraId, viewdr["一检姓名"].ToString(), dbConnectionOra, transOra);
                        transOra.Commit();

                    }
                }
            }
            //样本信息
            DataSet ybds = DBHelperSQL.Select("样本信息", "案件ID='" + SRCID + "' ", string.Empty, "*");
            foreach (DataRow dr in ybds.Tables[0].Rows)
            {
                if (IDS.Contains(dr["ID"].ToString()))
                {
                    if (DBHelperOracle.Query("select id from gdna.scene_evidence where id='" + dr["ID"] + "'").Tables[0].Rows.Count == 0)
                    {
                        using (OracleConnection dbConnectionOra = new OracleConnection(DBHelperOracle.strConnectionString))
                        {
                            dbConnectionOra.Open();
                            using (OracleTransaction transOra = dbConnectionOra.BeginTransaction())
                            {
                                //大连DNA修改【物证样本名称前加案件编号】
                                SCENE_EVIDENCEOracleDAL.AddWithTransaction(dr["ID"].ToString(), SRCID, Helper.GetNextNoForDna(DBHelperOracle.INIT_SERVER_NO, "W"),
                                        (ajdr["案件编号"].ToString() + dr["名称"].ToString()), EVIDENCE_TYPE[dr["样本类型"].ToString()], dr["数量"].ToString(), EVIDENCE_CARRIER_TYPE[dr["承载物"].ToString()],
                                       dr["样本编号"].ToString(), dr["样本包装"].ToString(), viewdr["一检姓名"].ToString(), userOraId, dbConnectionOra, transOra);
                                transOra.Commit();

                            }
                        }
                    }
                }
            }
            //涉案人员
            ybds = DBHelperSQL.Select("涉案人员", "案件ID='" + SRCID + "' ", string.Empty, "*");
            foreach (DataRow dr in ybds.Tables[0].Rows)
            {
                if (IDS.Contains(dr["ID"].ToString()))
                {
                    if (DBHelperOracle.Query("select id from gdna.CASE_PERSONNEL_SAMPLE where id='" + dr["ID"] + "'").Tables[0].Rows.Count == 0)
                    {
                        using (OracleConnection dbConnectionOra = new OracleConnection(DBHelperOracle.strConnectionString))
                        {
                            dbConnectionOra.Open();
                            using (OracleTransaction transOra = dbConnectionOra.BeginTransaction())
                            {
                                CASE_PERSONNEL_SAMPLEOracleDAL.AddWithTransaction(dr["ID"].ToString(), SRCID, Helper.GetRelationWithCaseByScType(dr["库类型"].ToString()),
                                       dr["姓名"].ToString(),
                                EVIDENCE_TYPE[dr["样本类型"].ToString()], GENDER[dr["性别"].ToString()],
                                PERSONNEL_TYPE[dr["人员类型"].ToString()], dr["出生日期"].ToString(), NATIONALITY[dr["民族"].ToString()],
                                COUNTRY[dr["国籍"].ToString()], dr["身份证"].ToString(), EDUCATION_LEVEL[dr["学历"].ToString()],
                                IDENTITY[dr["身份"].ToString()], dr["籍贯"].ToString(), dr["现住址"].ToString(),
                                dr["包装情况"].ToString(), dr["样本描述"].ToString(), dr["备注"].ToString(),
                                dr["样本编号"].ToString(), Helper.GetNextNoForDna(DBHelperOracle.INIT_SERVER_NO, Helper.GetNoPreByScType(dr["库类型"].ToString())),
                                viewdr["一检姓名"].ToString(), userOraId, dbConnectionOra, transOra);
                                transOra.Commit();

                            }
                        }
                    }
                }
            }
            //无名尸体
            ybds = DBHelperSQL.Select("无名尸体", "案件ID='" + SRCID + "' ", string.Empty, "*");
            foreach (DataRow dr in ybds.Tables[0].Rows)
            {
                if (IDS.Contains(dr["ID"].ToString()))
                {
                    if (DBHelperOracle.Query("select id from gdna.UNKNOWN_DECEASED where id='" + dr["ID"] + "'").Tables[0].Rows.Count == 0)
                    {
                        using (OracleConnection dbConnectionOra = new OracleConnection(DBHelperOracle.strConnectionString))
                        {
                            dbConnectionOra.Open();
                            using (OracleTransaction transOra = dbConnectionOra.BeginTransaction())
                            {
                                UNKNOWN_DECEASEDOracleDAL.AddWithTransaction(dr["ID"].ToString(), SRCID, viewdr["委托表号"].ToString(), dr["姓名"].ToString(),
                                     EVIDENCE_TYPE[dr["样本类型"].ToString()], GENDER[dr["性别"].ToString()],
                                     dr["包装情况"].ToString(), dr["样本描述"].ToString(), dr["尸体特征"].ToString(), dr["大致年龄"].ToString(), dr["备注"].ToString(),
                                     dr["样本编号"].ToString(), Helper.GetNextNoForDna(DBHelperOracle.INIT_SERVER_NO, "U"), viewdr["一检姓名"].ToString(), userOraId,
                                     dbConnectionOra, transOra);
                                transOra.Commit();

                            }
                        }
                    }
                }
            }
            //亲属定义
            ybds = DBHelperSQL.Select("案件亲属视图", "案件ID='" + SRCID + "'", string.Empty, "*");
            foreach (DataRow dr in ybds.Tables[0].Rows)
            {
                if (IDS.Contains(dr["亲属一ID"].ToString()) || IDS.Contains(dr["亲属二ID"].ToString()))
                {
                    if (DBHelperOracle.Query("select id from gdna.relation_definition where id='" + dr["ID"] + "'").Tables[0].Rows.Count == 0)
                    {
                        using (OracleConnection dbConnectionOra = new OracleConnection(DBHelperOracle.strConnectionString))
                        {
                            dbConnectionOra.Open();
                            using (OracleTransaction transOra = dbConnectionOra.BeginTransaction())
                            {
                                RELATION_DEFINITIONOracleDAL.AddWithTransaction(dr["ID"].ToString(), RELATION[dr["亲属关系"].ToString()], dr["亲属一ID"].ToString(),
            dr["亲属二ID"].ToString(), string.Empty, string.Empty, string.Empty, string.Empty,
            dr["姓名"].ToString(), viewdr["一检姓名"].ToString(), dbConnectionOra, transOra);
                                RELATIVEOracleDAL.AddWithTransaction(dr["亲属一ID"].ToString(), SRCID, viewdr["委托表号"].ToString(),
                                    Helper.GetRelativeTypeByScType(dr["库类型"].ToString()), dr["ID"].ToString(),
                                    RELATION_WITH_TARGET[dr["亲属一目标关系"].ToString()], dr["亲属一姓名"].ToString(),
                                    EVIDENCE_TYPE[dr["亲属一样本类型"].ToString()], GENDER[dr["亲属一性别"].ToString()],
                                    string.Empty, string.Empty, string.Empty,
                                    string.Empty, dr["亲属一身份证"].ToString(), string.Empty,
                                    string.Empty, dr["亲属一籍贯"].ToString(), string.Empty,
                                    string.Empty, dr["亲属一样本描述"].ToString(), string.Empty,
                                    dr["亲属一样本编号"].ToString(), Helper.GetNextNoForDna(DBHelperOracle.INIT_SERVER_NO, "P"), viewdr["一检姓名"].ToString(),
                                    userOraId, dbConnectionOra, transOra);
                                if (!dr["亲属关系"].ToString().Equals("单亲"))
                                {
                                    RELATIVEOracleDAL.AddWithTransaction(dr["亲属二ID"].ToString(), SRCID, viewdr["委托表号"].ToString(),
                                     Helper.GetRelativeTypeByScType(dr["库类型"].ToString()), string.Empty,
                                     RELATION_WITH_TARGET[dr["亲属二目标关系"].ToString()], dr["亲属二姓名"].ToString(),
                                     EVIDENCE_TYPE[dr["亲属二样本类型"].ToString()], GENDER[dr["亲属二性别"].ToString()],
                                     string.Empty, string.Empty, string.Empty,
                                     string.Empty, dr["亲属二身份证"].ToString(), string.Empty,
                                     string.Empty, dr["亲属二籍贯"].ToString(), string.Empty,
                                     string.Empty, dr["亲属二样本描述"].ToString(), string.Empty,
                                     dr["亲属二样本编号"].ToString(), Helper.GetNextNoForDna(DBHelperOracle.INIT_SERVER_NO, "P"), viewdr["一检姓名"].ToString(),
                                     userOraId, dbConnectionOra, transOra);
                                }
                                transOra.Commit();

                            }
                        }
                    }
                }
            }
            return "1";
        }
        [WebMethod]
        public string GetAcceptDup(string 委托编号, string pageSize, string pageIndex)
        {
            DataSet ds = DBHelperSQL.Select("案件人员", "委托编号='" + 委托编号 + "'", "创建时间 desc", "姓名,身份证");
            if (ds.Tables[0].Rows.Count == 0) return string.Empty;
            string filter = string.Empty;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                filter += "姓名='" + dr["姓名"] + "' or 身份证='" + dr["身份证"] + "' or ";
            }
            filter = "委托编号<>'" + 委托编号 + "' and (" + filter.Substring(0, filter.Length - 4) + ")";

            ds = DBHelperSQL.Select("案件人员", filter, "创建时间 desc", "委托编号");
            if (ds.Tables[0].Rows.Count == 0) return string.Empty;
            filter = string.Empty;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                filter += "委托编号='" + dr["委托编号"] + "' or ";
            }
            return DBHelperSQL.SelectRowCount("案件视图", "鉴定状态<>'新的委托' and 鉴定状态<>'不予受理' and (" + filter.Substring(0, filter.Length - 4) + ")", "受理年份 desc,受理序号 desc", "*",
                Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string UpdateBsInfo(string ID, string 委托编号, string SRCID, string 案件编号, string 案件序号)
        {
            string sql = "update 案件信息 set SRCID='" + SRCID + "',案件编号='" + 案件编号 + "' where ID='" + ID + "';";
            sql += "update 亲属定义 set 案件ID='" + SRCID + "' where 委托编号='" + 委托编号 + "';";
            sql += "update 涉案人员 set 案件ID='" + SRCID + "' where 委托编号='" + 委托编号 + "';";
            sql += "update 无名尸体 set 案件ID='" + SRCID + "' where 委托编号='" + 委托编号 + "';";
            sql += "update 样本信息 set 案件ID='" + SRCID + "' where 委托编号='" + 委托编号 + "';";
            sql += "update 鉴定流程 set 案件序号=" + (SRCID.Equals(ID) ? "NULL" : ("'" + 案件序号 + "'")) + " where 委托编号='" + 委托编号 + "';";
            return DBHelperSQL.ExecuteSql(sql);
        }
        [WebMethod]
        public string GetOneRecord(string ID)
        {
            return DBHelperSQL.Select("案件视图", "ID='" + ID + "'", "ID", "*").GetXml();
        }
        #endregion
        #region 统计信息
        [WebMethod]
        public string GetYearStaData(string 鉴定单位, string 受理年份, string 鉴定专业)
        {
            IDictionary<string, string> jdzy = new Dictionary<string, string>();
            IDictionary<string, int> jan = new Dictionary<string, int>();
            IDictionary<string, int> feb = new Dictionary<string, int>();
            IDictionary<string, int> mar = new Dictionary<string, int>();
            IDictionary<string, int> apr = new Dictionary<string, int>();
            IDictionary<string, int> may = new Dictionary<string, int>();
            IDictionary<string, int> jun = new Dictionary<string, int>();
            IDictionary<string, int> jul = new Dictionary<string, int>();
            IDictionary<string, int> aug = new Dictionary<string, int>();
            IDictionary<string, int> sep = new Dictionary<string, int>();
            IDictionary<string, int> oct = new Dictionary<string, int>();
            IDictionary<string, int> nov = new Dictionary<string, int>();
            IDictionary<string, int> dec = new Dictionary<string, int>();

            DataSet ds = DBHelperSQL.Query("select 鉴定专业,鉴定类别,受理时间 from 鉴定流程 where 鉴定状态<>'新的委托' and 鉴定状态<>'不予受理' and 鉴定单位='" + 鉴定单位 + "'and 鉴定专业='" + 鉴定专业 + "'");
            DataSet dataSet = new DataSet("NewDataSet");
            DataTable dt = new DataTable("tableName"); dataSet.Tables.Add(dt);
            DataColumn dc = new DataColumn("jdzy", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("jdlb", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("jan", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("feb", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("mar", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("apr", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("may", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("jun", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("jul", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("aug", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("sep", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("oct", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("nov", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("dec", typeof(string)); dt.Columns.Add(dc);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (!jdzy.ContainsKey(dr["鉴定类别"].ToString()))
                {
                    jdzy.Add(dr["鉴定类别"].ToString(), dr["鉴定专业"].ToString());
                    jan.Add(dr["鉴定类别"].ToString(), 0); feb.Add(dr["鉴定类别"].ToString(), 0); mar.Add(dr["鉴定类别"].ToString(), 0);
                    apr.Add(dr["鉴定类别"].ToString(), 0); may.Add(dr["鉴定类别"].ToString(), 0); jun.Add(dr["鉴定类别"].ToString(), 0);
                    jul.Add(dr["鉴定类别"].ToString(), 0); aug.Add(dr["鉴定类别"].ToString(), 0); sep.Add(dr["鉴定类别"].ToString(), 0);
                    oct.Add(dr["鉴定类别"].ToString(), 0); nov.Add(dr["鉴定类别"].ToString(), 0); dec.Add(dr["鉴定类别"].ToString(), 0);
                }
            }

            DataRow[] drs = ds.Tables[0].Select("受理时间>='" + (Convert.ToInt32(受理年份) - 1).ToString() + "-12-21' and 受理时间<='" + 受理年份 + "-1-20'");
            foreach (DataRow dr in drs)
            {
                jan[dr["鉴定类别"].ToString()]++;
            }
            drs = ds.Tables[0].Select("受理时间>='" + 受理年份 + "-1-21' and 受理时间<='" + 受理年份 + "-2-20'");
            foreach (DataRow dr in drs)
            {
                feb[dr["鉴定类别"].ToString()]++;
            }
            drs = ds.Tables[0].Select("受理时间>='" + 受理年份 + "-2-21' and 受理时间<='" + 受理年份 + "-3-20'");
            foreach (DataRow dr in drs)
            {
                mar[dr["鉴定类别"].ToString()]++;
            }
            drs = ds.Tables[0].Select("受理时间>='" + 受理年份 + "-3-21' and 受理时间<='" + 受理年份 + "-4-20'");
            foreach (DataRow dr in drs)
            {
                apr[dr["鉴定类别"].ToString()]++;
            }
            drs = ds.Tables[0].Select("受理时间>='" + 受理年份 + "-4-21' and 受理时间<='" + 受理年份 + "-5-20'");
            foreach (DataRow dr in drs)
            {
                may[dr["鉴定类别"].ToString()]++;
            }
            drs = ds.Tables[0].Select("受理时间>='" + 受理年份 + "-5-21' and 受理时间<='" + 受理年份 + "-6-20'");
            foreach (DataRow dr in drs)
            {
                jun[dr["鉴定类别"].ToString()]++;
            }
            drs = ds.Tables[0].Select("受理时间>='" + 受理年份 + "-6-21' and 受理时间<='" + 受理年份 + "-7-20'");
            foreach (DataRow dr in drs)
            {
                jul[dr["鉴定类别"].ToString()]++;
            }
            drs = ds.Tables[0].Select("受理时间>='" + 受理年份 + "-7-21' and 受理时间<='" + 受理年份 + "-8-20'");
            foreach (DataRow dr in drs)
            {
                aug[dr["鉴定类别"].ToString()]++;
            }
            drs = ds.Tables[0].Select("受理时间>='" + 受理年份 + "-8-21' and 受理时间<='" + 受理年份 + "-9-20'");
            foreach (DataRow dr in drs)
            {
                sep[dr["鉴定类别"].ToString()]++;
            }
            drs = ds.Tables[0].Select("受理时间>='" + 受理年份 + "-9-21' and 受理时间<='" + 受理年份 + "-10-20'");
            foreach (DataRow dr in drs)
            {
                oct[dr["鉴定类别"].ToString()]++;
            }
            drs = ds.Tables[0].Select("受理时间>='" + 受理年份 + "-10-21' and 受理时间<='" + 受理年份 + "-11-20'");
            foreach (DataRow dr in drs)
            {
                nov[dr["鉴定类别"].ToString()]++;
            }
            drs = ds.Tables[0].Select("受理时间>='" + 受理年份 + "-11-21' and 受理时间<='" + 受理年份 + "-12-20'");
            foreach (DataRow dr in drs)
            {
                dec[dr["鉴定类别"].ToString()]++;
            }

            foreach (string key in jdzy.Keys)
            {
                DataRow dataRow = dt.NewRow();
                dataRow["jdzy"] = jdzy[key];
                dataRow["jdlb"] = key;
                dataRow["jan"] = jan[key]; dataRow["feb"] = feb[key]; dataRow["mar"] = mar[key]; dataRow["apr"] = apr[key];
                dataRow["may"] = may[key]; dataRow["jun"] = jun[key]; dataRow["jul"] = jul[key]; dataRow["aug"] = aug[key];
                dataRow["sep"] = sep[key]; dataRow["oct"] = oct[key]; dataRow["nov"] = nov[key]; dataRow["dec"] = dec[key];
                dt.Rows.Add(dataRow);
            }
            return dataSet.GetXml();
        }
        [WebMethod]
        public string GetAllJds(string 鉴定单位, string StarTime, string EndTime, string 鉴定专业)
        {
            IDictionary<string, string> jdzy = new Dictionary<string, string>();
            IDictionary<string, int> jds = new Dictionary<string, int>();
            string sql = "select 鉴定专业,鉴定类别 from 鉴定流程 where 鉴定状态<>'新的委托' and 鉴定状态<>'不予受理' and 鉴定单位='" + 鉴定单位 + "' and 鉴定专业='" + 鉴定专业 + "'";
            if (StarTime.Length > 0) sql += " and 受理时间>='" + StarTime + "'";
            if (EndTime.Length > 0) sql += " and 受理时间<'" + EndTime + "'";
            DataSet ds = DBHelperSQL.Query(sql);
            DataSet dataSet = new DataSet("NewDataSet");
            DataTable dt = new DataTable("tableName"); dataSet.Tables.Add(dt);
            DataColumn dc = new DataColumn("jdzy", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("jdlb", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("jds", typeof(string)); dt.Columns.Add(dc);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (!jdzy.ContainsKey(dr["鉴定类别"].ToString()))
                {
                    jdzy.Add(dr["鉴定类别"].ToString(), dr["鉴定专业"].ToString());
                    jds.Add(dr["鉴定类别"].ToString(), 0);
                }
            }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                jds[dr["鉴定类别"].ToString()]++;
            }
            //专业累计
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (!jdzy.ContainsKey(dr["鉴定专业"].ToString() + "总计"))
                {
                    jdzy.Add(dr["鉴定专业"].ToString() + "总计", dr["鉴定专业"].ToString() + "总计");
                    jds.Add(dr["鉴定专业"].ToString() + "总计", 0);
                }
            }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                jds[dr["鉴定专业"].ToString() + "总计"]++;
            }
            jdzy.Add("总计", "总计");
            jds.Add("总计", 0);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                jds["总计"]++;
            }
            foreach (string key in jdzy.Keys)
            {
                DataRow dataRow = dt.NewRow();
                dataRow["jdzy"] = jdzy[key];
                dataRow["jdlb"] = key;
                dataRow["jds"] = jds[key];
                dt.Rows.Add(dataRow);
            }
            return dataSet.GetXml();
        }
        #endregion
        public static void DeleteDNAOra(string ID, string CONSIGNID, string 委托编号, SqlConnection dbConnection, SqlTransaction trans)
        {
            string sql = "delete from gdna.law_case where id='" + ID + "';";
            sql += "delete from gdna.consignment where id='" + CONSIGNID + "';";
            DataSet ds = DBHelperSQL.Query("select id from 涉案人员 where 委托编号='" + 委托编号 + "';", dbConnection, trans);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sql += "delete from gdna.case_personnel_sample where id='" + dr["id"].ToString() + "';";
                sql += "delete from gdna.SAMPLE_EXAMINATION where SAMPLE_ID='" + dr["id"].ToString() + "';";
            }
            ds = DBHelperSQL.Query("select id from 样本信息 where 委托编号='" + 委托编号 + "';", dbConnection, trans);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sql += "delete from gdna.scene_evidence where id='" + dr["id"].ToString() + "';";
                sql += "delete from gdna.SAMPLE_EXAMINATION where SAMPLE_ID='" + dr["id"].ToString() + "';";
            }
            ds = DBHelperSQL.Query("select id from 无名尸体 where 委托编号='" + 委托编号 + "';", dbConnection, trans);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sql += "delete from gdna.unknown_deceased where id='" + dr["id"].ToString() + "';";
                sql += "delete from gdna.SAMPLE_EXAMINATION where SAMPLE_ID='" + dr["id"].ToString() + "';";
            }
            ds = DBHelperSQL.Query("select id,亲属一ID,亲属二ID from 亲属定义 where 委托编号='" + 委托编号 + "';", dbConnection, trans);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sql += "delete from gdna.RELATION_DEFINITION where id='" + dr["id"].ToString() + "';";
                sql += "delete from gdna.RELATIVE where id='" + dr["亲属一ID"].ToString() + "';";
                sql += "delete from gdna.SAMPLE_EXAMINATION where SAMPLE_ID='" + dr["亲属一ID"].ToString() + "';";
                if (dr["亲属二ID"].ToString().Length > 0)
                {
                    sql += "delete from gdna.RELATIVE where id='" + dr["亲属二ID"].ToString() + "';";
                    sql += "delete from gdna.SAMPLE_EXAMINATION where SAMPLE_ID='" + dr["亲属二ID"].ToString() + "';";
                }
            }
            DBHelperOracle.ExecuteSql(sql);
        }
        [WebMethod]
        public string GetMain(string LoginUser, string 鉴定专业, string psbid)
        {
            // psbid = "1";
            //string strWhere = " (鉴定状态='新的委托' or 鉴定状态='审核中' or 鉴定状态='不予受理' or 鉴定状态='检验中' or 鉴定状态='待领取' or 鉴定状态='已归档' )";
            string strWhere = "1=1";
            DataSet ds = DBHelperSQL.Select("案件视图", strWhere, " 委托编号 ", " * ");
            int dlq = 0;
            int dsl = 0;
            int djy = 0;
            int dshh = 0;
            int dfh = 0;
            int dshp = 0;
            int dfw = 0;
            //int ygd = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["鉴定状态"].ToString().Equals("待领取") && dr["委托单位"].ToString().Equals(psbid) && (dr["送检人一"].ToString().Equals(LoginUser) || dr["二送姓名"].ToString().Equals(LoginUser))) dlq++;
                //if (dr["鉴定单位"].ToString().Equals(psbid) && (dr["鉴定状态"].ToString().Equals("新的委托") || dr["鉴定状态"].ToString().Equals("不予受理") || dr["鉴定状态"].ToString().Equals("终止鉴定"))) dsl++;
                //if (dr["鉴定单位"].ToString().Equals(psbid) && dr["一检姓名"].ToString().Equals(LoginUser) && dr["鉴定状态"].ToString().Equals("检验中")) djy++;
                //if (dr["鉴定单位"].ToString().Equals(psbid) && dr["鉴定状态"].ToString().Equals("审核中") && (dr["二检姓名"].ToString().Equals(LoginUser) || dr["三检姓名"].ToString().Equals(LoginUser) || dr["四检姓名"].ToString().Equals(LoginUser))) dshh++;
                //if (dr["鉴定单位"].ToString().Equals(psbid) && dr["鉴定状态"].ToString().Equals("审核中") && (dr["复核姓名"].ToString().Equals(LoginUser) || dr["签字姓名"].ToString().Equals(LoginUser) || dr["技管姓名"].ToString().Equals(LoginUser) || dr["领导姓名"].ToString().Equals(LoginUser))) dfh++;
                //if (dr["鉴定单位"].ToString().Equals(psbid) && dr["鉴定状态"].ToString().Equals("审核中") && dr["领导姓名"].ToString().Equals(LoginUser)) dshp++;
                //if (dr["鉴定单位"].ToString().Equals(psbid) && dr["一检姓名"].ToString().Equals(LoginUser) && dr["审批完成"].ToString().Length > 0 && dr["鉴定状态"].ToString().Equals("审核中")) dfw++;
                //if (dr["鉴定单位"].ToString().Equals(psbid) && dr["一检姓名"].ToString().Equals(LoginUser) && dr["鉴定状态"].ToString().Equals("已归档")) ygd++;


                if ((dr["鉴定状态"].ToString().Equals("新的委托") || dr["鉴定状态"].ToString().Equals("不予受理") || dr["鉴定状态"].ToString().Equals("终止鉴定")) && (dr["鉴定专业"].ToString().Equals(鉴定专业))) dsl++;
                if (dr["一检姓名"].ToString().Equals(LoginUser) && dr["鉴定状态"].ToString().Equals("检验中")) djy++;
                if (dr["鉴定状态"].ToString().Equals("审核中") && (dr["二检姓名"].ToString().Equals(LoginUser) || dr["三检姓名"].ToString().Equals(LoginUser) || dr["四检姓名"].ToString().Equals(LoginUser))) dshh++;
                if (dr["鉴定状态"].ToString().Equals("审核中") && (dr["复核姓名"].ToString().Equals(LoginUser) || dr["签字姓名"].ToString().Equals(LoginUser) || dr["技管姓名"].ToString().Equals(LoginUser) || dr["领导姓名"].ToString().Equals(LoginUser))) dfh++;
                if (dr["鉴定状态"].ToString().Equals("审核中") && dr["领导姓名"].ToString().Equals(LoginUser)) dshp++;
                if (dr["一检姓名"].ToString().Equals(LoginUser) && dr["审批完成"].ToString().Length > 0 && dr["鉴定状态"].ToString().Equals("审核中")) dfw++;
                // if (dr["一检姓名"].ToString().Equals(LoginUser) && dr["鉴定状态"].ToString().Equals("已归档")) ygd++;
            }
            string ret = dlq.ToString() + "," +
      dsl.ToString() + "," +
      djy.ToString() + "," + dshh.ToString() + "," + dfh.ToString() + "," + dshp.ToString() + "," + dfw.ToString();
            //ygd.ToString() + ",";
            return ret;
        }

        public static bool ValidConNo(string 委托编号, string 委托年份, string 委托序号, string 委托单位简称, string 鉴定单位)
        {
            return DBHelperSQL.SelectRowCount("鉴定流程,单位信息", "简称='" + 委托单位简称 + "' and 委托编号<>'" + 委托编号 + "' and 委托年份='" + 委托年份 +
                "' and 鉴定单位='" + 鉴定单位 + "' and 委托序号='" + 委托序号 + "' and 单位信息.ID=鉴定流程.委托单位", "委托编号") == 0;
        }

    }
}
