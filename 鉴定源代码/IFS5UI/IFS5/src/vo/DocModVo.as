package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import util.Helper;	
	import locator.SysUserLocator;
	
	public class DocModVo implements ValueObject
	{
		public function DocModVo(ID:String,CONNO:String,MODER:String,MODTIME:String,AUDIT:String,AUDITTIME:String,
								   POSITION:String,ORITEXT:String,NOWTEXT:String,NUMBER:String)
		{
			this.ID=ID;
			this.CONNO=CONNO;
			this.MODER=MODER;
			this.MODTIME=Helper.getDateString(MODTIME);
			this.AUDIT=AUDIT;
			this.AUDITTIME=Helper.getDateString(AUDITTIME);
			this.POSITION=POSITION;
			this.ORITEXT=ORITEXT;
			this.NOWTEXT=NOWTEXT;
			this.NUMBER=NUMBER;
			
			this.MODER_NAME=SysUserLocator.getInstance().getUserName(MODER);
			this.AUDIT_NAME=SysUserLocator.getInstance().getUserName(AUDIT);
		}
		public var ID:String;
		public var CONNO:String;
		public var MODER:String;
		public var MODTIME:String;
		public var AUDIT:String;
		public var AUDITTIME:String;
		public var POSITION:String;
		public var ORITEXT:String;
		public var NOWTEXT:String;
		public var MODER_NAME:String;
		public var AUDIT_NAME:String;
		public var NUMBER:String;
	}
}