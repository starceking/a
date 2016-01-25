package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.DictLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	public class DictDelegate
	{
		public function DictDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("DICTWS"));
				
				ws.GetXml.addEventListener(ResultEvent.RESULT,getXmlResultHandler);	
				ws.InsertItem.addEventListener(ResultEvent.RESULT,getXmlResultHandler);	
				ws.DeleteItem.addEventListener(ResultEvent.RESULT,getXmlResultHandler);	
				ws.SaveDNATestItem.addEventListener(ResultEvent.RESULT,getXmlResultHandler);	
				ws.GetXml.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.InsertItem.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.DeleteItem.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.SaveDNATestItem.addEventListener(FaultEvent.FAULT,faultHandler);
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
		}
		//Object
		private var responder:IFSCommand;
		//External call
		public function getXml():void
		{
			Ws.GetXml.send();
		}
		public function insertItem():void
		{
			var locObj:DictLocator=DictLocator.getInstance();
			Ws.InsertItem.send(locObj.dictdictName,locObj.itemitemName);
		}
		public function deleteItem():void
		{
			var locObj:DictLocator=DictLocator.getInstance();
			Ws.DeleteItem.send(locObj.dictdictName,locObj.itemitemName);
		}
		public function SaveDNATestItem():void
		{
			var locObj:DictLocator=DictLocator.getInstance();
			Ws.SaveDNATestItem.send(locObj.dictdictName,locObj.itemitemName);
		}
	}
}