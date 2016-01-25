using System;
using System.Collections.Generic;

using System.Text;

using System.Data.OracleClient;
using System.Data;
using LIB;
using DNADAL;

namespace IFSOracleDAL
{
    public class MISSING_PERSONOracleDAL
    {
        public static void AddWithTransaction(string ID, string CONSIGNMENT_ID, string PERSONNEL_NAME, string SAMPLE_TYPE, string GENDER,
            string PERSONNEL_TYPE, string BIRTH_DATE, string NATIONALITY, string DISTRICT, string ID_CARD_NO,
            string EDUCATION_LEVEL, string IDENTITY, string NATIVE_PLACE_ADDR, string RESIDENCE_ADDR, string SAMPLE_PACKAGING,
            string SAMPLE_DESCRIPTION, string REMARK, string SAMPLE_LAB_NO,
            string PERSONNEL_NO, string EXAMINE_USER1, string reception_man_id, OracleConnection dbConnection, OracleTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into gdna.MISSING_PERSON(");
            strSql.Append("ID,CONSIGNMENT_ID,PERSONNEL_NAME,SAMPLE_TYPE, GENDER,PERSONNEL_TYPE,BIRTH_DATE,NATIONALITY,DISTRICT,ID_CARD_NO,EDUCATION_LEVEL,IDENTITY,NATIVE_PLACE_ADDR,RESIDENCE_ADDR,SAMPLE_PACKAGING,SAMPLE_DESCRIPTION,REMARK,SAMPLE_LAB_NO,PERSONNEL_NO,EXAMINE_USER1,STORE_FLAG,STR_FLAG,LAB_ID,INIT_SERVER_NO,CREATE_USER,CREATE_DATETIME,RECEPTION_MAN_ID, RECEPTION_MAN, RECEPTION_REGIONALISM, RECEPTION_ORG_NAME,RECEPTION_TEL)");
            strSql.Append(" values (");
            strSql.Append(":ID,:CONSIGNMENT_ID,:PERSONNEL_NAME,:SAMPLE_TYPE,: GENDER,:PERSONNEL_TYPE,to_date(:BIRTH_DATE,'yyyy-mm-dd'),:NATIONALITY,:DISTRICT,:ID_CARD_NO,:EDUCATION_LEVEL,:IDENTITY,:NATIVE_PLACE_ADDR,:RESIDENCE_ADDR,:SAMPLE_PACKAGING,:SAMPLE_DESCRIPTION,:REMARK,:SAMPLE_LAB_NO,:PERSONNEL_NO,:EXAMINE_USER1,:STORE_FLAG,:STR_FLAG,:LAB_ID,:INIT_SERVER_NO,:CREATE_USER,sysdate,:RECEPTION_MAN_ID, :RECEPTION_MAN, :RECEPTION_REGIONALISM, :RECEPTION_ORG_NAME,:RECEPTION_TEL)");
            OracleParameter[] parameters = {
					new OracleParameter(":ID",Helper.GetDBValue(ID)),
                    new OracleParameter(":CONSIGNMENT_ID",Helper.GetDBValue(CONSIGNMENT_ID)),
                    new OracleParameter(":PERSONNEL_NAME",Helper.GetDBValue(PERSONNEL_NAME)),
                    new OracleParameter(":SAMPLE_TYPE", Helper.GetDBValue(SAMPLE_TYPE)),
                    new OracleParameter(":GENDER",Helper.GetDBValue(GENDER)),
                    new OracleParameter(":PERSONNEL_TYPE",Helper.GetDBValue(PERSONNEL_TYPE)),
                    new OracleParameter(":BIRTH_DATE", Helper.GetDBValue( BIRTH_DATE.Length>0?BIRTH_DATE.Split(' ')[0]:string.Empty)),
                    new OracleParameter(":NATIONALITY",Helper.GetDBValue( NATIONALITY)),
                    new OracleParameter(":DISTRICT", Helper.GetDBValue(DISTRICT)),
                    new OracleParameter(":ID_CARD_NO", Helper.GetDBValue(ID_CARD_NO)),
                    new OracleParameter(":EDUCATION_LEVEL",Helper.GetDBValue( EDUCATION_LEVEL)),
                    new OracleParameter(":IDENTITY", Helper.GetDBValue(IDENTITY)),
                    new OracleParameter(":NATIVE_PLACE_ADDR",Helper.GetDBValue( NATIVE_PLACE_ADDR)),
                    new OracleParameter(":RESIDENCE_ADDR", Helper.GetDBValue(RESIDENCE_ADDR)),
                    new OracleParameter(":SAMPLE_PACKAGING", Helper.GetDBValue(SAMPLE_PACKAGING)),
                    new OracleParameter(":SAMPLE_DESCRIPTION",Helper.GetDBValue( SAMPLE_DESCRIPTION)),
                    new OracleParameter(":REMARK", Helper.GetDBValue(REMARK)),
                    new OracleParameter(":SAMPLE_LAB_NO", Helper.GetDBValue(SAMPLE_LAB_NO)),
                    new OracleParameter(":PERSONNEL_NO", Helper.GetDBValue(PERSONNEL_NO)),
                    new OracleParameter(":EXAMINE_USER1",Helper.GetDBValue( EXAMINE_USER1)),
                    new OracleParameter(":STORE_FLAG", "0"),
                    new OracleParameter(":STR_FLAG", "0"),
                    new OracleParameter(":LAB_ID", DBHelperOracle.LAB_ID),
                    new OracleParameter(":INIT_SERVER_NO", DBHelperOracle.INIT_SERVER_NO),
                    new OracleParameter(":CREATE_USER", Helper.GetDBValue( EXAMINE_USER1)),
                    new OracleParameter(":RECEPTION_MAN_ID", Helper.GetDBValue(reception_man_id)),
                    new OracleParameter(":RECEPTION_MAN", Helper.GetDBValue(EXAMINE_USER1)),
                    new OracleParameter(":RECEPTION_REGIONALISM", Helper.GetDBValue(DBHelperOracle.RECEPTION_REGIONALISM)),
                    new OracleParameter(":RECEPTION_ORG_NAME", Helper.GetDBValue(DBHelperOracle.RECEPTION_ORG_NAME)),
                    new OracleParameter(":RECEPTION_TEL", Helper.GetDBValue(DBHelperOracle.RECEPTION_TEL))};
            DBHelperOracle.ExecuteSqlWithTransaction(strSql.ToString(), parameters, dbConnection, trans);
            LAW_CASEOracleDAL.InsertSample_Examination(Helper.GenerateID(), ID, reception_man_id,
                              EXAMINE_USER1, EXAMINE_USER1, "6".ToString(), dbConnection, trans);
        }
        public static void UpdateWithTransaction(string ID, string PERSONNEL_NAME, string SAMPLE_TYPE, string GENDER,
              string PERSONNEL_TYPE, string BIRTH_DATE, string NATIONALITY, string DISTRICT, string ID_CARD_NO,
              string EDUCATION_LEVEL, string IDENTITY, string NATIVE_PLACE_ADDR, string RESIDENCE_ADDR, string SAMPLE_PACKAGING,
              string SAMPLE_DESCRIPTION, string REMARK, string SAMPLE_LAB_NO,
              string userName, OracleConnection dbConnection, OracleTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GDNA.MISSING_PERSON set ");
            strSql.Append("PERSONNEL_NAME=:PERSONNEL_NAME,");
            strSql.Append("SAMPLE_TYPE=:SAMPLE_TYPE,");
            strSql.Append("GENDER=:GENDER,");
            strSql.Append("PERSONNEL_TYPE=:PERSONNEL_TYPE,");
            strSql.Append("BIRTH_DATE=to_date(:BIRTH_DATE,'yyyy-mm-dd'),");
            strSql.Append("NATIONALITY=:NATIONALITY,");
            strSql.Append("DISTRICT=:DISTRICT,");
            strSql.Append("ID_CARD_NO=:ID_CARD_NO,");
            strSql.Append("EDUCATION_LEVEL=:EDUCATION_LEVEL,");
            strSql.Append("IDENTITY=:IDENTITY,");
            strSql.Append("NATIVE_PLACE_ADDR=:NATIVE_PLACE_ADDR,");
            strSql.Append("RESIDENCE_ADDR=:RESIDENCE_ADDR,");
            strSql.Append("SAMPLE_PACKAGING=:SAMPLE_PACKAGING,");
            strSql.Append("SAMPLE_DESCRIPTION=:SAMPLE_DESCRIPTION,");
            strSql.Append("REMARK=:REMARK, ");
            strSql.Append("SAMPLE_LAB_NO=:SAMPLE_LAB_NO, ");
            strSql.Append("UPDATE_USER=:UPDATE_USER, ");
            strSql.Append("UPDATE_DATETIME=sysdate ");
            strSql.Append(" where ID=:ID ");
            OracleParameter[] parameters = {
					new OracleParameter(":ID",Helper.GetDBValue(ID)),
                    new OracleParameter(":PERSONNEL_NAME",Helper.GetDBValue(PERSONNEL_NAME)),
                    new OracleParameter(":SAMPLE_TYPE", Helper.GetDBValue(SAMPLE_TYPE)),
                    new OracleParameter(":GENDER",Helper.GetDBValue(GENDER)),
                    new OracleParameter(":PERSONNEL_TYPE",Helper.GetDBValue(PERSONNEL_TYPE)),
                    new OracleParameter(":BIRTH_DATE", Helper.GetDBValue( BIRTH_DATE.Length>0?BIRTH_DATE.Split(' ')[0]:string.Empty)),
                    new OracleParameter(":NATIONALITY",Helper.GetDBValue( NATIONALITY)),
                    new OracleParameter(":DISTRICT", Helper.GetDBValue(DISTRICT)),
                    new OracleParameter(":ID_CARD_NO", Helper.GetDBValue(ID_CARD_NO)),
                    new OracleParameter(":EDUCATION_LEVEL",Helper.GetDBValue( EDUCATION_LEVEL)),
                    new OracleParameter(":IDENTITY", Helper.GetDBValue(IDENTITY)),
                    new OracleParameter(":NATIVE_PLACE_ADDR",Helper.GetDBValue( NATIVE_PLACE_ADDR)),
                    new OracleParameter(":RESIDENCE_ADDR", Helper.GetDBValue(RESIDENCE_ADDR)),
                    new OracleParameter(":SAMPLE_PACKAGING", Helper.GetDBValue(SAMPLE_PACKAGING)),
                    new OracleParameter(":SAMPLE_DESCRIPTION",Helper.GetDBValue( SAMPLE_DESCRIPTION)),
                    new OracleParameter(":REMARK", Helper.GetDBValue(REMARK)),
                    new OracleParameter(":SAMPLE_LAB_NO", Helper.GetDBValue(SAMPLE_LAB_NO)),
                    new OracleParameter(":UPDATE_USER",Helper.GetDBValue( userName))};

            DBHelperOracle.ExecuteSqlWithTransaction(strSql.ToString(), parameters, dbConnection, trans);
        }
    }
}
