using System;
using System.Collections.Generic;

using System.Text;

using System.Data.OracleClient;
using LIB;
using System.Data;
using DNADAL;

namespace IFSOracleDAL
{
    public class LAW_CASEOracleDAL
    {
        public static void AddWithTransaction(string ID, string CONSIGNMENT_ID, string CASE_NO, string CASE_LAB_NO, string CASE_NAME, string SCENE_REGIONALISM, string SCENE_PLACE,
            string OCCURRENCE_DATE, string CASE_TYPE, string CASE_PROPERTY, string IDENTIFIY_REQUEST, string CASE_SUMMARY, string REMARK,
            string RECEPTION_MAN_ID, string RECEPTION_MAN, OracleConnection dbConnection, OracleTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into gdna.LAW_CASE(");
            strSql.Append("ID,CONSIGNMENT_ID,CASE_NO,CASE_LAB_NO,CASE_NAME,SCENE_REGIONALISM,SCENE_PLACE,OCCURRENCE_DATE,CASE_TYPE,CASE_PROPERTY,IDENTIFIY_REQUEST,CASE_SUMMARY,REMARK,LAB_ID,INIT_SERVER_NO,CREATE_USER,CREATE_DATETIME,RECEPTION_MAN_ID, RECEPTION_MAN, RECEPTION_REGIONALISM, RECEPTION_ORG_NAME,RECEPTION_TEL,STORE_FLAG)");
            strSql.Append(" values (");
            strSql.Append(":ID,:CONSIGNMENT_ID,:CASE_NO,:CASE_LAB_NO,:CASE_NAME,:SCENE_REGIONALISM,:SCENE_PLACE,to_date(:OCCURRENCE_DATE,'yyyy-mm-dd'),:CASE_TYPE,:CASE_PROPERTY,:IDENTIFIY_REQUEST,:CASE_SUMMARY,:REMARK,:LAB_ID,:INIT_SERVER_NO,:CREATE_USER,sysdate,:RECEPTION_MAN_ID, :RECEPTION_MAN, :RECEPTION_REGIONALISM, :RECEPTION_ORG_NAME,:RECEPTION_TEL,'0')");
            OracleParameter[] parameters = {
					new OracleParameter(":ID",Helper.GetDBValue(ID)),
					new OracleParameter(":CONSIGNMENT_ID",Helper.GetDBValue(CONSIGNMENT_ID)),
                    new OracleParameter(":CASE_NO",Helper.GetDBValue(CASE_NO)),
                    new OracleParameter(":CASE_LAB_NO",Helper.GetDBValue(CASE_LAB_NO)),
                    new OracleParameter(":CASE_NAME",Helper.GetDBValue(CASE_NAME)),
                    new OracleParameter(":SCENE_REGIONALISM",Helper.GetDBValue(SCENE_REGIONALISM)),
                    new OracleParameter(":SCENE_PLACE",Helper.GetDBValue(SCENE_PLACE)),
                    new OracleParameter(":OCCURRENCE_DATE", Helper.GetDBValue( OCCURRENCE_DATE.Length>0?OCCURRENCE_DATE.Split(' ')[0]:string.Empty)),
                    new OracleParameter(":CASE_TYPE",Helper.GetDBValue( CASE_TYPE)),
                    new OracleParameter(":CASE_PROPERTY",Helper.GetDBValue( CASE_PROPERTY)),
                    new OracleParameter(":IDENTIFIY_REQUEST",Helper.GetDBValue( IDENTIFIY_REQUEST)),
                    new OracleParameter(":CASE_SUMMARY",Helper.GetDBValue( CASE_SUMMARY)),
                    new OracleParameter(":REMARK",Helper.GetDBValue( REMARK)),
                    new OracleParameter(":LAB_ID", DBHelperOracle.LAB_ID),
                    new OracleParameter(":INIT_SERVER_NO", DBHelperOracle.INIT_SERVER_NO),
                    new OracleParameter(":CREATE_USER", Helper.GetDBValue(RECEPTION_MAN)),
                    new OracleParameter(":RECEPTION_MAN_ID", Helper.GetDBValue(RECEPTION_MAN_ID)),
                    new OracleParameter(":RECEPTION_MAN", Helper.GetDBValue(RECEPTION_MAN)),
                    new OracleParameter(":RECEPTION_REGIONALISM", Helper.GetDBValue(DBHelperOracle.RECEPTION_REGIONALISM)),
                    new OracleParameter(":RECEPTION_ORG_NAME", Helper.GetDBValue(DBHelperOracle.RECEPTION_ORG_NAME)),
                    new OracleParameter(":RECEPTION_TEL", Helper.GetDBValue(DBHelperOracle.RECEPTION_TEL))};
            DBHelperOracle.ExecuteSqlWithTransaction(strSql.ToString(), parameters, dbConnection, trans);
        }
        public static void UpdateWithTransaction(string ID, string CASE_LAB_NO, string CASE_NAME, string SCENE_REGIONALISM, string SCENE_PLACE,
            string OCCURRENCE_DATE, string CASE_TYPE, string CASE_PROPERTY, string IDENTIFIY_REQUEST, string CASE_SUMMARY, string REMARK,
            string userName, OracleConnection dbConnection, OracleTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GDNA.LAW_CASE set ");
            strSql.Append("CASE_LAB_NO=:CASE_LAB_NO,");
            strSql.Append("CASE_NAME=:CASE_NAME,");
            strSql.Append("SCENE_REGIONALISM=:SCENE_REGIONALISM,");
            strSql.Append("SCENE_PLACE=:SCENE_PLACE,");
            strSql.Append("OCCURRENCE_DATE=to_date(:OCCURRENCE_DATE,'yyyy-mm-dd'),");
            strSql.Append("CASE_TYPE=:CASE_TYPE,");
            strSql.Append("CASE_PROPERTY=:CASE_PROPERTY,");
            strSql.Append("IDENTIFIY_REQUEST=:IDENTIFIY_REQUEST,");
            strSql.Append("CASE_SUMMARY=:CASE_SUMMARY,");
            strSql.Append("REMARK=:REMARK,");
            strSql.Append("UPDATE_USER=:UPDATE_USER, ");
            strSql.Append("UPDATE_DATETIME=sysdate ");
            strSql.Append(" where ID=:ID ");
            OracleParameter[] parameters = {
					new OracleParameter(":ID",  Helper.GetDBValue(ID)),
					new OracleParameter(":CASE_LAB_NO",Helper.GetDBValue(CASE_LAB_NO)),
					new OracleParameter(":CASE_NAME",Helper.GetDBValue( CASE_NAME)),
                    new OracleParameter(":SCENE_REGIONALISM", Helper.GetDBValue(SCENE_REGIONALISM)),
					new OracleParameter(":SCENE_PLACE", Helper.GetDBValue(SCENE_PLACE)),
					new OracleParameter(":OCCURRENCE_DATE",Helper.GetDBValue( OCCURRENCE_DATE.Length>0?OCCURRENCE_DATE.Split(' ')[0]:string.Empty)),
                    new OracleParameter(":CASE_TYPE",Helper.GetDBValue( CASE_TYPE)),
					new OracleParameter(":CASE_PROPERTY",Helper.GetDBValue( CASE_PROPERTY)),
					new OracleParameter(":IDENTIFIY_REQUEST", Helper.GetDBValue(IDENTIFIY_REQUEST)),
					new OracleParameter(":CASE_SUMMARY", Helper.GetDBValue(CASE_SUMMARY)),
					new OracleParameter(":REMARK",Helper.GetDBValue( REMARK)),
                    new OracleParameter(":UPDATE_USER",Helper.GetDBValue( userName))};

            DBHelperOracle.ExecuteSqlWithTransaction(strSql.ToString(), parameters, dbConnection, trans);
        }
        public static void InsertSample_Examination(string ID, string SAMPLE_ID, string APPOINTED_EXAMINER_ID, string CREATE_USER,
            string REQUEST_MAN, string SAMPLE_CATEGORY, OracleConnection dbConnection, OracleTransaction trans)
        {
            string sql = string.Format(@"Insert into gdna.SAMPLE_EXAMINATION(
ID,SAMPLE_ID,APPOINTED_EXAMINER_ID,SAMPLE_CATEGORY,
REQUEST_MAN,REQUEST_DATE,EXAMINE_ITEM,EXAMINE_STAGE,NEXT_EXAMINE_STAGE,EXAMINE_STATUS,
CREATE_USER,
CREATE_DATETIME) values('{0}','{1}','{2}','{4}','{3}',SYSDATE,'1','0','1','1','{5}',SYSDATE)",
ID, SAMPLE_ID, APPOINTED_EXAMINER_ID, REQUEST_MAN,
SAMPLE_CATEGORY, CREATE_USER);
            DBHelperOracle.ExecuteSqlWithTransaction(sql, dbConnection, trans);
        }
    }
}
