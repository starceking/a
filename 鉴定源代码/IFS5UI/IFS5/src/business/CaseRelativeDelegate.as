package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.CaseRelativeShrLocator;
	import locator.CaseRelativeXyrLocator;
	import locator.PsbLocator;
	import locator.IdCaseLocator;
	import locator.JusTypeLocator;
	import locator.SysUserLocator;
	import locator.IdFlowLocator;
	
	import util.Helper;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	import vo.CaseRelativeVo;
	
	public class CaseRelativeDelegate
	{
		public function CaseRelativeDelegate(responder:IFSCommand)
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
				
				ws.Insert.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.InsertWithNo.addEventListener(ResultEvent.RESULT,insertNoResultHandler);
				ws.Update.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Delete.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetAll.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				
				ws.Insert.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.InsertWithNo.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Update.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Delete.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAll.addEventListener(FaultEvent.FAULT,faultHandler);
			}
			return ws;
		}
		//Handler
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
			updatePending(true);
		}
		private function insertNoResultHandler(evt:ResultEvent):void
		{
			var result:String=evt.result.toString();
			if(result=="0")
			{
				responder.onFault(result);		
			}	
			else
			{
				responder.onResult(result);
			}
			updatePending(true);
		}
		private function getXmlResultHandler(evt:ResultEvent):void
		{
			responder.onResult(new XML(evt.result));	
			updatePending(true);
		}
		private function faultHandler(evt:FaultEvent):void
		{
			responder.onFault(evt.fault.faultString);	
			updatePending(true);
		}
		//Object
		private var responder:IFSCommand;
		//External call
		public function insert():void
		{
			var wsObj:CaseRelativeVo=getVo();
			Ws.Insert.send(wsObj.ID,wsObj.CASE_ID,wsObj.CONNO,wsObj.SAMPLE_CATEGORY,wsObj.RELATION,wsObj.RELATIVE1_ID,wsObj.RELATIVE2_ID,wsObj.PERSONE_NAME,
				wsObj.R1_NAME,wsObj.R1_SAMPLE_TYPE,wsObj.R1_GENDER,wsObj.R1_ID_CARD_NO,wsObj.R1_NATIVE_PLACE_ADDR,wsObj.R1_SAMPLE_DESCRIPTION,
				wsObj.R1_RELATION_WITH_TARGET,
				wsObj.R2_NAME,wsObj.R2_SAMPLE_TYPE,wsObj.R2_GENDER,wsObj.R2_ID_CARD_NO,wsObj.R2_NATIVE_PLACE_ADDR,wsObj.R2_SAMPLE_DESCRIPTION,
				wsObj.R2_RELATION_WITH_TARGET);
			updatePending(false);
		}
		public function insertWithNo():void
		{
			var wsObj:CaseRelativeVo=getVo();
			Ws.InsertWithNo.send(wsObj.ID,wsObj.CASE_ID,wsObj.CONNO,wsObj.SAMPLE_CATEGORY,wsObj.RELATION,wsObj.RELATIVE1_ID,wsObj.RELATIVE2_ID,wsObj.PERSONE_NAME,
				wsObj.R1_NAME,wsObj.R1_SAMPLE_TYPE,wsObj.R1_GENDER,wsObj.R1_ID_CARD_NO,wsObj.R1_NATIVE_PLACE_ADDR,wsObj.R1_SAMPLE_DESCRIPTION,
				wsObj.R1_RELATION_WITH_TARGET,
				wsObj.R2_NAME,wsObj.R2_SAMPLE_TYPE,wsObj.R2_GENDER,wsObj.R2_ID_CARD_NO,wsObj.R2_NATIVE_PLACE_ADDR,wsObj.R2_SAMPLE_DESCRIPTION,
				wsObj.R2_RELATION_WITH_TARGET,
				Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.RSLN,
					IdCaseLocator.getInstance().bsMainObj.ACC_YEAR,IdCaseLocator.getInstance().bsMainObj.ACC_CASE_NO,""),
				JusTypeLocator.getInstance().jusTypeVo.WholeNo=="1"?"样本视图":"亲属信息",
				Helper.getSlnNumLen(JusTypeLocator.getInstance().jusTypeVo.RSLN),
			IdFlowLocator.getInstance().curObj.CONSIGNID,SysUserLocator.getInstance().loginUser.NAME,IdFlowLocator.getInstance().curObj.ORA_FLAG);
			updatePending(false);
		}
		public function update():void
		{
			var wsObj:CaseRelativeVo=getVo();
			Ws.Update.send(wsObj.ID,wsObj.RELATIVE1_ID,wsObj.RELATIVE2_ID,wsObj.PERSONE_NAME,wsObj.RELATION,
				wsObj.R1_NAME,wsObj.R1_SAMPLE_TYPE,wsObj.R1_GENDER,wsObj.R1_ID_CARD_NO,wsObj.R1_NATIVE_PLACE_ADDR,wsObj.R1_SAMPLE_DESCRIPTION,
				wsObj.R1_RELATION_WITH_TARGET,wsObj.R1_SLN,
				wsObj.R2_NAME,wsObj.R2_SAMPLE_TYPE,wsObj.R2_GENDER,wsObj.R2_ID_CARD_NO,wsObj.R2_NATIVE_PLACE_ADDR,wsObj.R2_SAMPLE_DESCRIPTION,
				wsObj.R2_RELATION_WITH_TARGET,wsObj.R2_SLN,wsObj.ORA_FLAG);
			updatePending(false);
		}
		public function deleteObj():void
		{
			var wsObj:CaseRelativeVo=getVo();
			Ws.Delete.send(wsObj.ID,wsObj.RELATIVE1_ID,wsObj.RELATIVE2_ID,wsObj.ORA_FLAG);
			updatePending(false);
		}
		public function getAll():void
		{
			var wsObj:CaseRelativeVo=getVo();
			Ws.GetAll.send(wsObj.CASE_ID,wsObj.CONNO,wsObj.SAMPLE_CATEGORY);
			updatePending(false);
		}
		private function getVo():CaseRelativeVo
		{
			var wsObj:CaseRelativeVo=null;
			if(responder.evtType.search("CASERELATIVE_SHRWS_")==0)
			{
				wsObj=CaseRelativeShrLocator.getInstance().wsObj;
			}
			else if(responder.evtType.search("CASERELATIVE_XYRWS_")==0)
			{
				wsObj=CaseRelativeXyrLocator.getInstance().wsObj;
			}	
			return wsObj;
		}
		private function updatePending(rel:Boolean):void
		{
			PsbLocator.getInstance().npending=rel;	
		}
	}
}