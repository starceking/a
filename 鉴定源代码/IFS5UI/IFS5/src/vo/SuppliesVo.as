package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	import util.Helper;
	import mx.collections.ArrayList;
	public class SuppliesVo  implements ValueObject
	{
	  public function SuppliesVo(ID:String,CPMC:String,BM:String,GG:String,SCCJ:String,BH:String,
								   YT:String,BCTJ:String,BZ:String,KCL:String,CFDD:String,YXQ:String,BGR:String)
		{
			this.ID=ID;
			this.CPMC=CPMC;
			this.BM=BM;
			this.GG=GG;
			this.SCCJ=SCCJ;
			this.BH=BH;
			this.YT=YT;
			this.BCTJ=BCTJ;
			this.BZ=BZ;
			this.KCL=KCL;
			this.CFDD=CFDD;
			this.YXQ=Helper.getDateString(YXQ);
			this.BGR=BGR;
			
		}
		public var ID:String;
		public var CPMC:String;
		public var BM:String;
		public var GG:String;
		public var SCCJ:String;
		public var BH:String;
		public var BZ:String;
		public var YT:String;
		public var BCTJ:String;
		public var KCL:String;
		public var CFDD:String;
		public var YXQ:String;
		public var BGR:String;
	}
}