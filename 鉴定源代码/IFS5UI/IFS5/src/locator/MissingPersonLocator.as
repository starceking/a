package locator
{
	import com.adobe.cairngorm.model.ModelLocator;	
	import vo.MissingPersonVo;
	import util.Helper;
	
	public class MissingPersonLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:MissingPersonLocator;
		public static function getInstance():MissingPersonLocator
		{
			if(locObj==null)
			{
				locObj=new MissingPersonLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var curObj:MissingPersonVo;
		public var wsObj:MissingPersonVo;
		//Ws call	
		public function impToOra():void
		{
			curObj.ORA_FLAG="1";
			Helper.showAlert("案件及检材的基本信息已导入，请再点击“导入STR”");
			CodiesLocator.getInstance().orc_imp=true;
		}
		public function update():void
		{
			curObj=wsObj;
			Helper.showAlert("保存成功");
		}
		public function getOne(xml:XML):void
		{
			if(xml.children().length()>0)
			{
				curObj=new MissingPersonVo(xml.children()[0].ID,xml.children()[0].委托编号,
					xml.children()[0].姓名,xml.children()[0].样本类型,
					xml.children()[0].性别,xml.children()[0].人员类型,xml.children()[0].出生日期,xml.children()[0].民族,
					xml.children()[0].国籍,xml.children()[0].身份证,xml.children()[0].学历,xml.children()[0].身份,xml.children()[0].籍贯,
					xml.children()[0].现住址,xml.children()[0].包装情况,xml.children()[0].样本描述,xml.children()[0].备注,xml.children()[0].案件名称,
					xml.children()[0].样本编号,xml.children()[0].ORA_FLAG);
			}
			else
			{
				PsbLocator.getInstance().setMsg("失败，读取不到ID='"+wsObj.ID+"'的失踪人员数据。");
			}
		}
	}
}