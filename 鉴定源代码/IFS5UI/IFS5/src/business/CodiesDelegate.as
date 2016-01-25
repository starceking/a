package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import flash.net.FileReference;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	
	import locator.CodiesLocator;
	import locator.IdCaseLocator;
	import locator.IdFlowLocator;
	import locator.PsbLocator;
	import locator.SysUserLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Server;
	
	import vo.StrVo;
	
	public class CodiesDelegate
	{
		public function CodiesDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("CODIESWS"));
				
				ws.GetAll.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.GetAllTmpStr.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.deleteTmpStr.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.deleteAllTmpStr.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateStrFromTmp.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.UpdateAllStrFromTmp.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.GetAllStr.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.UpdateStr.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.PrintStrTable.addEventListener(ResultEvent.RESULT,strTableHandler);
				ws.SameCaseBzAna.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.Import.addEventListener(ResultEvent.RESULT,resultHandler);
				ws.QuerySample.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.PrintCODISdat.addEventListener(ResultEvent.RESULT,printCODISdatHandler);
				
				ws.GetAll.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAllTmpStr.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.deleteTmpStr.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.deleteAllTmpStr.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateStrFromTmp.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateAllStrFromTmp.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetAllStr.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.UpdateStr.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.PrintStrTable.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.SameCaseBzAna.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.Import.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.QuerySample.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.PrintCODISdat.addEventListener(FaultEvent.FAULT,faultHandler);
			}
			return ws;
		}
		//Handler
		private function printCODISdatHandler(evt:ResultEvent):void
		{
			var result:String=evt.result.toString();
			if(result.search(".txt")>=0)
			{
				CodiesLocator.getInstance().fileURL=result;
			}	
			else
			{
				responder.onFault(result);	
			}
			PsbLocator.getInstance().npending=true;
		}
		private function strTableHandler(evt:ResultEvent):void
		{
			var result:String=evt.result.toString();
			navigateToURL(new URLRequest(result));
			PsbLocator.getInstance().npending=true;
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
			Ws.GetAll.send(PsbLocator.getInstance().idPsb.ID,CodiesLocator.getInstance().today);
			PsbLocator.getInstance().npending=false;
		}
		public function getAllTmpStr():void
		{
			var wsObj:StrVo=CodiesLocator.getInstance().wsObj;
			Ws.GetAllTmpStr.send(wsObj.CASE_ID,wsObj.CONNO,wsObj.SLN,wsObj.NAME,wsObj.SC,
				CodiesLocator.getInstance().listPager.pageSize,CodiesLocator.getInstance().listPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
		public function deleteTmpStr():void
		{
			var wsObj:StrVo=CodiesLocator.getInstance().wsObj;
			Ws.deleteTmpStr.send(wsObj.ID);
			PsbLocator.getInstance().npending=false;
		}
		public function deleteAllTmpStr():void
		{
			var wsObj:StrVo=CodiesLocator.getInstance().wsObj;
			Ws.deleteAllTmpStr.send(wsObj.ID);
			PsbLocator.getInstance().npending=false;
		}
		public function updateStrFromTmp():void
		{
			var wsObj:StrVo=CodiesLocator.getInstance().wsObj;
			Ws.UpdateStrFromTmp.send(wsObj.ID,wsObj.SC);
			PsbLocator.getInstance().npending=false;
		}
		public function updateAllStrFromTmp():void
		{
			var wsObj:StrVo=CodiesLocator.getInstance().wsObj;
			Ws.UpdateAllStrFromTmp.send(wsObj.ID,wsObj.SC);
			PsbLocator.getInstance().npending=false;
		}
		public function getAllStr():void
		{
			var wsObj:StrVo=CodiesLocator.getInstance().wsObj;
			Ws.GetAllStr.send(wsObj.CASE_ID,wsObj.CONNO,wsObj.SLN,wsObj.NAME,wsObj.SC,
				CodiesLocator.getInstance().allListPager.pageSize,CodiesLocator.getInstance().allListPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
		public function querySample():void
		{
			var locObj:CodiesLocator=CodiesLocator.getInstance();
			var wsObj:StrVo=locObj.wsObj;
			Ws.QuerySample.send(PsbLocator.getInstance().ID_PSB_ID,wsObj.SLN,wsObj.NAME,locObj.yblx,wsObj.SC,locObj.slsjs,locObj.slsje,locObj.yjr,
				locObj.str,locObj.ystr,locObj.imp,locObj.jystatus,locObj.preexam,locObj.confirm,
				locObj.allListPager.pageSize,locObj.allListPager.pageIndex);
			PsbLocator.getInstance().npending=false;
		}
		public function updateStr():void
		{
			var wsObj:StrVo=CodiesLocator.getInstance().wsObj;
			Ws.UpdateStr.send(wsObj.ID,wsObj.SC, wsObj.AMEL, wsObj.D8S1179, wsObj.D21S11, wsObj.D18S51, wsObj.vWA,
				wsObj.D3S1358, wsObj.FGA, wsObj.TH01, wsObj.D5S818, wsObj.D13S317, wsObj.D7S820, wsObj.CSF1PO, wsObj.D16S539, wsObj.TPOX, wsObj.D2S1338,
				wsObj.D19S433, wsObj.Penta_D, wsObj.Penta_E, wsObj.D6S1043, wsObj.F13A01, wsObj.FESFPS, wsObj.D1S80, wsObj.D12S391, wsObj.D1S1656,
				wsObj.D2S441, wsObj.D22S1045,wsObj.SE33,wsObj.D10S1248,wsObj.Y_indel, wsObj.B_DYS456, wsObj.B_DYS389I,
				wsObj.B_DYS390, wsObj.B_DYS389II, wsObj.G_DYS458, wsObj.G_DYS19, wsObj.G_DYS385, wsObj.Y_DYS393, wsObj.Y_DYS391, wsObj.Y_DYS439, wsObj.Y_DYS635, wsObj.Y_DYS392,
				wsObj.R_Y_GATA_H4, wsObj.R_DYS437, wsObj.R_DYS438, wsObj.R_DYS448,wsObj.IMP_FLAG);
			PsbLocator.getInstance().npending=false;
		}
		public function printStrTable():void
		{
			var wsObj:StrVo=CodiesLocator.getInstance().wsObj;
			Ws.PrintStrTable.send(PsbLocator.getInstance().idPsb.ID,wsObj.CASE_ID,wsObj.CONNO,wsObj.SC,
				CodiesLocator.getInstance().KIT);
			PsbLocator.getInstance().npending=false;
		}
		public function sameCaseBzAna():void
		{
			Ws.SameCaseBzAna.send(IdCaseLocator.getInstance().bsMainObj.ID);
			PsbLocator.getInstance().npending=false;
		}
		public function importStr():void
		{
			var cid:String="";
			if(IdFlowLocator.getInstance().curObj.CONNO.search("D")==0) cid=IdCaseLocator.getInstance().bsMainObj.ID;
			Ws.Import.send(CodiesLocator.getInstance().idsForImp,SysUserLocator.getInstance().loginUser.NAME,cid);
			PsbLocator.getInstance().npending=false;
		}
		public function printCODISdat():void
		{
			Ws.PrintCODISdat.send(PsbLocator.getInstance().idPsb.ID,CodiesLocator.getInstance().fileName,CodiesLocator.getInstance().GeneMapper,
				CodiesLocator.getInstance().RecordType,CodiesLocator.getInstance().SLNIDs);
			PsbLocator.getInstance().npending=false;
		}
	}
}