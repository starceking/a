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
                    发布时间.Text = nm.create_time.ToString("yyyy年MM月dd日");
                    content1.Text = nm.content;
                }
            }
        }
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
</head>
<body>
    <form id="example" runat="server">
        <table width="100%">
            <tr>
                <td class="table_value_col">
                    <center>
                    <asp:Label ID="标题" runat="server" Font-Size="30px"></asp:Label></center>
                </td>
            </tr>
            <tr>
                <td class="table_value_col" align="right">
                    <asp:Label ID="发布时间" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="content1" runat="server"></asp:Label>
    </form>
</body>
</html>
