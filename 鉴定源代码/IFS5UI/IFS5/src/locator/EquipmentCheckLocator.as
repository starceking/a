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
	
	import vo.EquipmentCheckVo;
	public class EquipmentCheckLocator  implements ModelLocator
	{
		//Singleton
		private static var locObj:EquipmentCheckLocator;
		public static function getInstance():EquipmentCheckLocator	
		{
			if(locObj==null)
			{
				locObj=new EquipmentCheckLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		[Bindable]
		public var mackObj:ArrayList=new ArrayList();
		[Bindable]
		public var currObj:EquipmentCheckVo;
		
		//For the Ws
		public var wsObj:EquipmentCheckVo;
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
				var voObj:EquipmentCheckVo=new EquipmentCheckVo(xml.children()[i].ID,xml.children()[i].设备ID,xml.children()[i].查核人,xml.children()[i].监督人,
					xml.children()[i].核查结果,xml.children()[i].核查日期,xml.children()[i].核查内容);
				listObj.addItem(voObj);
			}
		}
		
		public function getOne(xml:XML):void
		{
			listObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:EquipmentCheckVo=new EquipmentCheckVo(xml.children()[i].ID,xml.children()[i].设备ID,xml.children()[i].查核人,xml.children()[i].监督人,
					xml.children()[i].核查结果,xml.children()[i].核查日期,xml.children()[i].核查内容);
				listObj.addItem(voObj);
			}
		}
		
		public function mackAllFB (xml:XML):void
		{
			mackObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:EquipmentCheckVo=new EquipmentCheckVo(xml.children()[i].ID,xml.children()[i].设备ID,xml.children()[i].查核人,xml.children()[i].监督人,
					xml.children()[i].核查结果,xml.children()[i].核查日期,xml.children()[i].核查内容);
				mackObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:EquipmentCheckVo=listObj.getItemAt(i) as EquipmentCheckVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}