<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OpenWord.aspx.cs" Inherits="OpenWord_OpenWord" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在线打开Word文件</title>

    <script type="text/javascript">
        function AfterDocumentOpened() {
            refreshList();
        }
        //获取当前痕迹列表
        function refreshList() {
            var i;
            document.getElementById("ul_Comments").innerHTML = "";
            for (i = 1; i <= document.getElementById("PageOfficeCtrl1").Document.Revisions.Count; i++) {
                var str = "";
                str = str + document.getElementById("PageOfficeCtrl1").Document.Revisions.Item(i).Author;

                if (document.getElementById("PageOfficeCtrl1").Document.Revisions.Item(i).Type == "1") {
                    str = str + ' 插入：' + document.getElementById("PageOfficeCtrl1").Document.Revisions.Item(i).Range.Text;
                }
                else if (document.getElementById("PageOfficeCtrl1").Document.Revisions.Item(i).Type == "2") {
                    str = str + ' 删除：' + document.getElementById("PageOfficeCtrl1").Document.Revisions.Item(i).Range.Text;
                }
                else {
                    str = str + ' 调整格式或样式。';
                }
                document.getElementById("ul_Comments").innerHTML += "<li><a href='#' onclick='goToRevision(" + i + ")'>" + str + "</a></li>"
            }
        }
        //定位到当前痕迹
        function goToRevision(index) {
            var sMac = "Sub myfunc() " + "\r\n"
                     + "ActiveDocument.Revisions.Item(" + index + ").Range.Select " + "\r\n"
                     + "End Sub ";
            document.getElementById("PageOfficeCtrl1").RunMacro("myfunc", sMac);
        }
        //设置隐藏批注
        function page_load() {
            if (get_url_para(window.location.href, "mode") != "revision") {
                document.getElementById("Div_Comments").style.display = "none";
            }
        }
        function get_url_para(source, name) {
            var reg = new RegExp("(^|\\?|&)" + name + "=([^&]*)(\\s|&|$)", "i");
            if (reg.test(source)) return RegExp.$2; return "";
        }
    </script>

    <style>
        *
        {
            margin: 0;
            padding: 0;
        }
    </style>
</head>
<body onload="page_load()">
    <div style="width: 1300px; height: 700px;">
        <div style="width: 1050px; height: 700px; float: left;">
            <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
            </po:PageOfficeCtrl>
        </div>
        <div id="Div_Comments" style="float: right; width: 200px; height: 700px; border: solid 1px red;">
            <h3>
                痕迹列表</h3>
            <input type="button" name="refresh" value="刷新" onclick=" return refreshList()" />
            <ul id="ul_Comments">
            </ul>
        </div>
</body>
</html>
