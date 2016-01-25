package util
{
	//Import
	import com.adobe.cairngorm.control.CairngormEventDispatcher;
	
	import locator.MenuLocator;
	
	import mx.controls.Alert;
	import mx.controls.DateField;
	import mx.core.FlexGlobals;
	import mx.formatters.DateFormatter;
	import mx.managers.PopUpManager;
	import mx.utils.UIDUtil;
	
	import spark.components.Application;
	import spark.components.TextArea;
	import spark.components.TextInput;
	
	import vo.MenuVo;
	
	public class Helper
	{
		//External call
		public static function getAddedDate(date:Date,day:String):String{
			if(day.length==0)day="0";
			var dayN:Number=Number(day);
			date.setTime(date.getTime()+dayN * 24 * 3600 * 1000);
			return getStrByDate(date);
		}
		public static function getStrByDate(date:Date):String
		{
			var dateFormatter:DateFormatter = new DateFormatter();      
			dateFormatter.formatString = "YYYY-MM-DD";       
			return dateFormatter.format(date); 
		}
		public static function getDateString(date:String,format:String=""):String
		{
			if(date.length==0)return"";
			var dateFormatter:DateFormatter = new DateFormatter();
			if(format.length==0)
			{
				dateFormatter.formatString= "YYYY-MM-DD";
			}
			else
			{
				dateFormatter.formatString=format;
			}
			
			var str:String =dateFormatter.format(date);
			return str;
		}	
		public static function getByDateString(date:String,format:String=""):String
		{
			if(date.length==0)return"";
			var dateFormatter:DateFormatter = new DateFormatter();
			if(format.length==0)
			{
				dateFormatter.formatString= "YYYY-MM-DD H:NN:SS";
			}
			else
			{
				dateFormatter.formatString=format;
			}
			
			var str:String =dateFormatter.format(date);
			return str;
		}	
		public static function getGUID():String
		{
			return UIDUtil.createUID().replace("-","").replace("-","").replace("-","").replace("-","");
		}		
		public static function showAlert(msg:String):void
		{
			var alert:Alert = Alert.show(msg,"提示",Alert.YES,FlexGlobals.topLevelApplication as Application);    
			PopUpManager.centerPopUp (alert);     
			alert.move(FlexGlobals.topLevelApplication.contentMouseX/2+100,FlexGlobals.topLevelApplication.contentMouseY/2+200);  
		}
		public static function showConfirmAlert(msg:String,func:Function):void
		{
			var alert:Alert = Alert.show(msg,"请确定",Alert.YES|Alert.CANCEL,FlexGlobals.topLevelApplication as Application,func);    			
			PopUpManager.centerPopUp (alert);     
			alert.move(FlexGlobals.topLevelApplication.contentMouseX/2,FlexGlobals.topLevelApplication.contentMouseY/2); 
		}
		public static function getDiffDays(now:String,start:String):Number
		{
			var nowDate:Date = DateField.stringToDate(now,"YYYY-MM-DD");
			return getDiffDaysByDate(nowDate,start);
		}
		public static function getDiffDaysByDate(nowDate:Date,start:String):Number
		{
			var startDate:Date = DateField.stringToDate(start,"YYYY-MM-DD");
			return int((Number(nowDate)-Number(startDate))/(3600000*24));
		}
		public static function setIndexContentNoMenu(module:String=""):void
		{					
			FlexGlobals.topLevelApplication.contentModule.unloadModule();		

			if(module.length>0)
			{
				FlexGlobals.topLevelApplication.contentModule.loadModule(module);
			}		
		}
		public static function changeTopState(topState:String):void
		{
			FlexGlobals.topLevelApplication.currentState=topState;				
			MenuLocator.getInstance().initMenu(topState);				
			if(topState=="Login")
			{
				setIndexContentNoMenu("view/index/IndexModule.swf");
			}
			else if((topState=="Consign")||(topState=="DNA")||(topState=="Office")||
				(topState=="Insider")||(topState=="Techer")||(topState=="Leader"))
			{
				setIndexContentNoMenu("view/usermain/"+topState+"IndexModule.swf");
			}
		}
		public static function validateTextInput(txtIn:TextInput):Boolean
		{
			if(txtIn.text.length>0)
			{
				txtIn.setStyle("borderColor","6908265");
				txtIn.setStyle("contentBackgroundColor","white");
				txtIn.setStyle("contentBackgroundAlpha","0.0");
				return true;
			}
			else
			{
				txtIn.setStyle("borderColor","red");
				txtIn.setStyle("contentBackgroundColor","#F8FB0B");
				txtIn.setStyle("contentBackgroundAlpha","1.0");
				return false;
			}
		}
		public static function validateTextArea(txtIn:TextArea):Boolean
		{
			if(txtIn.text.length>0)
			{
				txtIn.setStyle("borderColor","6908265");
				txtIn.setStyle("contentBackgroundColor","white");
				txtIn.setStyle("contentBackgroundAlpha","0.0");
				return true;
			}
			else
			{
				txtIn.setStyle("borderColor","red");
				txtIn.setStyle("contentBackgroundColor","#F8FB0B");
				txtIn.setStyle("contentBackgroundAlpha","1.0");
				return false;
			}
		}
		public static function getSlnNumLen(pattern:String):String
		{
			var len:int=0;
			for(var i:int=0;i<pattern.length;i++)
			{
				if(pattern.charAt(i)=="n")len++;
			}
			return len.toString();
		}
		public static function getSlnStr(pattern:String,year:String,accNo:String,num:String):String
		{
			if(year.length>0)
			{
				if(pattern.search("yyyy")>=0)pattern=pattern.replace("yyyy",year);
				else if(pattern.search("yy")>=0)pattern=pattern.replace("yy",year.substr(2));
			}
			else
			{
				pattern=pattern.replace("y","").replace("y","").replace("y","").replace("y","");
			}
			if(accNo.length>0)
			{
				if(pattern.search("cccccc")>=0)pattern=pattern.replace("cccccc",strPadLenLeft(accNo,6,"0"));
				else if(pattern.search("ccccc")>=0)pattern=pattern.replace("ccccc",strPadLenLeft(accNo,5,"0"));
				else if(pattern.search("cccc")>=0)pattern=pattern.replace("cccc",strPadLenLeft(accNo,4,"0"));
				else if(pattern.search("ccc")>=0)pattern=pattern.replace("ccc",strPadLenLeft(accNo,3,"0"));
				else if(pattern.search("cc")>=0)pattern=pattern.replace("cc",strPadLenLeft(accNo,2,"0"));
				else if(pattern.search("c")>=0)pattern=pattern.replace("c",strPadLenLeft(accNo,1,"0"));
			}
			else
			{
				pattern=pattern.replace("c","").replace("c","").replace("c","").replace("c","").replace("c","").replace("c","");
			}
			if(num.length>0)
			{
				if(pattern.search("nnnnnn")>=0)pattern=pattern.replace("nnnnnn",strPadLenLeft(num,6,"0"));
				else if(pattern.search("nnnnn")>=0)pattern=pattern.replace("nnnnn",strPadLenLeft(num,5,"0"));
				else if(pattern.search("nnnn")>=0)pattern=pattern.replace("nnnn",strPadLenLeft(num,4,"0"));
				else if(pattern.search("nnn")>=0)pattern=pattern.replace("nnn",strPadLenLeft(num,3,"0"));
				else if(pattern.search("nn")>=0)pattern=pattern.replace("nn",strPadLenLeft(num,2,"0"));
				else if(pattern.search("n")>=0)pattern=pattern.replace("n",strPadLenLeft(num,1,"0"));
			}
			else
			{
				pattern=pattern.replace("n","").replace("n","").replace("n","").replace("n","").replace("n","").replace("n","");
			}
			
			return pattern;
		}
		private static function strPadLenLeft(src:String,len:int,padChar:String):String
		{
			for(var i:int=src.length;i<len;i++)
			{
				src=padChar+src;
			}
			return src;
		}
		public static function setIndexContent(module:String=""):void
		{					
			FlexGlobals.topLevelApplication.contentModule.unloadModule();		
			
			if(module.length>0)
			{
				FlexGlobals.topLevelApplication.contentModule.loadModule(module);
			}		
		}
		public static function pushMenu(label:String,url:String)
		{
			var voObj:MenuVo=new MenuVo(label,url);
			MenuLocator.getInstance().push(voObj);
		}
		public static function getTableByScType(type:String):String
		{
			switch (type)
			{
				case "现场物证": return "样本信息";
				case "受害人":
				case "嫌疑人":
				case "其他人员": return "涉案人员";
				case "受害人亲属":
				case "嫌疑人亲属":
				case "失踪人亲属": return "亲属信息";
				case "失踪人员": return "失踪人员";
				case "无名尸体": return "无名尸体";
			}
			return "";
		}
		public static function getTime():String
		{
			var thisTime:String;
			var dt:Date = new Date();
			thisTime=dt.fullYear.toString()+"-"+(dt.month+1).toString()+"-"+dt.date.toString()
				+" "+dt.hours.toString()+":"+dt.minutes.toString();
			return thisTime;
		}
	}
}