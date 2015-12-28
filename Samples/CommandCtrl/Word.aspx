<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="word" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <p>点击“文件”菜单，可以看到“保存”、“另存为”、“页面设置”、“打印”菜单项已经变灰。保存菜单项不仅变灰，Ctrl+S也被禁用。</p>
    <form id="form1" runat="server">
    <script type="text/javascript">
        function AfterDocumentOpened() {
            document.getElementById("PageOfficeCtrl1").SetEnableFileCommand(3, false); // 禁止保存
            document.getElementById("PageOfficeCtrl1").SetEnableFileCommand(4, false); // 禁止另存
            document.getElementById("PageOfficeCtrl1").SetEnableFileCommand(5, false); //禁止打印
            document.getElementById("PageOfficeCtrl1").SetEnableFileCommand(6, false); // 禁止页面设置
        }
    </script>
    <div style=" width:1000px; height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="False" 
            onload="PageOfficeCtrl1_Load">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
