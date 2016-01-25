package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	import util.Helper;
	
	public class ExtractSampleVo implements ValueObject
	{
		public function ExtractSampleVo(ID:String,CASE_ID:String,CONNO:String,SC:String,
								  SLN:String,NAME:String,SAMPLE_TYPE:String,
								  TQ_METHOD:String,CH_METHOD:String,YL:String,TQ_ID:String,CH_ID:String,WZ:String,
								  TQR:String,TQRQ:String,KZJS:String,DYJS:String,SampleSheet:String,RunFold:String,
								  KZTX:String,
								  CHLXJ:String,CHYYQ:String,CHJRY:String,CHHWHYY:String,
								  CHXWHHQ:String,CHSY:String,CHXWJ:String,
								  CHGZZ:String,GZZMS_ID:String,GZZMS:String,TQQR:String,KZDY_ID:String,TQPH:String,KZDYPH:String,
								  TQTJYL:String,CHTJYL:String)
		{
			this.ID=ID;
			this.CASE_ID=CASE_ID;
			this.CONNO=CONNO;
			this.SLN=SLN;
			this.NAME=NAME;
			this.SC=SC;
			this.SAMPLE_TYPE=SAMPLE_TYPE;
			this.TQ_METHOD=TQ_METHOD;
			this.CH_METHOD=CH_METHOD;
			this.YL=YL;
			this.TQ_ID=TQ_ID;
			this.CH_ID=CH_ID;
			this.WZ=WZ;
			
			this.TQR=TQR;
			this.TQRQ=Helper.getDateString(TQRQ);
			this.KZJS=KZJS;
			this.DYJS=DYJS;
			this.SampleSheet=SampleSheet;
			this.RunFold=RunFold;
			
			this.KZTX=KZTX;
			
			this.CHLXJ=CHLXJ;
			this.CHYYQ=CHYYQ;
			this.CHJRY=CHJRY;
			this.CHHWHYY=CHHWHYY;
			this.CHXWHHQ=CHXWHHQ;
			this.CHSY=CHSY;
			this.CHXWJ=CHXWJ;
			this.CHGZZ=CHGZZ;
			this.GZZMS_ID=GZZMS_ID;
			this.GZZMS=GZZMS;
			this.TQQR=TQQR;
			this.KZDY_ID=KZDY_ID;
			this.TQPH=TQPH;
			this.KZDYPH=KZDYPH;
			this.TQTJYL=TQTJYL;
			this.CHTJYL=CHTJYL;
		}
		public var ID:String;//ID
		public var CASE_ID:String;//案件ID
		public var CONNO:String;//委托编号
		public var SLN:String;//样本编号
		public var NAME:String;//样本名称
		public var SC:String;//库类型
		public var SAMPLE_TYPE:String;//样本类型
		public var TQ_METHOD:String;//提取方法
		public var CH_METHOD:String;//纯化方法
		public var YL:String;//模板用量
		public var TQ_ID:String;//提取记录ID
		public var CH_ID:String;//纯化记录ID
		public var WZ:String;//电泳位置
		
		public var TQR:String;//提取人
		public var TQRQ:String;//日期
		public var KZJS:String;//扩增时间结束
		public var DYJS:String;//检测时间结束
		public var SampleSheet:String;//SampleSheet
		public var RunFold:String;//RunFold
		
		public var KZTX:String;//扩增体系
		
		public var CHLXJ:String;//纯化离心机
		public var CHYYQ:String;//纯化移液器
		public var CHJRY:String;//纯化加热仪
		public var CHHWHYY:String;//纯化恒温混匀仪
		public var CHXWHHQ:String;//纯化漩涡混合器
		public var CHSY:String;//纯化水浴
		public var CHXWJ:String;//纯化显微镜
		public var CHGZZ:String;//纯化工作站
		public var GZZMS_ID:String;//工作站模式ID
		public var GZZMS:String;//工作站模式
		public var TQQR:String;//提取确认
		public var KZDY_ID:String;//扩增电泳ID
		public var TQPH:String;//提取批号
		public var KZDYPH:String;//扩增电泳批号
		public var TQTJYL:String;//提取体积用量
		public var CHTJYL:String;//纯化体积用量
	}
}
