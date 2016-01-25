package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.IdFlowLocator;
	import locator.MRelativeLocator;
	import locator.PsbLocator;
	import locator.SysUserLocator;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	import vo.IdFlowVo;
	import vo.MRelativeVo;
	
	public class MRelativeDelegate
	{
		public function MRelativeDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("亲属定义WS"));
				
				ws.NewConsign.addEventListener(ResultEvent.RESULT,insertHandler);
				ws.UpdateR.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.DeleteR.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetOneMpr.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.ImportToOraMpr.addEventListener(ResultEvent.RESULT,resultHandler);
				
				ws.NewConsign.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateR.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.DeleteR.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetOneMpr.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.ImportToOraMpr.addEventListener(FaultEvent.FAULT,faultHandler);
			}
			return ws;
		}
		//Handler
		private function insertHandler(evt:ResultEvent):void
		{
			var result:String=evt.result.toString();
			if((result.length==19)&&(result.search("R")==0))
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
		public function newConsign():void
		{
			var wsObj:MRelativeVo=MRelativeLocator.getInstance().wsObj;
			var voObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;
			Ws.NewConsign.send(wsObj.ID,wsObj.CONNO,wsObj.RELATION,wsObj.RELATIVE1_ID,wsObj.RELATIVE2_ID,
				wsObj.PERSON_BIRTHDATE,wsObj.PERSON_GENDER,
				wsObj.PERSON_SPEC,wsObj.PERSON_SIGN,wsObj.PERSON_NAME,wsObj.CASE_NAME,wsObj.CASE_SUMMARY,
				voObj.ID_PSB,voObj.CONSIGNID,
				voObj.CON_PSB,voObj.CONER1,
				voObj.CONER1NAME2,voObj.CONER1POLICENO,voObj.CONER1PHONE,
				voObj.CONER2NAME,voObj.CONER2POLICENO,voObj.CONER2PHONE,
				voObj.CON_YEAR,voObj.CON_NO,voObj.CON_DATE,voObj.ID_REQUEST,voObj.DOC_NAME,
				wsObj.R1_NAME,wsObj.R1_SAMPLE_TYPE,wsObj.R1_GENDER,wsObj.R1_PERSONNEL_TYPE,wsObj.R1_BIRTH_DATE,wsObj.R1_NATIONALITY,
				wsObj.R1_DISTRICT,wsObj.R1_ID_CARD_NO,wsObj.R1_EDUCATION_LEVEL,wsObj.R1_IDENTITY,wsObj.R1_NATIVE_PLACE_ADDR,wsObj.R1_RESIDENCE_ADDR,
				wsObj.R1_SAMPLE_PACKAGING,wsObj.R1_SAMPLE_DESCRIPTION,wsObj.R1_RELATION_WITH_TARGET,wsObj.R1_REMARK,
				wsObj.R2_NAME,wsObj.R2_SAMPLE_TYPE,wsObj.R2_GENDER,wsObj.R2_PERSONNEL_TYPE,wsObj.R2_BIRTH_DATE,wsObj.R2_NATIONALITY,
				wsObj.R2_DISTRICT,wsObj.R2_ID_CARD_NO,wsObj.R2_EDUCATION_LEVEL,wsObj.R2_IDENTITY,wsObj.R2_NATIVE_PLACE_ADDR,wsObj.R2_RESIDENCE_ADDR,
				wsObj.R2_SAMPLE_PACKAGING,wsObj.R2_SAMPLE_DESCRIPTION,wsObj.R2_RELATION_WITH_TARGET,wsObj.R2_REMARK);			
			PsbLocator.getInstance().npending=false;
		}
		public function updateR():void
		{
			var wsObj:MRelativeVo=MRelativeLocator.getInstance().wsObj;
			var voObj:IdFlowVo=IdFlowLocator.getInstance().curObj;
			Ws.UpdateR.send(wsObj.ID,wsObj.CONNO,wsObj.RELATION,wsObj.RELATIVE1_ID,wsObj.RELATIVE2_ID,wsObj.PERSON_BIRTHDATE,wsObj.PERSON_GENDER,
				wsObj.PERSON_SPEC,wsObj.PERSON_SIGN,wsObj.PERSON_NAME,wsObj.CASE_NAME,wsObj.CASE_SUMMARY,
				voObj.CONER1NAME2,voObj.CONER1POLICENO,voObj.CONER1PHONE,
				voObj.CONER2NAME,voObj.CONER2POLICENO,voObj.CONER2PHONE,
				voObj.CON_YEAR,voObj.CON_NO,voObj.CON_DATE,voObj.ID_REQUEST,
				wsObj.R1_NAME,wsObj.R1_SAMPLE_TYPE,wsObj.R1_GENDER,wsObj.R1_PERSONNEL_TYPE,wsObj.R1_BIRTH_DATE,wsObj.R1_NATIONALITY,
				wsObj.R1_DISTRICT,wsObj.R1_ID_CARD_NO,wsObj.R1_EDUCATION_LEVEL,wsObj.R1_IDENTITY,wsObj.R1_NATIVE_PLACE_ADDR,wsObj.R1_RESIDENCE_ADDR,
				wsObj.R1_SAMPLE_PACKAGING,wsObj.R1_SAMPLE_DESCRIPTION,wsObj.R1_RELATION_WITH_TARGET,wsObj.R1_REMARK,wsObj.R1_SLN,
				wsObj.R2_NAME,wsObj.R2_SAMPLE_TYPE,wsObj.R2_GENDER,wsObj.R2_PERSONNEL_TYPE,wsObj.R2_BIRTH_DATE,wsObj.R2_NATIONALITY,
				wsObj.R2_DISTRICT,wsObj.R2_ID_CARD_NO,wsObj.R2_EDUCATION_LEVEL,wsObj.R2_IDENTITY,wsObj.R2_NATIVE_PLACE_ADDR,wsObj.R2_RESIDENCE_ADDR,
				wsObj.R2_SAMPLE_PACKAGING,wsObj.R2_SAMPLE_DESCRIPTION,wsObj.R2_RELATION_WITH_TARGET,wsObj.R2_REMARK,wsObj.R2_SLN,wsObj.ORA_FLAG);			
			PsbLocator.getInstance().npending=false;
		}
		public function deleteR():void
		{
			var wsObj:MRelativeVo=MRelativeLocator.getInstance().curObj;
			Ws.DeleteR.send(wsObj.ID,IdFlowLocator.getInstance().curObj.CONSIGNID,wsObj.CONNO,wsObj.RELATIVE1_ID,wsObj.RELATIVE2_ID,wsObj.ORA_FLAG);			
			PsbLocator.getInstance().npending=false;
		}
		public function getOneMpr():void
		{
			var locObj:MRelativeLocator=MRelativeLocator.getInstance();
			var wsObj:MRelativeVo=locObj.wsObj;
			Ws.GetOneMpr.send(wsObj.ID);			
			PsbLocator.getInstance().npending=false;
		}
		public function importToOraMpr():void
		{
			var locObj:MRelativeLocator=MRelativeLocator.getInstance();
			var curObj:MRelativeVo=locObj.curObj;
			Ws.ImportToOraMpr.send(curObj.ID,SysUserLocator.getInstance().loginUser.NAME);			
			PsbLocator.getInstance().npending=false;
		}
	}
}