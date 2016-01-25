package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.CodiesLocator;
	import locator.IdCaseLocator;
	import locator.IdFlowLocator;
	import locator.PsbLocator;
	import locator.SysUserLocator;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	import util.Helper;
	import vo.IdFlowVo;
	
	public class IdCaseDelegate
	{
		public function IdCaseDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("案件信息WS"));
				
				ws.Insert.addEventListener(ResultEvent.RESULT,insertHandler);
				ws.Update.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.DeleteC.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.DeleteD.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetAllD.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetAcceptDup.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.UpdateBsInfo.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetOneRecord.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.ImportToOraCase.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetYearStaData.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetAllJds.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				
				ws.Insert.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Update.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.DeleteC.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.DeleteD.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAllD.addEventListener(FaultEvent.FAULT,faultHandler);				
				ws.GetAcceptDup.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateBsInfo.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetOneRecord.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.ImportToOraCase.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetYearStaData.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAllJds.addEventListener(FaultEvent.FAULT,faultHandler);
				
			}
			return ws;
		}
		//Handler
		private function insertHandler(evt:ResultEvent):void
		{
			var result:String=evt.result.toString();
			if(result.length==19)
			{
				if((result.search("D")==0)||(result.search("C")==0))
				{
					responder.onResult(result);	
				}
			}
			else
			{
				responder.onFault(result);	
			}
			PsbLocator.getInstance().npending=true;
		}
		private function impHandler(evt:ResultEvent):void
		{
			var result:String=evt.result.toString();
			if(result=="1")
			{
				Helper.showAlert("案件及检材的基本信息已导入，请再点击“导入STR”");
				CodiesLocator.getInstance().orc_imp=true;
			}	
			else
			{
				responder.onFault(result);	
			}
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
		//External call
		public function insert():void
		{		
			var voObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;
			Ws.Insert.send(voObj.ID,voObj.CONNO,voObj.DFGKNO,voObj.XCKYNO,voObj.CASE_NAME,voObj.CASE_TYPE,voObj.CASE_TYPE2,voObj.CASE_NO,
				voObj.SCENE_PLACE,voObj.RegionCode,voObj.OCCURRENCE_DATE,voObj.CASE_PROPERTY,voObj.CASE_SUMMARY,voObj.SRCID,
				voObj.ID_PSB,voObj.CONSIGNID,
				voObj.CON_PSB,voObj.CON_PSBNICKNAME,voObj.CONER1,
				voObj.CONER1NAME2,voObj.CONER1POLICENO,voObj.CONER1PHONE,
				voObj.CONER2NAME,voObj.CONER2POLICENO,voObj.CONER2PHONE,
				voObj.CON_YEAR,voObj.CON_NO,voObj.CON_DATE,voObj.ID_OFFICE,voObj.ID_JUSTYPE,voObj.ID_JUSITEM,
				voObj.ID_REQUEST,voObj.DOC_NAME,voObj.OrgIdResult,voObj.ACC_CASE_NO);			
			PsbLocator.getInstance().npending=false;
		}
		public function update():void
		{
			var voObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;	
			Ws.Update.send(voObj.ID,voObj.CONNO,voObj.DFGKNO,voObj.XCKYNO,voObj.CASE_NAME,voObj.CASE_TYPE,voObj.CASE_TYPE2,voObj.CASE_NO,
				voObj.SCENE_PLACE,voObj.RegionCode,voObj.OCCURRENCE_DATE,voObj.CASE_PROPERTY,voObj.CASE_SUMMARY,
				voObj.ID_PSB,voObj.CONSIGNID,
				voObj.CON_PSB,voObj.CON_PSBNICKNAME,voObj.CONER1,
				voObj.CONER1NAME2,voObj.CONER1POLICENO,voObj.CONER1PHONE,
				voObj.CONER2NAME,voObj.CONER2POLICENO,voObj.CONER2PHONE,
				voObj.CON_YEAR,voObj.CON_NO,voObj.CON_DATE,voObj.ID_JUSTYPE,voObj.ID_JUSITEM,voObj.ID_REQUEST,voObj.DOC_NAME,
				voObj.OrgIdResult,voObj.ORA_FLAG);			
			PsbLocator.getInstance().npending=false;
		}
		public function deleteC():void
		{
			var voObj:IdFlowVo=IdFlowLocator.getInstance().curObj;
			Ws.DeleteC.send(voObj.ID,voObj.CONNO);			
			PsbLocator.getInstance().npending=false;
		}
		public function deleteD():void
		{
			var voObj:IdFlowVo=IdFlowLocator.getInstance().curObj;
			Ws.DeleteD.send(voObj.ID,voObj.CONNO,voObj.CONSIGNID,voObj.ORA_FLAG);			
			PsbLocator.getInstance().npending=false;
		}
		public function getAllD():void
		{
			var listPager:ListPager=IdFlowLocator.getInstance().listPager;
			var voObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;
			Ws.GetAllD.send(voObj.CASE_NAME,voObj.CASE_NO,voObj.ID_PSB,listPager.pageSize,listPager.pageIndex);			
			PsbLocator.getInstance().npending=false;
		}
		public function getAcceptDup():void
		{
			var listPager:ListPager=IdFlowLocator.getInstance().listPager;
			var voObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;
			Ws.GetAcceptDup.send(voObj.CONNO,listPager.pageSize,listPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
		public function updateBsInfo():void
		{
			var wsObj:IdFlowVo=IdFlowLocator.getInstance().wsObj
			Ws.UpdateBsInfo.send(wsObj.ID,wsObj.CONNO,wsObj.SRCID,wsObj.CASE_NO,wsObj.ACC_CASE_NO);
			PsbLocator.getInstance().npending=false;
		}
		public function getOneRecord():void
		{
			var wsObj:IdFlowVo=IdCaseLocator.getInstance().bsWsObj;
			Ws.GetOneRecord.send(wsObj.SRCID);
			PsbLocator.getInstance().npending=false;
		}
		public function importToOraCase():void
		{
			var wsObj:IdFlowVo=IdFlowLocator.getInstance().curObj
			Ws.ImportToOraCase.send(wsObj.SRCID,SysUserLocator.getInstance().loginUser.NAME,IdCaseLocator.getInstance().year);
			PsbLocator.getInstance().npending=false;
		}
		public function getYearStaData():void
		{
			Ws.GetYearStaData.send(PsbLocator.getInstance().ID_PSB_ID,
				IdCaseLocator.getInstance().year,
				SysUserLocator.getInstance().loginUser.OFFICE);
			PsbLocator.getInstance().npending=false;
		}
		public function getAllJds():void
		{
			Ws.GetAllJds.send(PsbLocator.getInstance().ID_PSB_ID,
				IdCaseLocator.getInstance().starTime,
				IdCaseLocator.getInstance().endTime,
				SysUserLocator.getInstance().loginUser.OFFICE);
			PsbLocator.getInstance().npending=false;
		}
	}
}