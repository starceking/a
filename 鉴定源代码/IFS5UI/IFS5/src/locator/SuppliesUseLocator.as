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
	
	import vo.SuppliesUseVo;
	public class SuppliesUseLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:SuppliesUseLocator;
		public static function getInstance():SuppliesUseLocator	
		{
			if(locObj==null)
			{
				locObj=new SuppliesUseLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		[Bindable]
		public var mackObj:ArrayList=new ArrayList();
		[Bindable]
		public var currObj:SuppliesUseVo;
		public var filename:String="";
		public var wordname:String="";
		
		//For the Ws
		public var wsObj:SuppliesUseVo;
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
				var voObj:SuppliesUseVo=new SuppliesUseVo(xml.children()[i].ID,xml.children()[i].耗材ID,xml.children()[i].使用人,xml.children()[i].使用日期,
					xml.children()[i].消耗数量,xml.children()[i].批号);
				listObj.addItem(voObj);
			}
		}
		
		public function getOne(xml:XML):void
		{
			listObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:SuppliesUseVo=new SuppliesUseVo(xml.children()[i].ID,xml.children()[i].耗材ID,xml.children()[i].使用人,xml.children()[i].使用日期,
					xml.children()[i].消耗数量,xml.children()[i].批号);
				listObj.addItem(voObj);
			}
		}
		
		public function mackAllFB (xml:XML):void
		{
			mackObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:SuppliesUseVo=new SuppliesUseVo(xml.children()[i].ID,xml.children()[i].耗材ID,xml.children()[i].使用人,xml.children()[i].使用日期,
					xml.children()[i].消耗数量,xml.children()[i].批号);
				mackObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:SuppliesUseVo=listObj.getItemAt(i) as SuppliesUseVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}