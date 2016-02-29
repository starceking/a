function sys_user_Login(btn, login_name, login_pwd) {
    ws_get_json(btn, "SysUser", "Login", { "login_name": login_name, "login_pwd": login_pwd }, "", "", sys_user_Login_sucFunc);
}
function sys_user_Login_sucFunc(table, page, json) {
    if (json != null) {
        addCookie("sys_user_id", json.id, 0, 1);
        addCookie("sys_user_name", json.name, 0, 1);
        addCookie("sys_access_id", json.access_id, 0, 1);
        addCookie("sys_access_token", json.access_token, 0, 1);
        addCookie("sys_is_investor", json.user_id, 0, 1);
        addCookie("sys_privilege_ids", json.privilege_ids, 0, 1);
        addCookie("sys_cmp_id", json.cmp_id, 0, 1);
        top.location = "index.htm?v=" + (new Date).toLocaleString();
    }
    else alert("账号或密码错误");
}
function sys_user_Insert(btn, login_name, login_pwd, mobile, name, user_id) {
    ws_exec(btn, "POST", "SysUser", "Insert", {
        "login_name": login_name, "login_pwd": login_pwd, "mobile": mobile, "name": name, "user_id": user_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function sys_user_Update(btn, id, mobile, name, delete_flag) {
    ws_exec(btn, "PUT", "SysUser", "Update", {
        "id": id, "mobile": mobile, "name": name, "delete_flag": delete_flag,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function sys_user_InsertSys(btn, login_name, login_pwd, mobile, name, privilege_ids, cmp_id) {
    ws_exec(btn, "POST", "SysUser", "InsertSys", {
        "login_name": login_name, "login_pwd": login_pwd, "mobile": mobile, "name": name, "privilege_ids": privilege_ids,
        "cmp_id": cmp_id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function sys_user_UpdateSys(btn, id, mobile, name, privilege_ids, cmp_id, delete_flag) {
    ws_exec(btn, "PUT", "SysUser", "UpdateSys", {
        "id": id, "mobile": mobile, "name": name, "privilege_ids": privilege_ids, "cmp_id": cmp_id, "delete_flag": delete_flag,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function sys_user_UpdatePwd(btn, id, login_pwd, login_pwd_old) {
    ws_exec(btn, "PUT", "SysUser", "UpdatePwd", {
        "id": id, "login_pwd": login_pwd, "name": login_pwd_old,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "关闭");
}
function sys_user_ResetPwd(btn, id, login_pwd) {
    if (!confirm("确定重置？新密码为 1")) return;
    ws_exec(btn, "PUT", "SysUser", "ResetPwd", {
        "id": id, "login_pwd": login_pwd,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "重置成功，新密码为 1", "");
}
function sys_user_GetOne(id) {
    ws_get_json("", "SysUser", "GetOne", {
        "id": id, "login_name": "", "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", sys_user_GetOne_sucFunc);
}
function sys_user_GetOne_sucFunc(table, page, json) {
    if (json != null) {
        fill_input_nde("用户名i", json.login_name);
        fill_input_nde("电话i", json.mobile);
        fill_input_nde("姓名i", json.name);
        fill_label_nde("客户ID", json.user_id);
        dict_GetAll("所属s", "请选择", json.cmp_id, dict_cmp_json);
        fill_select_nde("状态s", json.delete_flag);
        if (json.privilege_ids) {
            var arr = json.privilege_ids.split(',');
            for (var i = 0; i < arr.length; i++) {
                fill_a_attr("pcb" + arr[i], "checked", "checked");
            }
        }
    }
}
function sys_user_GetList(btn, table, page, login_name, mobile, name, user_id, page_size, page_index) {
    ws_qry(btn, table, page, "SysUser", "GetList", {
        "login_name": login_name, "mobile": mobile, "name": name, "user_id": user_id,
        "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function sys_user_GetCount(login_name, mobile, name, user_id) {
    ws_exec("", "GET", "SysUser", "GetCount", {
        "login_name": login_name, "mobile": mobile, "name": name, "user_id": user_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}