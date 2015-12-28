<%@ Page Language="C#" AutoEventWireup="true" CodeFile="createTable.aspx.cs" Inherits="WordCreateTable_createTable" %>
<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Word中动态创建表格</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div style=" width:auto; height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" onload="Page_Load">
        </po:PageOfficeCtrl>
    </div>
    </div>
    </form>
</body>
</html>
