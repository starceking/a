<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateWord.aspx.cs" Inherits="CreateWord_CreateWord" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../images/csstg.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function Save() {
            document.getElementById("PageOfficeCtrl1").WebSave();
            alert('保存成功！');
            location.href = "word-lists.aspx";
        }

        function Cancel() {
            window.close();
        }

        function getFocus() {
            var str = document.getElementById("FileSubject").value;
            if (str == "请输入文档主题") {
                document.getElementById("FileSubject").value = "";
            }
        }
        function lostFocus() {
            var str = document.getElementById("FileSubject").value;
            if (str.length <= 0) {
                document.getElementById("FileSubject").value = "请输入文档主题";
            }
        }
        function BeforeDocumentSaved() {
            var str = document.getElementById("FileSubject").value;
            if (str.length <= 0) {
                document.getElementById("PageOfficeCtrl1").Alert("请输入文档主题");
                return false
            }
            else
                return true;
        }
    </script>

</head>
<body>
    <form id="form2"  action="CreateWord.aspx" runat="server">
    <div id="header">
        <div style="float: left; margin-left: 20px;">
            <img src="../images/logo.jpg" height="30" /></div>
        <ul>
            <li><a href="#">卓正网站</a></li>
            <li><a href="#">客户问吧</a></li>
            <li><a href="#">在线帮助</a></li>
            <li><a href="#">联系我们</a></li>
        </ul>
    </div>
    <div id="content">
        <div id="textcontent" style="width: 1000px; height: 800px;">
            <div class="flow4">
                <span style="width: 100px;"> &nbsp;
                    </span>
            </div>
            <div>
                文档主题：<input name="FileSubject" id="FileSubject" type="text" onfocus="getFocus()" onblur="lostFocus()"
                    class="boder" style="width: 180px;" value="请输入文档主题" />
                    <input type="button" onclick="Save()"  value="保存"/>
                    <input type="button" onclick="Cancel()"  value="取消"/>
            </div>
            <div>
           &nbsp;
            </div>
            <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="False" Menubar="False"
                OnLoad="PageOfficeCtrl1_Load">
            </po:PageOfficeCtrl>
        </div>
    </div>
    <div id="footer">
        <hr width="1000" />
        <div>
            Copyright (c) 2012 北京卓正志远软件有限公司</div>
    </div>
    </form>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</body>
</html>
