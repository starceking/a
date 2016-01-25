package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	import util.Helper;
	import mx.collections.ArrayList;
	public class EquipmentUseVo implements ValueObject
	{
		public function EquipmentUseVo(ID:String,SBID:String,SYR:String,KSSJ:String,JSSJ:String,ZT:String,
									   SYYY:String)
		{
			this.ID=ID;
			this.SBID=SBID;
			this.SYR=SYR;
			this.KSSJ=KSSJ;
			this.JSSJ=JSSJ;
			this.ZT=ZT;
			this.SYYY=SYYY;

		}
		public var ID:String;
		public var SBID:String;
		public var SYR:String;
		public var KSSJ:String;
		public var JSSJ:String;
		public var ZT:String;
		public var SYYY:String;

	}
}