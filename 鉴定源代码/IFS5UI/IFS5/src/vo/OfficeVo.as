package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import mx.collections.ArrayList;

	public class OfficeVo implements ValueObject
	{
		public function OfficeVo(Name:String,Leader:String,PlanDate:String,officeTypeList:ArrayList)
		{
			this.Name=Name;
			this.Leader=Leader;
			this.PlanDate=PlanDate;
			this.officeTypeList=officeTypeList;
		}
		public var Name:String;
		public var Leader:String;
		public var PlanDate:String;
		public var officeTypeList:ArrayList;
	}
}