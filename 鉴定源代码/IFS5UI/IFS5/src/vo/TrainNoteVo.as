package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import util.Helper;
	
	public class TrainNoteVo implements ValueObject
	{
		public function TrainNoteVo(ID:String,PersonID:String,Name:String,Office:String,TrainXM:String,TrainTeacher:String,TrainTime:String,
									TrainMD:String,TrainGC:String,TrainXGPJ:String,JLR:String,JLTime:String,PXJLID:String)
		{
			this.ID=ID;
			this.PersonID=PersonID;
			this.Name=Name;
			this.Office=Office;
			this.TrainXM=TrainXM;
			this.TrainTeacher=TrainTeacher;
			this.TrainTime=Helper.getDateString(TrainTime);
			this.TrainMD=TrainMD;
			this.TrainGC=TrainGC;
			this.TrainXGPJ=TrainXGPJ;
			this.JLR=JLR;
			this.JLTime=Helper.getDateString(JLTime);
			this.PXJLID=PXJLID;
		}
		public var ID:String;
		public var PersonID:String;
		public var Name:String;
		public var Office:String;
		public var TrainXM:String;
		public var TrainTeacher:String;
		public var TrainTime:String;
		public var TrainMD:String;
		public var TrainGC:String;
		public var TrainXGPJ:String;
		public var JLR:String;
		public var JLTime:String;
		public var PXJLID:String;
	}
}