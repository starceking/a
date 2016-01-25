package locator
{	
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import vo.WritingVo;
	
	public class WritingLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:WritingLocator;
		public static function getInstance():WritingLocator
		{
			if(locObj==null)
			{
				locObj=new WritingLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		//For the Ws
		public var wsObj:WritingVo;
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
				var voObj:WritingVo=new WritingVo(xml.children()[i].ID,xml.children()[i].人员ID,xml.children()[i].著作名称,
					xml.children()[i].出版单位,xml.children()[i].发布日期,xml.children()[i].具体工作量,xml.children()[i].备注);
				listObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:WritingVo=listObj.getItemAt(i) as WritingVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}