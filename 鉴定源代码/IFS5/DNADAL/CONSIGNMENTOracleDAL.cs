using System;
using System.Collections.Generic;

using System.Text;

using System.Data.OracleClient;
using LIB;
using DNADAL;

namespace IFSOracleDAL
{
    public class CONSIGNMENTOracleDAL
    {
        public static void AddWithTransaction(string ID, string AGENCY_REGIONALISM, string CONSIGNMENT_NO, string AGENCY_NAME, string AGENCY_PHONE,
            string CONSIGN_DATE, string CONSIGNER_NAME, string CONSIGNER_CERTIFICATE_NO, string CONSIGNER_ADDRESS,
            string userName, OracleConnection dbConnection, OracleTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into gdna.CONSIGNMENT(");
            strSql.Append("ID,AGENCY_REGIONALISM,CONSIGNMENT_NO,AGENCY_NAME,AGENCY_PHONE,CONSIGN_DATE,CONSIGNER_NAME,CONSIGNER_CERTIFICATE_NO,CONSIGNER_ADDRESS,LAB_ID,INIT_SERVER_NO,CREATE_USER,CREATE_DATETIME)");
            strSql.Append(" values (");
            strSql.Append(":ID,:AGENCY_REGIONALISM,:CONSIGNMENT_NO,:AGENCY_NAME,:AGENCY_PHONE,to_date(:CONSIGN_DATE,'yyyy-mm-dd'),:CONSIGNER_NAME,:CONSIGNER_CERTIFICATE_NO,:CONSIGNER_ADDRESS,:LAB_ID,:INIT_SERVER_NO,:CREATE_USER,sysdate)");
            OracleParameter[] parameters = {
					new OracleParameter(":ID",Helper.GetDBValue(ID)),
					new OracleParameter(":AGENCY_REGIONALISM",Helper.GetDBValue(AGENCY_REGIONALISM)),
                    new OracleParameter(":CONSIGNMENT_NO",Helper.GetDBValue(CONSIGNMENT_NO)),
                    new OracleParameter(":AGENCY_NAME",Helper.GetDBValue(AGENCY_NAME)),
                    new OracleParameter(":AGENCY_PHONE",Helper.GetDBValue(AGENCY_PHONE)),
                    new OracleParameter(":CONSIGN_DATE",Helper.GetDBValue(CONSIGN_DATE.Split(' ')[0])),
                    new OracleParameter(":CONSIGNER_NAME", Helper.GetDBValue( CONSIGNER_NAME)),
                    new OracleParameter(":CONSIGNER_CERTIFICATE_NO",Helper.GetDBValue( CONSIGNER_CERTIFICATE_NO)),
                    new OracleParameter(":CONSIGNER_ADDRESS",Helper.GetDBValue( CONSIGNER_ADDRESS)),
                    new OracleParameter(":LAB_ID", DBHelperOracle.LAB_ID),
                    new OracleParameter(":INIT_SERVER_NO", DBHelperOracle.INIT_SERVER_NO),
                    new OracleParameter(":CREATE_USER", Helper.GetDBValue(userName))};
            DBHelperOracle.ExecuteSqlWithTransaction(strSql.ToString(), parameters, dbConnection, trans);
        }
    }
}
