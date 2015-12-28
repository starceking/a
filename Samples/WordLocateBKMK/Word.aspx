<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="WordLocateBKMK_Word" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>演示：定位光标到指定书签</title>
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

    <script type="text/javascript">
//        function locateBookMark() {
//            //获取书签名称
//            var bkName = document.getElementById("txtBkName").value;
//            //将光标定位到书签所在的位置
//            document.getElementById("PageOfficeCtrl1").Document.Bookmarks(bkName).Select();
//        }
        function locateBookMark() {
            //获取书签名称
            var bkName = document.getElementById("txtBkName").value;
            //将光标定位到书签所在的位置
            var mac = "Function myfunc()" + " \r\n"
                    + "  ActiveDocument.Bookmarks(\""+ bkName +"\").Select " + " \r\n"
                    + "End Function " + " \r\n";
            document.getElementById("PageOfficeCtrl1").RunMacro("myfunc", mac);
        }
    </script>

</head>
<body>
    <div style=" font-size:small;">
        <label>关键代码：点右键，选择“查看源文件”，看js函数：</label>
        <label style=" background-color:Yellow;">function  locateBookMark()</label>
        <br/>
    <label>将光标定位到书签前，请先在文本框中输入要定位到的书签名称（可点击Office工具栏上的“插入”→“书签”，来查看当前Word文档中所有的书签名称）！</label><br />
        <label>书签名称：</label><input id="txtBkName" type="text" value="PO_seal" />
    </div>
    <br />
    <form id="form1" runat="server" style="height: 100%;">
    <div class="main">
        <!--**************   PageOffice 客户端代码开始    ************************-->
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" Menubar="False">
        </po:PageOfficeCtrl>
        <!--**************   PageOffice 客户端代码结束    ************************-->
    </div>
    </form>
</body>
</html>
