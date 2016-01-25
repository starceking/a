package util
{
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	
	import mx.core.FlexGlobals;
	import mx.utils.URLUtil;
	
	public class Server
	{
		//Object
		public static var wsName:String="LNIFS5DNAWs";		
		public static var webName:String="LNIFS5DNAWeb";
		public static var serverName:String=URLUtil.getServerName(FlexGlobals.topLevelApplication.url);	
		//str calc
		public static var srbUrl:String= "http://"+serverName+"/STRCALC/Qizjd/Qizjd/Ohe.aspx";
		public static var fqzsUrl:String= "http://"+serverName+"/STRCALC/Qizjd/Qizjd/Import.aspx";
		//help down
		public static var helpDown:String= "http://"+serverName+"/"+webName+"/az.zip";
		public static var bmDown:String= "http://"+serverName+"/"+webName+"/书签模版.doc";
		public static var dywzmbDown:String= "http://"+serverName+"/"+webName+"/STR/导出电泳位置信息模板.xls";
		//pki
		public static var pkiUrl:String= "https://"+serverName+"/"+webName+"/GetPki.aspx";
		//External use
		public static function getWsUrl(wsClass:String):String
		{
			return "http://"+serverName+"/"+wsName+"/"+wsClass+".asmx?wsdl";
		}
		public static function getUIImage(image:String):String
		{
			return "http://"+serverName+"/"+webName+"/App_Themes/UIImage/"+image;
		}
		//External call
		public static function getLinksUrl(psb:String):String
		{
			return "http://"+serverName+"/LNIFS5DNA/IFS5.html?psb="+psb;
		}
		public static function getBarCodeUrl(tableName:String,sc:String,sampleId:String,conno:String):String
		{
			if(sampleId.length>0)
			{
				if(sc.length>0)
				{
					return "http://"+serverName+"/BarCodePrint.aspx?tableName="+tableName+"&sc="+sc+"&sampleId="+sampleId;
				}
				else
				{
					return "http://"+serverName+"/BarCodePrint.aspx?tableName="+tableName+"&sampleId="+sampleId;
				}
			}
			else
			{
				if(sc.length>0)
				{
					return "http://"+serverName+"/BarCodePrint.aspx?tableName="+tableName+"&sc="+sc+"&conno="+conno;
				}
				else
				{
					return "http://"+serverName+"/BarCodePrint.aspx?tableName="+tableName+"&conno="+conno;
				}
			}
		}
		//Table down
		public static function getTableDownUploadUrl(psb:String,tp:String):String
		{
			return encodeURI("http://"+serverName+"/"+webName+"/TableDown.aspx?psb="+psb+"&tp="+tp);
		}
		public static function getTableDownUrl(psb:String,fileName:String):String
		{
			return "http://"+serverName+"/"+webName+"/"+psb+"/表格下载/"+fileName;
		}
		public static function getWordUrl(fileName:String):String
		{
			return "http://"+serverName+"/"+webName+"/Tmp/"+fileName+".doc";
		}
		//case file
		public static function getCaseUploadUrl(psb:String,conno:String,type:String,sln:String=""):String
		{
			if(sln.length==0)
				return encodeURI("http://"+serverName+"/"+webName+"/CaseDataUpload.aspx?psb="+psb+"&conno="+conno+"&type="+type);
			else
				return encodeURI("http://"+serverName+"/"+webName+"/CaseDataUpload.aspx?psb="+psb+"&conno="+conno+"&type="+type+"&sln="+sln);
		}
		//case word
		public static function getCaseWordUploadUrl(psb:String,jtype:String,wtype:String):String
		{
			return encodeURI("http://"+serverName+"/"+webName+"/CaseWordTmpUpload.aspx?psb="+psb+"&jtype="+jtype+"&wtype="+wtype);
		}
		//barcode
		public static function getSmallBcUrl(con:String,sc:String,sln:String=""):void
		{
			var pre:String="http://"+serverName+"/"+webName+"/SmallBarCodesPrint.aspx?";
			if(sln.length>0)pre=pre+"sln="+sln;
			else
			{
				switch(sc)
				{
					case "检材信息":sc="1";break;
					case "对照样本":sc="2";break;
					case "失踪人亲属":sc="3";break;
					case "受害人亲属":sc="4";break;
					case "嫌疑人亲属":sc="5";break;
					case "受害人":sc="6";break;
					case "嫌疑人":sc="7";break;
					case "其他人员":sc="8";break;
					case "失踪人员":sc="9";break;
					case "无名尸体":sc="10";break;
					case "样本信息":sc="11";break;
				}
				pre= pre+"con="+con+"&sc="+sc;
			}
			navigateToURL(new URLRequest(pre));
		}
		//tes pic
		public static function getTesPicDownUrl(psb:String,conno:String,sln:String):String
		{
			return "http://"+serverName+"/"+webName+"/"+psb+"/鉴定档案/"+conno+"/物证照片/"+sln+".jpg";
		}
		public static function getTesMsUrl(psb:String,conno:String,sln:String):String
		{
			return "http://"+serverName+"/"+webName+"/"+psb+"/鉴定档案/"+conno+"/物证描述/"+sln+".doc";
		}
		//str
		public static function getImportCodiesUrl(psb:String):String
		{
			return "http://"+serverName+"/"+webName+"/ImportCodies.aspx?psb="+psb;
		}
		//docmod
		public static function getDocModUpload(psb:String,conno:String,uni:String):String
		{
			return "http://"+serverName+"/"+webName+"/DocModUpload.aspx?psb="+psb+"&conno="+conno+"&uni="+uni;
		}
		public static function getDocModDownload(psb:String,conno:String,uni:String):String
		{
			return "http://"+serverName+"/"+webName+"/"+psb+"/鉴定档案/"+conno+"/文书修改/"+uni+".rar";
		}
		//notification
		public static function getNotiUpload(psb:String,uni:String):String
		{
			return "http://"+serverName+"/"+webName+"/NotificationUpload.aspx?psb="+psb+"&uni="+uni;
		}
		public static function getNotiDownload(psb:String,uni:String):String
		{
			return "http://"+serverName+"/"+webName+"/"+psb+"/通知通告/"+uni+".rar";
		}
		//signpic
		public static function getSignPicUpload(uni:String):String
		{
			return "http://"+serverName+"/"+webName+"/SignPicUpload.aspx?uni="+uni;
		}
		public static function getSignPicDownload(uni:String):String
		{
			return "http://"+serverName+"/"+webName+"/SYSUSER/图片签名/"+uni+".jpg";
		}
		public static function getWordPath(uni:String):String
		{
			return "http://"+serverName+"/"+webName+"/1/DNA汇总表/"+uni;
		}
		public static function getWordDirPath():String
		{
			return "http://"+serverName+"/"+webName+"/Tmp/";
		}
		//人员 照片上传&下载
		public static function getSysUserPicUpload(uni:String):String
		{
			return "http://"+serverName+"/"+webName+"/SysUserPicUpload.aspx?uni="+uni;
		}
		public static function getSysUserPicDownload(uni:String):String
		{
			return "http://"+serverName+"/"+webName+"/LabFile/系统用户/"+uni+".jpg";
		}
		//体系文件 上传&下载
		public static function getWJTXDownUrl(fileName:String):String
		{
			return "http://"+serverName+"/"+webName+"/1/文件体系/"+fileName;
		}
		public static function getWJTXDownUploadUrl(tp:String):String
		{
			return encodeURI("http://"+serverName+"/"+webName+"/WJTXDown.aspx?tp="+tp);
		}
		//设备 图片上传&下载
		public static function getPICDownUploadUrl(tp:String):String
		{
			return encodeURI("http://"+serverName+"/"+webName+"/pictureUp.aspx?tp="+tp);
		}
		public static function getPICDownUrl(fileName:String):String
		{
			return "http://"+serverName+"/"+webName+"/1/pic/"+fileName;
		}
		//html 委托书
		public static function getConWordUrl(id:String):String
		{
			return encodeURI("http://"+serverName+"/"+webName+"/ConWord.aspx?id="+id);
		}
		//SampleTestExce
		public static function getImportImportSampleTestExcelUrl(kzID:String):String
		{
			return "http://"+serverName+"/"+webName+"/ImportSampleTestExcel.aspx?kzID="+kzID;
		}
		//file Upload
		public static function getFileDataUpload(psb:String,offs:String,type:String):String
		{
			return "http://"+serverName+"/"+webName+"/FileDataUpload.aspx?psb="+psb+"&offs="+offs+"&type="+type;
		}
	}
}