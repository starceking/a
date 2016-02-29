function hsi_plan_PreSettle(btn, id, oper_amount) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "PUT", "HsiPlan", "PreSettle", {
        "id": id, "oper_amount": oper_amount,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "刷新");
}
function hsi_plan_Settle(btn, id) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "PUT", "HsiPlan", "Settle", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "刷新");
}
function hsi_plan_UpdateOperAmount(btn, id, oper_amount) {
    ws_exec(btn, "PUT", "HsiPlan", "UpdateOperAmount", {
        "id": id, "oper_amount": oper_amount,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "");
}
function hsi_plan_GetList(btn, table, page, hsi_account_id, plan_status_id, create_dates, create_datee, page_size, page_index) {
    ws_qry(btn, table, page, "HsiPlan", "GetList", {
        "hsi_account_id": hsi_account_id, "plan_status_id": plan_status_id, "create_dates": create_dates, "create_datee": create_datee,
        "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function hsi_plan_GetCount(hsi_account_id, plan_status_id, create_dates, create_datee) {
    ws_exec("", "GET", "HsiPlan", "GetCount", {
        "hsi_account_id": hsi_account_id, "plan_status_id": plan_status_id, "create_dates": create_dates, "create_datee": create_datee,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}
function hsi_plan_GetOne(id, hsi_account_id, create_date) {
    ws_get_json("", "HsiPlan", "GetOne", {
        "id": id, "hsi_account_id": hsi_account_id, "create_date": create_date,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", hsi_plan_GetOne_sucFunc);
}
function hsi_plan_GetOne_sucFunc(table, page, json) {
    if (json != null) {
        hpId = json.id;
        accId = json.hsi_account_id;
        accD = get_date_str(json.create_date);

        fill_label_nde("方案ID", json.id);
        fill_label_nde("母账号", json.hsi_account_number);
        fill_label_nde("盈亏", json.profit);
        fill_label_nde("状态", dict_GetValue(dict_hsi_plan_status_json, json.plan_status_id));
        fill_label_nde("日期", get_date_str(json.create_date));
        fill_label_nde("手数", json.stock_amount);
        fill_label_nde("点买额度", json.money_debt / 10000);
        fill_label_nde("保证金", json.money_margin);
        fill_label_nde("服务费", json.money_fee);
        fill_input_nde("操盘次数i", json.oper_amount);
        fill_label_nde("清算时间", json.end_time);

        if (json.plan_status_id == 5) $("#psBtn").show();
        else if (json.plan_status_id == 6) $("#seBtn").show();
        if (json.plan_status_id == 7) $("#uoaBtn").show();

        hsi_img_GetList("", "照片", "hsi_img_list", hpId);
        search();
    }
}