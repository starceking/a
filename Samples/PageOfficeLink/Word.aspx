<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="word" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>最简单打开保存Word文件</title>
</head>
<body>
    <script type="text/javascript">
        function AfterDocumentOpened() {
            document.getElementById("Text1").value = document.getElementById("PageOfficeCtrl1").DataRegionList.GetDataRegionByName("PO_Title").Value;
        }

        function setTitleText() {
            document.getElementById("PageOfficeCtrl1").DataRegionList.GetDataRegionByName("PO_Title").Value = document.getElementById("Text1").value;
        }
    </script>
    <p style=" text-indent:10px;" >
        PageOffice 3.0在2.0版本的基础上增加了全新的文件打开方式“PageOfficeLink 方式”，此方式提供了更完美的浏览器兼容性解决方案。
        </p>
        <p style=" text-indent:10px;" >
        <span style=" font-weight:bold;"> PageOfficeLink ：简称POL</span>，是卓正公司为PageOffice在线打开文档的页面专门开发的特殊超链接；
        </p>
        <p style=" text-indent:10px;" >
       
            常规打开文档超链接的代码写法：&lt;a href=&quot;Word.aspx?id=12&quot;&gt;某某公司公文-12&lt;a&gt;</p>
        <p style=" text-indent:10px;" >
            POL打开文档超链接的代码写法：
       &lt;a href=&quot;<span style=" background-color:#D2E9FF;">&lt;%=PageOfficeLink.OpenWindow(&quot;</span>Word.aspx?id=12<span style=" background-color:#D2E9FF;">&quot;,&quot;width=800px;height=800px;&quot;)%&gt;</span>&quot;&gt;
     
            某某公司公文-12&lt;a&gt;
            &nbsp;</p>
    文档标题：<input id="Text1" type="text" size="50"      />
    <input type="button" value="修改" onclick="setTitleText();" />
    <form id="form1" runat="server">
    <div style=" width:auto; height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="False" 
            onload="PageOfficeCtrl1_Load">
            
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
