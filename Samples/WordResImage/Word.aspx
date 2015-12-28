<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="WordResWord_Word" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>演示：PageOffice </title>
    <style type="text/css">
        html, body
        {
            height: 100%;
        }
        .main
        {
            height: 100%;
        }
    </style>
</head>
<body>
    <div style="font-size: 12px; line-height: 20px; border-bottom: dotted 1px #ccc; border-top: dotted 1px #ccc;
        padding: 5px;">
        关键代码：<span style="background-color: Yellow;"> <br />PageOffice.WordWriter.DataRegion dataRegion
            = worddoc.OpenDataRegion("PO_开头书签名称");
            <br />
            dataRegion.Value = "[image]doc/1.jpg[/image]";</span><br />
    </div>
    <br />
    <form id="form1" runat="server" style="height: 100%;">
    <div class="main">
        <!--**************   PageOffice 客户端代码开始    ************************-->
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" Menubar="False" 
            CustomToolbar="False">
        </po:PageOfficeCtrl>
        <!--**************   PageOffice 客户端代码结束    ************************-->
    </div>
    </form>
</body>
</html>
