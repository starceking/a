<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <link href="images/csstg.css" rel="stylesheet" type="text/css" />
</head>
<body>
   <br />
   ��ʾ����Ӧ�����������¼���
   <br /><br />
    <div id="content">
        <div id="textcontent" style="width: 1000px; height: 800px;">
            <script type="text/javascript">
                //***********************************PageOffice������õ�js����**************************************
                //����ҳ��
                function Save() {
                    document.getElementById("PageOfficeCtrl1").WebSave();
                }

                //ȫ��/��ԭ
                function IsFullScreen() {
                    document.getElementById("PageOfficeCtrl1").FullScreen = !document.getElementById("PageOfficeCtrl1").FullScreen;
                }

                function OnWordDataRegionClick(Name, Value, Left, Bottom) {
                    if (Name == "PO_deptName") {
     
                        var strRet = document.getElementById("PageOfficeCtrl1").ShowHtmlModalDialog("HTMLPage.htm", Value, "left=" + Left + "px;top=" + Bottom + "px;width=400px;height=300px;frame=no;");
                        if (strRet != "") {
                            return (strRet);
                        }
                        else {
                            if ((Value == undefined) || (Value == ""))
                                return " ";
                            else
                                return Value;
                        }
                    }
                }

                //***********************************PageOffice������õ�js����**************************************

            </script>

            <!--**************   ׿�� PageOffice��� ************************-->
            <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" Menubar="False" >
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
