//页面填写控制
function ctrl_not_null(ctrl) {
    if ($("#" + ctrl) == null) return false;
    if ($("#" + ctrl).val() == null) return false;
    if (jQuery.trim($("#" + ctrl).val()).length == 0) { set_bk_color(ctrl, "yellow"); return false; }
    else set_bk_color(ctrl, "white");
    return true;
}
function ctrl_must_length(ctrl, len) {
    if ($("#" + ctrl) == null) return false;
    if (jQuery.trim($("#" + ctrl).val()).length != len) { set_bk_color(ctrl, "yellow"); return false; }
    else set_bk_color(ctrl, "white");
    return true;
}
function set_bk_color(ctrl, color) {
    if ($("#" + ctrl) == null) return;
    document.getElementById(ctrl).style.backgroundColor = color;
}
//设置颜色
function set_color(ctrl, color) {
    if ($("#" + ctrl) == null) return;
    document.getElementById(ctrl).style.color = color;
}
//设置enabled
function set_enabled(ctrl, enabled) {
    if ($("#" + ctrl) == null) return;
    if (enabled) $("#" + ctrl).removeAttr("disabled");
    else $("#" + ctrl).attr("disabled", "disabled");
}
//判断ctrl是否显示/隐藏
function ctr_hidden(ctr) {
    if ($("#" + ctr) == null) return false;
    return $("#" + ctr).is(":hidden");
}
function set_ctrl_show(ctr, show) {
    if ($("#" + ctr) == null) return;
    if (show) $("#" + ctr).show();
    else $("#" + ctr).hide();
}
//select
function init_select(table, all, rdfsval, rdfstxt) {
    if ($("#" + table) == null) return;

    $("#" + table).empty();
    if (all.length > 0) {
        var option = $("<option>").text(all).val("");
        $("#" + table).append(option);
    }
    for (var i = 0; i < rdfsval.length; i++) {
        var option = $("<option>").text(rdfstxt[i]).val(rdfsval[i]);
        $("#" + table).append(option);
    }
}
function fill_select(ctrl, val) {
    if ($("#" + ctrl) != null) $("#" + ctrl).val(strdecode(val));
}
function fill_select_nde(ctrl, val) {
    if ($("#" + ctrl) != null) $("#" + ctrl).val(val);
}
//label
function fill_label(ctrl, val) {
    if ($("#" + ctrl) != null) $("#" + ctrl).html(strdecode(val));
}
function fill_label_d(ctrl, val) {
    if ($("#" + ctrl) != null) $("#" + ctrl).html(get_date_str(strdecode(val)));
}
function fill_label_nde(ctrl, val) {
    if ($("#" + ctrl) != null) $("#" + ctrl).html(val);
}
//input
function fill_input(ctrl, val) {
    if ($("#" + ctrl) != null) $("#" + ctrl).val(strdecode(val));
}
function fill_input_d(ctrl, val) {
    if ($("#" + ctrl) != null) $("#" + ctrl).val(get_date_str(strdecode(val)));
}
function fill_input_nde(ctrl, val) {
    if ($("#" + ctrl) != null) $("#" + ctrl).val(val);
}
function fill_input_d_nde(ctrl, val) {
    if ($("#" + ctrl) != null) $("#" + ctrl).val(get_date_str(val));
}
//a
function fill_a_href(ctrl, val) {
    if ($("#" + ctrl) != null) $("#" + ctrl).attr("href", strdecode(val));
}
function fill_a_text(ctrl, val) {
    if ($("#" + ctrl) != null) $("#" + ctrl).text(strdecode(val));
}
function fill_a_href_nde(ctrl, val) {
    if ($("#" + ctrl) != null) $("#" + ctrl).attr("href", val);
}
function fill_a_text_nde(ctrl, val) {
    if ($("#" + ctrl) != null) $("#" + ctrl).text(val);
}
function fill_a_attr(ctrl, attr, val) {
    if ($("#" + ctrl) != null) $("#" + ctrl).attr(attr, val);
}
function remove_a_attr(ctrl, attr) {
    if ($("#" + ctrl) != null) $("#" + ctrl).removeAttr(attr);
}
//radio
function get_checked_radio(name) {
    var val = $("input:radio[name='" + name + "']:checked").val();
    return val;
}
//checkbox
function fill_checkbox(ctrl, val) {
    if (val == "checked") $("#" + ctrl).attr("checked", val);
    else $("#" + ctrl).removeAttr("checked");
}
//checkbox in table
function change_zcb(table, ctrl) {
    if ($("#" + ctrl).attr("checked")) fill_table_cb(table, "checked");
    else fill_table_cb(table, "");
}
function fill_table_cb(table, checked) {
    if ($("#" + table) == null) return;
    for (var i = 1; i < document.getElementById(table).rows.length; i++) {
        fill_checkbox(table + "_cb" + i.toString(), checked);
    }
}
function get_table_cbs(table, idx) {
    if ($("#" + table) == null) return;

    var IDs = "";
    for (var i = 1; i < document.getElementById(table).rows.length; i++) {
        if ($("#" + table + "_cb" + i.toString()).attr("checked") == "checked") {
            IDs += (document.getElementById(table).rows[i].cells[idx].innerHTML + ",");
        }
    }
    if (IDs.length > 0) {
        IDs = IDs.substr(0, IDs.length - 1);
    }
    return IDs;
}
function open_dialog(ctrl, okfunc) {
    var dialogOpts = {
        buttons: {
            "确定": okfunc,
            "取消": function () { $(this).dialog("close"); }
        },
        modal: true,
        width: "600px"
    }
    $("#" + ctrl).dialog(dialogOpts);
}