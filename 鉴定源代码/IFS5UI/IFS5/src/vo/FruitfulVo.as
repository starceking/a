package vo
{

	import com.adobe.cairngorm.vo.ValueObject;
	
	import util.Helper;
	
	public class FruitfulVo
	{
		public function FruitfulVo(ID:String="",SysUserID:String="",ProjectName:String="",AwardDate:String="",AwardUnit:String="",
								   RewardName:String="",AwardRank:String="",Remark:String="")
		{
			this.ID=ID;
			this.SysUserID=SysUserID;
			this.ProjectName=ProjectName;
			this.AwardDate=Helper.getDateString(AwardDate);
			this.AwardUnit=AwardUnit;
			this.RewardName=RewardName;
			this.AwardRank=AwardRank;
			this.Remark=Remark;
		}
		public var ID:String;
		public var SysUserID:String;
		public var ProjectName:String;
		public var AwardDate:String;
		public var AwardUnit:String;
		public var RewardName:String;
		public var AwardRank:String;
		public var Remark:String;
	}
}