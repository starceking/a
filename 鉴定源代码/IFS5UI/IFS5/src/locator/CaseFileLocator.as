package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import vo.CaseFileVo;
	
	public class CaseFileLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:CaseFileLocator;
		public static function getInstance():CaseFileLocator
		{
			if(locObj==null)
			{
				locObj=new CaseFileLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var picList:ArrayList=new ArrayList();
		[Bindable]
		public var edataList:ArrayList=new ArrayList();
		[Bindable]
		public var wzmsList:ArrayList=new ArrayList();
		[Bindable]
		public var tableList:ArrayList=new ArrayList();
		//For the Ws
		public var fileOffs:String;
		public var fileType:String;
		public var wsObj:CaseFileVo;
		//Ws call
		public function deleteFunc():void
		{
			if(fileType=="照片图像")picList.removeItem(wsObj);
			else if(fileType=="电子材料")edataList.removeItem(wsObj);
			else if(fileType=="物证描述")wzmsList.removeItem(wsObj);
		}
		public function getAll(xml:XML):void
		{
			if(fileType=="照片图像")picList.removeAll();
			else if(fileType=="电子材料")edataList.removeAll();	
			else if(fileType=="物证描述")wzmsList.removeAll();	
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:CaseFileVo=new CaseFileVo(xml.children()[i].FileName,xml.children()[i].Url,xml.children()[i].DiskPath);
				if(fileType=="照片图像")picList.addItem(voObj);
				else if(fileType=="电子材料")edataList.addItem(voObj);
				else if(fileType=="物证描述")wzmsList.addItem(voObj);
			}
		}
		public function getAllFileData(xml:XML):void
		{
			tableList.removeAll();
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:CaseFileVo=new CaseFileVo(xml.children()[i].FileName,
					xml.children()[i].Url,xml.children()[i].DiskPath);
				tableList.addItem(voObj);
			}
		}
	}
}