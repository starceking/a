package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import vo.IdTestimonyVo;
	
	public class IdCtrLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:IdCtrLocator;
		public static function getInstance():IdCtrLocator
		{
			if(locObj==null)
			{
				locObj=new IdCtrLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		//For the Ws
		public var wsObj:IdTestimonyVo;
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
				var voObj:IdTestimonyVo=new IdTestimonyVo(xml.children()[i].ID,xml.children()[i].是否样本,xml.children()[i].委托编号,xml.children()[i].名称,
					xml.children()[i].数量,xml.children()[i].重量,xml.children()[i].包装,xml.children()[i].性质,
					xml.children()[i].提取人,xml.children()[i].提取方法,xml.children()[i].提取位置,xml.children()[i].提取时间,xml.children()[i].备注,
					xml.children()[i].材料编号,xml.children()[i].单位);
				listObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:IdTestimonyVo=listObj.getItemAt(i) as IdTestimonyVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}		
			return -1;
		}
	}
}