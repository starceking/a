package locator
{
	import mx.collections.ArrayList;
	
	import util.Helper;
	
	import vo.ExtractSampleVo;
	import vo.ExtractVo;
	
	public class ElectrophoresisLocator
	{
		//Singleton
		private static var locObj:ElectrophoresisLocator;
		public static function getInstance():ElectrophoresisLocator
		{
			if(locObj==null)
			{
				locObj=new ElectrophoresisLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var EPListObj:ArrayList=new ArrayList();
		public var EPListPager:ListPager;
		[Bindable]
		public var EPRecordListObj:ArrayList=new ArrayList();
		public var EPRecordListPager:ListPager;
		[Bindable]
		public var EPCaseListObj:ArrayList=new ArrayList();
		public var EPCaseListPager:ListPager;
		[Bindable]
		public var SampleEPListObj:ArrayList=new ArrayList();
		public var SampleEPRecordListObj:ArrayList=new ArrayList();
		//For the Ws
		public var wsObj:ExtractVo;
		public var curObj:ExtractVo;	
		public var rqs:String="";//日期s
		public var rqe:String="";//日期e
		public var kzqr:String="";//扩增确认
		public var dyqr:String="";//扩增确认
		public var jcbh:String="";//检材编号
		public var KZID:String="";//扩增记录ID
		public var DYID:String="";//电泳记录ID
		public var CaseID:String="";//案件ID
		public var ConNo:String="";//委托编号
		public var EPArray:Array = new Array();
		public var EPRecordArray:Array = new Array();	
		
		[Bindable]
		public var EPInsertFlag:Boolean=false;
		
		//Ws call
		public function queryAmplifyRecord(xml:XML):void
		{
			EPListObj.removeAll();	
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:ExtractVo=new ExtractVo(xml.children()[i].ID,xml.children()[i].离心机,xml.children()[i].移液器,xml.children()[i].加热仪,xml.children()[i].恒温混匀仪,xml.children()[i].漩涡混合器,xml.children()[i].水浴,
					xml.children()[i].显微镜,xml.children()[i].工作站,xml.children()[i].提取人,xml.children()[i].日期,xml.children()[i].提取方法,xml.children()[i].纯化方法,
					xml.children()[i].扩增仪,xml.children()[i].扩增离心机,xml.children()[i].扩增漩涡混合器,
					xml.children()[i].扩增移液器,xml.children()[i].扩增超净台,xml.children()[i].扩增工作站,xml.children()[i].扩增时间开始,
					xml.children()[i].扩增时间结束,xml.children()[i].扩增方法,xml.children()[i].试剂盒批号,xml.children()[i].扩增体系,xml.children()[i].扩增模板,
					xml.children()[i].循环数,xml.children()[i].环境温度,xml.children()[i].环境湿度,xml.children()[i].电泳仪,xml.children()[i].电泳离心机,xml.children()[i].电泳加热仪,
					xml.children()[i].电泳漩涡混合器,xml.children()[i].电泳移液器,xml.children()[i].电泳制冰机,xml.children()[i].电泳超净台,
					xml.children()[i].电泳工作站,xml.children()[i].内标,xml.children()[i].内标量,xml.children()[i].检测时间开始,xml.children()[i].检测时间结束,xml.children()[i].质控样本,
					xml.children()[i].变性溶剂,xml.children()[i].溶剂量,xml.children()[i].产物量,xml.children()[i].胶液,xml.children()[i].胶液批号,xml.children()[i].预电泳电流,
					xml.children()[i].电泳电流,xml.children()[i].SampleSheet,xml.children()[i].RunFold,xml.children()[i].电泳环境温度,
					xml.children()[i].电泳环境湿度,xml.children()[i].鉴定单位,xml.children()[i].提取批号,xml.children()[i].扩增批号,xml.children()[i].质控样本位置,xml.children()[i].提取确认,xml.children()[i].扩增确认,
					xml.children()[i].对应提取记录ID,xml.children()[i].对应扩增记录ID);		
				EPListObj.addItem(voObj);
			}
			if(xml.children().length()>0)
			{
				EPListPager.RowCount=xml.children()[0].RowCount;
			}
			else
			{
				EPListPager.RowCount="0";
			}
		}
		public function getSampleEP(xml:XML):void
		{
			var zkyb_flag:Boolean=true;
			SampleEPListObj.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{			
				var voObj:ExtractSampleVo=new ExtractSampleVo(xml.children()[i].ID,xml.children()[i].案件ID,xml.children()[i].委托编号,
					xml.children()[i].库类型,xml.children()[i].样本编号,xml.children()[i].样本名称,xml.children()[i].样本类型,
					xml.children()[i].提取方法,xml.children()[i].纯化方法,xml.children()[i].模板用量,xml.children()[i].提取记录ID,
					xml.children()[i].纯化记录ID,xml.children()[i].扩增位置,xml.children()[i].提取人,xml.children()[i].日期,
					xml.children()[i].扩增时间结束,xml.children()[i].检测时间结束,xml.children()[i].SampleSheet,xml.children()[i].RunFold,
					xml.children()[i].扩增体系,xml.children()[i].纯化离心机,xml.children()[i].纯化移液器,xml.children()[i].纯化加热仪,
					xml.children()[i].纯化恒温混匀仪,xml.children()[i].纯化漩涡混合器,xml.children()[i].纯化水浴,xml.children()[i].纯化显微镜,
					xml.children()[i].纯化工作站,xml.children()[i].工作站模式ID,xml.children()[i].工作站模式,xml.children()[i].提取确认,
					xml.children()[i].扩增电泳ID,xml.children()[i].提取批号,xml.children()[i].扩增电泳批号,xml.children()[i].提取用量,xml.children()[i].纯化用量);
				SampleEPListObj.addItem(voObj);	
			}	
		}
		public function joinEP(xml:XML):void
		{
			if(xml.children().length()=="0")
			{
				Helper.showAlert("没有找到该检材，请核对检材编号是否输入正确！");
			}
			else
			{
				ElectrophoresisLocator.getInstance().SampleEPListObj.addItem(new ExtractSampleVo(xml.children()[0].ID,xml.children()[0].案件ID,xml.children()[0].委托编号,
					xml.children()[0].库类型,xml.children()[0].样本编号,xml.children()[0].名称,xml.children()[0].样本类型,
					xml.children()[0].提取方法,xml.children()[0].纯化方法,xml.children()[0].模板用量,xml.children()[0].提取记录ID,xml.children()[0].纯化记录ID,xml.children()[0].电泳位置,
					xml.children()[0].提取人,xml.children()[0].日期,xml.children()[0].扩增时间结束,xml.children()[0].检测时间结束,
					xml.children()[0].SampleSheet,xml.children()[0].RunFold,xml.children()[0].扩增体系,
					xml.children()[0].纯化离心机,xml.children()[0].纯化移液器,xml.children()[0].纯化加热仪,xml.children()[0].纯化恒温混匀仪,
					xml.children()[0].纯化漩涡混合器,xml.children()[0].纯化水浴,xml.children()[0].纯化显微镜,xml.children()[0].纯化工作站,
					xml.children()[0].工作站模式ID,xml.children()[0].工作站模式,xml.children()[0].提取确认,xml.children()[0].扩增电泳ID,
					xml.children()[0].提取批号,xml.children()[0].扩增电泳批号,xml.children()[0].提取用量,xml.children()[0].纯化用量));	
			}
		}
		public function insertEP():void
		{
			EPInsertFlag=true;
			Helper.showAlert("保存成功！");
		}
		public function updateEP():void
		{
			Helper.showAlert("保存成功！");
		}
		public function deleteEPRecord():void
		{
			ElectrophoresisLocator.getInstance().EPRecordListObj.removeItem(curObj);
			Helper.showAlert("删除成功！");
		}
		public function queryEPRecord(xml:XML):void
		{
			EPRecordListObj.removeAll();	
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:ExtractVo=new ExtractVo(xml.children()[i].ID,xml.children()[i].离心机,xml.children()[i].移液器,xml.children()[i].加热仪,xml.children()[i].恒温混匀仪,xml.children()[i].漩涡混合器,xml.children()[i].水浴,
					xml.children()[i].显微镜,xml.children()[i].工作站,xml.children()[i].提取人,xml.children()[i].日期,xml.children()[i].提取方法,xml.children()[i].纯化方法,
					xml.children()[i].扩增仪,xml.children()[i].扩增离心机,xml.children()[i].扩增漩涡混合器,
					xml.children()[i].扩增移液器,xml.children()[i].扩增超净台,xml.children()[i].扩增工作站,xml.children()[i].扩增时间开始,
					xml.children()[i].扩增时间结束,xml.children()[i].扩增方法,xml.children()[i].试剂盒批号,xml.children()[i].扩增体系,xml.children()[i].扩增模板,
					xml.children()[i].循环数,xml.children()[i].环境温度,xml.children()[i].环境湿度,xml.children()[i].电泳仪,xml.children()[i].电泳离心机,xml.children()[i].电泳加热仪,
					xml.children()[i].电泳漩涡混合器,xml.children()[i].电泳移液器,xml.children()[i].电泳制冰机,xml.children()[i].电泳超净台,
					xml.children()[i].电泳工作站,xml.children()[i].内标,xml.children()[i].内标量,xml.children()[i].电泳时间开始,xml.children()[i].电泳时间结束,xml.children()[i].质控样本,
					xml.children()[i].变性溶剂,xml.children()[i].溶剂量,xml.children()[i].产物量,xml.children()[i].胶液,xml.children()[i].胶液批号,xml.children()[i].预电泳电流,
					xml.children()[i].电泳电流,xml.children()[i].SampleSheet,xml.children()[i].RunFold,xml.children()[i].电泳环境温度,
					xml.children()[i].电泳环境湿度,xml.children()[i].鉴定单位,xml.children()[i].提取批号,xml.children()[i].电泳批号,xml.children()[i].质控样本位置,xml.children()[i].提取确认,xml.children()[i].电泳确认,
					xml.children()[i].对应提取记录ID,xml.children()[i].对应扩增记录ID);		
				EPRecordListObj.addItem(voObj);
			}
			if(xml.children().length()>0)
			{
				EPRecordListPager.RowCount=xml.children()[0].RowCount;
			}
			else
			{
				EPRecordListPager.RowCount="0";
			}
		}
		public function getSampleEPRecord(xml:XML):void
		{
			SampleEPRecordListObj.removeAll();			
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
				SampleEPRecordListObj.addItem(voObj);	
			}	
		}
		public function queryCaseEP(xml:XML):void
		{
			EPCaseListObj.removeAll();	
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
					xml.children()[i].电泳记录ID,xml.children()[i].提取批号,xml.children()[i].电泳批号,xml.children()[i].提取用量,xml.children()[i].纯化用量);
				SampleEPRecordListObj.addItem(voObj);	
				EPCaseListObj.addItem(voObj);
			}
			if(xml.children().length()>0)
			{
				EPCaseListPager.RowCount=xml.children()[0].RowCount;
			}
			else
			{
				EPCaseListPager.RowCount="0";
			}
		}
	}
}