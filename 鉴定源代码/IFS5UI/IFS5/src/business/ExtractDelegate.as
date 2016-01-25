package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	
	import locator.ExtractLocator;
	import locator.PsbLocator;
	import locator.SysUserLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	import vo.StrVo;
	import vo.PreExamVo;
	import vo.ExtractVo;
	
	public class ExtractDelegate
	{
		public function ExtractDelegate(responder:IFSCommand)
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
				
				ws.InsertExtract.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateExtract.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.InsertPure.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdatePure.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.DeleteExtractRecord.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.NoTest.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.QueryExtract.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.QueryPure.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.QueryCaseExtract.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				
				ws.InsertExtract.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateExtract.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.InsertPure.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdatePure.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.DeleteExtractRecord.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.NoTest.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.QueryExtract.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.QueryPure.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.QueryCaseExtract.addEventListener(FaultEvent.FAULT,faultHandler);
			}
			return ws;
		}
		//Handler
		private function printHandler(evt:ResultEvent):void
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
		private var locObj:ExtractLocator=ExtractLocator.getInstance();
		//External call
		public function insertExtract():void
		{
			Ws.InsertExtract.send(locObj.ExtractArray,locObj.ExtractRecordArray);
			PsbLocator.getInstance().npending=false;
		}
		public function updateExtract():void
		{
			Ws.UpdateExtract.send(locObj.ExtractArray,locObj.ExtractRecordArray);			
			PsbLocator.getInstance().npending=false;
		}
		public function insertPure():void
		{
			Ws.InsertPure.send(locObj.PureArray,locObj.PureRecordArray);
			PsbLocator.getInstance().npending=false;
		}
		public function updatePure():void
		{
			Ws.UpdatePure.send(locObj.PureRecordArray);			
			PsbLocator.getInstance().npending=false;
		}
		public function deleteExtractRecord():void
		{
			Ws.DeleteExtractRecord.send(locObj.TQID);			
			PsbLocator.getInstance().npending=false;
		}
		public function noTest():void
		{
			Ws.NoTest.send(locObj.noTestArray);			
			PsbLocator.getInstance().npending=false;
		}
		public function queryExtract():void
		{
			var StrwsObj:StrVo=locObj.StrwsObj;
			Ws.QueryExtract.send(PsbLocator.getInstance().ID_PSB_ID,StrwsObj.SLN,StrwsObj.NAME,locObj.yblx,StrwsObj.SC,
				locObj.slsjs,locObj.slsje,locObj.yjr,locObj.jystatus,
				locObj.ExtractListPager.pageSize,locObj.ExtractListPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
		public function queryPure():void
		{
			Ws.QueryPure.send(locObj.TQID);
			PsbLocator.getInstance().npending=false;
		}
		public function queryCaseExtract():void
		{
			Ws.QueryCaseExtract.send(locObj.CaseID,locObj.ConNo,
				locObj.ExtractCaseListPager.pageSize,locObj.ExtractCaseListPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
	}
}