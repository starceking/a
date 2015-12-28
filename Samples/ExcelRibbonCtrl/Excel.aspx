<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Excel.aspx.cs" Inherits="word" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>控制Excel的Ribbon工具栏</title>
</head>
<body>
    <script type="text/javascript">
        function SaveFile() {
            document.getElementById("PageOfficeCtrl1").WebSave();
        }
    </script>
    <form id="form1" runat="server">
    
    <div style="font-size:14px; line-height:20px; ">
	 PageOfficeCtrl1.RibbonBar.SetTabVisible("TabHome", false);实现隐藏Ribbon栏中的“开始”命令分组.（"TabHome"为该命令分组的名称,false为隐藏，true为显示）
	<br/>
	 PageOfficeCtrl1.RibbonBar.SetSharedVisible("FileSave", false);实现隐藏Ribbon快速工具栏中的“保存”按钮.（"FileSave"为按钮的名称,false为隐藏，true为显示）
	<br/>
	PageOfficeCtrl1.RibbonBar.SetGroupVisible("GroupClipboard", false);实现隐藏“开始”命令分组中的“剪切板”面板.（"GroupClipboard"为该面板的名称,false为隐藏，true为显示）
	<br/>
	<span style="color:red">注意：此控制是会同步影响到本地文件打开时Ribbon栏中的各个工具按钮的显示状态，当关闭在线编辑窗口时，本地Ribbon栏状态恢复正常。</span>
	<br/><br/>
	</div>

    <div style=" width:auto; height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" 
            onload="PageOfficeCtrl1_Load">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
