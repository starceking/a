<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ExaminationPaper_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>动态生成试卷</title>
</head>
<body>
    <script type="text/javascript">
		// JS方式生成试卷
		function button1Click(){
			document.getElementById("form1").action = "Compose.aspx";
			document.getElementById("form1").submit();
		}
		// 后台编程生成试卷
		function button2Click(){
		    document.getElementById("form1").action = "Compose2.aspx";
			document.getElementById("form1").submit();
		}
	</script>
    <form name="form1" id="form1" method="post" action="Compose.aspx">
    <div style="text-align: center;">
        <span style="color: Red;">1.可以点“操作”栏中的“编辑”来编辑各个试题&nbsp;&nbsp;&nbsp;&nbsp;<br />
            2.可以选择多个试题，点“生成试卷”按钮，生成试卷</span>
        <table style="background-color: #999; width: 600px; margin-left:28%;">
            <asp:Literal ID="LiteralGrid" runat="server"></asp:Literal>
        </table>
        <br />
        <input type="button" onclick="button1Click();" id="Button1" value="JS方式生成试卷" /><span>（所有版本）</span>
		<input type="button" onclick="button2Click();" id="Button2" value="后台编程生成试卷" /><span style="color:Red;">（专业版、企业版）</span>
    </div>
    </form>
</body>
</html>
