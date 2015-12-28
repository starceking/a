<%@ Page Language="C#" AutoEventWireup="true" CodeFile="word-lists.aspx.cs" Inherits="CreateWord_word_lists" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function onColor(td) {
            td.style.backgroundColor = '#D7FFEE';
        }
        function offColor(td) {
            td.style.backgroundColor = '';
        }
        function getFocus() {
            var str = document.getElementById("FileSubject").value;
            if (str == "请输入文档主题") {
                document.getElementById("FileSubject").value = "";
            }

        }
        function lostFocus() {
            var str = document.getElementById("FileSubject").value;
            if (str.length <= 0) {
                document.getElementById("FileSubject").value = "请输入文档主题";
            }
        }


    </script>

</head>
<body>
    <!--header-->
    <div class="zz-headBox br-5 clearfix">
        <div class="zz-head mc">
            <!--logo-->
            <div class="logo fl">
                <a href="http://www.zhuozhengsoft.com/">
                    <img src="../images/logo.png" alt="" /></a></div>
            <!--logo end-->
            <ul class="head-rightUl fr">
                <li><a href="http://www.zhuozhengsoft.com/">卓正网站</a></li>
                <li><a href="###">客户问吧</a></li>
                <li class="bor-0"><a href="http://www.zhuozhengsoft.com/contact-us.html">联系我们</a></li>
            </ul>
        </div>
    </div>
    <!--header end-->
    <!--content-->
    <div class="zz-content mc clearfix pd-28">
        <div class="demo mc">
            <h2 class="fs-16">
                PageOffice 两种创建新文档的方式</h2>
        </div>
        <div class="demo mc">
            <h3 class="fs-12">
                新建文件</h3>
            <form id="form1" runat="server">
            <table class="text" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td style="font-size: 9pt" align="left">
                        方法1：通过复制文件创建新文件
                    </td>
                    <td align="center">
                        <input name="FileSubject" id="FileSubject" type="text" onfocus="getFocus()" onblur="lostFocus()"
                            class="boder" style="width: 180px;" value="请输入文档主题" />
                    </td>
                    <td style="width: 221px;">
                        &nbsp;
                        <asp:Button ID="btnCreate0" runat="server" Text="创建文件" Width="86px" OnClick="btnCreate0_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        方法2：利用属性WebCreateNew创建新文件&nbsp;&nbsp;
                    </td>
                    <td>
                        &nbsp;<a href="CreateWord.aspx" target="_blank"style=" color:Blue; text-decoration:underline;">新建文件</a>
                        </td>
                    <td style="width: 221px;">
                        &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            </form>
        </div>
        <div class="zz-talbeBox mc">
            <h2 class="fs-12">
                文档列表</h2>
            <table class="zz-talbe">
                <thead>
                    <tr>
                        <th width="22%">
                            文档名称
                        </th>
                        <th width="10%">
                            创建日期
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <%=strHtmls %>
                </tbody>
            </table>
        </div>
    </div>
    <!--content end-->
    <!--footer-->
    <div class="login-footer clearfix">
        Copyright &copy 2012 北京卓正志远软件有限公司</div>
    <!--footer end-->
</body>
</html>
