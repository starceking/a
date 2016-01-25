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
	
	import vo.SuppliesProcurementVo;
	public class SuppliesProcurementLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:SuppliesProcurementLocator;
		public static function getInstance():SuppliesProcurementLocator	
		{
			if(locObj==null)
			{
				locObj=new SuppliesProcurementLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		[Bindable]
		public var mackObj:ArrayList=new ArrayList();
		[Bindable]
		public var currObj:SuppliesProcurementVo;
		
		//For the Ws
		public var wordName:String="";
		public var filename:String="";
		public var num:String="";
		public var num1:String="";
		public var wsObj:SuppliesProcurementVo;
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
				var voObj:SuppliesProcurementVo=new SuppliesProcurementVo(xml.children()[i].ID,xml.children()[i].耗材ID,xml.children()[i].采购人,xml.children()[i].采购数量,
					xml.children()[i].批号,xml.children()[i].采购日期,xml.children()[i].预算价格);
				listObj.addItem(voObj);
			}
		}
		
		public function getOne(xml:XML):void
		{
			listObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:SuppliesProcurementVo=new SuppliesProcurementVo(xml.children()[i].ID,xml.children()[i].耗材ID,xml.children()[i].采购人,xml.children()[i].采购数量,
					xml.children()[i].批号,xml.children()[i].采购日期,xml.children()[i].预算价格);
				listObj.addItem(voObj);
			}
		}
		
		public function mackAllFB (xml:XML):void
		{
			mackObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:SuppliesProcurementVo=new SuppliesProcurementVo(xml.children()[i].ID,xml.children()[i].耗材ID,xml.children()[i].采购人,xml.children()[i].采购数量,
					xml.children()[i].批号,xml.children()[i].采购日期,xml.children()[i].预算价格);
				mackObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:SuppliesProcurementVo=listObj.getItemAt(i) as SuppliesProcurementVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
		
	}
}