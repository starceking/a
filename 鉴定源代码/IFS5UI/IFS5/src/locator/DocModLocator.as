package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import vo.DocModVo;
	
	public class DocModLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:DocModLocator;
		public static function getInstance():DocModLocator
		{
			if(locObj==null)
			{
				locObj=new DocModLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		//For the Ws
		public var wsObj:DocModVo;
		//Ws call	
		public function insert():void
		{
			listObj.addItemAt(wsObj,0);
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
				var voObj:DocModVo=new DocModVo(xml.children()[i].ID,xml.children()[i].委托编号,xml.children()[i].修改人,
					xml.children()[i].修改时间,xml.children()[i].审批人,xml.children()[i].审批时间,xml.children()[i].修改位置,
					xml.children()[i].原内容,xml.children()[i].修改后内容,xml.children()[i].新的编号);
				listObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:DocModVo=listObj.getItemAt(i) as DocModVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}