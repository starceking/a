<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataRegionFill.aspx.cs" Inherits="DataRegionFill_DataRegionFill" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    PageOffice中操作的DataRegion（数据区域）实际上就是Word文件中的书签，但是书签的名字必须以：PO_ 开头。<br />
    可以打开DataRegionFill/doc/test.doc文件，点“插入”-“书签”查看test.doc文件中所包含的数据区域。
    <form id="form1" runat="server">
    <div style=" width:auto; height:650px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
