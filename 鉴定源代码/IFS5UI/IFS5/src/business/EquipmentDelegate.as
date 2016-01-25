package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.EquipmentLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	import flash.net.navigateToURL;
	import flash.net.URLRequest;
	import vo.EquipmentVo;
	public class EquipmentDelegate
	{
		public function EquipmentDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("仪器设备WS"));
				
				ws.Insert.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Update.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Delete.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetAll.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetSBAll.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetOne.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.MackALLFB.addEventListener(ResultEvent.RESULT,strUrlHandler);
				
				ws.Insert.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Update.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Delete.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAll.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetSBAll.addEventListener(FaultEvent.FAULT,faultHandler);
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
			var voObj:EquipmentVo=EquipmentLocator.getInstance().wsObj;
			Ws.Insert.send(voObj.ID,voObj.SBYQMC,voObj.CZSMC,voObj.XHGG,voObj.CCBH,voObj.SBSBH,voObj.JSRQ,voObj.QYRQ,voObj.MQCFDD,
				voObj.JSZT,voObj.SZSJ,voObj.SZZQ,voObj.SYZY,voObj.GLR,voObj.WHFS,voObj.YZFS,voObj.ZP,voObj.FJ,voObj.SBLB);
			
		}
		public function update():void
		{
			var voObj:EquipmentVo=EquipmentLocator.getInstance().wsObj;
			Ws.Update.send(voObj.ID,voObj.SBYQMC,voObj.CZSMC,voObj.XHGG,voObj.CCBH,voObj.SBSBH,voObj.JSRQ,voObj.QYRQ,voObj.MQCFDD,
				voObj.JSZT,voObj.SZSJ,voObj.SZZQ,voObj.SYZY,voObj.GLR,voObj.WHFS,voObj.YZFS,voObj.ZP,voObj.FJ,voObj.SBLB);
			
		}
		public function deleteObj():void
		{
			var voObj:EquipmentVo=EquipmentLocator.getInstance().wsObj;
			Ws.Delete.send(voObj.ID);
			
		}
		public function getAll():void
		{
			var voObj:EquipmentVo=EquipmentLocator.getInstance().wsObj;
			Ws.GetAll.send(voObj.SYZY,voObj.SBLB);
			
		}
		public function getSBAll():void
		{
			var voObj:EquipmentVo=EquipmentLocator.getInstance().wsObj;
			Ws.GetSBAll.send(voObj.SYZY,voObj.SBLB);
			
		}
		public function getOne():void
		{
			var voObj:EquipmentVo=EquipmentLocator.getInstance().wsObj;
			Ws.GetOne.send(voObj.ID);
			
		}
		public function mackAllFB():void
		{
			var locObj:EquipmentLocator=EquipmentLocator.getInstance();
			Ws.MackALLFB.send(locObj.wordName,locObj.num,locObj.filename,locObj.num1);	
		}
	}
}