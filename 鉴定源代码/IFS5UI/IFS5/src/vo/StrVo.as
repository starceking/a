package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	
	public class StrVo implements ValueObject
	{
		public function StrVo(ID:String,CASE_ID:String,CONNO:String,SAMPLE_ID:String,SLN:String,NAME:String,SC:String,
							  AMEL:String,D8S1179:String,D21S11:String,D18S51:String,
							  vWA:String,D3S1358:String,FGA:String,TH01:String,D5S818:String,D13S317:String,
							  D7S820:String,CSF1PO:String,D16S539:String,TPOX:String,D2S1338:String,D19S433:String,Penta_D:String,
							  Penta_E:String,D6S1043:String,F13A01:String,FESFPS:String,D1S80:String,D12S391:String,D1S1656:String,
							  D2S441:String, D22S1045:String,SE33:String,D10S1248:String,Y_indel:String,
							  B_DYS456:String,B_DYS389I:String,B_DYS390:String,B_DYS389II:String,G_DYS458:String,G_DYS19:String,
							  G_DYS385:String,Y_DYS393:String,Y_DYS391:String,Y_DYS439:String,Y_DYS635:String,Y_DYS392:String,
							  R_Y_GATA_H4:String,R_DYS437:String,R_DYS438:String,R_DYS448:String,
							  BZ_NUM:String,IMP_FLAG:String, STR_FLAG:String, YSTR_FLAG:String,SAMPLE_TYPE:String)
		{
			this.ID=ID;
			this.CASE_ID=CASE_ID;
			this.CONNO=CONNO;
			this.SAMPLE_ID=SAMPLE_ID;
			this.SLN=SLN;
			this.NAME=NAME;
			this.SC=SC;
			this.AMEL=AMEL;
			this.D8S1179=D8S1179;
			this.D21S11=D21S11;
			this.D18S51=D18S51;
			this.vWA=vWA;
			this.D3S1358=D3S1358;
			this.FGA=FGA;
			this.TH01=TH01;
			this.D5S818=D5S818;
			this.D13S317=D13S317;
			this.D7S820=D7S820;
			this.CSF1PO=CSF1PO;
			this.D16S539=D16S539;
			this.TPOX=TPOX;
			this.D2S1338=D2S1338;
			this.D19S433=D19S433;
			this.Penta_D=Penta_D;
			this.Penta_E=Penta_E;
			this.D6S1043=D6S1043;
			this.F13A01=F13A01;
			this.FESFPS=FESFPS;
			this.D1S80=D1S80;
			this.D12S391=D12S391;
			this.D1S1656=D1S1656;
			
			this.D2S441=D2S441;
			this.D22S1045=D22S1045;
			this.SE33=SE33;
			this.D10S1248=D10S1248;
			this.Y_indel=Y_indel;
			
			this.B_DYS456=B_DYS456;
			this.B_DYS389I=B_DYS389I;
			this.B_DYS390=B_DYS390;
			this.B_DYS389II=B_DYS389II;
			this.G_DYS458=G_DYS458;
			this.G_DYS19=G_DYS19;
			this.G_DYS385=G_DYS385;
			this.Y_DYS393=Y_DYS393;
			this.Y_DYS391=Y_DYS391;
			this.Y_DYS439=Y_DYS439;
			this.Y_DYS635=Y_DYS635;
			this.Y_DYS392=Y_DYS392;
			this.R_Y_GATA_H4=R_Y_GATA_H4;
			this.R_DYS437=R_DYS437;
			this.R_DYS438=R_DYS438;
			this.R_DYS448=R_DYS448;
			
			this.BZ_NUM=BZ_NUM;
			this.IMP_FLAG=(IMP_FLAG=="1"?"已导入":"未导入");
			this.STR_FLAG=(STR_FLAG=="1"?"有":"没有");
			this.YSTR_FLAG=(YSTR_FLAG=="1"?"有":"没有");
			
			this.SAMPLE_TYPE=SAMPLE_TYPE;
		}
		public var ID:String;
		public var CASE_ID:String;
		public var CONNO:String;
		public var SAMPLE_ID:String;
		public var SLN:String;
		public var NAME:String;
		public var SC:String;
		public var AMEL:String;
		public var D8S1179:String;
		public var D21S11:String;
		public var D18S51:String;
		public var vWA:String;
		public var D3S1358:String;
		public var FGA:String;
		public var TH01:String;
		public var D5S818:String;
		public var D13S317:String;
		public var D7S820:String;
		public var CSF1PO:String;
		public var D16S539:String;
		public var TPOX:String;
		public var D2S1338:String;
		public var D19S433:String;
		public var Penta_D:String;
		public var Penta_E:String;
		public var D6S1043:String;
		public var F13A01:String;
		public var FESFPS:String;
		public var D1S80:String;
		public var D12S391:String;
		public var D1S1656:String;
		
		public var D2S441:String;
		public var D22S1045:String;
		public var SE33:String;
		public var D10S1248:String;
		public var Y_indel:String;
		
		public var B_DYS456:String;
		public var B_DYS389I:String;
		public var B_DYS390:String;
		public var B_DYS389II:String;
		public var G_DYS458:String;
		public var G_DYS19:String;
		public var G_DYS385:String;
		public var Y_DYS393:String;
		public var Y_DYS391:String;
		public var Y_DYS439:String;
		public var Y_DYS635:String;
		public var Y_DYS392:String;
		public var R_Y_GATA_H4:String;
		public var R_DYS437:String;
		public var R_DYS438:String;
		public var R_DYS448:String;
		
		public var BZ_NUM:String;
		public var IMP_FLAG:String;
		public var STR_FLAG:String;
		public var YSTR_FLAG:String;
		
		public var SAMPLE_TYPE:String;
	}
}