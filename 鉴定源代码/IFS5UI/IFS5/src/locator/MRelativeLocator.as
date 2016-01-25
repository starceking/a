package locator
{
	import com.adobe.cairngorm.model.ModelLocator;	
	import vo.MRelativeVo;
	import util.Helper;
	
	public class MRelativeLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:MRelativeLocator;
		public static function getInstance():MRelativeLocator
		{
			if(locObj==null)
			{
				locObj=new MRelativeLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var curObj:MRelativeVo;
		public var wsObj:MRelativeVo;
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
				curObj=new MRelativeVo(xml.children()[0].ID,xml.children()[0].委托编号,xml.children()[0].亲属关系,
					xml.children()[0].亲属一ID,xml.children()[0].亲属二ID,xml.children()[0].出生日期,xml.children()[0].性别,
					xml.children()[0].特殊特征,xml.children()[0].体表标记,xml.children()[0].姓名,xml.children()[0].案件名称,
					xml.children()[0].简要案情,
					xml.children()[0].亲属一姓名,xml.children()[0].亲属一样本类型,
					xml.children()[0].亲属一性别,xml.children()[0].亲属一人员类型,xml.children()[0].亲属一出生日期,xml.children()[0].亲属一民族,
					xml.children()[0].亲属一国籍,xml.children()[0].亲属一身份证,xml.children()[0].亲属一学历,xml.children()[0].亲属一身份,xml.children()[0].亲属一籍贯,
					xml.children()[0].亲属一现住址,xml.children()[0].亲属一包装情况,xml.children()[0].亲属一样本描述,xml.children()[0].亲属一备注,
					xml.children()[0].亲属一目标关系,xml.children()[0].亲属一样本编号,
					xml.children()[0].亲属二姓名,xml.children()[0].亲属二样本类型,
					xml.children()[0].亲属二性别,xml.children()[0].亲属二人员类型,xml.children()[0].亲属二出生日期,xml.children()[0].亲属二民族,
					xml.children()[0].亲属二国籍,xml.children()[0].亲属二身份证,xml.children()[0].亲属二学历,xml.children()[0].亲属二身份,xml.children()[0].亲属二籍贯,
					xml.children()[0].亲属二现住址,xml.children()[0].亲属二包装情况,xml.children()[0].亲属二样本描述,xml.children()[0].亲属二备注,
					xml.children()[0].亲属二目标关系,xml.children()[0].亲属二样本编号,xml.children()[0].ORA_FLAG);
			}
			else
			{
				PsbLocator.getInstance().setMsg("失败，读取不到ID='"+wsObj.ID+"'的失踪人员亲属数据。");
			}
		}
	}
}