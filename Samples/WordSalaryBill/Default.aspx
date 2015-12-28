<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>动态生成工资条</title>

    <script type="text/javascript">


        function getID() {
            var ids = "";
            for (var i = 1; i < 11; i++) {
                if (document.getElementById("check" + i.toString()).checked) {
                    ids += i.toString() + ",";
                }
            }
            
            if (ids == "")
                alert("请先选择要生成工资条的记录");
            else
                location.href = "Compose.aspx?ids=" + ids.substr(0, ids.length - 1);
        }

    </script>

</head>
<body>
    <div style="text-align: center;">
        <p style="color: Red; font-size: 14px;">
            1.可以点击“操作”栏中的“编辑”来编辑各个员工的工作条&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
            2.可以点击“操作”栏中的“查看”来查看各个员工的工作条&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
            3.可选择多条工资记录，点“生成工资条”按钮，生成工资条&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
        <table style="width: 60%; font-size: 12px; text-align: center; border: solid 1px #a2c5d9;">
            <asp:Literal ID="LiteralGrid" runat="server"></asp:Literal>
        </table>
        <br />
        <input type="button" value="生成工资条" onclick="getID()" />
    </div>
</body>
</html>
