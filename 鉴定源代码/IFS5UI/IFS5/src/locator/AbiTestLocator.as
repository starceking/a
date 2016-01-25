package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import vo.AbiTestVo;
	public class AbiTestLocator implements ModelLocator
	{
		private static var locObj:AbiTestLocator;
		public static function getInstance():AbiTestLocator
		{
			if(locObj==null)
			{
				locObj=new AbiTestLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		public var listPager:ListPager;
		//For the ws
		[Bindable]
		public var voObj:AbiTestVo;
		public var filename:String="";
		public var wordname:String="";
		//Ws call
		public function insert():void
		{
			listObj.addItem(voObj);
		}
		public function update():void
		{
			var index:int=getVoIndex(voObj.ID);
			listObj.removeItemAt(index);
			listObj.addItemAt(voObj,index);
		}		
		public function deleteFunc():void
		{
			listObj.removeItem(getVo(voObj.ID));
		}
		public function getAll(xml:XML):void
		{
			listObj.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:AbiTestVo=new AbiTestVo(xml.children()[i].ID,xml.children()[i].人员ID,xml.children()[i].姓名,xml.children()[i].部门,
					xml.children()[i].职称,xml.children()[i].考核类别,xml.children()[i].考核时间,xml.children()[i].考核内容
					,xml.children()[i].考核结果,xml.children()[i].考核评价,xml.children()[i].考评负责人,xml.children()[i].考评时间,xml.children()[i].备注);
				listObj.addItem(voObj);
			}
			
		}
		//Inner call
		private function getVo(id:String):AbiTestVo
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var abiTestVo:AbiTestVo=listObj.getItemAt(i) as AbiTestVo;
				if(abiTestVo.ID==id)
				{
					return abiTestVo;
				}
			}
			return null;
		}
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var abiTestVo:AbiTestVo=listObj.getItemAt(i) as AbiTestVo;
				if(abiTestVo.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}