<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录页面</title>
</head>
<body>
    <form id="form1"  action="SetHandDrawByUserName.aspx" method="post"> 
    <div style=" text-align:center;">
    <div>请选择登录用户：</div><br />
    <select name="userName">
        <option selected="selected" value="zhangsan">张三</option>
        <option  value="lisi">李四</option>
         <option  value="wangwu">王五</option>
    </select><br /><br />
    <input type="submit"  value="打开文件" /><br /><br />
    <div style=" color:Red;">不同的用户登录后，在文档中隐藏其他人的手写批注，只显示当前用户的手写批注。</div>
    </div>
    </form>
</body>

</html>
