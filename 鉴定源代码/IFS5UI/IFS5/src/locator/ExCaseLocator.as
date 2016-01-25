package locator
{
	import com.adobe.cairngorm.control.CairngormEventDispatcher;
	import com.adobe.cairngorm.model.ModelLocator;
	
	import control.*;
	
	import flash.utils.setInterval;
	
	import mx.collections.ArrayList;
	
	import spark.components.Label;
	
	import util.Helper;
	
	import vo.ExCaseVo;
	import vo.IdFlowVo;
	
	public class ExCaseLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:ExCaseLocator;
		public static function getInstance():ExCaseLocator
		{
			if(locObj==null)
			{
				locObj=new ExCaseLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var jyrw:String="";
		[Bindable]
		public var jjdq:String="";
		[Bindable]
		public var wszz:String="";
		[Bindable]
		public var shrw:String="";
		[Bindable]
		public var sprModList:ArrayList=new ArrayList();
		[Bindable]
		public var getReportList:ArrayList=new ArrayList();
		[Bindable]
		public var spTaskList:ArrayList=new ArrayList();
		[Bindable]
		public var listObj:ArrayList=new ArrayList();
		[Bindable]
		public var winlistObj:ArrayList=new ArrayList();
		public var winMsgLable:Label;
		[Bindable]
		public var currentCase:ExCaseVo;
		public var listPager:ListPager;
		public var interval:uint; 
		//For the Ws
		public var wsObj:ExCaseVo;
		public var phone:String;
		public var sql:String;
		public var slxh:String;
		public var djrqs:String;//登记日期s
		public var djrqe:String;//登记日期e
		//Ex call
		public function getTaskRemind(task:String):void
		{
			var arr:Array=task.split('，');
			if(arr[0]!="0")jyrw="("+arr[0]+")";
			if(arr[2]!="0")wszz="("+arr[2]+")";
			if(arr[3]!="0")shrw="("+arr[3]+")";
			if(arr[1]!="0")
			{
				jjdq="(有"+arr[1].toString()+"个案件即将超过或已超过预定期限！)";
			}
		}

		public function sendNote(phone:String,msg:String):void
		{			
			this.phone=phone;
			var nono:String=IdFlowLocator.getInstance().curObj.DOC_NO.length>0?
				IdFlowLocator.getInstance().curObj.DOC_NO_SHOW:IdFlowLocator.getInstance().curObj.ACC_NO_SHOW;
			
			if(msg=="1")//领取报告通知
			{
				sql=nono+"("+
					SysUserLocator.getInstance().loginUser.NAME+SysUserLocator.getInstance().loginUser.SHORTPHONE+
					"发):请前来领取该报告/鉴定书";
			}
			else if(msg=="2")//鉴定结论通知
			{
				sql=nono+"("+
					SysUserLocator.getInstance().loginUser.NAME+SysUserLocator.getInstance().loginUser.SHORTPHONE+
					"发)，鉴定结论:"+IdFlowLocator.getInstance().curObj.CONCLUSION+"。"+
					IdFlowLocator.getInstance().curObj.CONCLUSION_REMARK;
			}
			CairngormEventDispatcher.getInstance().dispatchEvent(new IFSEvent(IFSControl.EXCASEWS_SendShortNote));
		}
		//Ws call	
		public function getAll(xml:XML):void
		{
			winlistObj.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:ExCaseVo=new ExCaseVo(xml.children()[i].ID,xml.children()[i].委托编号,xml.children()[i].打防管控,
					xml.children()[i].现场勘验,xml.children()[i].案件名称,xml.children()[i].案件类型,"",
					xml.children()[i].发案地点,xml.children()[i].发案时间,xml.children()[i].案件性质,xml.children()[i].简要案情,
					xml.children()[i].区划代码);
				winlistObj.addItem(voObj);
			}
			if(xml.children().length()>0)
			{
				listPager.RowCount=xml.children()[0].RowCount;
			}
			else
			{
				listPager.RowCount="0";
			}
		}
		public function queryGetReportCase(xml:XML):void
		{
			getReportList.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:IdFlowVo=new IdFlowVo("","","","","","","","","","","","","","","","","","","","", 
					xml.children()[i].文书名称,"","","","","","","","","","","",
					"","","","","","","","","","","","","","","","","","","","","","","","","","",xml.children()[i].委托单位名称,"","",
					"","","","","","","","","","","","","","","",xml.children()[i].案件名称,"","",xml.children()[i].案件编号,"",
					"","","","","","","","","","","","","","");
				getReportList.addItem(voObj);
			}
			//滚动设置
//			if(getReportList.length>27)
//			{
//				interval=setInterval(scrollGetReportCase,1000);
//			}
//			else
//			{
//				interval=0;
//			}
		}
		public function getSpTaskAmount(xml:XML):void
		{
			spTaskList.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{
				spTaskList.addItem({amount:xml.children()[i].amount,x:xml.children()[i].领导姓名});
			}
		}
		public function getYjrSp(xml:XML):void
		{
			sprModList.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{
				sprModList.addItem({conno:xml.children()[i].委托编号,docname:xml.children()[i].文书名称,
					accyear:xml.children()[i].受理年份,accno:xml.children()[i].受理序号,spr:xml.children()[i].领导,
					sprname:SysUserLocator.getInstance().getUserName(xml.children()[i].领导)});
			}
		}
		function scrollGetReportCase():void
		{
			var voo:IdFlowVo=getReportList.getItemAt(0) as IdFlowVo;
			getReportList.removeItemAt(0);
			getReportList.addItem(voo);
		}
	}
}