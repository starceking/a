using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OpenWord_OpenWord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageOffice组件的引用
        //首先确保已安装了pageoffice服务器端组件，且在项目中已添加了pageoffice文件夹，
        //在该文件夹中已存在posetup.exe和server.aspx服务器端页面，再在前台页面中引入PageOfficeCtrl控件

        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        #region 设置打开参数
        //页面标题
        string caption = Request.QueryString["caption"];
        if (string.IsNullOrWhiteSpace(caption)) caption = "文档查看";
        //打开人的姓名
        string opener = Request.QueryString["opener"];
        if (string.IsNullOrWhiteSpace(opener)) opener = "匿名" + DateTime.Now.ToString("yyyyMMddHHmmss");
        //word模板的硬盘地址，新建模式必须传-----------------------------------
        string template = Request.QueryString["template"];
        //要打开的word的硬盘地址-----------------------------------------------
        string url = Request.QueryString["url"];
        if (string.IsNullOrWhiteSpace(url)) return;
        url = GetFile(url);
        //打开的模式：只读、新建、编辑
        PageOffice.OpenModeType omt = PageOffice.OpenModeType.docReadOnly;
        string mode = Request.QueryString["mode"];
        if (!string.IsNullOrWhiteSpace(mode))
        {
            // 设置保存文件页面
            PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
            if (mode.Equals("ins"))//新建模式
            {
                //新建word就要先从template复制一份到url
                if (string.IsNullOrWhiteSpace(template)) return;
                template = GetTemplate(template);
                if (!File.Exists(template)) return;
                try
                {
                    File.Copy(template, url);
                }
                catch
                {
                    return;
                }
            }
            else if (mode.Equals("edit"))//普通编辑，一般用于一检
            {
                omt = PageOffice.OpenModeType.docNormalEdit;
            }
            else if (mode.Equals("revision"))//痕迹模式，一般用于审核、审批
            {
                omt = PageOffice.OpenModeType.docRevisionOnly;//同时只能一个人编辑
                PageOfficeCtrl1.TimeSlice = 20; // 设置并发控制时间, 单位:分钟
                PageOfficeCtrl1.JsFunction_AfterDocumentOpened = "AfterDocumentOpened()";
            }
        }
        #endregion
        #region 此处填充word内容，该处代码根据实际情况编写
        DataRegionFill("PO_userName", "王五");
        SetTable();
        SetTag("{部门名}", "技术");
        SetTag("{姓名}", "李志");
        SetTag("【时间】", DateTime.Now.ToString("yyyy-MM-dd"));
        SetPic("PO_p1", @"C:\dev src\pageoffice\OpenWord\doc\1.jpg");
        #endregion
        //打开
        PageOfficeCtrl1.Caption = caption;
        //在只读模式下工具条和菜单栏都已不起作用，不需要显示
        if (omt == PageOffice.OpenModeType.docReadOnly)
        {
            PageOfficeCtrl1.Titlebar = false; //隐藏标题栏
            PageOfficeCtrl1.Menubar = false; //隐藏菜单栏
            PageOfficeCtrl1.OfficeToolbars = false; //隐藏Office工具栏
            PageOfficeCtrl1.CustomToolbar = false; //隐藏自定义工具栏
            PageOfficeCtrl1.AllowCopy = false;//禁止拷贝
        }
        //打开文件
        PageOfficeCtrl1.WebOpen(url, omt, opener);
    }
    #region 此处填充word内容，该处代码根据实际情况编写
    /// <summary>
    /// 填充书签
    /// </summary>
    /// <param name="name">书签名前缀必须为PO_，例如：PO_userName</param>
    /// <param name="value"></param>
    void DataRegionFill(string name, string value)
    {
        PageOffice.WordWriter.WordDocument wordDoc = new PageOffice.WordWriter.WordDocument();//后面的WordWriter会覆盖前面的，使用中注意
        //获取数据区域对象后赋值
        PageOffice.WordWriter.DataRegion dr = wordDoc.OpenDataRegion(name);
        if (dr != null)
            dr.Value = value;
        PageOfficeCtrl1.SetWriter(wordDoc);
    }
    /// <summary>
    /// 操作表格
    /// </summary>
    void SetTable()
    {
        PageOffice.WordWriter.WordDocument doc = new PageOffice.WordWriter.WordDocument();
        //获取Table所在的数据区域对象
        PageOffice.WordWriter.DataRegion dataRegion = doc.OpenDataRegion("PO_regTable");
        if (dataRegion == null) return;
        //打开table，OpenTable(index)方法中的index代表Word文档中table位置的索引，从1开始
        PageOffice.WordWriter.Table table = dataRegion.OpenTable(1);
        //给table中的单元格赋值， OpenCellRC(行, 列)
        table.OpenCellRC(3, 1).Value = "A公司";
        table.OpenCellRC(3, 2).Value = "开发部";
        table.OpenCellRC(3, 3).Value = "李清";
        //插入一空行，InsertRowAfter方法中的参数表示在哪个单元格下面插入一行
        table.InsertRowAfter(table.OpenCellRC(3, 3));
        table.OpenCellRC(4, 1).Value = "B公司";
        table.OpenCellRC(4, 2).Value = "销售部";
        table.OpenCellRC(4, 3).Value = "张三";
        PageOfficeCtrl1.SetWriter(doc);
    }
    /// <summary>
    /// 使用数据标签，比书签更好用
    /// </summary>
    void SetTag(string name, string value)
    {
        //定义WordDocument对象
        PageOffice.WordWriter.WordDocument doc = new PageOffice.WordWriter.WordDocument();
        //定义DataTag对象
        PageOffice.WordWriter.DataTag tag = doc.OpenDataTag(name);
        if (tag != null)
            tag.Value = value;
        PageOfficeCtrl1.SetWriter(doc);
    }
    /// <summary>
    /// 插入图片
    /// </summary>
    void SetPic(string name, string value)
    {
        PageOffice.WordWriter.WordDocument worddoc = new PageOffice.WordWriter.WordDocument();
        //先在要插入word文件的位置手动插入书签,书签必须以“PO_”为前缀
        //给DataRegion赋值,值的形式为："[word]word文件路径[/word]"
        if (!File.Exists(value)) return;
        PageOffice.WordWriter.DataRegion dataReg = worddoc.OpenDataRegion(name);
        if (dataReg != null)
            dataReg.Value = "[image]" + value + "[/image]";
        PageOfficeCtrl1.SetWriter(worddoc);
    }
    #endregion
    #region 辅助
    /// <summary>
    /// 获取word模板的路径，该函数根据实际情况编写
    /// </summary>
    string GetTemplate(string name)
    {
        return @"C:\dev src\pageoffice\OpenWord\doc\" + name + ".doc";
    }
    /// <summary>
    /// 获取目标word的路径，该函数根据实际情况编写
    /// </summary>
    string GetFile(string name)
    {
        return @"C:\dev src\pageoffice\OpenWord\doc\" + name + ".doc";
    }
    #endregion
}
