package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import vo.TrainNoteVo;
	public class TrainLocator implements ModelLocator
	{
		private static var locObj:TrainLocator;
		public static function getInstance():TrainLocator
		{
			if(locObj==null)
			{
				locObj=new TrainLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		public var listPager:ListPager;
		//For the ws
		[Bindable]
		public var voObj:TrainNoteVo;
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
				var voObj:TrainNoteVo=new TrainNoteVo(xml.children()[i].ID,xml.children()[i].人员ID,xml.children()[i].姓名,xml.children()[i].检验室,
					xml.children()[i].培训项目,xml.children()[i].培训教师,xml.children()[i].培训时间,xml.children()[i].培训目的
					,xml.children()[i].培训过程,xml.children()[i].培训效果评价,xml.children()[i].记录人,xml.children()[i].记录时间,xml.children()[i].培训记录ID);
				listObj.addItem(voObj);
			}
			
		}
		//Inner call
		private function getVo(id:String):TrainNoteVo
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var trainNoteVo:TrainNoteVo=listObj.getItemAt(i) as TrainNoteVo;
				if(trainNoteVo.ID==id)
				{
					return trainNoteVo;
				}
			}
			return null;
		}
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<listObj.length;i++)
			{
				var trainNoteVo:TrainNoteVo=listObj.getItemAt(i) as TrainNoteVo;
				if(trainNoteVo.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}