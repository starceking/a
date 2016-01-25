package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	import locator.SysUserLocator;
	import util.Helper;
	
	public class PreExamVo implements ValueObject
	{
		public function PreExamVo(ID:String,CASE_ID:String,CONNO:String,SC:String,
							  SLN:String,NAME:String,SAMPLE_TYPE:String,
							  TEST_METHOD:String,TESTER:String,TEST_DATE:String,RESULT:String)
		{
			this.ID=ID;
			this.CASE_ID=CASE_ID;
			this.CONNO=CONNO;
			this.SLN=SLN;
			this.NAME=NAME;
			this.SC=SC;
			this.SAMPLE_TYPE=SAMPLE_TYPE;
			this.TEST_METHOD=TEST_METHOD;
			this.TESTER=TESTER;
			this.TEST_DATE=Helper.getDateString(TEST_DATE);
			this.RESULT=RESULT;
			this.TESTER_NAME=SysUserLocator.getInstance().getUserName(TESTER);
		}
		public var ID:String;
		public var CASE_ID:String;
		public var CONNO:String;
		public var SLN:String;
		public var NAME:String;
		public var SC:String;		
		public var SAMPLE_TYPE:String;
		public var TEST_METHOD:String;
		public var TESTER:String;
		public var TEST_DATE:String;		
		public var RESULT:String;
		public var TESTER_NAME:String;
	}
}