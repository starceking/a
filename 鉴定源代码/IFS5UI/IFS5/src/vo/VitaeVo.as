package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import util.Helper;
	
	public class VitaeVo
	{
		public function VitaeVo(ID:String="",SysUserID:String="",Vitaes:String="",StartDate:String="",EndDate:String="",Dept:String="",
							  Section:String="",Remark:String="")
		{
			this.ID=ID;
			this.SysUserID=SysUserID;
			this.Vitaes=Vitaes;
			this.StartDate=Helper.getDateString(StartDate);
			this.EndDate=Helper.getDateString(EndDate);
			this.Dept=Dept;
			this.Section=Section;
			this.Remark=Remark;
		}
		public var ID:String;
		public var SysUserID:String;
		public var Vitaes:String;
		public var StartDate:String;
		public var EndDate:String;
		public var Dept:String;		
		public var Section:String;
		public var Remark:String;
	}
}