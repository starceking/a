package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	
	import locator.IdCaseLocator;
	import locator.IdFlowLocator;
	import locator.JusTypeLocator;
	import locator.MRelativeLocator;
	import locator.MissingPersonLocator;
	import locator.PsbLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Helper;
	import util.Server;
	
	import vo.IdFlowVo;
	
	public class IdFlowDelegate
	{
		public function IdFlowDelegate(responder:IFSCommand)
		{
			this.responder=responder;
		}
		//WebService
		private var ws:WebService; 
		private function get Ws():WebService
		{
			if(ws==null)
			{
				ws=new WebService(); 
				ws.loadWSDL(Server.getWsUrl("鉴定流程WS"));
				
				ws.Cancel.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateAccept.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateAcceptMp.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateAcceptMpr.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateAcceptDna.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateConclusion.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateTesterFinish.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateAudit.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateRepGet.addEventListener(ResultEvent.RESULT,resultHandler);
				
				ws.Cancel.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateAccept.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateAcceptMp.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateAcceptMpr.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateAcceptDna.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateConclusion.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateTesterFinish.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateAudit.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateRepGet.addEventListener(FaultEvent.FAULT,faultHandler);
				
				ws.GetNextConNo.addEventListener(ResultEvent.RESULT,getNextNoResultHandler);
				ws.GetNextAccNo.addEventListener(ResultEvent.RESULT,getNextNoResultHandler);
				ws.GetNextDocNo.addEventListener(ResultEvent.RESULT,getNextNoResultHandler);
				ws.GetNextConNo.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetNextAccNo.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetNextDocNo.addEventListener(FaultEvent.FAULT,faultHandler);
				
				ws.GetCaseAcceptTask.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetAcceptByBarCode.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetTestTask.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetAuditTask.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetDocMakeTask.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetReportTask.addEventListener(ResultEvent.RESULT,getXmlResultHandler);				
				ws.GetCaseAcceptTask.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAcceptByBarCode.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetTestTask.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAuditTask.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetDocMakeTask.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetReportTask.addEventListener(FaultEvent.FAULT,faultHandler);
				
				ws.GetNextSLN.addEventListener(ResultEvent.RESULT,getNextNoResultHandler);
				ws.GetNextSLN.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetCaseNextSLN.addEventListener(ResultEvent.RESULT,getNextNoResultHandler);
				ws.GetCaseNextSLN.addEventListener(FaultEvent.FAULT,faultHandler);				
				
				ws.QueryAllCase.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.QueryAllCase.addEventListener(FaultEvent.FAULT,faultHandler);
				
				ws.PrintTz.addEventListener(ResultEvent.RESULT,tzTableHandler);
				ws.PrintTz.addEventListener(FaultEvent.FAULT,faultHandler);
				
				ws.PrintFWJL.addEventListener(ResultEvent.RESULT,tzTableHandler);
				ws.PrintFWJL.addEventListener(FaultEvent.FAULT,faultHandler);

			}
			return ws;
		}
		//Handler
		private function tzTableHandler(evt:ResultEvent):void
		{
			var result:String=evt.result.toString();
			navigateToURL(new URLRequest(result));
			PsbLocator.getInstance().npending=true;
		}
		private function resultHandler(evt:ResultEvent):void
		{
			var result:String=evt.result.toString();
			if(result=="1")
			{
				responder.onResult();	
			}	
			else
			{
				responder.onFault(result);	
			}
			PsbLocator.getInstance().npending=true;
		}
		private function getNextNoResultHandler(evt:ResultEvent):void
		{
			responder.onResult(evt.result.toString());
			PsbLocator.getInstance().npending=true;
		}
		private function getXmlResultHandler(evt:ResultEvent):void
		{
			responder.onResult(new XML(evt.result));		
			PsbLocator.getInstance().npending=true;
		}
		private function faultHandler(evt:FaultEvent):void
		{
			responder.onFault(evt.fault.faultString);	
			PsbLocator.getInstance().npending=true;
		}
		//Object
		private var responder:IFSCommand;
		//External calltring CASE_ID, string CONSIGNID, string MPID, string 委托编号, string ORA_
		public function cancelFunc():void
		{
			var caseid:String="";
			var CONSIGNID:String="";
			var conno:String=IdFlowLocator.getInstance().curObj.CONNO;
			var mpid:String="";
			var ora:String="";
			if(conno.search("D")==0)
			{
				caseid=IdFlowLocator.getInstance().curObj.ID;
				CONSIGNID=IdFlowLocator.getInstance().curObj.CONSIGNID;
				ora=IdFlowLocator.getInstance().curObj.ORA_FLAG;
			}
			else if(conno.search("L")==0)
			{
				CONSIGNID=IdFlowLocator.getInstance().curObj.CONSIGNID;
				mpid=MissingPersonLocator.getInstance().curObj.ID;
				ora=MissingPersonLocator.getInstance().curObj.ORA_FLAG;
			}	
			else if(conno.search("R")==0)
			{
				CONSIGNID=IdFlowLocator.getInstance().curObj.CONSIGNID;
				ora=MRelativeLocator.getInstance().curObj.ORA_FLAG;
			}
			Ws.Cancel.send(caseid,CONSIGNID,mpid,conno,ora);
			PsbLocator.getInstance().npending=false;
		}
		public function updateAccept():void
		{
			var voObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;			
			var sesln:String="";
			if(voObj.CONNO.search("C")==0)
			{
				sesln=JusTypeLocator.getInstance().getJusType(voObj.ID_OFFICE,voObj.ID_JUSTYPE).SESLN;
			}
			Ws.UpdateAccept.send(voObj.ID,voObj.CONNO,voObj.ACC_YEAR,voObj.ACC_NO,voObj.ACC_CASE_NO,voObj.ACCER,voObj.ACC_TIME,
				voObj.PLAN_DATE,voObj.RZRK,voObj.ACC_REMARK,voObj.TESTER,voObj.ID_STATUS,
				sesln,
				voObj.DOC_NAME,PsbLocator.getInstance().ID_PSB_ID,voObj.ID_Method);
			PsbLocator.getInstance().npending=false;
		}
		public function updateAcceptMp():void
		{
			var voObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;			
			Ws.UpdateAcceptMp.send(voObj.ID,voObj.CONNO,voObj.ACC_YEAR,voObj.ACC_NO,voObj.ACC_CASE_NO,voObj.ACCER,voObj.ACC_TIME,
				voObj.PLAN_DATE,voObj.RZRK,voObj.ACC_REMARK,voObj.TESTER,voObj.ID_STATUS,
				MissingPersonLocator.getInstance().wsObj.SLN,voObj.DOC_NAME,PsbLocator.getInstance().ID_PSB_ID,voObj.ID_Method);
			PsbLocator.getInstance().npending=false;
		}
		public function updateAcceptMpr():void
		{
			var voObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;			
			Ws.UpdateAcceptMpr.send(MRelativeLocator.getInstance().wsObj.ID,MRelativeLocator.getInstance().wsObj.RELATIVE1_ID,MRelativeLocator.getInstance().wsObj.RELATIVE2_ID,
				voObj.CONNO,voObj.ACC_YEAR,voObj.ACC_NO,voObj.ACC_CASE_NO,voObj.ACCER,voObj.ACC_TIME,
				voObj.PLAN_DATE,voObj.RZRK,voObj.ACC_REMARK,voObj.TESTER,voObj.ID_STATUS,
				MRelativeLocator.getInstance().wsObj.R1_SLN,MRelativeLocator.getInstance().wsObj.R2_SLN,voObj.DOC_NAME,PsbLocator.getInstance().ID_PSB_ID,voObj.ID_Method);
			PsbLocator.getInstance().npending=false;
		}
		public function updateAcceptDna():void
		{
			var locObj:IdFlowLocator=IdFlowLocator.getInstance();
			var wsObj:IdFlowVo=locObj.wsObj;
			var sepre:String=Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.SESLN,wsObj.ACC_YEAR,wsObj.ACC_CASE_NO,"");
			var cpspre:String=Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.CPSSLN,wsObj.ACC_YEAR,wsObj.ACC_CASE_NO,"");
			var crpre:String=Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.RSLN,wsObj.ACC_YEAR,wsObj.ACC_CASE_NO,"");
			var udpre:String=Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.USLN,wsObj.ACC_YEAR,wsObj.ACC_CASE_NO,"");
			if(wsObj.ID!=wsObj.SRCID)
			{
				sepre=Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.SESLN,IdCaseLocator.getInstance().bsMainObj.ACC_YEAR,IdCaseLocator.getInstance().bsMainObj.ACC_CASE_NO,"");
				cpspre=Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.CPSSLN,IdCaseLocator.getInstance().bsMainObj.ACC_YEAR,IdCaseLocator.getInstance().bsMainObj.ACC_CASE_NO,"");
				crpre=Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.RSLN,IdCaseLocator.getInstance().bsMainObj.ACC_YEAR,IdCaseLocator.getInstance().bsMainObj.ACC_CASE_NO,"");
				udpre=Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.USLN,IdCaseLocator.getInstance().bsMainObj.ACC_YEAR,IdCaseLocator.getInstance().bsMainObj.ACC_CASE_NO,"");
			}			
			Ws.UpdateAcceptDna.send(wsObj.ID,wsObj.SRCID,
				wsObj.CONNO,wsObj.ACC_YEAR,wsObj.ACC_NO,wsObj.ACC_CASE_NO,wsObj.ACCER,wsObj.ACC_TIME,
				wsObj.PLAN_DATE,wsObj.RZRK,wsObj.ACC_REMARK,wsObj.TESTER,wsObj.ID_STATUS,
				wsObj.CASE_NO,JusTypeLocator.getInstance().jusTypeVo.WholeNo,
				sepre,cpspre,crpre,udpre,
				locObj.preFixSe,locObj.preFixCps,locObj.preFixCr,locObj.preFixUd,
				Helper.getSlnNumLen(JusTypeLocator.getInstance().jusTypeVo.SESLN),
				Helper.getSlnNumLen(JusTypeLocator.getInstance().jusTypeVo.CPSSLN),
				Helper.getSlnNumLen(JusTypeLocator.getInstance().jusTypeVo.RSLN),
				Helper.getSlnNumLen(JusTypeLocator.getInstance().jusTypeVo.USLN),wsObj.DOC_NAME,PsbLocator.getInstance().ID_PSB_ID,wsObj.ID_Method);
			PsbLocator.getInstance().npending=false;
		}
		public function updateConclusion():void
		{
			var locObj:IdFlowLocator=IdFlowLocator.getInstance();
			var wsObj:IdFlowVo=locObj.wsObj;
			Ws.UpdateConclusion.send(wsObj.CONNO,wsObj.CONCLUSION,wsObj.CONCLUSION_REMARK,wsObj.ID);
			PsbLocator.getInstance().npending=false;
		}
		public function updateTesterFinish():void
		{
			var locObj:IdFlowLocator=IdFlowLocator.getInstance();
			var wsObj:IdFlowVo=locObj.wsObj;
			Ws.UpdateTesterFinish.send(wsObj.ID_PSB,wsObj.CONNO,wsObj.TESTER_REMARK,
				wsObj.TESTER2,wsObj.TESTER3,wsObj.TESTER4,wsObj.CHECKER,wsObj.SIGNER,wsObj.TECHER,wsObj.LEADER,
				locObj.isSubmit,locObj.operReason,wsObj.DOC_YEAR,wsObj.DOC_NO);
			PsbLocator.getInstance().npending=false;
		}
		public function updateAudit():void
		{
			var locObj:IdFlowLocator=IdFlowLocator.getInstance();
			var wsObj:IdFlowVo=locObj.wsObj;
			Ws.UpdateAudit.send(wsObj.CONNO,locObj.operTimeCol,locObj.oper,locObj.operReason,wsObj.ID_STATUS);
			PsbLocator.getInstance().npending=false;
		}
		public function updateRepGet():void
		{
			var wsObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;
			Ws.UpdateRepGet.send(wsObj.CONNO,wsObj.SDER,wsObj.GP1NAME,wsObj.GP1PHONE,wsObj.GP2NAME,wsObj.GP2PHONE,wsObj.ID_STATUS);
			PsbLocator.getInstance().npending=false;
		}		
		public function getNextConNo():void
		{
			var wsObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;
			Ws.GetNextConNo.send(wsObj.CON_PSBNICKNAME,
				wsObj.CON_YEAR,wsObj.CONNO,PsbLocator.getInstance().ID_PSB_ID);
			PsbLocator.getInstance().npending=false;
		}
		public function getNextAccNo():void
		{
			var wsObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;
			Ws.GetNextAccNo.send(wsObj.ID_PSB,wsObj.ACC_YEAR,wsObj.DOC_NAME,wsObj.CONNO);
			PsbLocator.getInstance().npending=false;
		}
		public function getNextDocNo():void
		{
			var wsObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;
			Ws.GetNextDocNo.send(wsObj.ID_PSB,wsObj.DOC_YEAR,wsObj.DOC_NAME,wsObj.CONNO);
			PsbLocator.getInstance().npending=false;
		}		
		public function getCaseAcceptTask():void
		{
			var listPager:ListPager=IdFlowLocator.getInstance().listPager;
			var wsObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;
			Ws.GetCaseAcceptTask.send(wsObj.ID_PSB,wsObj.CASE_NAME,wsObj.ID_OFFICE,wsObj.ID_JUSTYPE,
				listPager.pageSize,listPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
		public function getAcceptByBarCode():void
		{
			var voObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;
			Ws.GetAcceptByBarCode.send(voObj.ID_PSB,voObj.CONNO);
			PsbLocator.getInstance().npending=false;
		}
		public function getTestTask():void
		{
			var listPager:ListPager=IdFlowLocator.getInstance().listPager;
			var wsObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;			
			Ws.GetTestTask.send(wsObj.ID_PSB,wsObj.TESTER,wsObj.CASE_NAME,wsObj.ACC_CASE_NO,listPager.pageSize,listPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
		public function getAuditTask():void
		{
			var listPager:ListPager=IdFlowLocator.getInstance().listPager;
			var wsObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;			
			Ws.GetAuditTask.send(wsObj.ID_PSB,wsObj.TESTER,listPager.pageSize,listPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
		public function getDocMakeTask():void
		{
			var listPager:ListPager=IdFlowLocator.getInstance().listPager;
			var wsObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;				
			Ws.GetDocMakeTask.send(wsObj.ID_PSB,wsObj.TESTER,wsObj.CASE_NAME,wsObj.ACC_CASE_NO,listPager.pageSize,listPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
		public function getReportTask():void
		{
			var listPager:ListPager=IdFlowLocator.getInstance().listPager;
			var wsObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;			
			Ws.GetReportTask.send(wsObj.ID_PSB,wsObj.CONNO,wsObj.CASE_NAME,listPager.pageSize,listPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
		public function getNextSLN():void
		{
			var locObj:IdFlowLocator=IdFlowLocator.getInstance();
			Ws.GetNextSLN.send(locObj.preFixSe,locObj.tableName);
			PsbLocator.getInstance().npending=false;
		}
		public function getCaseNextSLN():void
		{
			var locObj:IdFlowLocator=IdFlowLocator.getInstance();
			Ws.GetCaseNextSLN.send(locObj.preFixSe,locObj.preFixCps,locObj.preFixCr,locObj.preFixUd);
			PsbLocator.getInstance().npending=false;
		}
		public function queryAllCase():void
		{
			var locObj:IdFlowLocator=IdFlowLocator.getInstance();
			var listPager:ListPager=locObj.listPager;
			var wsObj:IdFlowVo=locObj.wsObj;	
			Ws.QueryAllCase.send(wsObj.DFGKNO,wsObj.XCKYNO,wsObj.CASE_NAME,wsObj.CASE_TYPE,wsObj.CASE_NO,wsObj.SCENE_PLACE,locObj.sOccDate,locObj.eOccDate,
				wsObj.CASE_PROPERTY,
				wsObj.ID_PSB,wsObj.ID_STATUS,wsObj.CON_PSB,locObj.conPsbName,wsObj.CON_YEAR,wsObj.CON_NO,locObj.sConDate,locObj.eConDate,wsObj.ID_OFFICE,
				wsObj.ID_JUSTYPE,wsObj.ID_JUSITEM,wsObj.ACC_YEAR,wsObj.ACC_NO,wsObj.DOC_YEAR,wsObj.DOC_NO,wsObj.ACCER,locObj.sAccDate,locObj.eAccDate,wsObj.RZRK,
				wsObj.CONCLUSION,wsObj.TESTER,wsObj.TESTER2,wsObj.TESTER3,wsObj.TESTER4,wsObj.CHECKER,wsObj.SIGNER,wsObj.TECHER,wsObj.LEADER,wsObj.SDER,locObj.sGpDate,
				locObj.eGpDate,wsObj.GP1NAME,wsObj.BJDR,locObj.listPager.pageSize,locObj.listPager.pageIndex);
			PsbLocator.getInstance().npending=false;			
		}
		public function printTz():void
		{
			var locObj:IdFlowLocator=IdFlowLocator.getInstance();
			var wsObj:IdFlowVo=locObj.wsObj;	
			Ws.PrintTz.send(wsObj.DFGKNO,wsObj.XCKYNO,wsObj.CASE_NAME,wsObj.CASE_TYPE,wsObj.CASE_NO,wsObj.SCENE_PLACE,locObj.sOccDate,locObj.eOccDate,
				wsObj.CASE_PROPERTY,
				wsObj.ID_PSB,wsObj.ID_STATUS,wsObj.CON_PSB,locObj.conPsbName,wsObj.CON_YEAR,wsObj.CON_NO,locObj.sConDate,locObj.eConDate,wsObj.ID_OFFICE,
				wsObj.ID_JUSTYPE,wsObj.ID_JUSITEM,wsObj.ACC_YEAR,wsObj.ACC_NO,wsObj.DOC_YEAR,wsObj.DOC_NO,wsObj.ACCER,locObj.sAccDate,locObj.eAccDate,wsObj.RZRK,
				wsObj.CONCLUSION,wsObj.TESTER,wsObj.TESTER2,wsObj.TESTER3,wsObj.TESTER4,wsObj.CHECKER,wsObj.SIGNER,wsObj.TECHER,wsObj.LEADER,wsObj.SDER,locObj.sGpDate,
				locObj.eGpDate,wsObj.GP1NAME,wsObj.BJDR);
			PsbLocator.getInstance().npending=false;			
		}
		public function printFWJL():void
		{
			var locObj:IdFlowLocator=IdFlowLocator.getInstance();
			var wsObj:IdFlowVo=locObj.wsObj;	
			Ws.PrintFWJL.send(wsObj.DFGKNO,wsObj.XCKYNO,wsObj.CASE_NAME,wsObj.CASE_TYPE,wsObj.CASE_NO,wsObj.SCENE_PLACE,locObj.sOccDate,locObj.eOccDate,
				wsObj.CASE_PROPERTY,
				wsObj.ID_PSB,wsObj.ID_STATUS,wsObj.CON_PSB,locObj.conPsbName,wsObj.CON_YEAR,wsObj.CON_NO,locObj.sConDate,locObj.eConDate,wsObj.ID_OFFICE,
				wsObj.ID_JUSTYPE,wsObj.ID_JUSITEM,wsObj.ACC_YEAR,wsObj.ACC_NO,wsObj.DOC_YEAR,wsObj.DOC_NO,wsObj.ACCER,locObj.sAccDate,locObj.eAccDate,wsObj.RZRK,
				wsObj.CONCLUSION,wsObj.TESTER,wsObj.TESTER2,wsObj.TESTER3,wsObj.TESTER4,wsObj.CHECKER,wsObj.SIGNER,wsObj.TECHER,wsObj.LEADER,wsObj.SDER,locObj.sGpDate,
				locObj.eGpDate,wsObj.GP1NAME,locObj.filename);
			PsbLocator.getInstance().npending=false;			
		}

	}
}