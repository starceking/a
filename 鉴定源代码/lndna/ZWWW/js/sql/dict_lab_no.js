function dict_lab_no_GetList(table, userDept, val) {
    var json = eval("(" + dict_lab_no_json + ")");
    var DICT_id = new Array();
    var DICT_name = new Array();
    var idx = 0;
    $(json).each(function () {
        var id = this.id;
        var name = this.name;
        if (userDept.length == 6) {
            if (userDept.indexOf("0000") < 0) {
                if (userDept.indexOf("00") < 0) {
                    if (this.id != userDept) {
                        id = "";
                        name = "";
                    }
                }
                else if (this.id.substring(0, 4) != userDept.substring(0, 4)) {
                    id = "";
                    name = "";
                }
            }
        }
        if (id.length > 0) {
            DICT_id[idx] = id;
            DICT_name[idx] = name;
            idx++;
        }
    });
    if (DICT_id.length == 0) return;
    init_select(table, "选择", DICT_id, DICT_name);
    fill_select_nde(table, val);
}