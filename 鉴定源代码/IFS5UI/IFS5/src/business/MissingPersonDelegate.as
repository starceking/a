package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.IdFlowLocator;
	import locator.MissingPersonLocator;
	import locator.PsbLocator;
	import locator.SysUserLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	import vo.IdFlowVo;
	import vo.MissingPersonVo;
	
	public class MissingPersonDelegate
	{
		public function MissingPersonDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("失踪人员WS"));
				
				ws.Insert.addEventListener(ResultEvent.RESULT,insertHandler);
				ws.Update.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Delete.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetOneMp.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.ImportToOraMp.addEventListener(ResultEvent.RESULT,resultHandler);
				
				ws.Insert.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Update.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Delete.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetOneMp.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.ImportToOraMp.addEventListener(FaultEvent.FAULT,faultHandler);
			}
			return ws;
		}
		//Handler
		private function insertHandler(evt:ResultEvent):void
		{
			var result:String=evt.result.toString();
			if((result.length==19)&&(result.search("L")==0))
			{
				responder.onResult(result);	
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
			var wsObj:MissingPersonVo=MissingPersonLocator.getInstance().wsObj;
			var voObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;
			Ws.Insert.send(wsObj.ID,wsObj.CONNO,wsObj.NAME,wsObj.SAMPLE_TYPE,wsObj.GENDER,
				wsObj.PERSONNEL_TYPE,wsObj.BIRTH_DATE,wsObj.NATIONALITY,wsObj.DISTRICT,wsObj.ID_CARD_NO,wsObj.EDUCATION_LEVEL,
				wsObj.IDENTITY,wsObj.NATIVE_PLACE_ADDR,
				wsObj.RESIDENCE_ADDR,wsObj.SAMPLE_PACKAGING,wsObj.SAMPLE_DESCRIPTION,wsObj.REMARK,wsObj.CASE_NAME,
				voObj.ID_PSB,voObj.CONSIGNID,
				voObj.CON_PSB,voObj.CONER1,
				voObj.CONER1NAME2,voObj.CONER1POLICENO,voObj.CONER1PHONE,
				voObj.CONER2NAME,voObj.CONER2POLICENO,voObj.CONER2PHONE,
				voObj.CON_YEAR,voObj.CON_NO,voObj.CON_DATE,voObj.ID_REQUEST,voObj.DOC_NAME);		
			PsbLocator.getInstance().npending=false;
		}
		public function update():void
		{
			var wsObj:MissingPersonVo=MissingPersonLocator.getInstance().wsObj;
			var voObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;			
			Ws.Update.send(wsObj.ID,wsObj.CONNO,wsObj.NAME,wsObj.SAMPLE_TYPE,wsObj.GENDER,
				wsObj.PERSONNEL_TYPE,wsObj.BIRTH_DATE,wsObj.NATIONALITY,wsObj.DISTRICT,wsObj.ID_CARD_NO,wsObj.EDUCATION_LEVEL,
				wsObj.IDENTITY,wsObj.NATIVE_PLACE_ADDR,
				wsObj.RESIDENCE_ADDR,wsObj.SAMPLE_PACKAGING,wsObj.SAMPLE_DESCRIPTION,wsObj.REMARK,wsObj.CASE_NAME,wsObj.SLN,
				voObj.CONER1NAME2,voObj.CONER1POLICENO,voObj.CONER1PHONE,
				voObj.CONER2NAME,voObj.CONER2POLICENO,voObj.CONER2PHONE,
				voObj.CON_YEAR,voObj.CON_NO,voObj.CON_DATE,voObj.ID_REQUEST,wsObj.ORA_FLAG);			
			PsbLocator.getInstance().npending=false;
		}
		public function deleteObj():void
		{
			var wsObj:MissingPersonVo=MissingPersonLocator.getInstance().curObj;
			Ws.Delete.send(wsObj.ID,IdFlowLocator.getInstance().curObj.CONSIGNID,wsObj.CONNO,wsObj.ORA_FLAG);			
			PsbLocator.getInstance().npending=false;
		}
		public function getOneMp():void
		{
			var locObj:MissingPersonLocator=MissingPersonLocator.getInstance();
			var wsObj:MissingPersonVo=locObj.wsObj;
			Ws.GetOneMp.send(wsObj.ID);			
			PsbLocator.getInstance().npending=false;
		}
		public function importToOraMp():void
		{
			var locObj:MissingPersonLocator=MissingPersonLocator.getInstance();
			Ws.ImportToOraMp.send(locObj.curObj.ID,SysUserLocator.getInstance().loginUser.NAME);			
			PsbLocator.getInstance().npending=false;
		}
	}
}