<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        //获取后台定义的Tag 字符串
        function getTagNames() {
            var tagNames = document.getElementById("PageOfficeCtrl1").DefineTagNames;
            return tagNames;
        }
        
        //定位Tag
        function locateTag(tagName) {
            
            var appSlt = document.getElementById("PageOfficeCtrl1").Document.Application.Selection;
            var bFind = false;
            //appSlt.HomeKey(6);
            appSlt.Find.ClearFormatting();
            appSlt.Find.Replacement.ClearFormatting();

            bFind = appSlt.Find.Execute(tagName);
            if (!bFind) {
                document.getElementById("PageOfficeCtrl1").Alert("已搜索到文档末尾。");
                appSlt.HomeKey(6);
            }
            window.focus();

        }

        //添加Tag
        function addTag(tagName) {
            try {
                var tmpRange = document.getElementById("PageOfficeCtrl1").Document.Application.Selection.Range;
                tmpRange.Text = tagName;
                tmpRange.Select();
                return "true";
            } catch (e) {
                return "false";
            }
        }
        
        //删除Tag
        function delTag(tagName) {
            var tmpRange = document.getElementById("PageOfficeCtrl1").Document.Application.Selection.Range;
            
            if (tagName == tmpRange.Text) {
                tmpRange.Text = "";
                return "true";
            }
            else
                return "false";

        }
   
    </script>
    
    <script type="text/javascript">
        function Save() {
            document.getElementById("PageOfficeCtrl1").WebSave();
        }
        function ShowDefineDataTags() {
            document.getElementById("PageOfficeCtrl1").ShowHtmlModelessDialog("DataTagDlg.htm", "parameter=xx", "left=300px;top=390px;width=430px;height=410px;frame:no;");
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 1000px; height: 800px;">
        <%--<button onclick="my();">
            管理书签</button>--%>
        <%--<input id="Button1" type="button" value="button" onclick="delBookMark('PO_use')" />--%>
        <%-- <input type="button"  value="定位" onclick="locateBK('')"/>
        <input id="Button1" type="button" value="button" onclick="addBookMark('','')" />--%>
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="True" Menubar="False">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
