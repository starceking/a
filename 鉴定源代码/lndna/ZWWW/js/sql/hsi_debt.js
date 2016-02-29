function hsi_debt_Insert(btn, amount, money_debt, fee) {
    ws_exec(btn, "POST", "HsiDebt", "Insert", {
        "amount": amount, "money_debt": money_debt, "fee": fee,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function hsi_debt_Update(btn, id, amount, money_debt, fee) {
    ws_exec(btn, "PUT", "HsiDebt", "Update", {
        "id": id, "amount": amount, "money_debt": money_debt, "fee": fee,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function hsi_debt_Delete(btn, id) {
    if (!confirm("删除？")) return;
    ws_exec(btn, "DELETE", "HsiDebt", "Delete", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "刷新");
}
function hsi_debt_GetOne(id) {
    ws_get_json("", "HsiDebt", "GetOne", {
        "id": id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", hsi_debt_GetOne_sucFunc);
}
function hsi_debt_GetOne_sucFunc(table, page, json) {
    if (json != null) {
        fill_input_nde("手数i", json.amount);
        fill_input_nde("配资额i", json.money_debt);
        fill_input_nde("服务费i", json.fee);
    }
}
function hsi_debt_GetList(btn, table, page) {
    ws_qry(btn, table, page, "HsiDebt", "GetList", {
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}