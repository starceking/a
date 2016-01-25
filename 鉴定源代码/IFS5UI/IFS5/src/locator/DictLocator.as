package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import util.Helper;
	
	import vo.DictVo;

	public class DictLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:DictLocator;
		public static function getInstance():DictLocator
		{
			if(locObj==null)
			{
				locObj=new DictLocator();
			}
			return locObj;
		}
		//For the view
		public var dictList:ArrayList=new ArrayList();
		public var dictNameList:ArrayList=new ArrayList();
		
		public var itemitemName:String;
		public var dictdictName:String;
		//Ws call		
		public function getXml(xml:XML):void
		{			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var name:String=xml.Dict[i].@Name;
				var itemList:ArrayList=new ArrayList();
				for(var j:int=0;j<xml.Dict[i].children().length();j++)
				{
					itemList.addItem({label:xml.Dict[i].children()[j].children(),data:xml.Dict[i].children()[j].children()});
				}
				var voObj:DictVo=new DictVo(name,itemList);
				dictList.addItem(voObj);
			}
		}
		//External call
		public function getItem(dictName:String):ArrayList
		{
			var voObj:DictVo=getDict(dictName);
			if(voObj==null)
			{
				return new ArrayList();
			}
			return voObj.Item;
		}
		public function getDictDict(name:String):ArrayList
		{
			
			for(var i:int=0;i<dictList.length;i++)
			{
				var voObj:DictVo=dictList.getItemAt(i) as DictVo;
				if(voObj.Name==name)
				{
					break;
				}
				dictNameList.addItemAt(voObj.Name,i);
			}
			return dictNameList;
			
		}
		//Inner call		
		private function getDict(dictName:String):DictVo
		{
			for(var i:int=0;i<dictList.length;i++)
			{
				var voObj:DictVo=dictList.getItemAt(i) as DictVo;
				if(voObj.Name==dictName)
				{
					return voObj;
				}
			}
			return null;
		}
		public function insertItem():void
		{
			var itemList:ArrayList=getItem(dictdictName);
			var aa:Boolean=true;
			for(var i:int=0;i<itemList.length;i++)
			{
				if(itemList.getItemAt(i).label==itemitemName)
				{
					Helper.showAlert("列表已经存在该项！");
					aa=false;
					break;
				}
			}
			if(aa==true)
			{
			itemList.addItem({label:itemitemName});
			}
		}
		public function deleteItem():void
		{
			var itemList:ArrayList=getItem(dictdictName);
			for(var i:int=0;i<itemList.length;i++)
			{
				if(itemList.getItemAt(i).label==itemitemName)
				{
					itemList.removeItemAt(i);
					break;
				}
			}
		}
		public function SaveDNATestItem():void
		{
			var itemList:ArrayList=getItem(dictdictName);
			Helper.showAlert("保存成功！");
			itemList.addItem({label:itemitemName});
		}
	}
}