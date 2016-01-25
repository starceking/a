package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	import util.Helper;
	import mx.collections.ArrayList;
	public class SuppliesUseVo  implements ValueObject
	{
		public function SuppliesUseVo(ID:String,HCID:String,SYR:String,SYRQ:String,XHSL:String,PH:String)
		{
			this.ID=ID;
			this.HCID=HCID;
			this.SYR=SYR;
			this.SYRQ=SYRQ;
			this.XHSL=XHSL;
			this.PH=PH;
			
		}
		public var ID:String;
		public var HCID:String;
		public var SYR:String;
		public var SYRQ:String;
		public var XHSL:String;
		public var PH:String;
	}
}