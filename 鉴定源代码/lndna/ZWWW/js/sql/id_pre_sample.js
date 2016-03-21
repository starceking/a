function id_pre_sample_Insert(btn, id_amp_id, case_sample_id, id_method, id_result) {
    ws_exec(btn, "POST", "IdPreSample", "Insert", {
        "id_amp_id": id_amp_id, "case_sample_id": case_sample_id, "id_method": id_method, "id_result": id_result,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function id_pre_sample_Update(btn, id, id_method, id_result) {
    ws_exec(btn, "PUT", "IdPreSample", "Update", {
        "id": id, "id_method": id_method, "id_result": id_result,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function id_pre_sample_Delete(btn, id) {
    if (!confirm("确定删除？")) return;
    ws_exec(btn, "DELETE", "IdPreSample", "Delete", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function id_pre_sample_GetOne(id) {
    ws_get_json("", "IdPreSample", "GetOne", {
        "id": id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", id_pre_sample_GetOne_sucFunc);
}
function id_pre_sample_GetOne_sucFunc(table, page, json) {
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
function id_pre_sample_GetList(btn, table, page, id_amp_id) {
    ws_qry(btn, table, page, "IdPreSample", "GetList", {
        "id_amp_id": id_amp_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}