function user_ResetPwd(btn, id, login_pwd) {
    if (!confirm("确定重置？新密码为 123456")) return;
    ws_exec(btn, "PUT", "User", "ResetPwd", {
        "id": id, "login_pwd": login_pwd,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "重置成功", "");
}
function user_GetList(btn, table, page, id, mobile, nick_name, name, id_card_no, create_times, create_timee, ref_id, moneys, moneye, cmp_id, page_size, page_index) {
    ws_qry(btn, table, page, "User", "GetList", {
        "id": id, "mobile": mobile, "nick_name": nick_name, "name": name, "id_card_no": id_card_no,
        "create_times": create_times, "create_timee": create_timee, "ref_id": ref_id,
        "moneys": moneys, "moneye": moneye, "cmp_id": cmp_id, "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function user_GetCount(id, mobile, nick_name, name, id_card_no, create_times, create_timee, ref_id, moneys, moneye, cmp_id) {
    ws_exec("", "GET", "User", "GetCount", {
        "id": id, "mobile": mobile, "nick_name": nick_name, "name": name, "id_card_no": id_card_no,
        "create_times": create_times, "create_timee": create_timee, "ref_id": ref_id, "moneys": moneys, "moneye": moneye,
        "cmp_id": cmp_id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}
function user_Exp(btn, id, mobile, nick_name, name, id_card_no, create_times, create_timee, ref_id, moneys, moneye, cmp_id) {
    ws_qry(btn, "导出结果", "导出数据", "User", "Exp", {
        "id": id, "mobile": mobile, "nick_name": nick_name, "name": name, "id_card_no": id_card_no,
        "create_times": create_times, "create_timee": create_timee, "ref_id": ref_id, "moneys": moneys, "moneye": moneye,
        "cmp_id": cmp_id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function user_GetOne(id) {
    ws_get_json("", "User", "GetOne", {
        "id": id, "mobile": "", "nick_name": "", "id_card_no": "",
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    },
        "", "", user_GetOne_sucFunc);
}
function user_GetOne_sucFunc(table, page, json) {
    if (json != null) {
        if (json.mobile != null) fill_label_nde("电话", json.mobile);
        if (json.nick_name != null) fill_label_nde("昵称", json.nick_name);
        fill_label_nde("注册时间", json.create_time);
        fill_label_nde("用户余额", json.money);
        fill_label_nde("欠费", json.money_npay);
        if (json.name != null) fill_label_nde("姓名", json.name);
        if (json.id_card_no != null) fill_label_nde("身份证", json.id_card_no);
        if (json.gender_id != null) fill_label_nde("性别", dict_GetValue(dict_gender_json, json.gender_id));
        if (json.email != null) fill_label_nde("邮箱", json.email);
        if (json.qq != null) fill_label_nde("QQ", json.qq);
        fill_label_nde("点买额度", json.stock_plan_debt);
        fill_label_nde("方案总数", json.stock_plan_amount);
        //fill_label_nde("正收益额", json.stock_plan_earn);
        //fill_label_nde("负收益额", json.stock_plan_loss);
        fill_select_nde("用户状态s", json.delete_flag);
        fill_input_nde("邀请收益i", json.profit_from_ref);
        fill_input_nde("邀请人i", json.ref_id);
        fill_label_nde("邀请人", json.ref_info);
    }
}
function user_UpdateRef(btn, id, profit_from_ref, ref_id, delete_flag) {
    ws_exec(btn, "PUT", "User", "UpdateRef", {
        "id": id, "profit_from_ref": profit_from_ref, "ref_id": ref_id, "delete_flag": delete_flag,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "");
}
function user_UpdateBatchRef(btn, id, refIds) {
    ws_exec(btn, "PUT", "User", "UpdateBatchRef", {
        "id": id, "name": refIds,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "刷新");
}