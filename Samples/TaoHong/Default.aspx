<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

		<link rel="stylesheet" href="css/style.css" type="text/css"></link>
		<title>套红演示示例</title>
		<script type="text/javascript">
			function onColor(dd){
				dd.style.backgroundColor = "#D7FFEE";
			}
			function offColor(dd){
				dd.style.backgroundColor="";
			}
															
		</script>
	</head>
	<body>
		<!--header-->
		<div class="zz-headBox br-5 clearfix">
			<div class="zz-head mc">
				<!--logo-->
				<div class="logo fl">
					<a href="#"> <img src="images/logo.png" alt="" /> </a>
				</div>
				<!--logo end-->
				<ul class="head-rightUl fr">
					<li><a href="http://www.zhuozhengsoft.com" target="_blank">卓正网站</a></li>
                <li><a href="http://www.zhuozhengsoft.com/poask/index.asp" target="_blank">客户问吧</a></li>
                <li class="bor-0"><a href="http://www.zhuozhengsoft.com/contact-us.html" target="_blank">联系我们</a></li>
				</ul>
			</div>
		</div>
		<!--header end-->
		<!--content-->
		<div class="zz-content mc clearfix pd-28">
			<div class="demo mc">
				<h2 class="fs-16">
					PageOffice 实现模板套红
				</h2>
				<h3 class="fs-12">
					演示说明:
				</h3>
				
				<p>
					操作流程：编辑正文 → 套红 → 正式发文。
				</p>
				
			</div>
			
			<div class="zz-talbeBox mc">
				<h2 class="fs-12">
					文档列表
				</h2>
				<table class="zz-talbe">
					<thead>
						<tr onmouseover="onColor(this);" onmouseout="offColor(this);">		
							<th width="20%" style="text-align:center;">
								文档名称
							</th>
							<th width="20%" style="text-align:center;">
								创建日期
							</th>
							<th width="60%" style="text-align:center;">
								操作
							</th>
							
						</tr>
					</thead>
					<tbody>
						<tr>
							<td style="text-align:center;">测试文件</td>
							<td style="text-align:center;">2013-05-30</td>
							<td style="text-align:center;">
								<a href="Edit.aspx"><span style=" color:Green;">编辑正文</span></a>&nbsp;&nbsp;&nbsp;
								<a href="TaoHong.aspx"><span style=" color:Green;">套红</span></a>&nbsp;&nbsp;&nbsp;
								<a href="ReadOnly.aspx"><span style=" color:Green;">正式发文</span></a>
							</td>
						</tr>
					</tbody>
				</table>
			</div>
		</div>
		<!--content end-->
		<!--footer-->
		
		<!--footer end-->
	</body>
</html>
