package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import flash.external.ExternalInterface;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	
	import locator.IdFlowLocator;
	import locator.IdTestimonyLocator;
	import locator.PsbLocator;
	import locator.SysUserLocator;
	import locator.WordLocator;
	import locator.JusTypeLocator;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Helper;
	import util.Server;
	
	import vo.IdFlowVo;
	
	public class WordDelegate
	{
		public function WordDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("WordWS"));
				
				ws.GenerateWord.addEventListener(ResultEvent.RESULT,generateWordResultHandler);
				ws.GenerateWzms.addEventListener(ResultEvent.RESULT,generateWzmsResultHandler);
				ws.DeleteWord.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetCaseWordList.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetAllCaseWord.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetCaseWordManageList.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				
				ws.GenerateWord.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GenerateWzms.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.DeleteWord.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetCaseWordList.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAllCaseWord.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetCaseWordManageList.addEventListener(FaultEvent.FAULT,faultHandler);
				
				ws.PrintSampleTestRecord.addEventListener(ResultEvent.RESULT,printTableHandler);
				ws.PrintCaseTestRecord.addEventListener(ResultEvent.RESULT,printTableHandler);
				ws.PrintDNATestHelpFile.addEventListener(ResultEvent.RESULT,printTableHandler);
				
				ws.PrintSampleTestRecord.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.PrintCaseTestRecord.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.PrintDNATestHelpFile.addEventListener(FaultEvent.FAULT,faultHandler);
			}
			return ws;
		}
		//Handler
		private function printTableHandler(evt:ResultEvent):void
		{
			var result:String=evt.result.toString();
			navigateToURL(new URLRequest(result));
			PsbLocator.getInstance().npending=true;
		}
		private function generateWordResultHandler(event:ResultEvent):void
		{
			var curObj:IdFlowVo=IdFlowLocator.getInstance().curObj;
			
			WordLocator.getInstance().npending=true;
			
			var result:String=event.result.toString();
			if(result!="1")
			{
				Helper.showAlert("打开文书异常："+result);
				return;
			}
			
			if(	WordLocator.getInstance().pageWord)
			{
				var para:String="path="+escape(WordLocator.getInstance().wsObj.WordDir)+"&name="+escape(WordLocator.getInstance().wsObj.FileName);
				para+="&mode=revision&opener="+escape(SysUserLocator.getInstance().loginUser.NAME);
				
				var func:String="var el = document.createElement('a');"+
					"document.body.appendChild(el);"+
					"el.href = 'PageOffice://|http://"+Server.serverName+"/"+Server.webName+"/OpenWord.aspx?"+ para +"|width=800px;height=800px;||';"+
					"el.click();"+
					"document.body.removeChild(el);";
				ExternalInterface.call("function(){"+func+"}");
			}
			else
			{
				var func:String="var WordApp;"+
					"try {"+
					"WordApp = new ActiveXObject('Word.Application');"+
					"} catch (e) {"+
					"alert('请设置服务器为可信任站点，并确认自己的电脑正确安装了office2003或以上');"+
					"return;"+
					"}"+
					"try {"+
					"WordApp.Application.UserName = '"+SysUserLocator.getInstance().loginUser.NAME+"';";
				func+="WordApp.Documents.Open('"+WordLocator.getInstance().wsObj.OpenWordUrl+"');";				
				func+="WordApp.ActiveWindow.View.RevisionsView = 0;"+
					"WordApp.ActiveWindow.View.ShowRevisionsAndComments = false;"+
					"WordApp.ActiveWindow.View.Type = 3;"+
					"WordApp.Visible = true;"+
					"}"+
					"catch (e) {"+
					"WordApp.Quit();"+
					"};";
				ExternalInterface.call("function(){"+func+"}");
			}
		}
		private function generateWzmsResultHandler(event:ResultEvent):void
		{
			var curObj:IdFlowVo=IdFlowLocator.getInstance().curObj;
			
			WordLocator.getInstance().npending=true;
			
			var result:String=event.result.toString();
			if(result!="1")
			{
				Helper.showAlert("打开文书异常："+result);
				return;
			}
			
			var url:String=Server.getTesMsUrl(PsbLocator.getInstance().idPsb.ID,curObj.CONNO,
				IdTestimonyLocator.getInstance().sln);
			if(curObj.ID_STATUS=="已存档"||curObj.LEADERF.length>0)//存档或领导审批后都不能修改
			{
				navigateToURL(new URLRequest(url));
			}
			else
			{
				var func:String="var WordApp;"+
					"try {"+
					"WordApp = new ActiveXObject('Word.Application');"+
					"} catch (e) {"+
					"alert('请设置服务器为可信任站点，并确认自己的电脑正确安装了office2003或以上');"+
					"return;"+
					"}"+
					"try {"+
					"WordApp.Application.UserName = '"+SysUserLocator.getInstance().loginUser.NAME+"';";
				func+="WordApp.Documents.Open('"+url+"');";				
				func+="WordApp.ActiveWindow.View.RevisionsView = 0;"+
					"WordApp.ActiveWindow.View.ShowRevisionsAndComments = false;"+
					"WordApp.ActiveWindow.View.Type = 3;"+
					"WordApp.Visible = true;"+
					"}"+
					"catch (e) {"+
					"WordApp.Quit();"+
					"};";
				ExternalInterface.call("function(){"+func+"}");
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
			WordLocator.getInstance().npending=true;
		}
		private function getXmlResultHandler(evt:ResultEvent):void
		{
			responder.onResult(new XML(evt.result));	
			WordLocator.getInstance().npending=true;
		}
		private function faultHandler(evt:FaultEvent):void
		{
			responder.onFault(evt.fault.faultString);
			WordLocator.getInstance().npending=true;
		}
		//Object
		private var responder:IFSCommand;
		//External call
		public function getCaseWordList():void
		{
			var locObj:WordLocator=WordLocator.getInstance();
			Ws.GetCaseWordList.send(locObj.idPsb,locObj.conno,locObj.jusType,locObj.wordType);
			
			WordLocator.getInstance().npending=false;
		}
		public function generateWord():void
		{
			var locObj:WordLocator=WordLocator.getInstance();
			Ws.GenerateWord.send(locObj.wsObj.TemplatePath,locObj.wsObj.WordDir,locObj.wsObj.FileName,locObj.conno,
				locObj.status,locObj.isTesNote);
			
			WordLocator.getInstance().npending=false;
		}
		public function generateWzms():void
		{
			var justype:String="DNA";
			if(IdFlowLocator.getInstance().curObj.ID_OFFICE!="DNA")
			{
				justype=IdFlowLocator.getInstance().curObj.ID_JUSTYPE;
			}
			var locObj:WordLocator=WordLocator.getInstance();
			Ws.GenerateWzms.send(PsbLocator.getInstance().idPsb.ID,justype,IdTestimonyLocator.getInstance().sln,
				IdFlowLocator.getInstance().curObj.CONNO);
			
			WordLocator.getInstance().npending=false;
		}
		public function getAllCaseWord():void
		{
			var locObj:WordLocator=WordLocator.getInstance();
			Ws.GetAllCaseWord.send(locObj.idPsb,locObj.conno);			
			WordLocator.getInstance().npending=false;
		}
		public function getCaseWordManageList():void
		{
			var locObj:WordLocator=WordLocator.getInstance();
			Ws.GetCaseWordManageList.send(locObj.idPsb,locObj.jusType,locObj.wordType);			
			WordLocator.getInstance().npending=false;
		}
		public function deleteWord():void
		{
			var locObj:WordLocator=WordLocator.getInstance();
			Ws.DeleteWord.send(locObj.wsObj.WordDir,locObj.wsObj.FileName);
			
			WordLocator.getInstance().npending=false;
		}
		public function printSampleTestRecord():void
		{
			var locObj:WordLocator=WordLocator.getInstance();
			Ws.PrintSampleTestRecord.send(PsbLocator.getInstance().idPsb.ID,
				locObj.FileName,locObj.wordType,locObj.RecordType,locObj.RecordID);
			WordLocator.getInstance().npending=false;
		}
		public function printCaseTestRecord():void
		{
			var locObj:WordLocator=WordLocator.getInstance();
			Ws.PrintCaseTestRecord.send(PsbLocator.getInstance().idPsb.ID,
				locObj.FileName,locObj.wordType,locObj.RecordType,locObj.RecordID,locObj.conno);
			WordLocator.getInstance().npending=false;
		}
		public function printDNATestHelpFile():void
		{
			var locObj:WordLocator=WordLocator.getInstance();
			Ws.PrintDNATestHelpFile.send(PsbLocator.getInstance().idPsb.ID,
				locObj.FileName,locObj.wordType,locObj.RecordType,locObj.RecordID);
			WordLocator.getInstance().npending=false;
		}
	}
}