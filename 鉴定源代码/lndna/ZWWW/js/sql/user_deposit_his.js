function user_deposit_his_GetList(btn, table, page, number, deposit_src_id, user_id, create_times,
                 create_timee, page_size, page_index) {
    ws_qry(btn, table, page, "UserDepositHis", "GetList", {
        "number": number, "deposit_src_id": deposit_src_id,
        "user_id": user_id,
        "create_times": create_times, "create_timee": create_timee,
        "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function user_deposit_his_GetCount(number, deposit_src_id, user_id, create_times,
                 create_timee) {
    ws_exec("", "GET", "UserDepositHis", "GetCount", {
        "number": number, "deposit_src_id": deposit_src_id,
        "user_id": user_id,
        "create_times": create_times, "create_timee": create_timee,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}
function user_deposit_his_Exp(btn, number, deposit_src_id, user_id, create_times,
                 create_timee) {
    ws_qry(btn, "导出结果", "导出数据", "UserDepositHis", "Exp", {
        "number": number, "deposit_src_id": deposit_src_id,
        "user_id": user_id,
        "create_times": create_times, "create_timee": create_timee,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}