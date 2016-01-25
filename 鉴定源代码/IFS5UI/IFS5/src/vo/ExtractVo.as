package vo
{
	import com.adobe.cairngorm.vo.ValueObject;
	import locator.SysUserLocator;
	import util.Helper;
	
	public class ExtractVo implements ValueObject
	{		
		public function ExtractVo(ID:String,LXJ:String,YYQ:String,JRY:String,HWHYY:String,
								  XWHHQ:String,SY:String,XWJ:String,
								  GZZ:String,TQR:String,RQ:String,TQFF:String,CHFF:String,
		
								  KZY:String,KZLXJ:String,KZXWHHQ:String,KZYYQ:String,KZCJT:String,
								  KZGZZ:String,KZSJKS:String,KZSJJS:String,
								  KZFF:String,SJHPH:String,KZTX:String,
								  KZMB:String,XHS:String,HJWD:String,HJSD:String,
		
								  DYY:String,DYLXJ:String,DYJRY:String,DYXWHHQ:String,DYYYQ:String,
								  DYZBJ:String,DYCJT:String,DYGZZ:String,
								  NB:String,NBL:String,JCSJKS:String,
								  JCSJJS:String,ZKYB:String,BXRJ:String,RJL:String,CWL:String,
								  JY:String,JYPH:String,YDYDL:String,DYDL:String,SampleSheet:String,
								  RunFold:String,DYHJWD:String,DYHJSD:String,
								  JDDW:String,TQPH:String,KZDYPH:String,ZKYBWZ:String,TQQR:String,KZDYQR:String,
								  DYTQ_ID:String,DYKZ_ID:String)
		{
			this.ID=ID;
			this.LXJ=LXJ;
			this.YYQ=YYQ;
			this.JRY=JRY;
			this.HWHYY=HWHYY;
			this.XWHHQ=XWHHQ;
			this.SY=SY;
			this.XWJ=XWJ;
			this.GZZ=GZZ;
			this.TQR=TQR;
			this.RQ=Helper.getDateString(RQ);
			this.TQR_NAME=SysUserLocator.getInstance().getUserName(TQR);
			this.TQFF=TQFF;
			this.CHFF=CHFF;
			
			this.KZY=KZY;
			this.KZLXJ=KZLXJ;
			this.KZXWHHQ=KZXWHHQ;
			this.KZYYQ=KZYYQ;
			this.KZCJT=KZCJT;
			this.KZGZZ=KZGZZ;
			this.KZSJKS=KZSJKS;
			this.KZSJJS=KZSJJS;
			this.KZFF=KZFF;
			this.SJHPH=SJHPH;
			this.KZTX=KZTX;
			this.KZMB=KZMB;
			this.XHS=XHS;
			this.HJWD=HJWD;
			this.HJSD=HJSD;
			
			this.DYY=DYY;
			this.DYLXJ=DYLXJ;
			this.DYJRY=DYJRY;
			this.DYXWHHQ=DYXWHHQ;
			this.DYYYQ=DYYYQ;
			this.DYZBJ=DYZBJ;
			this.DYCJT=DYCJT;
			this.DYGZZ=DYGZZ;
			this.NB=NB;
			this.NBL=NBL;
			this.JCSJKS=JCSJKS;
			this.JCSJJS=JCSJJS;
			this.ZKYB=ZKYB;
			this.BXRJ=BXRJ;
			this.RJL=RJL;
			this.CWL=CWL;
			this.JY=JY;
			this.JYPH=JYPH;
			this.YDYDL=YDYDL;
			this.DYDL=DYDL;
			this.SampleSheet=SampleSheet;
			this.RunFold=RunFold;
			this.DYHJWD=DYHJWD;
			this.DYHJSD=DYHJSD;
			this.JDDW=JDDW;
			this.TQPH=TQPH;
			this.KZDYPH=KZDYPH;
			this.ZKYBWZ=ZKYBWZ;
			this.TQQR=TQQR;
			this.KZDYQR=KZDYQR;
			this.DYTQ_ID=DYTQ_ID;
			this.DYKZ_ID=DYKZ_ID;
		}
		public var ID:String;//ID
		public var LXJ:String;//离心机
		public var YYQ:String;//移液器
		public var JRY:String;//加热仪
		public var HWHYY:String;//恒温混匀仪
		public var XWHHQ:String;//漩涡混合器
		public var SY:String;//水浴
		public var XWJ:String;//显微镜
		public var GZZ:String;//工作站
		public var TQR:String;//提取人
		public var RQ:String;//日期	
		public var TQR_NAME:String;//提取方法
		public var TQFF:String;//提取方法
		public var CHFF:String;//纯化方法
		
		public var KZY:String;//扩增仪
		public var KZLXJ:String;//扩增离心机
		public var KZXWHHQ:String;//扩增漩涡混合器
		public var KZYYQ:String;//扩增移液器
		public var KZCJT:String;//扩增超净台
		public var KZGZZ:String;//扩增工作站
		public var KZSJKS:String;//扩增时间开始
		public var KZSJJS:String;//扩增时间结束
		public var KZFF:String;//扩增方法
		public var SJHPH:String;//试剂盒批号
		public var KZTX:String;//扩增体系
		public var KZMB:String;//扩增模板
		public var XHS:String;//循环数
		public var HJWD:String;//环境温度
		public var HJSD:String;//环境湿度
		
		public var DYY:String;//电泳仪
		public var DYLXJ:String;//电泳离心机
		public var DYJRY:String;//电泳加热仪
		public var DYXWHHQ:String;//电泳漩涡混合器
		public var DYYYQ:String;//电泳移液器
		public var DYZBJ:String;//电泳制冰机
		public var DYCJT:String;//电泳超净台
		public var DYGZZ:String;//电泳工作站
		public var NB:String;//内标
		public var NBL:String;//内标量
		public var JCSJKS:String;//检测时间开始
		public var JCSJJS:String;//检测时间结束
		public var ZKYB:String;//质控样本
		public var BXRJ:String;//变性溶剂
		public var RJL:String;//溶剂量
		public var CWL:String;//产物量
		public var JY:String;//胶液
		public var JYPH:String;//胶液批号
		public var YDYDL:String;//预电泳电流
		public var DYDL:String;//电泳电流
		public var SampleSheet:String;//SampleSheet
		public var RunFold:String;//RunFold
		public var DYHJWD:String;//电泳环境温度
		public var DYHJSD:String;//电泳环境湿度
		public var JDDW:String;//鉴定单位
		public var TQPH:String;//提取批号
		public var KZDYPH:String;//扩增电泳批号
		public var ZKYBWZ:String;//质控样本位置
		public var TQQR:String;//提取确认
		public var KZDYQR:String;//扩增电泳确认
		public var DYTQ_ID:String;//对应提取记录ID
		public var DYKZ_ID:String;//对应扩增记录ID
	}
}