function case_evidence_Insert(btn, case_info_id, name, evi_type, description, remark) {
    ws_exec(btn, "POST", "CaseEvidence", "Insert", {
        "case_info_id": case_info_id, "name": name, "evi_type": evi_type, "description": description, "remark": remark,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function case_evidence_Update(btn, id, name, evi_type, description, remark) {
    ws_exec(btn, "PUT", "CaseEvidence", "Update", {
        "id": id, "name": name, "evi_type": evi_type, "description": description, "remark": remark,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function case_evidence_Delete(btn, id) {
    if (!confirm("确定删除？")) return;
    ws_exec(btn, "DELETE", "CaseEvidence", "Delete", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function case_evidence_GetOne(id) {
    ws_get_json("", "CaseEvidence", "GetOne", {
        "id": id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", case_evidence_GetOne_sucFunc);
}
function case_evidence_GetOne_sucFunc(table, page, json) {
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
function case_evidence_GetList(btn, table, page, case_info_id) {
    ws_qry(btn, table, page, "CaseEvidence", "GetList", {
        "case_info_id": case_info_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}