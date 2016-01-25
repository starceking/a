package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import locator.CasePersonnelSampleQtrLocator;
	import locator.CasePersonnelSampleShrLocator;
	import locator.CasePersonnelSampleXyrLocator;
	import locator.IdCaseLocator;
	import locator.JusTypeLocator;
	import locator.PsbLocator;
	import locator.SysUserLocator;
	import locator.IdFlowLocator;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import util.Helper;
	import util.Server;
	
	import vo.CasePersonnelSampleVo;
	
	public class CasePersonnelSampleDelegate
	{
		public function CasePersonnelSampleDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("涉案人员WS"));
				
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
			updatePending(true);
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
			updatePending(true);
		}
		private function getXmlResultHandler(evt:ResultEvent):void
		{
			responder.onResult(new XML(evt.result));	
			updatePending(true);
		}
		private function faultHandler(evt:FaultEvent):void
		{
			responder.onFault(evt.fault.faultString);	
			updatePending(true);
		}
		//Object
		private var responder:IFSCommand;
		//External call
		public function insert():void
		{
			var wsObj:CasePersonnelSampleVo=getVo();
			Ws.Insert.send(wsObj.ID,wsObj.CASE_ID,wsObj.CONNO,wsObj.SAMPLE_CATEGORY,wsObj.NAME,wsObj.SAMPLE_TYPE,wsObj.GENDER,
				wsObj.PERSONNEL_TYPE,wsObj.BIRTH_DATE,wsObj.NATIONALITY,wsObj.DISTRICT,wsObj.ID_CARD_NO,wsObj.EDUCATION_LEVEL,
				wsObj.IDENTITY,wsObj.NATIVE_PLACE_ADDR,
				wsObj.RESIDENCE_ADDR,wsObj.SAMPLE_PACKAGING,wsObj.SAMPLE_DESCRIPTION,wsObj.REMARK);			
			updatePending(false);
		}
		public function insertWithNo():void
		{
			var wsObj:CasePersonnelSampleVo=getVo();
			Ws.InsertWithNo.send(wsObj.ID,wsObj.CASE_ID,wsObj.CONNO,wsObj.SAMPLE_CATEGORY,wsObj.NAME,wsObj.SAMPLE_TYPE,wsObj.GENDER,
				wsObj.PERSONNEL_TYPE,wsObj.BIRTH_DATE,wsObj.NATIONALITY,wsObj.DISTRICT,wsObj.ID_CARD_NO,wsObj.EDUCATION_LEVEL,
				wsObj.IDENTITY,wsObj.NATIVE_PLACE_ADDR,
				wsObj.RESIDENCE_ADDR,wsObj.SAMPLE_PACKAGING,wsObj.SAMPLE_DESCRIPTION,wsObj.REMARK,
				Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.CPSSLN,
					IdCaseLocator.getInstance().bsMainObj.ACC_YEAR,IdCaseLocator.getInstance().bsMainObj.ACC_CASE_NO,""),
				JusTypeLocator.getInstance().jusTypeVo.WholeNo=="1"?"样本视图":"涉案人员",
				Helper.getSlnNumLen(JusTypeLocator.getInstance().jusTypeVo.CPSSLN),
				SysUserLocator.getInstance().loginUser.NAME,IdFlowLocator.getInstance().curObj.ORA_FLAG);		
			updatePending(false);
		}
		public function update():void
		{
			var wsObj:CasePersonnelSampleVo=getVo();
			Ws.Update.send(wsObj.ID,wsObj.NAME,wsObj.SAMPLE_TYPE,wsObj.GENDER,
				wsObj.PERSONNEL_TYPE,wsObj.BIRTH_DATE,wsObj.NATIONALITY,wsObj.DISTRICT,wsObj.ID_CARD_NO,wsObj.EDUCATION_LEVEL,
				wsObj.IDENTITY,wsObj.NATIVE_PLACE_ADDR,
				wsObj.RESIDENCE_ADDR,wsObj.SAMPLE_PACKAGING,wsObj.SAMPLE_DESCRIPTION,wsObj.REMARK,wsObj.SLN,wsObj.ORA_FLAG);			
			updatePending(false);
		}
		public function deleteObj():void
		{
			var wsObj:CasePersonnelSampleVo=getVo();
			Ws.Delete.send(wsObj.ID,wsObj.ORA_FLAG);
			updatePending(false);
		}
		public function getAll():void
		{
			var wsObj:CasePersonnelSampleVo=getVo();
			var listPager:ListPager=getListPager();
			Ws.GetAll.send(wsObj.CASE_ID,wsObj.CONNO,wsObj.SAMPLE_CATEGORY,listPager.pageSize,listPager.pageIndex);
			updatePending(false);
		}
		private function getVo():CasePersonnelSampleVo
		{
			var wsObj:CasePersonnelSampleVo=null;
			if(responder.evtType.search("CASEPERSONNELSAMPLE_SHRWS_")==0)
			{
				wsObj=CasePersonnelSampleShrLocator.getInstance().wsObj;
			}
			else if(responder.evtType.search("CASEPERSONNELSAMPLE_XYRWS_")==0)
			{
				wsObj=CasePersonnelSampleXyrLocator.getInstance().wsObj;
			}	
			else if(responder.evtType.search("CASEPERSONNELSAMPLE_QTRWS_")==0)
			{
				wsObj=CasePersonnelSampleQtrLocator.getInstance().wsObj;
			}
			return wsObj;
		}
		private function getListPager():ListPager
		{
			var listPager:ListPager=null;
			if(responder.evtType.search("CASEPERSONNELSAMPLE_SHRWS_")==0)
			{
				listPager=CasePersonnelSampleShrLocator.getInstance().listPager;
			}
			else if(responder.evtType.search("CASEPERSONNELSAMPLE_XYRWS_")==0)
			{
				listPager=CasePersonnelSampleXyrLocator.getInstance().listPager;
			}	
			else if(responder.evtType.search("CASEPERSONNELSAMPLE_QTRWS_")==0)
			{
				listPager=CasePersonnelSampleQtrLocator.getInstance().listPager;
			}
			return listPager;
		}
		private function updatePending(rel:Boolean):void
		{
			PsbLocator.getInstance().npending=rel;
		}
	}
}