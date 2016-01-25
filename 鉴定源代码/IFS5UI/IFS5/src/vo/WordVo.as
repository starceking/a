package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	public class WordVo implements ValueObject
	{
		public function WordVo(WordType:String,FileName:String,TemplatePath:String,WordDir:String,CreationTime:String,
							   LastWriteTime:String,OpenWordUrl:String,CONNO:String)
		{
			this.WordType=WordType;
			this.FileName=FileName;
			this.TemplatePath=TemplatePath;
			this.WordDir=WordDir;
			this.CreationTime=CreationTime;
			this.LastWriteTime=LastWriteTime;
			this.OpenWordUrl=OpenWordUrl;
			this.CONNO=CONNO;
		}
		public var WordType:String;
		public var FileName:String;
		public var TemplatePath:String;
		public var WordDir:String;
		public var CreationTime:String;
		public var LastWriteTime:String;
		public var OpenWordUrl:String;
		public var CONNO:String;
	}
}