package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import util.Helper;
	public class ServiceTrainVo implements ValueObject
	{
		public function ServiceTrainVo(ID:String,PersonID:String,TeacherName:String,Office:String,ZC:String,StuName:String,PSB:String,
									   Degree:String,StuTime:String,StuAim:String,TeachText:String,StuPGJG:String,KPYJ:String,ZYPerson:String,JLTime:String)
		{
			this.ID=ID;
			this.PersonID=PersonID;
			this.TeacherName=TeacherName;
			this.Office=Office;
			this.ZC=ZC;
			this.StuName=StuName;
			this.PSB=PSB;
			this.Degree=Degree;
			this.StuTime=Helper.getDateString(StuTime);
			this.StuAim=StuAim;
			this.TeachText=TeachText;
			this.StuPGJG=StuPGJG;
			this.KPYJ=KPYJ;
			this.ZYPerson=ZYPerson;
			this.JLTime=Helper.getDateString(JLTime);
		}
		public var ID:String;
		public var PersonID:String;
		public var TeacherName:String;
		public var Office:String;
		public var ZC:String;
		public var StuName:String;
		public var PSB:String;
		public var Degree:String;
		public var StuTime:String;
		public var StuAim:String;
		public var TeachText:String;
		public var StuPGJG:String;
		public var KPYJ:String;
		public var ZYPerson:String;
		public var JLTime:String;
	}
}