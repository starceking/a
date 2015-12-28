<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="CustomToolButton_Word" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">
        function mySave() {
            document.getElementById("PageOfficeCtrl1").WebSave();
        }
        
        // 隐藏/显示 标题栏
        function Button1_onclick() {
            var bVisible = document.getElementById("PageOfficeCtrl1").Titlebar;
            document.getElementById("PageOfficeCtrl1").Titlebar = !bVisible;
        }
        
        // 隐藏/显示 菜单栏
        function Button2_onclick() {
            var bVisible = document.getElementById("PageOfficeCtrl1").Menubar;
            document.getElementById("PageOfficeCtrl1").Menubar = !bVisible;
        }


        // 隐藏/显示 自定义工具栏
        function Button3_onclick() {
            var bVisible = document.getElementById("PageOfficeCtrl1").CustomToolbar;
            document.getElementById("PageOfficeCtrl1").CustomToolbar = !bVisible;
        }
        // 隐藏/显示 Office工具栏
        function Button4_onclick() {
            var bVisible = document.getElementById("PageOfficeCtrl1").OfficeToolbars;
            document.getElementById("PageOfficeCtrl1").OfficeToolbars = !bVisible;
        }

    </script>
    <form id="form1" runat="server">
    <input id="Button1" type="button" value="隐藏/显示 标题栏"  onclick="return Button1_onclick()" />
    <input id="Button2" type="button" value="隐藏/显示 菜单栏" onclick="return Button2_onclick()" />
    <input id="Button3" type="button" value="隐藏/显示 自定义工具栏"  onclick="return Button3_onclick()" />
    <input id="Button4" type="button" value="隐藏/显示 Office工具栏"  onclick="return Button4_onclick()" />
    <br /><br />

    <div style=" width:auto; height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
