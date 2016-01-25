package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	import util.Helper;
	import mx.collections.ArrayList;
	public class EquipmentRepairVo implements ValueObject
	{
		public function EquipmentRepairVo(ID:String,SBID:String,FZR:String,XLSJ:String,SLYY:String,XXMS:String)
		{
			this.ID=ID;
			this.SBID=SBID;
			this.FZR=FZR;
			this.XLSJ=XLSJ;
			this.SLYY=SLYY;
			this.XXMS=XXMS;
			
		}
		public var ID:String;
		public var SBID:String;
		public var FZR:String;
		public var XLSJ:String;
		public var SLYY:String;
		public var XXMS:String;
	}
}