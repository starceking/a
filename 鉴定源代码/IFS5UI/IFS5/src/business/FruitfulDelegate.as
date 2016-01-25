package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.FruitfulLocator;
	import locator.PsbLocator;
	import locator.SysUserLocator;
	
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	import vo.FruitfulVo;
	
	public class FruitfulDelegate
	{
		public function FruitfulDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("专业成果WS"));
				
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
		public function getAll():void
		{
			if(SysUserLocator.getInstance().curObj!=null)
				Ws.GetAll.send(SysUserLocator.getInstance().curObj.ID);
		}
		public function insert():void
		{
			var wsObj:FruitfulVo=FruitfulLocator.getInstance().wsObj;
			Ws.Insert.send(wsObj.ID,wsObj.SysUserID,wsObj.ProjectName,wsObj.AwardDate,wsObj.AwardUnit,wsObj.RewardName,wsObj.AwardRank,wsObj.Remark);			
			PsbLocator.getInstance().npending=false;
		}
		public function update():void
		{
			var wsObj:FruitfulVo=FruitfulLocator.getInstance().wsObj;
			Ws.Update.send(wsObj.ID,wsObj.ProjectName,wsObj.AwardDate,wsObj.AwardUnit,wsObj.RewardName,wsObj.AwardRank,wsObj.Remark);			
			PsbLocator.getInstance().npending=false;
		}
		public function deleteObj():void
		{
			var wsObj:FruitfulVo=FruitfulLocator.getInstance().wsObj;
			Ws.Delete.send(wsObj.ID);			
			PsbLocator.getInstance().npending=false;
		}
	}
}