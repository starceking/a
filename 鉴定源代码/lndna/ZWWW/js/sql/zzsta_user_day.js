﻿function zzsta_user_day_GetList(btn, table, page, sta_days, sta_daye, cmp_id) {
    ws_qry(btn, table, page, "ZZStaUserDay", "GetList", {
        "sta_days": sta_days, "sta_daye": sta_daye, "cmp_id": cmp_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}