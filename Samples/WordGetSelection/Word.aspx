<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="WordGetSelection_Word" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>演示：获取选中的文本内容</title>
    <style>
        html, body
        {
            height: 100%;
        }
        .main
        {
            height: 700px;
            width: auto;
        }
    </style>

    <script type="text/javascript">
    function  getSelectionText()
    {
        if (document.getElementById("PageOfficeCtrl1").Document.Application.Selection.Range.Text != "") {
            document.getElementById("PageOfficeCtrl1").Alert(document.getElementById("PageOfficeCtrl1").Document.Application.Selection.Range.Text);
	    }
	    else{
	        document.getElementById("PageOfficeCtrl1").Alert("您没有选择任何文本。");
	    }     
    }
    </script>

</head>
<body>
    <div style="font-size: 12px; line-height: 20px; border-bottom: dotted 1px #ccc; border-top: dotted 1px #ccc;
        padding: 5px;">
        <span style="color: red;">操作说明：</span>选中word文件中的一段文字，然后点“获取选中文字”按钮。<br />
        关键代码：点右键，选择“查看源文件”，看js函数<span style="background-color: Yellow;">getSelectionText()</span></div>
    <input id="Button1" type="button" onclick="getSelectionText();" value="获取选中的文本内容" /><br />
    <br />
    <form id="form1" runat="server" style="height: 100%;">
    <div class="main">
        <!--**************   PageOffice 客户端代码开始    ************************-->
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl>
        <!--**************   PageOffice 客户端代码结束    ************************-->
    </div>
    </form>
</body>
</html>
