function stock_plan_xrd_Insert(btn, stock_no, stock_name, amount) {
    if (!confirm("确定除权除息？")) return;
    ws_exec(btn, "POST", "StockPlanXrd", "Insert", {
        "stock_no": stock_no, "stock_name": stock_name, "amount": amount,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "search()");
}
function stock_plan_xrd_Cancel(btn, id) {
    if (!confirm("确定撤销？")) return;
    ws_exec(btn, "PUT", "StockPlanXrd", "Cancel", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "search()");
}
function stock_plan_xrd_GetList(btn, table, page, create_times, create_timee, page_size, page_index) {
    ws_qry(btn, table, page, "StockPlanXrd", "GetList", {
        "create_times": create_times, "create_timee": create_timee,
        "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function stock_plan_xrd_GetCount(create_times, create_timee) {
    ws_exec("", "GET", "StockPlanXrd", "GetCount", {
        "create_times": create_times, "create_timee": create_timee,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}