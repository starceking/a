package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.SuppliesUseLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.*;
	import locator.WordLocator;
	import flash.net.navigateToURL;
	import flash.net.URLRequest;
	import vo.SuppliesUseVo;
	public class SuppliesUseDelegate
	{
		public function SuppliesUseDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("耗材使用WS"));
				
				ws.Insert.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Update.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Delete.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetAll.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetOne.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.MackALLHCSY.addEventListener(ResultEvent.RESULT,generateWordResultHandler);
				
				ws.Insert.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Update.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Delete.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAll.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetOne.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.MackALLHCSY.addEventListener(FaultEvent.FAULT,faultHandler);
				
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
				navigateToURL(new URLRequest(Server.getWordUrl(SuppliesUseLocator.getInstance().filename)));
			}
		}
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
			var voObj:SuppliesUseVo=SuppliesUseLocator.getInstance().wsObj;
			Ws.Insert.send(voObj.ID,voObj.HCID,voObj.SYR,voObj.SYRQ,voObj.XHSL,voObj.PH);
			
		}
		public function update():void
		{
			var voObj:SuppliesUseVo=SuppliesUseLocator.getInstance().wsObj;
			Ws.Update.send(voObj.ID,voObj.HCID,voObj.SYR,voObj.SYRQ,voObj.XHSL,voObj.PH);
			
		}
		public function deleteObj():void
		{
			var voObj:SuppliesUseVo=SuppliesUseLocator.getInstance().wsObj;
			Ws.Delete.send(voObj.ID);
			
		}
		public function getAll():void
		{
			Ws.GetAll.send();
			
		}
		public function getOne():void
		{
			var voObj:SuppliesUseVo=SuppliesUseLocator.getInstance().wsObj;
			Ws.GetOne.send(voObj.HCID);
			
		}
		public function mackAllHCSY():void
		{
			var voObj:SuppliesUseVo=SuppliesUseLocator.getInstance().wsObj;
			Ws.MackALLHCSY.send(voObj.HCID,voObj.ID,SuppliesUseLocator.getInstance().wordname,SuppliesUseLocator.getInstance().filename);
			
		}

	}
}