using System;
using System.Collections.Generic;

using System.Text;

using System.Data.OracleClient;
using System.Data;
using LIB;
using DNADAL;

namespace IFSOracleDAL
{
    public class UNKNOWN_DECEASEDOracleDAL
    {
        public static void AddWithTransaction(string ID, string CASE_ID, string CONSIGNMENT_ID, string PERSONNEL_NAME, string SAMPLE_TYPE, string GENDER,
            string SAMPLE_PACKAGING, string SAMPLE_DESCRIPTION, string SPECIALITY, string RESERVE2, string REMARK,
            string SAMPLE_LAB_NO, string PERSONNEL_NO, string EXAMINE_USER1, string RECEPTION_MAN_ID,
            OracleConnection dbConnection, OracleTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into gdna.UNKNOWN_DECEASED(");
            strSql.Append("ID,CASE_ID,CONSIGNMENT_ID,PERSONNEL_NAME,SAMPLE_TYPE,GENDER,SAMPLE_PACKAGING,SAMPLE_DESCRIPTION,SPECIALITY,RESERVE2,REMARK,SAMPLE_LAB_NO,PERSONNEL_NO,EXAMINE_USER1,STORE_FLAG,STR_FLAG,LAB_ID,INIT_SERVER_NO,CREATE_USER,CREATE_DATETIME,RECEPTION_MAN_ID, RECEPTION_MAN, RECEPTION_REGIONALISM, RECEPTION_ORG_NAME,RECEPTION_TEL)");
            strSql.Append(" values (");
            strSql.Append(":ID,:CASE_ID,:CONSIGNMENT_ID,:PERSONNEL_NAME,:SAMPLE_TYPE,:GENDER,:SAMPLE_PACKAGING,:SAMPLE_DESCRIPTION,:SPECIALITY,:RESERVE2,:REMARK,:SAMPLE_LAB_NO,:PERSONNEL_NO,:EXAMINE_USER1,:STORE_FLAG,:STR_FLAG,:LAB_ID,:INIT_SERVER_NO,:CREATE_USER,sysdate,:RECEPTION_MAN_ID, :RECEPTION_MAN, :RECEPTION_REGIONALISM, :RECEPTION_ORG_NAME,:RECEPTION_TEL)");
            OracleParameter[] parameters = {
					new OracleParameter(":ID",Helper.GetDBValue(ID)),
					new OracleParameter(":CASE_ID",Helper.GetDBValue(CASE_ID)),
                    new OracleParameter(":CONSIGNMENT_ID",Helper.GetDBValue(CONSIGNMENT_ID)),
                    new OracleParameter(":PERSONNEL_NAME", Helper.GetDBValue(PERSONNEL_NAME)),
					new OracleParameter(":SAMPLE_TYPE",Helper.GetDBValue(SAMPLE_TYPE)),
                    new OracleParameter(":GENDER", Helper.GetDBValue(GENDER)),
                    new OracleParameter(":SAMPLE_PACKAGING",Helper.GetDBValue(SAMPLE_PACKAGING)),
                    new OracleParameter(":SAMPLE_DESCRIPTION",Helper.GetDBValue(SAMPLE_DESCRIPTION)),
                    new OracleParameter(":SPECIALITY",Helper.GetDBValue(SPECIALITY)),
                    new OracleParameter(":RESERVE2",Helper.GetDBValue(RESERVE2)),
                    new OracleParameter(":REMARK",Helper.GetDBValue(REMARK)),
                    new OracleParameter(":SAMPLE_LAB_NO",Helper.GetDBValue(SAMPLE_LAB_NO)),
                    new OracleParameter(":PERSONNEL_NO",Helper.GetDBValue(PERSONNEL_NO)),
                    new OracleParameter(":EXAMINE_USER1", Helper.GetDBValue(EXAMINE_USER1)),
                    new OracleParameter(":STORE_FLAG", "0"),
                    new OracleParameter(":STR_FLAG","0"),
                    new OracleParameter(":LAB_ID", DBHelperOracle.LAB_ID),
                    new OracleParameter(":INIT_SERVER_NO", DBHelperOracle.INIT_SERVER_NO),
                    new OracleParameter(":CREATE_USER", Helper.GetDBValue(EXAMINE_USER1)),
                    new OracleParameter(":RECEPTION_MAN_ID", Helper.GetDBValue(RECEPTION_MAN_ID)),
                    new OracleParameter(":RECEPTION_MAN", Helper.GetDBValue(EXAMINE_USER1)),
                    new OracleParameter(":RECEPTION_REGIONALISM", Helper.GetDBValue(DBHelperOracle.RECEPTION_REGIONALISM)),
                    new OracleParameter(":RECEPTION_ORG_NAME", Helper.GetDBValue(DBHelperOracle.RECEPTION_ORG_NAME)),
                    new OracleParameter(":RECEPTION_TEL", Helper.GetDBValue(DBHelperOracle.RECEPTION_TEL))};
            DBHelperOracle.ExecuteSqlWithTransaction(strSql.ToString(), parameters, dbConnection, trans);
            LAW_CASEOracleDAL.InsertSample_Examination(Helper.GenerateID(), ID, RECEPTION_MAN_ID,
                          EXAMINE_USER1, EXAMINE_USER1, "5", dbConnection, trans);
        }
        public static void UpdateWithTransaction(string ID, string PERSONNEL_NAME, string SAMPLE_TYPE, string GENDER,
            string SAMPLE_PACKAGING, string SAMPLE_DESCRIPTION, string SPECIALITY, string RESERVE2, string REMARK,
            string SAMPLE_LAB_NO, string userName, OracleConnection dbConnection, OracleTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GDNA.UNKNOWN_DECEASED set ");
            strSql.Append("PERSONNEL_NAME=:PERSONNEL_NAME,");
            strSql.Append("SAMPLE_TYPE=:SAMPLE_TYPE,");
            strSql.Append("GENDER=:GENDER,");
            strSql.Append("SAMPLE_PACKAGING=:SAMPLE_PACKAGING,");
            strSql.Append("SAMPLE_DESCRIPTION=:SAMPLE_DESCRIPTION,");
            strSql.Append("SPECIALITY=:SPECIALITY,");
            strSql.Append("RESERVE2=:RESERVE2,");
            strSql.Append("REMARK=:REMARK,");
            strSql.Append("SAMPLE_LAB_NO=:SAMPLE_LAB_NO,");
            strSql.Append("UPDATE_USER=:UPDATE_USER, ");
            strSql.Append("UPDATE_DATETIME=sysdate ");
            strSql.Append(" where ID=:ID ");
            OracleParameter[] parameters = {
					new OracleParameter(":ID",  Helper.GetDBValue(ID)),
					new OracleParameter(":PERSONNEL_NAME",Helper.GetDBValue(PERSONNEL_NAME)),
                    new OracleParameter(":SAMPLE_TYPE", Helper.GetDBValue(SAMPLE_TYPE)),
					new OracleParameter(":GENDER", Helper.GetDBValue(GENDER)),	
					new OracleParameter(":SAMPLE_PACKAGING", Helper.GetDBValue(SAMPLE_PACKAGING)),
					new OracleParameter(":SAMPLE_DESCRIPTION", Helper.GetDBValue(SAMPLE_DESCRIPTION)),
                    new OracleParameter(":SPECIALITY",Helper.GetDBValue( SPECIALITY)),
                    new OracleParameter(":RESERVE2", Helper.GetDBValue(RESERVE2)),
                    new OracleParameter(":REMARK", Helper.GetDBValue(REMARK)),
                    new OracleParameter(":SAMPLE_LAB_NO", Helper.GetDBValue(SAMPLE_LAB_NO)),
                    new OracleParameter(":UPDATE_USER",Helper.GetDBValue( userName))};

            DBHelperOracle.ExecuteSqlWithTransaction(strSql.ToString(), parameters, dbConnection, trans);
        }
    }
}
