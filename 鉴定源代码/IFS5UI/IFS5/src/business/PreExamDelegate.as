package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	
	import locator.PreExamLocator;
	import locator.PsbLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	import vo.StrVo;
	import vo.PreExamVo;
	
	public class PreExamDelegate
	{
		public function PreExamDelegate(responder:IFSCommand)
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
				
				ws.InsertPreExam.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateCasePre.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.DeleteCasePre.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.QueryCasePre.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.QueryPreExam.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				
				ws.InsertPreExam.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateCasePre.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.DeleteCasePre.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.QueryCasePre.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.QueryPreExam.addEventListener(FaultEvent.FAULT,faultHandler);
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
		private var locObj:PreExamLocator=PreExamLocator.getInstance();
		//External call
		public function insertPreExam():void
		{
			Ws.InsertPreExam.send(PreExamLocator.getInstance().PreExamArray);
			PsbLocator.getInstance().npending=false;
		}
		public function updateCasePre():void
		{
			var wsObj:PreExamVo=PreExamLocator.getInstance().wsObj;
			Ws.UpdateCasePre.send(wsObj.ID,wsObj.TEST_METHOD,wsObj.TEST_DATE,wsObj.RESULT);			
			PsbLocator.getInstance().npending=false;
		}
		public function deleteCasePre():void
		{
			var wsObj:PreExamVo=PreExamLocator.getInstance().wsObj;
			Ws.DeleteCasePre.send(wsObj.ID);			
			PsbLocator.getInstance().npending=false;
		}
		public function queryCasePre():void
		{
			var wsObj:PreExamVo=PreExamLocator.getInstance().wsObj;
			Ws.QueryCasePre.send(wsObj.CASE_ID,wsObj.CONNO,PreExamLocator.getInstance().PreExamListPager.pageSize,
				PreExamLocator.getInstance().PreExamListPager.pageIndex);
		}
		public function queryPreExam():void
		{
			var StrwsObj:StrVo=locObj.StrwsObj;
			Ws.QueryPreExam.send(PsbLocator.getInstance().ID_PSB_ID,StrwsObj.SLN,StrwsObj.NAME,locObj.yblx,StrwsObj.SC,
				locObj.slsjs,locObj.slsje,locObj.yjr,locObj.jystatus,locObj.preexam,
				locObj.PreExamListPager.pageSize,locObj.PreExamListPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
	}
}