package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import util.Helper;	
	
	public class IdPersonVo implements ValueObject
	{
		public function IdPersonVo(ID:String,CONNO:String,NAME:String,GENDER:String,IDCARDNO:String,PHONE:String,BIRTH_DATE:String,
								   AGE:String,JOB:String,EDUCATION:String,NATIVE_PLACE:String,WORK_PLACE:String,RESIDENCE_PLACE:String)
		{
			this.ID=ID;
			this.CONNO=CONNO;
			this.NAME=NAME;
			this.GENDER=GENDER;
			this.IDCARDNO=IDCARDNO;
			this.PHONE=PHONE;
			this.BIRTH_DATE=BIRTH_DATE;
			this.AGE=AGE;
			this.JOB=JOB;
			this.EDUCATION=EDUCATION;
			this.NATIVE_PLACE=NATIVE_PLACE;
			this.WORK_PLACE=WORK_PLACE;
			this.RESIDENCE_PLACE=RESIDENCE_PLACE;
		}
		public var ID:String;
		public var CONNO:String;
		public var NAME:String;
		public var GENDER:String;
		public var IDCARDNO:String;
		public var PHONE:String;
		public var BIRTH_DATE:String;
		public var AGE:String;
		public var JOB:String;
		public var EDUCATION:String;
		public var NATIVE_PLACE:String;
		public var WORK_PLACE:String;
		public var RESIDENCE_PLACE:String;
	}
}