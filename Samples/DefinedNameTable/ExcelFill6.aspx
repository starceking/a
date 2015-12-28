<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExcelFill6.aspx.cs" Inherits="DefinedNameTable_ExcelFill6" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<head>
		<title>给Excel文档中定义名称的区域赋值</title>

		<meta http-equiv="pragma" content="no-cache">
		<meta http-equiv="cache-control" content="no-cache">
		<meta http-equiv="expires" content="0">
		<meta http-equiv="keywords" content="keyword1,keyword2,keyword3">
		<meta http-equiv="description" content="This is my page">
		<!--
	<link rel="stylesheet" type="text/css" href="styles.css">
	-->
	<script type="text/javascript">
    </script>
	</head>

	<body>
		<a href="Default.aspx">返回首页</a><br/>
		未动态填充数据时的显示的效果
		<div style="width: 1000px; height: 800px;">
			<po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl>
		</div>
	</body>
</html>
