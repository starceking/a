<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="SplitWord_Word" %>
<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>word拆分</title>
    <script type="text/javascript">
        function Save() {
            document.getElementById("PageOfficeCtrl1").WebSave();
        }
    </script>
</head>
<body>
   <div style=" font-size:14px; line-height:20px;">演示说明：<br />点击“保存”按钮，PageOffice会把文档中三个数据区域（PO_test1，PO_test2，PO_test3）中的内容保存为三个独立的子文件（new1.doc，new2.doc，new3.doc）到“Samples/SplitWord/doc” 目录下。</div>
   <div style="color: red;font-size:14px; line-height:20px;" >Word拆分功能只有企业版支持，并且文档的打开模式必须是OpenModeType.docSubmitForm，需要设置数据区域的属性dataRegion1.SubmitAsFile = true 。<br /><br /></div>
    <form id="form1" runat="server">
   
   <div style=" width:auto; height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="False" 
            onload="PageOfficeCtrl1_Load">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
