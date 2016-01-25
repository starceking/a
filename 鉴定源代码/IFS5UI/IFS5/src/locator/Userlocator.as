package locator
{
	import com.adobe.cairngorm.control.CairngormEventDispatcher;
	import com.adobe.cairngorm.model.ModelLocator;
	
	import control.*;
	import mx.core.FlexGlobals;
	
	import util.Helper;
	
	import vo.UserVo;
	public class Userlocator implements ModelLocator
	{
		//Singleton
		private static var locObj:Userlocator;
		public static function getInstance():Userlocator
		{
			if(locObj==null)
			{
				locObj=new Userlocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var userVO:UserVo;
		public function update():void
		{
			Helper.showAlert("修改成功！");
		}
		public function getAll(xml:XML):void//技管查询本单位用户
		{	
			for(var i:int=0;i<xml.children().length();i++)
			{
				Helper.showAlert(xml.children()[i].ID);
				userVO=new UserVo(xml.children()[i].ID,xml.children()[i].单位ID,xml.children()[i].鉴定专业,xml.children()[i].警号,
					xml.children()[i].密码,xml.children()[i].姓名,xml.children()[i].性别,xml.children()[i].身份证,
					xml.children()[i].长号,xml.children()[i].短号,xml.children()[i].技术职称,
					xml.children()[i].角色,xml.children()[i].用户权限,xml.children()[i].是否删除,xml.children()[i].序号,
					xml.children()[i].出生年月, xml.children()[i].参加工作时间,xml.children()[i].民族,xml.children()[i].现任行政职务,
					xml.children()[i].政治面貌,
					xml.children()[i].取得职称时间,
					xml.children()[i].司法鉴定人执业类别,
					xml.children()[i].执业证号,
					xml.children()[i].通讯地址,
					xml.children()[i].邮编,
					xml.children()[i].学历,
					xml.children()[i].学历毕业时间,
					xml.children()[i].学历毕业院校,
					xml.children()[i].学历所学专业,
					xml.children()[i].学位,
					xml.children()[i].学位毕业时间,
					xml.children()[i].学位毕业院校,
					xml.children()[i].学位所学专业,
					xml.children()[i].学术团体,
					xml.children()[i].外语,
					xml.children()[i].重大事故记录);
				
			}
			//return userVO;
			
		}
		//Inner call
		
	}
}