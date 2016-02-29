function hsi_plan_sub_InsMoney(btn, id) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "PUT", "HsiPlanSub", "InsMoney", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "search()");
}
function hsi_plan_sub_Abandon(btn, id) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "PUT", "HsiPlanSub", "Abandon", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "search()");
}
function hsi_plan_sub_InsMoneyOk(btn, id) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "PUT", "HsiPlanSub", "InsMoneyOk", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "search()");
}
function hsi_plan_sub_Sell(btn, id, profit) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "PUT", "HsiPlanSub", "Sell", {
        "id": id, "profit": profit,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "search()");
}
function hsi_plan_sub_UpdateProfit(btn, id, profit) {
    ws_exec(btn, "PUT", "HsiPlanSub", "UpdateProfit", {
        "id": id, "profit": profit,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "search()");
}
function hsi_plan_sub_GetList(btn, table, page, user_id, hsi_account_id, hsi_account_sub_number, plan_status_id,
                create_dates, create_datee, page_size, page_index) {
    ws_qry(btn, table, page, "HsiPlanSub", "GetList", {
        "user_id": user_id, "hsi_account_id": hsi_account_id, "hsi_account_sub_number": hsi_account_sub_number, "plan_status_id": plan_status_id,
        "create_dates": create_dates, "create_datee": create_datee, "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function hsi_plan_sub_GetCount(user_id, hsi_account_id, hsi_account_sub_number, plan_status_id,
                create_dates, create_datee) {
    ws_exec("", "GET", "HsiPlanSub", "GetCount", {
        "user_id": user_id, "hsi_account_id": hsi_account_id, "hsi_account_sub_number": hsi_account_sub_number, "plan_status_id": plan_status_id,
        "create_dates": create_dates, "create_datee": create_datee,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}
function hsi_plan_sub_GetTask() {
    ws_get_json("", "HsiPlanSub", "GetTask", {
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", hsi_plan_sub_GetTask_sucFunc);
}
function hsi_plan_sub_GetTask_sucFunc(table, page, json) {
    if (json != null) {
        var arr = json.split(',');
        if (arr.length == 2) {
            fill_a_text_nde("待入金A", "待入金(" + arr[0] + ")");
            fill_a_text_nde("待平仓A", "待平仓(" + arr[1] + ")");
        }
    }
}