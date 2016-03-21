function user_Login(btn, police_no, login_pwd) {
    ws_get_json(btn, "User", "Login", { "police_no": police_no, "login_pwd": login_pwd }, "", "", user_Login_sucFunc);
}
function user_Login_sucFunc(table, page, json) {
    if (json != null) {
        addCookie("user_id", json.id, 0, 1);
        addCookie("user_name", json.name, 0, 1);
        addCookie("user_dept_no", json.dept_no, 0, 1);
        addCookie("sys_access_id", json.access_id, 0, 1);
        addCookie("sys_access_token", json.access_token, 0, 1);
        addCookie("sys_auth_ids", json.auth_ids, 0, 1);
        top.location = "index.htm?v=" + (new Date).toLocaleString();
    }
    else alert("账号或密码错误");
}
function user_Insert(btn, dept_no, police_no, login_pwd, name, id_card_no, auth_ids) {
    ws_exec(btn, "POST", "User", "Insert", {
        "dept_no": dept_no, "police_no": police_no, "login_pwd": login_pwd, "name": name, "id_card_no": id_card_no,
        "auth_ids": auth_ids, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function user_Update(btn, id, dept_no, police_no, name, id_card_no, auth_ids) {
    ws_exec(btn, "PUT", "User", "Update", {
        "id": id, "dept_no": dept_no, "police_no": police_no, "name": name, "id_card_no": id_card_no, "auth_ids": auth_ids,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function user_UpdatePwd(btn, id, login_pwd, login_pwd_old) {
    ws_exec(btn, "PUT", "User", "UpdatePwd", {
        "id": id, "login_pwd": login_pwd, "name": login_pwd_old,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "关闭");
}
function user_ResetPwd(btn, id, login_pwd) {
    if (!confirm("确定重置？新密码为 1")) return;
    ws_exec(btn, "PUT", "User", "ResetPwd", {
        "id": id, "login_pwd": login_pwd,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "重置成功，新密码为 1", "");
}
function user_Delete(btn, id) {
    if (!confirm("确定删除？")) return;
    ws_exec(btn, "DELETE", "User", "Delete", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function user_GetOne(id) {
    ws_get_json("", "User", "GetOne", {
        "id": id, "login_name": "", "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", user_GetOne_sucFunc);
}
function user_GetOne_sucFunc(table, page, json) {
    if (json != null) {
        if (json.cg_flag == 0)
            dict_lab_no_GetList("所属单位s", getCookie("user_dept_no", 1), json.dept_no);
        else {
            fill_label_nde("所属单位sid", json.dept_no);
            fill_label_nde("所属单位sname", json.dept_name);
        }
        fill_input_nde("警号i", json.police_no);
        fill_input_nde("姓名i", json.name);
        fill_input_nde("身份证i", json.id_card_no);
        if (json.auth_ids) {
            var arr = json.auth_ids.split(',');
            for (var i = 0; i < arr.length; i++) {
                fill_a_attr("pcb" + arr[i], "checked", "checked");
            }
        }
    }
}
function user_GetList(btn, table, page, dept_no, police_no, name, cg_flag, page_size, page_index) {
    ws_qry(btn, table, page, "User", "GetList", {
        "dept_no": dept_no, "police_no": police_no, "name": name, "cg_flag": cg_flag,
        "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function user_GetCount(dept_no, police_no, name, cg_flag) {
    ws_exec("", "GET", "User", "GetCount", {
        "dept_no": dept_no, "police_no": police_no, "name": name, "cg_flag": cg_flag,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}