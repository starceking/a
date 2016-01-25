package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	public class OfficeJusTypeVo implements ValueObject
	{
		public function OfficeJusTypeVo(Id:String,Name:String,DocName:String,TESNAME:String,JUSITEM:String,
										IDREQ:String,SESLN:String,CONCLUSION:String)
		{
			this.Id=Id;
			this.Name=Name;
			this.DocName=DocName;
			this.JUSITEM=JUSITEM;
			this.IDREQ=IDREQ;
			this.SESLN=SESLN;
			this.CONCLUSION=CONCLUSION;
			this.TESNAME=TESNAME;
		}
		public var Id:String;
		public var Name:String;
		public var DocName:String;
		public var JUSITEM:String;
		public var IDREQ:String;
		public var SESLN:String;
		public var CONCLUSION:String;
		public var TESNAME:String;
	}
}