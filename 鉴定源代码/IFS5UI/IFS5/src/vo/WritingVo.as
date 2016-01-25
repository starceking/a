package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import util.Helper;
	
	public class WritingVo
	{
		public function WritingVo(ID:String="",SysUserID:String="",Title:String="",Publishing:String="",Date:String="",
								  Workload:String="",Remark:String="")
		{
			this.ID=ID;
			this.SysUserID=SysUserID;
			this.Title=Title;
			this.Publishing=Publishing;
			this.Date=Helper.getDateString(Date);
			this.Workload=Workload;
			this.Remark=Remark;
		}
		public var ID:String;
		public var SysUserID:String;
		public var Title:String;
		public var Publishing:String;
		public var Date:String;
		public var Workload:String;
		public var Remark:String;
	}
}