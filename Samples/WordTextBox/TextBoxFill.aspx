<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TextBoxFill.aspx.cs" Inherits="DataRegionFill_DataRegionFill" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>给word中的文本框赋值</title>
</head>
<body>
    PageOffice中操作的DataRegion（数据区域）实际上就是Word文件中的书签，但是书签的名字必须以：PO_ 开头。<br />
  
    <form id="form1" runat="server">
    <div style=" width:auto; height:650px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
