package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	import locator.PsbLocator;
	import locator.JusTypeLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	import vo.JusTypeVo;
	
	public class JusTypeDelegate
	{
		public function JusTypeDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("JUSTYPEWS"));
				
				ws.GetXml.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				
				ws.InsertOffice.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateOffice.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.DeleteOffice.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.InsertType.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateType.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.DeleteType.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateAdmin.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateDna.addEventListener(ResultEvent.RESULT,resultHandler);
				
				ws.GetXml.addEventListener(FaultEvent.FAULT,faultHandler);
				
				ws.InsertOffice.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateOffice.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.DeleteOffice.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.InsertType.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateType.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.DeleteType.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateAdmin.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateDna.addEventListener(FaultEvent.FAULT,faultHandler);
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
		private var JusLoc:JusTypeLocator=JusTypeLocator.getInstance();
		//External call
		public function getXml():void
		{
			Ws.GetXml.send();
			PsbLocator.getInstance().npending=false;
		}
		public function InsertOffice():void
		{
			Ws.InsertOffice.send(
				PsbLocator.getInstance().ID_PSB_ID,
				JusLoc.officeVo.Name,JusLoc.officeVo.Leader,JusLoc.officeVo.PlanDate);
			PsbLocator.getInstance().npending=false;
		}
		public function UpdateOffice():void
		{
			Ws.UpdateOffice.send(
				PsbLocator.getInstance().ID_PSB_ID,JusLoc.Office,
				JusLoc.officeVo.Name,JusLoc.officeVo.Leader,JusLoc.officeVo.PlanDate);
			PsbLocator.getInstance().npending=false;
		}
		public function DeleteOffice():void
		{
			Ws.DeleteOffice.send(
				PsbLocator.getInstance().ID_PSB_ID,JusLoc.Office);
			PsbLocator.getInstance().npending=false;
		}
		public function InsertType():void
		{
			Ws.InsertType.send(
				PsbLocator.getInstance().ID_PSB_ID,JusLoc.Office,
				JusLoc.TypeVo.Name,JusLoc.TypeVo.DocName,JusLoc.TypeVo.TESNAME,JusLoc.TypeVo.JUSITEM,
				JusLoc.TypeVo.IDREQ,JusLoc.TypeVo.SESLN,JusLoc.TypeVo.CONCLUSION);
			PsbLocator.getInstance().npending=false;
		}
		public function UpdateType():void
		{
			Ws.UpdateType.send(
				PsbLocator.getInstance().ID_PSB_ID,JusLoc.Office,JusLoc.JUSTYPE,
				JusLoc.TypeVo.Name,JusLoc.TypeVo.DocName,JusLoc.TypeVo.TESNAME,JusLoc.TypeVo.JUSITEM,
				JusLoc.TypeVo.IDREQ,JusLoc.TypeVo.SESLN,JusLoc.TypeVo.CONCLUSION);
			PsbLocator.getInstance().npending=false;
		}
		public function DeleteType():void
		{
			Ws.DeleteType.send(
				PsbLocator.getInstance().ID_PSB_ID,JusLoc.Office,JusLoc.JUSTYPE);
			PsbLocator.getInstance().npending=false;
		}
		public function UpdateAdmin():void
		{
			Ws.UpdateAdmin.send(
				PsbLocator.getInstance().ID_PSB_ID,
				JusLoc.DnaVo.YEAR,JusLoc.DnaVo.TESTER,JusLoc.DnaVo.TESTER2,JusLoc.DnaVo.TESTER3,JusLoc.DnaVo.TESTER4,
				JusLoc.DnaVo.CHECKER,JusLoc.DnaVo.SIGN,JusLoc.DnaVo.TECH,JusLoc.DnaVo.LEADER,JusLoc.DnaVo.TESTERSD);
			PsbLocator.getInstance().npending=false;
		}
		public function UpdateDna():void
		{
			Ws.UpdateDna.send(
				PsbLocator.getInstance().ID_PSB_ID,
				JusLoc.DnaVo.Leader,JusLoc.DnaVo.WholeNo,JusLoc.DnaVo.DocName,JusLoc.DnaVo.PlanDate,JusLoc.DnaVo.Enabled,
				JusLoc.DnaVo.IDREQ,JusLoc.DnaVo.CLN,JusLoc.DnaVo.SESLN,JusLoc.DnaVo.CPSSLN,JusLoc.DnaVo.RSLN,
				JusLoc.DnaVo.USLN,JusLoc.DnaVo.LSLN,JusLoc.DnaVo.LRSLN);
			PsbLocator.getInstance().npending=false;
		}
	}
}