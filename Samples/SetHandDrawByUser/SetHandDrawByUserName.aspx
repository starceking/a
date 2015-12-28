<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetHandDrawByUserName.aspx.cs" Inherits="SetHandDraw" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <link href="images/csstg.css" rel="stylesheet" type="text/css" />
</head>
<body >
   
    <div id="content">
        <div id="textcontent" style="width: 1000px; height: 800px;">
            <div class="flow4">
                <a href="Default.aspx">返回登录页</a>
                    <span style="width: 100px;"> </span><strong>当前用户：</strong>
					<span style="color: Red;"><%=userName %></span>
            </div>
            <br />
            <script type="text/javascript">
                //保存页面
                function Save() {
                    document.getElementById("PageOfficeCtrl1").WebSave();
                }

                //领导圈阅签字
                function StartHandDraw() {
                    document.getElementById("PageOfficeCtrl1").HandDraw.Start();
                }
                
                /*
                //分层显示手写批注
                function ShowHandDrawDispBar() {
                    document.getElementById("PageOfficeCtrl1").HandDraw.ShowLayerBar(); ;
                }*/

                //全屏/还原
                function IsFullScreen() {
                    document.getElementById("PageOfficeCtrl1").FullScreen = !document.getElementById("PageOfficeCtrl1").FullScreen;
                }

                //显示/隐藏用户的手写批注
                function ShowByUserName() {
                    var userName = "<%=userName %>"; //从后台获取登录用户名
                    document.getElementById("PageOfficeCtrl1").HandDraw.ShowByUserName(null, false); // 隐藏所有的手写
                    document.getElementById("PageOfficeCtrl1").HandDraw.ShowByUserName(userName); // 显示Tom的手写，第二个参数为true时可省略
                }
                function AfterDocumentOpened() {
                    ShowByUserName();
                }
            </script>

            <!--**************   卓正 PageOffice组件 ************************-->
            <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
            </po:PageOfficeCtrl>
        </div>
    </div>
    <div id="footer">
        <hr width="1000" />
        <div>
            Copyright (c) 2012 北京卓正志远软件有限公司</div>
    </div>

</body>
</html>
