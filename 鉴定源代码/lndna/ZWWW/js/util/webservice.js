//执行
var apiUrl = "/wcp/";
function ws_exec(btn, type, ctrler, func, data, sucMsg, destUrl, callback) {
    //for (var x in data) alert(x + "->" + data[x]);
    set_enabled(btn, false);
    $.ajax({
        type: type,
        url: apiUrl + ctrler + "/" + func,
        dataType: "json",
        data: data,
        success: function (json) {
            set_enabled(btn, true);

            if (callback)
                callback(json);
            else if (func == "GetCount") {
                row_count = json;
                set_pager_label();
            }
            else if (func.match("GetCount") != null) {
                row_count = json;
                set_pager_label();
            }
            else if (json.length == 0) {
                if (destUrl == "关闭") {
                    window.opener = null;
                    window.open('', '_self');
                    window.close();
                }
                else if (destUrl == "后退") {
                    location.href = document.referrer;
                }
                else if (destUrl == "刷新") {
                    window.history.go(0);
                }
                else if (destUrl == "search()") {
                    search();
                    if ($("#upDiv").attr("id"))
                        $("#upDiv").dialog("close");
                }
                else if (destUrl.length > 0) window.location = destUrl;
                else if (sucMsg.length > 0) alert(sucMsg);
            }
            else alert(json);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            set_enabled(btn, true);
        }
    });
}
//查询
function ws_qry(qry, table, page, ctrler, func, data) {
    switch (page.split('_', 1)[0]) {
        case "导出数据":
            fill_a_href_nde(table, "javascript:void(0)");
            fill_a_text_nde(table, "读取中，请耐心等待...");
            break;
        default:
            $("#" + table).html("<caption class='caption1'> </caption><tr><th class='th1'>读取中...</th></tr>");
            break;
    }
    ws_get_json(qry, ctrler, func, data, table, page, ws_qry_sucFunc);
}
function ws_get_json(qry, ctrler, func, data, table, page, sucFunc) {
    //for (var x in data) alert(x + "->" + data[x]);
    set_enabled(qry, false);
    $.ajax({
        type: "GET",
        url: apiUrl + ctrler + "/" + func,
        dataType: "json",
        data: data,
        success: function (json) {
            set_enabled(qry, true);
            sucFunc(table, page, json);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            set_enabled(qry, true);
        }
    });
}
function ws_qry_sucFunc(table, page, json) {
    //表头
    var html = "<caption class='caption1'> </caption>";
    switch (page.split('_', 1)[0]) {
        case "导出数据":
            fill_a_href_nde(table, json);
            fill_a_text_nde(table, "请点右键->链接另存为");
            return;
        default:
            html = ws_qry_sucFunc_pre(page);
            break;
    }
    //表的内容
    if (json != null) {
        if (json.length == 0) {
            row_count = 0;
            html = "<tr><th class='th1'>无数据</th><tr>";
        }
        else {
            $.each(json, function () {
                html += ws_qry_sucFunc_mid(table, page, this);
                page_row_idx++;
            });
            html = ws_qry_sucFunc_last(page, html);
        }
        $("#" + table).html(html);
        switch (page) {
            case "stock_plan_list2":
                new TableSorter("查询列表", 8);
                break;
            default:
                break;
        }
    }
    readFinish = true;
}
function ws_qry_sucFunc_pre(page) {
    var html = "";
    switch (page) {
        case "user_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>电话</th><th class='th1'>昵称</th><th class='th1'>姓名</th><th class='th1'>身份证</th><th class='th1'>余额</th><th class='th1'>点买额</th><th class='th1'>邀请人</th><th class='th1'>资金</th><th class='th1'>登密</th><th class='th1'>方案</th><th class='th1'>投资人</th><th class='th1'>来源</th></tr>";
            break;
        case "user_bank_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>银行</th><th class='th1'>所在地区</th><th class='th1'>支行名称</th><th class='th1'>卡号</th><th class='th1'>状态</th></tr>";
            break;
        case "user_bank_audit_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>客户ID</th><th class='th1'>电话</th><th class='th1'>姓名</th><th class='th1'>银行</th><th class='th1'>支行名称</th><th class='th1'>卡号</th><th class='th1'>状态</th></tr>";
            break;
        case "user_money_change_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>客户ID</th><th class='th1'>电话</th><th class='th1'>姓名</th><th class='th1'>流向</th><th class='th1'>变动资金</th><th class='th1'>剩余资金</th><th class='th1'>时间</th><th class='th1'>备注</th></tr>";
            break;
        case "user_deposit_his_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>客户ID</th><th class='th1'>电话</th><th class='th1'>姓名</th><th class='th1'>账单号</th><th class='th1'>充值来源</th><th class='th1'>充值额</th><th class='th1'>时间</th><th class='th1'>备注</th></tr>";
            break;
        case "sys_user_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>用户名</th><th class='th1'>电话</th><th class='th1'>姓名</th><th class='th1'>投资余额</th><th class='th1'>接单数</th><th class='th1'>平均买入(秒)</th><th class='th1'>平均卖出(秒)</th><th class='th1'>超过1分钟(买/卖)</th><th class='th1'>资金</th><th class='th1'>状态</th></tr>";
            break;
        case "sys_user_sys_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>用户名</th><th class='th1'>电话</th><th class='th1'>姓名</th><th class='th1'>权限</th><th class='th1'>状态</th><th class='th1'>所属</th></tr>";
            break;
        case "user_withdraw_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>提现额</th><th class='th1'>申请时间</th><th class='th1'>户名</th><th class='th1'>银行</th><th class='th1'>卡号</th><th class='th1'>状态</th><th class='th1'>操作</th></tr>";
            break;
        case "stock_forbidden_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>股票代码</th><th class='th1'>股票名称</th><th class='th1'>风险原因</th><th class='th1'>删除</th></tr>";
            break;
        case "stock_plan_trade_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>交易方向</th><th class='th1'>成交时间</th><th class='th1'>成交价格</th><th class='th1'>成交数量</th><th class='th1'>成交编号</th><th class='th1'>修改</th><th class='th1'>删除</th></tr>";
            break;
        case "stock_plan_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>客户ID</th><th class='th1'>电话</th><th class='th1'>姓名</th><th class='th1'>股票代码</th><th class='th1'>股票名称</th><th class='th1'>点买额度</th><th class='th1'>股票数量</th><th class='th1'>持仓截止</th><th class='th1'>保证金</th><th class='th1'>状态</th><th class='th1'>投资人</th><th class='th1'>总盈亏</th></tr>";
            break;
        case "stock_plan_list1":
            html = "<tr><th class='th2'>ID</th><th class='th1'>客户ID</th><th class='th1'>姓名</th><th class='th1'>股票代码</th><th class='th1'>股票名称</th><th class='th1'>点买额度</th><th class='th1'>股票数量</th><th class='th1'>发起时间</th><th class='th1'>止盈</th><th class='th1'>止损</th><th class='th1'>操作</th></tr>";
            break;
        case "stock_plan_list2":
            html = "<tr><th class='th2'>ID</th><th class='th1'>客户ID</th><th class='th1'>姓名</th><th class='th1'>股票代码</th><th class='th1'>股票名称</th><th class='th1'>点买额度</th><th class='th1'>持仓数量</th><th class='th1'>买入价格</th><th class='th1'>当前盈亏</th><th class='th1'>止损</th><th class='th1'>买入时间</th><th class='th1'>卖出价格</th><th class='th1'>卖出时间</th><th class='th1'>状态</th><th class='th1'>备注</th><th class='th1'>操作</th><th class='th1'>挂单</th></tr>";
            break;
        case "stock_plan_log_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>类型</th><th class='th1'>详情</th><th class='th1'>记录时间</th></tr>";
            break;
        case "sys_user_stock_money_change_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>流向</th><th class='th1'>变动资金</th><th class='th1'>剩余资金</th><th class='th1'>时间</th><th class='th1'>备注</th><th class='th1'>代码</th><th class='th1'>名称</th></tr>";
            break;
        case "hsi_account_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>交易软件</th><th class='th1'>结算周期</th><th class='th1'>操作主账号</th><th class='th1'>占用子帐号</th><th class='th1'>空闲子帐号</th></tr>";
            break;
        case "hsi_account_sub_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>操作账号</th><th class='th1'>密码</th><th class='th1'>关联用户</th><th class='th1'>手机号码</th><th class='th1'>解绑</th></tr>";
            break;
        case "hsi_debt_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>手数</th><th class='th1'>配资额（美元）</th><th class='th1'>服务费</th><th class='th1'>删除</th></tr>";
            break;
        case "hsi_plan_sub_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>客户</th><th class='th1'>电话</th><th class='th1'>母帐号</th><th class='th1'>日期</th><th class='th1'>子帐号</th><th class='th1'>密码</th><th class='th1'>手数</th><th class='th1'>保证金</th><th class='th1'>盈亏</th><th class='th1'>状态</th><th class='th1'>操作</th></tr>";
            break;
        case "hsi_plan_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>母帐号</th><th class='th1'>日期</th><th class='th1'>手数</th><th class='th1'>保证金</th><th class='th1'>盈亏</th><th class='th1'>状态</th><th class='th1'>查看</th></tr>";
            break;
        case "hsi_img_list":
            html = "<tr><th class='th2'></th><th class='th1'>删除</th></tr>";
            break;
        case "zzsta_user_day_list":
            html = "<tr><th class='th2'>日期</th><th class='th1'>访问ip</th><th class='th1'>访问次数</th><th class='th1'>注册用户</th><th class='th1'>转化率</th><th class='th1'>充值用户</th><th class='th1'>转化率</th><th class='th1'>A股点买用户</th><th class='th1'>点买金额</th><th class='th1'>平均点买金额</th><th class='th1'>点买次数</th><th class='th1'>平均点买次数</th><th class='th1'>恒指点买用户</th><th class='th1'>点买手数</th><th class='th1'>平均点买手数</th><th class='th1'>点买次数</th><th class='th1'>平均点买次数</th><th class='th1'>来源</th></tr>";
            break;
        case "stock_plan_day_list":
            html = "<tr><th class='th2'>日期</th><th class='th1'>点买人数</th><th class='th1'>点买次数</th><th class='th1'>当前持仓</th><th class='th1'>履约保证金</th><th class='th1'>点买盈利</th><th class='th1'>点买亏损</th><th class='th1'>综合费</th><th class='th1'>递延费</th><th class='th1'>佣金</th><th class='th1'>穿仓金额</th><th class='th1'>停牌损失</th><th class='th1'>来源</th></tr>";
            break;
        case "hsi_day_sta_list":
            html = "<tr><th class='th2'>日期</th><th class='th1'>用户人数</th><th class='th1'>保证金</th><th class='th1'>投资总额</th><th class='th1'>申请手数</th><th class='th1'>用户盈亏</th><th class='th1'>佣金收入</th><th class='th1'>穿仓指出</th><th class='th1'>操盘次数</th><th class='th1'>来源</th></tr>";
            break;
        case "notice_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>标题</th><th class='th1'>创建时间</th><th class='th1'>修改</th><th class='th1'>预览</th><th class='th1'>删除</th></tr>";
            break;
        case "stock_plan_xrd_list":
            html = "<tr><th class='th2'>ID</th><th class='th1'>股票代码</th><th class='th1'>名称</th><th class='th1'>配股数</th><th class='th1'>状态</th><th class='th1'>操作时间</th><th class='th1'>操作</th></tr>";
            break;
        default:
            break;
    }
    return html;
}
function ws_qry_sucFunc_mid(table, page, json) {
    var html = "";
    switch (page) {
        case "user_list":
            var ass = json.sys_user_name;
            if (ass == null) ass = "<a href='../sys_user/edit.htm?uid=" + json.id + "' style='color:blue;font-weight:bolder'>添加</a>";
            html = "<tr><td class='td2'><input id='tcb" + page_row_idx + "' type='checkbox' uid='" + json.id + "'></input><a href='javascript:void(window.open(\"../user/ro.htm?id=" + json.id + "\"))' style='color:blue;font-weight:bolder'>" + json.id + "</a></td><td class='td1'>" + (json.mobile == null ? "" : json.mobile) + "</td><td class='td1'>" + (json.nick_name == null ? "" : json.nick_name) + "</td><td class='td1'>" + (json.name == null ? "" : json.name) + "</td><td class='td1'>" + (json.id_card_no == null ? "" : json.id_card_no) + "</td><td class='td1'>" + json.money + "</td><td class='td1'>" + json.stock_plan_debt + "</td><td class='td1'>" + (json.ref_info ? "<a href='javascript:void(window.open(\"../user/ro.htm?id=" + json.ref_id + "\"))' style='color:blue;font-weight:bolder'>" + json.ref_info + "</a>" : "") + "</td><td class='td1'><a href='javascript:void(window.open(\"money_change.htm?id=" + json.id + "\"))' style='color:blue;font-weight:bolder'>变动</a></td><td class='td1'><a href='javascript:void(0)' onclick='user_ResetPwd(\"\", " + json.id + ",123456)' style='color:blue;font-weight:bolder'>重置</a></td><td class='td1'><a href='javascript:void(window.open(\"../stock/list.htm?userId=" + json.id + "\"))' style='color:blue;font-weight:bolder'>查看</a></td><td class='td1'>" + ass + "</td><td class='td1'>" + dict_GetValue(dict_cmp_json, json.cmp_id) + "</td></tr>";
            break;
        case "user_bank_list":
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'>" + dict_GetValue(dict_bank_json, json.bank_id) + "</td><td class='td1'>" + dict_GetValue(dict_district_json, json.district_id) + "</td><td class='td1'>" + json.branch_name + "</td><td class='td1'>" + json.card_no + "</td><td class='td1'>" + dict_GetValue(dict_process_status_json, json.process_status_id) + "</td></tr>";
            break;
        case "user_bank_audit_list":
            var ass = "<option value='2' selected='selected'>未审核</option><option value='3'>审核通过</option><option value='-2'>审核失败</option>";
            if (json.process_status_id == 3)
                ass = "<option value='2'>未审核</option><option value='3' selected='selected'>审核通过</option><option value='-2'>审核失败</option>";
            else if (json.process_status_id == -2)
                ass = "<option value='2'>未审核</option><option value='3'>审核通过</option><option value='-2' selected='selected'>审核失败</option>";
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'>" + json.user_id + "</td><td class='td1'>" + json.user_mobile + "</td><td class='td1'>" + (json.user_name == null ? json.user_nick_name : json.user_name) + "</td><td class='td1'>" + dict_GetValue(dict_bank_json, json.bank_id) + "</td><td class='td1'>" + json.branch_name + "</td><td class='td1'>" + json.card_no + "</td><td class='td1'><select id='状态" + json.id + "s' onchange='user_bank_Audit(\"\", " + json.id + ", $(\"#状态" + json.id + "s\").val())'>" + ass + "</select></td></tr>";
            break;
        case "user_money_change_list":
            var ass = "<a href='javascript:void(window.open(\"../user/ro.htm?id=" + json.user_id + "\"))' style='color:blue;font-weight:bolder'>" + json.user_id + "</a>";
            if (getCookie("sys_is_investor", 1) > 0) ass = json.user_id;
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'>" + ass + "</td><td class='td1'>" + (json.user_mobile == null ? "" : json.user_mobile) + "</td><td class='td1'>" + (json.user_name != null ? json.user_name : (json.user_nick_name == null ? "" : json.user_nick_name)) + "</td><td class='td1'>" + dict_GetValue(dict_money_flow_json, json.money_flow_id) + "</td><td class='td1'>" + json.money + "</td><td class='td1'>" + json.final_money + "</td><td class='td1'>" + json.create_time + "</td><td class='td1'>" + json.info + "</td></tr>";
            break;
        case "user_deposit_his_list":
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'><a href='javascript:void(window.open(\"../user/ro.htm?id=" + json.user_id + "\"))' style='color:blue;font-weight:bolder'>" + json.user_id + "</a></td><td class='td1'>" + (json.user_mobile == null ? "" : json.user_mobile) + "</td><td class='td1'>" + (json.user_name != null ? json.user_name : (json.user_nick_name == null ? "" : json.user_nick_name)) + "</td><td class='td1'>" + json.number + "</td><td class='td1'>" + dict_GetValue(dict_deposit_src_json, json.deposit_src_id) + "</td><td class='td1'>" + json.money + "</td><td class='td1'>" + json.create_time + "</td><td class='td1'>" + json.info + "</td></tr>";
            break;
        case "sys_user_list":
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'><a href='edit.htm?id=" + json.id + "' style='color:blue;font-weight:bolder'>" + json.login_name + "</a></td><td class='td1'>" + json.mobile + "</td><td class='td1'>" + json.name + "</td><td class='td1'>" + json.stock_money + "</td><td class='td1'>" + json.stock_plan_amount + "</td><td class='td1'>" + json.buy_pj + "</td><td class='td1'>" + json.sell_pj + "</td><td class='td1'>" + (json.buy_onem + "/" + json.sell_onem) + "</td><td class='td1'><a href='javascript:void(window.open(\"money_change.htm?id=" + json.id + "\"))' style='color:blue;font-weight:bolder'>变动</a></td><td class='td1'>" + getDeleteFlag(json) + "</td></tr>";
            break;
        case "sys_user_sys_list":
            var arr = "<a href='edit_sys.htm?id=" + json.id + "' style='color:blue;font-weight:bolder'>" + json.login_name + "</a>";
            if (json.id == 1) arr = json.login_name;
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'>" + arr + "</td><td class='td1'>" + json.mobile + "</td><td class='td1'>" + json.name + "</td><td class='td1'>" + json.privilege_ids + "</td><td class='td1'>" + getDeleteFlag(json) + "</td><td class='td1'>" + dict_GetValue(dict_cmp_json, json.cmp_id) + "</td></tr>";
            break;
        case "user_withdraw_list":
            var ass = "";
            if (json.process_status_id == "3" || json.process_status_id == "-1" || json.process_status_id == "-2") ass = json.finish_time;
            else if (json.process_status_id == "1") ass = "<a href='javascript:void(0)' onclick='user_withdraw_Finish(\"\", " + json.id + ",2)' style='color:blue;font-weight:bolder'>处理中</a>&nbsp;&nbsp;<a href='javascript:void(0)' onclick='user_withdraw_Finish(\"\", " + json.id + ",-1)' style='color:blue;font-weight:bolder'>拒绝</a>";
            else if (json.process_status_id == "2") ass = "<a href='javascript:void(0)' onclick='user_withdraw_Finish(\"\", " + json.id + ",3)' style='color:blue;font-weight:bolder'>成功</a>&nbsp;&nbsp;<a href='javascript:void(0)' onclick='user_withdraw_Finish(\"\", " + json.id + ",-2)' style='color:blue;font-weight:bolder'>失败</a>";
            var ass2 = dict_GetValue(dict_process_status_json, json.process_status_id);
            if (json.process_status_id == 1 && json.remark != null) ass2 = json.remark;
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'>" + json.money + "</td><td class='td1'>" + json.create_time + "</td><td class='td1'><a href='javascript:void(window.open(\"../user/ro.htm?id=" + json.user_id + "\"))' style='color:blue;font-weight:bolder'>" + json.user_name + "</a></td><td class='td1'>" + json.bank_name + "&nbsp;&nbsp;" + json.branch_name + "</td><td class='td1'>" + json.card_no + "</td><td class='td1'>" + ass2 + "</td><td class='td1'>" + ass + "</td></tr>";
            break;
        case "stock_forbidden_list":
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'>" + json.number + "</td><td class='td1'>" + json.name + "</td><td class='td1'>" + dict_GetValue(dict_forbidden_json, json.reason_id) + "</td><td class='td1'><a href='javascript:void(0)' onclick='stock_forbidden_Delete(\"\", " + json.id + ",\"" + json.number + "\")' style='color:blue;font-weight:bolder'>删除</a></td></tr>";
            break;
        case "stock_plan_trade_list":
            html = "<tr><td class='td2'><a href='../stock/trade.htm?id=" + json.id + "' style='color:blue;font-weight:bolder'>" + json.id + "</a></td><td class='td1'>" + (json.buy_or_sell == 1 ? "买入" : "卖出") + "</td><td class='td1'>" + json.trade_time + "</td><td class='td1'>" + json.price + "</td><td class='td1'>" + json.amount + "</td><td class='td1'>" + (json.trade_no == null ? "" : json.trade_no) + "</td><td class='td1'><a href='../stock/trade.htm?id=" + json.id + "' style='color:blue;font-weight:bolder'>修改</a></td><td class='td1'><a href='javascript:void(0)' onclick='stock_plan_trade_Delete(\"\", " + json.id + ")' style='color:blue;font-weight:bolder'>删除</a></td></tr>";
            break;
        case "stock_plan_list":
            var ass = "<a href='javascript:void(window.open(\"../user/ro.htm?id=" + json.user_id + "\"))' style='color:blue;font-weight:bolder'>" + json.user_id + "</a>";
            if (getCookie("sys_is_investor", 1) > 0) ass = json.user_id;
            html = "<tr><td class='td2'><a href='javascript:void(window.open(\"../stock/ro.htm?id=" + json.id + "\"))' style='color:blue;font-weight:bolder'>" + json.id + "</a></td><td class='td1'>" + ass + "</td><td class='td1'>" + (json.user_mobile == null ? "" : json.user_mobile) + "</td><td class='td1'>" + (json.user_name != null ? json.user_name : (json.user_nick_name == null ? "" : json.user_nick_name)) + "</td><td class='td1'>" + json.stock_no + "</td><td class='td1'>" + json.stock_name + "</td><td class='td1'>" + json.money_debt / 10000 + "万</td><td class='td1'>" + json.stock_amount + "</td><td class='td1'>" + get_date_str(json.end_date) + "</td><td class='td1'>" + json.money_margin + "</td><td class='td1'>" + dict_GetValue(dict_plan_status_json, json.plan_status_id) + "</td><td class='td1'>" + (json.sys_user_id > 0 ? json.sys_user_name : "") + "</td><td class='td1'>" + json.profit + "</td></tr>";
            break;
        case "stock_plan_list1":
            html = "<tr><td class='td2'><a href='javascript:void(window.open(\"../stock/ro.htm?id=" + json.id + "\"))' style='color:blue;font-weight:bolder'>" + json.id + "</a></td><td class='td1'>" + json.user_id + "</td><td class='td1'>" + (json.user_name != null ? json.user_name : (json.user_nick_name == null ? "" : json.user_nick_name)) + "</td><td class='td1'>" + json.stock_no + "</td><td class='td1'>" + json.stock_name + "</td><td class='td1'>" + parseInt(json.money_debt / 10000) + "万</td><td class='td1'>" + json.stock_amount + "</td><td class='td1'>" + json.buy_time + "</td><td class='td1'>" + json.stop_earn_percent * json.money_debt / 100 + "</td><td class='td1'>" + json.stop_loss_money + "</td><td class='td1'><a href='javascript:stock_plan_SetSysUser(\"\", " + json.id + ", 2, \"\")' style='color:blue;font-weight:bolder'>接单</a></td></tr>";
            break;
        case "stock_plan_list2":
            var ass = "-";
            if (json.plan_status_id == 4 || json.plan_status_id == 5) ass = "<a href='ro.htm?id=" + json.id + "' style='color:blue;font-weight:bolder'>平仓</a>";
                //else if (json.plan_status_id == 3) ass = "<a href='javascript:stock_plan_SetSysUser(\"\", " + json.id + ", 4, \"\")' style='color:blue;font-weight:bolder'>强制平仓</a>";
            else if (json.plan_status_id == 2) ass = "<a href='ro.htm?id=" + json.id + "' style='color:blue;font-weight:bolder'>买入</a>";

            var pos = new Array()
            pos[0] = "庞春春"
            pos[1] = "秦杰"
            pos[2] = "孙菡蔚"
            var po = "<option value='-'>未挂单</option>";
            for (var i = 0; i < pos.length; i++) {
                var ponm = pos[i];
                if (ponm == json.po_user) po += ("<option value='" + ponm + "' selected='selected'>" + ponm + "</option>");
                else po += ("<option value='" + ponm + "'>" + ponm + "</option>");
            }
            po = "<select id='pos" + json.id + "' onchange='stock_plan_UpdatePoUser(\"\", " + json.id + ", $(\"#pos" + json.id + "\").val())'>" + po + "</select>";

            html = "<tr><td class='td2'><a href='javascript:void(window.open(\"../stock/ro.htm?id=" + json.id + "\"))' style='color:blue;font-weight:bolder'>" + json.id + "</a></td><td class='td1'>" + json.user_id + "</td><td class='td1'>" + (json.user_name != null ? json.user_name : (json.user_nick_name == null ? "" : json.user_nick_name)) + "</td><td class='td1'>" + json.stock_no + "</td><td class='td1'>" + json.stock_name + "</td><td class='td1'>" + parseInt(json.money_debt / 10000) + "万</td><td class='td1'>" + json.stock_amount + "</td><td class='td1'>" + json.buy_price + "</td><td class='td1' style='font-size:larger'>-</td><td class='td1'>" + json.stop_loss_money + "</td><td class='td1'>" + json.buy_time + "</td><td class='td1'>" + json.sell_price + "</td><td class='td1'>" + ((json.plan_status_id == 6 || json.plan_status_id == 7) ? json.sell_time : "&nbsp;") + "</td><td class='td1'>" + dict_GetValue(dict_plan_status_json, json.plan_status_id) + "</td><td class='td1'>" + (json.remark == null ? "&nbsp;" : json.remark) + "</td><td class='td1'>" + ass + "</td><td class='td1'>" + po + "</td></tr>";
            break;
        case "stock_plan_log_list":
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'>" + json.log_type + "</td><td class='td1'>" + json.info + "</td><td class='td1'>" + json.create_time + "</td></tr>";
            break;
        case "sys_user_stock_money_change_list":
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'>" + dict_GetValue(dict_money_flow_json, json.money_flow_id) + "</td><td class='td1'>" + json.money + "</td><td class='td1'>" + json.final_money + "</td><td class='td1'>" + json.create_time + "</td><td class='td1'>" + json.info + "</td><td class='td1'>" + (json.stock_no == null ? "" : json.stock_no) + "</td><td class='td1'>" + (json.stock_name == null ? "" : json.stock_name) + "</td></tr>";
            break;
        case "hsi_account_list":
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'>" + dict_GetValue(dict_hsi_src_json, json.src_id) + "</td><td class='td1'>" + dict_GetValue(dict_hsi_calc_json, json.calc_id) + "</td><td class='td1'><a href='account_edit.htm?id=" + json.id + "' style='color:blue;font-weight:bolder'>" + json.number + "</a></td><td class='td1'>" + json.sub_used + "</td><td class='td1'>" + json.sub_free + "</td></tr>";
            break;
        case "hsi_account_sub_list":
            var ass = "-";
            if (json.user_id <= 0 || getdiffdays(json.last_plan_date, get_today_str()) >= 7)
                ass = "<a href='javascript:void(0)' onclick='hsi_account_sub_Delete(\"\", " + json.id + ")' style='color:blue;font-weight:bolder'>解绑/删除</a>";
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'><a href='account_sub_edit.htm?id=" + json.id + "' style='color:blue;font-weight:bolder'>" + json.number + "</a></td><td class='td1'>" + json.pwd + "</td><td class='td1'>" + (json.user_name != null ? json.user_name : (json.user_nick_name == null ? "" : json.user_nick_name)) + "</td><td class='td1'><a href='javascript:void(window.open(\"../user/ro.htm?id=" + json.user_id + "\"))' style='color:blue;font-weight:bolder'>" + (json.user_mobile != null ? json.user_mobile : "") + "</a></td><td class='td1'>" + ass + "</td></tr>";
            break;
        case "hsi_debt_list":
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'><a href='debt_edit.htm?id=" + json.id + "' style='color:blue;font-weight:bolder'>" + json.amount + "手</a></td><td class='td1'>" + json.money_debt + "</td><td class='td1'>" + json.fee + "</td><td class='td1'><a href='javascript:void(0)' onclick='hsi_debt_Delete(\"\", " + json.id + ")' style='color:blue;font-weight:bolder'>删除</a></td></tr>";
            break;
        case "hsi_plan_sub_list":
            var ass = "-";
            if (json.plan_status_id == 1) ass = "<a href='javascript:void(0)' onclick='hsi_plan_sub_InsMoney(\"\", " + json.id + ")' style='color:blue;font-weight:bolder'>开始入金</a>&nbsp;&nbsp;<a href='javascript:void(0)' onclick='hsi_plan_sub_Abandon(\"\", " + json.id + ")' style='color:blue;font-weight:bolder'>流单</a>";
            else if (json.plan_status_id == 2) ass = "<a href='javascript:void(0)' onclick='hsi_plan_sub_InsMoneyOk(\"\", " + json.id + ")' style='color:blue;font-weight:bolder'>入金完毕</a>&nbsp;&nbsp;<a href='javascript:void(0)' onclick='hsi_plan_sub_Abandon(\"\", " + json.id + ")' style='color:blue;font-weight:bolder'>流单</a>";
            else if (json.plan_status_id == 3 || json.plan_status_id == 4) ass = "<a href='javascript:void(0)' onclick='svType=1;hpsId=" + json.id + ";$(\"#盈亏i\").val(" + json.profit_d + ");open_upDiv_dialog();' style='color:blue;font-weight:bolder'>录入盈亏</a>";
            else if (json.plan_status_id > 4 && json.plan_status_id < 7) ass = "<a href='javascript:void(0)' onclick='svType=2;hpsId=" + json.id + ";$(\"#盈亏i\").val(" + json.profit_d + ");open_upDiv_dialog();' style='color:blue;font-weight:bolder'>修改盈亏</a>";
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'>" + (json.user_name != null ? json.user_name : (json.user_nick_name == null ? "" : json.user_nick_name)) + "</td><td class='td1'><a href='javascript:void(window.open(\"../user/ro.htm?id=" + json.user_id + "\"))' style='color:blue;font-weight:bolder'>" + (json.user_mobile != null ? json.user_mobile : "") + "</a></td><td class='td1'><a href='../hsi/hsi_plan_ro.htm?id=0&haid=" + json.hsi_account_id + "&hacd=" + json.create_date + "' style='color:blue;font-weight:bolder'>" + json.hsi_account_number + "</a></td><td class='td1'>" + get_date_str(json.create_date) + "</td><td class='td1'>" + json.hsi_account_sub_number + "</td><td class='td1'>" + json.hsi_account_sub_pwd + "</td><td class='td1'>" + json.stock_amount + "</td><td class='td1'>" + json.money_margin + "</td><td class='td1'>" + json.profit + "</td><td class='td1'>" + dict_GetValue(dict_hsi_plan_status_json, json.plan_status_id) + "</td><td class='td1'>" + ass + "</td></tr>";
            break;
        case "hsi_plan_list":
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'><a href='../hsi/hsi_plan_ro.htm?id=" + json.id + "&haid=0' style='color:blue;font-weight:bolder'>" + json.hsi_account_number + "</a></td><td class='td1'>" + get_date_str(json.create_date) + "</td><td class='td1'>" + json.stock_amount + "</td><td class='td1'>" + json.money_margin + "</td><td class='td1'>" + json.profit + "</td><td class='td1'>" + dict_GetValue(dict_hsi_plan_status_json, json.plan_status_id) + "</td><td class='td1'><a href='../hsi/hsi_plan_ro.htm?id=" + json.id + "&haid=0' style='color:blue;font-weight:bolder'>查看</a></td></tr>";
            break;
        case "hsi_img_list":
            if (json.url == "ins" || json.disk == null)
                html = "<tr><td class='td2'></td><td class='td1'><a href=\"javascript:void(0)\" onclick=\"imagesupload.click()\"  style='color:blue;font-weight:bolder'>添加</a></td></tr>";
            else
                html = "<tr><td class='td2'><img src='" + json.url + "' style='width:90%;height:400px' onclick='window.open(\"" + json.url + "\")'></img></td><td class='td1'><a href='javascript:void(0)' onclick='hsi_img_Delete(\"\", \"" + json.disk + "\")' style='color:blue;font-weight:bolder'>删除</a></td></tr>";
            break;
        case "zzsta_user_day_list":
            html = "<tr><td class='td2'>" + get_date_str(json.sta_day) + "</td><td class='td1'>" + json.ip_amount + "</td><td class='td1'>" + json.cs_amount + "</td><td class='td1'>" + json.reg_user + "</td><td class='td1'>" + json.reg_buy + "&nbsp;%</td><td class='td1'>" + json.dep_user + "</td><td class='td1'>" + json.dep_buy + "&nbsp;%</td><td class='td1'>" + json.buy_user + "</td><td class='td1'>" + parseInt(json.buy_money / 10000) + "&nbsp;万</td><td class='td1'>" + parseInt(json.buy_my_pj / 10000) + "&nbsp;万</td><td class='td1'>" + json.buy_amount + "</td><td class='td1'>" + json.buy_amt_pj + "</td><td class='td1'>" + json.hsi_buy_user + "</td><td class='td1'>" + json.hsi_buy_ss + "</td><td class='td1'>" + json.hsi_buy_ss_pj + "</td><td class='td1'>" + json.hsi_buy_amount + "</td><td class='td1'>" + json.hsi_buy_amt_pj + "</td><td class='td1'>" + dict_GetValue(dict_cmp_json, json.cmp_id) + "</td></tr>";
            break;
        case "stock_plan_day_list":
            person += json.person; cs += json.cs; money_debt = json.money_debt; money_margin += json.money_margin;
            profit_earn += json.profit_earn; profit_loss += json.profit_loss; start_fee += json.start_fee; delay_fee += json.delay_fee;
            delay_ref_fee += json.delay_ref_fee; cc_loss += json.cc_loss; tp_loss += json.tp_loss;
            html = "<tr><td class='td2'>" + get_date_str(json.sta_day) + "</td><td class='td1'>" + json.person + "</td><td class='td1'>" + json.cs + "</td><td class='td1'>" + parseInt(json.money_debt / 10000) + "&nbsp;万</td><td class='td1'>" + json.money_margin + "</td><td class='td1'>" + json.profit_earn + "</td><td class='td1'>" + json.profit_loss + "</td><td class='td1'>" + json.start_fee + "</td><td class='td1'>" + json.delay_fee + "</td><td class='td1'>" + json.delay_ref_fee + "</td><td class='td1'>" + json.cc_loss + "</td><td class='td1'>" + json.tp_loss + "</td><td class='td1'>" + dict_GetValue(dict_cmp_json, json.cmp_id) + "</td></tr>";
            break;
        case "hsi_day_sta_list":
            html = "<tr><td class='td2'>" + get_date_str(json.sta_day) + "</td><td class='td1'>" + json.person + "</td><td class='td1'>" + json.money_margin + "</td><td class='td1'>" + parseInt(json.money_debt / 10000) + "&nbsp;万</td><td class='td1'>" + json.amount + "</td><td class='td1'>" + json.profit + "</td><td class='td1'>" + json.cmp_earn + "</td><td class='td1'>" + json.cc_loss + "</td><td class='td1'>" + json.oper_amount + "</td><td class='td1'>" + dict_GetValue(dict_cmp_json, json.cmp_id) + "</td></tr>";
            break;
        case "notice_list":
            html += "<tr>";
            html += "   <td class='td2'>" + json.id + "</td>";
            html += "   <td class='td1'>" + json.head + "</td>";
            html += "   <td class='td1'>" + json.create_time + "</td>";
            html += "   <td class='td1'><a href='../../noti/main/edit.aspx?id=" + json.id + "' style='color:blue;font-weight:bolder'>修改</a></td>";
            html += "   <td class='td1'><a href='javascript:void(window.open(\"../../noti/main/view.aspx?id=" + json.id + "\"))' style='color:blue;font-weight:bolder'>预览</a></td>";
            html += "   <td class='td1'><a href='javascript:void' onclick='notice_Delete(\"\", " + json.id + ")' style='color:blue;font-weight:bolder'>删除</a></td>";
            html += "</tr>";
            break;
        case "stock_plan_xrd_list":
            var ass = "-";
            if (json.process_status_id == 3) {
                ass = "<a href='javascript:void(0)' onclick='stock_plan_xrd_Cancel(\"\", " + json.id + ")' style='color:blue;font-weight:bolder'>撤销</a>";
            }
            html = "<tr><td class='td2'>" + json.id + "</td><td class='td1'>" + json.stock_no + "</td><td class='td1'>" + json.stock_name + "</td><td class='td1'>" + json.amount + "</td><td class='td1'>" + (json.process_status_id == 3 ? "完成" : "撤销") + "</td><td class='td1'>" + (json.process_status_id == 3 ? json.create_time : json.cancel_time) + "</td><td class='td1'>" + ass + "</td></tr>";
            break;
        default:
            break;
    }
    return html;
}
function ws_qry_sucFunc_last(page, html) {
    switch (page) {
        case "stock_plan_day_list":
            html += "<tr><td class='td2'>总计：</td><td class='td1'>" + person + "</td><td class='td1'>" + cs + "</td><td class='td1'>" + parseInt(money_debt / 10000) + "&nbsp;万</td><td class='td1'>" + money_margin + "</td><td class='td1'>" + formatMoney(profit_earn) + "</td><td class='td1'>" + formatMoney(profit_loss) + "</td><td class='td1'>" + start_fee + "</td><td class='td1'>" + delay_fee + "</td><td class='td1'>" + delay_ref_fee + "</td><td class='td1'>" + cc_loss + "</td><td class='td1'>" + tp_loss + "</td><td class='td1'>&nbsp;</td></tr>";
            break;
        default:
            break;
    }
    return html;
}