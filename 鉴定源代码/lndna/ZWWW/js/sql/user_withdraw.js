function user_withdraw_Finish(btn, id, process_status_id) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "PUT", "UserWithdraw", "Finish", {
        "id": id, "process_status_id": process_status_id, "remark": "",
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "操作成功", "search()");
}
function user_withdraw_GetList(btn, table, page, user_id, sys_user_id, process_status_id, create_times,
                 create_timee, finish_times, finish_timee, page_size, page_index) {
    ws_qry(btn, table, page, "UserWithdraw", "GetList", {
        "user_id": user_id, "sys_user_id": sys_user_id,
        "process_status_id": process_status_id, "create_times": create_times,
        "create_timee": create_timee, "finish_times": finish_times, "finish_timee": finish_timee,
        "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function user_withdraw_GetCount(user_id, sys_user_id, process_status_id, create_times,
                 create_timee, finish_times, finish_timee) {
    ws_exec("", "GET", "UserWithdraw", "GetCount", {
        "user_id": user_id, "sys_user_id": sys_user_id,
        "process_status_id": process_status_id, "create_times": create_times,
        "create_timee": create_timee, "finish_times": finish_times, "finish_timee": finish_timee,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}
function user_withdraw_Exp(btn, user_id, sys_user_id, process_status_id, create_times,
                 create_timee, finish_times, finish_timee) {
    ws_qry(btn, "导出结果", "导出数据", "UserWithdraw", "Exp", {
        "user_id": user_id, "sys_user_id": sys_user_id,
        "process_status_id": process_status_id, "create_times": create_times,
        "create_timee": create_timee, "finish_times": finish_times, "finish_timee": finish_timee,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}