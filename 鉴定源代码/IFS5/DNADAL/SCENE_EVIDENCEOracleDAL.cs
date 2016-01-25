using System;
using System.Collections.Generic;

using System.Text;

using System.Data.OracleClient;
using System.Data;
using LIB;
using DNADAL;

namespace IFSOracleDAL
{
    public class SCENE_EVIDENCEOracleDAL
    {
        public static void AddWithTransaction(string ID, string CASE_ID, string EVIDENCE_NO, string EVIDENCE_NAME, string SAMPLE_TYPE, string AMOUNT,
            string CARRIER_TYPE, string SAMPLE_LAB_NO, string SAMPLE_PACKAGING, string EXAMINE_USER1,
            string reception_man_id, OracleConnection dbConnection, OracleTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into gdna.SCENE_EVIDENCE(");
            strSql.Append("ID,CASE_ID,EVIDENCE_NO,EVIDENCE_NAME,SAMPLE_TYPE,AMOUNT,CARRIER_TYPE,SAMPLE_LAB_NO,SAMPLE_PACKAGING,EXAMINE_USER1,STORE_FLAG,STR_FLAG,LAB_ID,INIT_SERVER_NO,CREATE_USER,CREATE_DATETIME)");
            strSql.Append(" values (");
            strSql.Append(":ID,:CASE_ID,:EVIDENCE_NO,:EVIDENCE_NAME,:SAMPLE_TYPE,:AMOUNT,:CARRIER_TYPE,:SAMPLE_LAB_NO,:SAMPLE_PACKAGING,:EXAMINE_USER1,:STORE_FLAG,:STR_FLAG,:LAB_ID,:INIT_SERVER_NO,:CREATE_USER,sysdate)");
            OracleParameter[] parameters = {
					new OracleParameter(":ID",Helper.GetDBValue(ID)),
					new OracleParameter(":CASE_ID",Helper.GetDBValue(CASE_ID)),
                    new OracleParameter(":EVIDENCE_NO",Helper.GetDBValue(EVIDENCE_NO)),
                    new OracleParameter(":EVIDENCE_NAME", Helper.GetDBValue(EVIDENCE_NAME)),
                    new OracleParameter(":SAMPLE_TYPE", Helper.GetDBValue(SAMPLE_TYPE)),
                    new OracleParameter(":AMOUNT",Helper.GetDBValue(AMOUNT)),
                    new OracleParameter(":CARRIER_TYPE",Helper.GetDBValue(CARRIER_TYPE)),
                    new OracleParameter(":SAMPLE_LAB_NO",Helper.GetDBValue(SAMPLE_LAB_NO)),
                    new OracleParameter(":SAMPLE_PACKAGING",Helper.GetDBValue(SAMPLE_PACKAGING)),
                    new OracleParameter(":EXAMINE_USER1", Helper.GetDBValue(EXAMINE_USER1)),
                    new OracleParameter(":STORE_FLAG", "0"),
                    new OracleParameter(":STR_FLAG","0"),
                    new OracleParameter(":LAB_ID", DBHelperOracle.LAB_ID),
                    new OracleParameter(":INIT_SERVER_NO", DBHelperOracle.INIT_SERVER_NO),
                    new OracleParameter(":CREATE_USER",Helper.GetDBValue(EXAMINE_USER1))};
            DBHelperOracle.ExecuteSqlWithTransaction(strSql.ToString(), parameters, dbConnection, trans);
            LAW_CASEOracleDAL.InsertSample_Examination(Helper.GenerateID(), ID, reception_man_id,
                EXAMINE_USER1, EXAMINE_USER1, "1", dbConnection, trans);
        }

        public static void UpdateWithTransaction(string ID, string EVIDENCE_NAME, string SAMPLE_TYPE, string AMOUNT,
            string CARRIER_TYPE, string SAMPLE_LAB_NO, string SAMPLE_PACKAGING, string userName, OracleConnection dbConnection, OracleTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GDNA.SCENE_EVIDENCE set ");
            strSql.Append("EVIDENCE_NAME=:EVIDENCE_NAME,");
            strSql.Append("SAMPLE_TYPE=:SAMPLE_TYPE,");
            strSql.Append("SAMPLE_LAB_NO=:SAMPLE_LAB_NO,");
            strSql.Append("SAMPLE_PACKAGING=:SAMPLE_PACKAGING,");
            strSql.Append("AMOUNT=:AMOUNT,");
            strSql.Append("CARRIER_TYPE=:CARRIER_TYPE,");
            strSql.Append("UPDATE_USER=:UPDATE_USER, ");
            strSql.Append("UPDATE_DATETIME=sysdate ");
            strSql.Append(" where ID=:ID ");
            OracleParameter[] parameters = {
					new OracleParameter(":ID",  Helper.GetDBValue(ID)),
					new OracleParameter(":EVIDENCE_NAME",Helper.GetDBValue(EVIDENCE_NAME)),
					new OracleParameter(":SAMPLE_TYPE",Helper.GetDBValue( SAMPLE_TYPE)),
					new OracleParameter(":SAMPLE_LAB_NO", Helper.GetDBValue(SAMPLE_LAB_NO)),
                    new OracleParameter(":SAMPLE_PACKAGING",Helper.GetDBValue(SAMPLE_PACKAGING)),
					new OracleParameter(":AMOUNT", Helper.GetDBValue(AMOUNT)),
					new OracleParameter(":CARRIER_TYPE",Helper.GetDBValue( CARRIER_TYPE)),
                    new OracleParameter(":UPDATE_USER",Helper.GetDBValue( userName))};

            DBHelperOracle.ExecuteSqlWithTransaction(strSql.ToString(), parameters, dbConnection, trans);
        }

    }
}
