package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.PsbLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	import vo.PsbVo;
	
	public class PsbDelegate
	{
		public function PsbDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("单位信息WS"));
				
				ws.GetAll.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.Insert.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Update.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Delete.addEventListener(ResultEvent.RESULT,resultHandler);
				
				ws.GetAll.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Insert.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Update.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Delete.addEventListener(FaultEvent.FAULT,faultHandler);
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
			var wsObj:PsbVo=PsbLocator.getInstance().wsObj;
			Ws.GetAll.send(wsObj.PID,wsObj.PTYPE,wsObj.PSBTYPE);
		}
		public function insert():void
		{
			var wsObj:PsbVo=PsbLocator.getInstance().wsObj;
			Ws.Insert.send(wsObj.ID,wsObj.PID,wsObj.NUMBER,wsObj.NAME,wsObj.ADDRESS,wsObj.POSTCODE,wsObj.NICKNAME,wsObj.PHONE);			
			PsbLocator.getInstance().npending=false;
		}
		public function update():void
		{
			var wsObj:PsbVo=PsbLocator.getInstance().wsObj;
			Ws.Update.send(wsObj.ID,wsObj.NUMBER,wsObj.NAME,wsObj.ADDRESS,wsObj.POSTCODE,wsObj.NICKNAME,wsObj.PHONE);			
			PsbLocator.getInstance().npending=false;
		}
		public function deleteObj():void
		{
			var wsObj:PsbVo=PsbLocator.getInstance().wsObj;
			Ws.Delete.send(wsObj.ID);			
			PsbLocator.getInstance().npending=false;
		}
	}
}