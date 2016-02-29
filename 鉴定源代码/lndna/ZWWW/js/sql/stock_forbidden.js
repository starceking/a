function stock_forbidden_Insert(btn, number, name, reason_id) {
    ws_exec(btn, "POST", "StockForbidden", "Insert", {
        "number": number, "name": name, "reason_id": reason_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "操作成功", "后退");
}
function stock_forbidden_Delete(btn, id, number) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "DELETE", "StockForbidden", "Delete", {
        "id": id, "number": number,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "操作成功", "刷新");
}
function stock_forbidden_GetList(btn, table, page) {
    ws_qry(btn, table, page, "StockForbidden", "GetList", {});
}