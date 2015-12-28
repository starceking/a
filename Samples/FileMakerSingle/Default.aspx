<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        window.myFunc = function() {
            document.getElementById("aDiv").style.display = "";
            //alert('转换完毕！');
        };

        function ConvertFiles() {
            document.getElementById("iframe1").src = "FileMakerSingle.aspx";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;">
        <br />
        <span style="color: Red; font-size: 12px;">演示：把数据填充到模板中生成正式的word文件，请点下面的按钮进行转换</span><br />
        <input id="Button1" type="button" value="转换Word文件" onclick="ConvertFiles()" />
        <div id="aDiv" style="display: none; color: Red; font-size: 12px;">
            <span>转换完成，可在下面的地址中打开文件名为“maker.doc”的Word文件，查看转换的效果：<%=url %></span>
        </div>
    </div>
    <div style="width: 1px; height: 1px; overflow: hidden;">
        <iframe id="iframe1" name="iframe1" src=""></iframe>
    </div>
    </form>
</body>
</html>
