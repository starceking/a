﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Compose.aspx.cs" Inherits="ExaminationPaper_Compose" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">

         <%=operateStr %>

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; height: 700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" Menubar="False" 
            CustomToolbar="False">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
