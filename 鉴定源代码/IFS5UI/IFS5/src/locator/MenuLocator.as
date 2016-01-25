package locator
{
	import flash.events.Event;
	import flash.events.MouseEvent;
	
	import mx.collections.ArrayList;
	import mx.controls.LinkButton;
	
	import spark.components.HGroup;
	import spark.components.Label;
	
	import util.Helper;
	
	import vo.MenuVo;

	public class MenuLocator
	{
		//Singleton
		private static var locObj:MenuLocator;
		public static function getInstance():MenuLocator
		{
			if(locObj==null)
			{
				locObj=new MenuLocator();
			}
			return locObj;
		}
		//For the view
		public var menuList:ArrayList=new ArrayList();
		//ex call
		public function push(voObj:MenuVo):void
		{
			menuList.addItem(voObj);
			Helper.setIndexContentNoMenu(voObj.URL);
		}
		public function pop():void
		{
			menuList.removeItemAt(menuList.length-1);
			Helper.setIndexContentNoMenu(menuList.getItemAt(menuList.length-1).URL);
		}
		public function jump(label:String):void
		{
			for(var i:int=menuList.length-1;i>=0;i--)
			{
				if(menuList.getItemAt(i).LABEL!=label)
				{
					menuList.removeItemAt(i);
				}
				else
				{
					Helper.setIndexContentNoMenu(menuList.getItemAt(i).URL);
					break;
				}
			}
		}
		public function initMenu(label:String):void
		{						
			var voObj:MenuVo;
			var url:String="";
			switch(label)
			{
				case "Consign":label="委托用户";clearMenu(0);url="view/usermain/ConsignIndexModule.swf";break;
				case "Office":label="专业用户";clearMenu(0);url="view/usermain/OfficeIndexModule.swf";break;
				case "Insider":label="行政内勤";clearMenu(0);url="view/usermain/InsiderIndexModule.swf";break;
				case "Techer":label="技管用户";clearMenu(0);url="view/usermain/TecherIndexModule.swf";break;
				case "Leader":label="中心领导";clearMenu(0);url="view/usermain/LeaderIndexModule.swf";break;
				case "DNA":label="DNA用户";clearMenu(0);url="view/usermain/DnaIndexModule.swf";break;
				case "XTSZ":label="系统设置";clearMenu(1);url="view/usermain/xtsz/XtszModule.swf";break;
				case "consign_jdlc":label="鉴定流程";clearMenu(1);url="view/usermain/consign/JdlcModule.swf";break;
				case "consign_jdda":label="鉴定档案";clearMenu(1);url="view/usermain/consign/JddaModule.swf";break;
				case "office_jdlc":label="鉴定流程";clearMenu(1);url="view/usermain/office/JdlcModule.swf";break;
				case "office_jdda":label="鉴定档案";clearMenu(1);url="view/usermain/office/JddaModule.swf";break;
				case "insider_jdlc":label="鉴定流程";clearMenu(1);url="view/usermain/insider/JdlcModule.swf";break;
				case "insider_jdda":label="鉴定档案";clearMenu(1);url="view/usermain/insider/JddaModule.swf";break;
				case "techer_jdda":label="鉴定档案";clearMenu(1);url="view/usermain/techer/JddaModule.swf";break;
				case "techer_jdlc":label="鉴定流程";clearMenu(1);url="view/usermain/techer/JdlcModule.swf";break;
				case "leader_jdlc":label="鉴定流程";clearMenu(1);url="view/usermain/leader/JdlcModule.swf";break;
				case "techer_ryda":label="人员档案";clearMenu(1);url="view/usermain/ryda/RydaModule.swf";break;
				case "SJDA":label="试剂档案";clearMenu(1);url="view/MATERIAL/MaterialList.swf";break;
				case "SBDA":label="设备档案";clearMenu(1);url="view/Machine/Machine.swf";break;
				case "dna_jdda":label="鉴定档案";clearMenu(1);url="view/usermain/dna/JddaModule.swf";break;
				case "dna_jdlc":label="鉴定流程";clearMenu(1);url="view/usermain/dna/JdlcModule.swf";break;
				case "dna_ybjc":label="样本检测";clearMenu(1);url="view/usermain/dna/YbjcModule.swf";break;
				case "RZRK":label="认证认可";clearMenu(1);url="view/usermain/sysgl/sysglModule.swf";break;
				case "gztx":label="工作提醒";clearMenu(1);url="view/sysset/UserMainModule.swf";break;	
				case "dlq":label="待领取案件";clearMenu(1);url="view/idcase/query/query/QueryIdCaseConsignWaitModule.swf";break;	
				case "dsl":label="待受理案件";clearMenu(1);url="view/idcase/query/task/AcceptTask.swf";break;
				case "djy":label="待检验案件";clearMenu(1);url="view/idcase/query/task/TestTaskModule.swf";break;
				case "dshp":label="待审批案件";clearMenu(1);url="view/idcase/query/task/AuditTaskModule.swf";break;
				case "dfw":label="待发文案件";clearMenu(1);url="view/idcase/query/task/TesterSdTaskModule.swf";break;
				case "tjbb":label="统计报表";clearMenu(1);url="view/report/reportView.swf";break;
				default:return;
			}
			voObj=new MenuVo(label,url);
			menuList.addItem(voObj);			
			Helper.setIndexContentNoMenu(url);
		}
		public function changeMenu(menuGp:HGroup):void
		{			
			menuGp.removeAllElements();
			
			for(var i:int=0;i<menuList.length;i++)
			{
				var lk:LinkButton=new LinkButton();
				lk.setStyle("fontSize","18");
				lk.setStyle("fontWeight","bold");
				lk.label=menuList.getItemAt(i).LABEL;
				if(i<menuList.length-1)
				{
					lk.setStyle("textDecoration","underline");
					lk.addEventListener(MouseEvent.CLICK,clickEvt);
				}
				menuGp.addElement(lk);
				
				if(i<menuList.length-1)
				{
					lk=new LinkButton();
					lk.label="→";
					menuGp.addElement(lk);
				}
			}
		}
		private function clearMenu(index:int):void
		{
			if(index==0)menuList.removeAll();
			else
			{
				for(var i:int=menuList.length-1;i>0;i--)
				{
					menuList.removeItemAt(i);
				}
			}
		}
		private function clickEvt(evt:Event):void
		{
			jump((evt.target as LinkButton).label);
		}
	}
}