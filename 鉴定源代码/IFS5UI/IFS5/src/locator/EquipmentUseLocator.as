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
	
	import vo.EquipmentUseVo;
	public class EquipmentUseLocator  implements ModelLocator
	{
		//Singleton
		private static var locObj:EquipmentUseLocator;
		public static function getInstance():EquipmentUseLocator	
		{
			if(locObj==null)
			{
				locObj=new EquipmentUseLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		[Bindable]
		public var mackObj:ArrayList=new ArrayList();
		[Bindable]
		public var currObj:EquipmentUseVo;
		public var sqlcon:String="";
		public var syr:String="";
		//For the Ws
		public var wsObj:EquipmentUseVo;
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
				var voObj:EquipmentUseVo=new EquipmentUseVo(xml.children()[i].ID,xml.children()[i].设备ID,xml.children()[i].使用人,xml.children()[i].开始时间,
					xml.children()[i].结束时间,xml.children()[i].状态,xml.children()[i].使用原因);
				listObj.addItem(voObj);
			}
		}
		
		public function getOne(xml:XML):void
		{
			listObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:EquipmentUseVo=new EquipmentUseVo(xml.children()[i].ID,xml.children()[i].设备ID,xml.children()[i].使用人,xml.children()[i].开始时间,
					xml.children()[i].结束时间,xml.children()[i].状态,xml.children()[i].使用原因);
				listObj.addItem(voObj);
			}
		}
		
		public function mackAllFB (xml:XML):void
		{
			mackObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:EquipmentUseVo=new EquipmentUseVo(xml.children()[i].ID,xml.children()[i].设备ID,xml.children()[i].使用人,xml.children()[i].开始时间,
					xml.children()[i].结束时间,xml.children()[i].状态,xml.children()[i].使用原因);
				mackObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:EquipmentUseVo=listObj.getItemAt(i) as EquipmentUseVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
		
	}
}