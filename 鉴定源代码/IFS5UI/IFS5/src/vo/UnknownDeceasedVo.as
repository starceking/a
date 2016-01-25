package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	public class UnknownDeceasedVo implements ValueObject
	{
		public function UnknownDeceasedVo(ID:String,CASE_ID:String,CONNO:String,NAME:String,SAMPLE_TYPE:String,GENDER:String,PACKAGE:String,
										  SAMPLE_DESCRIPTION:String,SPECIFICATION:String,AGE:String,REMARK:String,SLN:String,ORA_FLAG:String)
		{
			this.ID=ID;
			this.CASE_ID=CASE_ID;
			this.CONNO=CONNO;
			this.NAME=NAME;
			this.SAMPLE_TYPE=SAMPLE_TYPE;
			this.GENDER=GENDER;
			this.PACKAGE=PACKAGE;
			this.SAMPLE_DESCRIPTION=SAMPLE_DESCRIPTION;
			this.SPECIFICATION=SPECIFICATION;
			this.AGE=AGE;
			this.REMARK=REMARK;
			this.SLN=SLN;
			this.ORA_FLAG=ORA_FLAG;
		}
		public var ID:String;
		public var CASE_ID:String;
		public var CONNO:String;
		public var NAME:String;
		public var SAMPLE_TYPE:String;
		public var GENDER:String;
		public var PACKAGE:String;
		public var SAMPLE_DESCRIPTION:String;
		public var SPECIFICATION:String;
		public var AGE:String;
		public var REMARK:String;
		public var SLN:String;
		public var ORA_FLAG:String;
	}
}