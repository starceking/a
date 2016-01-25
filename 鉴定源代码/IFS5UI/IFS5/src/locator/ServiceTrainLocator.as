package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import vo.ServiceTrainVo;
	public class ServiceTrainLocator implements ModelLocator
	{
		private static var locObj:ServiceTrainLocator;
		public static function getInstance():ServiceTrainLocator
		{
			if(locObj==null)
			{
				locObj=new ServiceTrainLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		public var listPager:ListPager;
		//For the ws
		[Bindable]
		public var voObj:ServiceTrainVo;
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
				var voObj:ServiceTrainVo=new ServiceTrainVo(xml.children()[i].ID,xml.children()[i].人员ID,xml.children()[i].教师姓名,xml.children()[i].科室,
					xml.children()[i].职称,xml.children()[i].学员姓名,xml.children()[i].单位,xml.children()[i].学历
					,xml.children()[i].学习时间,xml.children()[i].学习目的,xml.children()[i].教授内容,xml.children()[i].学员考评结果,xml.children()[i].考评意见,xml.children()[i].专业负责人,xml.children()[i].记录时间);
				listObj.addItem(voObj);
			}
			
		}
		//Inner call
		private function getVo(id:String):ServiceTrainVo
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var serviceTrainVo:ServiceTrainVo=listObj.getItemAt(i) as ServiceTrainVo;
				if(serviceTrainVo.ID==id)
				{
					return serviceTrainVo;
				}
			}
			return null;
		}
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var serviceTrainVo:ServiceTrainVo=listObj.getItemAt(i) as ServiceTrainVo;
				if(serviceTrainVo.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}