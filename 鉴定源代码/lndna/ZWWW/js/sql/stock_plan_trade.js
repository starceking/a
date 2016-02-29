function stock_plan_trade_Update(btn, id, trade_time, price, amount, trade_no) {
    ws_exec(btn, "PUT", "StockPlanTrade", "Update", {
        "id": id, "trade_time": trade_time, "price": price, "amount": amount, "trade_no": trade_no,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "操作成功", "后退");
}
function stock_plan_trade_GetOne(id) {
    ws_get_json("", "StockPlanTrade", "GetOne", {
        "id": id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", stock_plan_trade_GetOne_sucFunc);
}
function stock_plan_trade_GetOne_sucFunc(table, page, json) {
    if (json != null) {
        fill_input_nde("成交时间i", json.trade_time);
        fill_input_nde("成交价格i", json.price);
        fill_input_nde("成交数量i", json.amount);
        fill_input_nde("成交编号i", json.trade_no);
    }
}
function stock_plan_trade_GetList(btn, table, page, plan_id) {
    ws_qry(btn, table, page, "StockPlanTrade", "GetList", {
        "plan_id": plan_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function stock_plan_trade_Delete(btn, id) {
    if (!confirm("删除？")) return;
    ws_exec(btn, "DELETE", "StockPlanTrade", "Delete", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "操作成功", "刷新");
}