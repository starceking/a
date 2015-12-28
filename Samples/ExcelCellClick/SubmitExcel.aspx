<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubmitExcel.aspx.cs" Inherits="SubmitExcel_SubmitExcel" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function Save() {
            document.getElementById("PageOfficeCtrl1").WebSave();
        }

        function OnCellClick(Celladdress, value, left, bottom) {
            var i = 0;
            while (i<5) {//表格第一列的5个单元格都弹出选择对话框
                if (Celladdress == "$B$" + (4 + i)) {
                    var strRet = document.getElementById("PageOfficeCtrl1").ShowHtmlModalDialog("select.aspx", "", "left=" + left + "px;top=" + bottom + "px;width=320px;height=230px;frame=no;");
                    if (strRet != "") {
                        return (strRet);
                    }
                    else {
                        if ((value == undefined) || (value == ""))
                            return " ";
                        else
                            return value;
                    }
                }
                i++;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    演示：点击Excel单元格弹出自定义对话框的效果。请看实现下面表格中的“部门名称”只能选择的效果。<br /><br />
    <div style="width: auto; height: 700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
