package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import util.Helper;	
	
	public class CasePersonnelSampleVo implements ValueObject
	{
		public function CasePersonnelSampleVo(ID:String,CASE_ID:String,CONNO:String,SAMPLE_CATEGORY:String,NAME:String,SAMPLE_TYPE:String,
											  GENDER:String,PERSONNEL_TYPE:String,BIRTH_DATE:String,
											  NATIONALITY:String,DISTRICT:String,ID_CARD_NO:String,EDUCATION_LEVEL:String,IDENTITY:String,
											  NATIVE_PLACE_ADDR:String,RESIDENCE_ADDR:String,SAMPLE_PACKAGING:String,SAMPLE_DESCRIPTION:String,
											  REMARK:String,SLN:String,ORA_FLAG:String)
		{
			this.ID=ID;
			this.CASE_ID=CASE_ID;
			this.CONNO=CONNO;
			this.SAMPLE_CATEGORY=SAMPLE_CATEGORY;
			this.NAME=NAME;
			this.SAMPLE_TYPE=SAMPLE_TYPE;
			this.GENDER=GENDER;
			this.PERSONNEL_TYPE=PERSONNEL_TYPE;
			this.BIRTH_DATE=Helper.getDateString(BIRTH_DATE);
			this.NATIONALITY=NATIONALITY;
			this.DISTRICT=DISTRICT;
			this.ID_CARD_NO=ID_CARD_NO;
			this.EDUCATION_LEVEL=EDUCATION_LEVEL;
			this.IDENTITY=IDENTITY;
			this.NATIVE_PLACE_ADDR=NATIVE_PLACE_ADDR;
			this.RESIDENCE_ADDR=RESIDENCE_ADDR;
			this.SAMPLE_PACKAGING=SAMPLE_PACKAGING;
			this.SAMPLE_DESCRIPTION=SAMPLE_DESCRIPTION;
			this.REMARK=REMARK;
			this.SLN=SLN;
			this.ORA_FLAG=ORA_FLAG;
		}
		public var ID:String;
		public var CASE_ID:String;
		public var CONNO:String;
		public var SAMPLE_CATEGORY:String;
		public var NAME:String;
		public var SAMPLE_TYPE:String;
		public var GENDER:String;
		public var PERSONNEL_TYPE:String;
		public var BIRTH_DATE:String;
		public var NATIONALITY:String;
		public var DISTRICT:String;
		public var ID_CARD_NO:String;
		public var EDUCATION_LEVEL:String;
		public var IDENTITY:String;
		public var NATIVE_PLACE_ADDR:String;
		public var RESIDENCE_ADDR:String;
		public var SAMPLE_PACKAGING:String;
		public var SAMPLE_DESCRIPTION:String;
		public var REMARK:String;
		public var SLN:String;
		public var ORA_FLAG:String;
	}
}