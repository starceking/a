function zstock_settings_GetList() {
    ws_get_json("", "ZStockSettings", "GetList", {}, "", "", zstock_settings_GetList_sucFunc);
}
function zstock_settings_GetList_sucFunc(table, page, json) {
    if (json != null) {
        $.each(json, function () {
            fill_input_nde("风控" + this.id + "i", this.value);
        });
    }
}
function zstock_settings_Update(btn, id, value) {
    ws_exec(btn, "PUT", "ZStockSettings", "Update", {
        "id": id, "value": value,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "");
}
function zstock_settings_GetRongduan() {
    ws_get_json("", "ZStockSettings", "GetRongduan", {}, "", "", zstock_settings_GetRongduan_sucFunc);
}
function zstock_settings_GetRongduan_sucFunc(table, page, json) {
    if (json != null) {
        fill_input_nde("熔断i", json);
    }
}
function zstock_settings_SetRongduan(btn, id) {
    ws_exec(btn, "PUT", "ZStockSettings", "SetRongduan", {
        "id": id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "");
}
function zstock_settings_GetIdxData() {
    ws_get_json("", "ZStockSettings", "GetIdxData", {}, "", "", zstock_settings_GetIdxData_sucFunc);
}
function zstock_settings_GetIdxData_sucFunc(table, page, json) {
    if (json != null) {
        fill_input_nde("首页数据i", json);
    }
}
function zstock_settings_SetIdxData(btn, value) {
    ws_exec(btn, "PUT", "ZStockSettings", "SetIdxData", {
        "value": value, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "");
}