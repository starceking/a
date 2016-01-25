package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.IdCtrLocator;
	import locator.IdFlowLocator;
	import locator.IdTestimonyLocator;
	import locator.PsbLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	import vo.IdFlowVo;
	import vo.IdTestimonyVo;
	
	public class IdTestimonyDelegate
	{
		public function IdTestimonyDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("鉴定材料WS"));
				
				ws.Insert.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Update.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Delete.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetAll.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.Query.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.TesOper.addEventListener(ResultEvent.RESULT,resultHandler);
				
				ws.Insert.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Update.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Delete.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAll.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Query.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.TesOper.addEventListener(FaultEvent.FAULT,faultHandler);
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
			var wsObj:IdTestimonyVo=getVo();
			Ws.Insert.send(wsObj.ID,wsObj.IsCtr,wsObj.CONNO,wsObj.NAME,wsObj.AMOUNT,wsObj.WEIGHT,wsObj.PACKAGE,wsObj.PROPERTY,wsObj.EX_PERSON,
				wsObj.EX_METHOD,wsObj.EX_POSITION,wsObj.EX_DATE,wsObj.REMARK,wsObj.SLN,wsObj.UNIT);
			updatePending(false);
		}
		public function update():void
		{
			var wsObj:IdTestimonyVo=getVo();
			Ws.Update.send(wsObj.ID,wsObj.SLN,wsObj.NAME,wsObj.AMOUNT,wsObj.WEIGHT,wsObj.PACKAGE,wsObj.PROPERTY,wsObj.EX_PERSON,
				wsObj.EX_METHOD,wsObj.EX_POSITION,wsObj.EX_DATE,wsObj.REMARK,wsObj.UNIT);
			updatePending(false);
		}
		public function deleteObj():void
		{
			var wsObj:IdTestimonyVo=getVo();
			Ws.Delete.send(wsObj.ID);
			updatePending(false);
		}
		public function getAll():void
		{
			var wsObj:IdTestimonyVo=getVo();
			Ws.GetAll.send(wsObj.CONNO,wsObj.IsCtr);
			updatePending(false);
		}
		public function query():void
		{
			var locObj:IdTestimonyLocator=IdTestimonyLocator.getInstance();
			var wsObj:IdTestimonyVo=getVo();
			Ws.Query.send(wsObj.CONNO,wsObj.IsCtr,wsObj.NAME,locObj.weights,locObj.weighte,wsObj.PACKAGE,wsObj.PROPERTY,
				locObj.ctimes,locObj.ctimee,wsObj.SLN,
				locObj.jdzy,locObj.jdlb,locObj.jdxm,locObj.yjr,PsbLocator.getInstance().idPsb.ID,
				locObj.listPager.pageSize,locObj.listPager.pageIndex);
			updatePending(false);
		}
		public function tesOper():void
		{
			var wsObj:IdFlowVo=IdFlowLocator.getInstance().wsObj;
			Ws.TesOper.send(wsObj.CONNO,wsObj.TesOper,wsObj.GetTesPerson,wsObj.GetTesDate,wsObj.TesOperRemark);
			updatePending(false);
		}
		private function getVo():IdTestimonyVo
		{
			var wsObj:IdTestimonyVo=null;
			if(responder.evtType.search("IDTESTIMONYWS_")==0)
			{
				wsObj=IdTestimonyLocator.getInstance().wsObj;
			}
			else if(responder.evtType.search("IDTESTIMONY_CTRWS_")==0)
			{
				wsObj=IdCtrLocator.getInstance().wsObj;
			}	
			return wsObj;
		}
		private function updatePending(rel:Boolean):void
		{
			PsbLocator.getInstance().npending=rel;
		}
	}
}