<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="word" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<meta charset="utf-8">
<title>XX文档系统</title>
<link rel="stylesheet" href="css/style.css"  type="text/css">
<script type="text/javascript">
document.createElement("section");
document.createElement("article");
document.createElement("footer");
document.createElement("header");
document.createElement("hgroup");
document.createElement("nav");
document.createElement("menu");
</script>
</head>
<body>
 <header>
   <div class="w12 header">
   <a class="db logo fl"><img src="images/logo.jpg" width="327" height="94"  alt=""/></a>
   <div class="fr logofr"><a href="#" class="blk">返回首页</a> |<a href="#" class="blk">客服中心</a><br>
如注册遇到问题请拨打：<strong style="font-size:14px;">400-000-0000</strong></div>
   </div>
 </header>
 <div class="head_border"></div>
 <section class="w12 login">
 <em class="fr">当前用户：张三 </em>
 </section>
 <section class="main w12">
   <div class="title"><a class="title1 db fl">文档内容</a><a class="title2 db fl">其他信息</a></div>
   <div class="fr tit2"><span class="arr"></span></div>
 
   <form id="form1" runat="server">
    <div style=" width:auto; height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="False" 
            onload="PageOfficeCtrl1_Load">
        </po:PageOfficeCtrl>
    </div>
    </form>
 </section>
 <br /><br />
 <div style=" text-align:center; height:80px; border-top: solid 1px #666; line-height:70px;">Copyright &copy 2015 北京卓正志远软件有限公司</div>
</body>
</html>

