package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	import util.Helper;
	import mx.collections.ArrayList;
	public class EquipmentVo implements ValueObject
	{
		public function EquipmentVo(ID:String,SBYQMC:String,CZSMC:String,XHGG:String,CCBH:String,SBSBH:String,
									JSRQ:String,QYRQ:String,MQCFDD:String,
									JSZT:String,SZSJ:String,SZZQ:String,SYZY:String,GLR:String,WHFS:String,
									YZFS:String,ZP:String,FJ:String,SBLB:String)
		{
			this.ID=ID;
			this.SBYQMC=SBYQMC;
			this.CZSMC=CZSMC;
			this.XHGG=XHGG;
			this.CCBH=CCBH;
			this.SBSBH=SBSBH;
			this.JSRQ=JSRQ;
			this.QYRQ=QYRQ;
			this.MQCFDD=MQCFDD;
			this.JSZT=JSZT;
			this.SZSJ=SZSJ;
			this.SZZQ=SZZQ;
			this.SYZY=SYZY;
			this.GLR=GLR;
			this.WHFS=WHFS;
			this.YZFS=YZFS;
			this.ZP=ZP;
			this.FJ=FJ;
			this.SBLB=SBLB;
		}
		public var ID:String;
		public var SBYQMC:String;
		public var CZSMC:String;
		public var XHGG:String;
		public var CCBH:String;
		public var SBSBH:String;
		public var JSRQ:String;
		public var QYRQ:String;
		public var MQCFDD:String;
		public var JSZT:String;
		public var SZSJ:String;
		public var SZZQ:String;
		public var SYZY:String;
		public var GLR:String;
		public var WHFS:String;
		public var YZFS:String;
		public var ZP:String;
		public var FJ:String;
		public var SBLB:String;
	}
}