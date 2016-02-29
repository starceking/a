var row_count = -1;
var page_size = 15;
var page_index = 1;
var page_row_idx = 1;
function set_pager_label() {
    if (page_index == 0) page_index = 1;
    var page_count = 0;
    if (row_count % page_size == 0) page_count = row_count / page_size;
    else page_count = (row_count - (row_count % page_size)) / page_size + 1;

    var ps = "";
    for (var i = page_index - 50; i < page_index; i++) {
        if (i <= 0 || i > page_count) continue;
        ps += "<option value='" + i + "'>" + i + "</option>";
    }
    ps += "<option value='" + i + "' selected='true' >" + i + "</option>";
    for (var i = parseInt(page_index) + 1; i < (parseInt(page_index) + 50) ; i++) {
        if (i > page_count) break;
        ps += "<option value='" + i + "'>" + i + "</option>";
    }

    var div = "<a href='javascript:void(0)' onclick='$(\"#pagerSelecter\").val(1);pagerSc()'>首页</a>";
    div += "<a href='javascript:void(0)' onclick='$(\"#pagerSelecter\").val(parseInt($(\"#pagerSelecter\").val())-1);pagerSc()'>上一页</a>";

    for (var i = page_index - 5; i < page_index; i++) {
        if (i <= 0 || i > page_count) continue;
        div += ("<a href='javascript:void(0)' onclick='$(\"#pagerSelecter\").val(" + i + ");pagerSc()'>" + i + "</a>");
    }
    div += ("<a href='javascript:void(0)' class='pagok' disabled='disabled'>" + page_index + "</a>");
    for (var i = parseInt(page_index) + 1; i < (parseInt(page_index) + 5) ; i++) {
        if (i > page_count) break;
        div += ("<a href='javascript:void(0)' onclick='$(\"#pagerSelecter\").val(" + i + ");pagerSc()'>" + i + "</a>");
    }

    div += "<a href='javascript:void(0)' onclick='$(\"#pagerSelecter\").val(parseInt($(\"#pagerSelecter\").val())+1);pagerSc()'>下一页</a>";
    div += "<a href='javascript:void(0)' onclick='$(\"#pagerSelecter\").val(pagerSelecter.length);pagerSc()'>末页</a>";
    div += ("<div class='f_l allsex'>共" + row_count + "条，到<select id='pagerSelecter' onchange='pagerSc();'>" + ps + "</select></div>");
    $("#pagerDiv").html(div);
}
function pagerQry() {
    page_row_idx = 1;
    row_count = -1;
    page_index = 1;
    search();
}
function pagerSc() {
    page_index = $("#pagerSelecter").val();
    page_row_idx = ((parseInt(page_index)-1) * parseInt(page_size) + 1);
    set_pager_label();
    search();
}