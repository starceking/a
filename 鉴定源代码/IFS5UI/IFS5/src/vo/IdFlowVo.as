package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import util.Helper;
	
	public class IdFlowVo implements ValueObject
	{
		public function IdFlowVo(CONNO:String,ID_PSB:String,CONSIGNID:String,ID_STATUS:String,CON_PSB:String,CONER1:String,
								 CONER1NAME2:String,CONER1POLICENO:String,CONER1PHONE:String,
								 CONER2NAME:String,CONER2POLICENO:String,CONER2PHONE:String,CON_YEAR:String,CON_NO:String,
								 CON_DATE:String,ID_OFFICE:String,ID_JUSTYPE:String,ID_JUSITEM:String,ID_REQUEST:String,DOC_NAME:String,
								 ACC_YEAR:String,ACC_NO:String,ACC_CASE_NO:String,DOC_YEAR:String,DOC_NO:String,ACCER:String,ACC_TIME:String,PLAN_DATE:String,
								 RZRK:String,ACC_REMARK:String,CONCLUSION:String,CONCLUSION_REMARK:String,TESTER:String,TESTERF:String,
								 TESTER2:String,TESTER2F:String,TESTER3:String,TESTER3F:String,TESTER4:String,TESTER4F:String,
								 CHECKER:String,CHECKERF:String,SIGNER:String,SIGNERF:String,TECHER:String,TECHERF:String,
								 LEADER:String,LEADERF:String,TESTERSDF:String,TESTER_REMARK:String,ID_RECORD:String,SDER:String,
								 SDF:String,GP1NAME:String,GP1PHONE:String,GP2NAME:String,GP2PHONE:String,
								 
								 ID_PSBNAME:String, CON_PSBNAME:String, CON_PSBNICKNAME:String,CON_PSBCODE:String,CONER1NAME:String,CONER1LPHONE:String,CONER1SPHONE:String,
								 ACCEPTER_NAME:String,
								 TESTER_NAME:String,TESTER2_NAME:String,TESTER3_NAME:String,TESTER4_NAME:String,
								 CHECKER_NAME:String,SIGNER_NAME:String,TECHER_NAME:String,LEADER_NAME:String,
								 
								 ID:String,DFGKNO:String,XCKYNO:String,CASE_NAME:String,CASE_TYPE:String,CASE_TYPE2:String,CASE_NO:String,
								 SCENE_PLACE:String, RegionCode:String,OCCURRENCE_DATE:String,CASE_PROPERTY:String,CASE_SUMMARY:String,SRCID:String,
								 
								 TesOper:String,GetTesPerson:String,GetTesDate:String,TesOperRemark:String,
								 
								 OrgIdResult:String,ORA_FLAG:String,BJDR:String,Name:String,ID_Method:String)
		{
			this.CONNO=CONNO;			
			this.ID_PSB=ID_PSB;
			this.CONSIGNID=CONSIGNID;
			this.ID_STATUS=ID_STATUS;
			this.CON_PSB=CON_PSB;
			this.CONER1=CONER1;
			this.CONER1NAME2=CONER1NAME2;
			this.CONER1POLICENO=CONER1POLICENO;
			this.CONER1PHONE=CONER1PHONE;
			this.CONER2NAME=CONER2NAME;
			this.CONER2POLICENO=CONER2POLICENO;
			this.CONER2PHONE=CONER2PHONE;
			this.CON_YEAR=CON_YEAR;
			this.CON_NO=CON_NO;
			this.CON_DATE=Helper.getDateString(CON_DATE);
			this.ID_OFFICE=ID_OFFICE;
			this.ID_JUSTYPE=ID_JUSTYPE;
			this.ID_JUSITEM=ID_JUSITEM;
			this.ID_REQUEST=ID_REQUEST;
			this.DOC_NAME=DOC_NAME;
			this.ACC_YEAR=ACC_YEAR;
			this.ACC_NO=ACC_NO;
			this.ACC_CASE_NO=ACC_CASE_NO;
			this.DOC_YEAR=DOC_YEAR;
			this.DOC_NO=DOC_NO;
			this.ACCER=ACCER;
			this.ACC_TIME=Helper.getDateString(ACC_TIME);
			this.PLAN_DATE=Helper.getDateString(PLAN_DATE);
			this.RZRK=RZRK;
			this.ACC_REMARK=ACC_REMARK;
			this.CONCLUSION=CONCLUSION;
			this.CONCLUSION_REMARK=CONCLUSION_REMARK;
			this.TESTER=TESTER;
			this.TESTERF=Helper.getDateString(TESTERF);
			this.TESTER2=TESTER2;
			this.TESTER2F=Helper.getDateString(TESTER2F);
			this.TESTER3=TESTER3;
			this.TESTER3F=Helper.getDateString(TESTER3F);
			this.TESTER4=TESTER4;
			this.TESTER4F=Helper.getDateString(TESTER4F);
			this.CHECKER=CHECKER;
			this.CHECKERF=Helper.getDateString(CHECKERF);
			this.SIGNER=SIGNER;
			this.SIGNERF=Helper.getDateString(SIGNERF);
			this.TECHER=TECHER;
			this.TECHERF=Helper.getDateString(TECHERF);
			this.LEADER=LEADER;
			this.LEADERF=Helper.getDateString(LEADERF);
			this.TESTERSDF=Helper.getDateString(TESTERSDF);
			this.TESTER_REMARK=TESTER_REMARK;
			this.ID_RECORD=ID_RECORD;
			this.SDER=SDER;
			this.SDF=Helper.getDateString(SDF);
			this.GP1NAME=GP1NAME;
			this.GP1PHONE=GP1PHONE;
			this.GP2NAME=GP2NAME;
			this.GP2PHONE=GP2PHONE;
			
			this.ID_PSBNAME=ID_PSBNAME;
			this.CON_PSBNAME=CON_PSBNAME;
			this.CON_PSBNICKNAME=CON_PSBNICKNAME;
			this.CON_PSBCODE=CON_PSBCODE;
			this.CONER1NAME=CONER1NAME;		
			this.CONER1LPHONE=CONER1LPHONE;		
			this.CONER1SPHONE=CONER1SPHONE;		
			
			this.ACCEPTER_NAME=ACCEPTER_NAME;
			this.TESTER_NAME=TESTER_NAME;
			this.TESTER2_NAME=TESTER2_NAME;
			this.TESTER3_NAME=TESTER3_NAME;
			this.TESTER4_NAME=TESTER4_NAME;
			this.CHECKER_NAME=CHECKER_NAME;
			this.SIGNER_NAME=SIGNER_NAME;
			this.TECHER_NAME=TECHER_NAME;
			this.LEADER_NAME=LEADER_NAME;
			
			this.ID=ID;
			this.DFGKNO=DFGKNO;
			this.XCKYNO=XCKYNO;
			this.CASE_NAME=CASE_NAME;
			this.CASE_TYPE=CASE_TYPE;
			this.CASE_TYPE2=CASE_TYPE2;
			this.CASE_NO=CASE_NO;
			this.SCENE_PLACE=SCENE_PLACE;
			this.RegionCode=RegionCode;
			this.OCCURRENCE_DATE=Helper.getDateString(OCCURRENCE_DATE);
			this.CASE_PROPERTY=CASE_PROPERTY;
			this.CASE_SUMMARY=CASE_SUMMARY;
			this.SRCID=SRCID;
			
			this.TesOper=TesOper;
			this.GetTesPerson=GetTesPerson;
			this.GetTesDate=GetTesDate;
			this.TesOperRemark=TesOperRemark;
			
			this.OrgIdResult=OrgIdResult;
			this.ORA_FLAG=ORA_FLAG;
			this.BJDR=BJDR;
			
			this.Name=Name;
			
			this.ID_Method=ID_Method;
			//this.TestedName=TestedName;
		}
		public var CONNO:String;//委托编号		
		public var ID_PSB:String;//鉴定单位
		public var CONSIGNID:String;//委托表号
		public var ID_STATUS:String;//鉴定状态
		public var CON_PSB:String;//委托单位
		public var CONER1:String;//送检人一
		public var CONER1NAME2:String;//一送姓名
		public var CONER1POLICENO:String;//一送警号
		public var CONER1PHONE:String;//一送电话
		public var CONER2NAME:String;//二送姓名
		public var CONER2POLICENO:String;//二送警号
		public var CONER2PHONE:String;//二送电话
		public var CON_YEAR:String;//委托年份
		public var CON_NO:String;//委托序号
		public var CON_DATE:String;//委托时间
		public var ID_OFFICE:String;//鉴定专业
		public var ID_JUSTYPE:String;//鉴定类别
		public var ID_JUSITEM:String;//鉴定项目
		public var ID_REQUEST:String;//鉴定要求
		public var DOC_NAME:String;//文书名称
		public var ACC_YEAR:String;//受理年份
		public var ACC_NO:String;//受理序号
		public var ACC_CASE_NO:String;//受理序号
		public var DOC_YEAR:String;//发文年份
		public var DOC_NO:String;//发文序号
		public var ACCER:String;//受理人员
		public var ACC_TIME:String;//受理时间
		public var PLAN_DATE:String;//计划完成
		public var RZRK:String;//认证认可
		public var ACC_REMARK:String;//受理意见
		public var CONCLUSION:String;//鉴定结论
		public var CONCLUSION_REMARK:String;//结论概述
		public var TESTER:String;//一检人
		public var TESTERF:String;//一检完成
		public var TESTER2:String;//二检人
		public var TESTER2F:String;//二检完成
		public var TESTER3:String;//三检人
		public var TESTER3F:String;//三检完成
		public var TESTER4:String;//四检人
		public var TESTER4F:String;//四检完成
		public var CHECKER:String;//复核人
		public var CHECKERF:String;//复核完成
		public var SIGNER:String;//授权签字
		public var SIGNERF:String;//签字完成
		public var TECHER:String;//技管
		public var TECHERF:String;//技管完成
		public var LEADER:String;//领导
		public var LEADERF:String;//审批完成
		public var TESTERSDF:String;//发文确认
		public var TESTER_REMARK:String;//一检留言
		public var ID_RECORD:String;//鉴定记事
		public var SDER:String;//文书领取
		public var SDF:String;//领取完成
		public var GP1NAME:String;//领取人一
		public var GP1PHONE:String;//领一电话
		public var GP2NAME:String;//领取人二
		public var GP2PHONE:String;//领二电话
		
		public function get ACC_NO_SHOW():String
		{
			return "("+DOC_NAME+")受字["+ACC_YEAR+"]"+ACC_NO+"号";
		}
		public function get DOC_NO_SHOW():String
		{
			return "("+DOC_NAME+")鉴字["+ACC_YEAR+"]"+ACC_CASE_NO+"号";
		}
		public var ID_PSBNAME:String;	//鉴定单位名称
		public var CON_PSBNAME:String;	//委托单位名称
		public var CON_PSBNICKNAME:String;	//委托单位简称
		public var CON_PSBCODE:String;	//委托单位编号
		public var CONER1NAME:String;//一送姓名
		public var CONER1LPHONE:String;//一送长号
		public var CONER1SPHONE:String;//一送短号
		
		public var ACCEPTER_NAME:String;//受理人员姓名
		public var TESTER_NAME:String;//一检姓名
		public var TESTER2_NAME:String;//二检姓名
		public var TESTER3_NAME:String;//三检姓名
		public var TESTER4_NAME:String;//四检姓名
		public var CHECKER_NAME:String;//复核姓名
		public var SIGNER_NAME:String;//签字姓名
		public var TECHER_NAME:String;//技管姓名
		public var LEADER_NAME:String;//领导姓名
		
		public var ID:String;//ID
		public var DFGKNO:String;//打防管控
		public var XCKYNO:String;//现场勘验
		public var CASE_NAME:String;//案件名称
		public var CASE_TYPE:String;//案件类型
		public var CASE_TYPE2:String;//案件类别
		public var CASE_NO:String;//案件编号
		public var SCENE_PLACE:String;//发案地点
		public var RegionCode:String;//区划代码
		public var OCCURRENCE_DATE:String;//发案时间
		public var CASE_PROPERTY:String;//案件性质
		public var CASE_SUMMARY:String;//简要案情
		public var SRCID:String;//SRCID
		
		public var TesOper:String;//物证处置
		public var GetTesPerson:String;//领物人
		public var GetTesDate:String;//领物时间
		public var TesOperRemark:String;//物证处置备注
		
		public var OrgIdResult:String;//原鉴定情况
		public var ORA_FLAG:String;//ORA_FLAG
		public var BJDR:String;//被鉴定人
		
		public var Name:String;//名称
		
		public var ID_Method:String;//鉴定方法
		
		
		
		
		//public var TestedName:String;
		public function get TEST_DAYS():String
		{
			if(TESTERF.length>0)
			{
				return Helper.getDiffDays(TESTERF,ACC_TIME).toString();
			}
			else if(ID_STATUS!="不予受理")
			{
				return Helper.getDiffDaysByDate(new Date(),ACC_TIME).toString();
			}
			return "";
		}
		
		public function get SD_DAYS():String
		{
			if(SDF.length>0)
			{
				return Helper.getDiffDays(SDF,ACC_TIME).toString();
			}
			else if((ID_STATUS!="不予受理")&&(ID_STATUS!="已存档"))
			{
				return Helper.getDiffDaysByDate(new Date(),ACC_TIME).toString();
			}
			return "";
		}
		
	}
}