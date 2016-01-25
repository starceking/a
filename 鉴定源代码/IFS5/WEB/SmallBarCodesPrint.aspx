<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmallBarCodesPrint.aspx.cs"
    Inherits="ToolPage_Tip_SmallBarCodesPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5/" />
    <title>Untitled Page</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="App_Themes/SmallBarCode.css" rel="stylesheet" type="text/css" />
    <style media="print" type="text/css">
        .Noprint
        {
            display: none;
        }
        .PageNext
        {
            page-break-after: always;
        }
    </style>

    <script type="text/javascript">
        function change1(){
            var tableone= document.getElementById("one");
            tableone.style.display="block";
            var tabletwo=document.getElementById("two");
            tabletwo.style.display="none";
        }
        
        function change2(){    
            var tableone= document.getElementById("one");
            tableone.style.display="none";
            var tabletwo=document.getElementById("two");
            tabletwo.style.display="block";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <center class="Noprint">
        <p>
            <object id="WebBrowser" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0"
                width="0">
            </object>
            <input type="button" value="��ӡ" onclick="document.all.WebBrowser.ExecWB(6,1)" />
            <input type="button" value="ֱ�Ӵ�ӡ" onclick="document.all.WebBrowser.ExecWB(6,6)" />
            <input type="button" value="ҳ������" onclick="document.all.WebBrowser.ExecWB(8,1)" />
        </p>
        <p>
            <input type="button" value="��ӡԤ��" onclick="document.all.WebBrowser.ExecWB(7,1)" />
            <br />
        </p>
    </center>
    <center class="Noprint">
        ��ӡ������<br />
        <br />
        һ����ӡ֮ǰ���ȵ����ҳ�����á����ڵ����ĶԻ������ٵ������ӡ������ťѡ����ȷ�Ĵ�ӡ����㡰ȷ������<br />
        ����������ҳ�߾ࣨ���ף�����������0����0����0����0��<br />
        ����ɾ��ҳü��ҳ����������ݡ�<br />
        �ģ�ѡ����ȷ��ֽ�Ŵ�С�������ȷ������ť��ҳ��������ɡ�<br />
        �壬�������ӡ����ť��ѡ����ȷ�������ӡ����ӡ��
    </center>
    <center class="Noprint">
        <input id="Radio1" type="radio" name="mode" value="����" checked="checked" onclick="change1()">����</input>
        <input id="Radio2" type="radio" name="mode" value="˫��" onclick="change2()">˫��</input>
    </center>
    <center>
        <table id="one">
            <tr>
                <td>
                    <asp:Repeater ID="Repeater3" runat="server">
                        <ItemTemplate>
                            <center>
                                <div style="margin: 8px 0; padding: 10px 0 10px 0; font-size: 10pt;">
                                    ����ǼǺţ�<asp:Label runat="server" ID="Label1" Text='<%# Bind("����ǼǺ�") %>' Font-Names="����"
                                        Font-Size="10pt"></asp:Label>
                                </div>
                                <div style="margin: 8px 0; padding: 0 0 0 0;">
                                    <asp:Label runat="server" ID="Label2" Text='<%# "*"+Eval("���ϱ��")+"*" %>' Font-Names="C39P48DhTt"
                                        Font-Size="48pt"></asp:Label>
                                </div>
                                <div style="margin: 8px 0; padding: 10px 0 10px 0; font-size: 9pt;">
                                    DNAlab�ţ�<asp:Label runat="server" ID="Label3" Text='<%# Bind("���ϱ��") %>' Font-Names="����"
                                        Font-Size="9pt"></asp:Label>
                                </div>
                            </center>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </center>
    <center>
        <table id="two" style="width: 320px; display: none;">
            <tr>
                <td>
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <center>
                                <div style="margin: 6px 0; padding: 0; font-size: 9pt;">
                                    ����ǼǺţ�<asp:Label runat="server" ID="Label1" Text='<%# Bind("����ǼǺ�") %>' Font-Names="����"
                                        Font-Size="9pt"></asp:Label>
                                </div>
                                <div style="margin: 6px 0; padding: 0;">
                                    <asp:Label runat="server" ID="Label2" Text='<%# "*"+Eval("���ϱ��")+"*" %>' Font-Names="3 of 9 Barcode"
                                        Font-Size="14pt"></asp:Label>
                                </div>
                                <div style="margin: 6px 0; padding: 0;">
                                    <asp:Label runat="server" ID="Label3" Text='<%# Bind("���ϱ��") %>' Font-Names="����"></asp:Label>
                                </div>
                            </center>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
                <td>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <center>
                                <div style="margin: 6px 0; padding: 0; font-size: 9pt;">
                                    ����ǼǺţ�<asp:Label runat="server" ID="Label1" Text='<%# Bind("����ǼǺ�") %>' Font-Names="����"
                                        Font-Size="9pt"></asp:Label>
                                </div>
                                <div style="margin: 6px 0; padding: 0;">
                                    <asp:Label runat="server" ID="Label2" Text='<%# "*"+Eval("���ϱ��")+"*" %>' Font-Names="3 of 9 Barcode"
                                        Font-Size="14pt"></asp:Label>
                                </div>
                                <div style="margin: 6px 0; padding: 0;">
                                    <asp:Label runat="server" ID="Label3" Text='<%# Bind("���ϱ��") %>' Font-Names="����"></asp:Label>
                                </div>
                            </center>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
