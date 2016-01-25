package control
{	
	import com.adobe.cairngorm.control.CairngormEventDispatcher;
	
	import control.*;
	import business.*;
	
	import com.adobe.cairngorm.business.Responder;
	import com.adobe.cairngorm.commands.Command;
	import com.adobe.cairngorm.control.CairngormEvent;
	
	import locator.*;
	
	import mx.rpc.events.ResultEvent;
	
	import util.Helper;
	
	public class IFSCommand implements Command,Responder
	{		
		public var evtType:String;
		public function onResult(evt:*=null):void
		{
			switch(evtType)
			{
				case IFSControl.EXCASEWS_GetTaskRemind:
					ExCaseLocator.getInstance().getTaskRemind(evt.toString());break;
				//DICTWS
				case IFSControl.DICTWS_GetXml:
					DictLocator.getInstance().getXml(evt as XML) ;break;
				case IFSControl.DICTWS_InsertXml:
					DictLocator.getInstance().insertItem() ;break;
				case IFSControl.DICTWS_DeleteXml:
					DictLocator.getInstance().deleteItem() ;break;
				case IFSControl.DICTWS_SaveDNATestItem:
					DictLocator.getInstance().SaveDNATestItem() ;break;
				//JUSTYPEWS
				case IFSControl.JUSTYPEWS_GetXml:
					JusTypeLocator.getInstance().getXml(evt as XML) ;break;
				case IFSControl.JUSTYPEWS_InsertOffice:
					Helper.showAlert("新增成功！请刷新页面") ;break;
				case IFSControl.JUSTYPEWS_UpdateOffice:
					Helper.showAlert("修改成功！请刷新页面") ;break;
				case IFSControl.JUSTYPEWS_DeleteOffice:
					Helper.showAlert("删除成功！请刷新页面") ;break;
				case IFSControl.JUSTYPEWS_InsertType:
					Helper.showAlert("新增成功！请刷新页面") ;break;
				case IFSControl.JUSTYPEWS_UpdateType:
					Helper.showAlert("修改成功！请刷新页面") ;break;
				case IFSControl.JUSTYPEWS_DeleteType:
					Helper.showAlert("删除成功！请刷新页面") ;break;
				case IFSControl.JUSTYPEWS_UpdateAdmin:
					Helper.showAlert("保存成功！请刷新页面") ;break;
				case IFSControl.JUSTYPEWS_UpdateDna:
					CairngormEventDispatcher.getInstance().dispatchEvent(new IFSEvent(IFSControl.JUSTYPEWS_UpdateAdmin));
					break;
				//IDCASEWS
				case IFSControl.IDCASEWS_Insert:
				case IFSControl.MRELATIVEWS_NewConsign:
				case IFSControl.MISSINGPERSONWS_Insert:
					IdFlowLocator.getInstance().insert(evt as String);break;
				case IFSControl.IDCASEWS_DeleteC:
				case IFSControl.IDCASEWS_DeleteD:
				case IFSControl.MRELATIVEWS_DeleteR:
				case IFSControl.MISSINGPERSONWS_Delete:
					IdFlowLocator.getInstance().deleteFunc();break;
				case IFSControl.IDCASEWS_Update:
					IdFlowLocator.getInstance().updateCase();break;
				case IFSControl.IDCASEWS_GetAllD:
				case IFSControl.IDCASEWS_GetAcceptDup:
					IdFlowLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.IDCASEWS_UpdateBsInfo:
					IdCaseLocator.getInstance().updateBsInfo();break;
				case IFSControl.IDCASEWS_GetOneRecord:
					IdCaseLocator.getInstance().getOneRecord(evt as XML);break; 
				case IFSControl.IDCASEWS_ImportToOraCase:
					IdCaseLocator.getInstance().impToOra();break; 
				case IFSControl.IDCASEWS_GetYearStaData:
					IdCaseLocator.getInstance().getYearStaData(evt as XML);break; 
				case IFSControl.IDCASEWS_GetAllJds:
					IdCaseLocator.getInstance().getAllJds(evt as XML);break; 
				//PSBWS
				case IFSControl.PSBWS_GetAll:
					PsbLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.PSBWS_Insert:
					PsbLocator.getInstance().insert();break;
				case IFSControl.PSBWS_Update:
					PsbLocator.getInstance().update();break;
				case IFSControl.PSBWS_Delete:
					PsbLocator.getInstance().deleteFunc();break;
				//REGIONWS
				case IFSControl.REGIONCODEWS_GetAll:
					RegionCodeLocator.getInstance().getAll(evt as XML);break;
				//IDFLOWWS
				case IFSControl.IDFLOWWS_Cancel:
					IdFlowLocator.getInstance().cancelFunc();break;
				case IFSControl.IDFLOWWS_UpdateAccept:
				case IFSControl.IDFLOWWS_UpdateAcceptDna:
				case IFSControl.IDFLOWWS_UpdateAcceptMp:
				case IFSControl.IDFLOWWS_UpdateAcceptMpr:
					IdFlowLocator.getInstance().updateAccept();break;
				case IFSControl.IDFLOWWS_UpdateConclusion:
					IdFlowLocator.getInstance().updateConclusion();break;
				case IFSControl.IDFLOWWS_UpdateTesterFinish:
					IdFlowLocator.getInstance().updateTesterFinish();break;
				case IFSControl.IDFLOWWS_UpdateAudit:
					IdFlowLocator.getInstance().updateAudit();break;
				case IFSControl.IDFLOWWS_UpdateRepGet:
					IdFlowLocator.getInstance().updateRepGet();break;
				case IFSControl.IDFLOWWS_GetNextConNo:
				case IFSControl.IDFLOWWS_GetNextAccNo:
				case IFSControl.IDFLOWWS_GetNextDocNo:
					IdFlowLocator.getInstance().getNextNo(evt as String);break;
				case IFSControl.IDFLOWWS_GetNextSLN:
					IdFlowLocator.getInstance().getNextSLN(evt as String);break;
				case IFSControl.IDFLOWWS_GetCaseNextSLN:
					IdFlowLocator.getInstance().getCaseNextSLN(evt as String);break;
				case IFSControl.IDFLOWWS_QueryAllCase:
					IdFlowLocator.getInstance().getAll(evt as XML);break;
				//SYSUSERWS_
				case IFSControl.SYSUSERWS_Insert:
					SysUserLocator.getInstance().insert();break;
				case IFSControl.SYSUSERWS_Update:
					SysUserLocator.getInstance().update();break;
				case IFSControl.SYSUSERWS_UpdateJBXX:
					SysUserLocator.getInstance().updatejbxx();break;
				case IFSControl.SYSUSERWS_Login:
					SysUserLocator.getInstance().login(evt as XML);break;
				case IFSControl.SYSUSERWS_GetAll:
					SysUserLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.SYSUSERWS_GetDeletedPerson:
				case IFSControl.SYSUSERWS_GetConsignPerson:
					SysUserLocator.getInstance().getOtherPerson(evt as XML);break;

				//IDTASKWS
				case IFSControl.IDTASKWS_GetCaseAcceptTask:
				case IFSControl.IDTASKWS_GetTestTask:
				case IFSControl.IDTASKWS_GetAuditTask:
				case IFSControl.IDTASKWS_GetDocMakeTask:
				case IFSControl.IDTASKWS_GetReportTask:
					IdFlowLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.IDTASKWS_GetAcceptByBarCode:
					IdFlowLocator.getInstance().getAcceptByBarCode(evt as XML);break;
				//EXCASEWS
				case IFSControl.EXCASEWS_GetExCaseList:
					ExCaseLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.EXCASEWS_QueryGetReportCase:
					ExCaseLocator.getInstance().queryGetReportCase(evt as XML);break;
				case IFSControl.EXCASEWS_GetSpTaskAmount:
					ExCaseLocator.getInstance().getSpTaskAmount(evt as XML);break;
				case IFSControl.EXCASEWS_GetYjrSp:
					ExCaseLocator.getInstance().getYjrSp(evt as XML);break;

				//STATISTICSWS
				case IFSControl.STATISTICSWS_PersonWork:
					StatisticsLocator.getInstance().PersonWork(evt as XML);break;
				case IFSControl.STATISTICSWS_StationWork:
					StatisticsLocator.getInstance().StationWork(evt as XML);break;
				case IFSControl.STATISTICSWS_GetStation:
					StatisticsLocator.getInstance().GetStation(evt as XML);break;
				case IFSControl.STATISTICSWS_CaseProperty:
					StatisticsLocator.getInstance().CaseProperty(evt as XML);break;
				case IFSControl.STATISTICSWS_CaseConclusion:
					StatisticsLocator.getInstance().CaseConclusion(evt as XML);break;

				//MRELATIVEWS
				case IFSControl.MRELATIVEWS_UpdateR:
					MRelativeLocator.getInstance().update();break;
				case IFSControl.MRELATIVEWS_GetOneMpr:
					MRelativeLocator.getInstance().getOne(evt as XML);break;
				case IFSControl.MRELATIVEWS_ImportToOraMpr:
					MRelativeLocator.getInstance().impToOra();break; 
				//MISSINGPERSONWS
				case IFSControl.MISSINGPERSONWS_Update:
					MissingPersonLocator.getInstance().update();break;
				case IFSControl.MISSINGPERSONWS_GetOneMp:
					MissingPersonLocator.getInstance().getOne(evt as XML);break;
				case IFSControl.MISSINGPERSONWS_ImportToOraMp:
					MissingPersonLocator.getInstance().impToOra();break; 
				//IDTESTIMONYWS
				case IFSControl.IDTESTIMONYWS_Insert:
					IdTestimonyLocator.getInstance().insert();break;
				case IFSControl.IDTESTIMONYWS_Update:
					IdTestimonyLocator.getInstance().update();break;
				case IFSControl.IDTESTIMONYWS_Delete:
					IdTestimonyLocator.getInstance().deleteFunc();break;
				case IFSControl.IDTESTIMONYWS_GetAll:
					IdTestimonyLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.IDTESTIMONYWS_TesOper:
					IdTestimonyLocator.getInstance().tesOper();break;
				case IFSControl.IDTESTIMONYWS_Query:
					IdTestimonyLocator.getInstance().query(evt as XML);break;
				//IDTESTIMONY_CTRWS
				case IFSControl.IDTESTIMONY_CTRWS_Insert:
					IdCtrLocator.getInstance().insert();break;
				case IFSControl.IDTESTIMONY_CTRWS_Update:
					IdCtrLocator.getInstance().update();break;
				case IFSControl.IDTESTIMONY_CTRWS_Delete:
					IdCtrLocator.getInstance().deleteFunc();break;
				case IFSControl.IDTESTIMONY_CTRWS_GetAll:
					IdCtrLocator.getInstance().getAll(evt as XML);break;
				//IDPERSONWS
				case IFSControl.IDPERSONWS_Insert:
					IdPersonLocator.getInstance().insert();break;
				case IFSControl.IDPERSONWS_Update:
					IdPersonLocator.getInstance().update();break;
				case IFSControl.IDPERSONWS_Delete:
					IdPersonLocator.getInstance().deleteFunc();break;
				case IFSControl.IDPERSONWS_GetAll:
					IdPersonLocator.getInstance().getAll(evt as XML);break;
				//CASEFILEWS
				case IFSControl.CASEFILEWS_Delete:
					CaseFileLocator.getInstance().deleteFunc();break;
				case IFSControl.CASEFILEWS_GetAll:
					CaseFileLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.CASEFILEWS_GetAllFileData:
					CaseFileLocator.getInstance().getAllFileData(evt as XML);break;	
				//WORDWS
				case IFSControl.WORDWS_DeleteWord:
					WordLocator.getInstance().deleteWord();break;
				case IFSControl.WORDWS_GetCaseWordList:
					WordLocator.getInstance().getCaseWordList(evt as XML);break;
				case IFSControl.WORDWS_GetAllCaseWord:
					WordLocator.getInstance().getAllCaseWord(evt as XML);break;
				case IFSControl.WORDWS_GetCaseWordManageList:
					WordLocator.getInstance().getCaseWordManageList(evt as XML);break;
				//DNASEWS
				case IFSControl.DNASEWS_Insert:
					DnaSeLocator.getInstance().insert();break;
				case IFSControl.DNASEWS_InsertWithNo:
					DnaSeLocator.getInstance().insertWithNo(evt as String);break;
				case IFSControl.DNASEWS_Update:
					DnaSeLocator.getInstance().update();break;
				case IFSControl.DNASEWS_Delete:
					DnaSeLocator.getInstance().deleteFunc();break;
				case IFSControl.DNASEWS_GetAll:
					DnaSeLocator.getInstance().getAll(evt as XML);break;
				//UNKNOWNDECEASEDWS
				case IFSControl.UNKNOWNDECEASEDWS_Insert:
					UnknownDeceasedLocator.getInstance().insert();break;
				case IFSControl.UNKNOWNDECEASEDWS_InsertWithNo:
					UnknownDeceasedLocator.getInstance().insertWithNo(evt as String);break;
				case IFSControl.UNKNOWNDECEASEDWS_Update:
					UnknownDeceasedLocator.getInstance().update();break;
				case IFSControl.UNKNOWNDECEASEDWS_Delete:
					UnknownDeceasedLocator.getInstance().deleteFunc();break;
				case IFSControl.UNKNOWNDECEASEDWS_GetAll:
					UnknownDeceasedLocator.getInstance().getAll(evt as XML);break;
				//CASEPERSONNELSAMPLE_SHRWS
				case IFSControl.CASEPERSONNELSAMPLE_SHRWS_Insert:
					CasePersonnelSampleShrLocator.getInstance().insert();break;
				case IFSControl.CASEPERSONNELSAMPLE_SHRWS_InsertWithNo:
					CasePersonnelSampleShrLocator.getInstance().insertWithNo(evt as String);break;
				case IFSControl.CASEPERSONNELSAMPLE_SHRWS_Update:
					CasePersonnelSampleShrLocator.getInstance().update();break;
				case IFSControl.CASEPERSONNELSAMPLE_SHRWS_Delete:
					CasePersonnelSampleShrLocator.getInstance().deleteFunc();break;
				case IFSControl.CASEPERSONNELSAMPLE_SHRWS_GetAll:
					CasePersonnelSampleShrLocator.getInstance().getAll(evt as XML);break;
				//CASEPERSONNELSAMPLE_XYRWS
				case IFSControl.CASEPERSONNELSAMPLE_XYRWS_Insert:
					CasePersonnelSampleXyrLocator.getInstance().insert();break;
				case IFSControl.CASEPERSONNELSAMPLE_XYRWS_InsertWithNo:
					CasePersonnelSampleXyrLocator.getInstance().insertWithNo(evt as String);break;
				case IFSControl.CASEPERSONNELSAMPLE_XYRWS_Update:
					CasePersonnelSampleXyrLocator.getInstance().update();break;
				case IFSControl.CASEPERSONNELSAMPLE_XYRWS_Delete:
					CasePersonnelSampleXyrLocator.getInstance().deleteFunc();break;
				case IFSControl.CASEPERSONNELSAMPLE_XYRWS_GetAll:
					CasePersonnelSampleXyrLocator.getInstance().getAll(evt as XML);break;
				//CASEPERSONNELSAMPLE_QTRWS
				case IFSControl.CASEPERSONNELSAMPLE_QTRWS_Insert:
					CasePersonnelSampleQtrLocator.getInstance().insert();break;
				case IFSControl.CASEPERSONNELSAMPLE_QTRWS_InsertWithNo:
					CasePersonnelSampleQtrLocator.getInstance().insertWithNo(evt as String);break;
				case IFSControl.CASEPERSONNELSAMPLE_QTRWS_Update:
					CasePersonnelSampleQtrLocator.getInstance().update();break;
				case IFSControl.CASEPERSONNELSAMPLE_QTRWS_Delete:
					CasePersonnelSampleQtrLocator.getInstance().deleteFunc();break;
				case IFSControl.CASEPERSONNELSAMPLE_QTRWS_GetAll:
					CasePersonnelSampleQtrLocator.getInstance().getAll(evt as XML);break;
				//CASERELATIVE_SHRWS
				case IFSControl.CASERELATIVE_SHRWS_Insert:
					CaseRelativeShrLocator.getInstance().insert();break;
				case IFSControl.CASERELATIVE_SHRWS_InsertWithNo:
					CaseRelativeShrLocator.getInstance().insertWithNo(evt as String);break;
				case IFSControl.CASERELATIVE_SHRWS_Update:
					CaseRelativeShrLocator.getInstance().update();break;
				case IFSControl.CASERELATIVE_SHRWS_Delete:
					CaseRelativeShrLocator.getInstance().deleteFunc();break;
				case IFSControl.CASERELATIVE_SHRWS_GetAll:
					CaseRelativeShrLocator.getInstance().getAll(evt as XML);break;
				//CASERELATIVE_XYRWS
				case IFSControl.CASERELATIVE_XYRWS_Insert:
					CaseRelativeXyrLocator.getInstance().insert();break;
				case IFSControl.CASERELATIVE_XYRWS_InsertWithNo:
					CaseRelativeXyrLocator.getInstance().insertWithNo(evt as String);break;
				case IFSControl.CASERELATIVE_XYRWS_Update:
					CaseRelativeXyrLocator.getInstance().update();break;
				case IFSControl.CASERELATIVE_XYRWS_Delete:
					CaseRelativeXyrLocator.getInstance().deleteFunc();break;
				case IFSControl.CASERELATIVE_XYRWS_GetAll:
					CaseRelativeXyrLocator.getInstance().getAll(evt as XML);break;
				//CODIESWS
				case IFSControl.CODIESWS_GetAll:
					CodiesLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.CODIESWS_GetAllTmpStr:
					CodiesLocator.getInstance().getAllTmpStr(evt as XML);break;
				case IFSControl.CODIESWS_deleteTmpStr:
					CodiesLocator.getInstance().deleteTmpStr();break;
				case IFSControl.CODIESWS_deleteAllTmpStr:
					CodiesLocator.getInstance().deleteAllTmpStr();break;
				case IFSControl.CODIESWS_UpdateStrFromTmp:
					CodiesLocator.getInstance().updateStrFromTmp();break;
				case IFSControl.CODIESWS_UpdateAllStrFromTmp:
					CodiesLocator.getInstance().updateAllStrFromTmp();break;
				case IFSControl.CODIESWS_GetAllStr:
					CodiesLocator.getInstance().getAllStr(evt as XML);break;
				case IFSControl.CODIESWS_UpdateStr:
					CodiesLocator.getInstance().updateStr();break;
				case IFSControl.CODIESWS_SameCaseBzAna:
					CodiesLocator.getInstance().sameCaseBzAna();break;
				case IFSControl.CODIESWS_Import:
					CodiesLocator.getInstance().importStr();break;
				case IFSControl.CODIESWS_QuerySample:
					CodiesLocator.getInstance().getAllStr(evt as XML);break;
				
				//IFAWS
				case IFSControl.IFAWS_ReadStr:
					IFALocator.getInstance().readStr();break;
				//DOCMODWS
				case IFSControl.DOCMODWS_Insert:
					DocModLocator.getInstance().insert();break;
				case IFSControl.DOCMODWS_Update:
					DocModLocator.getInstance().update();break;
				case IFSControl.DOCMODWS_Delete:
					DocModLocator.getInstance().deleteFunc();break;
				case IFSControl.DOCMODWS_GetAll:
					DocModLocator.getInstance().getAll(evt as XML);break;
				
				//PREEXAMWS
				case IFSControl.PREEXAMWS_InsertPreExam:
					PreExamLocator.getInstance().insertPreExam();break;
				case IFSControl.PREEXAMWS_UpdateCasePre:
					PreExamLocator.getInstance().updateCasePre();break;
				case IFSControl.PREEXAMWS_DeleteCasePre:
					PreExamLocator.getInstance().deleteCasePre();break;
				case IFSControl.PREEXAMWS_QueryCasePre:
					PreExamLocator.getInstance().queryCasePre(evt as XML);break;
				case IFSControl.PREEXAMWS_QueryPreExam:
					PreExamLocator.getInstance().queryPreExam(evt as XML);break;
				//CONFIRMWS
				case IFSControl.CONFIRMWS_InsertConfirm:
					ConfirmLocator.getInstance().insertConfirm();break;
				case IFSControl.CONFIRMWS_UpdateCaseConfirm:
					ConfirmLocator.getInstance().updateCaseConfirm();break;
				case IFSControl.CONFIRMWS_DeleteCaseConfirm:
					ConfirmLocator.getInstance().deleteCaseConfirm();break;
				case IFSControl.CONFIRMWS_QueryCaseConfirm:
					ConfirmLocator.getInstance().queryCaseConfirm(evt as XML);break;
				case IFSControl.CONFIRMWS_QueryConfirm:
					ConfirmLocator.getInstance().queryConfirm(evt as XML);break;
				//EXTRACTWS
				case IFSControl.EXTRACTWS_InsertExtract:
					ExtractLocator.getInstance().insertExtract();break;
				case IFSControl.EXTRACTWS_UpdateExtract:
					ExtractLocator.getInstance().updateExtract();break;
				case IFSControl.EXTRACTWS_InsertPure:
					ExtractLocator.getInstance().insertPure();break;
				case IFSControl.EXTRACTWS_UpdatePure:
					ExtractLocator.getInstance().updatePure();break;
				case IFSControl.EXTRACTWS_DeleteExtractRecord:
					ExtractLocator.getInstance().deleteExtractRecord();break;
				case IFSControl.EXTRACTWS_NoTest:
					ExtractLocator.getInstance().noTest();break;
				case IFSControl.EXTRACTWS_QueryExtract:
					ExtractLocator.getInstance().queryExtract(evt as XML);break;
				case IFSControl.EXTRACTWS_QueryPure:
					ExtractLocator.getInstance().queryPure(evt as XML);break;
				case IFSControl.EXTRACTWS_QueryCaseExtract:
					ExtractLocator.getInstance().queryCaseExtract(evt as XML);break;
				//AMPLIFYWS
				case IFSControl.AMPLIFYWS_QueryExtractRecord:
					AmplifyLocator.getInstance().queryExtractRecord(evt as XML);break;
				case IFSControl.AMPLIFYWS_GetSampleAmplify:
					AmplifyLocator.getInstance().getSampleAmplify(evt as XML);break;
				case IFSControl.AMPLIFYWS_JoinAmplify:
					AmplifyLocator.getInstance().joinAmplify(evt as XML);break;
				case IFSControl.AMPLIFYWS_InsertAmplify:
					AmplifyLocator.getInstance().insertAmplify();break;
				case IFSControl.AMPLIFYWS_UpdateAmplify:
					AmplifyLocator.getInstance().updateAmplify();break;
				case IFSControl.AMPLIFYWS_DeleteAmplifyRecord:
					AmplifyLocator.getInstance().deleteAmplifyRecord();break;
				case IFSControl.AMPLIFYWS_QueryCaseAmplify:
					AmplifyLocator.getInstance().queryCaseAmplify(evt as XML);break;
				//ELECTROPHORESISWS
				case IFSControl.ELECTROPHORESISWS_QueryAmplifyRecord:
					ElectrophoresisLocator.getInstance().queryAmplifyRecord(evt as XML);break;
				case IFSControl.ELECTROPHORESISWS_GetSampleEP:
					ElectrophoresisLocator.getInstance().getSampleEP(evt as XML);break;
				case IFSControl.ELECTROPHORESISWS_JoinEP:
					ElectrophoresisLocator.getInstance().joinEP(evt as XML);break;
				case IFSControl.ELECTROPHORESISWS_InsertEP:
					ElectrophoresisLocator.getInstance().insertEP();break;
				case IFSControl.ELECTROPHORESISWS_UpdateEP:
					ElectrophoresisLocator.getInstance().updateEP();break;
				case IFSControl.ELECTROPHORESISWS_DeleteEPRecord:
					ElectrophoresisLocator.getInstance().deleteEPRecord();break;
				case IFSControl.ELECTROPHORESISWS_QueryEPRecord:
					ElectrophoresisLocator.getInstance().queryEPRecord(evt as XML);break;
				case IFSControl.ELECTROPHORESISWS_GetSampleEPRecord:
					ElectrophoresisLocator.getInstance().getSampleEPRecord(evt as XML);break;
				case IFSControl.ELECTROPHORESISWS_QueryCaseEP:
					ElectrophoresisLocator.getInstance().queryCaseEP(evt as XML);break;
								
				//NOTIFICATIONWS
				case IFSControl.NOTIFICATIONWS_INSERT:
					NotificationLocator.getInstance().insert();break;
				case IFSControl.NOTIFICATIONWS_UPDATE:
					NotificationLocator.getInstance().update();break;
				case IFSControl.NOTIFICATIONWS_DELETE:
					NotificationLocator.getInstance().deleteFunc();break;
				case IFSControl.NOTIFICATIONWS_GET_ALL:
					NotificationLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.NOTIFICATIONWS_GetImportant:
					NotificationLocator.getInstance().GetImportant(evt as XML);break;
				
				//Supplies 
				case IFSControl.SUPPLIES_GetAll:
					SuppliesLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.SUPPLIES_Insert:
					SuppliesLocator.getInstance().insert();break;
				case IFSControl.SUPPLIES_Update:
					SuppliesLocator.getInstance().update();break;
				case IFSControl.SUPPLIES_Delete:
					SuppliesLocator.getInstance().deleteFunc();break;
				case IFSControl.SUPPLIES_GetOne:
					SuppliesLocator.getInstance().getOne(evt as XML);break;
				case IFSControl.SUPPLIES_MackAllHC:
					SuppliesLocator.getInstance().getAll(evt as XML);break;
				
				//SuppliesUse
				case IFSControl.SUPPLIESUSE_GetAll:
					SuppliesUseLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.SUPPLIESUSE_Insert:
					SuppliesUseLocator.getInstance().insert();break;
				case IFSControl.SUPPLIESUSE_Update:
					SuppliesUseLocator.getInstance().update();break;
				case IFSControl.SUPPLIESUSE_Delete:
					SuppliesUseLocator.getInstance().deleteFunc();break;
				case IFSControl.SUPPLIESUSE_GetOne:
					SuppliesUseLocator.getInstance().getOne(evt as XML);break;
				case IFSControl.SUPPLIESUSE_MackAllHCSY:
					SuppliesUseLocator.getInstance().getAll(evt as XML);break;
				//Supplies verification
				case IFSControl.SUPPLIESVERIFICATION_GetAll:
					SuppliesVerificationLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.SUPPLIESVERIFICATION_Insert:
					SuppliesVerificationLocator.getInstance().insert();break;
				case IFSControl.SUPPLIESVERIFICATION_Update:
					SuppliesVerificationLocator.getInstance().update();break;
				case IFSControl.SUPPLIESVERIFICATION_Delete:
					SuppliesVerificationLocator.getInstance().deleteFunc();break;
				case IFSControl.SUPPLIESVERIFICATION_GetOne:
					SuppliesVerificationLocator.getInstance().getOne(evt as XML);break;
				case IFSControl.SUPPLIESVERIFICATION_MackAllHCHC:
					SuppliesVerificationLocator.getInstance().getAll(evt as XML);break;
				//Supplies procurement
				case IFSControl.SUPPLIESPROCUREMENT_GetAll:
					SuppliesProcurementLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.SUPPLIESPROCUREMENT_Insert:
					SuppliesProcurementLocator.getInstance().insert();break;
				case IFSControl.SUPPLIESPROCUREMENT_Update:
					SuppliesProcurementLocator.getInstance().update();break;
				case IFSControl.SUPPLIESPROCUREMENT_Delete:
					SuppliesProcurementLocator.getInstance().deleteFunc();break;
				case IFSControl.SUPPLIESPROCUREMENT_GetOne:
					SuppliesProcurementLocator.getInstance().getOne(evt as XML);break;
				case IFSControl.SUPPLIESPROCUREMENT_MackAllHCCG:
					SuppliesProcurementLocator.getInstance().getAll(evt as XML);break;
				//Equipment
				case IFSControl.EQUIPMENT_GetAll:
					EquipmentLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.EQUIPMENT_GetSBAll:
					EquipmentLocator.getInstance().getSBAll(evt as XML);break;
				case IFSControl.EQUIPMENT_Insert:
					EquipmentLocator.getInstance().insert();break;
				case IFSControl.EQUIPMENT_Update:
					EquipmentLocator.getInstance().update();break;
				case IFSControl.EQUIPMENT_Delete:
					EquipmentLocator.getInstance().deleteFunc();break;
				case IFSControl.EQUIPMENT_GetOne:
					EquipmentLocator.getInstance().getOne(evt as XML);break;
				case IFSControl.EQUIPMENT_MackAllFB:
					EquipmentLocator.getInstance().mackAllFB(evt as XML);break;
				//Equipment Repair
				
				case IFSControl.EQUIPMENTREPAIR_GetAll:
					EquipmentRepairLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.EQUIPMENTREPAIR_Insert:
					EquipmentRepairLocator.getInstance().insert();break;
				case IFSControl.EQUIPMENTREPAIR_Update:
					EquipmentRepairLocator.getInstance().update();break;
				case IFSControl.EQUIPMENTREPAIR_Delete:
					EquipmentRepairLocator.getInstance().deleteFunc();break;
				case IFSControl.EQUIPMENTREPAIR_GetOne:
					EquipmentRepairLocator.getInstance().getOne(evt as XML);break;
				case IFSControl.EQUIPMENTREPAIR_MackAllFB:
					EquipmentRepairLocator.getInstance().mackAllFB(evt as XML);break;
				//Equipment Check
				
				case IFSControl.EQUIPMENTCHECK_GetAll:
					EquipmentCheckLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.EQUIPMENTCHECK_Insert:
					EquipmentCheckLocator.getInstance().insert();break;
				case IFSControl.EQUIPMENTCHECK_Update:
					EquipmentCheckLocator.getInstance().update();break;
				case IFSControl.EQUIPMENTCHECK_Delete:
					EquipmentCheckLocator.getInstance().deleteFunc();break;
				case IFSControl.EQUIPMENTCHECK_GetOne:
					EquipmentCheckLocator.getInstance().getOne(evt as XML);break;
				
				//Equipment Inspection
				case IFSControl.EQUIPMENTINSPECTION_GetAll:
					EquipmentInspectionLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.EQUIPMENTINSPECTION_Insert:
					EquipmentInspectionLocator.getInstance().insert();break;
				case IFSControl.EQUIPMENTINSPECTION_Update:
					EquipmentInspectionLocator.getInstance().update();break;
				case IFSControl.EQUIPMENTINSPECTION_Delete:
					EquipmentInspectionLocator.getInstance().deleteFunc();break;
				case IFSControl.EQUIPMENTINSPECTION_GetOne:
					EquipmentInspectionLocator.getInstance().getOne(evt as XML);break;
				
				//Equipment Maintain
				case IFSControl.EQUIPMENTMAINTAIN_GetAll:
					EquipmentMaintainLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.EQUIPMENTMAINTAIN_Insert:
					EquipmentMaintainLocator.getInstance().insert();break;
				case IFSControl.EQUIPMENTMAINTAIN_Update:
					EquipmentMaintainLocator.getInstance().update();break;
				case IFSControl.EQUIPMENTMAINTAIN_Delete:
					EquipmentMaintainLocator.getInstance().deleteFunc();break;
				case IFSControl.EQUIPMENTMAINTAIN_GetOne:
					EquipmentMaintainLocator.getInstance().getOne(evt as XML);break;
				case IFSControl.EQUIPMENTMAINTAIN_MackAllFB:
					EquipmentMaintainLocator.getInstance().mackAllFB(evt as XML);break;
				//Equipment USE
				case IFSControl.EQUIPMENTUSE_GetAll:
					EquipmentUseLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.EQUIPMENTUSE_Insert:
					EquipmentUseLocator.getInstance().insert();break;
				case IFSControl.EQUIPMENTUSE_Update:
					EquipmentUseLocator.getInstance().update();break;
				case IFSControl.EQUIPMENTUSE_Delete:
					EquipmentUseLocator.getInstance().deleteFunc();break;
				case IFSControl.EQUIPMENTUSE_GetOne:
					EquipmentUseLocator.getInstance().getOne(evt as XML);break;
				case IFSControl.EQUIPMENTUSE_MackAllFB:
					EquipmentUseLocator.getInstance().mackAllFB(evt as XML);break;
				
				//VITAE
				case IFSControl.VITAEWS_GetAll:
					VitaeLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.VITAEWS_Insert:
					VitaeLocator.getInstance().insert();break;
				case IFSControl.VITAEWS_Update:
					VitaeLocator.getInstance().update();break;
				case IFSControl.VITAEWS_Delete:
					VitaeLocator.getInstance().deleteFunc();break;
				//WORK
				case IFSControl.WORKWS_GetAll:
					WorkLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.WORKWS_Insert:
					WorkLocator.getInstance().insert();break;
				case IFSControl.WORKWS_Update:
					WorkLocator.getInstance().update();break;
				case IFSControl.WORKWS_Delete:
					WorkLocator.getInstance().deleteFunc();break;
				//WRITING
				case IFSControl.WRITINGWS_GetAll:
					WritingLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.WRITINGWS_Insert:
					WritingLocator.getInstance().insert();break;
				case IFSControl.WRITINGWS_Update:
					WritingLocator.getInstance().update();break;
				case IFSControl.WRITINGWS_Delete:
					WritingLocator.getInstance().deleteFunc();break;
				//FRUITFUL
				case IFSControl.FRUITFULWS_GetAll:
					FruitfulLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.FRUITFULWS_Insert:
					FruitfulLocator.getInstance().insert();break;
				case IFSControl.FRUITFULWS_Update:
					FruitfulLocator.getInstance().update();break;
				case IFSControl.FRUITFULWS_Delete:
					FruitfulLocator.getInstance().deleteFunc();break;
				//TrainNote_
				case IFSControl.TrainNote_GetAll:
					TrainLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.TrainNote_GetOne:
					TrainLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.TrainNote_Insert:
					TrainLocator.getInstance().insert();break;
				case IFSControl.TrainNote_InsertSS:
					TrainLocator.getInstance().insert();break;
				case IFSControl.TrainNote_Update:
					TrainLocator.getInstance().update();break;
				case IFSControl.TrainNote_Delete:
					TrainLocator.getInstance().deleteFunc();break;
				//ServiceTrain
				case IFSControl.ServiceTrain_GetAll:
					ServiceTrainLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.ServiceTrain_Insert:
					ServiceTrainLocator.getInstance().insert();break;
				case IFSControl.ServiceTrain_Update:
					ServiceTrainLocator.getInstance().update();break;
				case IFSControl.ServiceTrain_Delete:
					ServiceTrainLocator.getInstance().deleteFunc();break;
				//AbiTest
				case IFSControl.AbiTest_GetAll:
					AbiTestLocator.getInstance().getAll(evt as XML);break;
				case IFSControl.AbiTest_Insert:
					AbiTestLocator.getInstance().insert();break;
				case IFSControl.AbiTest_Update:
					AbiTestLocator.getInstance().update();break;
				case IFSControl.AbiTest_Delete:
					AbiTestLocator.getInstance().deleteFunc();break;

			}
		}
		public function onFault(evt:*=null):void
		{			
			Helper.showAlert("与服务器交互失败："+evt as String);
		}
		public function execute(evt:CairngormEvent):void
		{
			var ifsEvt:IFSEvent=evt as IFSEvent;
			evtType=ifsEvt.type;
			
			if(evtType.search("DICTWS_")==0)
			{
				var DICTWS_:DictDelegate=new DictDelegate(this);
				switch(evtType)
				{
					case IFSControl.DICTWS_GetXml:
						DICTWS_.getXml();break;
					case IFSControl.DICTWS_InsertXml:
						DICTWS_.insertItem();break;
					case IFSControl.DICTWS_DeleteXml:
						DICTWS_.deleteItem();break;
					case IFSControl.DICTWS_SaveDNATestItem:
						DICTWS_.SaveDNATestItem();break;
				}
			}	
			else if(evtType.search("JUSTYPEWS_")==0)
			{
				var JUSTYPEWS_:JusTypeDelegate=new JusTypeDelegate(this);
				switch(evtType)
				{
					case IFSControl.JUSTYPEWS_GetXml:
						JUSTYPEWS_.getXml() ;break;
					case IFSControl.JUSTYPEWS_InsertOffice:
						JUSTYPEWS_.InsertOffice() ;break;
					case IFSControl.JUSTYPEWS_UpdateOffice:
						JUSTYPEWS_.UpdateOffice() ;break;
					case IFSControl.JUSTYPEWS_DeleteOffice:
						JUSTYPEWS_.DeleteOffice() ;break;
					case IFSControl.JUSTYPEWS_InsertType:
						JUSTYPEWS_.InsertType() ;break;
					case IFSControl.JUSTYPEWS_UpdateType:
						JUSTYPEWS_.UpdateType() ;break;
					case IFSControl.JUSTYPEWS_DeleteType:
						JUSTYPEWS_.DeleteType() ;break;
					case IFSControl.JUSTYPEWS_UpdateAdmin:
						JUSTYPEWS_.UpdateAdmin() ;break;
					case IFSControl.JUSTYPEWS_UpdateDna:
						JUSTYPEWS_.UpdateDna() ;break;
				}
			}		
			else if(evtType.search("IDCASEWS_")==0)
			{
				var IDCASEWS_:IdCaseDelegate=new IdCaseDelegate(this);
				switch(evtType)
				{
					case IFSControl.IDCASEWS_Insert:
						IDCASEWS_.insert();break;
					case IFSControl.IDCASEWS_Update:
						IDCASEWS_.update();break;
					case IFSControl.IDCASEWS_DeleteC:
						IDCASEWS_.deleteC();break;
					case IFSControl.IDCASEWS_DeleteD:
						IDCASEWS_.deleteD();break;
					case IFSControl.IDCASEWS_GetAllD:
						IDCASEWS_.getAllD();break;
					case IFSControl.IDCASEWS_GetAcceptDup:
						IDCASEWS_.getAcceptDup();break;
					case IFSControl.IDCASEWS_UpdateBsInfo:
						IDCASEWS_.updateBsInfo();break;
					case IFSControl.IDCASEWS_GetOneRecord:
						IDCASEWS_.getOneRecord();break;
					case IFSControl.IDCASEWS_ImportToOraCase:
						IDCASEWS_.importToOraCase();break;
					case IFSControl.IDCASEWS_GetYearStaData:
						IDCASEWS_.getYearStaData();break;
					case IFSControl.IDCASEWS_GetAllJds:
						IDCASEWS_.getAllJds();break;
				}
			}
			else if(evtType.search("PSBWS_")==0)
			{
				var PSBWS_:PsbDelegate=new PsbDelegate(this);
				switch(evtType)
				{
					case IFSControl.PSBWS_GetAll:
						PSBWS_.getAll();break;
					case IFSControl.PSBWS_Insert:
						PSBWS_.insert();break;
					case IFSControl.PSBWS_Update:
						PSBWS_.update();break;
					case IFSControl.PSBWS_Delete:
						PSBWS_.deleteObj();break;
				}
			}	
			else if(evtType.search("REGIONCODEWS_")==0)
			{
				var REGIONCODEWS_:RegionCodeDelegate=new RegionCodeDelegate(this);
				switch(evtType)
				{
					case IFSControl.REGIONCODEWS_GetAll:
						REGIONCODEWS_.getAll();break;
				}
			}	
			else if(evtType.search("IDFLOWWS_")==0)
			{
				var IDFLOWWS_:IdFlowDelegate=new IdFlowDelegate(this);
				switch(evtType)
				{
					case IFSControl.IDFLOWWS_Cancel:
						IDFLOWWS_.cancelFunc();break;
					case IFSControl.IDFLOWWS_UpdateAccept:
						IDFLOWWS_.updateAccept();break;
					case IFSControl.IDFLOWWS_UpdateAcceptMp:
						IDFLOWWS_.updateAcceptMp();break;
					case IFSControl.IDFLOWWS_UpdateAcceptMpr:
						IDFLOWWS_.updateAcceptMpr();break;
					case IFSControl.IDFLOWWS_UpdateAcceptDna:
						IDFLOWWS_.updateAcceptDna();break;
					case IFSControl.IDFLOWWS_UpdateConclusion:
						IDFLOWWS_.updateConclusion();break;
					case IFSControl.IDFLOWWS_UpdateTesterFinish:
						IDFLOWWS_.updateTesterFinish();break;
					case IFSControl.IDFLOWWS_UpdateAudit:
						IDFLOWWS_.updateAudit();break;
					case IFSControl.IDFLOWWS_UpdateRepGet:
						IDFLOWWS_.updateRepGet();break;
					case IFSControl.IDFLOWWS_GetNextConNo:
						IDFLOWWS_.getNextConNo();break;
					case IFSControl.IDFLOWWS_GetNextAccNo:
						IDFLOWWS_.getNextAccNo();break;
					case IFSControl.IDFLOWWS_GetNextDocNo:
						IDFLOWWS_.getNextDocNo();break;
					case IFSControl.IDFLOWWS_GetNextSLN:
						IDFLOWWS_.getNextSLN();break;
					case IFSControl.IDFLOWWS_GetCaseNextSLN:
						IDFLOWWS_.getCaseNextSLN();break;
					case IFSControl.IDFLOWWS_QueryAllCase:
						IDFLOWWS_.queryAllCase();break;
					case IFSControl.IDFLOWWS_PrintTz:
						IDFLOWWS_.printTz();break;
					case IFSControl.IDFLOWWS_PrintFWJL:
						IDFLOWWS_.printFWJL();break;
					
				}
			}
			else if(evtType.search("SYSUSERWS_")==0)
			{
				var SYSUSERWS_:SysUserDelegate=new SysUserDelegate(this);
				switch(evtType)
				{
					case IFSControl.SYSUSERWS_Insert:
						SYSUSERWS_.insert();break;
					case IFSControl.SYSUSERWS_Update:
						SYSUSERWS_.update();break;
					case IFSControl.SYSUSERWS_UpdateJBXX:
						SYSUSERWS_.updatejbxx();break;
					case IFSControl.SYSUSERWS_Login:
						SYSUSERWS_.login();break;
					case IFSControl.SYSUSERWS_GetAll:
						SYSUSERWS_.getAll();break;
					case IFSControl.SYSUSERWS_GetDeletedPerson:
						SYSUSERWS_.login();break;
					case IFSControl.SYSUSERWS_GetConsignPerson:
						SYSUSERWS_.getConsignPerson();break;
				}
			}

			else if(evtType.search("IDTASKWS_")==0)
			{
				var IDFLOWWS2_:IdFlowDelegate=new IdFlowDelegate(this);
				switch(evtType)
				{
					case IFSControl.IDTASKWS_GetCaseAcceptTask:
						IDFLOWWS2_.getCaseAcceptTask();break;
					case IFSControl.IDTASKWS_GetAcceptByBarCode:
						IDFLOWWS2_.getAcceptByBarCode();break;
					case IFSControl.IDTASKWS_GetTestTask:
						IDFLOWWS2_.getTestTask();break;
					case IFSControl.IDTASKWS_GetAuditTask:
						IDFLOWWS2_.getAuditTask();break;
					case IFSControl.IDTASKWS_GetDocMakeTask:
						IDFLOWWS2_.getDocMakeTask();break;
					case IFSControl.IDTASKWS_GetReportTask:
						IDFLOWWS2_.getReportTask();break;
				}
			}
			else if(evtType.search("EXCASEWS_")==0)
			{
				var EXCASEWS_:ExCaseDelegate=new ExCaseDelegate(this);
				switch(evtType)
				{
					case IFSControl.EXCASEWS_GetExCaseList:
						EXCASEWS_.getExCaseList();break;
					case IFSControl.EXCASEWS_ExeSql:
						EXCASEWS_.exeSql();break;
					case IFSControl.EXCASEWS_SendShortNote:
						EXCASEWS_.sendShortNote();break;
					case IFSControl.EXCASEWS_QueryGetReportCase:
						EXCASEWS_.queryGetReportCase();break;
					case IFSControl.EXCASEWS_GetSpTaskAmount:
						EXCASEWS_.getSpTaskAmount();break;
					case IFSControl.EXCASEWS_GetYjrSp:
						EXCASEWS_.getYjrSp();break;
					case IFSControl.EXCASEWS_GetTaskRemind:
						EXCASEWS_.getTaskRemind();break;
				}
			}

			else if(evtType.search("STATISTICSWS_")==0)
			{
				var STATISTICSWS_:StatisticsDelegate=new StatisticsDelegate(this);
				switch(evtType)
				{
					case IFSControl.STATISTICSWS_PersonWork:
						STATISTICSWS_.PersonWork();break;
					case IFSControl.STATISTICSWS_StationWork:
						STATISTICSWS_.StationWork();break;
					case IFSControl.STATISTICSWS_GetStation:
						STATISTICSWS_.GetStation();break;
					case IFSControl.STATISTICSWS_CaseProperty:
						STATISTICSWS_.CaseProperty();break;
					case IFSControl.STATISTICSWS_CaseConclusion:
						STATISTICSWS_.CaseConclusion();break;
				}
			}
			
			else if(evtType.search("MRELATIVEWS_")==0)
			{
				var MRELATIVEWS_:MRelativeDelegate=new MRelativeDelegate(this);
				switch(evtType)
				{
					case IFSControl.MRELATIVEWS_NewConsign:
						MRELATIVEWS_.newConsign();break;
					case IFSControl.MRELATIVEWS_UpdateR:
						MRELATIVEWS_.updateR();break;
					case IFSControl.MRELATIVEWS_DeleteR:
						MRELATIVEWS_.deleteR();break;
					case IFSControl.MRELATIVEWS_GetOneMpr:
						MRELATIVEWS_.getOneMpr();break;
					case IFSControl.MRELATIVEWS_ImportToOraMpr:
						MRELATIVEWS_.importToOraMpr();break;
				}
			}
			else if(evtType.search("MISSINGPERSONWS_")==0)
			{
				var MISSINGPERSONWS_:MissingPersonDelegate=new MissingPersonDelegate(this);
				switch(evtType)
				{
					case IFSControl.MISSINGPERSONWS_Insert:
						MISSINGPERSONWS_.insert();break;
					case IFSControl.MISSINGPERSONWS_Update:
						MISSINGPERSONWS_.update();break;
					case IFSControl.MISSINGPERSONWS_Delete:
						MISSINGPERSONWS_.deleteObj();break;
					case IFSControl.MISSINGPERSONWS_GetOneMp:
						MISSINGPERSONWS_.getOneMp();break;
					case IFSControl.MISSINGPERSONWS_ImportToOraMp:
						MISSINGPERSONWS_.importToOraMp();break;
				}
			}
			else if((evtType.search("IDTESTIMONYWS_")==0)||(evtType.search("IDTESTIMONY_CTRWS_")==0))
			{
				var IDTESTIMONYWS_:IdTestimonyDelegate=new IdTestimonyDelegate(this);
				switch(evtType)
				{
					case IFSControl.IDTESTIMONYWS_Insert:
					case IFSControl.IDTESTIMONY_CTRWS_Insert:
						IDTESTIMONYWS_.insert();break;
					case IFSControl.IDTESTIMONYWS_Update:
					case IFSControl.IDTESTIMONY_CTRWS_Update:
						IDTESTIMONYWS_.update();break;
					case IFSControl.IDTESTIMONYWS_Delete:
					case IFSControl.IDTESTIMONY_CTRWS_Delete:
						IDTESTIMONYWS_.deleteObj();break;
					case IFSControl.IDTESTIMONYWS_GetAll:
					case IFSControl.IDTESTIMONY_CTRWS_GetAll:
						IDTESTIMONYWS_.getAll();break;
					case IFSControl.IDTESTIMONYWS_TesOper:
						IDTESTIMONYWS_.tesOper();break;
					case IFSControl.IDTESTIMONYWS_Query:
						IDTESTIMONYWS_.query();break;
				}
			}
			else if(evtType.search("IDPERSONWS_")==0)
			{
				var IDPERSONWS_:IdPersonDelegate=new IdPersonDelegate(this);
				switch(evtType)
				{
					case IFSControl.IDPERSONWS_Insert:
						IDPERSONWS_.insert();break;
					case IFSControl.IDPERSONWS_Update:
						IDPERSONWS_.update();break;
					case IFSControl.IDPERSONWS_Delete:
						IDPERSONWS_.deleteObj();break;
					case IFSControl.IDPERSONWS_GetAll:
						IDPERSONWS_.getAll();break;
				}
			}
			else if(evtType.search("CASEFILEWS_")==0)
			{
				var CASEFILEWS_:CaseFileDelegate=new CaseFileDelegate(this);
				switch(evtType)
				{
					case IFSControl.CASEFILEWS_Delete:
						CASEFILEWS_.deleteObj();break;
					case IFSControl.CASEFILEWS_GetAll:
						CASEFILEWS_.getAll();break;
					case IFSControl.CASEFILEWS_GetAllFileData:
						CASEFILEWS_.getAllFileData();break;
				}
			}
			else if(evtType.search("WORDWS_")==0)
			{
				var WORDWS_:WordDelegate=new WordDelegate(this);
				switch(evtType)
				{
					case IFSControl.WORDWS_GetCaseWordList:
						WORDWS_.getCaseWordList();break;
					case IFSControl.WORDWS_DeleteWord:
						WORDWS_.deleteWord();break;
					case IFSControl.WORDWS_GenerateWord:
						WORDWS_.generateWord();break;
					case IFSControl.WORDWS_GenerateWzms:
						WORDWS_.generateWzms();break;
					case IFSControl.WORDWS_GetAllCaseWord:
						WORDWS_.getAllCaseWord();break;
					case IFSControl.WORDWS_GetCaseWordManageList:
						WORDWS_.getCaseWordManageList();break;
					case IFSControl.WORDWS_PrintSampleTestRecord:
						WORDWS_.printSampleTestRecord();break;
					case IFSControl.WORDWS_PrintCaseTestRecord:
						WORDWS_.printCaseTestRecord();break;
					case IFSControl.WORDWS_PrintDNATestHelpFile:
						WORDWS_.printDNATestHelpFile();break;
				}
			}
			else if(evtType.search("DNASEWS_")==0)
			{
				var DNASEWS_:DnaSeDelegate=new DnaSeDelegate(this);
				switch(evtType)
				{
					case IFSControl.DNASEWS_Insert:
						DNASEWS_.insert();break;
					case IFSControl.DNASEWS_InsertWithNo:
						DNASEWS_.insertWithNo();break;
					case IFSControl.DNASEWS_Update:
						DNASEWS_.update();break;
					case IFSControl.DNASEWS_Delete:
						DNASEWS_.deleteObj();break;
					case IFSControl.DNASEWS_GetAll:
						DNASEWS_.getAll();break;
				}
			}
			else if(evtType.search("UNKNOWNDECEASEDWS_")==0)
			{
				var UNKNOWNDECEASEDWS_:UnkownDeceasedDelegate=new UnkownDeceasedDelegate(this);
				switch(evtType)
				{
					case IFSControl.UNKNOWNDECEASEDWS_Insert:
						UNKNOWNDECEASEDWS_.insert();break;
					case IFSControl.UNKNOWNDECEASEDWS_InsertWithNo:
						UNKNOWNDECEASEDWS_.insertWithNo();break;
					case IFSControl.UNKNOWNDECEASEDWS_Update:
						UNKNOWNDECEASEDWS_.update();break;
					case IFSControl.UNKNOWNDECEASEDWS_Delete:
						UNKNOWNDECEASEDWS_.deleteObj();break;
					case IFSControl.UNKNOWNDECEASEDWS_GetAll:
						UNKNOWNDECEASEDWS_.getAll();break;
				}
			}
			else if((evtType.search("CASEPERSONNELSAMPLE_SHRWS_")==0)||
				(evtType.search("CASEPERSONNELSAMPLE_XYRWS_")==0)||(evtType.search("CASEPERSONNELSAMPLE_QTRWS_")==0))
			{
				var CASEPERSONNELSAMPLEWS_:CasePersonnelSampleDelegate=new CasePersonnelSampleDelegate(this);
				switch(evtType)
				{
					case IFSControl.CASEPERSONNELSAMPLE_SHRWS_Insert:
					case IFSControl.CASEPERSONNELSAMPLE_XYRWS_Insert:
					case IFSControl.CASEPERSONNELSAMPLE_QTRWS_Insert:
						CASEPERSONNELSAMPLEWS_.insert();break;
					case IFSControl.CASEPERSONNELSAMPLE_SHRWS_InsertWithNo:
					case IFSControl.CASEPERSONNELSAMPLE_XYRWS_InsertWithNo:
					case IFSControl.CASEPERSONNELSAMPLE_QTRWS_InsertWithNo:
						CASEPERSONNELSAMPLEWS_.insertWithNo();break;
					case IFSControl.CASEPERSONNELSAMPLE_SHRWS_Update:
					case IFSControl.CASEPERSONNELSAMPLE_XYRWS_Update:
					case IFSControl.CASEPERSONNELSAMPLE_QTRWS_Update:
						CASEPERSONNELSAMPLEWS_.update();break;
					case IFSControl.CASEPERSONNELSAMPLE_SHRWS_Delete:
					case IFSControl.CASEPERSONNELSAMPLE_XYRWS_Delete:
					case IFSControl.CASEPERSONNELSAMPLE_QTRWS_Delete:
						CASEPERSONNELSAMPLEWS_.deleteObj();break;
					case IFSControl.CASEPERSONNELSAMPLE_SHRWS_GetAll:
					case IFSControl.CASEPERSONNELSAMPLE_XYRWS_GetAll:
					case IFSControl.CASEPERSONNELSAMPLE_QTRWS_GetAll:
						CASEPERSONNELSAMPLEWS_.getAll();break;
				}
			}
			else if((evtType.search("CASERELATIVE_SHRWS_")==0)||(evtType.search("CASERELATIVE_XYRWS_")==0))
			{
				var CASERELATIVEWS_:CaseRelativeDelegate=new CaseRelativeDelegate(this);
				switch(evtType)
				{
					case IFSControl.CASERELATIVE_SHRWS_Insert:
					case IFSControl.CASERELATIVE_XYRWS_Insert:
						CASERELATIVEWS_.insert();break;
					case IFSControl.CASERELATIVE_SHRWS_InsertWithNo:
					case IFSControl.CASERELATIVE_XYRWS_InsertWithNo:
						CASERELATIVEWS_.insertWithNo();break;
					case IFSControl.CASERELATIVE_SHRWS_Update:
					case IFSControl.CASERELATIVE_XYRWS_Update:
						CASERELATIVEWS_.update();break;
					case IFSControl.CASERELATIVE_SHRWS_Delete:
					case IFSControl.CASERELATIVE_XYRWS_Delete:
						CASERELATIVEWS_.deleteObj();break;
					case IFSControl.CASERELATIVE_SHRWS_GetAll:
					case IFSControl.CASERELATIVE_XYRWS_GetAll:
						CASERELATIVEWS_.getAll();break;
				}
			}
			else if(evtType.search("CODIESWS_")==0)
			{
				var CODIESWS_:CodiesDelegate=new CodiesDelegate(this);
				switch(evtType)
				{
					case IFSControl.CODIESWS_GetAll:
						CODIESWS_.getAll();break;
					case IFSControl.CODIESWS_GetAllTmpStr:
						CODIESWS_.getAllTmpStr();break;
					case IFSControl.CODIESWS_deleteTmpStr:
						CODIESWS_.deleteTmpStr();break;
					case IFSControl.CODIESWS_deleteAllTmpStr:
						CODIESWS_.deleteAllTmpStr();break;
					case IFSControl.CODIESWS_UpdateStrFromTmp:
						CODIESWS_.updateStrFromTmp();break;
					case IFSControl.CODIESWS_UpdateAllStrFromTmp:
						CODIESWS_.updateAllStrFromTmp();break;
					case IFSControl.CODIESWS_GetAllStr:
						CODIESWS_.getAllStr();break;
					case IFSControl.CODIESWS_UpdateStr:
						CODIESWS_.updateStr();break;
					case IFSControl.CODIESWS_PrintStrTable:
						CODIESWS_.printStrTable();break;
					case IFSControl.CODIESWS_SameCaseBzAna:
						CODIESWS_.sameCaseBzAna();break;
					case IFSControl.CODIESWS_Import:
						CODIESWS_.importStr();break;
					case IFSControl.CODIESWS_QuerySample:
						CODIESWS_.querySample();break;
					case IFSControl.CODIESWS_PrintCODISdat:
						CODIESWS_.printCODISdat();break;
				}
			}
			else if(evtType.search("IFAWS_")==0)
			{
				var IFAWS_:IFADelegate=new IFADelegate(this);
				switch(evtType)
				{
					case IFSControl.IFAWS_ReadStr:
						IFAWS_.readStr();break;
				}
			}
			else if(evtType.search("DOCMODWS_")==0)
			{
				var DOCMODWS_:DocModDelegate=new DocModDelegate(this);
				switch(evtType)
				{
					case IFSControl.DOCMODWS_Insert:
						DOCMODWS_.insert();break;
					case IFSControl.DOCMODWS_Update:
						DOCMODWS_.update();break;
					case IFSControl.DOCMODWS_Delete:
						DOCMODWS_.deleteObj();break;
					case IFSControl.DOCMODWS_GetAll:
						DOCMODWS_.getAll();break;
				}
			}
			else if(evtType.search("PREEXAMWS_")==0)
			{
				var PREEXAMWS_:PreExamDelegate=new PreExamDelegate(this);
				switch(evtType)
				{
					case IFSControl.PREEXAMWS_InsertPreExam:
						PREEXAMWS_.insertPreExam();break;
					case IFSControl.PREEXAMWS_UpdateCasePre:
						PREEXAMWS_.updateCasePre();break;
					case IFSControl.PREEXAMWS_DeleteCasePre:
						PREEXAMWS_.deleteCasePre();break;
					case IFSControl.PREEXAMWS_QueryCasePre:
						PREEXAMWS_.queryCasePre();break;
					case IFSControl.PREEXAMWS_QueryPreExam:
						PREEXAMWS_.queryPreExam();break;
				}
			}
			else if(evtType.search("CONFIRMWS_")==0)
			{
				var CONFIRMWS_:ConfirmDelegate=new ConfirmDelegate(this);
				switch(evtType)
				{
					case IFSControl.CONFIRMWS_InsertConfirm:
						CONFIRMWS_.insertConfirm();break;
					case IFSControl.CONFIRMWS_UpdateCaseConfirm:
						CONFIRMWS_.updateCaseConfirm();break;
					case IFSControl.CONFIRMWS_DeleteCaseConfirm:
						CONFIRMWS_.deleteCaseConfirm();break;
					case IFSControl.CONFIRMWS_QueryCaseConfirm:
						CONFIRMWS_.queryCaseConfirm();break;
					case IFSControl.CONFIRMWS_QueryConfirm:
						CONFIRMWS_.queryConfirm();break;
				}
			}
			else if(evtType.search("EXTRACTWS_")==0)
			{
				var EXTRACTWS_:ExtractDelegate=new ExtractDelegate(this);
				switch(evtType)
				{
					case IFSControl.EXTRACTWS_InsertExtract:
						EXTRACTWS_.insertExtract();break;
					case IFSControl.EXTRACTWS_UpdateExtract:
						EXTRACTWS_.updateExtract();break;
					case IFSControl.EXTRACTWS_InsertPure:
						EXTRACTWS_.insertPure();break;
					case IFSControl.EXTRACTWS_UpdatePure:
						EXTRACTWS_.updatePure();break;
					case IFSControl.EXTRACTWS_DeleteExtractRecord:
						EXTRACTWS_.deleteExtractRecord();break;
					case IFSControl.EXTRACTWS_NoTest:
						EXTRACTWS_.noTest();break;
					case IFSControl.EXTRACTWS_QueryExtract:
						EXTRACTWS_.queryExtract();break;
					case IFSControl.EXTRACTWS_QueryPure:
						EXTRACTWS_.queryPure();break;
					case IFSControl.EXTRACTWS_QueryCaseExtract:
						EXTRACTWS_.queryCaseExtract();break;
				}
			}
			else if(evtType.search("AMPLIFYWS_")==0)
			{
				var AMPLIFYWS_:AmplifyDelegate=new AmplifyDelegate(this);
				switch(evtType)
				{
					case IFSControl.AMPLIFYWS_QueryExtractRecord:
						AMPLIFYWS_.queryExtractRecord();break;
					case IFSControl.AMPLIFYWS_GetSampleAmplify:
						AMPLIFYWS_.getSampleAmplify();break;
					case IFSControl.AMPLIFYWS_JoinAmplify:
						AMPLIFYWS_.joinAmplify();break;
					case IFSControl.AMPLIFYWS_InsertAmplify:
						AMPLIFYWS_.insertAmplify();break;
					case IFSControl.AMPLIFYWS_UpdateAmplify:
						AMPLIFYWS_.updateAmplify();break;
					case IFSControl.AMPLIFYWS_DeleteAmplifyRecord:
						AMPLIFYWS_.deleteAmplifyRecord();break;
					case IFSControl.AMPLIFYWS_QueryCaseAmplify:
						AMPLIFYWS_.queryCaseAmplify();break;
				}
			}
			else if(evtType.search("ELECTROPHORESISWS_")==0)
			{
				var ELECTROPHORESISWS_:ElectrophoresisDelegate=new ElectrophoresisDelegate(this);
				switch(evtType)
				{
					case IFSControl.ELECTROPHORESISWS_QueryAmplifyRecord:
						ELECTROPHORESISWS_.queryAmplifyRecord();break;
					case IFSControl.ELECTROPHORESISWS_GetSampleEP:
						ELECTROPHORESISWS_.getSampleEP();break;
					case IFSControl.ELECTROPHORESISWS_JoinEP:
						ELECTROPHORESISWS_.joinEP();break;
					case IFSControl.ELECTROPHORESISWS_InsertEP:
						ELECTROPHORESISWS_.insertEP();break;
					case IFSControl.ELECTROPHORESISWS_UpdateEP:
						ELECTROPHORESISWS_.updateEP();break;
					case IFSControl.ELECTROPHORESISWS_DeleteEPRecord:
						ELECTROPHORESISWS_.deleteEPRecord();break;
					case IFSControl.ELECTROPHORESISWS_QueryEPRecord:
						ELECTROPHORESISWS_.queryEPRecord();break;
					case IFSControl.ELECTROPHORESISWS_GetSampleEPRecord:
						ELECTROPHORESISWS_.getSampleEPRecord();break;
					case IFSControl.ELECTROPHORESISWS_QueryCaseEP:
						ELECTROPHORESISWS_.queryCaseEP();break;
				}
			}
			else if(evtType.search("NOTIFICATIONWS_")==0)//NOTIFICATION_
			{
				var NOTIFICATIONWS_:NotificationDelegate=new NotificationDelegate(this);
				switch(evtType)
				{
					case IFSControl.NOTIFICATIONWS_INSERT:
						NOTIFICATIONWS_.insert();break;
					case IFSControl.NOTIFICATIONWS_UPDATE:
						NOTIFICATIONWS_.update();break;
					case IFSControl.NOTIFICATIONWS_DELETE:
						NOTIFICATIONWS_.deleteFunc();break;
					case IFSControl.NOTIFICATIONWS_GET_ALL:
						NOTIFICATIONWS_.getAll();break;
					case IFSControl.NOTIFICATIONWS_GetImportant:
						NOTIFICATIONWS_.GetImportant();break;
				}
			}

			else if (evtType.search("SUPPLIES_")==0)
			{
				var SUPPLIES_:SuppliesDelegate=new SuppliesDelegate(this);
				switch(evtType)
				{
					case IFSControl.SUPPLIES_GetAll:
						SUPPLIES_.getAll();break;
					case IFSControl.SUPPLIES_Insert:
						SUPPLIES_.insert();break;
					case IFSControl.SUPPLIES_Update:
						SUPPLIES_.update();break;
					case IFSControl.SUPPLIES_Delete:
						SUPPLIES_.deleteObj();break;
					case IFSControl.SUPPLIES_GetOne:
						SUPPLIES_.getOne();break;
					case IFSControl.SUPPLIES_MackAllHC:
						SUPPLIES_.mackAllHC();break;	
				}
			}
				
			else if (evtType.search("SUPPLIESUSE_")==0)
			{
				var SUPPLIESUSE_:SuppliesUseDelegate=new SuppliesUseDelegate(this);
				switch(evtType)
				{
					case IFSControl.SUPPLIESUSE_GetAll:
						SUPPLIESUSE_.getAll();break;
					case IFSControl.SUPPLIESUSE_Insert:
						SUPPLIESUSE_.insert();break;
					case IFSControl.SUPPLIESUSE_Update:
						SUPPLIESUSE_.update();break;
					case IFSControl.SUPPLIESUSE_Delete:
						SUPPLIESUSE_.deleteObj();break;
					case IFSControl.SUPPLIESUSE_GetOne:
						SUPPLIESUSE_.getOne();break;
					case IFSControl.SUPPLIESUSE_MackAllHCSY:
						SUPPLIESUSE_.mackAllHCSY();break;	
				}
			}
				
			else if (evtType.search("SUPPLIESVERIFICATION_")==0)
			{
				var SUPPLIEVERIFICATION_:SuppliesVerificationDelegate=new SuppliesVerificationDelegate(this);
				switch(evtType)
				{
					case IFSControl.SUPPLIESVERIFICATION_GetAll:
						SUPPLIEVERIFICATION_.getAll();break;
					case IFSControl.SUPPLIESVERIFICATION_Insert:
						SUPPLIEVERIFICATION_.insert();break;
					case IFSControl.SUPPLIESVERIFICATION_Update:
						SUPPLIEVERIFICATION_.update();break;
					case IFSControl.SUPPLIESVERIFICATION_Delete:
						SUPPLIEVERIFICATION_.deleteObj();break;
					case IFSControl.SUPPLIESVERIFICATION_GetOne:
						SUPPLIEVERIFICATION_.getOne();break;
					case IFSControl.SUPPLIESVERIFICATION_MackAllHCHC:
						SUPPLIEVERIFICATION_.mackAllHCHC();break;	
				}
			}
			else if (evtType.search("SUPPLIESPROCUREMENT_")==0)
			{
				var SUPPLIESPROCUREMENT_:SuppliesProcurementDelegate=new SuppliesProcurementDelegate(this);
				switch(evtType)
				{
					case IFSControl.SUPPLIESPROCUREMENT_GetAll:
						SUPPLIESPROCUREMENT_.getAll();break;
					case IFSControl.SUPPLIESPROCUREMENT_Insert:
						SUPPLIESPROCUREMENT_.insert();break;
					case IFSControl.SUPPLIESPROCUREMENT_Update:
						SUPPLIESPROCUREMENT_.update();break;
					case IFSControl.SUPPLIESPROCUREMENT_Delete:
						SUPPLIESPROCUREMENT_.deleteObj();break;
					case IFSControl.SUPPLIESPROCUREMENT_GetOne:
						SUPPLIESPROCUREMENT_.getOne();break;
					case IFSControl.SUPPLIESPROCUREMENT_MackAllHCCG:
						SUPPLIESPROCUREMENT_.mackAllHCCG();break;	
				}
			}	
			else if (evtType.search("EQUIPMENT_")==0)
			{ 
				var EQUIPMENT_:EquipmentDelegate=new EquipmentDelegate(this);
				switch(evtType)
				{
					case IFSControl.EQUIPMENT_GetAll:
						EQUIPMENT_.getAll();break;
					case IFSControl.EQUIPMENT_GetSBAll:
						EQUIPMENT_.getSBAll();break;
					case IFSControl.EQUIPMENT_Insert:
						EQUIPMENT_.insert();break;
					case IFSControl.EQUIPMENT_Update:
						EQUIPMENT_.update();break;
					case IFSControl.EQUIPMENT_Delete:
						EQUIPMENT_.deleteObj();break;
					case IFSControl.EQUIPMENT_GetOne:
						EQUIPMENT_.getOne();break;
					case IFSControl.EQUIPMENT_MackAllFB:
						EQUIPMENT_.mackAllFB();break;	
				}
			}
			else if (evtType.search("EQUIPMENTREPAIR_")==0)
			{
				var EQUIPMENTREPAIR_:EquipmentRepairDelegate=new EquipmentRepairDelegate(this);
				switch(evtType)
				{
					case IFSControl.EQUIPMENTREPAIR_GetAll:
						EQUIPMENTREPAIR_.getAll();break;
					case IFSControl.EQUIPMENTREPAIR_Insert:
						EQUIPMENTREPAIR_.insert();break;
					case IFSControl.EQUIPMENTREPAIR_Update:
						EQUIPMENTREPAIR_.update();break;
					case IFSControl.EQUIPMENTREPAIR_Delete:
						EQUIPMENTREPAIR_.deleteObj();break;
					case IFSControl.EQUIPMENTREPAIR_GetOne:
						EQUIPMENTREPAIR_.getOne();break;
					case IFSControl.EQUIPMENTREPAIR_MackAllFB:
						EQUIPMENTREPAIR_.mackAllFB();break;	
				}
			}
			else if (evtType.search("EQUIPMENTCHECK_")==0)
			{
				var EQUIPMENTCHECK_:EquipmentCheckDelegate=new EquipmentCheckDelegate(this);
				switch(evtType)
				{
					case IFSControl.EQUIPMENTCHECK_GetAll:
						EQUIPMENTCHECK_.getAll();break;
					case IFSControl.EQUIPMENTCHECK_Insert:
						EQUIPMENTCHECK_.insert();break;
					case IFSControl.EQUIPMENTCHECK_Update:
						EQUIPMENTCHECK_.update();break;
					case IFSControl.EQUIPMENTCHECK_Delete:
						EQUIPMENTCHECK_.deleteObj();break;
					case IFSControl.EQUIPMENTCHECK_GetOne:
						EQUIPMENTCHECK_.getOne();break;
					case IFSControl.EQUIPMENTCHECK_MackAllFB:
						EQUIPMENTCHECK_.mackAllFB();break;	
				}
			}
			else if (evtType.search("EQUIPMENTINSPECTION_")==0)
			{
				var EQUIPMENTINSPECTION_:EquipmentInspectionDelegate=new EquipmentInspectionDelegate(this);
				switch(evtType)
				{
					case IFSControl.EQUIPMENTINSPECTION_GetAll:
						EQUIPMENTINSPECTION_.getAll();break;
					case IFSControl.EQUIPMENTINSPECTION_Insert:
						EQUIPMENTINSPECTION_.insert();break;
					case IFSControl.EQUIPMENTINSPECTION_Update:
						EQUIPMENTINSPECTION_.update();break;
					case IFSControl.EQUIPMENTINSPECTION_Delete:
						EQUIPMENTINSPECTION_.deleteObj();break;
					case IFSControl.EQUIPMENTINSPECTION_GetOne:
						EQUIPMENTINSPECTION_.getOne();break;
					case IFSControl.EQUIPMENTINSPECTION_MackAllFB:
						EQUIPMENTINSPECTION_.mackAllFB();break;	
				}
			}
			else if (evtType.search("EQUIPMENTMAINTAIN_")==0)
			{
				var EQUIPMENTMAINTAIN_:EquipmentMaintainDelegate=new EquipmentMaintainDelegate(this);
				switch(evtType)
				{
					case IFSControl.EQUIPMENTMAINTAIN_GetAll:
						EQUIPMENTMAINTAIN_.getAll();break;
					case IFSControl.EQUIPMENTMAINTAIN_Insert:
						EQUIPMENTMAINTAIN_.insert();break;
					case IFSControl.EQUIPMENTMAINTAIN_Update:
						EQUIPMENTMAINTAIN_.update();break;
					case IFSControl.EQUIPMENTMAINTAIN_Delete:
						EQUIPMENTMAINTAIN_.deleteObj();break;
					case IFSControl.EQUIPMENTMAINTAIN_GetOne:
						EQUIPMENTMAINTAIN_.getOne();break;
					case IFSControl.EQUIPMENTMAINTAIN_MackAllFB:
						EQUIPMENTMAINTAIN_.mackAllFB();break;	
				}
			}		
			else if (evtType.search("EQUIPMENTUSE_")==0)
			{
				var EQUIPMENTUSE_:EquipmentUseDelegate=new EquipmentUseDelegate(this);
				switch(evtType)
				{
					case IFSControl.EQUIPMENTUSE_GetAll:
						EQUIPMENTUSE_.getAll();break;
					case IFSControl.EQUIPMENTUSE_Insert:
						EQUIPMENTUSE_.insert();break;
					case IFSControl.EQUIPMENTUSE_Update:
						EQUIPMENTUSE_.update();break;
					case IFSControl.EQUIPMENTUSE_Delete:
						EQUIPMENTUSE_.deleteObj();break;
					case IFSControl.EQUIPMENTUSE_GLSql:
						EQUIPMENTUSE_.gLSql();break;
					case IFSControl.EQUIPMENTUSE_GetOne:
						EQUIPMENTUSE_.getOne();break;
					case IFSControl.EQUIPMENTUSE_MackAllFB:
						EQUIPMENTUSE_.mackAllFB();break;	
				}
			}
			else if(evtType.search("VITAEWS_")==0)
			{
				var VITAEWS_:VitaeDelegate=new VitaeDelegate(this);
				switch(evtType)
				{
					case IFSControl.VITAEWS_GetAll:
						VITAEWS_.getAll();break;
					case IFSControl.VITAEWS_Insert:
						VITAEWS_.insert();break;
					case IFSControl.VITAEWS_Update:
						VITAEWS_.update();break;
					case IFSControl.VITAEWS_Delete:
						VITAEWS_.deleteObj();break;
				}
			}
			else if(evtType.search("WORKWS_")==0)
			{
				var WORKWS_:WorkDelegate=new WorkDelegate(this);
				switch(evtType)
				{
					case IFSControl.WORKWS_GetAll:
						WORKWS_.getAll();break;
					case IFSControl.WORKWS_Insert:
						WORKWS_.insert();break;
					case IFSControl.WORKWS_Update:
						WORKWS_.update();break;
					case IFSControl.WORKWS_Delete:
						WORKWS_.deleteObj();break;
				}
			}
			else if(evtType.search("WRITINGWS_")==0)
			{
				var WRITINGWS_:WritingDelegate=new WritingDelegate(this);
				switch(evtType)
				{
					case IFSControl.WRITINGWS_GetAll:
						WRITINGWS_.getAll();break;
					case IFSControl.WRITINGWS_Insert:
						WRITINGWS_.insert();break;
					case IFSControl.WRITINGWS_Update:
						WRITINGWS_.update();break;
					case IFSControl.WRITINGWS_Delete:
						WRITINGWS_.deleteObj();break;
				}
			}
			else if(evtType.search("FRUITFULWS_")==0)
			{
				var FRUITFULWS_:FruitfulDelegate=new FruitfulDelegate(this);
				switch(evtType)
				{
					case IFSControl.FRUITFULWS_GetAll:
						FRUITFULWS_.getAll();break;
					case IFSControl.FRUITFULWS_Insert:
						FRUITFULWS_.insert();break;
					case IFSControl.FRUITFULWS_Update:
						FRUITFULWS_.update();break;
					case IFSControl.FRUITFULWS_Delete:
						FRUITFULWS_.deleteObj();break;
				}
			}
			else if (evtType.search("TrainNote_")==0)//TrainNote
			{
				var TrainNote_:TrainDelegate=new TrainDelegate(this);
				switch(evtType)
				{
					case IFSControl.TrainNote_GetAll:
						TrainNote_.getAll();break;
					case IFSControl.TrainNote_GetOne:
						TrainNote_.getOne();break;
					case IFSControl.TrainNote_Insert:
						TrainNote_.insert();break;
					case IFSControl.TrainNote_InsertSS:
						TrainNote_.insertSS();break;
					case IFSControl.TrainNote_Update:
						TrainNote_.update();break;
					case IFSControl.TrainNote_Delete:
						TrainNote_.deleteFunc();break;
					case IFSControl.TrainNote_MackWord:
						TrainNote_.mackWord();break;
				}
			}
				//ServiceTrain
			else if (evtType.search("ServiceTrain_")==0)
			{
				var ServiceTrain_:ServiceTrainDelegate=new ServiceTrainDelegate(this);
				switch(evtType)
				{
					case IFSControl.ServiceTrain_GetAll:
						ServiceTrain_.getAll();break;
					case IFSControl.ServiceTrain_Insert:
						ServiceTrain_.insert();break;
					case IFSControl.ServiceTrain_Update:
						ServiceTrain_.update();break;
					case IFSControl.ServiceTrain_Delete:
						ServiceTrain_.deleteFunc();break;
					case IFSControl.ServiceTrain_MackWord:
						ServiceTrain_.mackWord();break;
				}
			}
				//AbiTest
			else if (evtType.search("AbiTest_")==0)
			{
				var AbiTest_:AbiTestDeltegate=new AbiTestDeltegate(this);
				switch(evtType)
				{
					case IFSControl.AbiTest_GetAll:
						AbiTest_.getAll();break;
					case IFSControl.AbiTest_Insert:
						AbiTest_.insert();break;
					case IFSControl.AbiTest_Update:
						AbiTest_.update();break;
					case IFSControl.AbiTest_Delete:
						AbiTest_.deleteFunc();break;
					case IFSControl.AbiTest_MackWord:
						AbiTest_.mackWord();break;
				}
			}

		}
	}
}