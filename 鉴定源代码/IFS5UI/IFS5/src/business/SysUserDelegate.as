package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.SysUserLocator;
	import locator.PsbLocator;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	public class SysUserDelegate
	{
		public function SysUserDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("系统用户WS"));
				
				ws.Insert.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Update.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateJBXX.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Login.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetAll.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetDeletedPerson.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetConsignPerson.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				
				ws.Insert.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Update.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateJBXX.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Login.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAll.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetDeletedPerson.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetConsignPerson.addEventListener(FaultEvent.FAULT,faultHandler);
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
		public function insert():void
		{
			var locObj:SysUserLocator=SysUserLocator.getInstance();		
			Ws.Insert.send(locObj.wsObj.ID,locObj.wsObj.PSBID,locObj.wsObj.OFFICE,locObj.wsObj.POLICENO,locObj.wsObj.PASSWORD,
				locObj.wsObj.NAME,locObj.wsObj.GENDER,locObj.wsObj.IDCARDNO,locObj.wsObj.LONGPHONE,locObj.wsObj.SHORTPHONE,
				locObj.wsObj.TECHTITLE,locObj.wsObj.ROLES,locObj.wsObj.AUTH,locObj.wsObj.INDEX,locObj.wsObj.ZW,
				locObj.wsObj.BrithDay,locObj.wsObj.Address,locObj.wsObj.POSTCODE,locObj.wsObj.PHONE,locObj.wsObj.NativeAddr,
				locObj.wsObj.PoliticalState,locObj.wsObj.JobStartDate,locObj.wsObj.JobTitle1,locObj.wsObj.AcquireDate1,locObj.wsObj.JobTitle2,
				locObj.wsObj.AcquireDate2,locObj.wsObj.Discipline,locObj.wsObj.JusType,locObj.wsObj.PractisingCertificateNo,locObj.wsObj.JG);			
			PsbLocator.getInstance().npending=false;
		}
		public function update():void
		{
			var locObj:SysUserLocator=SysUserLocator.getInstance();		
			Ws.Update.send(locObj.wsObj.ID,locObj.wsObj.PSBID,locObj.wsObj.OFFICE,locObj.wsObj.POLICENO,locObj.wsObj.PASSWORD,
				locObj.wsObj.NAME,locObj.wsObj.GENDER,locObj.wsObj.IDCARDNO,locObj.wsObj.LONGPHONE,locObj.wsObj.SHORTPHONE,
				locObj.wsObj.TECHTITLE,locObj.wsObj.ROLES,locObj.wsObj.AUTH,locObj.wsObj.DELETED,locObj.wsObj.INDEX,locObj.wsObj.ZW,
				locObj.wsObj.BrithDay,locObj.wsObj.Address,locObj.wsObj.POSTCODE,locObj.wsObj.PHONE,locObj.wsObj.NativeAddr,
				locObj.wsObj.PoliticalState,locObj.wsObj.JobStartDate,locObj.wsObj.JobTitle1,locObj.wsObj.AcquireDate1,locObj.wsObj.JobTitle2,
				locObj.wsObj.AcquireDate2,locObj.wsObj.Discipline,locObj.wsObj.JusType,locObj.wsObj.PractisingCertificateNo,locObj.wsObj.JG);			
			PsbLocator.getInstance().npending=false;
		}
		public function updatejbxx():void
		{
			var locObj:SysUserLocator=SysUserLocator.getInstance();	
			Ws.UpdateJBXX.send(locObj.wsObj.ID,locObj.wsObj.IDCARDNO,locObj.wsObj.ZW,
				locObj.wsObj.BrithDay,locObj.wsObj.Address,locObj.wsObj.POSTCODE,locObj.wsObj.PHONE,locObj.wsObj.NativeAddr,
				locObj.wsObj.PoliticalState,locObj.wsObj.JobStartDate,locObj.wsObj.JobTitle1,
				locObj.wsObj.AcquireDate1,locObj.wsObj.JobTitle2,
				locObj.wsObj.AcquireDate2,locObj.wsObj.Discipline,locObj.wsObj.JusType,locObj.wsObj.PractisingCertificateNo,locObj.wsObj.JG);
		}
		public function login():void
		{
			var locObj:SysUserLocator=SysUserLocator.getInstance();		
			Ws.Login.send(locObj.wsObj.POLICENO,locObj.wsObj.PASSWORD);			
			PsbLocator.getInstance().npending=false;
		}
		public function getAll():void
		{
			var locObj:SysUserLocator=SysUserLocator.getInstance();		
			Ws.GetAll.send(locObj.wsObj.PSBID,locObj.wsObj.OFFICE,locObj.wsObj.POLICENO,locObj.wsObj.NAME);			
			PsbLocator.getInstance().npending=false;
		}
		public function getDeletedPerson():void
		{
			var locObj:SysUserLocator=SysUserLocator.getInstance();		
			Ws.GetDeletedPerson.send(locObj.wsObj.POLICENO,locObj.wsObj.IDCARDNO,locObj.listPager.pageSize,locObj.listPager.pageIndex);			
			PsbLocator.getInstance().npending=false;
		}
		public function getConsignPerson():void
		{
			var locObj:SysUserLocator=SysUserLocator.getInstance();		
			Ws.GetConsignPerson.send(locObj.wsObj.PSBID,locObj.wsObj.POLICENO,locObj.wsObj.NAME,
				locObj.listPager.pageSize,locObj.listPager.pageIndex);			
			PsbLocator.getInstance().npending=false;
		}
	}
}