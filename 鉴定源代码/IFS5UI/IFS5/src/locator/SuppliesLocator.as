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
	
	import vo.SuppliesVo;
	public class SuppliesLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:SuppliesLocator;
		public static function getInstance():SuppliesLocator	
		{
			if(locObj==null)
			{
				locObj=new SuppliesLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		[Bindable]
		public var mackObj:ArrayList=new ArrayList();
		[Bindable]
		public var currObj:SuppliesVo;
		
		//For the Ws
		public var wordName:String="";
		public var filename:String="";
		public var num:String="";
		public var wsObj:SuppliesVo;
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
				var voObj:SuppliesVo=new SuppliesVo(xml.children()[i].ID,xml.children()[i].产品名称,xml.children()[i].编码,xml.children()[i].规格,
					xml.children()[i].生产厂家,xml.children()[i].批号,xml.children()[i].用途,xml.children()[i].保存条件,
					xml.children()[i].备注,xml.children()[i].库存量,xml.children()[i].存放地点,xml.children()[i].有效期,xml.children()[i].保管人);
				listObj.addItem(voObj);
			}
		}
		
		public function getOne(xml:XML):void
		{
			listObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:SuppliesVo=new SuppliesVo(xml.children()[i].ID,xml.children()[i].产品名称,xml.children()[i].编码,xml.children()[i].规格,
					xml.children()[i].生产厂家,xml.children()[i].批号,xml.children()[i].用途,xml.children()[i].保存条件,
					xml.children()[i].备注,xml.children()[i].库存量,xml.children()[i].存放地点,xml.children()[i].有效期,xml.children()[i].保管人);
				listObj.addItem(voObj);
			}
		}
		
		public function mackAllFB (xml:XML):void
		{
			mackObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:SuppliesVo=new SuppliesVo(xml.children()[i].ID,xml.children()[i].产品名称,xml.children()[i].编码,xml.children()[i].规格,
					xml.children()[i].生产厂家,xml.children()[i].批号,xml.children()[i].用途,xml.children()[i].保存条件,
					xml.children()[i].备注,xml.children()[i].库存量,xml.children()[i].存放地点,xml.children()[i].有效期,xml.children()[i].保管人);
				mackObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:SuppliesVo=listObj.getItemAt(i) as SuppliesVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
		
	}
}