﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SavaReturnValue.aspx.cs" Inherits="SavaReturnValue_SavaReturnValue" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function Save() {
            document.getElementById("PageOfficeCtrl1").WebSave();
            //document.getElementById("PageOfficeCtrl1").CustomSaveResult获取的是保存页面后台代码 fs.CustomSaveResult = "OK";定义的返回值
            document.getElementById("PageOfficeCtrl1").Alert("保存成功，返回值为：" + document.getElementById("PageOfficeCtrl1").CustomSaveResult);
        }
 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" font-size:small; color:Red;">
        <asp:Label ID="Label4" runat="server" Text="关键代码：点右键，选择“查看源文件”，看js函数“Save()”："></asp:Label>
        <br />document.getElementById("PageOfficeCtrl1").WebSave()//执行保存操作"
        <br/>document.getElementById("PageOfficeCtrl1").CustomSaveResult//获取返回值 保存页面SaveFile.aspx.cs后台代码 fs.CustomSaveResult = "OK";定义的返回值
        <br />
    </div>
    <div style=" width:auto; height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="True" 
            Menubar="False">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
