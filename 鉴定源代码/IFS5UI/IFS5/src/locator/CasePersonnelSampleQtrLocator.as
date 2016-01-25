package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import vo.CasePersonnelSampleVo;
	
	public class CasePersonnelSampleQtrLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:CasePersonnelSampleQtrLocator;
		public static function getInstance():CasePersonnelSampleQtrLocator
		{
			if(locObj==null)
			{
				locObj=new CasePersonnelSampleQtrLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();		
		public var listPager:ListPager;
		//For the Ws
		public var wsObj:CasePersonnelSampleVo;
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
				var voObj:CasePersonnelSampleVo=new CasePersonnelSampleVo(xml.children()[i].ID,xml.children()[i].案件ID,xml.children()[i].委托编号,
					xml.children()[i].库类型,xml.children()[i].姓名,xml.children()[i].样本类型,
					xml.children()[i].性别,xml.children()[i].人员类型,xml.children()[i].出生日期,xml.children()[i].民族,
					xml.children()[i].国籍,xml.children()[i].身份证,xml.children()[i].学历,xml.children()[i].身份,xml.children()[i].籍贯,
					xml.children()[i].现住址,xml.children()[i].包装情况,xml.children()[i].样本描述,xml.children()[i].备注,
					xml.children()[i].样本编号,xml.children()[i].ORA_FLAG);
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
				var voObj:CasePersonnelSampleVo=listObj.getItemAt(i) as CasePersonnelSampleVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}