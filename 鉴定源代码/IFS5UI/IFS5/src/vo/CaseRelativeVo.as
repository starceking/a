package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import util.Helper;	
	
	public class CaseRelativeVo implements ValueObject
	{
		public function CaseRelativeVo(ID:String,CASE_ID:String,CONNO:String,SAMPLE_CATEGORY:String,
									   RELATION:String,RELATIVE1_ID:String,RELATIVE2_ID:String,PERSONE_NAME:String,
									   R1_NAME:String,R1_SAMPLE_TYPE:String,R1_GENDER:String,R1_ID_CARD_NO:String,R1_NATIVE_PLACE_ADDR:String,
									   R1_SAMPLE_DESCRIPTION:String,R1_RELATION_WITH_TARGET:String,R1_SLN:String,
									   R2_NAME:String,R2_SAMPLE_TYPE:String,R2_GENDER:String,R2_ID_CARD_NO:String,R2_NATIVE_PLACE_ADDR:String,
									   R2_SAMPLE_DESCRIPTION:String,R2_RELATION_WITH_TARGET:String,R2_SLN:String,ORA_FLAG:String)
		{
			this.ID=ID;
			this.CASE_ID=CASE_ID;
			this.CONNO=CONNO;
			this.SAMPLE_CATEGORY=SAMPLE_CATEGORY;
			this.RELATION=RELATION;
			this.RELATIVE1_ID=RELATIVE1_ID;
			this.RELATIVE2_ID=RELATIVE2_ID;
			this.PERSONE_NAME=PERSONE_NAME;
			
			this.R1_NAME=R1_NAME;
			this.R1_SAMPLE_TYPE=R1_SAMPLE_TYPE;
			this.R1_GENDER=R1_GENDER;
			this.R1_ID_CARD_NO=R1_ID_CARD_NO;
			this.R1_NATIVE_PLACE_ADDR=R1_NATIVE_PLACE_ADDR;
			this.R1_SAMPLE_DESCRIPTION=R1_SAMPLE_DESCRIPTION;
			this.R1_RELATION_WITH_TARGET=R1_RELATION_WITH_TARGET;
			this.R1_SLN=R1_SLN;
		
			this.R2_NAME=R2_NAME;
			this.R2_SAMPLE_TYPE=R2_SAMPLE_TYPE;
			this.R2_GENDER=R2_GENDER;
			this.R2_ID_CARD_NO=R2_ID_CARD_NO;
			this.R2_NATIVE_PLACE_ADDR=R2_NATIVE_PLACE_ADDR;
			this.R2_SAMPLE_DESCRIPTION=R2_SAMPLE_DESCRIPTION;
			this.R2_RELATION_WITH_TARGET=R2_RELATION_WITH_TARGET;
			this.R2_SLN=R2_SLN;
			
			this.ORA_FLAG=ORA_FLAG;
		}
		public var ID:String;
		public var CASE_ID:String;
		public var CONNO:String;
		public var SAMPLE_CATEGORY:String;
		public var RELATION:String;
		public var RELATIVE1_ID:String;
		public var RELATIVE2_ID:String;
		public var PERSONE_NAME:String;
		
		public var R1_NAME:String;
		public var R1_SAMPLE_TYPE:String;
		public var R1_GENDER:String;
		public var R1_ID_CARD_NO:String;
		public var R1_NATIVE_PLACE_ADDR:String;
		public var R1_SAMPLE_DESCRIPTION:String;
		public var R1_RELATION_WITH_TARGET:String;
		public var R1_SLN:String;
		
		public var R2_NAME:String;
		public var R2_SAMPLE_TYPE:String;
		public var R2_GENDER:String;
		public var R2_ID_CARD_NO:String;
		public var R2_NATIVE_PLACE_ADDR:String;
		public var R2_SAMPLE_DESCRIPTION:String;
		public var R2_RELATION_WITH_TARGET:String;
		public var R2_SLN:String;
		
		public var ORA_FLAG:String;
	}
}