function hsi_account_sub_Insert(btn, account_id, number, pwd) {
    ws_exec(btn, "POST", "HsiAccountSub", "Insert", {
        "account_id": account_id, "number": number, "pwd": pwd,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function hsi_account_sub_Update(btn, id, number, pwd) {
    ws_exec(btn, "PUT", "HsiAccountSub", "Update", {
        "id": id, "number": number, "pwd": pwd,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function hsi_account_sub_Delete(btn, id) {
    if (!confirm("删除？")) return;
    ws_exec(btn, "DELETE", "HsiAccountSub", "Delete", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "刷新");
}
function hsi_account_sub_GetOne(id) {
    ws_get_json("", "HsiAccountSub", "GetOne", {
        "id": id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", hsi_account_sub_GetOne_sucFunc);
}
function hsi_account_sub_GetOne_sucFunc(table, page, json) {
    if (json != null) {
        fill_input_nde("操作账号i", json.number);
        fill_input_nde("密码i", json.pwd);
    }
}
function hsi_account_sub_GetList(btn, table, page, account_id, number, user_mobile, page_size, page_index) {
    ws_qry(btn, table, page, "HsiAccountSub", "GetList", {
        "account_id": account_id, "number": number, "user_mobile": user_mobile,
        "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function hsi_account_sub_GetCount(account_id, number, user_mobile) {
    ws_exec("", "GET", "HsiAccountSub", "GetCount", {
        "account_id": account_id, "number": number, "user_mobile": user_mobile,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}