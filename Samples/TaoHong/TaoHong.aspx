<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaoHong.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <link href="images/csstg.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        //��ʼ����ģ���б�

        function MyLoad() {
            if ("<%=mb %>" != "")
                document.getElementById("templateName").value = "<%=mb %>";
        }

        //���沢�ر�
        function saveAndClose() {
            document.getElementById("PageOfficeCtrl1").WebSave();
            location.href = "Default.aspx";
        }
    </script>

</head>
<body onload="MyLoad()">
    <form runat="server">
    <div id="header">
        <div style="float: left; margin-left: 20px;">
            <img src="images/logo.jpg" height="30" />
        </div>
        <ul>
            <li><a target="_blank" href="http://www.zhuozhengsoft.com">׿����վ</a> </li>
            <li><a target="_blank" href="http://www.zhuozhengsoft.com/poask/index.asp">�ͻ��ʰ�</a>
            </li>
            <li><a href="#">���߰���</a> </li>
            <li><a target="_blank" href="http://www.zhuozhengsoft.com/contact-us.html">��ϵ����</a>
            </li>
        </ul>
    </div>
    <div id="content">
        <div id="textcontent" style="width: 1000px; height: 800px;">
            <div class="flow4">
                <a href="Default.aspx">
                    <img alt="����" src="images/return.gif" border="0" />�ļ��б�</a> <span style="width: 100px;">
                    </span><strong>�ĵ����⣺</strong> <span style="color: Red;">�����ļ�</span> <strong>ģ���б�</strong>
                <span style="color: Red;">
                    <select name="templateName" id="templateName" style='width: 240px;'>
                        <option value='temp2008.doc' selected="selected">ģ��һ </option>
                        <option value='temp2009.doc'>ģ��� </option>
                        <option value='temp2010.doc'>ģ���� </option>
                    </select>
                </span>
                <asp:Button ID="BtnTaoHong" runat="server" Text="һ���׺�" OnClick="BtnTaoHong_Click" />
                <span style="color: Red;">
                    <input type="button" value="����ر�" onclick="saveAndClose()" />
                </span>
            </div>
            <!--**************   ׿�� PageOffice��� ************************-->

            <script type="text/javascript">

                //ȫ��/��ԭ
                function IsFullScreen() {
                    document.getElementById("PageOfficeCtrl1").FullScreen = !document.getElementById("PageOfficeCtrl1").FullScreen;
                }
            </script>

            <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="False" OfficeToolbars="True">
            </po:PageOfficeCtrl>
        </div>
    </div>
    <div id="footer">
        <hr width="1000" />
        <div>
            Copyright (c) 2012 ����׿��־Զ������޹�˾
        </div>
    </div>
    </form>
</body>
</html>
