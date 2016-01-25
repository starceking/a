package locator
{
	import mx.collections.ArrayList;
	
	import util.Helper;
	
	import vo.ExtractSampleVo;
	import vo.ExtractVo;
	
	public class AmplifyLocator
	{
		//Singleton
		private static var locObj:AmplifyLocator;
		public static function getInstance():AmplifyLocator
		{
			if(locObj==null)
			{
				locObj=new AmplifyLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var AmplifyListObj:ArrayList=new ArrayList();
		public var AmplifyListPager:ListPager;
		[Bindable]
		public var SampleAmplifyListObj:ArrayList=new ArrayList();
		[Bindable]
		public var AmplifyCaseListObj:ArrayList=new ArrayList();
		public var AmplifyCaseListPager:ListPager;
		//For the Ws
		public var wsObj:ExtractVo;
		public var curObj:ExtractVo;
		public var rqs:String="";//日期s
		public var rqe:String="";//日期e
		public var gzzms:String="";//工作站模式标记
		public var jcbh:String="";//检材编号
		public var TQID:String="";//提取记录ID
		public var KZID:String="";//扩增记录ID
		public var CaseID:String="";//案件ID
		public var ConNo:String="";//委托编号
		public var AmplifyArray:Array = new Array();
		public var AmplifyRecordArray:Array = new Array();
		
		[Bindable]
		public var AmplifyInsertFlag:Boolean=false;
		
		//Ws call
		public function queryExtractRecord(xml:XML):void
		{
			AmplifyListObj.removeAll();	
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
					xml.children()[i].电泳环境湿度,xml.children()[i].鉴定单位,xml.children()[i].提取批号,xml.children()[i].扩增电泳批号,xml.children()[i].质控样本位置,xml.children()[i].提取确认,xml.children()[i].扩增电泳确认,
					xml.children()[i].对应提取记录ID,xml.children()[i].对应扩增记录ID);		
				AmplifyListObj.addItem(voObj);
			}
			if(xml.children().length()>0)
			{
				AmplifyListPager.RowCount=xml.children()[0].RowCount;
			}
			else
			{
				AmplifyListPager.RowCount="0";
			}
		}
		public function getSampleAmplify(xml:XML):void
		{
			var zkyb_flag:Boolean=true;
			SampleAmplifyListObj.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{
//				if(xml.children()[i].提取用量!="1"&&zkyb_flag)
//				{
//					//阳性
//					SampleAmplifyListObj.addItem(new ExtractSampleVo("","",
//						"","","","阳性质控样本","阳性","","","","",
//						"","","","","","","","","","","","","","","","","","","","","","","","1",""));	
//					//阴性
//					SampleAmplifyListObj.addItem(new ExtractSampleVo("","",
//						"","","","阴性质控样本","阴性","","","","",
//						"","","","","","","","","","","","","","","","","","","","","","","","1",""));	
//					zkyb_flag=false;
//				}			
				var voObj:ExtractSampleVo=new ExtractSampleVo(xml.children()[i].ID,xml.children()[i].案件ID,xml.children()[i].委托编号,
					xml.children()[i].库类型,xml.children()[i].样本编号,xml.children()[i].样本名称,xml.children()[i].样本类型,
					xml.children()[i].提取方法,xml.children()[i].纯化方法,xml.children()[i].模板用量,xml.children()[i].提取记录ID,
					xml.children()[i].纯化记录ID,xml.children()[i].电泳位置,xml.children()[i].提取人,xml.children()[i].日期,
					xml.children()[i].扩增时间结束,xml.children()[i].检测时间结束,xml.children()[i].SampleSheet,xml.children()[i].RunFold,
					xml.children()[i].扩增体系,xml.children()[i].纯化离心机,xml.children()[i].纯化移液器,xml.children()[i].纯化加热仪,
					xml.children()[i].纯化恒温混匀仪,xml.children()[i].纯化漩涡混合器,xml.children()[i].纯化水浴,xml.children()[i].纯化显微镜,
					xml.children()[i].纯化工作站,xml.children()[i].工作站模式ID,xml.children()[i].工作站模式,xml.children()[i].提取确认,
					xml.children()[i].扩增电泳ID,xml.children()[i].提取批号,xml.children()[i].扩增电泳批号,xml.children()[i].提取用量,xml.children()[i].纯化用量);
				SampleAmplifyListObj.addItem(voObj);	
			}
//			if(zkyb_flag)
//			{
//				//阳性
//				SampleAmplifyListObj.addItem(new ExtractSampleVo("","",
//					"","","","阳性质控样本","阳性","","","","",
//					"","","","","","","","","","","","","","","","","","","","","","","","1",""));	
//				//阴性
//				SampleAmplifyListObj.addItem(new ExtractSampleVo("","",
//					"","","","阴性质控样本","阴性","","","","",
//					"","","","","","","","","","","","","","","","","","","","","","","","1",""));	
//				zkyb_flag=false;
//			}
		}
		public function joinAmplify(xml:XML):void
		{
			if(xml.children().length()=="0")
			{
				Helper.showAlert("没有找到该检材，请核对检材编号是否输入正确！");
			}
			else
			{
				//扩增样本 插入  
				AmplifyLocator.getInstance().SampleAmplifyListObj.addItem(new ExtractSampleVo(xml.children()[0].ID,xml.children()[0].案件ID,xml.children()[0].委托编号,
					xml.children()[0].库类型,xml.children()[0].样本编号,xml.children()[0].名称,xml.children()[0].样本类型,
					xml.children()[0].提取方法,xml.children()[0].纯化方法,xml.children()[0].模板用量,xml.children()[0].提取记录ID,xml.children()[0].纯化记录ID,xml.children()[0].电泳位置,
					xml.children()[0].提取人,xml.children()[0].日期,xml.children()[0].扩增时间结束,xml.children()[0].检测时间结束,
					xml.children()[0].SampleSheet,xml.children()[0].RunFold,xml.children()[0].扩增体系,
					xml.children()[0].纯化离心机,xml.children()[0].纯化移液器,xml.children()[0].纯化加热仪,xml.children()[0].纯化恒温混匀仪,
					xml.children()[0].纯化漩涡混合器,xml.children()[0].纯化水浴,xml.children()[0].纯化显微镜,xml.children()[0].纯化工作站,
					xml.children()[0].工作站模式ID,xml.children()[0].工作站模式,xml.children()[0].提取确认,xml.children()[0].扩增电泳ID,
					xml.children()[0].提取批号,xml.children()[0].扩增电泳批号,xml.children()[0].提取用量,xml.children()[0].纯化用量));	
				
				//扩增样本 修改
				ElectrophoresisLocator.getInstance().SampleEPListObj.addItem(new ExtractSampleVo(xml.children()[0].ID,xml.children()[0].案件ID,xml.children()[0].委托编号,
					xml.children()[0].库类型,xml.children()[0].样本编号,xml.children()[0].名称,xml.children()[0].样本类型,
					xml.children()[0].提取方法,xml.children()[0].纯化方法,xml.children()[0].模板用量,xml.children()[0].提取记录ID,xml.children()[0].纯化记录ID,xml.children()[0].电泳位置,
					xml.children()[0].提取人,xml.children()[0].日期,xml.children()[0].扩增时间结束,xml.children()[0].检测时间结束,
					xml.children()[0].SampleSheet,xml.children()[0].RunFold,xml.children()[0].扩增体系,
					xml.children()[0].纯化离心机,xml.children()[0].纯化移液器,xml.children()[0].纯化加热仪,xml.children()[0].纯化恒温混匀仪,
					xml.children()[0].纯化漩涡混合器,xml.children()[0].纯化水浴,xml.children()[0].纯化显微镜,xml.children()[0].纯化工作站,
					xml.children()[0].工作站模式ID,xml.children()[0].工作站模式,xml.children()[0].提取确认,xml.children()[0].扩增电泳ID,
					xml.children()[0].提取批号,xml.children()[0].扩增电泳批号,xml.children()[0].提取用量,xml.children()[0].纯化用量));	
				//电泳样本 修改
				ElectrophoresisLocator.getInstance().SampleEPRecordListObj.addItem(new ExtractSampleVo(xml.children()[0].ID,xml.children()[0].案件ID,xml.children()[0].委托编号,
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
		public function insertAmplify():void
		{
			AmplifyInsertFlag=true;
			Helper.showAlert("保存成功！");
		}
		public function updateAmplify():void
		{
			Helper.showAlert("保存成功！");
		}
		public function deleteAmplifyRecord():void
		{
			ElectrophoresisLocator.getInstance().EPListObj.removeItem(curObj);
			Helper.showAlert("删除成功！");
		}
		public function queryCaseAmplify(xml:XML):void
		{
			AmplifyCaseListObj.removeAll();			
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
					xml.children()[i].扩增电泳ID,xml.children()[i].提取批号,xml.children()[i].扩增批号,xml.children()[i].提取用量,xml.children()[i].纯化用量);
				AmplifyCaseListObj.addItem(voObj);	
			}
			if(xml.children().length()>0)
			{
				AmplifyCaseListPager.RowCount=xml.children()[0].RowCount;
			}
			else
			{
				AmplifyCaseListPager.RowCount="0";
			}
		}
		//Inner call
		
	}
}