using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using DAL;
using System.Collections.Generic;
using LIB;

namespace WS
{
    /// <summary>
    /// ok
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class 系统用户WS : System.Web.Services.WebService
    {
        /// <summary>
        /// 技管添加
        /// 委托用户注册
        /// </summary>
        [WebMethod]
        public string Insert(string ID, string 单位ID, string 鉴定专业, string 警号, string 密码, string 姓名,
            string 性别, string 身份证, string 长号, string 短号, string 技术职称, string 角色, string 用户权限
            , string 序号, string 现任行政职务,string 出生日期, string 地址, string 家庭电话, string 联系电话, string 民族, string 政治面貌,
            string 参加工作时间, string 专业技术职称1, string 取得时间1, string 专业技术职称2, string 取得时间2,
            string 现从事专业, string 司法鉴定人执业类别, string 执业证号,string 籍贯)
        {
            if (!ValidUser(ID, 警号, 身份证))
            {
                return "您输入的警号或身份证已被别人占用，请核实！";
            }

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("ID", ID);
            dict.Add("单位ID", 单位ID);
            dict.Add("鉴定专业", 鉴定专业);
            dict.Add("警号", 警号);
            dict.Add("密码", 密码);
            dict.Add("姓名", 姓名);
            dict.Add("性别", 性别);
            dict.Add("身份证", 身份证);
            dict.Add("长号", 长号);
            dict.Add("短号", 短号);
            dict.Add("技术职称", 技术职称);
            dict.Add("角色", 角色);
            dict.Add("用户权限", 用户权限);
            dict.Add("序号", 序号);
            dict.Add("现任行政职务", 现任行政职务);
            dict.Add("出生日期", 出生日期);
            dict.Add("地址", 地址);
            dict.Add("家庭电话", 家庭电话);
            dict.Add("联系电话", 联系电话);
            dict.Add("民族", 民族);
            dict.Add("政治面貌", 政治面貌);
            dict.Add("参加工作时间", 参加工作时间);
            dict.Add("专业技术职称1", 专业技术职称1);
            dict.Add("取得时间1", 取得时间1);
            dict.Add("专业技术职称2", 专业技术职称2);
            dict.Add("取得时间2", 取得时间2);
            dict.Add("现从事专业", 现从事专业);
            dict.Add("司法鉴定人执业类别", 司法鉴定人执业类别);
            dict.Add("执业证号", 执业证号);
            dict.Add("籍贯", 籍贯);
            return DBHelperSQL.Insert("系统用户", dict);
        }
        /// <summary>
        /// 技管修改、删除
        /// 个人修改
        /// </summary>
        [WebMethod]
        public string Update(string ID, string 单位ID, string 鉴定专业, string 警号, string 密码, string 姓名,
            string 性别, string 身份证, string 长号, string 短号, string 技术职称, string 角色, string 用户权限, string 是否删除, string 序号,
            string 现任行政职务, string 出生日期, string 地址, string 家庭电话, string 联系电话, string 民族, string 政治面貌,
            string 参加工作时间, string 专业技术职称1, string 取得时间1, string 专业技术职称2, string 取得时间2,
            string 现从事专业, string 司法鉴定人执业类别, string 执业证号,string 籍贯)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            if (是否删除.Equals("0"))
            {
                if (!ValidUser(ID, 警号, 身份证))
                {
                    return "您输入的警号或身份证已被别人占用，请核实！";
                }
                dict.Add("单位ID", 单位ID);
                dict.Add("鉴定专业", 鉴定专业);
                dict.Add("警号", 警号);
                dict.Add("密码", 密码);
                dict.Add("姓名", 姓名);
                dict.Add("性别", 性别);
                dict.Add("身份证", 身份证);
                dict.Add("长号", 长号);
                dict.Add("短号", 短号);
                dict.Add("技术职称", 技术职称);
                dict.Add("角色", 角色);
                dict.Add("用户权限", 用户权限);
                dict.Add("现任行政职务", 现任行政职务);
                dict.Add("出生日期", 出生日期);
                dict.Add("地址", 地址);
                dict.Add("家庭电话", 家庭电话);
                dict.Add("联系电话", 联系电话);
                dict.Add("民族", 民族);
                dict.Add("政治面貌", 政治面貌);
                dict.Add("参加工作时间", 参加工作时间);
                dict.Add("专业技术职称1", 专业技术职称1);
                dict.Add("取得时间1", 取得时间1);
                dict.Add("专业技术职称2", 专业技术职称2);
                dict.Add("取得时间2", 取得时间2);
                dict.Add("现从事专业", 现从事专业);
                dict.Add("司法鉴定人执业类别", 司法鉴定人执业类别);
                dict.Add("执业证号", 执业证号);
                dict.Add("籍贯", 籍贯);
            }
            dict.Add("是否删除", 是否删除);
            dict.Add("序号", 序号);
            return DBHelperSQL.Update("系统用户", "ID='" + ID + "'", dict);
        }
         [WebMethod]
        public string UpdateJBXX(string ID,string 身份证, string 现任行政职务, string 出生日期, string 地址,
            string 家庭电话, string 联系电话, string 民族, string 政治面貌,
            string 参加工作时间, string 专业技术职称1, string 取得时间1, string 专业技术职称2, string 取得时间2,
            string 现从事专业, string 司法鉴定人执业类别, string 执业证号, string 籍贯)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("身份证", 身份证);
            dict.Add("现任行政职务", 现任行政职务);
            dict.Add("出生日期", 出生日期);
            dict.Add("地址", 地址);
            dict.Add("家庭电话", 家庭电话);
            dict.Add("联系电话", 联系电话);
            dict.Add("民族", 民族);
            dict.Add("政治面貌", 政治面貌);
            dict.Add("参加工作时间", 参加工作时间);
            dict.Add("专业技术职称1", 专业技术职称1);
            dict.Add("取得时间1", 取得时间1);
            dict.Add("专业技术职称2", 专业技术职称2);
            dict.Add("取得时间2", 取得时间2);
            dict.Add("现从事专业", 现从事专业);
            dict.Add("司法鉴定人执业类别", 司法鉴定人执业类别);
            dict.Add("执业证号", 执业证号);
            dict.Add("籍贯", 籍贯);
            return DBHelperSQL.Update("系统用户", "ID='" + ID + "'", dict);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        [WebMethod]
        public string Login(string 警号, string 密码)
        {
            return DBHelperSQL.Select("系统用户视图", "警号='" + 警号 + "' and 密码='" + 密码 + "' and 是否删除='0'", "ID", "*").GetXml();
        }
        /// <summary>
        /// 技管查询
        /// </summary>
        [WebMethod]
        public string GetAll(string 单位ID, string 鉴定专业, string 警号, string 姓名)
        {
            string filter = "是否删除='0' and 单位ID='" + 单位ID + "' and ";
            if (鉴定专业.Length > 0) filter += "鉴定专业='" + 鉴定专业 + "' and ";
            if (警号.Length > 0) filter += "警号='" + 警号 + "' and ";
            if (姓名.Length > 0) filter += "姓名='" + 姓名 + "' and ";

            return DBHelperSQL.Select("系统用户视图", Helper.CutFilter(filter), "序号", "*").GetXml();
        }
        [WebMethod]
        public string GetDeletedPerson(string 警号, string 身份证, string pageSize, string pageIndex)
        {
            string filter = "是否删除='1' and ";
            if (警号.Length > 0) filter += "警号='" + 警号 + "' and ";
            if (身份证.Length > 0) filter += "身份证='" + 身份证 + "' and ";

            return DBHelperSQL.SelectRowCount("系统用户视图", Helper.CutFilter(filter), "姓名", "*",
                Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string GetConsignPerson(string 单位ID, string 警号, string 姓名, string pageSize, string pageIndex)
        {
            string filter = "是否删除='0' and 角色='Consign' and ";
            if (单位ID.Length > 0) filter += "单位ID='" + 单位ID + "' and ";
            if (警号.Length > 0) filter += "警号='" + 警号 + "' and ";
            if (姓名.Length > 0) filter += "姓名='" + 姓名 + "' and ";

            return DBHelperSQL.SelectRowCount("系统用户视图", Helper.CutFilter(filter), "姓名", "*",
                Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        /// <summary>
        /// 辅助
        /// </summary>
        private bool ValidUser(string ID, string 警号, string 身份证)
        {
            return (DBHelperSQL.SelectRowCount("系统用户视图", "ID<>'" + ID + "' AND (警号='" + 警号 + "' or 身份证='" + 身份证 + "') AND 是否删除='0'", "ID") == 0);
        }
    }
}
