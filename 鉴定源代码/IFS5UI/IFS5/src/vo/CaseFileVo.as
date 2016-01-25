package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	public class CaseFileVo implements ValueObject
	{
		public function CaseFileVo(FileName:String,Url:String,DiskPath:String)
		{
			this.FileName=FileName;
			this.Url=Url;
			this.DiskPath=DiskPath;
		}
		public var FileName:String;
		public var Url:String;
		public var DiskPath:String;
	}
}