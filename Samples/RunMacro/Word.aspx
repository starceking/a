<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="RunMacro_Word" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function Button1_onclick() {
            document.getElementById("PageOfficeCtrl1").RunMacro("myfunc", document.getElementById("TextBox1").value);
        }

// ]]>
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="font-size: 12px; line-height: 20px; border-bottom: dotted 1px #ccc; border-top: dotted 1px #ccc;
        padding: 5px;">
        注意：<span style="background-color: Yellow;">执行“执行宏myfunc”按钮之前需先设置好MS Word的关于执行宏命令的设置。
        <br />设置步骤如下：打开一个Word文档，点击“文件”→“选项”→“信任中心”→“信任中心设置”→“宏设置”→勾选上“信任对VBA工程对象模型的访问（V）”</span>
    </div>
    <asp:TextBox ID="TextBox1" runat="server" Height="87px" TextMode="MultiLine" Width="486px"></asp:TextBox>
    <input id="Button1" type="button" value="执行宏myfunc" onclick="return Button1_onclick()" />
    <div style="height: 800px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
