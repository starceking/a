package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.IFALocator;
	import locator.PsbLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	public class IFADelegate
	{
		public function IFADelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("IFAWS"));
				
				ws.ReadStr.addEventListener(ResultEvent.RESULT,resultHandler);
				
				ws.ReadStr.addEventListener(FaultEvent.FAULT,faultHandler);
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
		public function readStr():void
		{
			var loc:IFALocator=IFALocator.getInstance();
			Ws.ReadStr.send(loc.caseId,loc.conno,loc.ip);			
			PsbLocator.getInstance().npending=false;
		}
	}
}