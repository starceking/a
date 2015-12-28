<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaveData.aspx.cs" Inherits="SubmitWord_SaveFilePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" border:solid 1px gray;">
        <div class="errTopArea" style="text-align: left;border-bottom:solid 1px gray;">
            [提示标题：这是一个开发人员可自定义的对话框]</div>
        <div class="errTxtArea" style="height: 150px; text-align: left">
            <b class="txt_title">
                <div style="color: #FF0000;">
                    提交的信息如下：</div>
                <%=content%>
            </b>
        </div>
        <div class="errBtmArea" style=" text-align:center;">
            <input type="button" class="btnFn" value=" 关闭 " onclick="window.opener=null;window.open('','_self');window.close();" /></div>
    </div>
    </form>
</body>
</html>
