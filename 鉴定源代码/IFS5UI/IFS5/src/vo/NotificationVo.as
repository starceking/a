package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import util.Helper;
	
	public class NotificationVo implements ValueObject
	{
		public function NotificationVo(ID:String="",TITLE:String="",CONTENT_TEXT:String="",IMPORTANT:String="",
									   CREATOR:String="",CREATE_DATETIME:String="")
		{
			this.ID=ID;
			this.TITLE=TITLE;
			this.CONTENT_TEXT=CONTENT_TEXT;
			this.IMPORTANT=IMPORTANT;
			this.CREATE_DATETIME=Helper.getDateString(CREATE_DATETIME,"YYYY-MM-DD");
			this.CREATOR=CREATOR;
		}
		public var ID:String;
		public var TITLE:String;
		public var CONTENT_TEXT:String;
		public var IMPORTANT:String;
		public var CREATE_DATETIME:String;
		public var CREATOR:String;
	}
}