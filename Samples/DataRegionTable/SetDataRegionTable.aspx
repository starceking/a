<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetDataRegionTable.aspx.cs" Inherits="SetDataRegionTable" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>数据区域提交表格</title>
</head>
<body>
   
              <script type="text/javascript">
                  //保存页面
                  function Save() {
                      document.getElementById("PageOfficeCtrl1").WebSave();
                  }

                  //全屏/还原
                  function IsFullScreen() {
                      document.getElementById("PageOfficeCtrl1").FullScreen = !document.getElementById("PageOfficeCtrl1").FullScreen;
                  }

            </script> 
    <div id="content">
        <div id="textcontent" style="width: 1000px; height: 800px;">
            在文件里的表格中填写数据之后，点“保存”按钮，看后台获取到单元格数据的效果。
            <!--**************   卓正 PageOffice组件 ************************-->
            <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" Menubar="False" 
                Titlebar="False">
            </po:PageOfficeCtrl>
        </div>
    </div>
    <div id="footer">
        <hr width="1000" />
        <div>
            Copyright (c) 2013 北京卓正志远软件有限公司</div>
    </div>

</body>
</html>