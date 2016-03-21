function dict_region_GetList(table, parent_id, val) {
    dict_region_SetIdName(table);

    var json = eval("(" + dict_region_json + ")");
    if (parent_id.length == 0) parent_id = "000000";
    var DICT_id = new Array();
    var DICT_name = new Array();
    var idx = 0;
    $(json).each(function () {
        if (this.parent_id == parent_id) {
            DICT_id[idx] = this.id;
            DICT_name[idx] = this.name;
            idx++;
        }
    });
    if (DICT_id.length == 0) return;
    init_select(table, "选择", DICT_id, DICT_name);
    fill_select_nde(table, val);
}
function dict_region_SetIdName(table) {
    if ($("#" + table).val() == null || $("#" + table).val().length == 0) {
        if ($("#" + table + "id").attr("id") != null) $("#" + table + "id").html("");
        if ($("#" + table + "name").attr("id") != null) $("#" + table + "name").html("");
    }
    else {
        if ($("#" + table + "id").attr("id") != null) $("#" + table + "id").html($("#" + table).val());
        if ($("#" + table + "name").attr("id") != null) $("#" + table + "name").html($("#" + table + " option:selected").text());
    }
}