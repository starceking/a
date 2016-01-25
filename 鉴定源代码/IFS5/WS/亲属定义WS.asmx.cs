using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using DAL;
using System.Collections.Generic;
using LIB;
using System.Data.SqlClient;
using DNADAL;
using System.Data.OracleClient;
using IFSOracleDAL;

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
    public class 亲属定义WS : System.Web.Services.WebService
    {
        #region 失踪人员亲属
        [WebMethod]
        public string NewConsign(string ID, string 委托编号, string 亲属关系, string 亲属一ID, string 亲属二ID, string 出生日期,
            string 性别, string 特殊特征, string 体表标记, string 姓名, string 案件名称, string 简要案情,
            string 鉴定单位, string 委托表号, string 委托单位, string 送检人一,
            string 一送姓名, string 一送警号, string 一送电话, string 二送姓名, string 二送警号, string 二送电话,
            string 委托年份, string 委托序号, string 委托时间, string 鉴定要求, string 文书名称,
            string 姓名1, string 样本类型1, string 性别1, string 人员类型1, string 出生日期1, string 民族1, string 国籍1, string 身份证1, string 学历1, string 身份1,
            string 籍贯1, string 现住址1, string 包装情况1, string 样本描述1, string 目标关系1, string 备注1,
            string 姓名2, string 样本类型2, string 性别2, string 人员类型2, string 出生日期2, string 民族2, string 国籍2, string 身份证2, string 学历2, string 身份2,
            string 籍贯2, string 现住址2, string 包装情况2, string 样本描述2, string 目标关系2, string 备注2)
        {
            委托编号 = Helper.GenerateConNo(委托编号);

            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("委托编号", 委托编号);
                    dict.Add("鉴定单位", 鉴定单位);
                    dict.Add("委托表号", Helper.GenerateID());
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
                    dict.Add("鉴定专业", "DNA");
                    dict.Add("鉴定类别", "失踪人亲属");
                    dict.Add("鉴定要求", 鉴定要求);
                    dict.Add("文书名称", 文书名称);
                    DBHelperSQL.Insert("鉴定流程", dict, dbConnection, trans);

                    dict = new Dictionary<string, string>();
                    dict.Add("ID", 亲属一ID);
                    dict.Add("姓名", 姓名1);
                    dict.Add("样本类型", 样本类型1);
                    dict.Add("性别", 性别1);
                    dict.Add("人员类型", 人员类型1);
                    dict.Add("出生日期", 出生日期1);
                    dict.Add("民族", 民族1);
                    dict.Add("国籍", 国籍1);
                    dict.Add("身份证", 身份证1);
                    dict.Add("学历", 学历1);
                    dict.Add("身份", 身份1);
                    dict.Add("籍贯", 籍贯1);
                    dict.Add("现住址", 现住址1);
                    dict.Add("包装情况", 包装情况1);
                    dict.Add("样本描述", 样本描述1);
                    dict.Add("目标关系", 目标关系1);
                    dict.Add("备注", 备注1);
                    DBHelperSQL.Insert("亲属信息", dict, dbConnection, trans);

                    if (!亲属关系.Equals("单亲"))
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("ID", 亲属二ID);
                        dict.Add("姓名", 姓名2);
                        dict.Add("样本类型", 样本类型2);
                        dict.Add("性别", 性别2);
                        dict.Add("人员类型", 人员类型2);
                        dict.Add("出生日期", 出生日期2);
                        dict.Add("民族", 民族2);
                        dict.Add("国籍", 国籍2);
                        dict.Add("身份证", 身份证2);
                        dict.Add("学历", 学历2);
                        dict.Add("身份", 身份2);
                        dict.Add("籍贯", 籍贯2);
                        dict.Add("现住址", 现住址2);
                        dict.Add("包装情况", 包装情况2);
                        dict.Add("样本描述", 样本描述2);
                        dict.Add("目标关系", 目标关系2);
                        dict.Add("备注", 备注2);
                        dict.Add("创建时间", DateTime.Now.AddSeconds(1).ToString());
                        DBHelperSQL.Insert("亲属信息", dict, dbConnection, trans);
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("ID", ID);
                    dict.Add("委托编号", 委托编号);
                    dict.Add("库类型", "失踪人亲属");
                    dict.Add("亲属关系", 亲属关系);
                    dict.Add("亲属一ID", 亲属一ID);
                    if (亲属关系.Equals("单亲"))
                    {
                        dict.Add("亲属二ID", string.Empty);
                    }
                    else
                    {
                        dict.Add("亲属二ID", 亲属二ID);
                    }
                    dict.Add("出生日期", 出生日期);
                    dict.Add("性别", 性别);
                    dict.Add("特殊特征", 特殊特征);
                    dict.Add("体表标记", 体表标记);
                    dict.Add("姓名", 姓名);
                    dict.Add("案件名称", 案件名称);
                    dict.Add("简要案情", 简要案情);
                    DBHelperSQL.Insert("亲属定义", dict, dbConnection, trans);

                    trans.Commit();
                }
            }
            return 委托编号;
        }
        [WebMethod]
        public string UpdateR(string ID, string 委托编号, string 亲属关系, string 亲属一ID, string 亲属二ID, string 出生日期,
            string 性别, string 特殊特征, string 体表标记, string 姓名, string 案件名称, string 简要案情,
           string 一送姓名, string 一送警号, string 一送电话, string 二送姓名, string 二送警号, string 二送电话,
            string 委托年份, string 委托序号, string 委托时间, string 鉴定要求,
            string 姓名1, string 样本类型1, string 性别1, string 人员类型1, string 出生日期1, string 民族1, string 国籍1, string 身份证1, string 学历1, string 身份1,
            string 籍贯1, string 现住址1, string 包装情况1, string 样本描述1, string 目标关系1, string 备注1, string 样本编号1,
            string 姓名2, string 样本类型2, string 性别2, string 人员类型2, string 出生日期2, string 民族2, string 国籍2, string 身份证2, string 学历2, string 身份2,
            string 籍贯2, string 现住址2, string 包装情况2, string 样本描述2, string 目标关系2, string 备注2, string 样本编号2, string ORA_FLAG)//2012.1.4
        {
            if (!DataReader.ValidSlnDup2(样本编号1, 亲属一ID, 样本编号2, 亲属二ID))
            {
                return "样本编号重复！请检查。";
            }

            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("亲属关系", 亲属关系);
                    dict.Add("出生日期", 出生日期);
                    dict.Add("性别", 性别);
                    dict.Add("特殊特征", 特殊特征);
                    dict.Add("体表标记", 体表标记);
                    dict.Add("姓名", 姓名);
                    dict.Add("案件名称", 案件名称);
                    dict.Add("简要案情", 简要案情);
                    if (亲属关系.Equals("单亲"))
                    {
                        dict.Add("亲属二ID", string.Empty);
                    }
                    else
                    {
                        dict.Add("亲属二ID", 亲属二ID);
                    }
                    DBHelperSQL.Update("亲属定义", "ID='" + ID + "'", dict, dbConnection, trans);

                    dict = new Dictionary<string, string>();
                    dict.Add("一送姓名", 一送姓名);
                    dict.Add("一送警号", 一送警号);
                    dict.Add("一送电话", 一送电话);
                    dict.Add("二送姓名", 二送姓名);
                    dict.Add("二送警号", 二送警号);
                    dict.Add("二送电话", 二送电话);
                    dict.Add("委托年份", 委托年份);
                    dict.Add("委托序号", 委托序号);
                    dict.Add("委托时间", 委托时间);
                    dict.Add("鉴定要求", 鉴定要求);
                    DBHelperSQL.Update("鉴定流程", "委托编号='" + 委托编号 + "'", dict, dbConnection, trans);

                    dict = new Dictionary<string, string>();
                    dict.Add("姓名", 姓名1);
                    dict.Add("样本类型", 样本类型1);
                    dict.Add("性别", 性别1);
                    dict.Add("人员类型", 人员类型1);
                    dict.Add("出生日期", 出生日期1);
                    dict.Add("民族", 民族1);
                    dict.Add("国籍", 国籍1);
                    dict.Add("身份证", 身份证1);
                    dict.Add("学历", 学历1);
                    dict.Add("身份", 身份1);
                    dict.Add("籍贯", 籍贯1);
                    dict.Add("现住址", 现住址1);
                    dict.Add("包装情况", 包装情况1);
                    dict.Add("样本描述", 样本描述1);
                    dict.Add("目标关系", 目标关系1);
                    dict.Add("备注", 备注1);
                    dict.Add("样本编号", 样本编号1);
                    DBHelperSQL.Update("亲属信息", "ID='" + 亲属一ID + "'", dict, dbConnection, trans);

                    if (亲属关系.Equals("单亲") && 亲属二ID.Length > 0)//从非单亲变成单亲
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("亲属信息", "ID，" + 亲属二ID);
                        DBHelperSQL.Delete(dict, dbConnection, trans); ;
                    }
                    else if (亲属二ID.Length > 0)//本来就是非单亲
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("姓名", 姓名2); ;
                        dict.Add("样本类型", 样本类型2);
                        dict.Add("性别", 性别2);
                        dict.Add("人员类型", 人员类型2);
                        dict.Add("出生日期", 出生日期2);
                        dict.Add("民族", 民族2);
                        dict.Add("国籍", 国籍2);
                        dict.Add("身份证", 身份证2);
                        dict.Add("学历", 学历2);
                        dict.Add("身份", 身份2);
                        dict.Add("籍贯", 籍贯2);
                        dict.Add("现住址", 现住址2);
                        dict.Add("包装情况", 包装情况2);
                        dict.Add("样本描述", 样本描述2);
                        dict.Add("目标关系", 目标关系2);
                        dict.Add("备注", 备注2);
                        dict.Add("样本编号", 样本编号2);
                        DBHelperSQL.Update("亲属信息", "ID='" + 亲属二ID + "'", dict, dbConnection, trans);
                    }
                    else if (!亲属关系.Equals("单亲"))//从单亲变成非单亲
                    {
                        trans.Rollback();
                        return "不允许从单亲改为其他亲属关系，请删除该数据后重新录入。";
                    }
                    if (ORA_FLAG.Equals("1"))
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

                        using (OracleConnection dbConnectionOra = new OracleConnection(DBHelperOracle.strConnectionString))
                        {
                            dbConnectionOra.Open();
                            using (OracleTransaction transOra = dbConnectionOra.BeginTransaction())
                            {
                                RELATION_DEFINITIONOracleDAL.UpdateWithTransaction(ID, 出生日期, GENDER[性别], 特殊特征, 体表标记, 姓名, "admin", dbConnectionOra, transOra);
                                RELATIVEOracleDAL.UpdateWithTransaction(亲属一ID,
                                     RELATION_WITH_TARGET[目标关系1], 姓名1,
                                    EVIDENCE_TYPE[样本类型1], GENDER[性别1],
                                    PERSONNEL_TYPE[人员类型1], 出生日期1, NATIONALITY[民族1],
                                    COUNTRY[国籍1], 身份证1, EDUCATION_LEVEL[学历1],
                                    IDENTITY[身份1], 籍贯1, 现住址1,
                                    包装情况1, 样本描述1, 备注1,
                                    样本编号1, "admin", dbConnectionOra, transOra);
                                if (!亲属关系.Equals("单亲"))
                                {
                                    RELATIVEOracleDAL.UpdateWithTransaction(亲属二ID,
                                        RELATION_WITH_TARGET[目标关系2], 姓名2,
                                       EVIDENCE_TYPE[样本类型2], GENDER[性别2],
                                       PERSONNEL_TYPE[人员类型2], 出生日期2, NATIONALITY[民族2],
                                       COUNTRY[国籍2], 身份证2, EDUCATION_LEVEL[学历2],
                                       IDENTITY[身份2], 籍贯2, 现住址2,
                                       包装情况2, 样本描述2, 备注2,
                                       样本编号2, "admin", dbConnectionOra, transOra);
                                }
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
        public string DeleteR(string ID, string CONSIGNID, string 委托编号, string 亲属一ID, string 亲属二ID, string ORA_FLAG)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("亲属定义", "ID，" + ID);
            dict.Add("亲属信息", "ID，" + 亲属一ID);
            if (亲属二ID.Length > 0)
            {
                dict["亲属信息"] = "ID，" + 亲属一ID + "￥" + 亲属二ID;
            }
            dict.Add("鉴定流程", "委托编号，" + 委托编号);

            if (ORA_FLAG.Equals("1"))
            {
                using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
                {
                    dbConnection.Open();
                    using (SqlTransaction trans = dbConnection.BeginTransaction())
                    {
                        DBHelperSQL.Delete(dict, dbConnection, trans);
                        DeleteRelativeOra(ID, CONSIGNID, 亲属一ID, 亲属二ID);
                        trans.Commit();
                        return "1";
                    }
                }
            }
            else
            {
                return DBHelperSQL.Delete(dict);
            }
        }
        public static void DeleteRelativeOra(string ID, string CONSIGNID, string 亲属一ID, string 亲属二ID)
        {
            string sql = "delete from gdna.RELATION_DEFINITION where id='" + ID + "';";
            sql += "delete from gdna.CONSIGNMENT where id='" + CONSIGNID + "';";
            sql += "delete from gdna.RELATIVE where id='" + 亲属一ID + "';";
            sql += "delete from gdna.SAMPLE_EXAMINATION where SAMPLE_ID='" + 亲属一ID + "';";
            if (亲属二ID.Length > 0)
            {
                sql += "delete from gdna.RELATIVE where id='" + 亲属二ID + "';";
                sql += "delete from gdna.SAMPLE_EXAMINATION where SAMPLE_ID='" + 亲属二ID + "';";
            }
            DBHelperOracle.ExecuteSql(sql);
        }
        [WebMethod]
        public string GetOneMpr(string ID)
        {
            return DBHelperSQL.Select("失踪亲属视图", "ID='" + ID + "'", "ID", "*").GetXml();
        }
        [WebMethod]
        public string ImportToOraMpr(string ID, string userName)
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


            if (DBHelperOracle.Query("select id from gdna.relation_definition where id='" + ID + "'").Tables[0].Rows.Count == 0)
            {
                using (OracleConnection dbConnectionOra = new OracleConnection(DBHelperOracle.strConnectionString))
                {
                    dbConnectionOra.Open();
                    using (OracleTransaction transOra = dbConnectionOra.BeginTransaction())
                    {
                        DataSet viewds = DBHelperSQL.Select("失踪亲属视图", "ID='" + ID + "'", string.Empty, "*");
                        DataRow viewdr = viewds.Tables[0].Rows[0];
                        string userOraId = DBHelperOracle.GetUserOraId(viewdr["一检姓名"].ToString());

                        DataSet rdds = DBHelperSQL.Select("亲属定义", "ID='" + ID + "'", string.Empty, "*");
                        DataRow rddr = rdds.Tables[0].Rows[0];

                        CONSIGNMENTOracleDAL.AddWithTransaction(viewdr["委托表号"].ToString(), viewdr["委托单位编号"].ToString(), string.Empty,
                      viewdr["委托单位名称"].ToString(), viewdr["委托单位电话"].ToString(), viewdr["受理时间"].ToString(), viewdr["一送姓名"].ToString(),
                      viewdr["一送警号"].ToString(), viewdr["委托单位地址"].ToString(), userName, dbConnectionOra, transOra);
                        RELATION_DEFINITIONOracleDAL.AddWithTransaction(ID, RELATION[rddr["亲属关系"].ToString()], rddr["亲属一ID"].ToString(),
                            rddr["亲属二ID"].ToString(), rddr["出生日期"].ToString(), GENDER[rddr["性别"].ToString()], rddr["特殊特征"].ToString(),
                            rddr["体表标记"].ToString(), rddr["姓名"].ToString(), viewdr["一检姓名"].ToString(), dbConnectionOra, transOra);
                        RELATIVEOracleDAL.AddWithTransaction(rddr["亲属一ID"].ToString(), string.Empty, viewdr["委托表号"].ToString(), "2", ID,
                            RELATION_WITH_TARGET[viewdr["亲属一目标关系"].ToString()], viewdr["亲属一姓名"].ToString(),
                            EVIDENCE_TYPE[viewdr["亲属一样本类型"].ToString()], GENDER[viewdr["亲属一性别"].ToString()],
                            PERSONNEL_TYPE[viewdr["亲属一人员类型"].ToString()], viewdr["亲属一出生日期"].ToString(), NATIONALITY[viewdr["亲属一民族"].ToString()],
                            COUNTRY[viewdr["亲属一国籍"].ToString()], viewdr["亲属一身份证"].ToString(), EDUCATION_LEVEL[viewdr["亲属一学历"].ToString()],
                            IDENTITY[viewdr["亲属一身份"].ToString()], viewdr["亲属一籍贯"].ToString(), viewdr["亲属一现住址"].ToString(),
                            viewdr["亲属一包装情况"].ToString(), viewdr["亲属一样本描述"].ToString(), viewdr["亲属一备注"].ToString(),
                            viewdr["亲属一样本编号"].ToString(), Helper.GetNextNoForDna(DBHelperOracle.INIT_SERVER_NO, "P"), viewdr["一检姓名"].ToString(),
                            userOraId, dbConnectionOra, transOra);
                        if (!rddr["亲属关系"].ToString().Equals("单亲"))
                        {
                            RELATIVEOracleDAL.AddWithTransaction(rddr["亲属二ID"].ToString(), string.Empty, viewdr["委托表号"].ToString(), "2", string.Empty,
                            RELATION_WITH_TARGET[viewdr["亲属二目标关系"].ToString()], viewdr["亲属二姓名"].ToString(),
                            EVIDENCE_TYPE[viewdr["亲属二样本类型"].ToString()], GENDER[viewdr["亲属二性别"].ToString()],
                            PERSONNEL_TYPE[viewdr["亲属二人员类型"].ToString()], viewdr["亲属二出生日期"].ToString(), NATIONALITY[viewdr["亲属二民族"].ToString()],
                            COUNTRY[viewdr["亲属二国籍"].ToString()], viewdr["亲属二身份证"].ToString(), EDUCATION_LEVEL[viewdr["亲属二学历"].ToString()],
                            IDENTITY[viewdr["亲属二身份"].ToString()], viewdr["亲属二籍贯"].ToString(), viewdr["亲属二现住址"].ToString(),
                            viewdr["亲属二包装情况"].ToString(), viewdr["亲属二样本描述"].ToString(), viewdr["亲属二备注"].ToString(),
                            viewdr["亲属二样本编号"].ToString(), Helper.GetNextNoForDna(DBHelperOracle.INIT_SERVER_NO, "P"), viewdr["一检姓名"].ToString(),
                            userOraId, dbConnectionOra, transOra);
                        }

                        transOra.Commit();
                    }
                }
            }
            return "1";
        }
        #endregion
        #region 案件亲属
        [WebMethod]
        public string Insert(string ID, string 案件ID, string 委托编号, string 库类型, string 亲属关系, string 亲属一ID, string 亲属二ID, string 姓名,
            string 姓名1, string 样本类型1, string 性别1, string 身份证1, string 籍贯1, string 样本描述1, string 目标关系1,
            string 姓名2, string 样本类型2, string 性别2, string 身份证2, string 籍贯2, string 样本描述2, string 目标关系2)
        {
            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("ID", 亲属一ID);
                    dict.Add("姓名", 姓名1);
                    dict.Add("样本类型", 样本类型1);
                    dict.Add("性别", 性别1);
                    dict.Add("身份证", 身份证1);
                    dict.Add("籍贯", 籍贯1);
                    dict.Add("样本描述", 样本描述1);
                    dict.Add("目标关系", 目标关系1);
                    DBHelperSQL.Insert("亲属信息", dict, dbConnection, trans);

                    if (!亲属关系.Equals("单亲"))
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("ID", 亲属二ID);
                        dict.Add("姓名", 姓名2);
                        dict.Add("样本类型", 样本类型2);
                        dict.Add("性别", 性别2);
                        dict.Add("身份证", 身份证2);
                        dict.Add("籍贯", 籍贯2);
                        dict.Add("样本描述", 样本描述2);
                        dict.Add("目标关系", 目标关系2);
                        dict.Add("创建时间", DateTime.Now.AddSeconds(1).ToString());
                        DBHelperSQL.Insert("亲属信息", dict, dbConnection, trans);
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("ID", ID);
                    dict.Add("案件ID", 案件ID);
                    dict.Add("委托编号", 委托编号);
                    dict.Add("库类型", 库类型);
                    dict.Add("亲属关系", 亲属关系);
                    dict.Add("亲属一ID", 亲属一ID);
                    if (亲属关系.Equals("单亲"))
                    {
                        dict.Add("亲属二ID", string.Empty);
                    }
                    else
                    {
                        dict.Add("亲属二ID", 亲属二ID);
                    }
                    dict.Add("姓名", 姓名);
                    DBHelperSQL.Insert("亲属定义", dict, dbConnection, trans);

                    trans.Commit();
                }
            }
            return "1";
        }
        [WebMethod]
        public string InsertWithNo(string ID, string 案件ID, string 委托编号, string 库类型, string 亲属关系, string 亲属一ID, string 亲属二ID, string 姓名,
            string 姓名1, string 样本类型1, string 性别1, string 身份证1, string 籍贯1, string 样本描述1, string 目标关系1,
            string 姓名2, string 样本类型2, string 性别2, string 身份证2, string 籍贯2, string 样本描述2, string 目标关系2,
            string preFix, string tableName, string len, string CONSIGNID, string userName, string CASE_ORA_FLAG)//2012.1.4
        {
            int num = Convert.ToInt32(DataReader.GetNextSLN(preFix, tableName));
            string 样本编号1 = preFix + num.ToString().PadLeft(Convert.ToInt32(len), '0');
            num++;
            string 样本编号2 = preFix + num.ToString().PadLeft(Convert.ToInt32(len), '0');
            string result = 样本编号1 + "￥" + 样本编号2;

            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("ID", 亲属一ID);
                    dict.Add("姓名", 姓名1);
                    dict.Add("样本类型", 样本类型1);
                    dict.Add("性别", 性别1);
                    dict.Add("身份证", 身份证1);
                    dict.Add("籍贯", 籍贯1);
                    dict.Add("样本描述", 样本描述1);
                    dict.Add("目标关系", 目标关系1);
                    dict.Add("样本编号", 样本编号1);
                    DBHelperSQL.Insert("亲属信息", dict, dbConnection, trans);

                    if (!亲属关系.Equals("单亲"))
                    {
                        num++;
                        dict = new Dictionary<string, string>();
                        dict.Add("ID", 亲属二ID);
                        dict.Add("姓名", 姓名2);
                        dict.Add("样本类型", 样本类型2);
                        dict.Add("性别", 性别2);
                        dict.Add("身份证", 身份证2);
                        dict.Add("籍贯", 籍贯2);
                        dict.Add("样本描述", 样本描述2);
                        dict.Add("目标关系", 目标关系2);
                        dict.Add("创建时间", DateTime.Now.AddSeconds(1).ToString());
                        dict.Add("样本编号", 样本编号2);
                        DBHelperSQL.Insert("亲属信息", dict, dbConnection, trans);
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("ID", ID);
                    dict.Add("案件ID", 案件ID);
                    dict.Add("委托编号", 委托编号);
                    dict.Add("库类型", 库类型);
                    dict.Add("亲属关系", 亲属关系);
                    dict.Add("亲属一ID", 亲属一ID);
                    if (亲属关系.Equals("单亲"))
                    {
                        dict.Add("亲属二ID", string.Empty);
                    }
                    else
                    {
                        dict.Add("亲属二ID", 亲属二ID);
                    }
                    dict.Add("姓名", 姓名);
                    dict.Add("ORA_FLAG", CASE_ORA_FLAG);
                    DBHelperSQL.Insert("亲属定义", dict, dbConnection, trans);

                    if (CASE_ORA_FLAG.Equals("1"))
                    {
                        string userOraId = DBHelperOracle.GetUserOraId(userName);
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
                        using (OracleConnection dbConnectionOra = new OracleConnection(DBHelperOracle.strConnectionString))
                        {
                            dbConnectionOra.Open();
                            using (OracleTransaction transOra = dbConnectionOra.BeginTransaction())
                            {
                                RELATION_DEFINITIONOracleDAL.AddWithTransaction(ID, RELATION[亲属关系], 亲属一ID,
                                        亲属二ID, string.Empty, string.Empty, string.Empty, string.Empty,
                                            姓名, userName, dbConnectionOra, transOra);
                                RELATIVEOracleDAL.AddWithTransaction(亲属一ID, 案件ID, CONSIGNID,
                                    Helper.GetRelativeTypeByScType(库类型), ID,
                                    RELATION_WITH_TARGET[目标关系1], 姓名1,
                                    EVIDENCE_TYPE[样本类型1], GENDER[性别1],
                                    string.Empty, string.Empty, string.Empty,
                                    string.Empty, 身份证1, string.Empty,
                                    string.Empty, 籍贯1, string.Empty,
                                    string.Empty, 样本描述1, string.Empty,
                                    样本编号1, Helper.GetNextNoForDna(DBHelperOracle.INIT_SERVER_NO, "P"), userName,
                                    userOraId, dbConnectionOra, transOra);
                                if (!亲属关系.Equals("单亲"))
                                {
                                    RELATIVEOracleDAL.AddWithTransaction(亲属二ID, 案件ID, CONSIGNID,
                                    Helper.GetRelativeTypeByScType(库类型), ID,
                                    RELATION_WITH_TARGET[目标关系2], 姓名2,
                                    EVIDENCE_TYPE[样本类型2], GENDER[性别2],
                                    string.Empty, string.Empty, string.Empty,
                                    string.Empty, 身份证2, string.Empty,
                                    string.Empty, 籍贯2, string.Empty,
                                    string.Empty, 样本描述2, string.Empty,
                                    样本编号2, Helper.GetNextNoForDna(DBHelperOracle.INIT_SERVER_NO, "P"), userName,
                                    userOraId, dbConnectionOra, transOra);
                                }
                                transOra.Commit();
                            }
                        }
                    }

                    trans.Commit();
                }
            }
            return result;
        }
        [WebMethod]
        public string Update(string ID, string 亲属一ID, string 亲属二ID, string 姓名, string 亲属关系,
            string 姓名1, string 样本类型1, string 性别1, string 身份证1, string 籍贯1, string 样本描述1, string 目标关系1, string 样本编号1,
            string 姓名2, string 样本类型2, string 性别2, string 身份证2, string 籍贯2, string 样本描述2, string 目标关系2, string 样本编号2, string ORA_FLAG)//2012.1.4
        {
            if (!DataReader.ValidSlnDup2(样本编号1, 亲属一ID, 样本编号2, 亲属二ID))
            {
                return "样本编号重复！请检查。";
            }

            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("姓名", 姓名);
                    dict.Add("亲属关系", 亲属关系);
                    if (亲属关系.Equals("单亲"))
                    {
                        dict.Add("亲属二ID", string.Empty);
                    }
                    else
                    {
                        dict.Add("亲属二ID", 亲属二ID);
                    }
                    DBHelperSQL.Update("亲属定义", "ID='" + ID + "'", dict, dbConnection, trans);

                    dict = new Dictionary<string, string>();
                    dict.Add("姓名", 姓名1);
                    dict.Add("样本类型", 样本类型1);
                    dict.Add("性别", 性别1);
                    dict.Add("身份证", 身份证1);
                    dict.Add("籍贯", 籍贯1);
                    dict.Add("样本描述", 样本描述1);
                    dict.Add("目标关系", 目标关系1);
                    dict.Add("样本编号", 样本编号1);
                    DBHelperSQL.Update("亲属信息", "ID='" + 亲属一ID + "'", dict, dbConnection, trans);

                    if (亲属关系.Equals("单亲") && 亲属二ID.Length > 0)//从非单亲变成单亲
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("亲属信息", "ID，" + 亲属二ID);
                        DBHelperSQL.Delete(dict, dbConnection, trans); ;
                    }
                    else if (亲属二ID.Length > 0)//本来就是非单亲
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("姓名", 姓名2);
                        dict.Add("样本类型", 样本类型2);
                        dict.Add("性别", 性别2);
                        dict.Add("身份证", 身份证2);
                        dict.Add("籍贯", 籍贯2);
                        dict.Add("样本描述", 样本描述2);
                        dict.Add("目标关系", 目标关系2);
                        dict.Add("样本编号", 样本编号2);
                        DBHelperSQL.Update("亲属信息", "ID='" + 亲属二ID + "'", dict, dbConnection, trans);
                    }
                    else if (!亲属关系.Equals("单亲"))//从单亲变成非单亲
                    {
                        trans.Rollback();
                        return "不允许从单亲改为其他亲属关系，请删除该数据后重新录入。";
                    }

                    if (ORA_FLAG.Equals("1"))
                    {
                        DNADICTWS dictWs = new DNADICTWS();
                        IDictionary<string, string> EVIDENCE_TYPE = dictWs.GetDnaDict("样本类型");
                        IDictionary<string, string> GENDER = dictWs.GetDnaDict("性别");
                        IDictionary<string, string> RELATION_WITH_TARGET = dictWs.GetDnaDict("目标关系");

                        using (OracleConnection dbConnectionOra = new OracleConnection(DBHelperOracle.strConnectionString))
                        {
                            dbConnectionOra.Open();
                            using (OracleTransaction transOra = dbConnectionOra.BeginTransaction())
                            {
                                RELATION_DEFINITIONOracleDAL.UpdateWithTransaction(ID, string.Empty, string.Empty, string.Empty, string.Empty,
                                             姓名, "admin", dbConnectionOra, transOra);
                                RELATIVEOracleDAL.UpdateWithTransaction(亲属一ID,
                                    RELATION_WITH_TARGET[目标关系1], 姓名1,
                                    EVIDENCE_TYPE[样本类型1], GENDER[性别1],
                                    string.Empty, string.Empty, string.Empty,
                                    string.Empty, 身份证1, string.Empty,
                                    string.Empty, 籍贯1, string.Empty,
                                    string.Empty, 样本描述1, string.Empty,
                                    样本编号1, "admin", dbConnectionOra, transOra);
                                if (!亲属关系.Equals("单亲"))
                                {
                                    RELATIVEOracleDAL.UpdateWithTransaction(亲属二ID,
                                 RELATION_WITH_TARGET[目标关系2], 姓名2,
                                 EVIDENCE_TYPE[样本类型2], GENDER[性别2],
                                 string.Empty, string.Empty, string.Empty,
                                 string.Empty, 身份证2, string.Empty,
                                 string.Empty, 籍贯2, string.Empty,
                                 string.Empty, 样本描述2, string.Empty,
                                 样本编号2, "admin", dbConnectionOra, transOra);
                                }
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
        public string Delete(string ID, string 亲属一ID, string 亲属二ID, string ORA_FLAG)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("亲属定义", "ID，" + ID);
            dict.Add("亲属信息", "ID，" + 亲属一ID);
            if (亲属二ID.Length > 0)
            {
                dict["亲属信息"] = "ID，" + 亲属一ID + "￥" + 亲属二ID;
            }
            if (ORA_FLAG.Equals("1"))
            {
                using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
                {
                    dbConnection.Open();
                    using (SqlTransaction trans = dbConnection.BeginTransaction())
                    {
                        DBHelperSQL.Delete(dict, dbConnection, trans);
                        DeleteRelativeOra(ID, string.Empty, 亲属一ID, 亲属二ID);
                        trans.Commit();
                        return "1";
                    }
                }
            }
            else
            {
                return DBHelperSQL.Delete(dict);
            }
        }
        [WebMethod]
        public string GetAll(string 案件ID, string 委托编号, string 库类型)
        {
            string filter = string.Empty;
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";
            if (案件ID.Length > 0) filter += "案件ID='" + 案件ID + "' and ";
            if (库类型.Length > 0) filter += "库类型='" + 库类型 + "' and ";

            return DBHelperSQL.Select("案件亲属视图", Helper.CutFilter(filter), "创建时间", "*").GetXml();
        }
        #endregion
    }
}
