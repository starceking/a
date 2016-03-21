//执行
var apiUrl = "/lndna/";
function ws_exec(btn, type, ctrler, func, data, sucMsg, destUrl, callback) {
    //for (var x in data) alert(x + "->" + data[x]);
    set_enabled(btn, false);
    $.ajax({
        type: type,
        url: apiUrl + ctrler + "/" + func,
        dataType: "json",
        data: data,
        success: function (json) {
            set_enabled(btn, true);

            if (callback)
                callback(json);
            else if (func.match("GetCount") != null) {
                row_count = json;
                set_pager_label();
            }
            else if (json.length == 0) {
                if (destUrl == "关闭") {
                    window.opener = null;
                    window.open('', '_self');
                    window.close();
                }
                else if (destUrl == "后退") {
                    location.href = document.referrer;
                }
                else if (destUrl == "刷新") {
                    window.history.go(0);
                }
                else if (destUrl == "search()") {
                    search();
                }
                else if (destUrl.length > 0) window.location = destUrl;
                else if (sucMsg.length > 0) alert(sucMsg);
            }
            else alert(json);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            set_enabled(btn, true);
        }
    });
}
//查询
function ws_qry(qry, table, page, ctrler, func, data) {
    switch (page.split('_', 1)[0]) {
        case "导出数据":
            fill_a_href_nde(table, "javascript:void(0)");
            fill_a_text_nde(table, "读取中，请耐心等待...");
            break;
        default:
            $("#" + table).html("<caption class='caption1'> </caption><tr><th class='th1'>读取中...</th></tr>");
            break;
    }
    ws_get_json(qry, ctrler, func, data, table, page, ws_qry_sucFunc);
}
function ws_get_json(qry, ctrler, func, data, table, page, sucFunc) {
    //for (var x in data) alert(x + "->" + data[x]);
    set_enabled(qry, false);
    $.ajax({
        type: "GET",
        url: apiUrl + ctrler + "/" + func,
        dataType: "json",
        data: data,
        success: function (json) {
            set_enabled(qry, true);
            sucFunc(table, page, json);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            set_enabled(qry, true);
        }
    });
}
function ws_qry_sucFunc(table, page, json) {
    //表头
    var html = "<caption class='caption1'> </caption>";
    switch (page.split('_', 1)[0]) {
        case "导出数据":
            fill_a_href_nde(table, json);
            fill_a_text_nde(table, "请点右键->链接另存为");
            return;
        default:
            html = ws_qry_sucFunc_pre(page);
            break;
    }
    //表的内容
    if (json != null) {
        if (json.length == 0) {
            row_count = 0;
            html = "<tr><th class='th1'>无数据</th><tr>";
        }
        else {
            $.each(json, function () {
                html += ws_qry_sucFunc_mid(table, page, this);
                page_row_idx++;
            });
            html = ws_qry_sucFunc_last(page, html);
        }
        $("#" + table).html(html);
        //数据绑定完毕后
        switch (page) {
            default:
                break;
        }
    }
    readFinish = true;
}
function ws_qry_sucFunc_pre(page) {
    var html = "";
    switch (page) {
        case "user_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>所属单位</th><th class='th1'>警号</th><th class='th1'>姓名</th><th class='th1'>权限</th></tr>";
            break;
        case "case_info_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>物证</th><th class='th1'>状态</th></tr>";
            break;
        case "case_evidence_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>添加样本</th></tr>";
            break;
        case "case_sample_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>添加样本</th></tr>";
            break;
        default:
            break;
    }
    return html;
}
function ws_qry_sucFunc_mid(table, page, json) {
    var html = "";
    switch (page) {
        case "user_list":
            html = "<tr><td class='td2'><a href='../user/" + (json.cg_flag == 1 ? "edit_cg" : "edit") + ".htm?id=" + json.id + "' style='color:blue;font-weight:bolder'>" + json.id + "</a></td><td class='td1'>" + json.dept_no + "</td><td class='td1'>" + json.police_no + "</td><td class='td1'>" + json.name + "</td><td class='td1'>" + json.auth_ids + "</td></tr>";
            break;
        case "case_info_list":
            html = "<tr><td class='td2'><a href='../case_info/edit.htm?id=" + json.id + "' style='color:blue;font-weight:bolder'>" + json.id + "</a></td><td class='td1'><a href='../case_info/evidence_list.htm?case=" + json.id + "' style='color:blue;font-weight:bolder'>物证</a></td><td class='td1'>" + dict_GetValue(dict_case_status_json, json.case_status_id) + "</td></tr>";
            break;
        case "case_evidence_list":
            html = "<tr><td class='td2'><a href='../case_info/evidence_edit.htm?id=" + json.id + "' style='color:blue;font-weight:bolder'>" + json.id + "</a></td><td class='td1'><a href='../case_info/sample_edit.htm?case=" + get_url_para(window.location.href, "case") + "&table=case_evidence&id=" + json.id + "' style='color:blue;font-weight:bolder'>添加样本</a></td></tr>";
            break;
        case "case_sample_list":
            html = "<tr><td class='td2'><a href='../case_info/evidence_edit.htm?id=" + json.id + "' style='color:blue;font-weight:bolder'>" + json.id + "</a></td><td class='td1'><a href='../case_info/sample_edit.htm?case=" + get_url_para(window.location.href, "case") + "&table=case_evidence&id=" + json.id + "' style='color:blue;font-weight:bolder'>添加样本</a></td></tr>";
            break;
        default:
            break;
    }
    return html;
}
function ws_qry_sucFunc_last(page, html) {
    switch (page) {
        default:
            break;
    }
    return html;
}