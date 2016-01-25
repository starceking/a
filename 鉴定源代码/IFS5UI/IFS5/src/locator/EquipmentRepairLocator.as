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
	
	import vo.EquipmentRepairVo;
	public class EquipmentRepairLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:EquipmentRepairLocator;
		public static function getInstance():EquipmentRepairLocator	
		{
			if(locObj==null)
			{
				locObj=new EquipmentRepairLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		[Bindable]
		public var mackObj:ArrayList=new ArrayList();
		[Bindable]
		public var currObj:EquipmentRepairVo;
		
		//For the Ws
		public var wordName:String="";
		public var filename:String="";
		public var num:String="";
		public var wsObj:EquipmentRepairVo;
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
				var voObj:EquipmentRepairVo=new EquipmentRepairVo(xml.children()[i].ID,xml.children()[i].设备ID,xml.children()[i].负责人,xml.children()[i].修理时间,
					xml.children()[i].修理原因,xml.children()[i].详细描述);
				listObj.addItem(voObj);
			}
		}
		
		public function getOne(xml:XML):void
		{
			listObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:EquipmentRepairVo=new EquipmentRepairVo(xml.children()[i].ID,xml.children()[i].设备ID,xml.children()[i].负责人,xml.children()[i].修理时间,
					xml.children()[i].修理原因,xml.children()[i].详细描述);
				listObj.addItem(voObj);
			}
		}
		
		public function mackAllFB (xml:XML):void
		{
			mackObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:EquipmentRepairVo=new EquipmentRepairVo(xml.children()[i].ID,xml.children()[i].设备ID,xml.children()[i].负责人,xml.children()[i].修理时间,
					xml.children()[i].修理原因,xml.children()[i].详细描述);
				mackObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:EquipmentRepairVo=listObj.getItemAt(i) as EquipmentRepairVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}