<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WordAddBKMK.aspx.cs" Inherits="WordAddBKMK" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在word当前光标处插入书签</title>

</head>
<body>
    <form id="form1" runat="server">
    <div style=" font-size:small; color:Red;">
        <asp:Label ID="Label4" runat="server" Text="关键代码：点右键，选择“查看源文件”，看js函数："></asp:Label>
        <asp:Label ID="Label5" runat="server" Text="function  addBookMark() 和 function delBookMark()"></asp:Label>
        <br/>
    <asp:Label ID="Label3" runat="server" Text="插入书签时，请先输入要插入的书签名称和文本；删除书签时，请先输入相应的书签名称！"></asp:Label><br />
        <asp:Label ID="Label1" runat="server" Text="书签名称："></asp:Label><input id="txtBkName" type="text" value="test" />
        &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="书签文本："></asp:Label><input id="txtBkText" type="text" value="[测试]" />
    </div>
    <input id="Button1" type="button" onclick="addBookMark();" value="插入书签" />
    <input id="Button2" type="button" onclick="delBookMark()" value="删除书签" />
    <div style=" width:auto; height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" Menubar="False">
        </po:PageOfficeCtrl>
    </div>
    </form>
    
    <script type="text/javascript">
        //        var range;
        //    function  addBookMark()
        //    {
        //        var obj = document.getElementById("PageOfficeCtrl1").Document;
        //        var bkName = document.getElementById("txtBkName").value;
        //        var bkText = document.getElementById("txtBkText").value;
        //        range = obj.Application.Selection.Range;
        //        range.Text = bkText;
        //        obj.Bookmarks.Add(bkName,  range);
        //        obj.Bookmarks(bkName).Select();
        //    }
        //    function delBookMark()
        //    {
        //        var bkName = document.getElementById("txtBkName").value;
        //        var obj = document.getElementById("PageOfficeCtrl1").Document;
        //        range = obj.Application.Selection.Range;
        //        if(obj.Bookmarks.Exists(bkName)){
        //            obj.Bookmarks(bkName).Select();
        //            obj.Application.Selection.Range.Text = "";
        //        }
        //
        //    }

        var bkName = document.getElementById("txtBkName").value;
        var bkText = document.getElementById("txtBkText").value;


        function addBookMark() {
            var mac = "Function myfunc()" + " \r\n"
                    + "Dim r As Range " + " \r\n"
                    + "Set r = Application.Selection.Range " + " \r\n"
                    + "r.Text = \"" + bkText + "\"" + " \r\n"
                    + "Application.ActiveDocument.Bookmarks.Add Name:=\"" + bkName + "\", Range:=r " + " \r\n"
                    + "Application.ActiveDocument.Bookmarks(\"" + bkName + "\").Select " + " \r\n"
                    + "End Function " + " \r\n";
            document.getElementById("PageOfficeCtrl1").RunMacro("myfunc", mac);
        }
        function delBookMark() {
            var mac = "Function myfunc()" + " \r\n"
                    + "If  Application.ActiveDocument.Bookmarks.Exists(\"" + bkName + "\") Then " + " \r\n"
                    + "    Application.ActiveDocument.Bookmarks(\"" + bkName + "\").Select " + " \r\n"
                    + "    Application.Selection.Range.Text = \"\" " + " \r\n"
                    + "End If " + " \r\n"
                    + "End Function " + " \r\n";
            document.getElementById("PageOfficeCtrl1").RunMacro("myfunc", mac);
        }
    </script>
</body>
</html>
