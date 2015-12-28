<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaveFile.aspx.cs" Inherits="Savedoc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="errTopArea" style="text-align: left">
            [提示标题：这是一个开发人员可自定义的对话框]</div>
        <div class="errTxtArea" style="height: 150px; text-align: left">
            <b class="txt_title">
                <div style="color: #FF0000;">
                    错误信息如下：</div>
                <%=strErrHtml%>
            </b>
        </div>
        <div class="errBtmArea">
            <input type="button" class="btnFn" value=" 关闭 " onclick="window.opener=null;window.open('','_self');window.close();" /></div>
    </div>
    </form>
</body>
</html>
