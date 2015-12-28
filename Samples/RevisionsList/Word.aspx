<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="RevisionsList_Word" %>
<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>

<body>
    <script type="text/javascript">

        function Save() {
            document.getElementById("PageOfficeCtrl1").WebSave();
        }

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

        //刷新列表
        function refresh_click() {
            refreshList();
        }


    </script>
    <div  style=" width:1300px; height:700px;">
        <div id="Div_Comments" style=" float:left; width:200px; height:700px; border:solid 1px red;">
        <h3>痕迹列表</h3>
        <input type="button" name="refresh" value="刷新"onclick=" return refresh_click()"/>
        <ul id="ul_Comments">
            
        </ul>
        </div>
<div style=" width:1050px; height:700px; float:right;">

 PageOffice也可以通过调用VBA接口获取当前痕迹的时间,相关js代码如下:</br>
 document.getElementById("PageOfficeCtrl1").Document.Revisions.Item(i).Range.Date;</br>
 document.getElementById("PageOfficeCtrl1").Document.Revisions.Item(i).Range.DateTime;</br>
 document.getElementById("PageOfficeCtrl1").Document.Revisions.Item(i).Range.Time; </br>

	
           <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" >
        </po:PageOfficeCtrl>
    </div>
    
</body>
</html>

