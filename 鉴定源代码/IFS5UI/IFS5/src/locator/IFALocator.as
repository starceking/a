package locator
{
	import com.adobe.cairngorm.control.CairngormEventDispatcher;
	
	import control.*;
	
	import com.adobe.cairngorm.model.ModelLocator;
	import util.Helper;
	
	import locator.CodiesLocator;
	import locator.IdFlowLocator;
	
	import vo.StrVo;
	
	public class IFALocator implements ModelLocator
	{
		//Singleton
		private static var locObj:IFALocator;
		public static function getInstance():IFALocator
		{
			if(locObj==null)
			{
				locObj=new IFALocator();
			}
			return locObj;
		}
		//For the view
		public var caseId:String="";
		public var conno:String="";
		public var ip:String="";
		public function readStr():void
		{
			Helper.showAlert("读取完毕！");
			var conno:String=IdFlowLocator.getInstance().curObj.CONNO;
			CodiesLocator.getInstance().wsObj=new StrVo("","",conno,"","","","","","","","","","","","","",
				"","","","","","","","","","","","","","","","",
				"","","","","","","","","","","","","","","","","","","","","","","","","");
			CairngormEventDispatcher.getInstance().dispatchEvent(new IFSEvent(IFSControl.CODIESWS_GetAllTmpStr));
		}
	}
}