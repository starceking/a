function user_bank_Audit(btn, id, process_status_id) {
    ws_exec(btn, "PUT", "UserBank", "Audit", {
        "id": id, "process_status_id": process_status_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "");
}
function user_bank_GetList(btn, table, page, user_id) {
    ws_qry(btn, table, page, "UserBank", "GetList", {
        "user_id": user_id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function user_bank_GetListAudit(btn, table, page, user_id, process_status_id, page_size, page_index) {
    ws_qry(btn, table, page, "UserBank", "GetListAudit", {
        "user_id": user_id, "process_status_id": process_status_id, "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function user_bank_GetCountAudit(user_id, process_status_id) {
    ws_exec("", "GET", "UserBank", "GetCountAudit", {
        "user_id": user_id, "process_status_id": process_status_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}