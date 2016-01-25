package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.TrainLocator;
	import locator.PsbLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	import locator.WordLocator;
	import locator.SysUserLocator;
	import flash.external.ExternalInterface;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	
	
	import util.Server;
	import util.Helper;
	public class TrainDelegate
	{
		public function TrainDelegate(responder:IFSCommand)
		{
			this.responder=responder;
		}
		//WebService
		var ws:WebService; 
		function get Ws():WebService
		{
			if(ws==null)
			{
				ws=new WebService(); 
				ws.loadWSDL(Server.getWsUrl("培训记录WS"));
				
				ws.Insert.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.InsertSS.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Update.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Delete.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetAll.addEventListener(ResultEvent.RESULT,getAllResultHandler);
				ws.GetOne.addEventListener(ResultEvent.RESULT,getAllResultHandler);
				ws.MackWord.addEventListener(ResultEvent.RESULT,generateWordResultHandler);
				
				
				ws.Insert.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.InsertSS.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Update.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Delete.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAll.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetOne.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.MackWord.addEventListener(FaultEvent.FAULT,faultHandler);
			}
			return ws;
		}
		//Handler
		private function generateWordResultHandler(event:ResultEvent):void
		{
			
			WordLocator.getInstance().npending=true;
			
			var result:String=event.result.toString();
			if(result!="1")
			{
				Helper.showAlert("打开文书异常："+result);
				return;
			}
			
			else
			{
				navigateToURL(new URLRequest(Server.getWordUrl(TrainLocator.getInstance().filename)));
			}
		}
		function resultHandler(evt:ResultEvent):void
		{
			responder.onResult();	
			PsbLocator.getInstance().npending=true;	
		}
		function getAllResultHandler(evt:ResultEvent):void
		{
			responder.onResult(new XML(evt.result));	
			PsbLocator.getInstance().npending=true;	
		}
		function faultHandler(evt:FaultEvent):void
		{
			responder.onFault(evt.fault.faultString);	
			PsbLocator.getInstance().npending=true;
		}
		//Object
		private var responder:IFSCommand;
		//External call
		public function insert():void
		{
			var locObj:TrainLocator=TrainLocator.getInstance();
			Ws.Insert.send(locObj.voObj.ID,locObj.voObj.PersonID,locObj.voObj.Name,locObj.voObj.Office,locObj.voObj.TrainXM,locObj.voObj.TrainTeacher,locObj.voObj.TrainTime,
				locObj.voObj.TrainMD,locObj.voObj.TrainGC,locObj.voObj.TrainXGPJ,locObj.voObj.JLR,locObj.voObj.JLTime);
			PsbLocator.getInstance().npending=false;
		}	
		public function insertSS():void
		{
			var locObj:TrainLocator=TrainLocator.getInstance();
			Ws.InsertSS.send(locObj.voObj.ID,locObj.voObj.PersonID,locObj.voObj.Name,locObj.voObj.Office,locObj.voObj.TrainXM,locObj.voObj.TrainTeacher,locObj.voObj.TrainTime,
				locObj.voObj.TrainMD,locObj.voObj.TrainGC,locObj.voObj.TrainXGPJ,locObj.voObj.JLR,locObj.voObj.JLTime,locObj.voObj.PXJLID);
			PsbLocator.getInstance().npending=false;
		}	
		public function update():void
		{
			var locObj:TrainLocator=TrainLocator.getInstance();
			Ws.Update.send(locObj.voObj.ID,locObj.voObj.Name,locObj.voObj.Office,locObj.voObj.TrainXM,locObj.voObj.TrainTeacher,locObj.voObj.TrainTime,
				locObj.voObj.TrainMD,locObj.voObj.TrainGC,locObj.voObj.TrainXGPJ,locObj.voObj.JLR,locObj.voObj.JLTime);
			PsbLocator.getInstance().npending=false;
		}		
		public function deleteFunc():void
		{
			Ws.Delete.send(TrainLocator.getInstance().voObj.ID);
			PsbLocator.getInstance().npending=false;
		}
		public function getAll():void
		{
			var locObj:TrainLocator=TrainLocator.getInstance();		
			Ws.GetAll.send(locObj.voObj.PersonID);
			PsbLocator.getInstance().npending=false;
		}
		public function getOne():void
		{
			var locObj:TrainLocator=TrainLocator.getInstance();		
			Ws.GetOne.send(locObj.voObj.PersonID);
			PsbLocator.getInstance().npending=false;
		}
		public function mackWord():void
		{
			var locObj:TrainLocator=TrainLocator.getInstance();		
			Ws.MackWord.send(locObj.voObj.ID,locObj.wordname,locObj.filename);
			PsbLocator.getInstance().npending=false;
		}
	}
}