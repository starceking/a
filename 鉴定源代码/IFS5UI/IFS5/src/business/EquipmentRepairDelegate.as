package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.EquipmentRepairLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	import flash.net.navigateToURL;
	import flash.net.URLRequest;
	import vo.EquipmentRepairVo;
	public class EquipmentRepairDelegate
	{
		public function EquipmentRepairDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("设备维修记录WS"));
				
				ws.Insert.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Update.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Delete.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetAll.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetOne.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.MackALLFB.addEventListener(ResultEvent.RESULT,strUrlHandler);
				
				ws.Insert.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Update.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Delete.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAll.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetOne.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.MackALLFB.addEventListener(FaultEvent.FAULT,faultHandler);
				
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
		}
		private function strUrlHandler(evt:ResultEvent):void
		{
			var result:String=evt.result.toString();
			navigateToURL(new URLRequest(result));
		}
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
		public function insert():void
		{
			var voObj:EquipmentRepairVo=EquipmentRepairLocator.getInstance().wsObj;
			Ws.Insert.send(voObj.ID,voObj.SBID,voObj.FZR,voObj.XLSJ,voObj.SLYY,voObj.XXMS);
			
		}
		public function update():void
		{
			var voObj:EquipmentRepairVo=EquipmentRepairLocator.getInstance().wsObj;
			Ws.Update.send(voObj.ID,voObj.SBID,voObj.FZR,voObj.XLSJ,voObj.SLYY,voObj.XXMS);
			
		}
		public function deleteObj():void
		{
			var voObj:EquipmentRepairVo=EquipmentRepairLocator.getInstance().wsObj;
			Ws.Delete.send(voObj.ID);
			
		}
		public function getAll():void
		{
			Ws.GetAll.send();
			
		}
		public function getOne():void
		{
			var voObj:EquipmentRepairVo=EquipmentRepairLocator.getInstance().wsObj;
			Ws.GetOne.send(voObj.SBID);
			
		}
		public function mackAllFB():void
		{
			var locObj:EquipmentRepairLocator=EquipmentRepairLocator.getInstance();
			Ws.MackALLFB.send(locObj.wordName,locObj.num,locObj.filename);	
			
		}
	}
}