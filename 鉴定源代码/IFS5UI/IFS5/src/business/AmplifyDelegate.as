package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	
	import locator.AmplifyLocator;
	import locator.SysUserLocator;
	import locator.PsbLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	public class AmplifyDelegate
	{
		public function AmplifyDelegate(responder:IFSCommand)
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
				
				ws.QueryExtractRecord.addEventListener(ResultEvent.RESULT,getXmlResultHandler);	
				ws.GetSampleAmplify.addEventListener(ResultEvent.RESULT,getXmlResultHandler);	
				ws.JoinAmplify.addEventListener(ResultEvent.RESULT,getXmlResultHandler);	
				ws.InsertAmplify.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateAmplify.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.DeleteAmplifyRecord.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.QueryCaseAmplify.addEventListener(ResultEvent.RESULT,getXmlResultHandler);	

				ws.QueryExtractRecord.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetSampleAmplify.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.JoinAmplify.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.InsertAmplify.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateAmplify.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.DeleteAmplifyRecord.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.QueryCaseAmplify.addEventListener(FaultEvent.FAULT,faultHandler);
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
		private var locObj:AmplifyLocator=AmplifyLocator.getInstance();
		//External call
		public function queryExtractRecord():void
		{
			Ws.QueryExtractRecord.send(PsbLocator.getInstance().idPsb.ID,SysUserLocator.getInstance().loginUser.ID,
				locObj.rqs,locObj.rqe,locObj.gzzms,locObj.AmplifyListPager.pageSize,locObj.AmplifyListPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
		public function getSampleAmplify():void
		{
			Ws.GetSampleAmplify.send(locObj.TQID);
			PsbLocator.getInstance().npending=false;
		}
		public function joinAmplify():void
		{
			Ws.JoinAmplify.send(PsbLocator.getInstance().idPsb.ID,locObj.jcbh);
			PsbLocator.getInstance().npending=false;
		}
		public function insertAmplify():void
		{
			Ws.InsertAmplify.send(locObj.AmplifyArray,locObj.AmplifyRecordArray);
			PsbLocator.getInstance().npending=false;
		}
		public function updateAmplify():void
		{
			Ws.UpdateAmplify.send(locObj.AmplifyArray,locObj.AmplifyRecordArray);			
			PsbLocator.getInstance().npending=false;
		}
		public function deleteAmplifyRecord():void
		{
			Ws.DeleteAmplifyRecord.send(locObj.KZID);			
			PsbLocator.getInstance().npending=false;
		}
		public function queryCaseAmplify():void
		{
			Ws.QueryCaseAmplify.send(locObj.CaseID,locObj.ConNo,
				locObj.AmplifyCaseListPager.pageSize,locObj.AmplifyCaseListPager.pageIndex);			
			PsbLocator.getInstance().npending=false;
		}
	}
}