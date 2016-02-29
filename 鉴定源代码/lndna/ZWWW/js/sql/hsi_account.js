function hsi_account_Insert(btn, src_id, calc_id, number) {
    ws_exec(btn, "POST", "HsiAccount", "Insert", {
        "src_id": src_id, "calc_id": calc_id, "number": number,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function hsi_account_Update(btn, id, src_id, calc_id, number) {
    ws_exec(btn, "PUT", "HsiAccount", "Update", {
        "id": id, "src_id": src_id, "calc_id": calc_id, "number": number,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function hsi_account_Delete(btn, id) {
    if (!confirm("删除？")) return;
    ws_exec(btn, "DELETE", "HsiAccount", "Delete", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function hsi_account_GetList(btn, table, page, src_id, number, page_size, page_index) {
    ws_qry(btn, table, page, "HsiAccount", "GetList", {
        "src_id": src_id, "number": number,
        "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function hsi_account_GetCount(src_id, number) {
    ws_exec("", "GET", "HsiAccount", "GetCount", {
        "src_id": src_id, "number": number,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}
function hsi_account_GetOne(id) {
    ws_get_json("", "HsiAccount", "GetOne", {
        "id": id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", hsi_account_GetOne_sucFunc);
}
function hsi_account_GetOne_sucFunc(table, page, json) {
    if (json != null) {
        fill_input_nde("主账号i", json.number);
        dict_GetAll("交易软件s", "请选择", json.src_id, dict_hsi_src_json);
        dict_GetAll("结算周期s", "请选择", json.calc_id, dict_hsi_calc_json);
    }
}
function hsi_account_GetJson() {
    ws_get_json("", "HsiAccount", "GetJson", {
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", hsi_account_GetJson_sucFunc);
}
function hsi_account_GetJson_sucFunc(table, page, json) {
    if (json != null) {
        dict_hsi_account_json = json;
    }
}