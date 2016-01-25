package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import vo.FruitfulVo;
	
	public class FruitfulLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:FruitfulLocator;
		public static function getInstance():FruitfulLocator
		{
			if(locObj==null)
			{
				locObj=new FruitfulLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		//For the Ws
		public var wsObj:FruitfulVo;
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
				var voObj:FruitfulVo=new FruitfulVo(xml.children()[i].ID,xml.children()[i].人员ID,xml.children()[i].项目名称,
					xml.children()[i].颁发日期,xml.children()[i].颁发单位,xml.children()[i].奖励名称,xml.children()[i].奖励等级,
					xml.children()[i].备注);
				listObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:FruitfulVo=listObj.getItemAt(i) as FruitfulVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}