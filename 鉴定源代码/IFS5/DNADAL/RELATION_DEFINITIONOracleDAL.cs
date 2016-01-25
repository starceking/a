using System;
using System.Collections.Generic;

using System.Text;

using System.Data.OracleClient;
using LIB;
using System.Data;
using DNADAL;

namespace IFSOracleDAL
{
    public class RELATION_DEFINITIONOracleDAL
    {
        public static void AddWithTransaction(string ID, string RELATION, string RELATIVE1_ID, string RELATIVE2_ID, string TARGET_BIRTHDATE_FROM,
            string TARGET_GENDER, string TARGET_SPECIALITY, string TARGET_EXTRINSIC_SIGN, string REMARLK, string CREATE_USER,
            OracleConnection dbConnection, OracleTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into gdna.RELATION_DEFINITION(");
            strSql.Append("ID,RELATION,RELATIVE1_ID,RELATIVE2_ID,TARGET_BIRTHDATE_FROM,TARGET_GENDER,TARGET_SPECIALITY,TARGET_EXTRINSIC_SIGN,REMARLK,LAB_ID,INIT_SERVER_NO,CREATE_USER,CREATE_DATETIME)");
            strSql.Append(" values (");
            strSql.Append(":ID,:RELATION,:RELATIVE1_ID,:RELATIVE2_ID,to_date(:TARGET_BIRTHDATE_FROM,'yyyy-mm-dd'),:TARGET_GENDER,:TARGET_SPECIALITY,:TARGET_EXTRINSIC_SIGN,:REMARLK,:LAB_ID,:INIT_SERVER_NO,:CREATE_USER,sysdate)");
            OracleParameter[] parameters = {
					new OracleParameter(":ID",Helper.GetDBValue(ID)),
					new OracleParameter(":RELATION",Helper.GetDBValue(RELATION)),
                    new OracleParameter(":RELATIVE1_ID",Helper.GetDBValue(RELATIVE1_ID)),
                    new OracleParameter(":RELATIVE2_ID",Helper.GetDBValue(RELATIVE2_ID)),
                    new OracleParameter(":TARGET_BIRTHDATE_FROM",Helper.GetDBValue(TARGET_BIRTHDATE_FROM.Length>0?TARGET_BIRTHDATE_FROM.Split(' ')[0]:string.Empty)),
                    new OracleParameter(":TARGET_GENDER",Helper.GetDBValue(TARGET_GENDER)),
                    new OracleParameter(":TARGET_SPECIALITY", Helper.GetDBValue( TARGET_SPECIALITY)),
                    new OracleParameter(":TARGET_EXTRINSIC_SIGN",Helper.GetDBValue( TARGET_EXTRINSIC_SIGN)),
                    new OracleParameter(":REMARLK",Helper.GetDBValue( REMARLK)),
                    new OracleParameter(":LAB_ID", DBHelperOracle.LAB_ID),
                    new OracleParameter(":INIT_SERVER_NO", DBHelperOracle.INIT_SERVER_NO),
                    new OracleParameter(":CREATE_USER", Helper.GetDBValue(CREATE_USER))};
            DBHelperOracle.ExecuteSqlWithTransaction(strSql.ToString(), parameters, dbConnection, trans);
        }
        public static void UpdateWithTransaction(string ID, string TARGET_BIRTHDATE_FROM,
            string TARGET_GENDER, string TARGET_SPECIALITY, string TARGET_EXTRINSIC_SIGN, string REMARLK,
            string userName, OracleConnection dbConnection, OracleTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GDNA.RELATION_DEFINITION set ");
            strSql.Append("TARGET_BIRTHDATE_FROM=to_date(:TARGET_BIRTHDATE_FROM,'yyyy-mm-dd'),");
            strSql.Append("TARGET_GENDER=:TARGET_GENDER,");
            strSql.Append("TARGET_SPECIALITY=:TARGET_SPECIALITY,");
            strSql.Append("TARGET_EXTRINSIC_SIGN=:TARGET_EXTRINSIC_SIGN,");
            strSql.Append("REMARLK=:REMARLK,");
            strSql.Append("UPDATE_USER=:UPDATE_USER, ");
            strSql.Append("UPDATE_DATETIME=sysdate ");
            strSql.Append(" where ID=:ID ");
            OracleParameter[] parameters = {
					new OracleParameter(":ID",Helper.GetDBValue(ID)),
                    new OracleParameter(":TARGET_BIRTHDATE_FROM",Helper.GetDBValue(TARGET_BIRTHDATE_FROM.Length>0?TARGET_BIRTHDATE_FROM.Split(' ')[0]:string.Empty)),
                    new OracleParameter(":TARGET_GENDER",Helper.GetDBValue(TARGET_GENDER)),
                    new OracleParameter(":TARGET_SPECIALITY", Helper.GetDBValue( TARGET_SPECIALITY)),
                    new OracleParameter(":TARGET_EXTRINSIC_SIGN",Helper.GetDBValue( TARGET_EXTRINSIC_SIGN)),
                    new OracleParameter(":REMARLK",Helper.GetDBValue( REMARLK)),
                    new OracleParameter(":UPDATE_USER", Helper.GetDBValue(userName))};

            DBHelperOracle.ExecuteSqlWithTransaction(strSql.ToString(), parameters, dbConnection, trans);
        }
    }
}
