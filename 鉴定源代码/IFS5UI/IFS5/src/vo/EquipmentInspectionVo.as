package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	import util.Helper;
	import mx.collections.ArrayList;
	public class EquipmentInspectionVo implements ValueObject
	{
		public function EquipmentInspectionVo(ID:String,SBID:String,JYJG:String,JYRQ:String,JYNR:String,JYJGG:String)
		{
			this.ID=ID;
			this.SBID=SBID;
			this.JYJG=JYJG;
			this.JYRQ=JYRQ;
			this.JYNR=JYNR;
			this.JYJG=JYJG;
			
		}
		public var ID:String;
		public var SBID:String;
		public var JYJG:String;
		public var JYRQ:String;
		public var JYNR:String;
		public var JYJGG:String;

	}
}