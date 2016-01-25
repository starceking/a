package locator
{
	import com.adobe.cairngorm.control.CairngormEventDispatcher;
	import com.adobe.cairngorm.model.ModelLocator;
	
	import control.*;
	
	import flash.external.ExternalInterface;
	
	import mx.collections.ArrayList;
	import mx.managers.BrowserManager;
	import mx.managers.IBrowserManager;
	
	import util.Helper;
	
	import vo.SuppliesVerificationVo;
	public class SuppliesVerificationLocator  implements ModelLocator
	{
		//Singleton
		private static var locObj:SuppliesVerificationLocator;
		public static function getInstance():SuppliesVerificationLocator	
		{
			if(locObj==null)
			{
				locObj=new SuppliesVerificationLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		[Bindable]
		public var mackObj:ArrayList=new ArrayList();
		[Bindable]
		public var currObj:SuppliesVerificationVo;
		public var filename:String="";
		public var wordname:String="";
		
		//For the Ws
		public var wsObj:SuppliesVerificationVo;
		//Ws call
		public function insert():void
		{
			listObj.addItem(wsObj);
		}
		public function update():void
		{
			var index:int=getVoIndex(wsObj.ID);
			listObj.removeItemAt(index);
			listObj.addItemAt(wsObj,index);
		}	
		public function deleteFunc():void
		{
			listObj.removeItemAt(getVoIndex(wsObj.ID));
		}
		public function getAll(xml:XML):void
		{
			listObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:SuppliesVerificationVo=new SuppliesVerificationVo(xml.children()[i].ID,xml.children()[i].耗材ID,xml.children()[i].查核人,xml.children()[i].监督人,
					xml.children()[i].查核日期,xml.children()[i].查核结果,xml.children()[i].查核过程);
				listObj.addItem(voObj);
			}
		}
		
		public function getOne(xml:XML):void
		{
			listObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:SuppliesVerificationVo=new SuppliesVerificationVo(xml.children()[i].ID,xml.children()[i].耗材ID,xml.children()[i].查核人,xml.children()[i].监督人,
					xml.children()[i].查核日期,xml.children()[i].查核结果,xml.children()[i].查核过程);
				listObj.addItem(voObj);
			}
		}
		
		public function mackAllFB (xml:XML):void
		{
			mackObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:SuppliesVerificationVo=new SuppliesVerificationVo(xml.children()[i].ID,xml.children()[i].耗材ID,xml.children()[i].查核人,xml.children()[i].监督人,
					xml.children()[i].查核日期,xml.children()[i].查核结果,xml.children()[i].查核过程);
				mackObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:SuppliesVerificationVo=listObj.getItemAt(i) as SuppliesVerificationVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
		
	}
}