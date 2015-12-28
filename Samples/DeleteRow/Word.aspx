<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="DeleteRow_Word" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>js 删除word表格中光标所在行</title>

    <script type="text/javascript" language="javascript">
    //删除word表格中光标所在行
//        function DeleteRow() {
//            var appObj = document.getElementById("PageOfficeCtrl1").Document.Application;

//            appObj.Selection.HomeKey(10);
//            appObj.Selection.EndKey(10, true);
//            appObj.Selection.Cells.Delete(2);
//            appObj.Selection.TypeBackspace();
        //        }
        //删除word表格中光标所在行，各种浏览器下均可使用
        function DeleteRow() {
            var mac = "Function myfunc()" + " \r\n"
                    + "Application.Selection.HomeKey Unit:=wdLine " + " \r\n"
                    + "Application.Selection.EndKey Unit:=wdLine, Extend:=true " + " \r\n"
                    + "Application.Selection.Cells.Delete ShiftCells:=wdDeleteCellsEntireRow " + " \r\n" 
                    + "Application.Selection.TypeBackspace " + " \r\n" 
                    + "End Function " + " \r\n";
            document.getElementById("PageOfficeCtrl1").RunMacro("myfunc", mac);
        } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="width:auto; height: 700px; ">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="True" Menubar="False"
            OfficeToolbars="True" OnLoad="PageOfficeCtrl1_Load">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
