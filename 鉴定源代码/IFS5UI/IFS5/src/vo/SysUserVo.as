package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	import util.Helper;
	
	public class SysUserVo implements ValueObject
	{
		public function SysUserVo(ID:String,PSBID:String,OFFICE:String,POLICENO:String,PASSWORD:String,NAME:String,GENDER:String,
								  IDCARDNO:String,LONGPHONE:String,SHORTPHONE:String,TECHTITLE:String,ROLES:String,AUTH:String,
								  DELETED:String,INDEX:String,ZW:String,BrithDay:String,Address:String,POSTCODE:String,
								  PHONE:String,NativeAddr:String,PoliticalState:String,JobStartDate:String,JobTitle1:String,
								  AcquireDate1:String, JobTitle2:String,AcquireDate2:String,Discipline:String,JusType:String,
								  PractisingCertificateNo:String,JG:String,
								  PID:String,PTYPE:String,PNUMBER:String,PNAME:String,PADDRESS:String,PPOSTCODE:String,
								  PNICKNAME:String,PPHONE:String)
		{
			this.ID=ID;
			this.PSBID=PSBID;
			this.OFFICE=OFFICE;
			this.POLICENO=POLICENO;
			this.PASSWORD=PASSWORD;
			this.NAME=NAME;
			this.GENDER=GENDER;
			this.IDCARDNO=IDCARDNO;
			this.LONGPHONE=LONGPHONE;
			this.SHORTPHONE=SHORTPHONE;
			this.TECHTITLE=TECHTITLE;
			this.ROLES=ROLES;
			this.AUTH=AUTH;				
			this.DELETED=DELETED;
			this.INDEX=INDEX;
			this.ZW=ZW;
			this.BrithDay=BrithDay;
			this.Address=Address;
			this.POSTCODE=POSTCODE;				
			this.PHONE=PHONE;
			this.NativeAddr=NativeAddr;
			this.PoliticalState=PoliticalState;
			this.JobStartDate=Helper.getDateString(JobStartDate);
			this.JobTitle1=JobTitle1;
			this.AcquireDate1=Helper.getDateString(AcquireDate1);			
			this.JobTitle2=JobTitle2;
			this.AcquireDate2=Helper.getDateString(AcquireDate2);
			this.Discipline=Discipline;
			this.JusType=JusType;
			this.PractisingCertificateNo=PractisingCertificateNo;
			this.JG=JG;
			
			this.PID=PID;
			this.PTYPE=PTYPE;
			this.PNUMBER=PNUMBER;
			this.PNAME=PNAME;
			this.PADDRESS=PADDRESS;
			this.PPOSTCODE=PPOSTCODE;
			this.PNICKNAME=PNICKNAME;
			this.PPHONE=PPHONE;
		}
		public var ID:String;//ID
		public var PSBID:String;//单位ID
		public var OFFICE:String;//鉴定专业
		public var POLICENO:String;//警号
		public var PASSWORD:String;//密码
		public var NAME:String;//姓名
		public var GENDER:String;//性别
		public var IDCARDNO:String;//身份证
		public var LONGPHONE:String;//长号
		public var SHORTPHONE:String;//短号
		public var TECHTITLE:String;//技术职称
		public var ROLES:String;//角色
		public var AUTH:String;//用户权限
		public var CURRENT_ROLE:String;//
		public var DELETED:String;//是否删除
		public var INDEX:String;//序号
		public var ZW:String;//职务
		public var BrithDay:String;//出生日期
		public var Address:String;//地址
		public var POSTCODE:String;//家庭电话
		public var PHONE:String;//联系电话
		public var NativeAddr:String;//民族
		public var PoliticalState:String;//政治面貌
		public var JobStartDate:String;//参加工作时间
		public var JobTitle1:String;//专业技术职称1
		public var AcquireDate1:String;//取得时间1
		public var JobTitle2:String;//专业技术职称2
		public var AcquireDate2:String;//取得时间2
		public var Discipline:String;//现从事专业
		public var JusType:String;//司法鉴定人执业类别
		public var PractisingCertificateNo:String;//执业证号
		public var JG:String;//籍贯
		
		public var PID:String;//单位父单位
		public var PTYPE:String;//单位类型
		public var PNUMBER:String;//单位编号
		public var PNAME:String;//单位名称
		public var PADDRESS:String;//单位地址
		public var PPOSTCODE:String;//单位邮编
		public var PNICKNAME:String;//单位简称
		public var PPHONE:String;//单位电话
	}
}