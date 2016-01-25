package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	import util.Helper;
	import mx.collections.ArrayList;
	public class SuppliesVerificationVo implements ValueObject
	{
		public function SuppliesVerificationVo(ID:String,HCID:String,HCR:String,JDR:String,HCRQ:String,HCJG:String,CHGC:String)
		{
			this.ID=ID;
			this.HCID=HCID;
			this.HCR=HCR;
			this.JDR=JDR;
			this.HCRQ=HCRQ;
			this.HCJG=HCJG;
			this.CHGC=CHGC;

			
		}
		public var ID:String;
		public var HCID:String;
		public var HCR:String;
		public var JDR:String;
		public var HCRQ:String;
		public var HCJG:String;
		public var CHGC:String;
	}
}