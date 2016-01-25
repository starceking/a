package locator
{
	import com.adobe.cairngorm.control.CairngormEventDispatcher;
	import com.adobe.cairngorm.model.ModelLocator;
	
	import control.*;
	
	import mx.collections.ArrayList;
	import mx.core.FlexGlobals;
	
	import spark.components.TextInput;
	
	import util.Helper;
	
	import vo.SysUserVo;
	
	public class SysUserLocator implements ModelLocator
	{
		//Singleton
		private static var locObj:SysUserLocator;
		public static function getInstance():SysUserLocator
		{
			if(locObj==null)
			{
				locObj=new SysUserLocator();
			}
			return locObj;
		}
		//For the view
		[Bindable]
		public var loginUser:SysUserVo;
		[Bindable]
		public var userList:ArrayList=new ArrayList();
		[Bindable]
		public var allUserList:ArrayList=new ArrayList();
		public var listPager:ListPager;
		//For the ws
		[Bindable]
		public var curObj:SysUserVo;
		public var wsObj:SysUserVo;
		[Bindable]
		public var SongJian:Boolean=false;
		//用户名
		public var UserNo:TextInput;
		//Ws call
		public function login(xml:XML):void
		{	
			if(xml.children().length()==0)
			{
				Helper.showAlert("警号或密码错误！");
				return;
			}
			
			loginUser=new SysUserVo(xml.children()[0].ID,xml.children()[0].单位ID,xml.children()[0].鉴定专业,xml.children()[0].警号,
				xml.children()[0].密码,xml.children()[0].姓名,xml.children()[0].性别,xml.children()[0].身份证,
				xml.children()[0].长号,xml.children()[0].短号,xml.children()[0].技术职称,
				xml.children()[0].角色,xml.children()[0].用户权限,xml.children()[0].是否删除,xml.children()[0].序号,xml.children()[0].现任行政职务,
				xml.children()[0].出生日期,xml.children()[0].地址,xml.children()[0].家庭电话,xml.children()[0].联系电话,
				xml.children()[0].民族,xml.children()[0].政治面貌,xml.children()[0].参加工作时间,xml.children()[0].专业技术职称1,
				xml.children()[0].取得时间1,xml.children()[0].专业技术职称2,xml.children()[0].取得时间2,xml.children()[0].现从事专业,
				xml.children()[0].司法鉴定人执业类别,xml.children()[0].执业证号,xml.children()[0].籍贯,xml.children()[0].单位父单位,xml.children()[0].单位类型,
				xml.children()[0].单位编号,xml.children()[0].单位名称,xml.children()[0].单位地址,xml.children()[0].单位邮编,
				xml.children()[0].单位简称,xml.children()[0].单位电话);
			
			if(loginUser.PSBID!=PsbLocator.getInstance().ID_PSB_ID)
			{
				if(loginUser.ROLES!="Consign")
				{
					Helper.showAlert("您登录到了其他单位，自动将您的身份转为委托用户");
					loginUser.ROLES="Consign";
				}
			}
			
			if(loginUser.ROLES.search("，")>0)
			{
				loginUser.CURRENT_ROLE=loginUser.ROLES.split('，')[0];
			}
			else
			{
				loginUser.CURRENT_ROLE=loginUser.ROLES;
			}	
			if(FlexGlobals.topLevelApplication.rcRgp!=null)
				FlexGlobals.topLevelApplication.rcRgp.setRole(loginUser.ROLES);
			//PsbLocator.getInstance().loginPsb=PsbLocator.getInstance().getSinglePsb(loginUser.PSBID);
			
			Helper.changeTopState(loginUser.CURRENT_ROLE);
			
			wsObj=new SysUserVo("",loginUser.PSBID,"","","","","","","","","","","",
				"","","","","","","","","","","","","","","","","","","","","","","","","","");
			CairngormEventDispatcher.getInstance().dispatchEvent(new IFSEvent(IFSControl.SYSUSERWS_GetAll));
			CairngormEventDispatcher.getInstance().dispatchEvent(new IFSEvent(IFSControl.EXCASEWS_GetTaskRemind));

		} 
		public function insert():void
		{
			if(wsObj.ROLES!="Consign")//技管添加人员
			{
				userList.addItemAt(wsObj,0);
			}
			else//委托用户注册成功
			{
				Helper.showAlert("注册成功！请返回并登录。");
			}
		}
		public function update():void
		{
			if(wsObj.ID==loginUser.ID)//用户自己修改个人信息
			{
				//if(loginUser.PSBID!=wsObj.PSBID)
				//{
				//	PsbLocator.getInstance().loginPsb=PsbLocator.getInstance().getSinglePsb(wsObj.PSBID);
				//}
				
				loginUser=wsObj;
			}
			else if(wsObj.ROLES!="Consign")//技管修改用户
			{
				var index:int=getUserIndex(wsObj.ID);
				if(index!=-1)
				{
					userList.removeItemAt(index);
					if(wsObj.DELETED=="0")userList.addItemAt(wsObj,index);
				}
			}
			Helper.showAlert("修改成功！");
		}
		public function updatejbxx():void
		{
			var index:int=getUserIndex(wsObj.ID);
			userList.removeItemAt(index);
			userList.addItemAt(wsObj,index);
			Helper.showAlert("修改成功！");
		}
		public function getAll(xml:XML):void
		{	
			userList.removeAll();
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:SysUserVo=new SysUserVo(xml.children()[i].ID,xml.children()[i].单位ID,xml.children()[i].鉴定专业,xml.children()[i].警号,
					xml.children()[i].密码,xml.children()[i].姓名,xml.children()[i].性别,xml.children()[i].身份证,
					xml.children()[i].长号,xml.children()[i].短号,xml.children()[i].技术职称,
					xml.children()[i].角色,xml.children()[i].用户权限,xml.children()[i].是否删除,xml.children()[i].序号,xml.children()[i].现任行政职务,
					xml.children()[i].出生日期,xml.children()[i].地址,xml.children()[i].家庭电话,xml.children()[i].联系电话,
					xml.children()[i].民族,xml.children()[i].政治面貌,xml.children()[i].参加工作时间,xml.children()[i].专业技术职称1,
					xml.children()[i].取得时间1,xml.children()[i].专业技术职称2,xml.children()[i].取得时间2,xml.children()[i].现从事专业,
					xml.children()[i].司法鉴定人执业类别,xml.children()[i].执业证号,xml.children()[i].籍贯,xml.children()[i].单位父单位,xml.children()[i].单位类型,
					xml.children()[i].单位编号,xml.children()[i].单位名称,xml.children()[i].单位地址,xml.children()[i].单位邮编,
					xml.children()[i].单位简称,xml.children()[i].单位电话);
				userList.addItem(voObj);
			}
		}
		public function getOtherPerson(xml:XML):void//恢复被删除的用户
		{	
			allUserList.removeAll();
			for(var i:int=0;i<xml.children().length();i++)
			{
				var voObj:SysUserVo=new SysUserVo(xml.children()[i].ID,xml.children()[i].单位ID,
					xml.children()[i].鉴定专业,xml.children()[i].警号,
					xml.children()[i].密码,xml.children()[i].姓名,xml.children()[i].性别,xml.children()[i].身份证,
					xml.children()[i].长号,xml.children()[i].短号,xml.children()[i].技术职称,
					xml.children()[i].角色,xml.children()[i].用户权限,xml.children()[i].是否删除,xml.children()[i].序号,xml.children()[i].现任行政职务,
					xml.children()[i].出生日期,xml.children()[i].地址,xml.children()[i].家庭电话,xml.children()[i].联系电话,
					xml.children()[i].民族,xml.children()[i].政治面貌,xml.children()[i].参加工作时间,xml.children()[i].专业技术职称1,
					xml.children()[i].取得时间1,xml.children()[i].专业技术职称2,xml.children()[i].取得时间2,xml.children()[i].现从事专业,
					xml.children()[i].司法鉴定人执业类别,xml.children()[i].执业证号,xml.children()[i].籍贯,xml.children()[i].单位父单位,xml.children()[i].单位类型,
					xml.children()[i].单位编号,xml.children()[i].单位名称,xml.children()[i].单位地址,xml.children()[i].单位邮编,
					xml.children()[i].单位简称,xml.children()[i].单位电话);
				allUserList.addItem(voObj);
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
		public function getUsers(office:String="",role:String="",auth:String=""):ArrayList
		{
			var list:ArrayList=new ArrayList();
			for(var i:int=0;i<userList.length;i++)
			{
				var voObj:SysUserVo=userList.getItemAt(i) as SysUserVo;
				if(((office.length==0)||voObj.OFFICE==office)&&
					(role.length==0||voObj.ROLES.search(role)>=0)&&
					(voObj.AUTH.search(auth)>=0))
				{
					list.addItem({label:voObj.NAME,data:voObj.ID});
				}
			}
			if(auth=="授权签字")
			{
				if(office=="理化")
				{
					list.addItem({label:"姜卸印",data:"D65733F9498AB40DE5B3D9C1B1C71195"});
				}
				else if(office=="DNA")
				{
					list.addItem({label:"姜卸印",data:"D65733F9498AB40DE5B3D9C1B1C71195"});
					list.addItem({label:"胡朝阳",data:"58FD63441924E08CFFABD9C4AD1514E0"});
				}
			}
			return list;
		}
		public function getUserName(ID:String):String
		{
			for(var i:int=0;i<userList.length;i++)
			{
				if(userList.getItemAt(i).ID==ID)return userList.getItemAt(i).NAME;
			}
			return "";
		}
		public function getUserVo(ID:String):SysUserVo
		{
			for(var i:int=0;i<userList.length;i++)
			{
				if(userList.getItemAt(i).ID==ID)return userList.getItemAt(i) as SysUserVo;
			}
			return null;
		}
		//Inner call
		private function getUserIndex(ID:String):int
		{
			for(var i:int=0;i<userList.length;i++)
			{
				if(userList.getItemAt(i).ID==ID)return i;
			}
			return -1;
		}
	}
}