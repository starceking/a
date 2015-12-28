<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="word" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Word文件另存为MHT</title>
    <script type="text/javascript">
        function saveAsMHT() {
            document.getElementById("PageOfficeCtrl1").WebSaveAsMHT();
            document.getElementById("PageOfficeCtrl1").Alert("MHT格式的文件已经保存到服务器上。");
            window.open("doc/test.mht" + "?r=" + Math.random());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" width:1000px; height:800px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" 
            onload="PageOfficeCtrl1_Load">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
