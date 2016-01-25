package locator
{
	import com.adobe.cairngorm.control.CairngormEventDispatcher;
	import com.adobe.cairngorm.model.ModelLocator;
	
	import control.*;
	
	import mx.collections.ArrayList;
	
	import spark.components.Label;
	
	import util.Helper;
	
	import vo.IdFlowVo;
	
	public class IdCaseLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:IdCaseLocator;
		public static function getInstance():IdCaseLocator
		{
			if(locObj==null)
			{
				locObj=new IdCaseLocator();
			}
			return locObj;
		}
		//For the Ws
		[Bindable]
		public var bsMainObj:IdFlowVo;
		public var bsWsObj:IdFlowVo;
		public var bsNoLbl:Label;
		public var year:String="";
		public var starTime:String="";
		public var endTime:String="";
		//for the view
		[Bindable]
		public var yearStaList:ArrayList=new ArrayList();
		[Bindable]
		public var JdsList:ArrayList=new ArrayList();
		//Ws call	
		public function impToOra():void
		{
			IdFlowLocator.getInstance().curObj.ORA_FLAG="1";
			//Helper.showAlert("受理成功，请打印鉴定事项确认书");
			Helper.showAlert("案件及检材的基本信息已导入，请再点击“导入STR”");
			CodiesLocator.getInstance().orc_imp=true;
		}
		public function updateBsInfo():void
		{
			bsMainObj=bsWsObj;
			if(IdFlowLocator.getInstance().curObj.ID==bsMainObj.ID)
			{
				bsNoLbl.text="该案件为非补送";
				IdFlowLocator.getInstance().curObj.SRCID=IdFlowLocator.getInstance().curObj.ID;
				IdFlowLocator.getInstance().curObj.CASE_NO="受理后自动生成";
			}
			else
			{
				bsNoLbl.text=bsMainObj.CASE_NO;
				IdFlowLocator.getInstance().curObj.SRCID=bsMainObj.ID;
				IdFlowLocator.getInstance().curObj.CASE_NO=bsMainObj.CASE_NO;
			}
			Helper.showAlert("设置成功！");
		}
		public function getOneRecord(xml:XML):void
		{
			if(xml.children().length()>0)
			{
				bsMainObj=new IdFlowVo(xml.children()[0].委托编号,
					xml.children()[0].鉴定单位,xml.children()[0].委托表号,xml.children()[0].鉴定状态,xml.children()[0].委托单位,xml.children()[0].送检人一,
					xml.children()[0].一送姓名,xml.children()[0].一送警号,xml.children()[0].一送电话,
					xml.children()[0].二送姓名,xml.children()[0].二送警号,xml.children()[0].二送电话,xml.children()[0].委托年份,
					xml.children()[0].委托序号,xml.children()[0].委托时间,xml.children()[0].鉴定专业,xml.children()[0].鉴定类别,xml.children()[0].鉴定项目,
					xml.children()[0].鉴定要求,xml.children()[0].文书名称,xml.children()[0].受理年份,xml.children()[0].受理序号,xml.children()[0].案件序号,xml.children()[0].发文年份,
					xml.children()[0].发文序号,xml.children()[0].受理人员,xml.children()[0].受理时间,xml.children()[0].计划完成,xml.children()[0].认证认可,
					xml.children()[0].受理意见,xml.children()[0].鉴定结论,xml.children()[0].结论概述,xml.children()[0].一检人,xml.children()[0].一检完成,
					xml.children()[0].二检人,xml.children()[0].二检完成,xml.children()[0].三检人,xml.children()[0].三检完成,xml.children()[0].四检人,
					xml.children()[0].四检完成,xml.children()[0].复核人,xml.children()[0].复核完成,xml.children()[0].授权签字,xml.children()[0].签字完成,
					xml.children()[0].技管,xml.children()[0].技管完成,xml.children()[0].领导,xml.children()[0].审批完成,xml.children()[0].发文确认,
					xml.children()[0].一检留言,xml.children()[0].鉴定记事,xml.children()[0].文书领取,xml.children()[0].领取完成,xml.children()[0].领取人一,
					xml.children()[0].领一电话,xml.children()[0].领取人二,xml.children()[0].领二电话,					
					xml.children()[0].鉴定单位名称,xml.children()[0].委托单位名称,xml.children()[0].委托单位简称,xml.children()[0].委托单位编号,xml.children()[0].送检人一姓名,xml.children()[0].送检人一长号,xml.children()[0].送检人一短号,
					xml.children()[0].受理人员姓名,xml.children()[0].一检姓名,xml.children()[0].二检姓名,
					xml.children()[0].三检姓名,xml.children()[0].四检姓名,xml.children()[0].复核姓名,xml.children()[0].签字姓名,
					xml.children()[0].技管姓名,xml.children()[0].领导姓名,					
					xml.children()[0].ID,xml.children()[0].打防管控,
					xml.children()[0].现场勘验,xml.children()[0].案件名称,xml.children()[0].案件类型,xml.children()[0].案件类别,xml.children()[0].案件编号,
					xml.children()[0].发案地点,xml.children()[0].区划代码,xml.children()[0].发案时间,xml.children()[0].案件性质,xml.children()[0].简要案情,xml.children()[0].SRCID,
					xml.children()[0].物证处置,xml.children()[0].领物人,xml.children()[0].领物时间,xml.children()[0].物证处置备注,
					xml.children()[0].原鉴定情况,xml.children()[0].ORA_FLAG,xml.children()[0].被鉴定人,xml.children()[0].名称,xml.children()[0].鉴定方法);
				if(bsNoLbl!=null)bsNoLbl.text=bsMainObj.CASE_NO;
			}
			else
			{
				Helper.showAlert("找不到补送的主案件");
			}
		}
		public function getBsMain(bsNoLbl:Label):void
		{
			var fwObj:IdFlowVo=IdFlowLocator.getInstance().curObj;
			if(fwObj.SRCID!=fwObj.ID)
			{
				this.bsNoLbl=bsNoLbl;
				bsWsObj=fwObj;
				CairngormEventDispatcher.getInstance().dispatchEvent(new IFSEvent(IFSControl.IDCASEWS_GetOneRecord));
			}
			else
			{
				locObj.bsMainObj=fwObj;
			}
		}
		public function getYearStaData(xml:XML):void
		{
			yearStaList.removeAll();
			for(var i:int=0;i<xml.children().length();i++)
			{				
				yearStaList.addItem({jdzy:xml.children()[i].jdzy,jdlb:xml.children()[i].jdlb,jan:xml.children()[i].jan,
					feb:xml.children()[i].feb,mar:xml.children()[i].mar,apr:xml.children()[i].apr,may:xml.children()[i].may,
					jun:xml.children()[i].jun,jul:xml.children()[i].jul,aug:xml.children()[i].aug,sep:xml.children()[i].sep,
					oct:xml.children()[i].oct,nov:xml.children()[i].nov,dec:xml.children()[i].dec});
			}
		}
		public function getAllJds(xml:XML):void
		{
			JdsList.removeAll();
			for(var i:int=0;i<xml.children().length();i++)
			{				
				JdsList.addItem({jdzy:xml.children()[i].jdzy,jdlb:xml.children()[i].jdlb,jds:xml.children()[i].jds});
			}
		}
	}
}