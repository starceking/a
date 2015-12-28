<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="WordHyperLink_Word" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>演示：在当前光标处用js插入超链接</title>
    <style>
        html,body{height:100%; }
        .main{height:100%; }
    </style>
</head>
<body>
    <script type="text/javascript">
//    function  addHyperLink()
//    {
//        var docObj = document.getElementById("PageOfficeCtrl1").Document;
//        docObj.Application.ActiveWindow.View.ShowFieldCodes = false; //不要以域代码的形式显示超链接
//	    docObj.Hyperlinks.Add(docObj.Application.Selection.Range, "http://www.zhuozhengsoft.com/", "", "", "卓正");
//	}

    function  addHyperLink()
    {
        var text = "卓正志远";
        var url = "http://www.zhuozhengsoft.com/";
        
        var mac = "Function myfunc()" + " \r\n"
                    + "  Application.ActiveWindow.View.ShowFieldCodes = False " + " \r\n"
                    + "  ActiveDocument.Hyperlinks.Add Anchor:=Selection.Range, Address:= _" + " \r\n"
                    + "   \"" + url + "\", SubAddress:=\"\", ScreenTip:=\"\", _ " + " \r\n" 
                    + "   TextToDisplay:=\""+text+"\" " + " \r\n" 
                    + "End Function " + " \r\n";
        document.getElementById("PageOfficeCtrl1").RunMacro("myfunc", mac);
	}    
    </script>
    <div style="font-size:12px; line-height:20px; border-bottom:dotted 1px #ccc;border-top:dotted 1px #ccc; padding:5px;">
     <span style="color:red;">操作说明：</span>定位word文件中的光标到想插入超链接的位置，然后点“插入超链”按钮。<br />
   
    关键代码：点右键，选择“查看源文件”，看js函数<span style="background-color:Yellow;">addHyperLink()</span></div><br />
    <form id="form1" runat="server" style="height:100%;">
    <div class="main">
    <!--**************   PageOffice 客户端代码开始    ************************-->
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl> 
    <!--**************   PageOffice 客户端代码结束    ************************-->
    </div>
    </form>
</body>
</html>
