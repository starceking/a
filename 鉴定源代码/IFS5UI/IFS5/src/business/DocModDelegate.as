package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;

	import locator.DocModLocator;
	import locator.PsbLocator;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	import vo.DocModVo;
	
	public class DocModDelegate
	{
		public function DocModDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("文书修改WS"));
				
				ws.Insert.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Update.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Delete.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetAll.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				
				ws.Insert.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Update.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Delete.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAll.addEventListener(FaultEvent.FAULT,faultHandler);
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
		public function insert():void
		{
			var wsObj:DocModVo=DocModLocator.getInstance().wsObj;
			Ws.Insert.send(wsObj.ID,wsObj.CONNO,wsObj.MODER,wsObj.MODTIME,wsObj.AUDIT,wsObj.POSITION,
				wsObj.ORITEXT,wsObj.NOWTEXT,wsObj.NUMBER,wsObj.AUDITTIME);			
			PsbLocator.getInstance().npending=false;
		}
		public function update():void
		{
			var wsObj:DocModVo=DocModLocator.getInstance().wsObj;
			Ws.Update.send(wsObj.ID,wsObj.MODTIME,wsObj.AUDIT,wsObj.POSITION,
				wsObj.ORITEXT,wsObj.NOWTEXT,wsObj.NUMBER,wsObj.AUDITTIME);			
			PsbLocator.getInstance().npending=false;
		}
		public function deleteObj():void
		{
			var wsObj:DocModVo=DocModLocator.getInstance().wsObj;
			Ws.Delete.send(wsObj.ID);			
			PsbLocator.getInstance().npending=false;
		}
		public function getAll():void
		{			
			var wsObj:DocModVo=DocModLocator.getInstance().wsObj;
			Ws.GetAll.send(wsObj.CONNO);
		}
	}
}