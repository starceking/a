package locator
{	
	import mx.collections.ArrayList;
	
	import util.Helper;
	
	import vo.StrVo;
	import vo.PreExamVo;
	import vo.ExtractSampleVo;
	import vo.ExtractVo;
	
	public class ExtractLocator 
	{
		//Singleton
		private static var locObj:ExtractLocator;
		public static function getInstance():ExtractLocator
		{
			if(locObj==null)
			{
				locObj=new ExtractLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var ExtractListObj:ArrayList=new ArrayList();
		public var ExtractListPager:ListPager;
		[Bindable]
		public var PureListObj:ArrayList=new ArrayList();
		public var PureListPager:ListPager;
		[Bindable]
		public var ExtractCaseListObj:ArrayList=new ArrayList();
		public var ExtractCaseListPager:ListPager;
		//for the ws
		[Bindable]
		public var StrwsObj:StrVo;
		public var wsObj:PreExamVo;
		public var curObj:ExtractVo;		
		public var slsjs:String="";//受理时间s
		public var slsje:String="";//受理时间e
		public var yblx:String="";//样本类型
		public var yjr:String="";//鉴定人
		public var jystatus:String="";//检验状态
		public var TQID:String="";//提取记录ID
		public var CaseID:String="";//案件ID
		public var ConNo:String="";//委托编号
		public var ExtractArray:Array = new Array();
		public var ExtractRecordArray:Array = new Array();
		public var PureArray:Array = new Array();
		public var PureRecordArray:Array = new Array();
		public var noTestArray:Array = new Array();
		[Bindable]
		public var ExtractInsertFlag:Boolean=false;
		
		//Ws call	
		public function insertExtract():void
		{
			ExtractInsertFlag=true;
			Helper.showAlert("保存成功！");
		}
		public function updateExtract():void
		{
			Helper.showAlert("保存成功！");
		}	
		public function insertPure():void
		{
			Helper.showAlert("保存成功！");
		}
		public function updatePure():void
		{
			Helper.showAlert("保存成功！");
		}
		public function deleteExtractRecord():void
		{		
			AmplifyLocator.getInstance().AmplifyListObj.removeItem(curObj);
			Helper.showAlert("删除成功！");
		}
		public function noTest():void
		{
			Helper.showAlert("设置成功！");
		}
		public function queryExtract(xml:XML):void
		{
			ExtractListObj.removeAll();			
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
				ExtractListObj.addItem(voObj);
			}
			if(xml.children().length()>0)
			{
				ExtractListPager.RowCount=xml.children()[0].RowCount;
			}
			else
			{
				ExtractListPager.RowCount="0";
			}
		}
		public function queryPure(xml:XML):void
		{
			PureListObj.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:ExtractSampleVo=new ExtractSampleVo(xml.children()[i].ID,xml.children()[i].案件ID,xml.children()[i].委托编号,
					xml.children()[i].库类型,xml.children()[i].样本编号,xml.children()[i].样本名称,xml.children()[i].样本类型,
					xml.children()[i].提取方法,xml.children()[i].纯化方法,xml.children()[i].模板用量,xml.children()[i].提取记录ID,
					xml.children()[i].纯化记录ID,xml.children()[i].电泳位置,xml.children()[i].提取人,xml.children()[i].日期,
					xml.children()[i].扩增时间结束,xml.children()[i].检测时间结束,xml.children()[i].SampleSheet,xml.children()[i].RunFold,
					xml.children()[i].扩增体系,xml.children()[i].纯化离心机,xml.children()[i].纯化移液器,xml.children()[i].纯化加热仪,
					xml.children()[i].纯化恒温混匀仪,xml.children()[i].纯化漩涡混合器,xml.children()[i].纯化水浴,xml.children()[i].纯化显微镜,
					xml.children()[i].纯化工作站,xml.children()[i].工作站模式ID,xml.children()[i].工作站模式,xml.children()[i].提取确认,
					xml.children()[i].扩增电泳ID,xml.children()[i].提取批号,xml.children()[i].扩增电泳批号,xml.children()[i].提取用量,xml.children()[i].纯化用量);
				PureListObj.addItem(voObj);
			}
		}
		public function queryCaseExtract(xml:XML):void
		{
			ExtractCaseListObj.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:ExtractSampleVo=new ExtractSampleVo(xml.children()[i].ID,xml.children()[i].案件ID,xml.children()[i].委托编号,
					xml.children()[i].库类型,xml.children()[i].样本编号,xml.children()[i].样本名称,xml.children()[i].样本类型,
					xml.children()[i].提取方法,xml.children()[i].纯化方法,xml.children()[i].模板用量,xml.children()[i].提取记录ID,
					xml.children()[i].纯化记录ID,xml.children()[i].电泳位置,xml.children()[i].提取人,xml.children()[i].日期,
					xml.children()[i].扩增时间结束,xml.children()[i].检测时间结束,xml.children()[i].SampleSheet,xml.children()[i].RunFold,
					xml.children()[i].扩增体系,xml.children()[i].纯化离心机,xml.children()[i].纯化移液器,xml.children()[i].纯化加热仪,
					xml.children()[i].纯化恒温混匀仪,xml.children()[i].纯化漩涡混合器,xml.children()[i].纯化水浴,xml.children()[i].纯化显微镜,
					xml.children()[i].纯化工作站,xml.children()[i].工作站模式ID,xml.children()[i].工作站模式,xml.children()[i].提取确认,
					xml.children()[i].扩增电泳ID,xml.children()[i].提取批号,xml.children()[i].扩增电泳批号,xml.children()[i].提取用量,xml.children()[i].纯化用量);
				ExtractCaseListObj.addItem(voObj);
			}
			if(xml.children().length()>0)
			{
				ExtractCaseListPager.RowCount=xml.children()[0].RowCount;
			}
			else
			{
				ExtractCaseListPager.RowCount="0";
			}
		}
	}
}