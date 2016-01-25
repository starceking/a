package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.UnknownDeceasedLocator;
	import locator.PsbLocator;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	import locator.IdCaseLocator;
	import locator.JusTypeLocator;
	import locator.SysUserLocator;
	import locator.IdFlowLocator;
	import util.Helper;
	import util.Server;
	
	import vo.UnknownDeceasedVo;
	
	public class UnkownDeceasedDelegate
	{
		public function UnkownDeceasedDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("无名尸体WS"));
				
				ws.Insert.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.InsertWithNo.addEventListener(ResultEvent.RESULT,insertNoResultHandler);
				ws.Update.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Delete.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetAll.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				
				ws.Insert.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.InsertWithNo.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Update.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Delete.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAll.addEventListener(FaultEvent.FAULT,faultHandler);
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
		private function insertNoResultHandler(evt:ResultEvent):void
		{
			var result:String=evt.result.toString();
			if(result=="0")
			{
				responder.onFault(result);		
			}	
			else
			{
				responder.onResult(result);
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
		public function insert():void
		{
			var wsObj:UnknownDeceasedVo=UnknownDeceasedLocator.getInstance().wsObj;
			Ws.Insert.send(wsObj.ID,wsObj.CASE_ID,wsObj.CONNO,wsObj.NAME,wsObj.SAMPLE_TYPE,wsObj.GENDER,wsObj.PACKAGE,wsObj.SAMPLE_DESCRIPTION,
				wsObj.SPECIFICATION,wsObj.AGE,wsObj.REMARK);			
			PsbLocator.getInstance().npending=false;
		}
		public function insertWithNo():void
		{
			var wsObj:UnknownDeceasedVo=UnknownDeceasedLocator.getInstance().wsObj;
			Ws.InsertWithNo.send(wsObj.ID,wsObj.CASE_ID,wsObj.CONNO,wsObj.NAME,wsObj.SAMPLE_TYPE,wsObj.GENDER,wsObj.PACKAGE,wsObj.SAMPLE_DESCRIPTION,
				wsObj.SPECIFICATION,wsObj.AGE,wsObj.REMARK,
				Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.USLN,
					IdCaseLocator.getInstance().bsMainObj.ACC_YEAR,IdCaseLocator.getInstance().bsMainObj.ACC_CASE_NO,""),
				JusTypeLocator.getInstance().jusTypeVo.WholeNo=="1"?"样本视图":"无名尸体",
				Helper.getSlnNumLen(JusTypeLocator.getInstance().jusTypeVo.USLN),
				IdFlowLocator.getInstance().curObj.CONSIGNID,SysUserLocator.getInstance().loginUser.NAME,IdFlowLocator.getInstance().curObj.ORA_FLAG);			
			PsbLocator.getInstance().npending=false;
		}
		public function update():void
		{
			var wsObj:UnknownDeceasedVo=UnknownDeceasedLocator.getInstance().wsObj;
			Ws.Update.send(wsObj.ID,wsObj.NAME,wsObj.SAMPLE_TYPE,wsObj.GENDER,wsObj.PACKAGE,wsObj.SAMPLE_DESCRIPTION,
				wsObj.SPECIFICATION,wsObj.AGE,wsObj.REMARK,wsObj.SLN,wsObj.ORA_FLAG);			
			PsbLocator.getInstance().npending=false;
		}
		public function deleteObj():void
		{
			var wsObj:UnknownDeceasedVo=UnknownDeceasedLocator.getInstance().wsObj;
			Ws.Delete.send(wsObj.ID,wsObj.ORA_FLAG);			
			PsbLocator.getInstance().npending=false;
		}
		public function getAll():void
		{
			var wsObj:UnknownDeceasedVo=UnknownDeceasedLocator.getInstance().wsObj;
			Ws.GetAll.send(wsObj.CASE_ID,wsObj.CONNO);
		}
	}
}