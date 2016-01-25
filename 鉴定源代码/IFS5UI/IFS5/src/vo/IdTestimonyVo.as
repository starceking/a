package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	public class IdTestimonyVo implements ValueObject
	{
		public function IdTestimonyVo(ID:String,IsCtr:String,CONNO:String,NAME:String,AMOUNT:String,WEIGHT:String,PACKAGE:String,
									PROPERTY:String,EX_PERSON:String,EX_METHOD:String,EX_POSITION:String,EX_DATE:String,REMARK:String,
									SLN:String,UNIT:String)
		{
			this.ID=ID;
			this.IsCtr=IsCtr;
			this.CONNO=CONNO;
			this.NAME=NAME;
			this.AMOUNT=AMOUNT;
			this.WEIGHT=WEIGHT;
			this.PACKAGE=PACKAGE;
			this.PROPERTY=PROPERTY;
			this.EX_PERSON=EX_PERSON;
			this.EX_METHOD=EX_METHOD;
			this.EX_POSITION=EX_POSITION;
			this.EX_DATE=EX_DATE;
			this.REMARK=REMARK;
			this.SLN=SLN;
			this.UNIT=UNIT;
		}
		public var ID:String;
		public var IsCtr:String;
		public var CONNO:String;
		public var NAME:String;
		public var AMOUNT:String;
		public var WEIGHT:String;
		public var PACKAGE:String;
		public var PROPERTY:String;
		public var EX_PERSON:String;
		public var EX_METHOD:String;
		public var EX_POSITION:String;
		public var EX_DATE:String;
		public var REMARK:String;
		public var SLN:String;
		public var UNIT:String;
	}
}