package locator
{
	import com.adobe.cairngorm.control.CairngormEventDispatcher;
	import com.adobe.cairngorm.model.ModelLocator;
	
	import control.*;
	
	import flash.external.ExternalInterface;
	
	import mx.collections.ArrayList;
	
	import util.Helper;
	
	import vo.RegionCodeVo;
	
	public class RegionCodeLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:RegionCodeLocator;
		public static function getInstance():RegionCodeLocator
		{
			if(locObj==null)
			{
				locObj=new RegionCodeLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var regionList:ArrayList=new ArrayList();
		[Bindable]
		public var regionList11:ArrayList=new ArrayList();//行政区划省
		[Bindable]
		public var regionList12:ArrayList=new ArrayList();//行政区划市
		[Bindable]
		public var regionList13:ArrayList=new ArrayList();//行政区划区县
		//for the ws
		[Bindable]
		public var curObj:RegionCodeVo;
		public var wsObj:RegionCodeVo;
		//Ws call
		public function getAll(xml:XML):void
		{		
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:RegionCodeVo=new RegionCodeVo(xml.children()[i].ID,xml.children()[i].区划代码,xml.children()[i].区划名称,xml.children()[i].类型);
				
				if(xml.children()[i].类型=="省")
				{
					regionList11.addItem(voObj);
				}
				else if(xml.children()[i].类型=="市")
				{
					regionList12.addItem(voObj);
				}
				else if(xml.children()[i].类型=="区县")
				{
					regionList13.addItem(voObj);
				}
			}
		}

	}
}