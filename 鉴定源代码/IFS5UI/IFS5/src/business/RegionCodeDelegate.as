package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.RegionCodeLocator;
	import locator.PsbLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	import vo.RegionCodeVo;
	
	public class RegionCodeDelegate
	{
		public function RegionCodeDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("行政区划WS"));
				
				ws.GetAll.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
			
				ws.GetAll.addEventListener(FaultEvent.FAULT,faultHandler);
			}
			return ws;
		}
		//Handler
		private function getXmlResultHandler(evt:ResultEvent):void
		{
			responder.onResult(new XML(evt.result));		
		}
		private function faultHandler(evt:FaultEvent):void
		{
			responder.onFault(evt.fault.faultString);	
			PsbLocator.getInstance().npending=true;
		}
		//Object
		private var responder:IFSCommand;
		//External call
		public function getAll():void
		{
			var wsObj:RegionCodeVo=RegionCodeLocator.getInstance().wsObj;
			Ws.GetAll.send(wsObj.Code,wsObj.RegionName,wsObj.RegionType);
		}
	}
}