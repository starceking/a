package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	import util.Helper;
	import mx.collections.ArrayList;
	public class SuppliesProcurementVo implements ValueObject
	{
		public function SuppliesProcurementVo(ID:String,HCID:String,CGR:String,CGSL:String,PH:String,CGRQ:String,
											  YSJG:String)
		{
			this.ID=ID;
			this.HCID=HCID;
			this.CGR=CGR;
			this.CGSL=CGSL;
			this.PH=PH;
			this.CGRQ=CGRQ;
			this.YSJG=YSJG;
			
		}
		public var ID:String;
		public var HCID:String;
		public var CGR:String;
		public var CGSL:String;
		public var PH:String;
		public var CGRQ:String;
		public var YSJG:String;

	}
}