package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import vo.DnaSeVo;
	
	public class DnaSeLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:DnaSeLocator;
		public static function getInstance():DnaSeLocator
		{
			if(locObj==null)
			{
				locObj=new DnaSeLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		public var listPager:ListPager;
		//For the Ws
		public var wsObj:DnaSeVo;
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
				var voObj:DnaSeVo=new DnaSeVo(xml.children()[i].ID,xml.children()[i].案件ID,xml.children()[i].委托编号,xml.children()[i].名称,
					xml.children()[i].样本类型,xml.children()[i].数量,xml.children()[i].承载物,xml.children()[i].样本编号,xml.children()[i].ORA_FLAG,
					xml.children()[i].样本包装);
				listObj.addItem(voObj);
			}
			if(xml.children().length()>0)
			{
				listPager.RowCount=xml.children()[0].RowCount;
			}
			else
			{
				listPager.RowCount="0";
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:DnaSeVo=listObj.getItemAt(i) as DnaSeVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}