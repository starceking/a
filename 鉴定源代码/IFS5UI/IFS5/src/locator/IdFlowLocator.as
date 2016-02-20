package locator
{
	import com.adobe.cairngorm.control.CairngormEventDispatcher;
	import com.adobe.cairngorm.model.ModelLocator;
	
	import control.*;
	
	import locator.MRelativeLocator;
	import locator.MissingPersonLocator;
	
	import mx.collections.ArrayList;
	
	import spark.components.Label;
	import spark.components.TextInput;
	
	import util.Helper;
	
	import vo.IdFlowVo;
	import vo.MRelativeVo;
	import vo.MissingPersonVo;
		
	public class IdFlowLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:IdFlowLocator;
		public static function getInstance():IdFlowLocator
		{
			if(locObj==null)
			{
				locObj=new IdFlowLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var auditMsg:String="任务检索中";
		[Bindable]
		public var listObj:ArrayList=new ArrayList();	
		public var listPager:ListPager;
		[Bindable]
		public var curObj:IdFlowVo;		
		//For the Ws
		public var wsObj:IdFlowVo;
		public var wholeCase:Boolean=false;
		//--案发地点、地点编号、委托单位、单位编号
		public var caseAFDD:TextInput;
		public var caseAFDDcode:TextInput;
		public var con_psbname:Label;
		public var con_psbcode:Label;
		//--获取委托序号、受理序号、发文序号
		public var notxt:TextInput;
		public var accCaseTxt:TextInput;
		//--获取样本编号
		public var mpnotxt:TextInput;
		public var mpr1notxt:TextInput;
		public var mpr2notxt:TextInput;
		public var senotxt:TextInput;
		public var cpsnotxt:TextInput;
		public var crnotxt:TextInput;
		public var udnotxt:TextInput;
		public var preFixSe:String;
		public var preFixCps:String;
		public var preFixCr:String;
		public var preFixUd:String;
		public var tableName:String;
		public var filename:String;
		//--流程中的操作
		public var isSubmit:String;		
		public var operTimeCol:String;
		public var oper:String;
		public var operReason:String="";
		//查询
		public var sOccDate:String="";
		public var eOccDate:String="";
		public var sConDate:String="";
		public var eConDate:String="";
		public var sAccDate:String="";
		public var eAccDate:String="";
		public var sGpDate:String="";
		public var eGpDate:String="";
		public var conPsbName:String="";
		//Ex call
		public function getOperTimeCol():String
		{
			if(curObj.TESTER2.length>0&&curObj.TESTER2F.length==0)
			{
				return "二检完成";
			}
			else if(curObj.TESTER3.length>0&&curObj.TESTER3F.length==0)
			{
				return "三检完成";
			}
			else if(curObj.TESTER4.length>0&&curObj.TESTER4F.length==0)
			{
				return "四检完成";
			}
			else if(curObj.CHECKER.length>0&&curObj.CHECKERF.length==0)
			{
				return "复核完成";
			}
			else if(curObj.SIGNER.length>0&&curObj.SIGNERF.length==0)
			{
				return "签字完成";
			}
			else if(curObj.TECHER.length>0&&curObj.TECHERF.length==0)
			{
				return "技管完成";
			}
			else if(curObj.LEADER.length>0&&curObj.LEADERF.length==0)
			{
				return "审批完成";
			}
			return "";
		}
		private function getNextAuditMsg():String
		{
			var pname:String="";
			var action:String="";
			var act:int=0;
			
			if(curObj.TESTERF.length==0)
			{
				act++;
			}
			if(curObj.TESTER2.length>0&&curObj.TESTER2F.length==0)
			{
				act++;
				if(act==2)
				{
					pname=curObj.TESTER2_NAME;action="进行下一步审核";
					if(JusTypeLocator.getInstance().jusTypeVo.TESTER2=="1")
					{
						var phone:String=SysUserLocator.getInstance().getUserVo(curObj.TESTER2).LONGPHONE;
						ExCaseLocator.getInstance().sendNote(phone,"收到新的审核任务："+curObj.ACC_NO_SHOW);
					}
					return "提交成功，请通知“"+pname+"”"+action+"。";
				}
			}
			if(curObj.TESTER3.length>0&&curObj.TESTER3F.length==0)
			{
				act++;
				if(act==2)
				{
					pname=curObj.TESTER3_NAME;action="进行下一步审核";
					if(JusTypeLocator.getInstance().jusTypeVo.TESTER3=="1")
					{
						var phone:String=SysUserLocator.getInstance().getUserVo(curObj.TESTER3).LONGPHONE;
						ExCaseLocator.getInstance().sendNote(phone,"收到新的审核任务："+curObj.ACC_NO_SHOW);
					}
					return "提交成功，请通知“"+pname+"”"+action+"。";
				}
			}
			if(curObj.TESTER4.length>0&&curObj.TESTER4F.length==0)
			{
				act++;
				if(act==2)
				{
					pname=curObj.TESTER4_NAME;action="进行下一步审核";
					if(JusTypeLocator.getInstance().jusTypeVo.TESTER4=="1")
					{
						var phone:String=SysUserLocator.getInstance().getUserVo(curObj.TESTER4).LONGPHONE;
						ExCaseLocator.getInstance().sendNote(phone,"收到新的审核任务："+curObj.ACC_NO_SHOW);
					}
					return "提交成功，请通知“"+pname+"”"+action+"。";
				}
			}
			if(curObj.CHECKER.length>0&&curObj.CHECKERF.length==0)
			{
				act++;
				if(act==2)
				{
					pname=curObj.CHECKER_NAME;action="复核";
					if(JusTypeLocator.getInstance().jusTypeVo.CHECKER=="1")
					{
						var phone:String=SysUserLocator.getInstance().getUserVo(curObj.CHECKER).LONGPHONE;
						ExCaseLocator.getInstance().sendNote(phone,"收到新的复核任务："+curObj.ACC_NO_SHOW);
					}
					return "提交成功，请通知“"+pname+"”"+action+"。";
				}
			}
			if(curObj.SIGNER.length>0&&curObj.SIGNERF.length==0)
			{
				act++;
				if(act==2)
				{
					pname=curObj.SIGNER_NAME;action="授权签字";
					if(JusTypeLocator.getInstance().jusTypeVo.SIGN=="1")
					{
						var phone:String=SysUserLocator.getInstance().getUserVo(curObj.SIGNER).LONGPHONE;
						ExCaseLocator.getInstance().sendNote(phone,"收到新的授权签字任务："+curObj.ACC_NO_SHOW);
					}
					return "提交成功，请通知“"+pname+"”"+action+"。";
				}
			}
			if(curObj.TECHER.length>0&&curObj.TECHERF.length==0)
			{
				act++;
				if(act==2)
				{
					pname=curObj.TECHER_NAME;action="进行下一步审核";
					if(JusTypeLocator.getInstance().jusTypeVo.TECH=="1")
					{
						var phone:String=SysUserLocator.getInstance().getUserVo(curObj.TECHER).LONGPHONE;
						ExCaseLocator.getInstance().sendNote(phone,"收到新的审核任务："+curObj.ACC_NO_SHOW);
					}
					return "提交成功，请通知“"+pname+"”"+action+"。";
				}
			}
			if(curObj.LEADER.length>0&&curObj.LEADERF.length==0)
			{
				act++;
				if(act==2)
				{
					pname=curObj.LEADER_NAME;action="审批签发";
					if(JusTypeLocator.getInstance().jusTypeVo.LEADER=="1")
					{
						var phone:String=SysUserLocator.getInstance().getUserVo(curObj.LEADER).LONGPHONE;
						ExCaseLocator.getInstance().sendNote(phone,"收到新的案件需审批："+curObj.ACC_NO_SHOW);
					}
					return "提交成功，请通知“"+pname+"”"+action+"。";
				}
			}
			if(curObj.TESTERSDF.length==0)
			{
				if(JusTypeLocator.getInstance().jusTypeVo.TESTERSD=="1")
				{
					var phone:String=SysUserLocator.getInstance().getUserVo(curObj.TESTER).LONGPHONE;
					ExCaseLocator.getInstance().sendNote(phone,"请开始制作该案件的文书正本："+curObj.ACC_NO_SHOW);
				}
				return "审批成功，请通知“"+curObj.TESTER_NAME+"”开始制作文书。";
			}
			return "提交成功，请通知委托方前来领取报告。";
		}
		//流程相关
		public function insert(cno:String):void//新建委托成功了
		{		
			var sta:String=curObj.ID_STATUS;
			
			curObj=wsObj;
			curObj.CONNO=cno;
			curObj.ID_STATUS="新的委托";		
			if(cno.search("R")==0)
			{
				MRelativeLocator.getInstance().curObj=MRelativeLocator.getInstance().wsObj;
				MRelativeLocator.getInstance().curObj.CONNO=cno;
			}
			else if(cno.search("L")==0)
			{
				MissingPersonLocator.getInstance().curObj=MissingPersonLocator.getInstance().wsObj;
				MissingPersonLocator.getInstance().curObj.CONNO=cno;
			}
			if(sta!="信息录入")
			{
				Helper.showAlert("保存成功");
			}
		}
		public function updateCase():void
		{
			curObj=wsObj;
			Helper.showAlert("保存成功");
		}
		public function cancelFunc():void
		{
			PsbLocator.getInstance().setMsg("成功取消对该案件的受理。");
		}
		public function deleteFunc():void
		{
			PsbLocator.getInstance().setMsg("成功删除该案件数据。");
		}
		public function getAcceptByBarCode(xml:XML):void
		{
			if(xml.children().length()>0)
			{
				curObj=new IdFlowVo(xml.children()[0].委托编号,
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
					xml.children()[0].原鉴定情况,"0",xml.children()[0].被鉴定人,xml.children()[0].名称,xml.children()[0].鉴定方法);
				goToAccept();
			}
			else
			{
				Helper.showAlert("找不到该案件！可能该案件是送检到其他专业，或者该案件已被受理。");
			}
		}
		public function goToAccept():void
		{
			if(SysUserLocator.getInstance().loginUser.CURRENT_ROLE=="DNA")
			{
				if(curObj.CONNO.search("C")==0)return;
			}
			else if(SysUserLocator.getInstance().loginUser.CURRENT_ROLE=="Office")
			{				
				var off:String=SysUserLocator.getInstance().loginUser.OFFICE;
				if(curObj.ID_OFFICE!=off)return;
			}
			
			curObj.ACC_YEAR=JusTypeLocator.getInstance().jusTypeVo.YEAR;
			curObj.ACC_TIME=Helper.getStrByDate(new Date());
			curObj.ACCER=SysUserLocator.getInstance().loginUser.ID;
			curObj.ACCEPTER_NAME=SysUserLocator.getInstance().loginUser.NAME;
			curObj.TESTER=SysUserLocator.getInstance().loginUser.ID;
			curObj.TESTER_NAME=SysUserLocator.getInstance().loginUser.NAME;
			
			if(curObj.CONNO.search("C")==0)
			{
				curObj.PLAN_DATE=Helper.getAddedDate(new Date(),
					JusTypeLocator.getInstance().getOffice(curObj.ID_OFFICE).PlanDate);
				if(curObj.ID_OFFICE=="法医")
				{
					Helper.pushMenu("受理查重","view/idcase/info/accept/idcase/AcceptDupShowModule.swf");
				}
				else
				{
					Helper.pushMenu("核对委托信息","view/idcase/info/accept/idcase/CheckConInfoModule.swf");
				}
			}
			else if(curObj.CONNO.search("D")==0)
			{
				curObj.PLAN_DATE=Helper.getAddedDate(new Date(),JusTypeLocator.getInstance().jusTypeVo.PlanDate);
				Helper.pushMenu("补送判定","view/idcase/info/accept/dna/DnaBsSetModule.swf");
			}
			else if(curObj.CONNO.search("R")==0)
			{
				curObj.PLAN_DATE=Helper.getAddedDate(new Date(),JusTypeLocator.getInstance().jusTypeVo.PlanDate);
				MRelativeLocator.getInstance().wsObj=new MRelativeVo(curObj.ID,"","","","","","","","","","","","","",
					"","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","0");
				CairngormEventDispatcher.getInstance().dispatchEvent(new IFSEvent(IFSControl.MRELATIVEWS_GetOneMpr));
				Helper.pushMenu("核对委托信息","view/idcase/info/accept/mpr/CheckConInfoModule.swf");
			}
			else if(curObj.CONNO.search("L")==0)
			{
				curObj.PLAN_DATE=Helper.getAddedDate(new Date(),JusTypeLocator.getInstance().jusTypeVo.PlanDate);
				MissingPersonLocator.getInstance().wsObj=new MissingPersonVo(curObj.ID,"","","","","","","","","",
					"","","","","","","","","","0");
				CairngormEventDispatcher.getInstance().dispatchEvent(new IFSEvent(IFSControl.MISSINGPERSONWS_GetOneMp));
				Helper.pushMenu("核对委托信息","view/idcase/info/accept/mp/CheckConInfoModule.swf");
			}
			PsbLocator.getInstance().clearData();
		}
		public function updateAccept():void
		{
			curObj=wsObj;			
			if(curObj.ID_STATUS=="新的委托")
			{
				curObj.ID_STATUS="检验中";
				if(curObj.CONNO.search("D")==0)
				{
					curObj.CASE_NO=Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.CLN,
						curObj.ACC_YEAR,curObj.ACC_CASE_NO,"");
					//curObj.ORA_FLAG="1";
					Helper.showAlert("受理成功，请打印鉴定事项确认书。");
				}
				else if(curObj.CONNO.search("R")==0)
				{
					MRelativeLocator.getInstance().curObj=MRelativeLocator.getInstance().wsObj;
					//MRelativeLocator.getInstance().curObj.ORA_FLAG="1";
					Helper.showAlert("受理成功，请打印鉴定事项确认书。");
				}
				else if(curObj.CONNO.search("L")==0)
				{
					MissingPersonLocator.getInstance().curObj=MissingPersonLocator.getInstance().wsObj;
					//MissingPersonLocator.getInstance().curObj.ORA_FLAG="1";
					Helper.showAlert("受理成功，请打印鉴定事项确认书。");
				}
				else if(curObj.CONNO.search("C")==0)
				{
					curObj.CASE_NO=curObj.ACC_YEAR+"-"+curObj.ACC_CASE_NO;	
					Helper.showAlert("受理成功，请打印鉴定事项确认书");
				}
				
				if(JusTypeLocator.getInstance().jusTypeVo.TESTER=="1")
				{
					var phone:String=SysUserLocator.getInstance().getUserVo(curObj.TESTER).LONGPHONE;
					ExCaseLocator.getInstance().sendNote(phone,"受理了新的案件："+curObj.ACC_NO_SHOW);
				}
			}
			else if(curObj.ID_STATUS=="不予受理")
			{
				Helper.showAlert("已拒绝受理该案件，请打印不受理委托鉴定告知书");
			}
			else
			{
				Helper.showAlert("保存成功");
			}
		}
		public function updateConclusion():void
		{
			Helper.showAlert("保存成功！");curObj=wsObj;
		}
		public function updateTesterFinish():void
		{
			switch(isSubmit)
			{
				case "0":Helper.showAlert("保存成功！请打印检验报告之后，再提交审核！");break;
				case "1":PsbLocator.getInstance().setMsg(getNextAuditMsg());break;
				case "2":PsbLocator.getInstance().setMsg("存档成功，以后若需出鉴定书可重新激活该案件！");break;
			}
		}
		public function updateAudit():void
		{
			switch(wsObj.ID_STATUS)
			{
				case "通过":
					auditMsg=getNextAuditMsg();
					MenuLocator.getInstance().pop();
					break;
				case "退回":
					auditMsg="退回成功，请通知“"+curObj.TESTER_NAME+"”进行修改。";
					MenuLocator.getInstance().pop();
					break;
				case "存档":PsbLocator.getInstance().setMsg("存档成功，以后若需出鉴定书可重新激活该案件！");break;
				case "激活":
					//PsbLocator.getInstance().setMsg("激活成功，请通知“"+wsObj.TESTER_NAME+"”重新进行文书制作与发送！");
					Helper.showAlert("激活成功，请通知“"+wsObj.TESTER_NAME+"”重新进行文书制作与发送！");break;
					break;
				case "修改发文号":Helper.showAlert("保存成功");break;
			}
		}

		public function updateRepGet():void
		{
			curObj=wsObj;
			curObj.ID_STATUS="已发文";
			Helper.showAlert("保存成功");
		}		
		public function getNextNo(num:String):void
		{
			var arr=num.split('，');
			notxt.text=arr[0];
			if(accCaseTxt!=null)accCaseTxt.text=arr[1];
		}
		public function getAll(xml:XML):void
		{	
			if(auditMsg=="任务检索中")auditMsg="";
			
			listObj.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var quhao="";
//				switch(xml.children()[i].鉴定单位.toString())
//				{
//					case "1":quhao="02";break;
//					case "7":quhao="0227";break;
//					case "8":quhao="0226";break;
//					case "10":quhao="0282";break;
//					case "11":quhao="0281";break;
//					default :quhao="";
//				}
				var voObj:IdFlowVo=new IdFlowVo(xml.children()[i].委托编号,
					xml.children()[i].鉴定单位,xml.children()[i].委托表号,xml.children()[i].鉴定状态,xml.children()[i].委托单位,xml.children()[i].送检人一,
					xml.children()[i].一送姓名,xml.children()[i].一送警号,xml.children()[i].一送电话,
					xml.children()[i].二送姓名,xml.children()[i].二送警号,xml.children()[i].二送电话,xml.children()[i].委托年份,
					xml.children()[i].委托序号,xml.children()[i].委托时间,xml.children()[i].鉴定专业,xml.children()[i].鉴定类别,xml.children()[i].鉴定项目,
					xml.children()[i].鉴定要求,xml.children()[i].文书名称,xml.children()[i].受理年份,xml.children()[i].受理序号,xml.children()[i].案件序号,xml.children()[i].发文年份,
					xml.children()[i].发文序号,xml.children()[i].受理人员,xml.children()[i].受理时间,xml.children()[i].计划完成,xml.children()[i].认证认可,
					xml.children()[i].受理意见,xml.children()[i].鉴定结论,xml.children()[i].结论概述,xml.children()[i].一检人,xml.children()[i].一检完成,
					xml.children()[i].二检人,xml.children()[i].二检完成,xml.children()[i].三检人,xml.children()[i].三检完成,xml.children()[i].四检人,
					xml.children()[i].四检完成,xml.children()[i].复核人,xml.children()[i].复核完成,xml.children()[i].授权签字,xml.children()[i].签字完成,
					xml.children()[i].技管,xml.children()[i].技管完成,xml.children()[i].领导,xml.children()[i].审批完成,xml.children()[i].发文确认,
					xml.children()[i].一检留言,xml.children()[i].鉴定记事,xml.children()[i].文书领取,xml.children()[i].领取完成,xml.children()[i].领取人一,
					xml.children()[i].领一电话,xml.children()[i].领取人二,xml.children()[i].领二电话,					
					xml.children()[i].鉴定单位名称,xml.children()[i].委托单位名称,xml.children()[i].委托单位简称,xml.children()[i].委托单位编号,xml.children()[i].送检人一姓名,xml.children()[i].送检人一长号,xml.children()[i].送检人一短号,
					xml.children()[i].受理人员姓名,xml.children()[i].一检姓名,xml.children()[i].二检姓名,
					xml.children()[i].三检姓名,xml.children()[i].四检姓名,xml.children()[i].复核姓名,xml.children()[i].签字姓名,
					xml.children()[i].技管姓名,xml.children()[i].领导姓名,					
					xml.children()[i].ID,xml.children()[i].打防管控,
					xml.children()[i].现场勘验,xml.children()[i].案件名称,xml.children()[i].案件类型,xml.children()[i].案件类别,
					(xml.children()[i].案件编号==""?(Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.CLN,xml.children()[i].受理年份,xml.children()[i].案件序号,"")):xml.children()[i].案件编号),
					xml.children()[i].发案地点,xml.children()[i].区划代码,xml.children()[i].发案时间,xml.children()[i].案件性质,xml.children()[i].简要案情,xml.children()[i].SRCID,
					xml.children()[i].物证处置,xml.children()[i].领物人,xml.children()[i].领物时间,xml.children()[i].物证处置备注,
					xml.children()[i].原鉴定情况,xml.children()[i].ORA_FLAG,xml.children()[i].被鉴定人,xml.children()[i].名称,xml.children()[i].鉴定方法);
				listObj.addItem(voObj);
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
		public function getNextSLN(num:String):void
		{
			if(mpnotxt!=null)
			{
				mpnotxt.text=Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.LSLN,wsObj.ACC_YEAR,wsObj.ACC_NO,num);
			}
			else if(mpr1notxt!=null)
			{
				mpr1notxt.text=Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.LRSLN,wsObj.ACC_YEAR,wsObj.ACC_NO,num);
				mpr2notxt.text=Helper.getSlnStr(JusTypeLocator.getInstance().jusTypeVo.LRSLN,wsObj.ACC_YEAR,wsObj.ACC_NO,(Number(num)+1).toString());
			}
			else
			{
				senotxt.text=num;
			}
		}
		public function getCaseNextSLN(num:String):void
		{
			var numarr:Array=num.split("-");
			senotxt.text=numarr[0];
			cpsnotxt.text=numarr[1];
			crnotxt.text=numarr[2];
			udnotxt.text=numarr[3];
		}
	}
}