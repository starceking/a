package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import vo.IdPersonVo;
	
	public class IdPersonLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:IdPersonLocator;
		public static function getInstance():IdPersonLocator
		{
			if(locObj==null)
			{
				locObj=new IdPersonLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		//For the Ws
		public var wsObj:IdPersonVo;
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
				var voObj:IdPersonVo=new IdPersonVo(xml.children()[i].ID,xml.children()[i].委托编号,xml.children()[i].姓名,
					xml.children()[i].性别,xml.children()[i].身份证,xml.children()[i].电话,xml.children()[i].出生日期,
					xml.children()[i].年龄,xml.children()[i].职业,xml.children()[i].学历,xml.children()[i].籍贯,xml.children()[i].工作地点,
					xml.children()[i].现住址);
				listObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:IdPersonVo=listObj.getItemAt(i) as IdPersonVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}