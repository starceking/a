<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OpenWord.aspx.cs" Inherits="CreateWord_OpenWord" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../images/csstg.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function Save() {
            document.getElementById("PageOfficeCtrl1").WebSave();
            //alert('保存成功！');
        }
    </script>

</head>
<body>
    <form id="form2" runat="server">
    <div id="header">
        <div style="float: left; margin-left: 20px;">
            <img src="../images/logo.jpg" height="30" /></div>
        <ul>
            <li><a href="#">卓正网站</a></li>
            <li><a href="#">客户问吧</a></li>
            <li><a href="#">在线帮助</a></li>
            <li><a href="#">联系我们</a></li>
        </ul>
    </div>
    <div id="content">
        <div id="textcontent" style="width: 1000px; height: 800px;">
            <div class="flow4">
                <a href="word-lists.aspx">
                    <img alt="返回" src="../images/return.gif" border="0" />文件列表</a> <span style="width: 100px;">
                    </span><strong>文档主题：</strong> <span style="color: Red;">
                        <asp:Literal ID="Literal_Subject" runat="server"></asp:Literal></span> <span style="width: 100px;">
                        </span>
            </div>
            <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="True" 
                Menubar="False" onload="PageOfficeCtrl1_Load">
            </po:PageOfficeCtrl>
        </div>
    </div>
    <div id="footer">
        <hr width="1000" />
        <div>
            Copyright (c) 2012 北京卓正志远软件有限公司</div>
    </div>
    </form>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</body>
</html>
