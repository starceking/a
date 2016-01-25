package locator
{	
	import mx.collections.ArrayList;
	
	import util.Helper;
	
	import vo.StrVo;
	import vo.PreExamVo;
	
	public class ConfirmLocator
	{
		//Singleton
		private static var locObj:ConfirmLocator;
		public static function getInstance():ConfirmLocator
		{
			if(locObj==null)
			{
				locObj=new ConfirmLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var ConfirmListObj:ArrayList=new ArrayList();
		public var ConfirmListPager:ListPager;
		//For the Ws
		public var StrwsObj:StrVo;
		public var wsObj:PreExamVo;
		public var slsjs:String="";//受理时间s
		public var slsje:String="";//受理时间e
		public var yblx:String="";//样本类型
		public var yjr:String="";//鉴定人
		public var jystatus:String="";//检验状态
		public var confirm:String="";//确证试验标记
		public var ConfirmArray:Array = new Array();
		//Ws call
		public function insertConfirm():void
		{
			Helper.showAlert("保存成功！");
		}
		public function updateCaseConfirm():void
		{
			var index:int=getConfirmVoIndex(wsObj.ID);
			ConfirmListObj.removeItemAt(index);
			ConfirmListObj.addItemAt(wsObj,index);
		}		
		public function deleteCaseConfirm():void
		{
			ConfirmListObj.removeItemAt(getConfirmVoIndex(wsObj.ID));
		}
		public function queryCaseConfirm(xml:XML):void
		{
			ConfirmListObj.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:PreExamVo=new PreExamVo(xml.children()[i].ID,xml.children()[i].案件ID,xml.children()[i].委托编号,xml.children()[i].库类型,
					xml.children()[i].样本编号,xml.children()[i].样本名称,xml.children()[i].样本类型,
					xml.children()[i].试验方法,xml.children()[i].试验人,xml.children()[i].日期,xml.children()[i].试验结果);
				ConfirmListObj.addItem(voObj);
			}
			if(xml.children().length()>0)
			{
				ConfirmListPager.RowCount=xml.children()[0].RowCount;
			}
			else
			{
				ConfirmListPager.RowCount="0";
			}
		}
		public function queryConfirm(xml:XML):void
		{
			ConfirmListObj.removeAll();			
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:StrVo=new StrVo(xml.children()[i].ID,xml.children()[i].案件ID,xml.children()[i].委托编号,
					"",xml.children()[i].样本编号,xml.children()[i].名称,xml.children()[i].库类型,
					xml.children()[i].AMEL,xml.children()[i].D8S1179,xml.children()[i].D21S11,xml.children()[i].D18S51,xml.children()[i].vWA,
					xml.children()[i].D3S1358,xml.children()[i].FGA,xml.children()[i].TH01,xml.children()[i].D5S818,xml.children()[i].D13S317,
					xml.children()[i].D7S820,xml.children()[i].CSF1PO,xml.children()[i].D16S539,xml.children()[i].TPOX,xml.children()[i].D2S1338,
					xml.children()[i].D19S433,xml.children()[i].Penta_D,xml.children()[i].Penta_E,xml.children()[i].D6S1043,xml.children()[i].F13A01,
					xml.children()[i].FESFPS,xml.children()[i].D1S80,xml.children()[i].D12S391,xml.children()[i].D1S1656,
					xml.children()[i].D2S441, xml.children()[i].D22S1045,xml.children()[i].SE33,xml.children()[i].D10S1248,xml.children()[i].Y_indel,
					xml.children()[i].B_DYS456,xml.children()[i].B_DYS389I,xml.children()[i].B_DYS390,xml.children()[i].B_DYS389II,
					xml.children()[i].G_DYS458,xml.children()[i].G_DYS19,xml.children()[i].G_DYS385,xml.children()[i].Y_DYS393,
					xml.children()[i].Y_DYS391,xml.children()[i].Y_DYS439,xml.children()[i].Y_DYS635,xml.children()[i].Y_DYS392,
					xml.children()[i].R_Y_GATA_H4,xml.children()[i].R_DYS437,xml.children()[i].R_DYS438,xml.children()[i].R_DYS448,
					xml.children()[i].比中序号,xml.children()[i].IMP_FLAG,xml.children()[i].STR_FLAG,xml.children()[i].YSTR_FLAG,xml.children()[i].样本类型);
				ConfirmListObj.addItem(voObj);
			}
			if(xml.children().length()>0)
			{
				ConfirmListPager.RowCount=xml.children()[0].RowCount;
			}
			else
			{
				ConfirmListPager.RowCount="0";
			}
		}
		//Inner call
		private function getConfirmVoIndex(id:String):int
		{
			for(var i:int=0;i<ConfirmListObj.length;i++)
			{
				var voObj:PreExamVo=ConfirmListObj.getItemAt(i) as PreExamVo;
				if(voObj.ID==id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}