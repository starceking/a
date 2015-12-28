<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="InsertPageBreak_Word" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">
        function InsertPageBreak(){
            document.getElementById("PageOfficeCtrl1").RunMacro("AddPage", "sub AddPage() \r\n Application.Selection.InsertBreak(7) \r\n End Sub");
        }

    </script>
    <form id="form1" runat="server">
     <div style="font-size: 12px; line-height: 20px; border-bottom: dotted 1px #ccc; border-top: dotted 1px #ccc;
        padding: 5px;">
        <span style="color: red;">操作说明：</span>手动定位光标到当前文档中要插入分页符的位置，然后点“插入分页符”的按钮。<br />
        关键代码：点右键，选择“查看源文件”，看js函数<span style="background-color: Yellow;">InsertPageBreak()</span>
    </div>

    <div style=" width:auto; height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
