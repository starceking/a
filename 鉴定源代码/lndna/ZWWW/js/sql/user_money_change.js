function user_deposit_his_Deposit(btn, user_id, money, info, number, deposit_src_id) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "POST", "UserDepositHis", "Deposit", {
        "user_id": user_id, "money": money, "info": info, "number": number, "deposit_src_id": deposit_src_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "操作成功", "关闭");
}
function user_money_change_Update(btn, user_id, money, info, ref_table, ref_id) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "PUT", "UserMoneyChange", "Update", {
        "user_id": user_id, "money": money, "info": info, "ref_table": ref_table, "ref_id": ref_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "操作成功", "关闭");
}
function user_money_change_GetList(btn, table, page, user_id, money_flow_id, ref_table, ref_id,
    create_times, create_timee, page_size, page_index) {
    ws_qry(btn, table, page, "UserMoneyChange", "GetList", {
        "user_id": user_id, "money_flow_id": money_flow_id,
        "ref_table": ref_table, "ref_id": ref_id,
        "create_times": create_times, "create_timee": create_timee,
        "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function user_money_change_GetCount(user_id, money_flow_id, ref_table, ref_id,
    create_times, create_timee) {
    ws_exec("", "GET", "UserMoneyChange", "GetCount", {
        "user_id": user_id, "money_flow_id": money_flow_id,
        "ref_table": ref_table, "ref_id": ref_id,
        "create_times": create_times, "create_timee": create_timee,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}
function user_money_change_Exp(btn, user_id, money_flow_id, ref_table, ref_id,
    create_times, create_timee) {
    ws_qry(btn, "导出结果", "导出数据", "UserMoneyChange", "Exp", {
        "user_id": user_id, "money_flow_id": money_flow_id,
        "ref_table": ref_table, "ref_id": ref_id,
        "create_times": create_times, "create_timee": create_timee,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}