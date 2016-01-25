package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	
	import locator.ConfirmLocator;
	import locator.PsbLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	import vo.StrVo;
	import vo.PreExamVo;
	
	public class ConfirmDelegate
	{
		public function ConfirmDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("样本检验WS"));
				
				ws.InsertConfirm.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateCaseConfirm.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.DeleteCaseConfirm.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.QueryCaseConfirm.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.QueryConfirm.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				
				ws.InsertConfirm.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateCaseConfirm.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.DeleteCaseConfirm.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.QueryCaseConfirm.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.QueryConfirm.addEventListener(FaultEvent.FAULT,faultHandler);
			}
			return ws;
		}
		//Handler
		private function strTableHandler(evt:ResultEvent):void
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
		private var locObj:ConfirmLocator=ConfirmLocator.getInstance();
		//External call
		public function insertConfirm():void
		{
			Ws.InsertConfirm.send(ConfirmLocator.getInstance().ConfirmArray);
			PsbLocator.getInstance().npending=false;
		}
		public function updateCaseConfirm():void
		{
			var wsObj:PreExamVo=ConfirmLocator.getInstance().wsObj;
			Ws.UpdateCaseConfirm.send(wsObj.ID,wsObj.TEST_METHOD,wsObj.TEST_DATE,wsObj.RESULT);			
			PsbLocator.getInstance().npending=false;
		}
		public function deleteCaseConfirm():void
		{
			var wsObj:PreExamVo=ConfirmLocator.getInstance().wsObj;
			Ws.DeleteCaseConfirm.send(wsObj.ID);			
			PsbLocator.getInstance().npending=false;
		}
		public function queryCaseConfirm():void
		{
			var wsObj:PreExamVo=ConfirmLocator.getInstance().wsObj;
			Ws.QueryCaseConfirm.send(wsObj.CASE_ID,wsObj.CONNO,ConfirmLocator.getInstance().ConfirmListPager.pageSize,
				ConfirmLocator.getInstance().ConfirmListPager.pageIndex);
		}
		public function queryConfirm():void
		{
			var StrwsObj:StrVo=locObj.StrwsObj;
			Ws.QueryConfirm.send(PsbLocator.getInstance().ID_PSB_ID,StrwsObj.SLN,StrwsObj.NAME,locObj.yblx,StrwsObj.SC,
				locObj.slsjs,locObj.slsje,locObj.yjr,locObj.jystatus,locObj.confirm,
				locObj.ConfirmListPager.pageSize,locObj.ConfirmListPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
		
	}
}