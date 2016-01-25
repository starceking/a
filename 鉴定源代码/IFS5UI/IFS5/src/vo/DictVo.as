package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import mx.collections.ArrayList;

	public class DictVo implements ValueObject
	{
		public function DictVo(Name:String,Item:ArrayList)
		{
			this.Name=Name;
			this.Item=Item;
		}
		public var Name:String;
		public var Item:ArrayList;
	}
}