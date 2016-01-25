package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.SuppliesVerificationLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.*;
	import flash.net.navigateToURL;
	import flash.net.URLRequest;
	import vo.SuppliesVerificationVo;
	import locator.WordLocator;
	public class SuppliesVerificationDelegate
	{
		public function SuppliesVerificationDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("耗材核查WS"));
				
				ws.Insert.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Update.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Delete.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetAll.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetOne.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.MackALLHCHC.addEventListener(ResultEvent.RESULT,generateWordResultHandler);
				
				ws.Insert.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Update.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Delete.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAll.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetOne.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.MackALLHCHC.addEventListener(FaultEvent.FAULT,faultHandler);
				
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
				navigateToURL(new URLRequest(Server.getWordUrl(SuppliesVerificationLocator.getInstance().filename)));
			}
		}
		private var responder:IFSCommand;
		//External call
		public function insert():void
		{
			var voObj:SuppliesVerificationVo=SuppliesVerificationLocator.getInstance().wsObj;
			Ws.Insert.send(voObj.ID,voObj.HCID,voObj.HCR,voObj.JDR,voObj.HCRQ,voObj.HCJG,voObj.CHGC);
			
		}
		public function update():void
		{
			var voObj:SuppliesVerificationVo=SuppliesVerificationLocator.getInstance().wsObj;
			Ws.Update.send(voObj.ID,voObj.HCID,voObj.HCR,voObj.JDR,voObj.HCRQ,voObj.HCJG,voObj.CHGC);
			
		}
		public function deleteObj():void
		{
			var voObj:SuppliesVerificationVo=SuppliesVerificationLocator.getInstance().wsObj;
			Ws.Delete.send(voObj.ID);
			
		}
		public function getAll():void
		{
			Ws.GetAll.send();
			
		}
		public function getOne():void
		{
			var voObj:SuppliesVerificationVo=SuppliesVerificationLocator.getInstance().wsObj;
			Ws.GetOne.send(voObj.HCID);
			
		}
		public function mackAllHCHC():void
		{
			var voObj:SuppliesVerificationVo=SuppliesVerificationLocator.getInstance().wsObj;
			Ws.MackALLHCHC.send(voObj.HCID,voObj.ID,SuppliesVerificationLocator.getInstance().wordname,SuppliesVerificationLocator.getInstance().filename);
			
		}
	}
}