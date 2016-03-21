function case_sample_Insert(btn, case_info_id, number, name, sample_type,
                description, remark, ref_table, ref_id) {
    ws_exec(btn, "POST", "CaseSample", "Insert", {
        "case_info_id": case_info_id, "number": number, "name": name, "sample_type": sample_type, "description": description,
        "remark": remark, "ref_table": ref_table, "ref_id": ref_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function case_sample_Update(btn, id, number, name, sample_type, description, remark) {
    ws_exec(btn, "PUT", "CaseSample", "Update", {
        "id": id, "number": number, "name": name, "sample_type": sample_type, "description": description,
        "remark": remark, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function case_sample_Accept(btn, id, id_status_id, accept_remark) {
    if (!confirm("确定提交？")) return;
    ws_exec(btn, "DELETE", "CaseSample", "Accept", {
        "id": id, "id_status_id": id_status_id, "accept_remark": accept_remark,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function case_sample_Delete(btn, id) {
    if (!confirm("确定删除？")) return;
    ws_exec(btn, "DELETE", "CaseSample", "Delete", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function case_sample_GetOne(id) {
    ws_get_json("", "CaseSample", "GetOne", {
        "id": id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", case_sample_GetOne_sucFunc);
}
function case_sample_GetOne_sucFunc(table, page, json) {
    if (json != null) {
        fill_input_nde("所属单位i", json.dept_no);
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
function case_sample_GetList(btn, table, page, case_info_id, number, sample_type,
                id_status_id, ref_table, ref_id, accept_user_id, page_size, page_index) {
    ws_qry(btn, table, page, "CaseSample", "GetList", {
        "case_info_id": case_info_id, "number": number, "sample_type": sample_type, "id_status_id": id_status_id,
        "ref_table": ref_table, "ref_id": ref_id, "accept_user_id": accept_user_id,
        "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function case_sample_GetCount(case_info_id, number, sample_type,
                id_status_id, ref_table, ref_id, accept_user_id) {
    ws_exec("", "GET", "CaseSample", "GetCount", {
        "case_info_id": case_info_id, "number": number, "sample_type": sample_type, "id_status_id": id_status_id,
        "ref_table": ref_table, "ref_id": ref_id, "accept_user_id": accept_user_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}