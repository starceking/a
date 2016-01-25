package locator
{
	import com.adobe.cairngorm.control.CairngormEventDispatcher;
	import com.adobe.cairngorm.model.ModelLocator;
	
	import control.*;
	
	import flash.utils.setInterval;
	
	import mx.collections.ArrayList;
	
	public class StatisticsLocator
	{
		//Singleton
		private static var locObj:StatisticsLocator;
		public static function getInstance():StatisticsLocator
		{
			if(locObj==null)
			{
				locObj=new StatisticsLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var PersonWorkList:ArrayList=new ArrayList();
		public var StationWorkList:ArrayList=new ArrayList();
		public var GetStationList:ArrayList=new ArrayList();
		public var CasePropertyList:ArrayList=new ArrayList();
		public var CaseConclusionList:ArrayList=new ArrayList();
		//For the Ws
		public var Office:String="";
		public var Person:String="";
		public var Station:String="";
		public var StationName:String="";
		public var State:String="";
		public var Type:String="";
		public var Property:String="";
		public var Conclusion:String="";
		public var DateTime1:String="";
		public var DateTime2:String="";
		//Ex call
		public function PersonWork(xml:XML):void
		{
			PersonWorkList.removeAll();		
			var Amountnum_1:int=0;var Amountnum_2:int=0;var Amountnum_3:int=0;
			var Amountnum_4:int=0;var Amountnum_5:int=0;var Amountnum_6:int=0;
			var Amountnum_7:int=0;var Amountnum_8:int=0;var Amountnum_9:int=0;
			var AmountNumAll:int=0;
			for(var i:int=0;i<xml.children().length();i++)
			{
				PersonWorkList.addItem({
					person:xml.children()[i].实验员,
					num_1:xml.children()[i].Num_1,num_2:xml.children()[i].Num_2,num_3:xml.children()[i].Num_3,
					num_4:xml.children()[i].Num_4,num_5:xml.children()[i].Num_5,num_6:xml.children()[i].Num_6,
					num_7:xml.children()[i].Num_7,num_8:xml.children()[i].Num_8,num_9:xml.children()[i].Num_9,
					AmountNum:xml.children()[i].案件总数});
				Amountnum_1+=Number(xml.children()[i].Num_1);Amountnum_2+=Number(xml.children()[i].Num_2);
				Amountnum_3+=Number(xml.children()[i].Num_3);Amountnum_4+=Number(xml.children()[i].Num_4);
				Amountnum_5+=Number(xml.children()[i].Num_5);Amountnum_6+=Number(xml.children()[i].Num_6);
				Amountnum_7+=Number(xml.children()[i].Num_7);Amountnum_8+=Number(xml.children()[i].Num_8);
				Amountnum_9+=Number(xml.children()[i].Num_9);
				AmountNumAll+=Number(xml.children()[i].案件总数);
			}
			PersonWorkList.addItem({
				person:"总数",
				num_1:String(Amountnum_1),num_2:String(Amountnum_2),num_3:String(Amountnum_3),
				num_4:String(Amountnum_4),num_5:String(Amountnum_5),num_6:String(Amountnum_6),
				num_7:String(Amountnum_7),num_8:String(Amountnum_8),num_9:String(Amountnum_9),
				AmountNum:String(AmountNumAll)});
		}
		
		public function StationWork(xml:XML):void
		{
			StationWorkList.removeAll();		
			var Amountnum_1:int=0;var Amountnum_2:int=0;var Amountnum_3:int=0;
			var Amountnum_4:int=0;var Amountnum_5:int=0;var Amountnum_6:int=0;
			var Amountnum_7:int=0;var Amountnum_8:int=0;var Amountnum_9:int=0;
			var AmountNumAll:int=0;
			for(var i:int=0;i<xml.children().length();i++)
			{
				StationWorkList.addItem({
					station:xml.children()[i].分县局,
					stationName:xml.children()[i].单位名称,
					num_1:xml.children()[i].Num_1,num_2:xml.children()[i].Num_2,num_3:xml.children()[i].Num_3,
					num_4:xml.children()[i].Num_4,num_5:xml.children()[i].Num_5,num_6:xml.children()[i].Num_6,
					num_7:xml.children()[i].Num_7,num_8:xml.children()[i].Num_8,num_9:xml.children()[i].Num_9,
					AmountNum:xml.children()[i].案件总数});
				Amountnum_1+=Number(xml.children()[i].Num_1);Amountnum_2+=Number(xml.children()[i].Num_2);
				Amountnum_3+=Number(xml.children()[i].Num_3);Amountnum_4+=Number(xml.children()[i].Num_4);
				Amountnum_5+=Number(xml.children()[i].Num_5);Amountnum_6+=Number(xml.children()[i].Num_6);
				Amountnum_7+=Number(xml.children()[i].Num_7);Amountnum_8+=Number(xml.children()[i].Num_8);
				Amountnum_9+=Number(xml.children()[i].Num_9);
				AmountNumAll+=Number(xml.children()[i].案件总数);
			}
			StationWorkList.addItem({
				station:"总数",stationName:"总数",
				num_1:String(Amountnum_1),num_2:String(Amountnum_2),num_3:String(Amountnum_3),
				num_4:String(Amountnum_4),num_5:String(Amountnum_5),num_6:String(Amountnum_6),
				num_7:String(Amountnum_7),num_8:String(Amountnum_8),num_9:String(Amountnum_9),
				AmountNum:String(AmountNumAll)});
		}
		
		public function GetStation(xml:XML):void
		{
			GetStationList.removeAll();			
			GetStationList.addItem({Name:""});
			for(var i:int=0;i<xml.children().length();i++)
			{
				GetStationList.addItem({Name:xml.children()[i].分县局});
			}
		}
		
		public function CaseProperty(xml:XML):void
		{
			CasePropertyList.removeAll();		
			var Amountnum_1:int=0;var Amountnum_2:int=0;var Amountnum_3:int=0;var Amountnum_4:int=0;var Amountnum_5:int=0;
			var Amountnum_6:int=0;var Amountnum_7:int=0;var Amountnum_8:int=0;var Amountnum_9:int=0;var Amountnum_10:int=0;
			var Amountnum_11:int=0;var Amountnum_12:int=0;var Amountnum_13:int=0;var Amountnum_14:int=0;var Amountnum_15:int=0;
			var Amountnum_16:int=0;var Amountnum_17:int=0;var Amountnum_18:int=0;var Amountnum_19:int=0;var Amountnum_20:int=0;
			var Amountnum_21:int=0;var Amountnum_22:int=0;var Amountnum_23:int=0;var Amountnum_24:int=0;var Amountnum_25:int=0;
			var Amountnum_26:int=0;var Amountnum_27:int=0;var Amountnum_28:int=0;var Amountnum_29:int=0;var Amountnum_30:int=0;
			var Amountnum_31:int=0;var Amountnum_32:int=0;var Amountnum_33:int=0;var Amountnum_34:int=0;var Amountnum_35:int=0;
			var Amountnum_36:int=0;var Amountnum_37:int=0;var Amountnum_38:int=0;var Amountnum_39:int=0;var Amountnum_40:int=0;
			var AmountNumAll:int=0;
			for(var i:int=0;i<xml.children().length();i++)
			{
				CasePropertyList.addItem({
					station:xml.children()[i].分县局,
					stationName:xml.children()[i].单位名称,
					num_1:xml.children()[i].Num_1,num_2:xml.children()[i].Num_2,num_3:xml.children()[i].Num_3,
					num_4:xml.children()[i].Num_4,num_5:xml.children()[i].Num_5,num_6:xml.children()[i].Num_6,
					num_7:xml.children()[i].Num_7,num_8:xml.children()[i].Num_8,num_9:xml.children()[i].Num_9,
					num_10:xml.children()[i].Num_10,num_11:xml.children()[i].Num_11,num_12:xml.children()[i].Num_12,
					num_13:xml.children()[i].Num_13,num_14:xml.children()[i].Num_14,num_15:xml.children()[i].Num_15,
					num_16:xml.children()[i].Num_16,num_17:xml.children()[i].Num_17,num_18:xml.children()[i].Num_18,
					num_19:xml.children()[i].Num_19,num_20:xml.children()[i].Num_20,num_21:xml.children()[i].Num_21,
					num_22:xml.children()[i].Num_22,num_23:xml.children()[i].Num_23,num_24:xml.children()[i].Num_24,
					num_25:xml.children()[i].Num_25,num_26:xml.children()[i].Num_26,num_27:xml.children()[i].Num_27,
					num_28:xml.children()[i].Num_28,num_29:xml.children()[i].Num_29,num_30:xml.children()[i].Num_30,
					num_31:xml.children()[i].Num_31,num_32:xml.children()[i].Num_32,num_33:xml.children()[i].Num_33,
					num_34:xml.children()[i].Num_34,num_35:xml.children()[i].Num_35,num_36:xml.children()[i].Num_36,
					num_37:xml.children()[i].Num_37,num_38:xml.children()[i].Num_38,num_39:xml.children()[i].Num_39,
					num_40:xml.children()[i].Num_40,
					AmountNum:xml.children()[i].案件总数});
				Amountnum_1+=Number(xml.children()[i].Num_1);Amountnum_2+=Number(xml.children()[i].Num_2);
				Amountnum_3+=Number(xml.children()[i].Num_3);Amountnum_4+=Number(xml.children()[i].Num_4);
				Amountnum_5+=Number(xml.children()[i].Num_5);Amountnum_6+=Number(xml.children()[i].Num_6);
				Amountnum_7+=Number(xml.children()[i].Num_7);Amountnum_8+=Number(xml.children()[i].Num_8);
				Amountnum_9+=Number(xml.children()[i].Num_9);Amountnum_10+=Number(xml.children()[i].Num_10);
				Amountnum_11+=Number(xml.children()[i].Num_11);Amountnum_12+=Number(xml.children()[i].Num_12);
				Amountnum_13+=Number(xml.children()[i].Num_13);Amountnum_14+=Number(xml.children()[i].Num_14);
				Amountnum_15+=Number(xml.children()[i].Num_15);Amountnum_16+=Number(xml.children()[i].Num_16);
				Amountnum_17+=Number(xml.children()[i].Num_17);Amountnum_18+=Number(xml.children()[i].Num_18);
				Amountnum_19+=Number(xml.children()[i].Num_19);Amountnum_20+=Number(xml.children()[i].Num_20);
				Amountnum_21+=Number(xml.children()[i].Num_21);Amountnum_22+=Number(xml.children()[i].Num_22);
				Amountnum_23+=Number(xml.children()[i].Num_23);Amountnum_24+=Number(xml.children()[i].Num_24);
				Amountnum_25+=Number(xml.children()[i].Num_25);Amountnum_26+=Number(xml.children()[i].Num_26);
				Amountnum_27+=Number(xml.children()[i].Num_27);Amountnum_28+=Number(xml.children()[i].Num_28);
				Amountnum_29+=Number(xml.children()[i].Num_29);Amountnum_30+=Number(xml.children()[i].Num_30);
				Amountnum_31+=Number(xml.children()[i].Num_31);Amountnum_32+=Number(xml.children()[i].Num_32);
				Amountnum_33+=Number(xml.children()[i].Num_33);Amountnum_34+=Number(xml.children()[i].Num_34);
				Amountnum_35+=Number(xml.children()[i].Num_35);Amountnum_36+=Number(xml.children()[i].Num_36);
				Amountnum_37+=Number(xml.children()[i].Num_37);Amountnum_38+=Number(xml.children()[i].Num_38);
				Amountnum_39+=Number(xml.children()[i].Num_39);Amountnum_40+=Number(xml.children()[i].Num_40);
				AmountNumAll+=Number(xml.children()[i].案件总数);
			}
			CasePropertyList.addItem({
				station:"总数",stationName:"总数",
				num_1:String(Amountnum_1),num_2:String(Amountnum_2),num_3:String(Amountnum_3),
				num_4:String(Amountnum_4),num_5:String(Amountnum_5),num_6:String(Amountnum_6),
				num_7:String(Amountnum_7),num_8:String(Amountnum_8),num_9:String(Amountnum_9),
				num_10:String(Amountnum_10),num_11:String(Amountnum_11),num_12:String(Amountnum_12),
				num_13:String(Amountnum_13),num_14:String(Amountnum_14),num_15:String(Amountnum_15),
				num_16:String(Amountnum_16),num_17:String(Amountnum_17),num_18:String(Amountnum_18),
				num_19:String(Amountnum_19),num_20:String(Amountnum_20),num_21:String(Amountnum_21),
				num_22:String(Amountnum_22),num_23:String(Amountnum_23),num_24:String(Amountnum_24),
				num_25:String(Amountnum_25),num_26:String(Amountnum_26),num_27:String(Amountnum_27),
				num_28:String(Amountnum_28),num_29:String(Amountnum_29),num_30:String(Amountnum_30),
				num_31:String(Amountnum_31),num_32:String(Amountnum_32),num_33:String(Amountnum_33),
				num_34:String(Amountnum_34),num_35:String(Amountnum_35),num_36:String(Amountnum_36),
				num_37:String(Amountnum_37),num_38:String(Amountnum_38),num_39:String(Amountnum_39),
				num_40:String(Amountnum_40),
				AmountNum:String(AmountNumAll)});
		}
		
		public function CaseConclusion(xml:XML):void
		{
			CaseConclusionList.removeAll();		

			for(var i:int=0;i<xml.children().length();i++)
			{
				CaseConclusionList.addItem({
					type:xml.children()[i].鉴定类别,
					num_1:xml.children()[i].Num_1,num_2:xml.children()[i].Num_2,num_3:xml.children()[i].Num_3,
					num_4:xml.children()[i].Num_4,num_5:xml.children()[i].Num_5,num_6:xml.children()[i].Num_6,
					num_7:xml.children()[i].Num_7,num_8:xml.children()[i].Num_8,num_9:xml.children()[i].Num_9,
					num_10:xml.children()[i].Num_10,num_11:xml.children()[i].Num_11,num_12:xml.children()[i].Num_12,
					num_13:xml.children()[i].Num_13,num_14:xml.children()[i].Num_14,num_15:xml.children()[i].Num_15,
					num_16:xml.children()[i].Num_16,num_17:xml.children()[i].Num_17,num_18:xml.children()[i].Num_18,
					num_19:xml.children()[i].Num_19,num_20:xml.children()[i].Num_20,num_21:xml.children()[i].Num_21,
					num_22:xml.children()[i].Num_22,num_23:xml.children()[i].Num_23,num_24:xml.children()[i].Num_24,
					num_25:xml.children()[i].Num_25,num_26:xml.children()[i].Num_26,num_27:xml.children()[i].Num_27,
					num_28:xml.children()[i].Num_28,num_29:xml.children()[i].Num_29,num_30:xml.children()[i].Num_30,
					num_31:xml.children()[i].Num_31,num_32:xml.children()[i].Num_32,num_33:xml.children()[i].Num_33,
					num_34:xml.children()[i].Num_34,num_35:xml.children()[i].Num_35,num_36:xml.children()[i].Num_36,
					num_37:xml.children()[i].Num_37,num_38:xml.children()[i].Num_38,num_39:xml.children()[i].Num_39,
					num_40:xml.children()[i].Num_40,num_41:xml.children()[i].Num_41,num_42:xml.children()[i].Num_42,
					num_43:xml.children()[i].Num_43,num_44:xml.children()[i].Num_44,num_45:xml.children()[i].Num_45,
					num_46:xml.children()[i].Num_46,num_47:xml.children()[i].Num_47,num_48:xml.children()[i].Num_48,
					num_49:xml.children()[i].Num_49,num_50:xml.children()[i].Num_50,
					AmountNum:xml.children()[i].案件总数});
			}
		}
	}
}