package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	public class RegionCodeVo implements ValueObject
	{
		public function RegionCodeVo(ID:String,Code:String,RegionName:String,RegionType:String)
		{
			this.ID=ID;
			this.Code=Code;
			this.RegionName=RegionName;
			this.RegionType=RegionType;
		}
		public var ID:String;//ID
		public var Code:String;//区划代码
		public var RegionName:String;//区划名称
		public var RegionType:String;//类型
	}
}