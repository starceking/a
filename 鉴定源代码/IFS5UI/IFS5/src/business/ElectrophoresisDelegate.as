package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	
	import locator.ElectrophoresisLocator;
	import locator.SysUserLocator;
	import locator.PsbLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	public class ElectrophoresisDelegate
	{
		public function ElectrophoresisDelegate(responder:IFSCommand)
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
				
				ws.QueryAmplifyRecord.addEventListener(ResultEvent.RESULT,getXmlResultHandler);	
				ws.GetSampleEP.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.JoinEP.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.InsertEP.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateEP.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.DeleteEPRecord.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.QueryEPRecord.addEventListener(ResultEvent.RESULT,getXmlResultHandler);	
				ws.GetSampleEPRecord.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.QueryCaseEP.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				
				ws.QueryAmplifyRecord.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetSampleEP.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.JoinEP.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.InsertEP.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateEP.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.DeleteEPRecord.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.QueryEPRecord.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetSampleEPRecord.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.QueryCaseEP.addEventListener(FaultEvent.FAULT,faultHandler);
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
		private var locObj:ElectrophoresisLocator=ElectrophoresisLocator.getInstance();
		//External call
		public function queryAmplifyRecord():void
		{
			Ws.QueryAmplifyRecord.send(PsbLocator.getInstance().idPsb.ID,SysUserLocator.getInstance().loginUser.ID,
				locObj.rqs,locObj.rqe,locObj.kzqr,locObj.EPListPager.pageSize,locObj.EPListPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
		public function getSampleEP():void
		{
			Ws.GetSampleEP.send(locObj.KZID);
			PsbLocator.getInstance().npending=false;
		}
		public function joinEP():void
		{
			Ws.JoinEP.send(PsbLocator.getInstance().idPsb.ID,locObj.jcbh);
			PsbLocator.getInstance().npending=false;
		}
		public function insertEP():void
		{
			Ws.InsertEP.send(locObj.EPArray,locObj.EPRecordArray);
			PsbLocator.getInstance().npending=false;
		}
		public function updateEP():void
		{
			Ws.UpdateEP.send(locObj.EPArray,locObj.EPRecordArray);			
			PsbLocator.getInstance().npending=false;
		}
		public function deleteEPRecord():void
		{
			Ws.DeleteEPRecord.send(locObj.DYID);			
			PsbLocator.getInstance().npending=false;
		}
		public function queryEPRecord():void
		{
			Ws.QueryEPRecord.send(PsbLocator.getInstance().idPsb.ID,SysUserLocator.getInstance().loginUser.ID,
				locObj.rqs,locObj.rqe,locObj.dyqr,locObj.EPRecordListPager.pageSize,locObj.EPRecordListPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
		public function getSampleEPRecord():void
		{
			Ws.GetSampleEPRecord.send(locObj.DYID);
			PsbLocator.getInstance().npending=false;
		}
		public function queryCaseEP():void
		{
			Ws.QueryCaseEP.send(locObj.CaseID,locObj.ConNo,
				locObj.EPCaseListPager.pageSize,locObj.EPCaseListPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
	}
}