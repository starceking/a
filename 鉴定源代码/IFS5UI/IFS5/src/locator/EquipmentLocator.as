package locator
			{
		import com.adobe.cairngorm.control.CairngormEventDispatcher;
		import com.adobe.cairngorm.model.ModelLocator;
		
		import control.*;
		
		import flash.external.ExternalInterface;
		
		import mx.collections.ArrayList;
		import mx.managers.BrowserManager;
		import mx.managers.IBrowserManager;
		
		import util.Helper;
		
		import vo.EquipmentVo;
		public class EquipmentLocator implements ModelLocator
		{
			//Singleton
			private static var locObj:EquipmentLocator;
			public static function getInstance():EquipmentLocator	
			{
				if(locObj==null)
				{
					locObj=new EquipmentLocator();
				}
				return locObj;
			}
			//For the view
			[Bindable]
			public var listObj:ArrayList=new ArrayList();
			[Bindable]
			public var mackObj:ArrayList=new ArrayList();
			public var sblistObj:ArrayList=new ArrayList();
			[Bindable]
			public var currObj:EquipmentVo;
			
			//For the Ws
			public var wordName:String="";
			public var filename:String="";
			public var num:String="";
			public var num1:String="";
			
			public var wsObj:EquipmentVo;
			//Ws call
			public function insert():void
			{
				listObj.addItem(wsObj);
			}
			public function update():void
			{
				var index:int=getVoIndex(wsObj.ID);
				listObj.removeItemAt(index);
				listObj.addItemAt(wsObj,index);
			}	
			public function deleteFunc():void
			{
				listObj.removeItemAt(getVoIndex(wsObj.ID));
			}
			public function getAll(xml:XML):void
			{
				listObj.removeAll();		
				
				for(var i:int=0;i<xml.children().length();i++)
				{
					var voObj:EquipmentVo=new EquipmentVo(xml.children()[i].ID,xml.children()[i].仪器设备名称,xml.children()[i].制造商名称,xml.children()[i].型号规格,
						xml.children()[i].出厂编号,xml.children()[i].设备识别号,xml.children()[i].接受日期,xml.children()[i].启用日期,
						xml.children()[i].目前存放地点,xml.children()[i].接受状态,xml.children()[i].校准时间,xml.children()[i].校准周期,xml.children()[i].使用专业,
						xml.children()[i].管理人,xml.children()[i].维护方式,xml.children()[i].验证方式,xml.children()[i].照片,xml.children()[i].附件,xml.children()[i].仪器类别);
					listObj.addItem(voObj);
				}
			}
			public function getSBAll(xml:XML):void
			{
				sblistObj.removeAll();		
				
				for(var i:int=0;i<xml.children().length();i++)
				{
					var voObj:EquipmentVo=new EquipmentVo(xml.children()[i].ID,xml.children()[i].仪器设备名称,xml.children()[i].制造商名称,xml.children()[i].型号规格,
						xml.children()[i].出厂编号,xml.children()[i].设备识别号,xml.children()[i].接受日期,xml.children()[i].启用日期,
						xml.children()[i].目前存放地点,xml.children()[i].接受状态,xml.children()[i].校准时间,xml.children()[i].校准周期,xml.children()[i].使用专业,
						xml.children()[i].管理人,xml.children()[i].维护方式,xml.children()[i].验证方式,xml.children()[i].照片,xml.children()[i].附件,xml.children()[i].仪器类别);
					sblistObj.addItem(voObj);
				}
			}
			
			
			public function getOne(xml:XML):void
			{
				listObj.removeAll();		
				
				for(var i:int=0;i<xml.children().length();i++)
				{
					var voObj:EquipmentVo=new EquipmentVo(xml.children()[i].ID,xml.children()[i].仪器设备名称,xml.children()[i].制造商名称,xml.children()[i].型号规格,
						xml.children()[i].出厂编号,xml.children()[i].设备识别号,xml.children()[i].接受日期,xml.children()[i].启用日期,
						xml.children()[i].目前存放地点,xml.children()[i].接受状态,xml.children()[i].校准时间,xml.children()[i].校准周期,xml.children()[i].使用专业,
						xml.children()[i].管理人,xml.children()[i].维护方式,xml.children()[i].验证方式,xml.children()[i].照片,xml.children()[i].附件,xml.children()[i].仪器类别);
					listObj.addItem(voObj);
				}
			}
			
			public function mackAllFB (xml:XML):void
			{
				mackObj.removeAll();		
				
				for(var i:int=0;i<xml.children().length();i++)
				{
					var voObj:EquipmentVo=new EquipmentVo(xml.children()[i].ID,xml.children()[i].仪器设备名称,xml.children()[i].制造商名称,xml.children()[i].型号规格,
						xml.children()[i].出厂编号,xml.children()[i].设备识别号,xml.children()[i].接受日期,xml.children()[i].启用日期,
						xml.children()[i].目前存放地点,xml.children()[i].接受状态,xml.children()[i].校准时间,xml.children()[i].校准周期,xml.children()[i].使用专业,
						xml.children()[i].管理人,xml.children()[i].维护方式,xml.children()[i].验证方式,xml.children()[i].照片,xml.children()[i].附件,xml.children()[i].仪器类别);
					mackObj.addItem(voObj);
				}
			}
			//Inner call
			private function getVoIndex(id:String):int
			{
				for(var i:int=0;i<listObj.length;i++)
				{
					var voObj:EquipmentVo=listObj.getItemAt(i) as EquipmentVo;
					if(voObj.ID==id)
					{
						return i;
					}
				}
				return -1;
			}
			
		}
	}