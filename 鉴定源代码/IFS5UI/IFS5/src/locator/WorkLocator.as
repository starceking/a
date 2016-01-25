package locator
{		
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import vo.WorkVo;
	
	public class WorkLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:WorkLocator;
		public static function getInstance():WorkLocator
		{
			if(locObj==null)
			{
				locObj=new WorkLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		//For the Ws
		public var wsObj:WorkVo;
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
				var voObj:WorkVo=new WorkVo(xml.children()[i].ID,xml.children()[i].人员ID,xml.children()[i].工作单位,
					xml.children()[i].工作地点,xml.children()[i].开始时间,xml.children()[i].结束时间,xml.children()[i].部门,
					xml.children()[i].职务,xml.children()[i].备注);
				listObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:WorkVo=listObj.getItemAt(i) as WorkVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}