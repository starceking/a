package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import vo.UnknownDeceasedVo;
	
	public class UnknownDeceasedLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:UnknownDeceasedLocator;
		public static function getInstance():UnknownDeceasedLocator
		{
			if(locObj==null)
			{
				locObj=new UnknownDeceasedLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		//For the Ws
		public var wsObj:UnknownDeceasedVo;
		//Ws call
		public function insert():void
		{
			listObj.addItem(wsObj);
		}
		public function insertWithNo(sln:String):void
		{
			wsObj.SLN=sln;
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
				var voObj:UnknownDeceasedVo=new UnknownDeceasedVo(xml.children()[i].ID,xml.children()[i].案件ID,xml.children()[i].委托编号,xml.children()[i].姓名,
					xml.children()[i].样本类型,xml.children()[i].性别,xml.children()[i].包装情况,xml.children()[i].样本描述,
					xml.children()[i].尸体特征,xml.children()[i].大致年龄,xml.children()[i].备注,xml.children()[i].样本编号,xml.children()[i].ORA_FLAG);
				listObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:UnknownDeceasedVo=listObj.getItemAt(i) as UnknownDeceasedVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}