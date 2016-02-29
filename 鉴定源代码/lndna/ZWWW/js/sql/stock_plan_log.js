function stock_plan_log_GetList(btn, table, page, plan_id, page_size, page_index) {
    ws_qry(btn, table, page, "StockPlanLog", "GetList", {
        "plan_id": plan_id,
        "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function stock_plan_log_GetCount(plan_id) {
    ws_exec("", "GET", "StockPlanLog", "GetCount", {
        "plan_id": plan_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}