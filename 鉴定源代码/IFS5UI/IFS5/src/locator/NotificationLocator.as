package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	import mx.core.FlexGlobals;
	
	import vo.NotificationVo;
	
	public class NotificationLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:NotificationLocator;
		public static function getInstance():NotificationLocator
		{
			if(locObj==null)
			{
				locObj=new NotificationLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		public var importantList:ArrayList=new ArrayList();
		public var listPager:ListPager;
		//For the ws
		[Bindable]
		public var currentObj:NotificationVo;
		public var voObj:NotificationVo;
		public var ctimes:String="";
		public var ctimee:String="";
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
				var voObj:NotificationVo=new NotificationVo(xml.children()[i].ID,xml.children()[i].标题,xml.children()[i].内容,
					xml.children()[i].是否重要,xml.children()[i].发布人,xml.children()[i].创建时间);
				listObj.addItem(voObj);
			}
			if(listPager!=null)
			{
				if(xml.children().length()>0)
				{
					listPager.RowCount=xml.children()[0].RowCount;
				}
				else
				{
					listPager.RowCount="0";
				}
			}
		}
		public function GetImportant(xml:XML):void
		{
			if(xml.children().length()>0)
			{
				for(var i:int=0;i<xml.children().length();i++)
				{
					currentObj=new NotificationVo(xml.children()[i].ID,xml.children()[i].标题,xml.children()[i].内容,
						xml.children()[i].是否重要,xml.children()[i].发布人,xml.children()[i].创建时间);
					FlexGlobals.topLevelApplication.showNotiModule();
				}
			}
		}
		//Inner call
		private function getVo(id:String):NotificationVo
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var notificationVo:NotificationVo=listObj.getItemAt(i) as NotificationVo;
				if(notificationVo.ID==id)
				{
					return notificationVo;
				}
			}
			return null;
		}
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var notificationVo:NotificationVo=listObj.getItemAt(i) as NotificationVo;
				if(notificationVo.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}