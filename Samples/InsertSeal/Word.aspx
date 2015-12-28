<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="InsertSeal_Word" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>演示：在word文件中插入电子印章</title>
    <style>
        html, body
        {
            height: 100%;
        }
        .main
        {
            height: 100%;
        }
    </style>
        <script type="text/javascript">
            function InsertSeal() {
                try {
                    document.getElementById("PageOfficeCtrl1").ZoomSeal.AddSeal();
                }
                catch (e) { }
            }
            function AddHandSign() {
                document.getElementById("PageOfficeCtrl1").ZoomSeal.AddHandSign();
            }
            function VerifySeal() {
                document.getElementById("PageOfficeCtrl1").ZoomSeal.VerifySeal();
            }
            function ChangePsw() {
                document.getElementById("PageOfficeCtrl1").ZoomSeal.ShowSettingsBox();
            }
    </script>
</head>
<body>

    <div style="font-size: 12px; line-height: 20px; border-bottom: dotted 1px #ccc; border-top: dotted 1px #ccc;
        padding: 5px;">
        <span style="color: red;">操作说明：</span>点“插入印章”按钮即可，插入印章时的用户名为：李志，密码默认为：111111。<br />
        关键代码：点右键，选择“查看源文件”，看js函数<span style="background-color: Yellow;">InsertSeal()</span>
        <p>点击 <a href="../pageoffice/login.aspx">电子印章简易管理平台</a> 添加、删除印章。管理员:admin 密码:111111</p>
    </div>
    <br />
    <form id="form1" runat="server" style="height: 100%;">
    <div class="main">
        <!--**************   PageOffice 客户端代码开始    ************************-->
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl>
        <!--**************   PageOffice 客户端代码结束    ************************-->
    </div>
    </form>
</body>
</html>
