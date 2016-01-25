package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import mx.collections.ArrayList;
	
	public class PsbVo implements ValueObject
	{
		public function PsbVo(ID:String,PID:String,PTYPE:String,NUMBER:String,NAME:String,ADDRESS:String,POSTCODE:String,
							  NICKNAME:String,PHONE:String,PSBTYPE:String)
		{
			this.ID=ID;
			this.PID=PID;
			this.PTYPE=PTYPE;
			this.NUMBER=NUMBER;
			this.NAME=NAME;
			this.ADDRESS=ADDRESS;
			this.POSTCODE=POSTCODE;
			this.NICKNAME=NICKNAME;
			this.PHONE=PHONE;
			this.PSBTYPE=PSBTYPE;
		}
		public var ID:String;//ID
		public var PID:String;//父单位
		public var PTYPE:String;//类型
		public var NUMBER:String;//编号
		public var NAME:String;//名称
		public var ADDRESS:String;//地址
		public var POSTCODE:String;//邮编
		public var NICKNAME:String;//简称
		public var PHONE:String;//电话
		public var PSBTYPE:String;//单位类别
	}
}