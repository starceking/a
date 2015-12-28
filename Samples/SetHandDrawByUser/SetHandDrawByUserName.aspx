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
                <a href="Default.aspx">���ص�¼ҳ</a>
                    <span style="width: 100px;"> </span><strong>��ǰ�û���</strong>
					<span style="color: Red;"><%=userName %></span>
            </div>
            <br />
            <script type="text/javascript">
                //����ҳ��
                function Save() {
                    document.getElementById("PageOfficeCtrl1").WebSave();
                }

                //�쵼Ȧ��ǩ��
                function StartHandDraw() {
                    document.getElementById("PageOfficeCtrl1").HandDraw.Start();
                }
                
                /*
                //�ֲ���ʾ��д��ע
                function ShowHandDrawDispBar() {
                    document.getElementById("PageOfficeCtrl1").HandDraw.ShowLayerBar(); ;
                }*/

                //ȫ��/��ԭ
                function IsFullScreen() {
                    document.getElementById("PageOfficeCtrl1").FullScreen = !document.getElementById("PageOfficeCtrl1").FullScreen;
                }

                //��ʾ/�����û�����д��ע
                function ShowByUserName() {
                    var userName = "<%=userName %>"; //�Ӻ�̨��ȡ��¼�û���
                    document.getElementById("PageOfficeCtrl1").HandDraw.ShowByUserName(null, false); // �������е���д
                    document.getElementById("PageOfficeCtrl1").HandDraw.ShowByUserName(userName); // ��ʾTom����д���ڶ�������Ϊtrueʱ��ʡ��
                }
                function AfterDocumentOpened() {
                    ShowByUserName();
                }
            </script>

            <!--**************   ׿�� PageOffice��� ************************-->
            <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
            </po:PageOfficeCtrl>
        </div>
    </div>
    <div id="footer">
        <hr width="1000" />
        <div>
            Copyright (c) 2012 ����׿��־Զ������޹�˾</div>
    </div>

</body>
</html>
