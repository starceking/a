package locator
{
	import com.adobe.cairngorm.model.ModelLocator;
	
	import mx.collections.ArrayList;
	
	import util.Helper;
	
	import vo.JusTypeVo;
	import vo.OfficeJusTypeVo;
	import vo.OfficeVo;
	
	public class JusTypeLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:JusTypeLocator;
		public static function getInstance():JusTypeLocator
		{
			if(locObj==null)
			{
				locObj=new JusTypeLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var jusTypeVo:JusTypeVo;
		[Bindable]
		public var officeList:ArrayList=new ArrayList();
		//For the Delegate
		public var Office:String;
		public var JUSTYPE:String;
		
		public var officeVo:OfficeVo;
		public var TypeVo:OfficeJusTypeVo;
		public var DnaVo:JusTypeVo;
		//Ws call		
		public function getXml(xml:XML):void
		{		
			for(var i:int=0;i<xml.children().length();i++)
			{
				var psbId:String=xml.PSB[i].@Id;
				if(psbId==PsbLocator.getInstance().idPsb.ID)
				{
					jusTypeVo=new JusTypeVo(xml.PSB[i].children()[0].children()[0].children(),
						xml.PSB[i].children()[0].children()[1].children()[0].children(),
						xml.PSB[i].children()[0].children()[1].children()[1].children(),
						xml.PSB[i].children()[0].children()[1].children()[2].children(),
						xml.PSB[i].children()[0].children()[1].children()[3].children(),
						xml.PSB[i].children()[0].children()[1].children()[4].children(),
						xml.PSB[i].children()[0].children()[1].children()[5].children(),
						xml.PSB[i].children()[0].children()[1].children()[6].children(),
						xml.PSB[i].children()[0].children()[1].children()[7].children(),
						xml.PSB[i].children()[0].children()[1].children()[8].children(),
						xml.PSB[i].children()[1].@Leader,
						xml.PSB[i].children()[1].@WholeNo,
						xml.PSB[i].children()[1].@DocName,
						xml.PSB[i].children()[1].@PlanDate,
						xml.PSB[i].children()[1].@Enabled,
						xml.PSB[i].children()[1].children()[0].children(),
						xml.PSB[i].children()[1].children()[1].children(),
						xml.PSB[i].children()[1].children()[2].children(),
						xml.PSB[i].children()[1].children()[3].children(),
						xml.PSB[i].children()[1].children()[4].children(),
						xml.PSB[i].children()[1].children()[5].children(),
						xml.PSB[i].children()[1].children()[6].children(),
						xml.PSB[i].children()[1].children()[7].children());
					
					for(var j:int=2;j<xml.PSB[i].children().length();j++)
					{
						var officeTypeList:ArrayList=new ArrayList();
						for(var k:int=0;k<xml.PSB[i].children()[j].children().length();k++)
						{
							var officeJusTypeVo:OfficeJusTypeVo=new OfficeJusTypeVo(
								xml.PSB[i].children()[j].children()[k].@Id,
								xml.PSB[i].children()[j].children()[k].@Name,
								xml.PSB[i].children()[j].children()[k].@DocName,
								xml.PSB[i].children()[j].children()[k].children()[0].children(),
								xml.PSB[i].children()[j].children()[k].children()[1].children(),
								xml.PSB[i].children()[j].children()[k].children()[2].children(),
								xml.PSB[i].children()[j].children()[k].children()[3].children(),
								xml.PSB[i].children()[j].children()[k].children()[4].children());
							officeTypeList.addItem(officeJusTypeVo);
						}
						
						var officeVo:OfficeVo=new OfficeVo(
							xml.PSB[i].children()[j].@Name,
							xml.PSB[i].children()[j].@Leader,
							xml.PSB[i].children()[j].@PlanDate,officeTypeList);
						officeList.addItem(officeVo);
					}
				}
			}
		}
		//Ex call
		public function getOffice(office:String):OfficeVo
		{
			for(var i:int=0;i<officeList.length;i++)
			{
				if(officeList.getItemAt(i).Name==office)
				{
					return officeList.getItemAt(i) as OfficeVo;
				}
			}
			return null;
		}
		public function getJusType(office:String,jusTypeName:String):OfficeJusTypeVo
		{
			var voObj:OfficeVo=getOffice(office);
			for(var i:int=0;i<voObj.officeTypeList.length;i++)
			{
				if(voObj.officeTypeList.getItemAt(i).Name==jusTypeName) 
					return voObj.officeTypeList.getItemAt(i) as OfficeJusTypeVo;
			}
			return null;
		}
		public function getJusTypeItems(office:String,jusTypeName:String):String
		{
			if(jusTypeName.length==0)return "";
			if(office=="DNA")return "";
			
			var voObj:OfficeVo=getOffice(office);
			for(var i:int=0;i<voObj.officeTypeList.length;i++)
			{
				if(voObj.officeTypeList.getItemAt(i).Name==jusTypeName) 
					return voObj.officeTypeList.getItemAt(i).JUSITEM;
			}
			return "";
		}
		public function getAllOffice():ArrayList
		{
			var list:ArrayList=new ArrayList();
			for(var i:int=0;i<officeList.length;i++)
			{
				list.addItem({label:officeList.getItemAt(i).Name});
			}
			return list;
		}
		public function getAllJusType(office:String):ArrayList
		{
			var list:ArrayList=new ArrayList();
			if(office=="DNA")
			{
				list.addItem({label:"DNA案件"});
				list.addItem({label:"失踪人亲属"});
				list.addItem({label:"失踪人员"});
			}
			else
			{
				var voObj:OfficeVo=getOffice(office);
				if(voObj==null) return list;				
				for(var i:int=0;i<voObj.officeTypeList.length;i++)
				{
					list.addItem({label:voObj.officeTypeList.getItemAt(i).Name});
				}
			}
			return list;
		}
	}
}