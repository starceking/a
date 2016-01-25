package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import util.Helper;
	
	public class ExCaseVo implements ValueObject
	{
		public function ExCaseVo(ID:String,CONNO:String,DFGKNO:String,XCKYNO:String,CASE_NAME:String,CASE_TYPE:String,CASE_NO:String,
								 SCENE_PLACE:String,OCCURRENCE_DATE:String,CASE_PROPERTY:String,CASE_SUMMARY:String,REGIONCODE:String)
		{
			this.ID=ID;
			this.CONNO=CONNO;
			this.DFGKNO=DFGKNO;
			this.XCKYNO=XCKYNO;
			this.CASE_NAME=CASE_NAME;
			this.CASE_TYPE=CASE_TYPE;
			this.SCENE_PLACE=SCENE_PLACE;
			this.OCCURRENCE_DATE=Helper.getDateString(OCCURRENCE_DATE);
			this.CASE_PROPERTY=CASE_PROPERTY;
			this.CASE_SUMMARY=CASE_SUMMARY;
			this.REGIONCODE=REGIONCODE;
		}
		public var ID:String;//ID
		public var CONNO:String;//委托编号
		public var DFGKNO:String;//警综编号
		public var XCKYNO:String;//现勘编号
		public var CASE_NAME:String;//案件名称
		public var CASE_TYPE:String;//案件类型
		public var CASE_NO:String;//案件编号
		public var SCENE_PLACE:String;//案发地点
		public var OCCURRENCE_DATE:String;//案发时间
		public var CASE_PROPERTY:String;//案件性质
		public var CASE_SUMMARY:String;//简要案情
		public var REGIONCODE:String;//区划代码
	}
}