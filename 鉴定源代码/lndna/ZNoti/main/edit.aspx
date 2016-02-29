<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                Model.NoticeModel nm = DBOper.Notice.GetOneSync(Convert.ToInt32(Request.QueryString["id"]));
                if (nm != null)
                {
                    标题.Text = nm.head;
                    content1.InnerText = nm.content;
                }
            }
        }
    }
    protected void Save(object sender, EventArgs e)
    {
        if (标题.Text.Trim().Length == 0) return;
        if (Request.Form["content1"].Trim().Length == 0) return;

        if (Request.QueryString["id"] != null)
        {
            DBOper.Notice.Update(Convert.ToInt32(Request.QueryString["id"]), 标题.Text, Request.Form["content1"]);
        }
        else
        {
            DBOper.Notice.Insert(标题.Text, Request.Form["content1"]);
        }
        msg.Text = "发布成功";
    }
    protected void Back(object sender, EventArgs e)
    {
        Response.Redirect("http://" + Request.ServerVariables.Get("Local_Addr") + "/gl/sys_user/notice.htm");
    }
</script>
<!doctype html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <title>通知通告</title>
    <link href="../css/table.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../themes/default/default.css" />
    <link rel="stylesheet" href="../plugins/code/prettify.css" />
    <script charset="utf-8" src="../kindeditor.js"></script>
    <script charset="utf-8" src="../lang/zh_CN.js"></script>
    <script charset="utf-8" src="../plugins/code/prettify.js"></script>
    <script>
        KindEditor.ready(function (K) {
            var editor1 = K.create('#content1', {
                cssPath: '../plugins/code/prettify.css',
                uploadJson: '../main/upload_json.ashx',
                fileManagerJson: '../main/file_manager_json.ashx',
                allowFileManager: true,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                }
            });
            prettyPrint();
        });
    </script>
</head>
<body>
    <form id="example" runat="server">
        <table width="700px">
            <tr>
                <td align="right" class="table_label_col" style="width: 100px">通告标题：
                </td>
                <td class="table_value_col" style="width: 600px">
                    <asp:TextBox ID="标题" Width="99%" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <textarea id="content1" cols="100" rows="8" style="width: 100%; height: 520px; visibility: hidden;"
            runat="server"></textarea>
        <br />
        <table width="700px">
            <tr>
                <td>
                    <center>
                    <asp:Button ID="Button1" runat="server" Text="提交内容" OnClick="Save" />&nbsp;
                    <asp:Label ID="msg" runat="server"></asp:Label>&nbsp;
        <asp:Button ID="Button2" runat="server" Text="返回" OnClick="Back" />
                    </center>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
