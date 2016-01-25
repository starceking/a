package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.NotificationLocator;
	import locator.PsbLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	public class NotificationDelegate
	{
		public function NotificationDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("通知通告WS"));
				
				ws.Insert.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Update.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Delete.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetAll.addEventListener(ResultEvent.RESULT,getAllResultHandler);
				ws.GetImportant.addEventListener(ResultEvent.RESULT,getAllResultHandler);
				
				ws.Insert.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Update.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Delete.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAll.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetImportant.addEventListener(FaultEvent.FAULT,faultHandler);
			}
			return ws;
		}
		//Handler
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
			var locObj:NotificationLocator=NotificationLocator.getInstance();
			Ws.Insert.send(locObj.voObj.ID,locObj.voObj.TITLE,locObj.voObj.CONTENT_TEXT,locObj.voObj.IMPORTANT,locObj.voObj.CREATOR,
			PsbLocator.getInstance().ID_PSB_ID);
			PsbLocator.getInstance().npending=false;
		}		
		public function update():void
		{
			var locObj:NotificationLocator=NotificationLocator.getInstance();
			Ws.Update.send(locObj.voObj.ID,locObj.voObj.TITLE,locObj.voObj.CONTENT_TEXT,locObj.voObj.IMPORTANT);
			PsbLocator.getInstance().npending=false;
		}		
		public function deleteFunc():void
		{
			Ws.Delete.send(NotificationLocator.getInstance().voObj.ID);
			PsbLocator.getInstance().npending=false;
		}
		public function getAll():void
		{
			var locObj:NotificationLocator=NotificationLocator.getInstance();			
			var ps:int=10;
			var pi:int=1;
			if(locObj.listPager!=null)
			{
				ps=locObj.listPager.pageSize;
				pi=locObj.listPager.pageIndex;
			}				
			Ws.GetAll.send(PsbLocator.getInstance().ID_PSB_ID,locObj.voObj.TITLE,locObj.voObj.IMPORTANT,locObj.ctimes,locObj.ctimee,ps,pi);
			PsbLocator.getInstance().npending=false;
		}
		public function GetImportant():void
		{			
			Ws.GetImportant.send(PsbLocator.getInstance().ID_PSB_ID);
			PsbLocator.getInstance().npending=false;
		}
	}
}