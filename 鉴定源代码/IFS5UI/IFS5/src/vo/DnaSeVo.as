package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	public class DnaSeVo implements ValueObject
	{
		public function DnaSeVo(ID:String,CASE_ID:String,CONNO:String,NAME:String,SAMPLE_TYPE:String,
								AMOUNT:String,CARRIER:String,SLN:String,ORA_FLAG:String,SAMPLE_PACKAGING:String)
		{
			this.ID=ID;
			this.CASE_ID=CASE_ID;
			this.CONNO=CONNO;
			this.NAME=NAME;
			this.SAMPLE_TYPE=SAMPLE_TYPE;
			this.AMOUNT=AMOUNT;
			this.CARRIER=CARRIER;
			this.SLN=SLN;
			this.ORA_FLAG=ORA_FLAG;
			this.SAMPLE_PACKAGING=SAMPLE_PACKAGING;
		}
		public var ID:String;
		public var CASE_ID:String;
		public var CONNO:String;
		public var NAME:String;
		public var SAMPLE_TYPE:String;
		public var AMOUNT:String;
		public var CARRIER:String;
		public var SLN:String;
		public var ORA_FLAG:String;
		public var SAMPLE_PACKAGING:String;
	}
}