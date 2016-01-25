package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	import util.Helper;
	import mx.collections.ArrayList;
	public class EquipmentCheckVo implements ValueObject
	{
		public function EquipmentCheckVo(ID:String,SBID:String,HCR:String,JDR:String,HCJG:String,HCRQ:String,
										 HCNR:String)
		{
			this.ID=ID;
			this.SBID=SBID;
			this.HCR=HCR;
			this.JDR=JDR;
			this.HCJG=HCJG;
			this.HCRQ=HCRQ;
			this.HCNR=HCNR;
			
		}
		public var ID:String;
		public var SBID:String;
		public var HCR:String;
		public var JDR:String;
		public var HCJG:String;
		public var HCRQ:String;
		public var HCNR:String;
	}
}