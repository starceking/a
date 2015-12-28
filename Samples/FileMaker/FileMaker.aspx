<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileMaker.aspx.cs" Inherits="FileMaker" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!--**************   卓正 PageOffice 客户端代码开始    ************************-->

        <script language="javascript" type="text/javascript">
            function OnProgressComplete() {
                window.parent.myFunc(); //调用父页面的js函数
            }
        </script>

        <!--**************   卓正 PageOffice 客户端代码结束    ************************-->
        <po:FileMakerCtrl ID="FileMakerCtrl1" runat="server">
        </po:FileMakerCtrl>
        
    </div>
    </form>
</body>
</html>
