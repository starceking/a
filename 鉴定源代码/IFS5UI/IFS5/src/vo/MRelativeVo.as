package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	import util.Helper;	
	
	public class MRelativeVo implements ValueObject
	{
		public function MRelativeVo(ID:String,CONNO:String,
									   RELATION:String,RELATIVE1_ID:String,RELATIVE2_ID:String,PERSON_BIRTHDATE:String,PERSON_GENDER:String,
									   PERSON_SPEC:String,PERSON_SIGN:String,PERSON_NAME:String,CASE_NAME:String,CASE_SUMMARY:String,									   
									   
									   R1_NAME:String,R1_SAMPLE_TYPE:String,R1_GENDER:String,R1_PERSONNEL_TYPE:String,R1_BIRTH_DATE:String,
									   R1_NATIONALITY:String,R1_DISTRICT:String,R1_ID_CARD_NO:String,R1_EDUCATION_LEVEL:String,R1_IDENTITY:String,
									   R1_NATIVE_PLACE_ADDR:String,R1_RESIDENCE_ADDR:String,R1_SAMPLE_PACKAGING:String,R1_SAMPLE_DESCRIPTION:String,
									   R1_REMARK:String,R1_RELATION_WITH_TARGET:String,R1_SLN:String,
									   
									   R2_NAME:String,R2_SAMPLE_TYPE:String,R2_GENDER:String,R2_PERSONNEL_TYPE:String,R2_BIRTH_DATE:String,
									   R2_NATIONALITY:String,R2_DISTRICT:String,R2_ID_CARD_NO:String,R2_EDUCATION_LEVEL:String,R2_IDENTITY:String,
									   R2_NATIVE_PLACE_ADDR:String,R2_RESIDENCE_ADDR:String,R2_SAMPLE_PACKAGING:String,R2_SAMPLE_DESCRIPTION:String,
									   R2_REMARK:String,R2_RELATION_WITH_TARGET:String,R2_SLN:String,ORA_FLAG:String)
		{
			this.ID=ID;
			this.CONNO=CONNO;
			this.RELATION=RELATION;
			this.RELATIVE1_ID=RELATIVE1_ID;
			this.RELATIVE2_ID=RELATIVE2_ID;
			this.PERSON_BIRTHDATE=Helper.getDateString(PERSON_BIRTHDATE);
			this.PERSON_GENDER=PERSON_GENDER;
			this.PERSON_SPEC=PERSON_SPEC;
			this.PERSON_SIGN=PERSON_SIGN;
			this.PERSON_NAME=PERSON_NAME;
			this.CASE_NAME=CASE_NAME;
			this.CASE_SUMMARY=CASE_SUMMARY;
			
			this.R1_NAME=R1_NAME;
			this.R1_SAMPLE_TYPE=R1_SAMPLE_TYPE;
			this.R1_GENDER=R1_GENDER;
			this.R1_PERSONNEL_TYPE=R1_PERSONNEL_TYPE;
			this.R1_BIRTH_DATE=Helper.getDateString(R1_BIRTH_DATE);
			this.R1_NATIONALITY=R1_NATIONALITY;
			this.R1_DISTRICT=R1_DISTRICT;
			this.R1_ID_CARD_NO=R1_ID_CARD_NO;
			this.R1_EDUCATION_LEVEL=R1_EDUCATION_LEVEL;
			this.R1_IDENTITY=R1_IDENTITY;
			this.R1_NATIVE_PLACE_ADDR=R1_NATIVE_PLACE_ADDR;
			this.R1_RESIDENCE_ADDR=R1_RESIDENCE_ADDR;
			this.R1_SAMPLE_PACKAGING=R1_SAMPLE_PACKAGING;
			this.R1_SAMPLE_DESCRIPTION=R1_SAMPLE_DESCRIPTION;
			this.R1_REMARK=R1_REMARK;
			this.R1_RELATION_WITH_TARGET=R1_RELATION_WITH_TARGET;
			this.R1_SLN=R1_SLN;
			
			this.R2_NAME=R2_NAME;
			this.R2_SAMPLE_TYPE=R2_SAMPLE_TYPE;
			this.R2_GENDER=R2_GENDER;
			this.R2_PERSONNEL_TYPE=R2_PERSONNEL_TYPE;
			this.R2_BIRTH_DATE=Helper.getDateString(R2_BIRTH_DATE);
			this.R2_NATIONALITY=R2_NATIONALITY;
			this.R2_DISTRICT=R2_DISTRICT;
			this.R2_ID_CARD_NO=R2_ID_CARD_NO;
			this.R2_EDUCATION_LEVEL=R2_EDUCATION_LEVEL;
			this.R2_IDENTITY=R2_IDENTITY;
			this.R2_NATIVE_PLACE_ADDR=R2_NATIVE_PLACE_ADDR;
			this.R2_RESIDENCE_ADDR=R2_RESIDENCE_ADDR;
			this.R2_SAMPLE_PACKAGING=R2_SAMPLE_PACKAGING;
			this.R2_SAMPLE_DESCRIPTION=R2_SAMPLE_DESCRIPTION;
			this.R2_REMARK=R2_REMARK;
			this.R2_RELATION_WITH_TARGET=R2_RELATION_WITH_TARGET;
			this.R2_SLN=R2_SLN;
			
			this.ORA_FLAG=ORA_FLAG;
		}
		public var ID:String;
		public var CONNO:String;
		public var RELATION:String;
		public var RELATIVE1_ID:String;
		public var RELATIVE2_ID:String;
		public var PERSON_BIRTHDATE:String;
		public var PERSON_GENDER:String;
		public var PERSON_SPEC:String;
		public var PERSON_SIGN:String;
		public var PERSON_NAME:String;
		public var CASE_NAME:String;
		public var CASE_SUMMARY:String;
		
		public var R1_NAME:String;
		public var R1_SAMPLE_TYPE:String;
		public var R1_GENDER:String;
		public var R1_PERSONNEL_TYPE:String;
		public var R1_BIRTH_DATE:String;
		public var R1_NATIONALITY:String;
		public var R1_DISTRICT:String;
		public var R1_ID_CARD_NO:String;
		public var R1_EDUCATION_LEVEL:String;
		public var R1_IDENTITY:String;
		public var R1_NATIVE_PLACE_ADDR:String;
		public var R1_RESIDENCE_ADDR:String;
		public var R1_SAMPLE_PACKAGING:String;
		public var R1_SAMPLE_DESCRIPTION:String;
		public var R1_REMARK:String;
		public var R1_RELATION_WITH_TARGET:String;
		public var R1_SLN:String;
		
		public var R2_NAME:String;
		public var R2_SAMPLE_TYPE:String;
		public var R2_GENDER:String;
		public var R2_PERSONNEL_TYPE:String;
		public var R2_BIRTH_DATE:String;
		public var R2_NATIONALITY:String;
		public var R2_DISTRICT:String;
		public var R2_ID_CARD_NO:String;
		public var R2_EDUCATION_LEVEL:String;
		public var R2_IDENTITY:String;
		public var R2_NATIVE_PLACE_ADDR:String;
		public var R2_RESIDENCE_ADDR:String;
		public var R2_SAMPLE_PACKAGING:String;
		public var R2_SAMPLE_DESCRIPTION:String;
		public var R2_REMARK:String;
		public var R2_RELATION_WITH_TARGET:String;
		public var R2_SLN:String;
		
		public var ORA_FLAG:String;
	}
}