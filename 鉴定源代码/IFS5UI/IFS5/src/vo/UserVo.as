package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	public class UserVo implements ValueObject
	{
		public function UserVo(ID:String="",PSBID:String="",OFFICE:String="",POLICENO:String="",PASSWORD:String="",NAME:String="",GENDER:String="",
								  IDCARDNO:String="",LONGPHONE:String="",SHORTPHONE:String="",TECHTITLE:String="",ROLES:String="",AUTH:String="",
								  DELETED:String="",INDEX:String="",Birthday:String="",JobTime:String="",Nation:String="",XRXZZW:String="",
								  ZZMM:String="",QDZCSJ:String="",ZYLB:String="",ZYXH:String="",TXDZ:String="",YB:String="",XL:String="",
								  XLBYSJ:String="",XLBYYX:String="",XLSXZY:String="",XW:String="",XWBYSJ:String="",XWBYYX:String="",
								  XWSXZY:String="",XSTT:String="",WY:String="",ZDSGJL:String="")
		{
			this.ID=ID;
			this.PSBID=PSBID;
			this.OFFICE=OFFICE;//鉴定专业
			this.POLICENO=POLICENO;//警号
			this.PASSWORD=PASSWORD;//密码
			this.NAME=NAME;//姓名
			this.GENDER=GENDER;//性别
			this.IDCARDNO=IDCARDNO;//身份证
			this.LONGPHONE=LONGPHONE;//长号
			this.SHORTPHONE=SHORTPHONE;//短号
			this.TECHTITLE=TECHTITLE;//技术职称
			this.ROLES=ROLES;//角色
			this.AUTH=AUTH;//用户权限	
			this.DELETED=DELETED;//是否删除
			this.INDEX=INDEX;//序号
			
			this.Birthday=Birthday;//出生年月
			this.JobTime=JobTime;//参加工作时间
			this.Nation=Nation;//民族
			this.XRXZZW=XRXZZW;//现任行政职务
			this.ZZMM=ZZMM;//政治面貌
			this.QDZCSJ=QDZCSJ;//取得职称时间
			this.ZYLB=ZYLB;//司法鉴定人职业类别
			this.ZYXH=ZYXH;//执业证号
			this.TXDZ=TXDZ;//通讯地址
			this.YB=YB;//邮编
			this.XL=XL;//学历
			this.XLBYSJ=XLBYSJ;//学历毕业时间
			this.XLBYYX=XLBYYX;//学历毕业院校
			this.XLSXZY=XLSXZY;//学历所学专业
			this.XW=XW;//学位
			this.XWBYSJ=XWBYSJ;//学位毕业时间
			this.XWBYYX=XWBYYX;//学位毕业院校
			this.XWSXZY=XWSXZY;//学位所学专业
			this.XSTT=XSTT;//学术团体
			this.WY=WY;//外语
			this.ZDSGJL=ZDSGJL;//重大事故记录
		}
		public var ID:String;
		public var PSBID:String;
		public var OFFICE:String;
		public var POLICENO:String;
		public var PASSWORD:String;
		public var NAME:String;
		public var GENDER:String;
		public var IDCARDNO:String;
		public var LONGPHONE:String;
		public var SHORTPHONE:String;
		public var TECHTITLE:String;
		public var ROLES:String;
		public var AUTH:String;
		public var CURRENT_ROLE:String;
		public var DELETED:String;
		public var INDEX:String;
		public var Birthday:String;
		public var JobTime:String;
		public var Nation:String;
		public var XRXZZW:String;
		public var ZZMM:String;
		public var QDZCSJ:String;
		public var ZYLB:String;
		public var ZYXH:String;
		public var TXDZ:String;
		public var YB:String;
		public var XL:String;
		public var XLBYSJ:String;
		public var XLBYYX:String;
		public var XLSXZY:String;
		public var XW:String;
		public var XWBYSJ:String;
		public var XWBYYX:String;
		public var XWSXZY:String;
		public var XSTT:String;
		public var WY:String;
		public var ZDSGJL:String;
	}
}