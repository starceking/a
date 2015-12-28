<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OpenWord.aspx.cs" Inherits="OpenWord_OpenWord" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在线打开Word文件</title>
    <script type="text/javascript">

    </script>
</head>
<body>
    <script type="text/javascript">
        function AfterDocumentOpened() {
            document.getElementById("PageOfficeCtrl1").SetEnableFileCommand(4, false);  //禁止另存
            document.getElementById("PageOfficeCtrl1").SetEnableFileCommand(5, false);  //禁止打印
            document.getElementById("PageOfficeCtrl1").SetEnableFileCommand(6, false);  //禁止页面设置
            document.getElementById("PageOfficeCtrl1").SetEnableFileCommand(8, false);  //禁止打印预览
        }
    </script>
    <form id="form1" runat="server">
    <div style="width:auto; height: 700px;" >
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" Theme="Office2010">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
