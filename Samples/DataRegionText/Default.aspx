<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>�������������ı�����ʽ</title>
</head>
<body>
   
    <div id="content">
        <div id="textcontent" style="width: 1000px; height: 800px;">
            <script type="text/javascript">

                //ȫ��/��ԭ
                function IsFullScreen() {
                    document.getElementById("PageOfficeCtrl1").FullScreen = !document.getElementById("PageOfficeCtrl1").FullScreen;
                }

            </script>
            ��ʾ�����ʹ�ó���������������ı�����ʽ��<a href="Default2.aspx" target="_blank">������Ӳ鿴ԭ�ļ�</a><br /><br />
            <!--**************   ׿�� PageOffice��� ************************-->
            <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" Menubar="False" 
                OfficeToolbars="False" Titlebar="False">
            </po:PageOfficeCtrl>
        </div>
    </div>
    <div id="footer">
        <hr width="1000" />
        <div>
            Copyright (c) 2013 ����׿��־Զ������޹�˾</div>
    </div>

</body>
</html>
