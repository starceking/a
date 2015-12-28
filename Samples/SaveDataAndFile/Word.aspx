<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="SaveDataAndFile_Word" %>

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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span style="color: Red; font-size: 14px;">请输入公司名称、年龄、部门等信息后，单击工具栏上的保存按钮</span><br />
        <span style="color: Red; font-size: 14px;">请输入公司名称：</span><input id="txtName" name="txtCompany"
            type="text" /><br />
    </div>
    <div style="width: auto; height: 700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" Menubar="False">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
