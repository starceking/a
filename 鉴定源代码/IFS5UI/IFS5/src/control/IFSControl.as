package control
{	
	import com.adobe.cairngorm.control.FrontController;	
	
	public class IFSControl extends FrontController
	{
		public function IFSControl()
		{
			//DICTWS
			addCommand(DICTWS_GetXml,IFSCommand);	
			addCommand(DICTWS_InsertXml,IFSCommand);	
			addCommand(DICTWS_DeleteXml,IFSCommand);	
			addCommand(DICTWS_SaveDNATestItem,IFSCommand);
			//JUSTYPEWS
			addCommand(JUSTYPEWS_GetXml,IFSCommand);
			addCommand(JUSTYPEWS_InsertOffice,IFSCommand);
			addCommand(JUSTYPEWS_UpdateOffice,IFSCommand);
			addCommand(JUSTYPEWS_DeleteOffice,IFSCommand);
			addCommand(JUSTYPEWS_InsertType,IFSCommand);
			addCommand(JUSTYPEWS_UpdateType,IFSCommand);
			addCommand(JUSTYPEWS_DeleteType,IFSCommand);
			addCommand(JUSTYPEWS_UpdateAdmin,IFSCommand);
			addCommand(JUSTYPEWS_UpdateDna,IFSCommand);
			//IDCASEWS
			addCommand(IDCASEWS_Insert,IFSCommand);
			addCommand(IDCASEWS_Update,IFSCommand);
			addCommand(IDCASEWS_DeleteC,IFSCommand);
			addCommand(IDCASEWS_DeleteD,IFSCommand);
			addCommand(IDCASEWS_GetAllD,IFSCommand);
			addCommand(IDCASEWS_GetAcceptDup,IFSCommand);
			addCommand(IDCASEWS_UpdateBsInfo,IFSCommand);
			addCommand(IDCASEWS_GetOneRecord,IFSCommand);
			addCommand(IDCASEWS_ImportToOraCase,IFSCommand);
			addCommand(IDCASEWS_GetYearStaData,IFSCommand);
			addCommand(IDCASEWS_GetAllJds,IFSCommand);
			//PSBWS
			addCommand(PSBWS_GetAll,IFSCommand);
			addCommand(PSBWS_Insert,IFSCommand);
			addCommand(PSBWS_Update,IFSCommand);
			addCommand(PSBWS_Delete,IFSCommand);
			//REGIONCODEWS
			addCommand(REGIONCODEWS_GetAll,IFSCommand);
			//IDFLOWWS
			addCommand(IDFLOWWS_Cancel,IFSCommand);
			addCommand(IDFLOWWS_UpdateAccept,IFSCommand);
			addCommand(IDFLOWWS_UpdateAcceptMp,IFSCommand);
			addCommand(IDFLOWWS_UpdateAcceptMpr,IFSCommand);
			addCommand(IDFLOWWS_UpdateAcceptDna,IFSCommand);
			addCommand(IDFLOWWS_UpdateConclusion,IFSCommand);
			addCommand(IDFLOWWS_UpdateTesterFinish,IFSCommand);
			addCommand(IDFLOWWS_UpdateAudit,IFSCommand);
			addCommand(IDFLOWWS_UpdateRepGet,IFSCommand);
			addCommand(IDFLOWWS_GetNextConNo,IFSCommand);
			addCommand(IDFLOWWS_GetNextAccNo,IFSCommand);
			addCommand(IDFLOWWS_GetNextDocNo,IFSCommand);
			addCommand(IDFLOWWS_GetNextSLN,IFSCommand);
			addCommand(IDFLOWWS_GetCaseNextSLN,IFSCommand);
			addCommand(IDFLOWWS_QueryAllCase,IFSCommand);
			addCommand(IDFLOWWS_PrintTz,IFSCommand);
			addCommand(IDFLOWWS_PrintFWJL,IFSCommand);
			
			//SYSUSERWS
			addCommand(SYSUSERWS_Insert,IFSCommand);
			addCommand(SYSUSERWS_Update,IFSCommand);
			addCommand(SYSUSERWS_UpdateJBXX,IFSCommand);
			addCommand(SYSUSERWS_Login,IFSCommand);
			addCommand(SYSUSERWS_GetAll,IFSCommand);
			addCommand(SYSUSERWS_GetAllByJG,IFSCommand);
			addCommand(SYSUSERWS_GetDeletedPerson,IFSCommand);
			addCommand(SYSUSERWS_GetConsignPerson,IFSCommand);

			//IDTASKWS
			addCommand(IDTASKWS_GetCaseAcceptTask,IFSCommand);
			addCommand(IDTASKWS_GetAcceptByBarCode,IFSCommand);
			addCommand(IDTASKWS_GetTestTask,IFSCommand);
			addCommand(IDTASKWS_GetAuditTask,IFSCommand);
			addCommand(IDTASKWS_GetDocMakeTask,IFSCommand);
			addCommand(IDTASKWS_GetReportTask,IFSCommand);
			//EXCASEWS
			addCommand(EXCASEWS_GetExCaseList,IFSCommand);
			addCommand(EXCASEWS_ExeSql,IFSCommand);
			addCommand(EXCASEWS_SendShortNote,IFSCommand);
			addCommand(EXCASEWS_QueryGetReportCase,IFSCommand);
			addCommand(EXCASEWS_GetSpTaskAmount,IFSCommand);
			addCommand(EXCASEWS_GetYjrSp,IFSCommand);
			
			//STATISTICSWS
			addCommand(STATISTICSWS_PersonWork,IFSCommand);
			addCommand(STATISTICSWS_StationWork,IFSCommand);
			addCommand(STATISTICSWS_GetStation,IFSCommand);
			addCommand(STATISTICSWS_CaseProperty,IFSCommand);
			addCommand(STATISTICSWS_CaseConclusion,IFSCommand);

			//MRELATIVEWS
			addCommand(MRELATIVEWS_NewConsign,IFSCommand);
			addCommand(MRELATIVEWS_UpdateR,IFSCommand);
			addCommand(MRELATIVEWS_DeleteR,IFSCommand);
			addCommand(MRELATIVEWS_GetOneMpr,IFSCommand);
			addCommand(MRELATIVEWS_ImportToOraMpr,IFSCommand);
			//MISSINGPERSONWS
			addCommand(MISSINGPERSONWS_Insert,IFSCommand);
			addCommand(MISSINGPERSONWS_Update,IFSCommand);
			addCommand(MISSINGPERSONWS_Delete,IFSCommand);
			addCommand(MISSINGPERSONWS_GetOneMp,IFSCommand);
			addCommand(MISSINGPERSONWS_ImportToOraMp,IFSCommand);
			//IDTESTIMONYWS
			addCommand(IDTESTIMONYWS_Insert,IFSCommand);
			addCommand(IDTESTIMONYWS_Update,IFSCommand);
			addCommand(IDTESTIMONYWS_Delete,IFSCommand);
			addCommand(IDTESTIMONYWS_GetAll,IFSCommand);
			addCommand(IDTESTIMONYWS_TesOper,IFSCommand);
			addCommand(IDTESTIMONYWS_Query,IFSCommand);
			//IDTESTIMONY_CTRWS
			addCommand(IDTESTIMONY_CTRWS_Insert,IFSCommand);
			addCommand(IDTESTIMONY_CTRWS_Update,IFSCommand);
			addCommand(IDTESTIMONY_CTRWS_Delete,IFSCommand);
			addCommand(IDTESTIMONY_CTRWS_GetAll,IFSCommand);
			//IDPERSONWS
			addCommand(IDPERSONWS_Insert,IFSCommand);
			addCommand(IDPERSONWS_Update,IFSCommand);
			addCommand(IDPERSONWS_Delete,IFSCommand);
			addCommand(IDPERSONWS_GetAll,IFSCommand);
			//CASEFILEWS
			addCommand(CASEFILEWS_GetAll,IFSCommand);
			addCommand(CASEFILEWS_GetAllFileData,IFSCommand);
			addCommand(CASEFILEWS_Delete,IFSCommand);
			//WORDWS
			addCommand(WORDWS_GetCaseWordList,IFSCommand);
			addCommand(WORDWS_DeleteWord,IFSCommand);
			addCommand(WORDWS_GenerateWord,IFSCommand);
			addCommand(WORDWS_GetAllCaseWord,IFSCommand);
			addCommand(WORDWS_GenerateWzms,IFSCommand);
			addCommand(WORDWS_GetCaseWordManageList,IFSCommand);
			addCommand(WORDWS_PrintSampleTestRecord,IFSCommand);
			addCommand(WORDWS_PrintCaseTestRecord,IFSCommand);
			addCommand(WORDWS_PrintDNATestHelpFile,IFSCommand);
			
			//CASERELATIVE_SHRWS
			addCommand(CASERELATIVE_SHRWS_Insert,IFSCommand);
			addCommand(CASERELATIVE_SHRWS_InsertWithNo,IFSCommand);
			addCommand(CASERELATIVE_SHRWS_Update,IFSCommand);
			addCommand(CASERELATIVE_SHRWS_Delete,IFSCommand);
			addCommand(CASERELATIVE_SHRWS_GetAll,IFSCommand);
			//CASERELATIVE_XYRWS
			addCommand(CASERELATIVE_XYRWS_Insert,IFSCommand);
			addCommand(CASERELATIVE_XYRWS_InsertWithNo,IFSCommand);
			addCommand(CASERELATIVE_XYRWS_Update,IFSCommand);
			addCommand(CASERELATIVE_XYRWS_Delete,IFSCommand);
			addCommand(CASERELATIVE_XYRWS_GetAll,IFSCommand);
			//CASEPERSONNELSAMPLE_SHRWS
			addCommand(CASEPERSONNELSAMPLE_SHRWS_Insert,IFSCommand);
			addCommand(CASEPERSONNELSAMPLE_SHRWS_InsertWithNo,IFSCommand);
			addCommand(CASEPERSONNELSAMPLE_SHRWS_Update,IFSCommand);
			addCommand(CASEPERSONNELSAMPLE_SHRWS_Delete,IFSCommand);
			addCommand(CASEPERSONNELSAMPLE_SHRWS_GetAll,IFSCommand);
			//CASEPERSONNELSAMPLE_XYRWS
			addCommand(CASEPERSONNELSAMPLE_XYRWS_Insert,IFSCommand);
			addCommand(CASEPERSONNELSAMPLE_XYRWS_InsertWithNo,IFSCommand);
			addCommand(CASEPERSONNELSAMPLE_XYRWS_Update,IFSCommand);
			addCommand(CASEPERSONNELSAMPLE_XYRWS_Delete,IFSCommand);
			addCommand(CASEPERSONNELSAMPLE_XYRWS_GetAll,IFSCommand);
			//CASEPERSONNELSAMPLE_QTRWS
			addCommand(CASEPERSONNELSAMPLE_QTRWS_Insert,IFSCommand);
			addCommand(CASEPERSONNELSAMPLE_QTRWS_InsertWithNo,IFSCommand);
			addCommand(CASEPERSONNELSAMPLE_QTRWS_Update,IFSCommand);
			addCommand(CASEPERSONNELSAMPLE_QTRWS_Delete,IFSCommand);
			addCommand(CASEPERSONNELSAMPLE_QTRWS_GetAll,IFSCommand);
			//UNKNOWNDECEASEDWS
			addCommand(UNKNOWNDECEASEDWS_Insert,IFSCommand);
			addCommand(UNKNOWNDECEASEDWS_InsertWithNo,IFSCommand);
			addCommand(UNKNOWNDECEASEDWS_Update,IFSCommand);
			addCommand(UNKNOWNDECEASEDWS_Delete,IFSCommand);
			addCommand(UNKNOWNDECEASEDWS_GetAll,IFSCommand);
			//DNASEWS
			addCommand(DNASEWS_Insert,IFSCommand);
			addCommand(DNASEWS_InsertWithNo,IFSCommand);
			addCommand(DNASEWS_Update,IFSCommand);
			addCommand(DNASEWS_Delete,IFSCommand);
			addCommand(DNASEWS_GetAll,IFSCommand);
			//CODIESWS
			addCommand(CODIESWS_GetAll,IFSCommand);
			addCommand(CODIESWS_GetAllTmpStr,IFSCommand);
			addCommand(CODIESWS_deleteTmpStr,IFSCommand);
			addCommand(CODIESWS_deleteAllTmpStr,IFSCommand);
			addCommand(CODIESWS_UpdateStrFromTmp,IFSCommand);
			addCommand(CODIESWS_UpdateAllStrFromTmp,IFSCommand);
			addCommand(CODIESWS_GetAllStr,IFSCommand);
			addCommand(CODIESWS_UpdateStr,IFSCommand);
			addCommand(CODIESWS_PrintStrTable,IFSCommand);
			addCommand(CODIESWS_SameCaseBzAna,IFSCommand);
			addCommand(CODIESWS_Import,IFSCommand);
			addCommand(CODIESWS_QuerySample,IFSCommand);
			addCommand(CODIESWS_PrintCODISdat,IFSCommand);
			//IFAWS
			addCommand(IFAWS_ReadStr,IFSCommand);
			//DOCMODWS
			addCommand(DOCMODWS_Insert,IFSCommand);
			addCommand(DOCMODWS_Update,IFSCommand);
			addCommand(DOCMODWS_Delete,IFSCommand);
			addCommand(DOCMODWS_GetAll,IFSCommand);
			
			//PREEXAMWS
			addCommand(PREEXAMWS_InsertPreExam,IFSCommand);
			addCommand(PREEXAMWS_UpdateCasePre,IFSCommand);
			addCommand(PREEXAMWS_DeleteCasePre,IFSCommand);
			addCommand(PREEXAMWS_QueryCasePre,IFSCommand);
			addCommand(PREEXAMWS_QueryPreExam,IFSCommand);
			//CONFIRMWS
			addCommand(CONFIRMWS_InsertConfirm,IFSCommand);
			addCommand(CONFIRMWS_UpdateCaseConfirm,IFSCommand);
			addCommand(CONFIRMWS_DeleteCaseConfirm,IFSCommand);
			addCommand(CONFIRMWS_QueryCaseConfirm,IFSCommand);
			addCommand(CONFIRMWS_QueryConfirm,IFSCommand);
			//EXTRACTWS
			addCommand(EXTRACTWS_InsertExtract,IFSCommand);
			addCommand(EXTRACTWS_UpdateExtract,IFSCommand);
			addCommand(EXTRACTWS_InsertPure,IFSCommand);
			addCommand(EXTRACTWS_UpdatePure,IFSCommand);
			addCommand(EXTRACTWS_DeleteExtractRecord,IFSCommand);
			addCommand(EXTRACTWS_NoTest,IFSCommand);
			addCommand(EXTRACTWS_QueryExtract,IFSCommand);
			addCommand(EXTRACTWS_QueryPure,IFSCommand);
			addCommand(EXTRACTWS_QueryCaseExtract,IFSCommand);
			//AMPLIFYWS
			addCommand(AMPLIFYWS_QueryExtractRecord,IFSCommand);
			addCommand(AMPLIFYWS_GetSampleAmplify,IFSCommand);
			addCommand(AMPLIFYWS_JoinAmplify,IFSCommand);
			addCommand(AMPLIFYWS_InsertAmplify,IFSCommand);
			addCommand(AMPLIFYWS_UpdateAmplify,IFSCommand);
			addCommand(AMPLIFYWS_DeleteAmplifyRecord,IFSCommand);
			addCommand(AMPLIFYWS_QueryCaseAmplify,IFSCommand);
			//ELECTROPHORESISWS
			addCommand(ELECTROPHORESISWS_QueryAmplifyRecord,IFSCommand);
			addCommand(ELECTROPHORESISWS_GetSampleEP,IFSCommand);
			addCommand(ELECTROPHORESISWS_JoinEP,IFSCommand);
			addCommand(ELECTROPHORESISWS_InsertEP,IFSCommand);
			addCommand(ELECTROPHORESISWS_UpdateEP,IFSCommand);
			addCommand(ELECTROPHORESISWS_DeleteEPRecord,IFSCommand);
			addCommand(ELECTROPHORESISWS_QueryEPRecord,IFSCommand);
			addCommand(ELECTROPHORESISWS_GetSampleEPRecord,IFSCommand);
			addCommand(ELECTROPHORESISWS_QueryCaseEP,IFSCommand);	
			
			//NOTIFICATIONWS
			addCommand(NOTIFICATIONWS_INSERT,IFSCommand);
			addCommand(NOTIFICATIONWS_UPDATE,IFSCommand);
			addCommand(NOTIFICATIONWS_DELETE,IFSCommand);
			addCommand(NOTIFICATIONWS_GET_ALL,IFSCommand);
			addCommand(NOTIFICATIONWS_GetImportant,IFSCommand);
			
			addCommand(EXCASEWS_GetTaskRemind,IFSCommand);
			
			//Supplies 
			addCommand(SUPPLIES_GetAll,IFSCommand);
			addCommand(SUPPLIES_Insert,IFSCommand);
			addCommand(SUPPLIES_Update,IFSCommand);
			addCommand(SUPPLIES_Delete,IFSCommand);
			addCommand(SUPPLIES_GetOne,IFSCommand);
			addCommand(SUPPLIES_MackAllHC,IFSCommand);
			//SuppliesUse
			addCommand(SUPPLIESUSE_GetAll,IFSCommand);
			addCommand(SUPPLIESUSE_Insert,IFSCommand);
			addCommand(SUPPLIESUSE_Update,IFSCommand);
			addCommand(SUPPLIESUSE_Delete,IFSCommand);
			addCommand(SUPPLIESUSE_GetOne,IFSCommand);
			addCommand(SUPPLIESUSE_MackAllHCSY,IFSCommand);
			//Supplies verification
			addCommand(SUPPLIESVERIFICATION_GetAll,IFSCommand);
			addCommand(SUPPLIESVERIFICATION_Insert,IFSCommand);
			addCommand(SUPPLIESVERIFICATION_Update,IFSCommand);
			addCommand(SUPPLIESVERIFICATION_Delete,IFSCommand);
			addCommand(SUPPLIESVERIFICATION_GetOne,IFSCommand);
			addCommand(SUPPLIESVERIFICATION_MackAllHCHC,IFSCommand);
			//Supplies procurement
			addCommand(SUPPLIESPROCUREMENT_GetAll,IFSCommand);
			addCommand(SUPPLIESPROCUREMENT_Insert,IFSCommand);
			addCommand(SUPPLIESPROCUREMENT_Update,IFSCommand);
			addCommand(SUPPLIESPROCUREMENT_Delete,IFSCommand);
			addCommand(SUPPLIESPROCUREMENT_GetOne,IFSCommand);
			addCommand(SUPPLIESPROCUREMENT_MackAllHCCG,IFSCommand);

			//Equipment
			addCommand(EQUIPMENT_GetAll,IFSCommand);
			addCommand(EQUIPMENT_GetSBAll,IFSCommand);
			addCommand(EQUIPMENT_Insert,IFSCommand);
			addCommand(EQUIPMENT_Update,IFSCommand);
			addCommand(EQUIPMENT_Delete,IFSCommand);
			addCommand(EQUIPMENT_GetOne,IFSCommand);
			addCommand(EQUIPMENT_MackAllFB,IFSCommand);
			//Equipment Repair
			addCommand(EQUIPMENTREPAIR_GetAll,IFSCommand);
			addCommand(EQUIPMENTREPAIR_Insert,IFSCommand);
			addCommand(EQUIPMENTREPAIR_Update,IFSCommand);
			addCommand(EQUIPMENTREPAIR_Delete,IFSCommand);
			addCommand(EQUIPMENTREPAIR_GetOne,IFSCommand);
			addCommand(EQUIPMENTREPAIR_MackAllFB,IFSCommand);
			//Equipment Check
			addCommand(EQUIPMENTCHECK_GetAll,IFSCommand);
			addCommand(EQUIPMENTCHECK_Insert,IFSCommand);
			addCommand(EQUIPMENTCHECK_Update,IFSCommand);
			addCommand(EQUIPMENTCHECK_Delete,IFSCommand);
			addCommand(EQUIPMENTCHECK_GetOne,IFSCommand);
			addCommand(EQUIPMENTCHECK_MackAllFB,IFSCommand);
			//Equipment Inspection
			addCommand(EQUIPMENTINSPECTION_GetAll,IFSCommand);
			addCommand(EQUIPMENTINSPECTION_Insert,IFSCommand);
			addCommand(EQUIPMENTINSPECTION_Update,IFSCommand);
			addCommand(EQUIPMENTINSPECTION_Delete,IFSCommand);
			addCommand(EQUIPMENTINSPECTION_GetOne,IFSCommand);
			addCommand(EQUIPMENTINSPECTION_MackAllFB,IFSCommand);
			////Equipment Maintain
			addCommand(EQUIPMENTMAINTAIN_GetAll,IFSCommand);
			addCommand(EQUIPMENTMAINTAIN_Insert,IFSCommand);
			addCommand(EQUIPMENTMAINTAIN_Update,IFSCommand);
			addCommand(EQUIPMENTMAINTAIN_Delete,IFSCommand);
			addCommand(EQUIPMENTMAINTAIN_GetOne,IFSCommand);
			addCommand(EQUIPMENTMAINTAIN_MackAllFB,IFSCommand);
			//Equipment USE
			addCommand(EQUIPMENTUSE_GetAll,IFSCommand);
			addCommand(EQUIPMENTUSE_Insert,IFSCommand);
			addCommand(EQUIPMENTUSE_Update,IFSCommand);
			addCommand(EQUIPMENTUSE_Delete,IFSCommand);
			addCommand(EQUIPMENTUSE_GLSql,IFSCommand);
			addCommand(EQUIPMENTUSE_GetOne,IFSCommand);
			addCommand(EQUIPMENTUSE_MackAllFB,IFSCommand);
			
			//VITAE
			addCommand(VITAEWS_GetAll,IFSCommand);
			addCommand(VITAEWS_Insert,IFSCommand);
			addCommand(VITAEWS_Update,IFSCommand);
			addCommand(VITAEWS_Delete,IFSCommand);
			//WORK  
			addCommand(WORKWS_GetAll,IFSCommand);
			addCommand(WORKWS_Insert,IFSCommand);
			addCommand(WORKWS_Update,IFSCommand);
			addCommand(WORKWS_Delete,IFSCommand);
			//WRITING
			addCommand(WRITINGWS_GetAll,IFSCommand);
			addCommand(WRITINGWS_Insert,IFSCommand);
			addCommand(WRITINGWS_Update,IFSCommand);
			addCommand(WRITINGWS_Delete,IFSCommand);		
			//FRUITFUL
			addCommand(FRUITFULWS_GetAll,IFSCommand);
			addCommand(FRUITFULWS_Insert,IFSCommand);
			addCommand(FRUITFULWS_Update,IFSCommand);
			addCommand(FRUITFULWS_Delete,IFSCommand);
			//TrainNote
			addCommand(TrainNote_GetAll,IFSCommand);
			addCommand(TrainNote_GetOne,IFSCommand);
			addCommand(TrainNote_Insert,IFSCommand);
			addCommand(TrainNote_InsertSS,IFSCommand);
			addCommand(TrainNote_Update,IFSCommand);
			addCommand(TrainNote_Delete,IFSCommand);
			addCommand(TrainNote_MackWord,IFSCommand);	
			//ServiceTrain
			addCommand(ServiceTrain_GetAll,IFSCommand);
			addCommand(ServiceTrain_Insert,IFSCommand);
			addCommand(ServiceTrain_Update,IFSCommand);
			addCommand(ServiceTrain_Delete,IFSCommand);
			addCommand(ServiceTrain_MackWord,IFSCommand);
			//AbiTest
			addCommand(AbiTest_GetAll,IFSCommand);
			addCommand(AbiTest_Insert,IFSCommand);
			addCommand(AbiTest_Update,IFSCommand);
			addCommand(AbiTest_Delete,IFSCommand);
			addCommand(AbiTest_MackWord,IFSCommand);
			
		}
		public static const EXCASEWS_GetTaskRemind:String="EXCASEWS_GetTaskRemind";
		
		//DICTWS
		public static const DICTWS_GetXml:String="DICTWS_GetXml";
		public static const DICTWS_InsertXml:String="DICTWS_InsertXml";
		public static const DICTWS_DeleteXml:String="DICTWS_DeleteXml";
		public static const DICTWS_SaveDNATestItem:String="DICTWS_SaveDNATestItem";
		//JUSTYPEWS
		public static const JUSTYPEWS_GetXml:String="JUSTYPEWS_GetXml";
		public static const JUSTYPEWS_InsertOffice:String="JUSTYPEWS_InsertOffice";
		public static const JUSTYPEWS_UpdateOffice:String="JUSTYPEWS_UpdateOffice";
		public static const JUSTYPEWS_DeleteOffice:String="JUSTYPEWS_DeleteOffice";
		public static const JUSTYPEWS_InsertType:String="JUSTYPEWS_InsertType";
		public static const JUSTYPEWS_UpdateType:String="JUSTYPEWS_UpdateType";
		public static const JUSTYPEWS_DeleteType:String="JUSTYPEWS_DeleteType";
		public static const JUSTYPEWS_UpdateAdmin:String="JUSTYPEWS_UpdateAdmin";
		public static const JUSTYPEWS_UpdateDna:String="JUSTYPEWS_UpdateDna";
		//IDCASEWS
		public static const IDCASEWS_Insert:String="IDCASEWS_Insert";
		public static const IDCASEWS_Update:String="IDCASEWS_Update";
		public static const IDCASEWS_DeleteC:String="IDCASEWS_DeleteC";
		public static const IDCASEWS_DeleteD:String="IDCASEWS_DeleteD";
		public static const IDCASEWS_GetAllD:String="IDCASEWS_GetAllD";
		public static const IDCASEWS_GetAcceptDup:String="IDCASEWS_GetAcceptDup";
		public static const IDCASEWS_UpdateBsInfo:String="IDCASEWS_UpdateBsInfo";
		public static const IDCASEWS_GetOneRecord:String="IDCASEWS_GetOneRecord";
		public static const IDCASEWS_ImportToOraCase:String="IDCASEWS_ImportToOraCase";
		public static const IDCASEWS_GetYearStaData:String="IDCASEWS_GetYearStaData";
		public static const IDCASEWS_GetAllJds:String="IDCASEWS_GetAllJds";
		//PSBWS
		public static const PSBWS_GetAll:String="PSBWS_GetAll";
		public static const PSBWS_Insert:String="PSBWS_Insert";
		public static const PSBWS_Update:String="PSBWS_Update";
		public static const PSBWS_Delete:String="PSBWS_Delete";
		//REGIONCODEWS
		public static const REGIONCODEWS_GetAll:String="REGIONCODEWS_GetAll";
		//IDFLOWWS
		public static const IDFLOWWS_Cancel:String="IDFLOWWS_Cancel";
		public static const IDFLOWWS_UpdateAccept:String="IDFLOWWS_UpdateAccept";
		public static const IDFLOWWS_UpdateAcceptMp:String="IDFLOWWS_UpdateAcceptMp";
		public static const IDFLOWWS_UpdateAcceptMpr:String="IDFLOWWS_UpdateAcceptMpr";
		public static const IDFLOWWS_UpdateAcceptDna:String="IDFLOWWS_UpdateAcceptDna";	
		public static const IDFLOWWS_UpdateConclusion:String="IDFLOWWS_UpdateConclusion";
		public static const IDFLOWWS_UpdateTesterFinish:String="IDFLOWWS_UpdateTesterFinish";
		public static const IDFLOWWS_UpdateAudit:String="IDFLOWWS_UpdateAudit";
		public static const IDFLOWWS_UpdateRepGet:String="IDFLOWWS_UpdateRepGet";
		public static const IDFLOWWS_GetNextConNo:String="IDFLOWWS_GetNextConNo";
		public static const IDFLOWWS_GetNextAccNo:String="IDFLOWWS_GetNextAccNo";
		public static const IDFLOWWS_GetNextDocNo:String="IDFLOWWS_GetNextDocNo";
		public static const IDFLOWWS_GetNextSLN:String="IDFLOWWS_GetNextSLN";
		public static const IDFLOWWS_GetCaseNextSLN:String="IDFLOWWS_GetCaseNextSLN";
		public static const IDFLOWWS_QueryAllCase:String="IDFLOWWS_QueryAllCase";
		public static const IDFLOWWS_PrintTz:String="IDFLOWWS_PrintTz";
		public static const IDFLOWWS_PrintFWJL:String="IDFLOWWS_PrintFWJL";
		//SYSUSERWS
		public static const SYSUSERWS_Insert:String="SYSUSERWS_Insert";
		public static const SYSUSERWS_Update:String="SYSUSERWS_Update";
		public static const SYSUSERWS_UpdateJBXX:String="SYSUSERWS_UpdateJBXX"
		public static const SYSUSERWS_Login:String="SYSUSERWS_Login";
		public static const SYSUSERWS_GetAll:String="SYSUSERWS_GetAll";
		public static const SYSUSERWS_GetAllByJG:String="SYSUSERWS_GetAllByJG";
		public static const SYSUSERWS_GetDeletedPerson:String="SYSUSERWS_GetDeletedPerson";
		public static const SYSUSERWS_GetConsignPerson:String="SYSUSERWS_GetConsignPerson";

		//IDTASKWS
		public static const IDTASKWS_GetCaseAcceptTask:String="IDTASKWS_GetCaseAcceptTask";
		public static const IDTASKWS_GetAcceptByBarCode:String="IDTASKWS_GetAcceptByBarCode";
		public static const IDTASKWS_GetTestTask:String="IDTASKWS_GetTestTask";
		public static const IDTASKWS_GetAuditTask:String="IDTASKWS_GetAuditTask";
		public static const IDTASKWS_GetDocMakeTask:String="IDTASKWS_GetDocMakeTask";
		public static const IDTASKWS_GetReportTask:String="IDTASKWS_GetReportTask";
		//EXCASEWS
		public static const EXCASEWS_GetExCaseList:String="EXCASEWS_GetExCaseList";
		public static const EXCASEWS_ExeSql:String="EXCASEWS_ExeSql";
		public static const EXCASEWS_SendShortNote:String="EXCASEWS_SendShortNote";
		public static const EXCASEWS_QueryGetReportCase:String="EXCASEWS_QueryGetReportCase";
		public static const EXCASEWS_GetSpTaskAmount:String="EXCASEWS_GetSpTaskAmount";
		public static const EXCASEWS_GetYjrSp:String="EXCASEWS_GetYjrSp";

		//STATISTICSWS
		public static const STATISTICSWS_PersonWork:String="STATISTICSWS_PersonWork";
		public static const STATISTICSWS_StationWork:String="STATISTICSWS_StationWork";
		public static const STATISTICSWS_GetStation:String="STATISTICSWS_GetStation";
		public static const STATISTICSWS_CaseProperty:String="STATISTICSWS_CaseProperty";
		public static const STATISTICSWS_CaseConclusion:String="STATISTICSWS_CaseConclusion";

		//MRELATIVEWS
		public static const MRELATIVEWS_NewConsign:String="MRELATIVEWS_NewConsign";
		public static const MRELATIVEWS_UpdateR:String="MRELATIVEWS_UpdateR";
		public static const MRELATIVEWS_DeleteR:String="MRELATIVEWS_DeleteR";
		public static const MRELATIVEWS_GetOneMpr:String="MRELATIVEWS_GetOneMpr";
		public static const MRELATIVEWS_ImportToOraMpr:String="MRELATIVEWS_ImportToOraMpr";
		//MISSINGPERSONWS
		public static const MISSINGPERSONWS_Insert:String="MISSINGPERSONWS_Insert";
		public static const MISSINGPERSONWS_Update:String="MISSINGPERSONWS_Update";
		public static const MISSINGPERSONWS_Delete:String="MISSINGPERSONWS_Delete";
		public static const MISSINGPERSONWS_GetOneMp:String="MISSINGPERSONWS_GetOneMp";
		public static const MISSINGPERSONWS_ImportToOraMp:String="MISSINGPERSONWS_ImportToOraMp";
		//IDTESTIMONYWS
		public static const IDTESTIMONYWS_Insert:String="IDTESTIMONYWS_Insert";
		public static const IDTESTIMONYWS_Update:String="IDTESTIMONYWS_Update";
		public static const IDTESTIMONYWS_Delete:String="IDTESTIMONYWS_Delete";
		public static const IDTESTIMONYWS_GetAll:String="IDTESTIMONYWS_GetAll";
		public static const IDTESTIMONYWS_TesOper:String="IDTESTIMONYWS_TesOper";
		public static const IDTESTIMONYWS_Query:String="IDTESTIMONYWS_Query";
		//IDTESTIMONY_CTRWS
		public static const IDTESTIMONY_CTRWS_Insert:String="IDTESTIMONY_CTRWS_Insert";
		public static const IDTESTIMONY_CTRWS_Update:String="IDTESTIMONY_CTRWS_Update";
		public static const IDTESTIMONY_CTRWS_Delete:String="IDTESTIMONY_CTRWS_Delete";
		public static const IDTESTIMONY_CTRWS_GetAll:String="IDTESTIMONY_CTRWS_GetAll";
		//IDPERSONWS
		public static const IDPERSONWS_Insert:String="IDPERSONWS_Insert";
		public static const IDPERSONWS_Update:String="IDPERSONWS_Update";
		public static const IDPERSONWS_Delete:String="IDPERSONWS_Delete";
		public static const IDPERSONWS_GetAll:String="IDPERSONWS_GetAll";
		//CASEFILEWS
		public static const CASEFILEWS_GetAll:String="CASEFILEWS_GetAll";
		public static const CASEFILEWS_GetAllFileData:String="CASEFILEWS_GetAllFileData";
		public static const CASEFILEWS_Delete:String="CASEFILEWS_Delete";
		//WORDWS
		public static const WORDWS_GetCaseWordList:String="WORDWS_GetCaseWordList";
		public static const WORDWS_DeleteWord:String="WORDWS_DeleteWord";
		public static const WORDWS_GenerateWord:String="WORDWS_GenerateWord";
		public static const WORDWS_GetAllCaseWord:String="WORDWS_GetAllCaseWord";
		public static const WORDWS_GenerateWzms:String="WORDWS_GenerateWzms";
		public static const WORDWS_GetCaseWordManageList:String="WORDWS_GetCaseWordManageList";
		public static const WORDWS_PrintSampleTestRecord:String="WORDWS_PrintSampleTestRecord";
		public static const WORDWS_PrintCaseTestRecord:String="WORDWS_PrintCaseTestRecord";
		public static const WORDWS_PrintDNATestHelpFile:String="WORDWS_PrintDNATestHelpFile";
		
		//CASERELATIVE_SHRWS
		public static const CASERELATIVE_SHRWS_Insert:String="CASERELATIVE_SHRWS_Insert";
		public static const CASERELATIVE_SHRWS_InsertWithNo:String="CASERELATIVE_SHRWS_InsertWithNo";
		public static const CASERELATIVE_SHRWS_Update:String="CASERELATIVE_SHRWS_Update";
		public static const CASERELATIVE_SHRWS_Delete:String="CASERELATIVE_SHRWS_Delete";
		public static const CASERELATIVE_SHRWS_GetAll:String="CASERELATIVE_SHRWS_GetAll";
		//CASERELATIVE_XYRWS
		public static const CASERELATIVE_XYRWS_Insert:String="CASERELATIVE_XYRWS_Insert";
		public static const CASERELATIVE_XYRWS_InsertWithNo:String="CASERELATIVE_XYRWS_InsertWithNo";
		public static const CASERELATIVE_XYRWS_Update:String="CASERELATIVE_XYRWS_Update";
		public static const CASERELATIVE_XYRWS_Delete:String="CASERELATIVE_XYRWS_Delete";
		public static const CASERELATIVE_XYRWS_GetAll:String="CASERELATIVE_XYRWS_GetAll";
		//CASEPERSONNELSAMPLE_SHRWS
		public static const CASEPERSONNELSAMPLE_SHRWS_Insert:String="CASEPERSONNELSAMPLE_SHRWS_Insert";
		public static const CASEPERSONNELSAMPLE_SHRWS_InsertWithNo:String="CASEPERSONNELSAMPLE_SHRWS_InsertWithNo";
		public static const CASEPERSONNELSAMPLE_SHRWS_Update:String="CASEPERSONNELSAMPLE_SHRWS_Update";
		public static const CASEPERSONNELSAMPLE_SHRWS_Delete:String="CASEPERSONNELSAMPLE_SHRWS_Delete";
		public static const CASEPERSONNELSAMPLE_SHRWS_GetAll:String="CASEPERSONNELSAMPLE_SHRWS_GetAll";
		//CASEPERSONNELSAMPLE_XYRWS
		public static const CASEPERSONNELSAMPLE_XYRWS_Insert:String="CASEPERSONNELSAMPLE_XYRWS_Insert";
		public static const CASEPERSONNELSAMPLE_XYRWS_InsertWithNo:String="CASEPERSONNELSAMPLE_XYRWS_InsertWithNo";
		public static const CASEPERSONNELSAMPLE_XYRWS_Update:String="CASEPERSONNELSAMPLE_XYRWS_Update";
		public static const CASEPERSONNELSAMPLE_XYRWS_Delete:String="CASEPERSONNELSAMPLE_XYRWS_Delete";
		public static const CASEPERSONNELSAMPLE_XYRWS_GetAll:String="CASEPERSONNELSAMPLE_XYRWS_GetAll";
		//CASEPERSONNELSAMPLE_QTRWS
		public static const CASEPERSONNELSAMPLE_QTRWS_Insert:String="CASEPERSONNELSAMPLE_QTRWS_Insert";
		public static const CASEPERSONNELSAMPLE_QTRWS_InsertWithNo:String="CASEPERSONNELSAMPLE_QTRWS_InsertWithNo";
		public static const CASEPERSONNELSAMPLE_QTRWS_Update:String="CASEPERSONNELSAMPLE_QTRWS_Update";
		public static const CASEPERSONNELSAMPLE_QTRWS_Delete:String="CASEPERSONNELSAMPLE_QTRWS_Delete";
		public static const CASEPERSONNELSAMPLE_QTRWS_GetAll:String="CASEPERSONNELSAMPLE_QTRWS_GetAll";
		//UNKNOWNDECEASEDWS
		public static const UNKNOWNDECEASEDWS_Insert:String="UNKNOWNDECEASEDWS_Insert";
		public static const UNKNOWNDECEASEDWS_InsertWithNo:String="UNKNOWNDECEASEDWS_InsertWithNo";
		public static const UNKNOWNDECEASEDWS_Update:String="UNKNOWNDECEASEDWS_Update";
		public static const UNKNOWNDECEASEDWS_Delete:String="UNKNOWNDECEASEDWS_Delete";
		public static const UNKNOWNDECEASEDWS_GetAll:String="UNKNOWNDECEASEDWS_GetAll";
		//DNASEWS
		public static const DNASEWS_Insert:String="DNASEWS_Insert";
		public static const DNASEWS_InsertWithNo:String="DNASEWS_InsertWithNo";
		public static const DNASEWS_Update:String="DNASEWS_Update";
		public static const DNASEWS_Delete:String="DNASEWS_Delete";
		public static const DNASEWS_GetAll:String="DNASEWS_GetAll";
		//CODIESWS
		public static const CODIESWS_GetAll:String="CODIESWS_GetAll";
		public static const CODIESWS_GetAllTmpStr:String="CODIESWS_GetAllTmpStr";
		public static const CODIESWS_deleteTmpStr:String="CODIESWS_deleteTmpStr";
		public static const CODIESWS_deleteAllTmpStr:String="CODIESWS_deleteAllTmpStr";
		public static const CODIESWS_UpdateStrFromTmp:String="CODIESWS_UpdateStrFromTmp";
		public static const CODIESWS_UpdateAllStrFromTmp:String="CODIESWS_UpdateAllStrFromTmp";
		public static const CODIESWS_GetAllStr:String="CODIESWS_GetAllStr";
		public static const CODIESWS_UpdateStr:String="CODIESWS_UpdateStr";
		public static const CODIESWS_PrintStrTable:String="CODIESWS_PrintStrTable";
		public static const CODIESWS_SameCaseBzAna:String="CODIESWS_SameCaseBzAna";
		public static const CODIESWS_Import:String="CODIESWS_Import";
		public static const CODIESWS_QuerySample:String="CODIESWS_QuerySample";
		public static const CODIESWS_PrintCODISdat:String="CODIESWS_PrintCODISdat";
		//IFAWS
		public static const IFAWS_ReadStr:String="IFAWS_ReadStr";
		//DOCMODWS
		public static const DOCMODWS_Insert:String="DOCMODWS_Insert";
		public static const DOCMODWS_Update:String="DOCMODWS_Update";
		public static const DOCMODWS_Delete:String="DOCMODWS_Delete";
		public static const DOCMODWS_GetAll:String="DOCMODWS_GetAll";
		
		//PREEXAMWS
		public static const PREEXAMWS_InsertPreExam:String="PREEXAMWS_InsertPreExam";
		public static const PREEXAMWS_UpdateCasePre:String="PREEXAMWS_UpdateCasePre";
		public static const PREEXAMWS_DeleteCasePre:String="PREEXAMWS_DeleteCasePre";
		public static const PREEXAMWS_QueryCasePre:String="PREEXAMWS_QueryCasePre";
		public static const PREEXAMWS_QueryPreExam:String="PREEXAMWS_QueryPreExam";		
		//CONFIRMWS
		public static const CONFIRMWS_InsertConfirm:String="CONFIRMWS_InsertConfirm";
		public static const CONFIRMWS_UpdateCaseConfirm:String="CONFIRMWS_UpdateCaseConfirm";
		public static const CONFIRMWS_DeleteCaseConfirm:String="CONFIRMWS_DeleteCaseConfirm";
		public static const CONFIRMWS_QueryCaseConfirm:String="CONFIRMWS_QueryCaseConfirm";
		public static const CONFIRMWS_QueryConfirm:String="CONFIRMWS_QueryConfirm";
		//EXTRACTWS
		public static const EXTRACTWS_InsertExtract:String="EXTRACTWS_InsertExtract";
		public static const EXTRACTWS_UpdateExtract:String="EXTRACTWS_UpdateExtract";
		public static const EXTRACTWS_InsertPure:String="EXTRACTWS_InsertPure";
		public static const EXTRACTWS_UpdatePure:String="EXTRACTWS_UpdatePure";
		public static const EXTRACTWS_DeleteExtractRecord:String="EXTRACTWS_DeleteExtractRecord";
		public static const EXTRACTWS_NoTest:String="EXTRACTWS_NoTest";
		public static const EXTRACTWS_QueryExtract:String="EXTRACTWS_QueryExtract";
		public static const EXTRACTWS_QueryPure:String="EXTRACTWS_QueryPure";
		public static const EXTRACTWS_QueryCaseExtract:String="EXTRACTWS_QueryCaseExtract";
		//AMPLIFYWS
		public static const AMPLIFYWS_QueryExtractRecord:String="AMPLIFYWS_QueryExtractRecord";
		public static const AMPLIFYWS_GetSampleAmplify:String="AMPLIFYWS_GetSampleAmplify";
		public static const AMPLIFYWS_JoinAmplify:String="AMPLIFYWS_JoinAmplify";
		public static const AMPLIFYWS_InsertAmplify:String="AMPLIFYWS_InsertAmplify";
		public static const AMPLIFYWS_UpdateAmplify:String="AMPLIFYWS_UpdateAmplify";
		public static const AMPLIFYWS_DeleteAmplifyRecord:String="AMPLIFYWS_DeleteAmplifyRecord";	
		public static const AMPLIFYWS_QueryCaseAmplify:String="AMPLIFYWS_QueryCaseAmplify";
		//ELECTROPHORESISWS
		public static const ELECTROPHORESISWS_QueryAmplifyRecord:String="ELECTROPHORESISWS_QueryAmplifyRecord";
		public static const ELECTROPHORESISWS_GetSampleEP:String="ELECTROPHORESISWS_GetSampleEP";
		public static const ELECTROPHORESISWS_JoinEP:String="ELECTROPHORESISWS_JoinEP";
		public static const ELECTROPHORESISWS_InsertEP:String="ELECTROPHORESISWS_InsertEP";
		public static const ELECTROPHORESISWS_UpdateEP:String="ELECTROPHORESISWS_UpdateEP";
		public static const ELECTROPHORESISWS_DeleteEPRecord:String="ELECTROPHORESISWS_DeleteEPRecord";
		public static const ELECTROPHORESISWS_QueryEPRecord:String="ELECTROPHORESISWS_QueryEPRecord";
		public static const ELECTROPHORESISWS_GetSampleEPRecord:String="ELECTROPHORESISWS_GetSampleEPRecord";
		public static const ELECTROPHORESISWS_QueryCaseEP:String="ELECTROPHORESISWS_QueryCaseEP";
		
		//NOTIFICATIONWS
		public static const NOTIFICATIONWS_INSERT:String="NOTIFICATIONWS_INSERT";
		public static const NOTIFICATIONWS_UPDATE:String="NOTIFICATIONWS_UPDATE";
		public static const NOTIFICATIONWS_DELETE:String="NOTIFICATIONWS_DELETE";
		public static const NOTIFICATIONWS_GET_ALL:String="NOTIFICATIONWS_GET_ALL";
		public static const NOTIFICATIONWS_GetImportant:String="NOTIFICATIONWS_GetImportant";
		
		//Supplies 
		public static const SUPPLIES_GetAll:String="SUPPLIES_GetAll";
		public static const SUPPLIES_Insert:String="SUPPLIES_Insert";
		public static const SUPPLIES_Update:String="SUPPLIES_Update";
		public static const SUPPLIES_Delete:String="SUPPLIES_Delete";
		public static const SUPPLIES_GetOne:String="SUPPLIES_GetOne";
		public static const SUPPLIES_MackAllHC:String="SUPPLIES_MackAllHC";
		//SuppliesUse
		public static const SUPPLIESUSE_GetAll:String="SUPPLIESUSE_GetAll";
		public static const SUPPLIESUSE_Insert:String="SUPPLIESUSE_Insert";
		public static const SUPPLIESUSE_Update:String="SUPPLIESUSE_Update";
		public static const SUPPLIESUSE_Delete:String="SUPPLIESUSE_Delete";
		public static const SUPPLIESUSE_GetOne:String="SUPPLIESUSE_GetOne";
		public static const SUPPLIESUSE_MackAllHCSY:String="SUPPLIESUSE_MackAllHCSY";
		//Supplies verification
		public static const SUPPLIESVERIFICATION_GetAll:String="SUPPLIESVERIFICATION_GetAll";
		public static const SUPPLIESVERIFICATION_Insert:String="SUPPLIESVERIFICATION_Insert";
		public static const SUPPLIESVERIFICATION_Update:String="SUPPLIESVERIFICATION_Update";
		public static const SUPPLIESVERIFICATION_Delete:String="SUPPLIESVERIFICATION_Delete";
		public static const SUPPLIESVERIFICATION_GetOne:String="SUPPLIESVERIFICATION_GetOne";
		public static const SUPPLIESVERIFICATION_MackAllHCHC:String="SUPPLIESVERIFICATION_MackAllHCHC";
		//Supplies procurement
		public static const SUPPLIESPROCUREMENT_GetAll:String="SUPPLIESPROCUREMENT_GetAll";
		public static const SUPPLIESPROCUREMENT_Insert:String="SUPPLIESPROCUREMENT_Insert";
		public static const SUPPLIESPROCUREMENT_Update:String="SUPPLIESPROCUREMENT_Update";
		public static const SUPPLIESPROCUREMENT_Delete:String="SUPPLIESPROCUREMENT_Delete";
		public static const SUPPLIESPROCUREMENT_GetOne:String="SUPPLIESPROCUREMENT_GetOne";
		public static const SUPPLIESPROCUREMENT_MackAllHCCG:String="SUPPLIESPROCUREMENT_MackAllHCCG";
		
		
		//Equipment
		public static const  EQUIPMENT_GetSBAll:String="EQUIPMENT_GetSBAll";
		public static const  EQUIPMENT_GetAll:String="EQUIPMENT_GetAll";
		public static const  EQUIPMENT_Insert:String="EQUIPMENT_Insert";
		public static const  EQUIPMENT_Update:String="EQUIPMENT_Update";
		public static const  EQUIPMENT_Delete:String="EQUIPMENT_Delete";
		public static const  EQUIPMENT_GetOne:String="EQUIPMENT_GetOne";
		public static const  EQUIPMENT_MackAllFB:String="EQUIPMENT_MackAllFB";
		//Equipment Repair
		public static const  EQUIPMENTREPAIR_GetAll:String="EQUIPMENTREPAIR_GetAll";
		public static const  EQUIPMENTREPAIR_Insert:String="EQUIPMENTREPAIR_Insert";
		public static const  EQUIPMENTREPAIR_Update:String="EQUIPMENTREPAIR_Update";
		public static const  EQUIPMENTREPAIR_Delete:String="EQUIPMENTREPAIR_Delete";
		public static const  EQUIPMENTREPAIR_GetOne:String="EQUIPMENTREPAIR_GetOne";
		public static const  EQUIPMENTREPAIR_MackAllFB:String="EQUIPMENTREPAIR_MackAllFB";
		//Equipment Check
		public static const  EQUIPMENTCHECK_GetAll:String="EQUIPMENTCHECK_GetAll";
		public static const  EQUIPMENTCHECK_Insert:String="EQUIPMENTCHECK_Insert";
		public static const  EQUIPMENTCHECK_Update:String="EQUIPMENTCHECK_Update";
		public static const  EQUIPMENTCHECK_Delete:String="EQUIPMENTCHECK_Delete";
		public static const  EQUIPMENTCHECK_GetOne:String="EQUIPMENTCHECK_GetOne";
		public static const  EQUIPMENTCHECK_MackAllFB:String="EQUIPMENTCHECK_MackAllFB";
		//Equipment Inspection
		public static const  EQUIPMENTINSPECTION_GetAll:String="EQUIPMENTINSPECTION_GetAll";
		public static const  EQUIPMENTINSPECTION_Insert:String="EQUIPMENTINSPECTION_Insert";
		public static const  EQUIPMENTINSPECTION_Update:String="EQUIPMENTINSPECTION_Update";
		public static const  EQUIPMENTINSPECTION_Delete:String="EQUIPMENTINSPECTION_Delete";
		public static const  EQUIPMENTINSPECTION_GetOne:String="EQUIPMENTINSPECTION_GetOne";
		public static const  EQUIPMENTINSPECTION_MackAllFB:String="EQUIPMENTINSPECTION_MackAllFB";
		//Equipment Maintain
		public static const  EQUIPMENTMAINTAIN_GetAll:String="EQUIPMENTMAINTAIN_GetAll";
		public static const  EQUIPMENTMAINTAIN_Insert:String="EQUIPMENTMAINTAIN_Insert";
		public static const  EQUIPMENTMAINTAIN_Update:String="EQUIPMENTMAINTAIN_Update";
		public static const  EQUIPMENTMAINTAIN_Delete:String="EQUIPMENTMAINTAIN_Delete";
		public static const  EQUIPMENTMAINTAIN_GetOne:String="EQUIPMENTMAINTAIN_GetOne";
		public static const  EQUIPMENTMAINTAIN_MackAllFB:String="EQUIPMENTMAINTAIN_MackAllFB";
		//Equipment USE
		public static const  EQUIPMENTUSE_GetAll:String="EQUIPMENTUSE_GetAll";
		public static const  EQUIPMENTUSE_Insert:String="EQUIPMENTUSE_Insert";
		public static const  EQUIPMENTUSE_Update:String="EQUIPMENTUSE_Update";
		public static const  EQUIPMENTUSE_Delete:String="EQUIPMENTUSE_Delete";
		public static const  EQUIPMENTUSE_GLSql:String="EQUIPMENTUSE_GLSql";
		public static const  EQUIPMENTUSE_GetOne:String="EQUIPMENTUSE_GetOne";
		public static const  EQUIPMENTUSE_MackAllFB:String="EQUIPMENTUSE_MackAllFB";
		
		//VITAE
		public static const VITAEWS_GetAll:String="VITAEWS_GetAll";
		public static const VITAEWS_Insert:String="VITAEWS_Insert";
		public static const VITAEWS_Update:String="VITAEWS_Update";
		public static const VITAEWS_Delete:String="VITAEWS_Delete";
		//WORK
		public static const WORKWS_GetAll:String="WORKWS_GetAll";
		public static const WORKWS_Insert:String="WORKWS_Insert";
		public static const WORKWS_Update:String="WORKWS_Update";
		public static const WORKWS_Delete:String="WORKWS_Delete";
		//WRITING
		public static const WRITINGWS_GetAll:String="WRITINGWS_GetAll";
		public static const WRITINGWS_Insert:String="WRITINGWS_Insert";
		public static const WRITINGWS_Update:String="WRITINGWS_Update";
		public static const WRITINGWS_Delete:String="WRITINGWS_Delete";
		//FRUITFUL
		public static const FRUITFULWS_GetAll:String="FRUITFULWS_GetAll";
		public static const FRUITFULWS_Insert:String="FRUITFULWS_Insert";
		public static const FRUITFULWS_Update:String="FRUITFULWS_Update";
		public static const FRUITFULWS_Delete:String="FRUITFULWS_Delete";
		//TrainNote
		public static const  TrainNote_GetAll:String="TrainNote_GetAll";
		public static const  TrainNote_GetOne:String="TrainNote_GetOne";
		public static const  TrainNote_Insert:String="TrainNote_Insert";
		public static const  TrainNote_InsertSS:String="TrainNote_InsertSS";
		public static const  TrainNote_Update:String="TrainNote_Update";
		public static const  TrainNote_Delete:String="TrainNote_Delete";
		public static const  TrainNote_MackWord:String="TrainNote_MackWord";
		//ServiceTrain
		public static const  ServiceTrain_GetAll:String="ServiceTrain_GetAll";
		public static const  ServiceTrain_Insert:String="ServiceTrain_Insert";
		public static const  ServiceTrain_Update:String="ServiceTrain_Update";
		public static const  ServiceTrain_Delete:String="ServiceTrain_Delete";
		public static const  ServiceTrain_MackWord:String="ServiceTrain_MackWord";
		//AbiTest
		public static const  AbiTest_GetAll:String="AbiTest_GetAll";
		public static const  AbiTest_Insert:String="AbiTest_Insert";
		public static const  AbiTest_Update:String="AbiTest_Update";
		public static const  AbiTest_Delete:String="AbiTest_Delete";
		public static const  AbiTest_MackWord:String="AbiTest_MackWord";

	}
}