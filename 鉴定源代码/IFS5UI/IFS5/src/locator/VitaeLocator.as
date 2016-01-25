package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import util.Helper;
	
	import vo.VitaeVo;
	
	public class VitaeLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:VitaeLocator;
		public static function getInstance():VitaeLocator
		{
			if(locObj==null)
			{
				locObj=new VitaeLocator();
			}
			return locObj;
		}
		
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		//For the Ws
		public var wsObj:VitaeVo;
				
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
			listObj.removeItem(getVo(wsObj.ID));
		}
		public function getAll(xml:XML):void
		{
			listObj.removeAll();		
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:VitaeVo=new VitaeVo(xml.children()[i].ID,xml.children()[i].人员ID,xml.children()[i].学历,xml.children()[i].开始时间,
					xml.children()[i].结束时间,xml.children()[i].所学专业,xml.children()[i].毕业院校,xml.children()[i].备注);
				listObj.addItem(voObj);
			}
		}	
		//Inner call
		private function getVo(id:String):VitaeVo
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var vitaeVo:VitaeVo=listObj.getItemAt(i) as VitaeVo;
				if(vitaeVo.ID==id)
				{
					return vitaeVo;
				}
			}
			return null;
		}
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var voObj:VitaeVo=listObj.getItemAt(i) as VitaeVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}