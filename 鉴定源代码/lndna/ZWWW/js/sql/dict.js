var dict_case_status_json = "[{ 'id':'1','name':'待送检'},{ 'id':'2','name':'待受理'},{ 'id':'3','name':'已受理'},{ 'id':'-1','name':'拒绝受理'}]";

function dict_DictInit(btn, id) {
    ws_exec(btn, "PUT", "DictSettings", "DictInit", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "读取成功", "");
}
function dict_GetList(table, data, val) {
    var json = eval("(" + data + ")");
    var DICT_id = new Array();
    var DICT_name = new Array();
    var idx = 0;
    $(json).each(function () {
        DICT_id[idx] = this.id;
        DICT_name[idx] = this.name;
        idx++;
    });
    if (DICT_id.length == 0) return;
    init_select(table, "选择", DICT_id, DICT_name);
    fill_select_nde(table, val);
}
function dict_GetValue(data, id) {
    if (id == null || id.length == 0) return "";
    var json = eval("(" + data + ")");
    var name = "";
    $(json).each(function () {
        if (name.length > 0) return name;
        if (id == this.id) { name = this.name; return false; }
    });
    return name;
}
function dict_page_frame_GetCont(table, name, sys_user_login_name) {
    if (name == "head" && sys_user_login_name.length == 0) {
        $("#" + table).html("<div class='bg' id='head'><div class='w1000 cen len'><ul class='f_l'><div id='append_parent'></div>欢迎来到综合管理系统<span></span></ul></div></div><div id='see'><div class='w1000 cen c_ban' id='ban'><div class='slogo2013'><a href='javascript:void(0)' title='综合管理系统'><img width='350' height='70' src='image/see-logo.jpg'></a></div></div></div>");
    }
    else if (name == "head" && sys_user_login_name.length > 0) {
        $("#" + table).html("<div class='bg' id='head'><div class='w1000 cen len'><ul class='f_l'><div id='append_parent'></div>您好&nbsp;[<a href='../sys_user/main.htm' style='color:blue;font-weight:bolder'>" + sys_user_login_name + "</a>]&nbsp;,欢迎来到综合管理系统!<span>[<a href='../login.htm'>退出</a>] </span></ul></div></div><div id='see'></div>");
    }
    else if (name == "foot" && sys_user_login_name.length == 0) {
        $("#" + table).html("<div class='w1000 cen m c_b' id='service'><ul class='footer1'></ul></div><div id='footer' class='cen w1200'><div class='links'><a href='javascript:void(0)'>首页</a>| <a href='javascript:void(0)' target='_blank'>关于我们</a>|<a href='javascript:void(0)' target='_blank'>联系我们</a>| <a href='javascript:void(0)' target='_blank'>人才招聘</a>| <a href='javascript:void(0)' target='_blank'>待定</a>|<a href='javascript:void(0)' target='_blank'>广告服务</a></div><div class='copyright'>Copyright 2015-2115 <a href='javascript:void(0)' target='_blank' title='待定'>待定</a>Inc.,All rights reserved.</div></div>");
    }
}