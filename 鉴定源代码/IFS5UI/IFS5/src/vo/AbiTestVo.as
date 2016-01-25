package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import util.Helper;
	public class AbiTestVo implements ValueObject
	{
		public function AbiTestVo(ID:String,PersonID:String,Name:String,Office:String,ZC:String,TestType:String,TestTime:String,
								  TestText:String,TestJG:String,TestPJ:String,TestPerson:String,KPTime:String,Remack:String)
		{
			this.ID=ID;
			this.PersonID=PersonID;
			this.Name=Name;
			this.Office=Office;
			this.ZC=ZC;
			this.TestType=TestType;
			this.TestTime=Helper.getDateString(TestTime);
			this.TestText=TestText;
			this.TestJG=TestJG;
			this.TestPJ=TestPJ;
			this.TestPerson=TestPerson;
			this.KPTime=Helper.getDateString(KPTime);
			this.Remack=Remack;
		}
		public var ID:String;
		public var PersonID:String;
		public var Name:String;
		public var Office:String;
		public var ZC:String;
		public var TestType:String;
		public var TestTime:String;
		public var TestText:String;
		public var TestJG:String;
		public var TestPJ:String;
		public var TestPerson:String;
		public var KPTime:String;
		public var Remack:String;
		
	}
}