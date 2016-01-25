package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	import util.Helper;
	import locator.ExCaseLocator;
	import locator.PsbLocator;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	import locator.SysUserLocator;
	import util.Server;
	
	import vo.ExCaseVo;
	
	public class ExCaseDelegate
	{
		public function ExCaseDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("EXCASEWS"));
				
				ws.GetExCaseList.addEventListener(ResultEvent.RESULT,getXmlResultHandler);		
				ws.GetExCaseList.addEventListener(FaultEvent.FAULT,faultHandler);
				
				ws.ExeSql.addEventListener(ResultEvent.RESULT,resultHandler);				
				ws.ExeSql.addEventListener(FaultEvent.FAULT,faultHandler);
				
				ws.SendShortNote.addEventListener(ResultEvent.RESULT,sendNoteHandler);				
				ws.SendShortNote.addEventListener(FaultEvent.FAULT,faultHandler);
				
				ws.QueryGetReportCase.addEventListener(ResultEvent.RESULT,getXmlResultHandler);	
				ws.QueryGetReportCase.addEventListener(FaultEvent.FAULT,faultHandler);
				
				ws.GetSpTaskAmount.addEventListener(ResultEvent.RESULT,getXmlResultHandler);		
				ws.GetSpTaskAmount.addEventListener(FaultEvent.FAULT,faultHandler);
				
				ws.GetYjrSp.addEventListener(ResultEvent.RESULT,getXmlResultHandler);		
				ws.GetYjrSp.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetTaskRemind.addEventListener(ResultEvent.RESULT,getXmlResultHandler);

			}
			return ws;
		}
		//Handler
		private function getXmlResultHandler(evt:ResultEvent):void
		{
			if(evt.result.toString().length==0)
			{
				Helper.showAlert("目前未与任何系统建立连接！");
			}
			else
			{
				responder.onResult(new XML(evt.result));
			}		
			PsbLocator.getInstance().npending=true;
		}
		private function resultHandler(evt:ResultEvent):void
		{
			var result:String=evt.result.toString();
			if(result=="1")
			{
				PsbLocator.getInstance().npending=true;
				Helper.showAlert("已全部提交成功");
			}	
			else
			{
				responder.onFault(result);	
			}
			PsbLocator.getInstance().npending=true;
		}
		private function sendNoteHandler(evt:ResultEvent):void
		{
			var result:String=evt.result.toString();
			if(result=="1")
			{
				Helper.showAlert("发送短信成功");
			}	
			else
			{
				Helper.showAlert(result);
			}
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
		public function getTaskRemind():void
		{
			Ws.GetTaskRemind.send(SysUserLocator.getInstance().loginUser.ID);
		}

		public function getExCaseList():void
		{
			var locObj:ExCaseLocator=ExCaseLocator.getInstance();
			var wsObj:ExCaseVo=locObj.wsObj;
			//借用案件名称，存放关联系统名称
			//借用案件编号，存放DNA系统编号
			Ws.GetExCaseList.send(wsObj.DFGKNO,wsObj.XCKYNO,locObj.wsObj.CASE_NAME,wsObj.CASE_NO,"",locObj.listPager.pageSize,locObj.listPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
		public function exeSql():void
		{
			var locObj:ExCaseLocator=ExCaseLocator.getInstance();
			Ws.ExeSql.send(locObj.sql);
			PsbLocator.getInstance().npending=false;
		}
		public function sendShortNote():void
		{
			var locObj:ExCaseLocator=ExCaseLocator.getInstance();
			Ws.SendShortNote.send(locObj.phone,locObj.sql);
			PsbLocator.getInstance().npending=false;
		}
		public function queryGetReportCase():void
		{
			var locObj:ExCaseLocator=ExCaseLocator.getInstance();
			Ws.QueryGetReportCase.send(PsbLocator.getInstance().ID_PSB_ID,locObj.wsObj.CASE_NAME);
			PsbLocator.getInstance().npending=false;
		}
		public function getSpTaskAmount():void
		{
			Ws.GetSpTaskAmount.send(PsbLocator.getInstance().ID_PSB_ID);
		}
		public function getYjrSp():void
		{
			Ws.GetYjrSp.send(SysUserLocator.getInstance().loginUser.ID,ExCaseLocator.getInstance().slxh);
		}
	}
}