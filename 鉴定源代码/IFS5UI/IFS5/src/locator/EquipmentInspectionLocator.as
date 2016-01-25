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
	
	import vo.EquipmentInspectionVo;
	public class EquipmentInspectionLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:EquipmentInspectionLocator;
		public static function getInstance():EquipmentInspectionLocator	
		{
			if(locObj==null)
			{
				locObj=new EquipmentInspectionLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		[Bindable]
		public var mackObj:ArrayList=new ArrayList();
		[Bindable]
		public var currObj:EquipmentInspectionVo;
		
		//For the Ws
		public var wsObj:EquipmentInspectionVo;
		public var filename:String="";
		public var wordname:String="";
		public var num:String="";
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
				var voObj:EquipmentInspectionVo=new EquipmentInspectionVo(xml.children()[i].ID,xml.children()[i].设备ID,xml.children()[i].检验机构,xml.children()[i].检验日期,
					xml.children()[i].检验内容,xml.children()[i].检验结果);
				listObj.addItem(voObj);
			}
		}
		
		public function getOne(xml:XML):void
		{
			listObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:EquipmentInspectionVo=new EquipmentInspectionVo(xml.children()[i].ID,xml.children()[i].设备ID,xml.children()[i].检验机构,xml.children()[i].检验日期,
					xml.children()[i].检验内容,xml.children()[i].检验结果);
				listObj.addItem(voObj);
			}
		}
		
		public function mackAllFB (xml:XML):void
		{
			mackObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:EquipmentInspectionVo=new EquipmentInspectionVo(xml.children()[i].ID,xml.children()[i].设备ID,xml.children()[i].检验机构,xml.children()[i].检验日期,
					xml.children()[i].检验内容,xml.children()[i].检验结果);
				mackObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:EquipmentInspectionVo=listObj.getItemAt(i) as EquipmentInspectionVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
		
	}
}