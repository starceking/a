var stock_plan_id = 0;
var stock_plan_sid = 0;
function stock_plan_SetSysUser(btn, id, plan_status_id, remark) {
    if (plan_status_id != 2 && !confirm("确定？")) return;
    stock_plan_id = id;
    stock_plan_sid = plan_status_id;
    ws_exec(btn, "PUT", "StockPlan", "SetSysUser", {
        "id": id, "plan_status_id": plan_status_id, "remark": remark,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "操作成功", "刷新", stock_plan_SetSysUser_Suc);
}
function stock_plan_SetSysUser_Suc(json) {
    if (json.length == 0) {
        if (stock_plan_sid == 2) window.location.href = "../stock/ro.htm?id=" + stock_plan_id;
        else window.history.go(0);
    } else alert(json);
}
function stock_plan_CancelPreSell(btn, id, remark) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "PUT", "StockPlan", "CancelPreSell", {
        "id": id, "remark": remark,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "操作成功", "刷新");
}
function stock_plan_Buy(btn, plan_id, trade_time, price, amount, trade_no) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "POST", "StockPlan", "Buy", {
        "plan_id": plan_id, "trade_time": trade_time, "price": price, "amount": amount, "trade_no": trade_no,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "操作成功", "刷新");
}
function stock_plan_Sell(btn, plan_id, trade_time, price, amount, trade_no) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "POST", "StockPlan", "Sell", {
        "plan_id": plan_id, "trade_time": trade_time, "price": price, "amount": amount, "trade_no": trade_no,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "操作成功", "刷新");
}
function stock_plan_Calc(btn, id) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "PUT", "StockPlan", "Calc", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "操作成功", "刷新");
}
function stock_plan_GetOne(id) {
    ws_get_json("", "StockPlan", "GetOne", {
        "id": id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", stock_plan_GetOne_sucFunc);
}
function stock_plan_GetOne_sucFunc(table, page, json) {
    if (json != null) {
        if (getCookie("sys_is_investor", 1) <= 0) fill_a_href_nde("客户A", "../user/ro.htm?id=" + json.user_id);
        if (json.user_name != null) fill_a_text_nde("客户A", json.user_name);
        else if (json.user_nick_name != null) fill_a_text_nde("客户A", json.user_nick_name);
        else if (json.user_mobile != null) fill_a_text_nde("客户A", json.user_mobile);
        fill_label_nde("投资人", json.sys_user_name);
        fill_label_nde("状态", dict_GetValue(dict_plan_status_json, json.plan_status_id));
        fill_label_nde("点买额度", json.money_debt / 10000);
        fill_label_nde("保证金", json.money_margin);
        fill_label_nde("止盈保证金", json.stop_earn_percent * json.money_debt / 100);
        fill_label_nde("发起费用", json.money_fee);
        fill_label_nde("递延费用", json.defer_fee);
        fill_label_nde("持仓时间", json.defer_times + 2);
        fill_label_nde("发起时间", json.start_time.replace("T", " "));
        fill_label_nde("股票代码", json.stock_no);
        fill_label_nde("股票名称", json.stock_name);
        fill_label_nde("点买价格", json.start_price);
        fill_label_nde("点买数量", json.start_amount);
        fill_label_nde("欠费", json.money_npay);

        if (json.plan_status_id == -1) {
            fill_label_nde("返还点买人保证金", json.money_margin);
            fill_label_nde("返还投资人保证金", json.money_debt * json.stop_earn_percent / 100);
        }
        if (json.plan_status_id > 2) {
            fill_label_nde("买入时间", json.buy_time.replace("T", " "));
            fill_label_nde("买入价格", json.buy_price);
            fill_label_nde("成交数量", json.stock_amount);
        }
        if (json.plan_status_id > 3) {
            fill_label_nde("点卖价格", json.end_price);
            fill_label_nde("点卖数量", json.stock_amount);
            fill_label_nde("剩余持仓", json.stock_amount - json.stock_amount_already);
        }
        if (json.plan_status_id >= 4) fill_label_nde("备注", json.remark);
        if (json.plan_status_id > 4) {
            fill_label_nde("卖出时间", json.sell_time.replace("T", " "));
            fill_label_nde("卖出价格", json.sell_price);
            fill_label_nde("卖出数量", json.stock_amount_already);
            fill_label_nde("总盈亏", json.profit);
            fill_label_nde("客户盈亏", json.user_profit);
            fill_label_nde("投资人盈亏", (json.profit - json.user_profit).toFixed(2));
            if (json.profit > 0) {
                fill_label_nde("返还点买人保证金", json.money_margin);
                fill_label_nde("返还投资人保证金", json.money_debt * json.stop_earn_percent / 100);
            }
            else {
                var fhkh = json.money_margin + json.profit;
                if (fhkh > 0) {
                    fill_label_nde("返还点买人保证金", fhkh);
                    fill_label_nde("返还投资人保证金", json.money_debt * json.stop_earn_percent / 100 - json.profit);
                }
                else {
                    fill_label_nde("返还点买人保证金", 0);
                    fill_label_nde("返还投资人保证金", json.money_debt * json.stop_earn_percent / 100 + json.money_margin);
                }
            }
        }
        if (json.plan_status_id > 3) {
            $("#sellTr").show();
            $("#sellTr1").show();
            $("#sellTr2").show();
            $("#sellTr3").show();
        }
        if (json.plan_status_id == -1 || json.plan_status_id >= 6) {
            $("#lastTr").show();
            $("#lastTr1").show();
            $("#lastTr2").show();
        }
        if (json.plan_status_id == 2) {
            document.getElementById("mainTb").style.backgroundColor = "pink";
            document.getElementById("bsTbl").style.backgroundColor = "pink";
        }
        if (json.plan_status_id == 4 || json.plan_status_id == 5) {
            document.getElementById("mainTb").style.backgroundColor = "lime";
            document.getElementById("bsTbl").style.backgroundColor = "lime";
        }
        stock_amount = json.stock_amount;
        stock_amount_already = json.stock_amount_already;

        //按钮状态
        if (json.plan_status_id == 1) { $("#getBtn").show(); $("#refuseBtn").show(); }
        else if (json.plan_status_id == 2) {
            $("#buyBtn").show();
            if (json.stock_amount_already == 0) $("#refuseBtn").show();
        }
        else if (json.plan_status_id == 4 || json.plan_status_id == 5) {
            $("#sellBtn").show();
            if (json.plan_status_id == 4 && getCookie("sys_is_investor", 1) <= 0) $("#cpcBtn").show();
        }
        else if (json.plan_status_id == 6 && getCookie("sys_is_investor", 1) <= 0) $("#shBtn").show();
        else if (json.plan_status_id == 7 && getCookie("sys_is_investor", 1) <= 0) $("#calcBtn").show();
        else if (json.plan_status_id == 3 && getCookie("sys_is_investor", 1) <= 0) $("#pcBtn").show();
    }
}
function stock_plan_GetList(btn, table, page, id, user_id, plan_status_id, profit, start_dates,
                    start_datee, end_dates, end_datee, stock_no, stock_name, money_debts, money_debte, order, page_size, page_index) {
    ws_qry(btn, table, page, "StockPlan", "GetList", {
        "id": id, "user_id": user_id, "plan_status_id": plan_status_id, "profit": profit, "start_dates": start_dates, "start_datee": start_datee,
        "end_dates": end_dates, "end_datee": end_datee, "stock_no": stock_no, "stock_name": stock_name, "money_debts": money_debts,
        "money_debte": money_debte, "order": order, "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
    readFinish = false;
}
function stock_plan_GetCount(id, user_id, plan_status_id, profit, start_dates,
                    start_datee, end_dates, end_datee, stock_no, stock_name, money_debts, money_debte) {
    ws_exec("", "GET", "StockPlan", "GetCount", {
        "id": id, "user_id": user_id, "plan_status_id": plan_status_id, "profit": profit, "start_dates": start_dates, "start_datee": start_datee,
        "end_dates": end_dates, "end_datee": end_datee, "stock_no": stock_no, "stock_name": stock_name, "money_debts": money_debts,
        "money_debte": money_debte,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}
function stock_plan_Exp(btn, id, user_id, plan_status_id, profit, start_dates,
                    start_datee, end_dates, end_datee, stock_no, stock_name, money_debts, money_debte, order) {
    ws_qry(btn, "导出结果", "导出数据", "StockPlan", "Exp", {
        "id": id, "user_id": user_id, "plan_status_id": plan_status_id, "profit": profit, "start_dates": start_dates, "start_datee": start_datee,
        "end_dates": end_dates, "end_datee": end_datee, "stock_no": stock_no, "stock_name": stock_name, "money_debts": money_debts,
        "money_debte": money_debte, "order": order,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function stock_plan_ReCalc(btn, id) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "PUT", "StockPlan", "ReCalc", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "操作成功", "刷新");
}
function stock_plan_GetTask() {
    ws_get_json("", "StockPlan", "GetTask", {
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", stock_plan_GetTask_sucFunc);
}
function stock_plan_GetTask_sucFunc(table, page, json) {
    if (json != null) {
        var arr = json.split(',');
        if (arr.length == 3) {
            var str = "待匹配(" + arr[0] + ")";
            if ($("#待匹配A").text() != "待匹配(..)" && $("#待匹配A").text() != str) {
                if (arr[0] > 0)
                    $("#待匹配A").css("background-color", "red");
                pagerQry();
            }
            else {
                $("#待匹配A").css("background-color", "white");
            }
            fill_a_text_nde("待匹配A", str);

            str = "待平仓(" + arr[1] + ")";
            if ($("#待平仓A").text() != "待平仓(..)" && $("#待平仓A").text() != str) {
                if (arr[1] > 0)
                    $("#待平仓A").css("background-color", "red");
            }
            else {
                $("#待平仓A").css("background-color", "white");
            }
            fill_a_text_nde("待平仓A", str);

            str = "平仓中(" + arr[2] + ")";
            if ($("#平仓中A").text() != "平仓中(..)" && $("#平仓中A").text() != str) {
                if (arr[2] > 0)
                    $("#平仓中A").css("background-color", "red");
            }
            else {
                $("#平仓中A").css("background-color", "white");
            }
            fill_a_text_nde("平仓中A", str);
        }
    }
}
function stock_plan_ExpToday(btn, start_dates, start_datee) {
    ws_qry(btn, "导出结果2", "导出数据", "StockPlan", "ExpToday", {
        "start_dates": start_dates, "start_datee": start_datee,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function stock_plan_UpdatePoUser(btn, id, po_user) {
    ws_exec(btn, "PUT", "StockPlan", "UpdatePoUser", {
        "id": id, "po_user": po_user,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}