package vo
{	
	import com.adobe.cairngorm.vo.ValueObject;
	
	import util.Helper;
	
	public class WorkVo
	{
		public function WorkVo(ID:String="",SysUserID:String="",Unit:String="",Place:String="",StartDate:String="",EndDate:String="",
								Section:String="",JobTitle:String="",Remark:String="")
		{
			this.ID=ID;
			this.SysUserID=SysUserID;
			this.Unit=Unit;
			this.Place=Place;
			this.StartDate=Helper.getDateString(StartDate);
			this.EndDate=Helper.getDateString(EndDate);
			this.Section=Section;
			this.JobTitle=JobTitle;
			this.Remark=Remark;
		}
		public var ID:String;
		public var SysUserID:String;
		public var Unit:String;
		public var Place:String;
		public var StartDate:String;
		public var EndDate:String;
		public var Section:String;
		public var JobTitle:String;
		public var Remark:String;
	}
}