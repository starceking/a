package business
{
	import com.adobe.cairngorm.business.ServiceLocator;
	
	import control.IFSCommand;
	
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.soap.WebService;
	
	import locator.SysUserLocator;
	import locator.StatisticsLocator;
	
	import util.Helper;
	import util.Server;
	
	public class StatisticsDelegate
	{
		public function StatisticsDelegate(responder:IFSCommand)
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
				ws.loadWSDL(Server.getWsUrl("统计WS"));
				
				ws.PersonWork.addEventListener(ResultEvent.RESULT,getXmlResultHandler);
				ws.StationWork.addEventListener(ResultEvent.RESULT,getXmlResultHandler);	
				ws.GetStation.addEventListener(ResultEvent.RESULT,getXmlResultHandler);	
				ws.CaseProperty.addEventListener(ResultEvent.RESULT,getXmlResultHandler);	
				ws.CaseConclusion.addEventListener(ResultEvent.RESULT,getXmlResultHandler);	
				
				ws.PersonWork.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.StationWork.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.GetStation.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.CaseProperty.addEventListener(FaultEvent.FAULT,faultHandler);
				ws.CaseConclusion.addEventListener(FaultEvent.FAULT,faultHandler);
				
			}
			return ws;
		}
		//Handler
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
		public function PersonWork():void
		{
			Ws.PersonWork.send(SysUserLocator.getInstance().loginUser.PSBID,StatisticsLocator.getInstance().Office,
				StatisticsLocator.getInstance().Person,StatisticsLocator.getInstance().DateTime1,StatisticsLocator.getInstance().DateTime2);
		}
		public function StationWork():void
		{
			Ws.StationWork.send(SysUserLocator.getInstance().loginUser.PSBID,StatisticsLocator.getInstance().Office,
				StatisticsLocator.getInstance().Station,StatisticsLocator.getInstance().StationName,StatisticsLocator.getInstance().State,
				StatisticsLocator.getInstance().DateTime1,StatisticsLocator.getInstance().DateTime2);
		}
		public function GetStation():void
		{
			Ws.GetStation.send();
		}
		public function CaseProperty():void
		{
			Ws.CaseProperty.send(SysUserLocator.getInstance().loginUser.PSBID,StatisticsLocator.getInstance().Office,
				StatisticsLocator.getInstance().Station,StatisticsLocator.getInstance().StationName,StatisticsLocator.getInstance().Property,
				StatisticsLocator.getInstance().DateTime1,StatisticsLocator.getInstance().DateTime2);
		}
		public function CaseConclusion():void
		{
			Ws.CaseConclusion.send(SysUserLocator.getInstance().loginUser.PSBID,SysUserLocator.getInstance().loginUser.OFFICE,
				StatisticsLocator.getInstance().Type,StatisticsLocator.getInstance().Conclusion,
				StatisticsLocator.getInstance().DateTime1,StatisticsLocator.getInstance().DateTime2);
		}

	}
}