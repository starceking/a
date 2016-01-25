package locator
{
	import com.adobe.cairngorm.control.CairngormEventDispatcher;
	
	import control.*;
	
	import mx.collections.ArrayList;
	
	import spark.components.TextInput;
	
	import util.Helper;
	
	import vo.StrVo;

	public class CodiesLocator
	{
		//Singleton
		private static var locObj:CodiesLocator;
		public static function getInstance():CodiesLocator
		{
			if(locObj==null)
			{
				locObj=new CodiesLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		[Bindable]
		public var tmpStrList:ArrayList=new ArrayList();
		[Bindable]
		public var allStrList:ArrayList=new ArrayList();
		public var listPager:ListPager;
		public var allListPager:ListPager;
		//for the ws
		public var slsjs:String="";
		public var slsje:String="";
		public var yblx:String="";
		public var yjr:String="";
		public var imp:String="";
		public var str:String="";
		public var ystr:String="";
		public var numinput:TextInput;
		[Bindable]
		public var orc_imp:Boolean=false;
		
		public var KIT:String;
		public var idsForImp:String;
		
		public var jystatus:String="";
		public var preexam:String="";
		public var confirm:String="";
		
		public var wsObj:StrVo;
		public var today:String="1";
		
		public var fileName:String="";
		public var GeneMapper:String="";
		public var RecordType:String="";
		public var SLNIDs:String="";
		[Bindable]
		public var fileURL:String="";
		
		public function getAll(xml:XML):void
		{
			listObj.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{
				listObj.addItem({label:xml.children()[i].FileName,Url:xml.children()[i].Url});
			}
		}
		public function getAllTmpStr(xml:XML):void
		{
			tmpStrList.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:StrVo=new StrVo(xml.children()[i].ID,xml.children()[i].案件ID,xml.children()[i].委托编号,
					xml.children()[i].SAMPLE_ID,xml.children()[i].样本编号,xml.children()[i].样本名称,xml.children()[i].库类型,
					xml.children()[i].AMEL,xml.children()[i].D8S1179,xml.children()[i].D21S11,xml.children()[i].D18S51,xml.children()[i].vWA,
					xml.children()[i].D3S1358,xml.children()[i].FGA,xml.children()[i].TH01,xml.children()[i].D5S818,xml.children()[i].D13S317,
					xml.children()[i].D7S820,xml.children()[i].CSF1PO,xml.children()[i].D16S539,xml.children()[i].TPOX,xml.children()[i].D2S1338,
					xml.children()[i].D19S433,xml.children()[i].Penta_D,xml.children()[i].Penta_E,xml.children()[i].D6S1043,xml.children()[i].F13A01,
					xml.children()[i].FESFPS,xml.children()[i].D1S80,xml.children()[i].D12S391,xml.children()[i].D1S1656,
					xml.children()[i].D2S441, xml.children()[i].D22S1045,xml.children()[i].SE33,xml.children()[i].D10S1248,xml.children()[i].Y_indel,
					xml.children()[i].B_DYS456,xml.children()[i].B_DYS389I,xml.children()[i].B_DYS390,xml.children()[i].B_DYS389II,
					xml.children()[i].G_DYS458,xml.children()[i].G_DYS19,xml.children()[i].G_DYS385,xml.children()[i].Y_DYS393,
					xml.children()[i].Y_DYS391,xml.children()[i].Y_DYS439,xml.children()[i].Y_DYS635,xml.children()[i].Y_DYS392,
					xml.children()[i].R_Y_GATA_H4,xml.children()[i].R_DYS437,xml.children()[i].R_DYS438,xml.children()[i].R_DYS448,
					"","","","",xml.children()[i].样本类型);
				tmpStrList.addItem(voObj);
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
		public function getAllStr(xml:XML):void
		{
			allStrList.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:StrVo=new StrVo(xml.children()[i].ID,xml.children()[i].案件ID,xml.children()[i].委托编号,
					"",xml.children()[i].样本编号,xml.children()[i].名称,xml.children()[i].库类型,
					xml.children()[i].AMEL,xml.children()[i].D8S1179,xml.children()[i].D21S11,xml.children()[i].D18S51,xml.children()[i].vWA,
					xml.children()[i].D3S1358,xml.children()[i].FGA,xml.children()[i].TH01,xml.children()[i].D5S818,xml.children()[i].D13S317,
					xml.children()[i].D7S820,xml.children()[i].CSF1PO,xml.children()[i].D16S539,xml.children()[i].TPOX,xml.children()[i].D2S1338,
					xml.children()[i].D19S433,xml.children()[i].Penta_D,xml.children()[i].Penta_E,xml.children()[i].D6S1043,xml.children()[i].F13A01,
					xml.children()[i].FESFPS,xml.children()[i].D1S80,xml.children()[i].D12S391,xml.children()[i].D1S1656,
					xml.children()[i].D2S441, xml.children()[i].D22S1045,xml.children()[i].SE33,xml.children()[i].D10S1248,xml.children()[i].Y_indel,
					xml.children()[i].B_DYS456,xml.children()[i].B_DYS389I,xml.children()[i].B_DYS390,xml.children()[i].B_DYS389II,
					xml.children()[i].G_DYS458,xml.children()[i].G_DYS19,xml.children()[i].G_DYS385,xml.children()[i].Y_DYS393,
					xml.children()[i].Y_DYS391,xml.children()[i].Y_DYS439,xml.children()[i].Y_DYS635,xml.children()[i].Y_DYS392,
					xml.children()[i].R_Y_GATA_H4,xml.children()[i].R_DYS437,xml.children()[i].R_DYS438,xml.children()[i].R_DYS448,
					xml.children()[i].比中序号,xml.children()[i].IMP_FLAG,xml.children()[i].STR_FLAG,xml.children()[i].YSTR_FLAG,xml.children()[i].样本类型);
				allStrList.addItem(voObj);
			}
			if(xml.children().length()>0)
			{
				allListPager.RowCount=xml.children()[0].RowCount;
			}
			else
			{
				allListPager.RowCount="0";
			}
		}
		public function deleteTmpStr():void
		{
			tmpStrList.removeItemAt(gettmpVoIndex(wsObj.ID));
		}
		public function deleteAllTmpStr():void
		{
			tmpStrList.removeAll();
		}
		public function updateStrFromTmp():void
		{
			CairngormEventDispatcher.getInstance().dispatchEvent(new IFSEvent(IFSControl.CODIESWS_deleteTmpStr));
			Helper.showAlert("设定成功！");
		}
		public function updateAllStrFromTmp():void
		{
			CairngormEventDispatcher.getInstance().dispatchEvent(new IFSEvent(IFSControl.CODIESWS_deleteAllTmpStr));
			Helper.showAlert("设定成功！");
		}
		public function sameCaseBzAna():void
		{
			Helper.showAlert("比对结束，请刷新数据以查看结果。");
		}
		public function importStr():void
		{
			Helper.showAlert("导入完成！请刷新数据以查看结果。");
			orc_imp=false;
		}
		public function updateStr():void
		{
			var index:int=getVoIndex(wsObj.ID);
			allStrList.removeItemAt(index);
			allStrList.addItemAt(wsObj,index);
		}
		private function getVoIndex(id:String):int
		{
			for(var i:int=0;i<allStrList.length;i++)
			{
				var voObj:StrVo=allStrList.getItemAt(i) as StrVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
		private function gettmpVoIndex(id:String):int
		{
			for(var i:int=0;i<tmpStrList.length;i++)
			{
				var voObj:StrVo=tmpStrList.getItemAt(i) as StrVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}