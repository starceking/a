<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConWord.aspx.cs" Inherits="ConWord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta name="Generator" content="Microsoft Word 11 (filtered)">
    <title>�� �� ί �� ��</title>
    <style>
        @font-face
        {
            font-family: SimSun;
        }
        @font-face
        {
            font-family: KaiTi_GB2312;
        }
        @font-face
        {
            font-family: SimSun;
        }
        @font-face
        {
            font-family: KaiTi_GB2312;
        }
        P.MsoNormal
        {
            text-justify: inter-ideograph;
            font-size: 10.5pt;
            margin: 0cm 0cm 0pt;
            font-family: "Times New Roman";
            text-align: justify;
            mso-style-parent: "";
            mso-pagination: none;
            mso-bidi-font-size: 12.0pt;
            mso-fareast-font-family: SimSun;
            mso-font-kerning: 1.0pt;
        }
        LI.MsoNormal
        {
            text-justify: inter-ideograph;
            font-size: 10.5pt;
            margin: 0cm 0cm 0pt;
            font-family: "Times New Roman";
            text-align: justify;
            mso-style-parent: "";
            mso-pagination: none;
            mso-bidi-font-size: 12.0pt;
            mso-fareast-font-family: SimSun;
            mso-font-kerning: 1.0pt;
        }
        DIV.MsoNormal
        {
            text-justify: inter-ideograph;
            font-size: 10.5pt;
            margin: 0cm 0cm 0pt;
            font-family: "Times New Roman";
            text-align: justify;
            mso-style-parent: "";
            mso-pagination: none;
            mso-bidi-font-size: 12.0pt;
            mso-fareast-font-family: SimSun;
            mso-font-kerning: 1.0pt;
        }
        P.MsoHeader
        {
            border-right: medium none;
            padding-right: 0cm;
            border-top: medium none;
            padding-left: 0cm;
            font-size: 9pt;
            padding-bottom: 0cm;
            margin: 0cm 0cm 0pt;
            border-left: medium none;
            layout-grid-mode: char;
            padding-top: 0cm;
            border-bottom: medium none;
            font-family: "Times New Roman";
            text-align: center;
            mso-pagination: none;
            mso-fareast-font-family: SimSun;
            mso-font-kerning: 1.0pt;
            mso-style-link: " Char Char";
            tab-stops: center 207.65pt right 415.3pt;
            mso-border-bottom-alt: solid windowtext .75pt;
            mso-padding-alt: 0cm 0cm 1.0pt 0cm;
        }
        LI.MsoHeader
        {
            border-right: medium none;
            padding-right: 0cm;
            border-top: medium none;
            padding-left: 0cm;
            font-size: 9pt;
            padding-bottom: 0cm;
            margin: 0cm 0cm 0pt;
            border-left: medium none;
            layout-grid-mode: char;
            padding-top: 0cm;
            border-bottom: medium none;
            font-family: "Times New Roman";
            text-align: center;
            mso-pagination: none;
            mso-fareast-font-family: SimSun;
            mso-font-kerning: 1.0pt;
            mso-style-link: " Char Char";
            tab-stops: center 207.65pt right 415.3pt;
            mso-border-bottom-alt: solid windowtext .75pt;
            mso-padding-alt: 0cm 0cm 1.0pt 0cm;
        }
        DIV.MsoHeader
        {
            border-right: medium none;
            padding-right: 0cm;
            border-top: medium none;
            padding-left: 0cm;
            font-size: 9pt;
            padding-bottom: 0cm;
            margin: 0cm 0cm 0pt;
            border-left: medium none;
            layout-grid-mode: char;
            padding-top: 0cm;
            border-bottom: medium none;
            font-family: "Times New Roman";
            text-align: center;
            mso-pagination: none;
            mso-fareast-font-family: SimSun;
            mso-font-kerning: 1.0pt;
            mso-style-link: " Char Char";
            tab-stops: center 207.65pt right 415.3pt;
            mso-border-bottom-alt: solid windowtext .75pt;
            mso-padding-alt: 0cm 0cm 1.0pt 0cm;
        }
        P.MsoFooter
        {
            font-size: 9pt;
            margin: 0cm 0cm 0pt;
            layout-grid-mode: char;
            font-family: "Times New Roman";
            mso-pagination: none;
            mso-fareast-font-family: SimSun;
            mso-font-kerning: 1.0pt;
            tab-stops: center 207.65pt right 415.3pt;
        }
        LI.MsoFooter
        {
            font-size: 9pt;
            margin: 0cm 0cm 0pt;
            layout-grid-mode: char;
            font-family: "Times New Roman";
            mso-pagination: none;
            mso-fareast-font-family: SimSun;
            mso-font-kerning: 1.0pt;
            tab-stops: center 207.65pt right 415.3pt;
        }
        DIV.MsoFooter
        {
            font-size: 9pt;
            margin: 0cm 0cm 0pt;
            layout-grid-mode: char;
            font-family: "Times New Roman";
            mso-pagination: none;
            mso-fareast-font-family: SimSun;
            mso-font-kerning: 1.0pt;
            tab-stops: center 207.65pt right 415.3pt;
        }
        SPAN.CharChar
        {
            font-family: SimSun;
            mso-style-parent: "";
            mso-bidi-font-size: 9.0pt;
            mso-fareast-font-family: SimSun;
            mso-font-kerning: 1.0pt;
            mso-style-link: ҳü;
            mso-style-name: " Char Char";
            mso-style-locked: yes;
            mso-ansi-font-size: 9.0pt;
            mso-ansi-language: EN-US;
            mso-fareast-language: ZH-CN;
            mso-bidi-language: AR-SA;
        }
        DIV.Section1
        {
            page: Section1;
        }
    </style>
     <style media="print" type="text/css">
        .Noprint
        {
            display: none;
        }
        .PageNext
        {
            page-break-after: always;
        }
        .btd
        {
        	
        }
    </style>

    <script src="jquery-1.3.2.min.js" type="text/javascript"></script>

    <script src="jquery-barcode-2.0.2.min.js" type="text/javascript"></script>

    <script type="text/javascript">
    
      function generateBarcode(value){
        var btype = "code93";
        var renderer ="css";
        		
        var settings = {
          output:renderer,
          bgColor: "#FFFFFF",
          color: "#000000",
          barWidth: "2",
          barHeight: "20",
          moduleSize: "",
          posX: "",
          posY: "",
          addQuietZone: "345454"
        };
        
         $("#barcodeTarget").html("").show().barcode(value, btype, settings);
      }
  
    </script>

</head>
<body lang="ZH-CN" style='text-justify-trim: punctuation;' onload="generateBarcode('<% = barCode %>')">
    <center class="Noprint">
        <p>
            <object id="WebBrowser" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0"
                width="0">
            </object>
            <input type="button" value="ҳ������" onclick="document.all.WebBrowser.ExecWB(8,1)" />
            <input type="button" value="��ӡԤ��" onclick="document.all.WebBrowser.ExecWB(7,1)" />
            <input type="button" value="ֱ�Ӵ�ӡ" onclick="document.all.WebBrowser.ExecWB(6,1)" />
            <input type="button" value="���Ϊ" onclick="document.all.WebBrowser.ExecWB(4,1)" /><br />
            <span>���Ƽ�ҳ�����á�ֽ�Ŵ�С��A4����������ҳ�߾ࣨ���ף����� 20���� 20���� 0���� 0��</span><br />
            <span style="color: #f00">�����ѡ���ӡ������ί����ҳ������ַ�����ڴ�ӡί�����ʱ�򽫡�ҳ�����á��е�ҳü��ҳ��ȥ����</span>
        </p>
    </center>
    <form id="form1" runat="server">
    
    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
        <HeaderTemplate>
            <div style="margin-left: auto; margin-right: auto; width: 700px; border-bottom: 1px double #000; font-weight:bold; font-size:small; margin-bottom:1px;">
                <div style="border-bottom:3px solid #000; padding-bottom:5px; margin-bottom:2px;">
                    <table style=" width:700px; border:1px solid #000;">
                        <tr>
                            <td colspan="2" style="border:1px solid #000;" align="center">����ʡ���������¼����ܶ�
                            </td>
                            <td style="border:1px solid #000;" align="center">�ļ����
                            </td>
                            <td style="border:1px solid #000;" align="center">LNFSD-BG-01/01-FW
                            </td>
                        </tr>
                        <tr>
                            <td style="border:1px solid #000;" align="center">����
                            </td>
                            <td style="border:1px solid #000;" align="center">DNAʵ���Ҽ������̹���ǼǱ�
                            </td>
                            <td style="border:1px solid #000;" align="center">���
                            </td>
                            <td style="border:1px solid #000;" align="center">��1�� ��0���޶�
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="Section1" style='layout-grid: 15.6pt;'>
                <p class="MsoNormal" style='text-align: center;'>
                    <b><span style='font-size: 18.0pt; font-family: ����'>��<span lang="EN-US">&nbsp; </span>
                        ��<span lang="EN-US">&nbsp; </span>ί<span lang="EN-US">&nbsp; </span>��<span lang="EN-US">&nbsp;
                        </span>��</span></b></p>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="MsoNormal" style='text-align: center;'>
                <a name="z"><span style='font-size: 22.0pt; font-family: ����'>
                    <div id="barcodeTarget" class="barcodeTarget" style="margin: 0 auto;">
                    </div>
                </span></a>
            </div>
            <div style="margin-left: auto; margin-right: auto; width: 700px;">
                <p class="MsoNormal" align="right" style='font-family: ����; font-size: 12pt; text-align: right;'>
                    ��ţ�[ <a name="wtnf"><span style='font-family: ����'>
                        <%# Eval("ί�����")%></span></a> ] �� <%# Eval("ί�����")%></span></a> ��
                </p>
            </div>
            <div style="margin-left: auto; margin-right: auto; width: 700px;">
                <table class="MsoNormalTable" style="border-right: medium none; border-top: medium none;
            border-left: medium none; border-bottom: medium none; border-collapse: collapse;
            mso-padding-alt: 0cm 5.4pt 0cm 5.4pt; mso-border-alt: solid windowtext .5pt;
            mso-yfti-tbllook: 480; mso-border-insideh: .5pt solid windowtext; mso-border-insidev: .5pt solid windowtext"
            cellspacing="0" cellpadding="0" border="1">
            <tbody>
                <tr style="height: 22.7pt; mso-yfti-irow: 0; mso-yfti-firstrow: yes">
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                        width: 95.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt;
                        mso-border-alt: solid windowtext .5pt" width="127" colspan="2">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">ί�м�����λ</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 189pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt" width="252" colspan="3">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span lang="EN-US" style="font-size: 12pt">
                                <%# Eval("ί�е�λ����")%></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 81pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt" width="108">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">ί��ʱ��</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 98.95pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt" width="132">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span lang="EN-US" style="font-size: 12pt">
                            <%# Eval("ί��ʱ��", "{0:d}")%></span></p>
                    </td>
                </tr>
                <tr style="height: 22.7pt; mso-yfti-irow: 1">
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                        width: 32.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt;
                        mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="43" rowspan="4">
                        <p class="MsoNormal" style="margin: 0cm 5.65pt 0pt; text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">�ͼ���</span><span style="font-size: 12pt">
                            </span><span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'"></span><span style="font-size: 12pt">
                            </span><span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'"></span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 63pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="84">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">��</span><span lang="EN-US" style="font-size: 12pt"><span
                                    style="mso-spacerun: yes">&nbsp; </span></span><span style="font-size: 12pt; font-family: SimSun;
                                        mso-ascii-font-family: 'Times New Roman'; mso-hansi-font-family: 'Times New Roman'">
                                        ��</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 72pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="96">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span lang="EN-US" style="font-size: 12pt">
                                <%#Eval("һ������")%></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 54pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="72">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">ְ</span><span style="font-size: 12pt">
                            </span><span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">��</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 63pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="84">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span lang="EN-US" style="font-size: 12pt">
                                ��</span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 81pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="108">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 9pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">֤�����Ƽ�����</span><span lang="EN-US" style="font-size: 9pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 98.95pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="132">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span lang="EN-US" style="font-size: 12pt">
                                <%#Eval("һ�;���")%></span></p>
                    </td>
                </tr>
                <tr style="height: 22.7pt; mso-yfti-irow: 2">
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 63pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="84">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">��</span><span lang="EN-US" style="font-size: 12pt"><span
                                    style="mso-spacerun: yes">&nbsp; </span></span><span style="font-size: 12pt; font-family: SimSun;
                                        mso-ascii-font-family: 'Times New Roman'; mso-hansi-font-family: 'Times New Roman'">
                                        ��</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 72pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="96">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span lang="EN-US" style="font-size: 12pt">
                                <%#Eval("��������")%></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 54pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="72">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">ְ</span><span style="font-size: 12pt">
                            </span><span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">��</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 63pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="84">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span lang="EN-US" style="font-size: 12pt">
                                ��</span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 81pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="108">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 9pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">֤�����Ƽ�����</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 98.95pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="132">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span lang="EN-US" style="font-size: 12pt">
                                <%#Eval("���;���")%></span></p>
                    </td>
                </tr>
                <tr style="height: 22.7pt; mso-yfti-irow: 3">
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 63pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="84">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">ͨѶ��ַ</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 189pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="252" colspan="3">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span lang="EN-US" style="font-size: 12pt">
                                <%#Eval("ί�е�λ��ַ")%></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 81pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="108">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">��������</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 98.95pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="132">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span lang="EN-US" style="font-size: 12pt">
                                <%#Eval("ί�е�λ�ʱ�")%></span></p>
                    </td>
                </tr>
                <tr style="height: 22.7pt; mso-yfti-irow: 4">
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 63pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="84">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">��ϵ�绰</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 189pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="252" colspan="3">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span lang="EN-US" style="font-size: 12pt">
                                <%#Eval("һ�͵绰")%>
                                    <%#Eval("���͵绰")%></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 81pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="108">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">�������</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 98.95pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="132">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span lang="EN-US" style="font-size: 12pt">
                                <%#Eval("ί�е�λ�绰")%></span></p>
                    </td>
                </tr>
                <tr style="height: 22.7pt; mso-yfti-irow: 5">
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                        width: 95.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt;
                        mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="127" colspan="2">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">������������</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 368.95pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="492" colspan="5">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">
                                <%#Eval("������λ����")%></span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                </tr>
                <tr style="height: 22.7pt; mso-yfti-irow: 6">
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                        width: 95.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt;
                        mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="127" colspan="2">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">�����£�������</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 189pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="252" colspan="3">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span lang="EN-US" style="font-size: 12pt">
                                <%#Eval("��������")%></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 81pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="108">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">�������</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 98.95pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.7pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="132">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span lang="EN-US" style="font-size: 12pt">
                                <%#Eval("����ܿ�")%></span></p>
                    </td>
                </tr>
                <tr style="height: 80.9pt; mso-yfti-irow: 7">
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                        width: 95.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 80.9pt;
                        mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="127" colspan="2">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">�����£���</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">��Ҫ���</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 368.95pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 80.9pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="492" colspan="5">
                        <p class="MsoNormal">
                            <span lang="EN-US" style="font-size: 12pt">
                                <%#Eval("��Ҫ����")%></span></p>
                    </td>
                </tr>
                <tr style="height: 26.45pt; mso-yfti-irow: 8">
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                        width: 95.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 26.45pt;
                        mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="127" colspan="2">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">ԭ�������</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 368.95pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 26.45pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="492" colspan="5">
                        <p class="MsoNormal">
                            <span lang="EN-US" style="font-size: 12pt">
                                <%#Eval("ԭ�������")%></span></p>
                    </td>
                </tr>
                <tr style="height: 151pt; mso-yfti-irow: 9">
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                        width: 95.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 151pt;
                        mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="127" colspan="2">
                        <p class="MsoNormal" style="margin: 0cm 12.6pt 0pt 8.9pt; mso-para-margin-top: 0cm;
                            mso-para-margin-right: 1.2gd; mso-para-margin-bottom: .0001pt; mso-para-margin-left: .85gd">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">�ͼ�ļ�ĺ�����������������ơ���������״����װ����ĵ���ȡ��λ����ȡ�����ȣ�</span><span
                                    lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 368.95pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 151pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="492" colspan="5">
                        <p class="MsoNormal">
                            <span lang="EN-US" style="font-size: 12pt">
                                <%#Eval("�������")%></span></p>
                    </td>
                </tr>
                <tr style="height: 135.95pt; mso-yfti-irow: 10; mso-yfti-lastrow: yes">
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                        width: 95.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 135.95pt;
                        mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="127" colspan="2">
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">ί�м�����λ</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">�ļ���Ҫ��</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                        <p class="MsoNormal" style="text-align: center" align="center">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">�ͳ�������</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                    </td>
                    <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: medium none;
                        padding-left: 5.4pt; padding-bottom: 0cm; border-left: medium none; width: 368.95pt;
                        padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 135.95pt; mso-border-alt: solid windowtext .5pt;
                        mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt"
                        width="492" colspan="5">
                        <p class="MsoNormal" style="word-break: break-all; margin-right: 36pt">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">����Ҫ��</span><span lang="EN-US" style="font-size: 12pt"><%#Eval("����Ҫ��")%></span></p>
                        <p class="MsoNormal" style="word-break: break-all; margin-right: 36pt">
                            <span lang="EN-US" style="font-size: 12pt">
                                <o:p>&nbsp;</o:p></span></p>
                        <p class="MsoNormal" style="word-break: break-all; text-indent: 21.6pt; margin-right: 36pt;
                            mso-char-indent-count: 1.8">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">�ҵ�λ��������������ܵ�����͹���ʵ���ύ�ļ�ĺ���������Դ����ɿ���</span><span
                                    lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                        <p class="MsoNormal" style="word-break: break-all; margin-right: 36pt">
                            <span lang="EN-US" style="font-size: 12pt">
                                <o:p>&nbsp;</o:p></span></p>
                        <p class="MsoNormal" style="margin: 0cm 36pt 0pt 3.45pt; word-break: break-all; mso-para-margin-top: 0cm;
                            mso-para-margin-right: 36.0pt; mso-para-margin-bottom: .0001pt; mso-para-margin-left: .33gd">
                            <span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">������ǩ�֣�</span><span lang="EN-US" style="font-size: 12pt"><span
                                    style="mso-spacerun: yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </span></span><span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                    mso-hansi-font-family: 'Times New Roman'">����λӡ�£�</span><span lang="EN-US" style="font-size: 12pt"><o:p></o:p></span></p>
                        <p class="MsoNormal" style="margin: 0cm 3.55pt 0pt 3.45pt; word-break: break-all;
                            text-align: right; tab-stops: 354.6pt; mso-para-margin-top: 0cm; mso-para-margin-right: 3.55pt;
                            mso-para-margin-bottom: .0001pt; mso-para-margin-left: .33gd" align="right">
                            <span lang="EN-US" style="font-size: 12pt">20<span style="mso-spacerun: yes">&nbsp;&nbsp;
                            </span></span><span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                mso-hansi-font-family: 'Times New Roman'">��</span><span lang="EN-US" style="font-size: 12pt"><span
                                    style="mso-spacerun: yes">&nbsp;&nbsp; </span></span><span style="font-size: 12pt;
                                        font-family: SimSun; mso-ascii-font-family: 'Times New Roman'; mso-hansi-font-family: 'Times New Roman'">
                                        ��</span><span lang="EN-US" style="font-size: 12pt"><span style="mso-spacerun: yes">&nbsp;&nbsp;
                                        </span></span><span style="font-size: 12pt; font-family: SimSun; mso-ascii-font-family: 'Times New Roman';
                                            mso-hansi-font-family: 'Times New Roman'">��</span><span lang="EN-US" style="font-size: 12pt"><span
                                                style="mso-spacerun: yes">&nbsp; </span></span><span lang="EN-US" style="mso-bidi-font-size: 10.5pt">
                                                    <o:p>
                                                    </o:p></span></p>
                    </td>
                </tr>
            </tbody>
            </table> </div>
        </ItemTemplate>
    </asp:Repeater>
    
    </form>
</body>
</html>
