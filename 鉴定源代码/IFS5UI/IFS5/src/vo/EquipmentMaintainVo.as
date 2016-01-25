package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import mx.collections.ArrayList;
	
	import util.Helper;
	public class EquipmentMaintainVo  implements ValueObject
	{
		public function EquipmentMaintainVo(ID:String,SBID:String,WFR:String,WFSJ:String,WHNR:String,RESULT:String)
		{
			this.ID=ID;
			this.SBID=SBID;
			this.WFR=WFR;
			this.WFSJ=WFSJ;
			this.WHNR=WHNR;
			this.RESULT=RESULT;
		}
		public var ID:String;
		public var SBID:String;
		public var WFR:String;
		public var WFSJ:String;
		public var WHNR:String;
		public var RESULT:String;

	}
}