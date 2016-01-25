package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.CaseFileLocator;
	import locator.PsbLocator;
	import locator.IdFlowLocator;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	import vo.CaseFileVo;
	
	public class CaseFileDelegate
	{
		public function CaseFileDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("CASEFILEWS"));
				
				ws.Delete.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetAll.addEventListener(ResultEvent.RESULT,getXmlResultHandler);			
				ws.GetAllFileData.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				
				ws.Delete.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAll.addEventListener(FaultEvent.FAULT,faultHandler);			
				ws.GetAllFileData.addEventListener(FaultEvent.FAULT,faultHandler);
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
		public function deleteObj():void
		{
			var wsObj:CaseFileVo=CaseFileLocator.getInstance().wsObj;
			Ws.Delete.send(wsObj.DiskPath);			
			PsbLocator.getInstance().npending=false;
		}
		public function getAll():void
		{
			Ws.GetAll.send(PsbLocator.getInstance().idPsb.ID,
				IdFlowLocator.getInstance().curObj.CONNO,CaseFileLocator.getInstance().fileType);
		}
		public function getAllFileData():void
		{
			Ws.GetAllFileData.send(PsbLocator.getInstance().idPsb.ID,
				CaseFileLocator.getInstance().fileOffs,CaseFileLocator.getInstance().fileType);
		}
	}
}