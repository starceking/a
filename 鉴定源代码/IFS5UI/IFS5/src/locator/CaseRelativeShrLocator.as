package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import vo.CaseRelativeVo;
	
	public class CaseRelativeShrLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:CaseRelativeShrLocator;
		public static function getInstance():CaseRelativeShrLocator
		{
			if(locObj==null)
			{
				locObj=new CaseRelativeShrLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		//For the Ws
		public var wsObj:CaseRelativeVo;
		//Ws call	
		public function insert():void
		{		
			if(wsObj.RELATION=="单亲")
				wsObj.RELATIVE2_ID="";
			listObj.addItem(wsObj);
		}
		public function insertWithNo(sln:String):void
		{
			if(wsObj.RELATION=="单亲")
				wsObj.RELATIVE2_ID="";			
			var arr:Array=sln.split("￥");
			wsObj.R1_SLN=arr[0];
			if(arr.length==2)wsObj.R2_SLN=arr[1];
			listObj.addItem(wsObj);
		}
		public function update():void
		{
			if(wsObj.RELATION=="单亲")
				wsObj.RELATIVE2_ID="";
			
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
				var voObj:CaseRelativeVo=new CaseRelativeVo(xml.children()[i].ID,xml.children()[i].案件ID,xml.children()[i].委托编号,
					xml.children()[i].库类型,xml.children()[i].亲属关系,xml.children()[i].亲属一ID,
					xml.children()[i].亲属二ID,xml.children()[i].姓名,xml.children()[i].亲属一姓名,xml.children()[i].亲属一样本类型,
					xml.children()[i].亲属一性别,xml.children()[i].亲属一身份证,xml.children()[i].亲属一籍贯,xml.children()[i].亲属一样本描述,
					xml.children()[i].亲属一目标关系,xml.children()[i].亲属一样本编号,
					xml.children()[i].亲属二姓名,xml.children()[i].亲属二样本类型,
					xml.children()[i].亲属二性别,xml.children()[i].亲属二身份证,xml.children()[i].亲属二籍贯,xml.children()[i].亲属二样本描述,
					xml.children()[i].亲属二目标关系,xml.children()[i].亲属二样本编号,xml.children()[i].ORA_FLAG);
				listObj.addItem(voObj);
			}
		}
		//Inner call
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:CaseRelativeVo=listObj.getItemAt(i) as CaseRelativeVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}