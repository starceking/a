package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import util.Helper;
	
	import vo.WordVo;
	public class WordLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:WordLocator;
		public static function getInstance():WordLocator
		{
			if(locObj==null)
			{
				locObj=new WordLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		[Bindable]
		public var consignList:ArrayList=new ArrayList();
		[Bindable]
		public var acceptList:ArrayList=new ArrayList();
		[Bindable]
		public var testList:ArrayList=new ArrayList();
		[Bindable]
		public var reportList:ArrayList=new ArrayList();
		[Bindable]
		public var coverList:ArrayList=new ArrayList();
		[Bindable]
		public var otherList:ArrayList=new ArrayList();
		[Bindable]
		public var maList:ArrayList=new ArrayList();
		//For the Ws
		public var wsObj:WordVo;
		
		public var idPsb:String;
		public var conno:String;//委托编号
		public var jusType:String;
		public var wordType:String;
		
		public var status:String;
		public var isTesNote:String;
		public var tqno:String;
		public var TemplatePath:String;
		public var WordDir:String;
		public var FileName:String;

		public var RecordType:String;//记录类型
		public var RecordID:String;//记录ID
		[Bindable]
		public var pageWord:Boolean=false;//打开word模式为pageWord
		
		[Bindable]
		public var npending:Boolean=true;
		//Ws call		
		public function getCaseWordList(xml:XML):void
		{			
			consignList.removeAll();
			acceptList.removeAll();
			testList.removeAll();
			reportList.removeAll();
			coverList.removeAll();
			otherList.removeAll();
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:WordVo=new WordVo(xml.children()[i].WordType,xml.children()[i].FileName,xml.children()[i].TemplatePath,
					xml.children()[i].WordDir,xml.children()[i].CreationTime,xml.children()[i].LastWriteTime,
					xml.children()[i].OpenWordUrl,xml.children()[i].CONNO);
				switch(voObj.WordType)
				{
					case "委托书":consignList.addItem(voObj);break;
					case "受理书":acceptList.addItem(voObj);break;
					case "意见报告书":reportList.addItem(voObj);break;
					case "检验检查记录":testList.addItem(voObj);break;
					case "封皮":coverList.addItem(voObj);break;
					case "其他":otherList.addItem(voObj);break;
				}
			}
		}
		public function getAllCaseWord(xml:XML):void
		{			
			listObj.removeAll();
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:WordVo=new WordVo(xml.children()[i].WordType,xml.children()[i].FileName,xml.children()[i].TemplatePath,
					xml.children()[i].WordDir,xml.children()[i].CreationTime,xml.children()[i].LastWriteTime,
					xml.children()[i].OpenWordUrl,xml.children()[i].CONNO);
				listObj.addItem(voObj);
			}
		}
		public function getCaseWordManageList(xml:XML):void
		{			
			maList.removeAll();
			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:WordVo=new WordVo("",xml.children()[i].FileName,"",
					xml.children()[i].WordDir,"","",
					xml.children()[i].OpenWordUrl,"");
				maList.addItem(voObj);
			}
		}
		public function deleteWord():void
		{
			Helper.showAlert("已成功删除该文档");
		}
	}
}