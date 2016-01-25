package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	public class JusTypeVo implements ValueObject
	{
		public function JusTypeVo(YEAR:String,TESTER:String,TESTER2:String,TESTER3:String,TESTER4:String,
								  CHECKER:String,SIGN:String,TECH:String,LEADER:String,TESTERSD:String,
								  Leader:String,WholeNo:String,DocName:String,PlanDate:String,Enabled:String,
								  IDREQ:String,CLN:String,SESLN:String,CPSSLN:String,RSLN:String,
								  USLN:String,LSLN:String,LRSLN:String)
		{
			this.YEAR=YEAR;
			this.TESTER=TESTER;
			this.TESTER2=TESTER2;
			this.TESTER3=TESTER3;
			this.TESTER4=TESTER4;
			this.CHECKER=CHECKER;
			this.SIGN=SIGN;
			this.TECH=TECH;
			this.LEADER=LEADER;
			this.TESTERSD=TESTERSD;
			
			this.Leader=Leader;
			this.WholeNo=WholeNo;
			this.DocName=DocName;
			this.PlanDate=PlanDate;
			this.Enabled=Enabled;
			this.IDREQ=IDREQ;
			this.CLN=CLN;
			this.SESLN=SESLN;
			this.CPSSLN=CPSSLN;
			this.RSLN=RSLN;
			this.USLN=USLN;
			this.LSLN=LSLN;
			this.LRSLN=LRSLN;
		}
		public var YEAR:String;
		public var TESTER:String;
		public var TESTER2:String;
		public var TESTER3:String;
		public var TESTER4:String;
		public var CHECKER:String;
		public var SIGN:String;
		public var TECH:String;
		public var LEADER:String;
		public var TESTERSD:String;
		
		public var Leader:String;
		public var WholeNo:String;
		public var DocName:String;
		public var PlanDate:String;
		public var Enabled:String;
		public var IDREQ:String;
		public var CLN:String;
		public var SESLN:String;
		public var CPSSLN:String;
		public var RSLN:String;
		public var USLN:String;
		public var LSLN:String;
		public var LRSLN:String;
	}
}