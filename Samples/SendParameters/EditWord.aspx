<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditWord.aspx.cs" Inherits="SendParameters_SendParameters" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        function Save() {
            document.getElementById("PageOfficeCtrl1").WebSave();
        }
        function SetFullScreen() {
            document.getElementById("PageOfficeCtrl1").FullScreen = !document.getElementById("PageOfficeCtrl1").FullScreen;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="font-size: 14px;">
        <span style="color: Red;">更新人员信息：</span><br />
        <input id="Hidden1" name="age" type="hidden" value="25" />
        <span style="color: Red;">姓名：</span><input id="Text1" name="userName" type="text" value="张三" /><br />
        <span style="color: Red;">性别：</span><select id="Select1" name="selSex">
            <option value="男">男</option>
            <option value="女">女</option>
        </select>
    </div>
    <!--PageOfficeCtrl控件-->
    <div style="width: auto; height: 700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="True" Menubar="True">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
