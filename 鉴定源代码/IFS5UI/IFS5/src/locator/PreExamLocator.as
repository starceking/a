package locator
{
	import mx.collections.ArrayList;
	
	import util.Helper;
	
	import vo.StrVo;
	import vo.PreExamVo;
	
	public class PreExamLocator
	{
		//Singleton
		private static var locObj:PreExamLocator;
		public static function getInstance():PreExamLocator
		{
			if(locObj==null)
			{
				locObj=new PreExamLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var PreExamListObj:ArrayList=new ArrayList();
		public var PreExamListPager:ListPager;
		//for the ws
		public var StrwsObj:StrVo;
		public var wsObj:PreExamVo;
		public var slsjs:String="";//受理时间s
		public var slsje:String="";//受理时间e
		public var yblx:String="";//样本类型
		public var yjr:String="";//鉴定人
		public var jystatus:String="";//检验状态
		public var preexam:String="";//预试验标记
		public var PreExamArray:Array = new Array();
		//Ws call
		public function insertPreExam():void
		{
			Helper.showAlert("保存成功！");
		}
		public function updateCasePre():void
		{
			var index:int=getPreExamVoIndex(wsObj.ID);
			PreExamListObj.removeItemAt(index);
			PreExamListObj.addItemAt(wsObj,index);
		}		
		public function deleteCasePre():void
		{
			PreExamListObj.removeItemAt(getPreExamVoIndex(wsObj.ID));
		}
		public function queryCasePre(xml:XML):void
		{
			PreExamListObj.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:PreExamVo=new PreExamVo(xml.children()[i].ID,xml.children()[i].案件ID,xml.children()[i].委托编号,xml.children()[i].库类型,
					xml.children()[i].样本编号,xml.children()[i].样本名称,xml.children()[i].样本类型,
					xml.children()[i].试验方法,xml.children()[i].试验人,xml.children()[i].日期,xml.children()[i].试验结果);
				PreExamListObj.addItem(voObj);
			}
			if(xml.children().length()>0)
			{
				PreExamListPager.RowCount=xml.children()[0].RowCount;
			}
			else
			{
				PreExamListPager.RowCount="0";
			}
		}
		public function queryPreExam(xml:XML):void
		{
			PreExamListObj.removeAll();			
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
				PreExamListObj.addItem(voObj);
			}
			if(xml.children().length()>0)
			{
				PreExamListPager.RowCount=xml.children()[0].RowCount;
			}
			else
			{
				PreExamListPager.RowCount="0";
			}
		}
		//Inner call
		private function getPreExamVoIndex(id:String):int
		{
			for(var i:int=0;i<PreExamListObj.length;i++)
			{
				var voObj:PreExamVo=PreExamListObj.getItemAt(i) as PreExamVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}