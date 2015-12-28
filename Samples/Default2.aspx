<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>


<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" type="text/css" href="css/style.css" />
<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/pageoffice.js"></script>
<!--[if lte IE 6]>
<script type="text/javascript" src="js/belatedPNG.js"></script>
<script type="text/javascript">
  DD_belatedPNG.fix('.png_bg,.png_bg a:hover,img,li');
</script>
<![endif]-->
<title>在线演示示例</title>
    <style type="text/css">
        .style1
        {
            height: 111px;
        }
        html{/* IE6中防止抖动 */ 
            _background-image: url(about:blank); 
            _background-attachment: fixed; 
        } 
        #menubar{ 
            position:fixed;/*非IE6浏览器*/ 
            bottom:78px;  
            width:150px; 
            z-index:999; 
            height:175px; 
            border-left:solid 1px #ccc;
            _position: absolute;/*兼容IE6浏览器*/ 
            _top: expression(eval(document.documentElement.scrollTop+document.documentElement.clientHeight-19)); 
        }
        #menubar ul
        {
            margin-left:13px;  font-weight:bold; color:#999;
            }
        #menubar ul li
        {
            font-size:14px;
            }
        
        .maodian
        {
            z-index:10; 
        }
        .off
        {
            color:#999; list-style-type:none;
        }
        .on
        {
            color:#333; list-style-type:square;
        }
        
    </style>
    <script type="text/javascript">
        window.onload = function() {

            if (!PO_checkPageOffice()) {
                var poCheck = document.getElementById("po_check"); // 显示信息，提示客户安装PageOffice
                poCheck.innerHTML = "<span style='font-size:12px;color:red;'>请先安装<a href=\"pageoffice/posetup.exe\" style=\"border:solid 1px #0473b3;  padding:1px;\">PageOffice客户端</a></span>";

            }

        }
        

    </script>
</head>
<body>

<!--header-->
<div class="zz-headBox br-5 clearfix" >
	<div class="zz-head mc" id="pol">
    <!--logo-->
    	<div class="logo fl" id="pagetop"><a href="http://www.zhuozhengsoft.com/" target="_blank"><img src="images/logo.png" alt="" /></a></div>
    <!--logo end-->
        <ul class="head-rightUl fr">
        	<li><a href="http://www.zhuozhengsoft.com/" target="_blank">卓正网站</a></li>
            <li><a href="http://www.zhuozhengsoft.com/poask/index.asp" target="_blank">客户问吧</a></li>
            <li class="bor-0"><a href="http://www.zhuozhengsoft.com/contact-us.html" target="_blank">联系我们</a></li>
        </ul>
  	</div>
</div>
<!--header end-->


<!--content-->
<div class="zz-content mc clearfix pd-28">
    <div class="demo mc">
        <h2 class="fs-16" style="color:#477ccc;">卓正PageOffice 3.0组件使用样例</h2>
        <h3 class="fs-12" >演示说明:</h3>
        <p style=" text-indent:40px;" >
        PageOffice 3.0在2.0版本的基础上增加了全新的文件打开方式“PageOfficeLink 方式”，此方式提供了更完美的浏览器兼容性解决方案。<span id="po_check"></span>
        </p>
        <p style=" text-indent:40px;" >
        <span style=" font-weight:bold;"> PageOfficeLink ：简称POL</span>，是卓正公司为PageOffice在线打开文档的页面专门开发的特殊超链接；
        </p>
        <p style=" text-indent:40px;" >
       
            常规打开文档超链接的代码写法：&lt;a href=&quot;Word.aspx?id=12&quot;&gt;某某公司公文-12&lt;a&gt;</p>
        <p style=" text-indent:40px;" >
            POL打开文档超链接的代码写法：<br/>
       &lt;a href=&quot;<span style=" background-color:#D2E9FF;">&lt;%=PageOfficeLink.OpenWindow(&quot;</span>Word.aspx?id=12<span style=" background-color:#D2E9FF;">&quot;,&quot;width=800px;height=800px;&quot;)%&gt;</span>&quot;&gt;
     
            某某公司公文-12&lt;a&gt;
            &nbsp;</p>
        <p style=" text-indent:40px;">由于PageOffice产品的功能很多，为了便于新老用户都能找到自己需要的功能和示例，在此把 
            PageOffice 的示例分为以下五类：
        </p>
        <p style=" text-indent:40px;"><span style=" font-weight:bold;">基础功能：</span>
        演示最基本的在线打开、编辑、保存Office文档；控制PageOffice产品的界面，包括标题栏、菜单栏、自定义工具条、Office工具栏的隐藏和显示，添加自定义菜单和添加自定义按钮，修改标题栏文本等；控制Office功能是否可用，包括保存、另存、打印、拷贝等功能；
        </p>
        <p style=" text-indent:40px;"><span style=" font-weight:bold;">高级功能：</span>
        演示调用Pageoffice提供的接口填充数据到Word、Excel模版文件，动态生成文档的功能；演示控制Word、Excel中内容格式（包括字体、颜色、对其方式、表格线、单元格背景色、行高、列宽）的功能；演示从Word、Excel中提取指定位置数据，保存到数据库的功能；
        </p>
        <p style=" text-indent:40px;"><span style=" font-weight:bold;">综合演示：</span>
        通过几种模型示例演示PageOffice在各种应用场景中，调用PageOffice具体的API解决实际问题的效果；
        </p>
        <p style=" text-indent:40px;"><span style=" font-weight:bold;">其他技巧：</span>
        演示PageOffice在客户端通过js调用Office的VBA接口实现各种效果的技巧；
        </p>
        <br />
    </div>
    <%--<div class="demo mc">
        <h3 class="fs-12">流程模拟</h3>
    </div>--%>
    <div class="zz-talbeBox mc">
        <br />
    	<h2 style=" margin-left:320px;"  id="jichu" class="maodian">一、基础功能</h2>
    	<table class="zz-talbe">
        	<thead>
            	<tr>
                	<th style="width:40px;">类型 </th>
                    <th style="width:500px;">功能示例</th>
                    <th style="width:120px;">文件夹</th>
                </tr>
            </thead>
            <tbody>
            	<tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、1、<a href="<%=PageOfficeLink.OpenWindow("SimpleWord/Word.aspx","width=1300px;height=730px;")%>" >最简单在线打开保存Word文件（URL地址方式）</a>
                        <p >演示PageOffice实现最基本的在线打开保存服务器上Word文件的功能，也是最简单的一个集成PageOffice的示例，第一次接触PageOffice产品的用户可以参考此示例把PageOffice集成到自己的项目中。<span style="color:#333;">（此示例演示以窗口居中显示的方式打开文件。）</span></p></td>
                    <td>/SimpleWord</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、2、<a href="<%=PageOfficeLink.OpenWindow("SimpleWord/Word1.aspx","width=0;")%>" >以磁盘路径方式打开Office文件（以Word为例）</a>
                    <p>最简单的集成PageOffice的示例，但使用的是服务器磁盘路径的方式，这种方式的优点：1. 支持中文路径；2. 文件可以保存在服务器上的任意磁盘文件夹下。<span style="color:#333;">（此示例演示以窗口全屏显示的方式打开文件。）</span></p>
                    </td>
                    
                    <td>/SimpleWord</td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>一、3、<a href="<%=PageOfficeLink.OpenWindow("SimpleExcel/Excel.aspx","left=20px;top=10px;width=800px;height=800px;")%>" >最简单在线打开保存Excel文件（URL地址方式）</a>
                    <p>演示在线打开保存Excel文件的效果，与上面打开保存Word的代码几乎完全一样，只是WebOpen的第二个参数不一样。WebOpen方法的第二个参数需要与实际要打开的Office文档的文件格式保持一致。<span style="color:#333;">（此示例演示使用参数控制在线打开文件的窗口位置和大小。）</span></p>
                    </td>
                    <td>/SimpleExcel</td>
                </tr>
                <tr>
                	<td><img src="images/office-3.png" /></td>
                    <td>一、4、<a href="<%=PageOfficeLink.OpenWindow("SimplePPT/ppt.aspx","width=800px;height=800px;")%>" >最简单在线打开保存PPT文件（URL地址方式）</a><p>演示在线打开保存PPT文件的效果，与上面打开保存Word的代码几乎完全一样，只是WebOpen的第二个参数不一样。WebOpen方法的第二个参数需要与实际要打开的Office文档的文件格式保持一致</p>
                    </td>
                    <td>/SimplePPT</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、5、<a href="<%=PageOfficeLink.OpenWindow("TitleText/Word.aspx","width=800px;height=800px;")%>" >修改标题栏文本内容</a>
                    <p>通过给Caption属性赋值可以修改标题栏的文本内容，如果不给Caption赋值的话，标题栏默认显示的文本是：卓正 PageOffice 开发平台。</p>
                    </td>
                    <td>/TitleText</td>
                </tr>
                <tr>
                	<td class="style1"><img  src="images/office-1.png" /></td>
                    <td class="style1">一、6、<a href="<%=PageOfficeLink.OpenWindow("ControlBars/OpenWord.aspx","width=800px;height=800px;")%>"  >隐藏标题栏、菜单栏、自定工具栏和Office工具栏（以Word为例）</a>
                    <p>演示如何隐藏标题栏、菜单栏、自定工具栏和Office工具栏，每个栏都是可以单独的控制是否隐藏。</p>
                    </td>
                    <td class="style1">/ControlBars</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、7、<a href="<%=PageOfficeLink.OpenWindow("SetTheme/Word.aspx","width=800px;height=800px;")%>" >设置PageOffice界面的主题样式</a>
                    <p>通过设置Theme 属性，改变控件窗口的界面样式。有自定义界面、Office2007界面和Office2010界面共三种主题可选。</p>
                    </td>
                    <td>/SetTheme</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、8、<a href="<%=PageOfficeLink.OpenWindow("OpenWord/OpenWord.aspx","width=800px;height=800px;")%>" >最简单的只读打开Office文件（以Word为例）</a>
                    <p>实现只读模式打开Office文件，只需要修改WebOpen的第二个参数即可，PageOffice针对Word、Excel和PPT分别提供了docReadOnly、xlsReadOnly和pptReadOnly模式。</p>
                    </td>
                    <td>/OpenWord</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、9、<a href="<%=PageOfficeLink.OpenWindow("SavaReturnValue/SavaReturnValue.aspx","width=800px;height=800px;")%>" >文档保存后给前台页面返回开发者自定义的保存结果值（以Word为例）</a>
                    <p>通过后台代码设置PageOffice.FileSaver.CustomSaveResult 属性，给前台页面PageOffice对象返回一个用户自定义的值，以满足部分开发者给前台页面返回ID或其他保存结果的需求。</p>
                    </td>
                    <td>/SavaReturnValue</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、10、<a href="<%=PageOfficeLink.OpenWindow("SendParameters/EditWord.aspx","width=800px;height=800px;")%>" >给保存页面（SaveFilePage属性指向的页面）传递参数</a>
                    <p>此示例演示了通过给SaveFilePage属性赋值URL查询参数和页面元素（Input和Select）向保存页传递参数的方法。</p>
                    </td>
                    <td>/SendParameters</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、11、<a href="<%=PageOfficeLink.OpenWindow("DataRegionFill/DataRegionFill.aspx","width=800px;height=800px;")%>" >给Word文档中的数据区域（DataRegion）赋值的简单示例</a>
                    <p>此示例是一个最简单的给Word数据区域赋值的示例。预先在Word文档中手工设置一些DataRegion，通过PageOffice可以实现在文档中标记的位置处动态填充内容。在word中插入书签，只要书签的命名有前缀PO_ 就都被PageOffice视为DataRegion。</p>
                    </td>
                    <td>/DataRegionFill</td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>一、12、<a href="<%=PageOfficeLink.OpenWindow("ExcelFill/ExcelFill.aspx","width=800px;height=800px;")%>" >简单的给Excel表格赋值</a>
                    <p>此示例是一个最简单的给Excel单元格赋值的示例。</p>
                    </td>
                    <td>/ExcelFill</td>
                </tr>
                
                
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、13、<a href="<%=PageOfficeLink.OpenWindow("SubmitWord/SubmitWord.aspx","width=800px;height=800px;")%>" >最简单的提交Word中的用户输入内容</a>
                    <p>演示PageOffice使用WordReader对象获取Word文档中数据的效果。此示例仅演示了最基本的功能，更详细功能请参考“综合演示”示例。</p>
                    </td>
                    <td>/SubmitWord</td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>一、14、<a href="<%=PageOfficeLink.OpenWindow("SubmitExcel/SubmitExcel.aspx","width=800px;height=800px;")%>" >最简单的提交Excel中的用户输入内容</a>
                    <p>演示PageOffice使用ExcelReader对象获取Excel文档中单元格数据的效果。此示例仅演示了最基本的功能，更详细功能请参考“综合演示”示例。</p>
                    </td>
                    <td>/SubmitExcel</td>
                </tr>
               
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、15、<a href="<%=PageOfficeLink.OpenWindow("InsertSeal/Word.aspx","width=800px;height=800px;")%>" >演示加盖印章和签字功能（以Word为例）</a>
                    <p>演示PageOffice在线编辑时盖章和签字的功能。此示例仅演示用户使用印章功能时的操作和盖章后的效果，更多印章相关的功能请参考“综合演示”示例。印章管理平台可以轻易集成到您的软件系统中。</p>
                    </td>
                    <td>/InsertSeal</td>
                </tr>
                 
                <tr>
                	<td class="style1"><img  src="images/office-1.png" /></td>
                    <td class="style1">一、16、<a href="<%=PageOfficeLink.OpenWindow("CommandCtrl/Word.aspx","width=800px;height=800px;")%>"  >控制保存、另存和打印功能（以Word为例）</a>
                    <p>演示怎样分别禁止Office的保存、另存和打印功能。</p>
                    </td>
                    <td class="style1">/CommandCtrl</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、17、<a href="<%=PageOfficeLink.OpenWindow("WordSetTable/WordSetTable.aspx","width=800px;height=800px;")%>" >给Word文档中Table赋值的简单示例</a>
                    <p>演示了PageOffice对Word文档中Table的操作，包括给单元格赋值和动态添加行的效果。</p>
                    </td>
                    <td>/WordSetTable</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、18、<a href="<%=PageOfficeLink.OpenWindow("WordDataTag2/DataTag.aspx","width=800px;height=800px;")%>" >使用数据标签（DataTag）给Word文件填充文本数据</a>
                    <p>给Word模板中数据标签（DataTag）赋值，针对模板中有多处位置需要同一数据的需求，使用数据标签可以重复标记多处需要填充同一数据的位置，然后对数据标签编程实现填充模板生成文件。</p>
                    </td>
                    <td>/WordDataTag2</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、19、<a href="<%=PageOfficeLink.OpenWindow("CustomToolButton/Word.aspx","width=800px;height=800px;")%>" >在PageOffice自定义工具条上添加一个按钮（以Word为例）</a>
                    <p>给PageOffice自定义工具条上添加一个按钮，并设置点击时执行的代码。</p>
                    </td>
                    <td>/CustomToolButton</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、20、<a href="<%=PageOfficeLink.OpenWindow("AfterDocOpened/Word.aspx","width=800px;height=800px;")%>" >添加文档打开之后在页面里触发的事件（以Word为例）</a>
                    <p>演示怎样使用文档打开之后在页面里触发的事件，此事件很常用，需要在文件打开的时候执行的代码都可以放到此事件中执行。</p>
                    </td>
                    <td>/AfterDocOpened</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、21、<a href="<%=PageOfficeLink.OpenWindow("JsControlBars/Word.aspx","width=800px;height=800px;")%>" >用JS控制PageOffice窗口上各个工具栏的隐藏和显示（以Word为例）</a>
                    <p>演示怎样用JS控制标题栏、菜单栏、自定义工具栏、Office工具栏的隐藏和显示。</p>
                    </td>
                    <td>/JsControlBars</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、22、<a href="<%=PageOfficeLink.OpenWindow("ConcurrencyCtrl/Default.aspx","width=800px;height=800px;")%>" >打开文档使用“并发控制”（以Word为例）</a>
                    <p>演示使用TimeSlice属性设置打开文档的并发控制时间，防止多个用户同时打开一个文件，出现编辑保存文件相互覆盖的问题。</p>
                    </td>
                    <td>/ConcurrencyCtrl</td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>一、23、<a href="<%=PageOfficeLink.OpenWindow("ExcelTable/Table.aspx","width=800px;height=800px;")%>" >对Excel中的一块区域赋值，并自动增加行</a>
                    <p>演示使用PageOffice的方法OpenTable，实现行增长，还可以循环使用原模板Table区域（B4:F13）单元格样式。 </p>
                    </td>
                    <td>/ExcelTable</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、24、<a href="<%=PageOfficeLink.OpenWindow("SaveAsHTML/Word.aspx","width=800px;height=800px;")%>" >另存文件为HTML格式（以Word为例）</a>
                    <p>演示使用PageOffice的WebSaveAsHTML方法，另存文件为Html格式保存到服务器。 </p>
                    </td>
                    <td>/SaveAsHTML</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、25、<a href="<%=PageOfficeLink.OpenWindow("SaveAsMHT/Word.aspx","width=800px;height=800px;")%>" >另存文件为MHT格式（以Word为例）</a>
                    <p>演示使用PageOffice的WebSaveAsMHT方法，另存文件为MHT格式保存到服务器。 </p>
                    </td>
                    <td>/SaveAsMHT</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、26、<a href="<%=PageOfficeLink.OpenWindow("BeforeAndAfterSave/Word.aspx","width=800px;height=800px;")%>" >文档保存前和保存后触发的事件（以Word为例）</a>
                    <p>演示怎样使用文档保存之前和保存之后触发的事件，这两个事件很常用，需要在保存文档时执行的js代码，都可以放到这两个事件中执行。 </p>
                    </td>
                    <td>/BeforeAndAfterSave</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、27、<a href="<%=PageOfficeLink.OpenWindow("PageOfficeLink/Word.aspx","width=1200px;height=800px;")%>" >PageOfficeLink方式在线打开文档（以Word为例）</a>
                    <p>全新的文件打开方式“PageOfficeLink 方式”，此方式提供了更完美的浏览器兼容性解决方案。 </p>
                    </td>
                    <td>/PageOfficeLink</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>一、28、<a href="<%=PageOfficeLink.OpenWindow("SaveDataAndFile/Word.aspx","width=1200px;height=800px;")%>" target="_blank">既保存Word文档中指定位置的数据,又保存整篇文档（以Word为例）</a>
                    <p>演示如何将SaveDataPage和SaveFilePage方法结合使用,以达到同时保存数据和文件的效果。</p>
                    </td>
                    <td>/SaveDataAndFile</td>
                </tr>
            </tbody>
        </table>
        <br />
        <div id="maoDiv"></div>
        <h2 style=" margin-left:300px;" id="gaoji" class="maodian">二、高级功能</h2>
        <a style=" margin-bottom:10px; text-decoration:underline;" href="#pagetop"><img src="images/arrow_px_up.gif" />回页首</a>
    	<table class="zz-talbe">
        	<thead>
            	<tr>
                	<th style="width:40px;">类型 </th>
                    <th style="width:500px;">功能示例</th>
                    <th style="width:120px;">文件夹</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、1、<a href="<%=PageOfficeLink.OpenWindow("ReadOnly/OpenWord.aspx","width=800px;height=800px;")%>" >文件在线安全浏览（以Word为例）</a>
                    <p>使用只读模式在线打开Word文件，禁止编辑、拷贝、打印、另存。</p>
                    <p>安全浏览文档禁止：编辑、复制、粘贴、右键菜单、选择、下载、另存、F12下载、PrintScreen拷屏等操作。</p>
                    </td>
                    <td>/ReadOnly</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、2、<a href="<%=PageOfficeLink.OpenWindow("DataBase/Stream.aspx","width=800px;height=800px;")%>" >打开保存数据库中的文件（以Word为例）</a>
                    <p>演示如何使用PageOffice以流的方式打开数据库中保存的文件。不推荐把文件保存在数据库中，不便于调试，并且影响数据库的查询速度。</p>
                    </td>
                    <td>/DataBase</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、3、<a href="<%=PageOfficeLink.OpenWindow("CreateWord/word-lists.aspx","width=800px;height=800px;")%>" >新建文件（以Word为例）</a>
                    <p>演示系统中创建文档的两种方式：1.复制文件创建新文件方法；2.利用PageOffice的WebCreateNew方法创建新文件。</p>
                    </td>
                    <td>/CreateWord</td>
                </tr>
                <tr>
                	<td><img src="images/pdf.jpg" /></td>
                    <td>二、4、<a href="<%=PageOfficeLink.OpenWindow("POPDF/PDF.aspx","width=800px;height=800px;")%>" >在线打开PDF文件</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示使用POPDF组件在线打开PDF文件的效果，标题栏、菜单栏、工具栏和自定义工具条都可以分别隐藏，同时自定义工具条上的按钮的数量和功能均可编程控制。</p>
                    </td>
                    <td>/POPDF</td>
                </tr>
                <tr>
                	<td><img src="images/pdf.jpg" /></td>
                    <td>二、5、<a href="<%=PageOfficeLink.OpenWindow("SaveAsPDF/WordToPDF.aspx","width=800px;height=800px;")%>" >Office文件转换为PDF文件（以Word为例）</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示打开Word文件，转存为PDF格式到服务器的效果。</p>
                    </td>
                    <td>/SaveAsPDF</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、6、<a href="<%=PageOfficeLink.OpenWindow("WordResWord/Word.aspx","width=800px;height=800px;")%>" >后台编程插入Word文件到数据区域</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>此示例演示了通过后台编程，实现打开文件时多个Word文件插入到模板指定位置，生成一个合并文档的效果。</p>
                    </td>
                    <td>/WordResWord</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、7、<a href="<%=PageOfficeLink.OpenWindow("WordResImage/Word.aspx","width=800px;height=800px;")%>" >后台编程插入图片到数据区域</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>此示例在示例9的基础上做了修改，实现了图片和Word文件混合插入到模板指定位置，生成一个合并文档的效果。</p>
                    </td>
                    <td>/WordResImage</td>
                </tr>
                 <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、8、<a href="<%=PageOfficeLink.OpenWindow("WordResExcel/Word.aspx","width=800px;height=800px;")%>" >后台编程插入Excel文件到数据区域</a><span style=" color:Red;">（企业版）</span>
                    <p>此示例在上一个示例的基础上做了修改，实现了Word和Excel文件混合插入到模板指定位置，生成一个合并文档的效果。</p>
                    </td>
                    <td>/WordResExcel</td>
                </tr>
                
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、9、<a href="<%=PageOfficeLink.OpenWindow("AddWaterMark/AddWaterMark.aspx","width=800px;height=800px;")%>" >给Word文档添加水印</a><span style=" color:Red;">（企业版）</span>
                    <p>通过设置PageOffice.WordWriter.WaterMark 属性，给Word文档添加水印。</p>
                    </td>
                    <td>/AddWaterMark</td>
                </tr>

                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、10、<a href="<%=PageOfficeLink.OpenWindow("WordDataTag/DataTag.aspx","width=800px;height=800px;")%>" >使用数据标签（DataTag）给Word文件填充带格式的数据</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>给Word模板中数据标签（DataTag）赋值，针对模板中有多处位置需要同一数据的需求，使用数据标签可以重复标记多处需要填充同一数据的位置，然后对数据标签编程实现填充模板生成文件。</p>
                    </td>
                    <td>/WordDataTag</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、11、<a href="<%=PageOfficeLink.OpenWindow("DataRegionCreate/DataRegionCreate.aspx","width=800px;height=800px;")%>" >在Word中动态创建数据区域</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示使用CreateDataRegion方法动态添加数据区域的功能。动态添加数据区域，可以在生成Word文件的时候更灵活，甚至可以从空白的Word文件生成一个图文并茂的文件（详见“高级功能”示例）。</p>
                    </td>
                    <td>/DataRegionCreate</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、12、<a href="<%=PageOfficeLink.OpenWindow("RunMacro/Word.aspx","width=800px;height=800px;")%>" >执行文档中的宏命令（以Word为例）</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>使用PageOffice的RunMacro方法可以运行Office文档本身包含的宏命令。</p>
                    </td>
                    <td>/RunMacro</td>
                </tr>
                
                
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、13、<a href="<%=PageOfficeLink.OpenWindow("FileMakerSingle/Default.aspx","width=800px;height=800px;")%>" >FileMaker转换单个文档（以Word为例）</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示使用FileMaker对象动态生成文件的效果。虽然还是在客户端生成文件后保存到服务器上的，但是不在客户端显式的打开文件。</p>
                    </td>
                    <td>/FileMakerSingle</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、14、<a href="<%=PageOfficeLink.OpenWindow("WordTable/WordTable.aspx","width=800px;height=800px;")%>" >向Word文档中的Table插入新行并赋值</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了PageOffice给Word中表格插入新行的功能，同时也演示了如何给存在纵向合并单元格的表格添加新行。</p>
                    </td>
                    <td>/WordTable</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、15、<a href="<%=PageOfficeLink.OpenWindow("WordHandDraw/Word.aspx","width=800px;height=800px;")%>" >手写批注接口演示</a><span style=" color:Red;">（企业版）</span>
                    <p>演示了如何使用程序控制手写批注的线宽、颜色、缩放、笔触类型等功能。</p>
                    </td>
                    <td>/WordHandDraw</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、16、<a href="<%=PageOfficeLink.OpenWindow("DataRegionTable/SetDataRegionTable.aspx","width=800px;height=800px;")%>" >获取Word文件中表格的数据</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了获取Word表格中的数据。要想获取表格中的数据，前提是：这个表格必须在一个数据区域内。使用数据区域对象的OpenTable方法就可以获取到表格中各个单元格的数据。</p>
                    </td>
                    <td>/DataRegionTable</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、17、<a href="<%=PageOfficeLink.OpenWindow("DataRegionText/Default.aspx","width=800px;height=800px;")%>" >控制数据区域文本的样式</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了如何使用程序控制数据区域文本的样式，包括设置文本的字体、字号、颜色、对齐方式。</p>
                    </td>
                    <td>/DataRegionText</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、18、<a href="<%=PageOfficeLink.OpenWindow("SetDrByUserWord/Default.aspx","width=800px;height=800px;")%>" >控制不同用户编辑Word文档中不同的区域</a><span style=" color:Red;"></span>
                    <p>演示了如何使用程序控制不同用户打开文件后，只能编辑Word文档中属于自己的区域。</p>
                    </td>
                    <td>/SetDrByUserWord</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、19、<a href="<%=PageOfficeLink.OpenWindow("SetDrByUserWord2/Default.aspx","width=800px;height=800px;")%>" >控制不同用户编辑Word文档中不同的区域（可同时编辑）</a><span style=" color:Red;">（企业版）</span>
                    <p>演示了如何使用程序控制不同用户打开文件后，只能编辑Word文档中属于自己的区域。用此方法开发的话，支持多个人同时打开一个文件编辑各自的区域而互不影响的。</p>
                    </td>
                    <td>/SetDrByUserWord2</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、20、<a href="<%=PageOfficeLink.OpenWindow("SetHandDrawByUser/Default.aspx","width=800px;height=800px;")%>" >控制用户打开文件只能看到自己的手写</a><span style=" color:Red;">（企业版）</span>
                    <p>演示了如何使用程序控制用户打开文件后，只能看到自己手写的内容。使用HandDraw对象的ShowByUserName方法控制手写内容的显示和隐藏。</p>
                    </td>
                    <td>/SetHandDrawByUser</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、21、<a href="<%=PageOfficeLink.OpenWindow("MergeWordCell/Default.aspx","width=800px;height=800px;")%>" >使用程序合并Word文件中表格的单元格并赋值</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了使用MergeTo方法合并Word文件中表格的指定单元格，并填充文本数据，设置文字的字体、样式和对齐方式。</p>
                    </td>
                    <td>/MergeWordCell</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、22、<a href="<%=PageOfficeLink.OpenWindow("ClickDataRegion/Default.aspx","width=800px;height=800px;")%>" >响应数据区域点击事件</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了获取数据区域点击事件，实现禁止用户直接编辑数据区域的内容。用此方法可以实现下拉框选择数据的功能。</p>
                    </td>
                    <td>/ClickDataRegion</td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>二、23、<a href="<%=PageOfficeLink.OpenWindow("MergeExcelCell/Default.aspx","width=800px;height=800px;")%>" >使用程序合并Excel的单元格并设置格式和赋值</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了使用程序合并指定的Excel单元格，并设置文本格式和赋值。</p>
                    </td>
                    <td>/MergeExcelCell</td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>二、24、<a href="<%=PageOfficeLink.OpenWindow("SetXlsTableByUser/Default.aspx","width=800px;height=800px;")%>" >控制不同用户编辑Excel文档中不同的区域</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了如何使用程序控制不同用户打开文件后，只能编辑Excel文档中属于自己的区域。</p>
                    </td>
                    <td>/SetXlsTableByUser</td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>二、25、<a href="<%=PageOfficeLink.OpenWindow("SetExcelCellBorder/Default.aspx","width=800px;height=800px;")%>" >使用程序 “绘制” Excel表格线</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了如何通过对ExcelWriter对象编程，在Excel文档中设置各个单元格或区域的边框样式，也就是设置Excel的表格线样式。</p>
                    </td>
                    <td>/SetExcelCellBorder</td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>二、26、<a href="<%=PageOfficeLink.OpenWindow("SetExcelCellText/Default.aspx","width=800px;height=800px;")%>" >用程序设置Excel单元格文本的字体、颜色、对齐和背景色</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了如何通过对ExcelWriter对象编程，设置Excel各个单元格文本的字体和颜色，设置单元格的对齐方式和背景色。</p>
                    </td>
                    <td>/SetExcelCellText</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、27、<a href="<%=PageOfficeLink.OpenWindow("DataRegionFill2/DataRegionFill.aspx","width=800px;height=800px;")%>" >给Word文档中的数据区域（DataRegion）赋值并设置样式 </a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>此示例是一个最简单的给Word数据区域赋值的示例。预先在Word文档中手工设置一些DataRegion，通过PageOffice可以实现在文档中标记的位置处动态填充内容并设置文本的样式。</p>
                    </td>
                    <td>/DataRegionFill2</td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>二、28、<a href="<%=PageOfficeLink.OpenWindow("ExcelCellClick/SubmitExcel.aspx","width=800px;height=800px;")%>" >响应Excel单元格点击事件</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了Excel单元格点击事件的使用，可以实现禁止用户直接编辑单元格内容的情况下，用下拉框选择数据的功能。</p>
                    </td>
                    <td>/ExcelCellClick</td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>二、29、<a href="<%=PageOfficeLink.OpenWindow("ExcelFill2/ExcelFill.aspx","width=800px;height=800px;")%>" >简单的给Excel单元格赋值设置文本颜色</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了用程序给Excel单元格填充数据，并设置文本的颜色。</p>
                    </td>
                    <td>/ExcelFill2</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、30、<a href="<%=PageOfficeLink.OpenWindow("DataRegionEdit/Default.aspx","width=800px;height=800px;")%>" >用户自定义模板中数据区域（DataRegion）的位置</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了使用PageOffice封装好的数据区域管理窗口，实现用户自己编辑模板，定义模板中各个数据区域位置的效果。</p>
                    </td>
                    <td>/DataRegionEdit</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、31、<a href="<%=PageOfficeLink.OpenWindow("DataTagEdit/Default.aspx","width=800px;height=800px;")%>" >用户自定义模板中数据标签（DataTag）的位置</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了使用PageOffice封装好的数据标签管理窗口，实现用户自己编辑模板，定义模板中各个数据标签位置的效果。</p>
                    </td>
                    <td>/DataTagEdit</td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>二、32、<a href="<%=PageOfficeLink.OpenWindow("DefinedNameCell/ExcelFill.aspx","width=800px;height=800px;")%>" >给Excel模板中定义了名称的单元格赋值</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>Excel自身有一个“定义名称”的功能，可以给任意的单元格定义一个名称，比如定义某个单元格的名称为：testA1，此示例演示了，如何给这个名称为“testA1”的单元格赋值。</p>
                    </td>
                    <td>/DefinedNameCell</td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>二、33、<a href="<%=PageOfficeLink.OpenWindow("DefinedNameTable/Default.aspx","width=800px;height=800px;")%>" >给Excel模板中定义了名称的一块区域赋值</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>Excel自身有一个“定义名称”的功能，可以给选中的一块区域（在PageOffice的概念里称这块区域为一个Table）定义一个名称，比如定义区域“B4:F13”的名称为：report，此示例演示了，如何给这个名称为“report”的Table赋值。</p>
                    </td>
                    <td>/DefinedNameTable</td>
                </tr>
                <tr>
                	<td><img src="images/pdf.jpg" /></td>
                    <td>二、34、<a href="<%=PageOfficeLink.OpenWindow("FileMakerPDF/Default.aspx","width=800px;height=800px;")%>" >FileMaker转换单个文档为PDF（以Word为例） </a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示使用FileMaker对象动态生成 PDF 文件的效果。虽然还是在客户端生成PDF文件后保存到服务器上的，但是不在客户端显式的打开文件。</p>
                    </td>
                    <td>/FileMakerPDF</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、35、<a href="<%=PageOfficeLink.OpenWindow("WordCompare/Compare.aspx","width=800px;height=800px;")%>" >演示比较两个版本的Word文档的功能 </a><span style=" color:Red;">（企业版）</span>
                    <p>使用PageOffice同时在线打开两个版本的Word文档，切换显示其中的一个文档，或同时显示两个文档对比文档内容，实现在线的文档内容比较功能。</p>
                    </td>
                    <td>/WordCompare</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、36、<a href="<%=PageOfficeLink.OpenWindow("WordTextBox/TextBoxFill.aspx","width=800px;height=800px;")%>" >给Word文本框中的数据区域赋值 </a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>给Word文档中文本框里的数据区域赋值，实现填充数据到word文件中某些特殊位置的效果。</p>
                    </td>
                    <td>/WordTextBox</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、37、<a href="<%=PageOfficeLink.OpenWindow("WordRibbonCtrl/Word.aspx","width=0;")%>" target="_blank">控制Word的Ribbon工具栏 </a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>隐藏或显示Word的Ribbon工具栏中的按钮、面板、命令分组。</p>
                    </td>
                    <td>/WordRibbonCtrl</td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>二、38、<a href="<%=PageOfficeLink.OpenWindow("ExcelRibbonCtrl/Excel.aspx","width=0;")%>" target="_blank">控制Excel的Ribbon工具栏 </a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>隐藏或显示Excel的Ribbon工具栏中的按钮、面板、命令分组。</p>
                    </td>
                    <td>/ExcelRibbonCtrl</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、39、<a href="<%=PageOfficeLink.OpenWindow("SplitWord/Word.aspx","width=0;")%>" target="_blank">拆分Word文档：把数据区域中的内容保存为子文档</a><span style=" color:Red;">（企业版）</span>
                    <p>保存文件时，把Word文档中属性SubmitAsFile = true的数据区域中的内容提取出来，保存为一个子文件。</p>
                    </td>
                    <td>/SplitWord</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、40、<a href="<%=PageOfficeLink.OpenWindow("CommentsList/Word.aspx","width=1400px;height=800px;")%>" target="_blank">Word的两种新建批注方式和批注列表效果</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了两种新建批注的方式：使用WordInsertComment方法新建批注和使用RunMacro方法调用VBA新建批注。使用RunMacro方法调用VBA接口在网页上实现批注列表，及点击批注列表中的批注导航到批注所在页的效果。</p>
                    </td>
                    <td>/CommentsList</td>
                </tr>
                 <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、41、<a href="<%=PageOfficeLink.OpenWindow("RevisionsList/Word.aspx","width=1400px;height=800px;")%>" target="_blank">Word中显示痕迹列表效果</a>
                    <p>演示了遍历当前文档中的所有痕迹以列表的形式显示以及点击痕迹列表中的痕迹导航到痕迹所在页的效果。</p>
                    </td>
                    <td>/RevisionsList</td>
                </tr>
                 <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、42、<a href="<%=PageOfficeLink.OpenWindow("HandDrawsList/Word.aspx","width=1400px;height=800px;")%>" target="_blank">Word显示手写批注列表效果</a><span style=" color:Red;">（企业版）</span>
                    <p>演示了遍历当前文档中的所有手写批注以列表的形式显示以及点击手写批注列表中的手写批注导航到批注所在页的效果。</p>
                    </td>
                    <td>/HandDrawsList</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、43、<a href="<%=PageOfficeLink.OpenWindow("WordCreateTable/createTable.aspx","width=800px;height=800px;")%>" >在Word文档中动态创建表格并赋值</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了如何在Word文档中动态创建多个表格并赋值。</p>
                    </td>
                    <td>/WordCreateTable</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>二、44、<a href="<%=PageOfficeLink.OpenWindow("RunMacro2/Word.aspx","width=800px;height=800px;")%>" >执行文档中有返回值的宏命令（以Word为例）</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>使用PageOffice的RunMacro方法可以运行Office的宏命令，并获取宏的返回值。</p>
                    </td>
                    <td>/RunMacro2</td>
                </tr>
                <tr>
                	<td><img src="images/pdf.jpg" /></td>
                    <td>二、45、<a href="<%=PageOfficeLink.OpenWindow("PDFSearch/PDF.aspx","width=1400px;height=800px;")%>">PDF文档中的关键字搜索</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了如何在PDF文档中以“上一个”和“下一个”的形式搜索关键字。</p>
                    </td>
                    <td>/PDFSearch</td>
                </tr>
            </tbody>
        </table>
        
        <br />
        <h2 style=" margin-left:300px;" id="zonghe" class="maodian">三、综合演示</h2>
        <a style=" margin-bottom:10px; text-decoration:underline;" href="#pagetop"><img src="images/arrow_px_up.gif" />回页首</a>
    	<table class="zz-talbe">
        	<thead>
            	<tr>
                	<th style="width:40px;">类型 </th>
                    <th style="width:500px;">功能示例</th>
                    <th style="width:120px;">文件夹</th>
                </tr>
            </thead>
            <tbody>
 
                
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>三、1、<a href="<%=PageOfficeLink.OpenWindow("FileMaker/Default.aspx","width=800px;height=800px;")%>" >FileMaker批量转换文档（以Word为例）</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示动态生成多个Word文件的效果。</p>
                    </td>
                    <td>/FileMaker</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>三、2、<a href="<%=PageOfficeLink.OpenWindow("ExaminationPaper/Default.aspx","width=800px;height=800px;")%>" >在Word文档中动态生成一张试卷</a>
                    <p>演示选择题库中的部分试题，动态生成一份试卷的效果。如果使用动态生成js的方式来实现，那么所有的PageOffice版本都可以支持；<span style="color:Maroon;">如果使用动态创建数据区域的方式来实现，编程会更简单，但是标准版不支持。</span> </p>
                    </td>
                    <td>/ExaminationPaper</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>三、3、<a href="http://www.zhuozhengsoft.com/down3/ASP.NET/BigDemo/WordDemo.rar" >在OA或文档系统里文件流转中的使用效果</a>
                    <p>用修改无痕迹模式起草文件，各个领导批注自己意见的时候使用强制留痕模式打开，文员清稿的时候用核稿模式打开，还有最后只读模式打开发布的正式文件。其中在领导批注环节也演示了PageOffice提供的手写功能，在文件核稿之后可以加盖印章。</p>
                    </td>
                    <td><p style="color:Red;">/卓正网站 / WordDemo.rar</p></td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>三、4、<a href="http://www.zhuozhengsoft.com/down3/ASP.NET/BigDemo/ExcelDemo.rar">对Excel文件格式提供的两种编辑模式（编辑模式和只读模式）</a>
                    <p>演示了PageOffice打开编辑保存Excel文件的效果，还有在Excel中手写圈阅和盖章的效果。</p>
                    </td>
                    <td><p style="color:Red;">/卓正网站 / ExcelDemo.rar</p></td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>三、5、<a href="http://www.zhuozhengsoft.com/down3/ASP.NET/BigDemo/POWord.rar">请假条示例</a>
                    <p>演示了PageOffice对Word模板的数据填充生成正式文件效果，同时演示了从Word文件中获取数据提交到服务器端保存到数据库中的效果，同时还可以看到PageOffice对Word文件中可编辑区域的控制效果，不但可以控制哪些区域可以编辑，还可以控制哪些区域只能以选择的方式选择指定的数据来修改内容。</p>
                    </td>
                    <td><p style="color:Red;">/卓正网站 / POWord.rar</p></td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>三、6、<a href="http://www.zhuozhengsoft.com/down3/ASP.NET/BigDemo/POExcel.rar">模拟了一个简易的订单系统</a>
                    <p>演示了PageOffice对Excel模板的数据填充生成Excel文件，演示了获取Excel表格中的数据保存到数据库，演示了用PageOffice填充数据库数据到Excel报表模板生成Excel报表，演示了填充不定行数据到模板表格中行自动增长效果。</p>
                    </td>
                    <td><p style="color:Red;">/卓正网站 / POExcel.rar</p></td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>三、7、<a href="<%=PageOfficeLink.OpenWindow("WordParagraph/Word.aspx","width=800px;height=800px;")%>" >完全编程实现动态生成Word文件</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了使用PageOffice.WordWriter命名空间中提供的类，用纯代码编程的方式在一个空白的Word文件中生成一个图文并茂、文本段落格式均已设置好的Word文档。</p>
                    </td>
                    <td>/WordParagraph</td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>三、8、<a href="<%=PageOfficeLink.OpenWindow("DrawExcel/excel.aspx","width=800px;height=800px;")%>" >完全编程实现动态生成Excel文件</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了使用PageOffice.ExcelWriter命名空间中提供的类，用纯代码编程的方式在一个空白的Excel文件中“绘制”一个包含了复杂公式的、表格线和文本颜色俱全、单元格格式完美并填充了数据的Excel表。</p>
                    </td>
                    <td>/DrawExcel</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>三、9、<a href="<%=PageOfficeLink.OpenWindow("TaoHong/Default.aspx","width=800px;height=800px;")%>" >使用PageOffice实现模板套红</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了使用PageOffice的数据填充功能实现Word文件套红的效果。</p>
                    </td>
                    <td>/TaoHong</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>三、10、<a href="<%=PageOfficeLink.OpenWindow("WordSalaryBill/Default.aspx","width=800px;height=800px;")%>" >插入Word表格模板动态生成工资条</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了插入Word文件、填充Word表格数据、合并Word文件、循环插入表格等功能。</p>
                    </td>
                    <td>/WordSalaryBill</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>三、11、<a href="http://www.zhuozhengsoft.com/down3/ASP.NET/BigDemo/HuiQianDan.rar" >“汇签单”效果</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了使用数据区域控制不同用户编辑不同区域，实现汇签单的效果。</p>
                    </td>
                    <td><p style="color:Red;">/卓正网站 / HuiQianDan.rar</p></td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>三、12、<a href="http://www.zhuozhengsoft.com/down3/ASP.NET/BigDemo/TemplateEdit.rar">实现“用户自定义Word模板”动态生成文件</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示了如何通过用户自定义模板实现更灵活的动态填充生成Word文档。</p>
                    </td>
                    <td><p style="color:Red;">/卓正网站 / TemplateEdit.rar</p></td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>三、13、<a href="<%=PageOfficeLink.OpenWindow("PrintFiles/Default.aspx","width=800px;height=800px;")%>" >FileMaker批量生成并打印文件（以Word为例）</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示动态生成多个Word文件并批量打印的效果，如果只需要批量打印功能也可以参考此示例。</p>
                    </td>
                    <td>/PrintFiles</td>
                </tr>
            </tbody>
        </table>
        
        <br />
        
        <h2 style=" margin-left:300px;" id="jiqiao" class="maodian">四、其他技巧</h2>
        <a style=" margin-bottom:10px; text-decoration:underline;" href="#pagetop"><img src="images/arrow_px_up.gif" />回页首</a>
    	<table id="mytable" class="zz-talbe">
        	<thead>
            	<tr>
                	<th style="width:40px;">类型 </th>
                    <th style="width:500px;">功能示例</th>
                    <th style="width:120px;">文件夹</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>四、1、<a href="<%=PageOfficeLink.OpenWindow("DeleteRow/Word.aspx","width=800px;height=800px;")%>" >js 删除Word表格中光标所在行</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示使用js调用Office的VBA接口删除Word表格中一行单元格的效果。</p>
                    </td>
                    <td>/DeleteRow</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>四、2、<a href="<%=PageOfficeLink.OpenWindow("HiddenRulars/Word.aspx","width=800px;height=800px;")%>" >显示/隐藏Word文件中的标尺</a>
                    <p>演示使用js调用Office的VBA接口隐藏Word标尺的效果。</p>
                    </td>
                    <td>/HiddenRulars</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>四、3、<a href="<%=PageOfficeLink.OpenWindow("WordAddBKMK/WordAddBKMK.aspx","width=800px;height=800px;")%>" >在Word当前光标处插入书签</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示使用js调用Office的VBA接口在文件中插入书签的功能。</p>
                    </td>
                    <td>/WordAddBKMK</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>四、4、<a href="<%=PageOfficeLink.OpenWindow("WordLocateBKMK/Word.aspx","width=800px;height=800px;")%>" >js 定位光标到书签</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示使用js调用Office的VBA接口，定位光标到书签所在位置，一般可以用来实现盖章自动到指定位置的效果。</p>
                    </td>
                    <td>/WordLocateBKMK</td>
                </tr>
                 <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>四、5、<a href="<%=PageOfficeLink.OpenWindow("WordHyperLink/Word.aspx","width=800px;height=800px;")%>" >Word 中插入超文本链接url</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示使用js调用Office的VBA接口，在Word中插入超链的效果。</p>
                    </td>
                    <td>/WordHyperLink</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>四、6、<a href="<%=PageOfficeLink.OpenWindow("WordMergeCell/Word.aspx","width=800px;height=800px;")%>" >js合并Word单元格</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示使用js调用Office的VBA接口，实现对Word中单元格的合并操作。</p>
                    </td>
                    <td>/WordMergeCell</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>四、7、<a href="<%=PageOfficeLink.OpenWindow("WordGetSelection/Word.aspx","width=800px;height=800px;")%>" >js获取Word选中的文字</a>
                    <p>演示使用js调用Office的VBA接口，获取到文件中目前选中的文本内容。</p>
                    </td>
                    <td>/WordGetSelection</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>四、8、<a href="<%=PageOfficeLink.OpenWindow("WordGoToPage/Word.aspx","width=800px;height=800px;")%>" >js实现Word跳转到指定页面</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示使用js调用Office的VBA接口，实现跳转到Word文档的指定页面和获取文档总页数。</p>
                    </td>
                    <td>/WordGoToPage</td>
                </tr>
                <tr>
                	<td><img src="images/office-2.png" /></td>
                    <td>四、9、<a href="<%=PageOfficeLink.OpenWindow("JsOpXlsCellText/Excel.aspx","width=800px;height=800px;")%>" >js获取和设置Excel单元格的值</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示使用js调用Office的VBA接口，实现获取和设置Excel文档中指定单元格的值。</p>
                    </td>
                    <td>/JsOpXlsCellText</td>
                </tr>
                <tr>
                	<td><img src="images/office-1.png" /></td>
                    <td>四、10、<a href="<%=PageOfficeLink.OpenWindow("InsertPageBreak/Word.aspx", "width=800px;height=800px;")%>" >js实现在Word光标处插入分页符</a><span style=" color:Red;">（专业版、企业版）</span>
                    <p>演示使用js调用Office的VBA接口，实现在Word文档光标处插入分页符。</p>
                    </td>
                    <td>/InsertPageBreak</td>
                </tr>
            </tbody>
        </table>
     </div>
</div>
<!--content end-->
<br /><br /><br />
<!--footer-->
<div class="login-footer clearfix">Copyright &copy 2013 北京卓正志远软件有限公司</div>
<!--footer end-->


<div id="menubar"  >
    <br />
    <div style=" font-size:16px; font-weight:bold; color:#aaa;padding-left:20px;  "> [目录] </div>
    <ul>    
        <li id="p0" class="menuli">一、<a href="#jichu" class="off">基础功能</a></li>
        <li id="p1" class="menuli">二、<a href="#gaoji" class="off">高级功能</a></li>
        <li id="p2" class="menuli">三、<a href="#zonghe" class="off">综合演示</a></li>
        <li id="p3" class="menuli">四、<a href="#jiqiao" class="off">其他技巧</a></li>
        <li id="p4" class="menuli">五、<a href="http://www.zhuozhengsoft.com/download.html" class="off" target="_blank">更多示例</a></li>
    </ul>
</div>

<script type="text/javascript">

    function getIE(e) {
        var t = e.offsetTop;
        var l = e.offsetLeft;
        while (e = e.offsetParent) {
            t += e.offsetTop;
            l += e.offsetLeft;
        }
        //document.getElementById("menubar").innerHTML = "l=" + l;
        return l;
    }

    function setMenuBarPos2() {
        if (isIE = navigator.userAgent.indexOf("MSIE") != -1) {
            document.getElementById("menubar").style.left = getIE(document.getElementById("maoDiv")) + 710 - document.documentElement.scrollLeft + "px";
        }
        else {
            document.getElementById("menubar").style.left = getIE(document.getElementById("maoDiv")) + 710 - document.body.scrollLeft + "px";
        }
    }
    
    //加载后设置一次
    setMenuBarPos2();
    
    window.onresize = function() {
        setMenuBarPos2();
    }
    window.onscroll = function() {
        setMenuBarPos2();
    }
</script>

<script type="text/javascript">
    $(function() {
        //返回顶部
        $('.home').click(function() {
            $("html,body").animate({ scrollTop: 0 }, 1000);
            return false;
        })

        //遍历锚点
        var mds = $(".maodian");
        var arrMd = [];
        for (var i = 0, len = mds.length; i < len; i++) {
            arrMd.push($(mds[i]));

        }

        function update() {
            var scrollH = $(window).scrollTop();
            for (var i = 0; i < len; i++) {
                var mdHeight = arrMd[i].offset().top;
                if (mdHeight-50 < scrollH) {
                    navon(i); 
                }
            }
        }

        //高亮导航菜单
        function navon(id) {
            $('.menuli').removeClass('on');
            $('.menuli a').removeClass('on');
            $('.menuli a').addClass('off');
            $('#p' + id).addClass('on');
            $('#p' + id + " a").addClass('on');
        }

        //绑定滚动事件
        $(window).bind('scroll', update);
    })
</script>
</body>
</html>
